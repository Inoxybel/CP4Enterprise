using CP4Enterprise.CrossCutting.Constants;
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
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Mock<ICLTService> _cltServiceMock;
        private readonly Mock<IPJService> _pjServiceMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _cltServiceMock = new Mock<ICLTService>();
            _pjServiceMock = new Mock<IPJService>();
            _employeeService = new EmployeeService(_cltServiceMock.Object, _pjServiceMock.Object, _employeeRepositoryMock.Object);
        }

        [Fact]
        public void GetEmployeeMonthlyTotalCost_SHOULD_ReturnNegative_WHEN_EmployeeIdIsZero()
        {
            int employeeId = 0;

            var result = _employeeService.GetEmployeeMonthlyTotalCost(employeeId);

            result.Should().Be(-1);
        }

        [Fact]
        public void GetEmployeeMonthlyTotalCost_SHOULD_ReturnNegative_WHEN_EmployeeNotFound()
        {
            int employeeId = 1;
            _employeeRepositoryMock.Setup(r => r.GetEmployee(It.IsAny<int>())).Returns(new Result<Employee> { Success = false });

            var result = _employeeService.GetEmployeeMonthlyTotalCost(employeeId);

            result.Should().Be(-1);
        }

        [Fact]
        public void GetEmployeeMonthlyTotalCost_SHOULD_ReturnCorrectTotal_WHEN_EmployeeIsCLT()
        {
            int employeeId = 1;
            decimal baseSalary = 1000m;
            var cltEmployee = new CLT("CLT Employee", Gender.Female, baseSalary, true, employeeId);

            _employeeRepositoryMock.Setup(r => r.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee> 
                { 
                    Success = true, 
                    Data = cltEmployee
                });

            var result = _employeeService.GetEmployeeMonthlyTotalCost(employeeId);

            decimal vacationFraction = (baseSalary * 1 / 3m) * Percents.VacationFraction / 100;
            decimal thirteenthSalaryFraction = (baseSalary / 12) * Percents.ThirteenthSalaryFraction / 100;
            decimal FGTS = baseSalary * Percents.FGTS / 100;
            decimal FGTSResignationProvision = baseSalary * Percents.FGTSResignationProvision / 100;
            decimal previdential = baseSalary * Percents.Previdential / 100;

            decimal totalCost = baseSalary + vacationFraction + thirteenthSalaryFraction + FGTS + FGTSResignationProvision + previdential;

            result.Should().Be(totalCost);
        }

        [Fact]
        public void GetEmployeeMonthlyTotalCost_SHOULD_ReturnCorrectTotal_WHEN_EmployeeIsPJ_WithoutExtraHours()
        {
            int employeeId = 2;
            decimal hourlyRate = 50m;
            int hoursWorked = 160;

            var pjEmployee = new PJ("PJ Employee", Gender.Female, hourlyRate, hoursWorked, "CNPJ", employeeId);
            
            _employeeRepositoryMock.Setup(r => r.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee> 
                { 
                    Success = true, 
                    Data = pjEmployee 
                });

            var result = _employeeService.GetEmployeeMonthlyTotalCost(employeeId);

            decimal expectedTotalCost = hourlyRate * hoursWorked;

            result.Should().Be(expectedTotalCost);
        }

        [Fact]
        public void GetEmployeeMonthlyTotalCost_SHOULD_ReturnCorrectTotal_WHEN_EmployeeIsPJ_WithExtraHours()
        {
            int employeeId = 2;
            decimal hourlyRate = 50m;
            int hoursWorked = 160;
            decimal extraHours = 20m;

            var pjEmployee = new PJ("PJ Employee", Gender.Other, hourlyRate, hoursWorked, "CNPJ", employeeId);

            _employeeRepositoryMock.Setup(r => r.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee> 
                { 
                    Success = true, 
                    Data = pjEmployee 
                });

            var result = _employeeService.GetEmployeeMonthlyTotalCost(employeeId, extraHours);

            decimal expectedTotalCost = (hourlyRate * hoursWorked) + (hourlyRate * extraHours);

            result.Should().Be(expectedTotalCost);
        }

        [Fact]
        public void GetTotalMonthlyCost_SHOULD_ReturnSumOfAllEmployeesMonthlyCost_WHEN_EmployeesExist()
        {
            var cltEmployee = new CLT("CLT Employee", Gender.Female, 1000m, true, 1);

            var pjEmployee = new PJ("PJ Employee", Gender.Male, 160m, 50m, "CNPJ", 2);

            var employees = new List<Employee> 
            { 
                cltEmployee, 
                pjEmployee 
            };

            _employeeRepositoryMock.Setup(r => r.GetAllEmployees())
                .Returns(employees);

            _employeeRepositoryMock.SetupSequence(r => r.GetEmployee(It.IsAny<int>()))
                .Returns(new Result<Employee>
                {
                    Success = true,
                    Data = cltEmployee
                })
                .Returns(new Result<Employee>
                {
                    Success = true,
                    Data = pjEmployee
                });

            var result = _employeeService.GetTotalMonthlyCost();

            decimal baseSalary = cltEmployee.Salary;
            decimal vacationFraction = (baseSalary * 1 / 3m) * Percents.VacationFraction / 100;
            decimal thirteenthSalaryFraction = (baseSalary / 12) * Percents.ThirteenthSalaryFraction / 100;
            decimal FGTS = baseSalary * Percents.FGTS / 100;
            decimal FGTSResignationProvision = baseSalary * Percents.FGTSResignationProvision / 100;
            decimal previdential = baseSalary * Percents.Previdential / 100;

            decimal totalCltCost = baseSalary + vacationFraction + thirteenthSalaryFraction + FGTS + FGTSResignationProvision + previdential;

            decimal expectedPJTotalCost = pjEmployee.HourValue * pjEmployee.HourWorked;
            decimal expectedTotalCost = totalCltCost + expectedPJTotalCost;

            result.Should().Be(expectedTotalCost);
        }
    }
}
