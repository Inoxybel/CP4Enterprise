using CP4Enterprise.CrossCutting.Enums;
using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;
using CP4Enterprise.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.Unit.Services
{
    public class PJServiceTests
    {
        [Fact]
        public void GetAllPJEmployees_SHOULD_ReturnOnlyPJEmployees_WHEN_Called()
        {
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            
            mockEmployeeRepository.Setup(repo => repo.GetAllEmployees()).Returns(new List<Employee>
            {
                new PJ(
                    register: 1, 
                    name: "PJ1",
                    gender: Gender.Male,
                    hourValue: 20, 
                    hourWorked: 40, 
                    cnpj: "1234567890123"
                ),
                new PJ(
                    register: 2, 
                    name: "PJ2", 
                    gender: Gender.Female, 
                    hourValue: 25, 
                    hourWorked: 30, 
                    cnpj: "3210987654321"
                ),
                new CLT(
                    register: 3,
                    name: "CLT1",
                    gender: Gender.Other,
                    salary: 3000,
                    trusted: true
                )
            });
            var service = new PJService(mockEmployeeRepository.Object);

            var result = service.GetAllPJEmployees();

            result.Should().HaveCount(2);
            result.All(e => e is PJ).Should().BeTrue();
        }

        [Theory]
        [InlineData(5, 1, 25)]
        [InlineData(10, -1, 30)]
        public void IncreasePJSalaryByHourlyRate_SHOULD_IncreaseHourlyRate_WHEN_ValidInput(decimal hourlyRateIncrease, int employeeRecordNumber, decimal expectedHourValue)
        {
            var mockEmployeeRepository = new Mock<IEmployeeRepository>();

            var pj1 = new PJ(
                    register: 1,
                    name: "PJ1",
                    gender: Gender.Male,
                    hourValue: 20,
                    hourWorked: 40,
                    cnpj: "1234567890123"
                );

            mockEmployeeRepository.Setup(repo => repo.GetAllEmployees()).Returns(new List<Employee>
            {
                pj1,
                new PJ(
                    register: 2, 
                    name: "PJ2", 
                    gender: Gender.Female, 
                    hourValue: 25, 
                    hourWorked: 30, 
                    cnpj: "3210987654321"
                )
            });

            mockEmployeeRepository.Setup(repo => repo.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee>
                {
                    Success = true,
                    Data = pj1
                });
            
            var service = new PJService(mockEmployeeRepository.Object);

            var result = service.IncreasePJSalaryByHourlyRate(hourlyRateIncrease, employeeRecordNumber);

            result.Should().Be(expectedHourValue);
        }
    }
}
