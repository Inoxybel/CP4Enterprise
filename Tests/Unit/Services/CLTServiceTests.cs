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
    public class CLTServiceTests
    {
        [Fact]
        public void GetAllCLTEmployees_SHOULD_ReturnOnlyCLTEmployees_WHEN_Called()
        {
            var mockRepository = new Mock<IEmployeeRepository>();

            mockRepository.Setup(repo => repo.GetAllEmployees())
                .Returns(new List<Employee>
                {
                    new CLT("CLT Employee 1", Gender.Male, 3000, true, 1),
                    new CLT("CLT Employee 2", Gender.Female, 4000, true, 2),
                    new PJ("PJ Employee", Gender.Male, 50, 160, "123456789", 3)
                });

            var service = new CLTService(mockRepository.Object);

            var result = service.GetAllCLTEmployees();

            result.Should().HaveCount(2);
            result.Should().AllBeOfType<CLT>();
        }

        [Fact]
        public void IncreaseCLTSalaryByPercentage_SHOULD_IncreaseSalary_WHEN_ValidEmployeeAndPercentage()
        {
            var mockRepository = new Mock<IEmployeeRepository>();

            mockRepository.Setup(repo => repo.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee>
                {
                    Success = true, 
                    Data = new CLT("CLT Employee 1", Gender.Male, 3000, true, 1)
                });

            mockRepository.Setup(repo => repo.UpdateEmployee(It.IsAny<CLT>()))
                .Returns(new Result<bool>
                {
                    Success = true,
                    Data = true
                });

            var service = new CLTService(mockRepository.Object);

            var newSalary = service.IncreaseCLTSalaryByPercentage(1, 10);

            newSalary.Should().Be(3300);
        }
    }
}
