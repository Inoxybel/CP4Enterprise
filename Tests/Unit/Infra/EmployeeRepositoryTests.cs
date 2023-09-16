using CP4Enterprise.CrossCutting.Enums;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Infra;
using FluentAssertions;
using Xunit;

namespace Tests.Unit.Infra
{
    public class EmployeeRepositoryTests
    {
        private EmployeeRepository _employeeRepository;

        public EmployeeRepositoryTests()
        {
            _employeeRepository = new EmployeeRepository();
        }

        [Fact]
        public void SaveEmployee_SHOULD_ReturnSuccess_TRUE_When_EmployeeIsSavedSuccessfully()
        {
            var employee = new CLT("CLT Employee", Gender.Female, 3000m, false, 1);

            var result = _employeeRepository.SaveEmployee(employee);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public void UpdateEmployee_SHOULD_ReturnSuccess_TRUE_When_EmployeeIsUpdatedSuccessfully()
        {
            var employee = new CLT("CLT Employee", Gender.Female, 3000m, false, 1);

            _employeeRepository.SaveEmployee(employee);

            employee.Name = "CLT Employee Updated";

            var result = _employeeRepository.UpdateEmployee(employee);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
        }

        [Fact]
        public void UpdateEmployee_SHOULD_ReturnSuccess_FALSE_When_EmployeeDoesNotExist()
        {
            var employee = new CLT("Ghost CLT Employee", Gender.Other, 3000m, false, 999);

            var result = _employeeRepository.UpdateEmployee(employee);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Employee not founded.");
        }

        [Fact]
        public void GetEmployee_SHOULD_ReturnSuccess_TRUE_When_EmployeeExists()
        {
            var employee = new CLT("CLT Employee", Gender.Female, 3000m, true, 1);

            _employeeRepository.SaveEmployee(employee);

            var result = _employeeRepository.GetEmployee(employee.Register);

            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(employee);
        }

        [Fact]
        public void GetEmployee_SHOULD_ReturnSuccess_FALSE_When_EmployeeDoesNotExist()
        {
            var result = _employeeRepository.GetEmployee(999);

            result.Success.Should().BeFalse();
            result.ErrorMessage.Should().Be("Employee doesn't exists.");
        }

        [Fact]
        public void GetAllEmployees_SHOULD_ReturnAllSavedEmployees()
        {
            var employee1 = new CLT("CLT Employee 1", Gender.Male, 3000m, true, 1);
            var employee2 = new CLT("CLT Employee 2", Gender.Female, 4000m, false, 1);

            _employeeRepository.SaveEmployee(employee1);
            _employeeRepository.SaveEmployee(employee2);

            var result = _employeeRepository.GetAllEmployees();

            result.Should().Contain(employee1);
            result.Should().Contain(employee2);
        }
    }
}
