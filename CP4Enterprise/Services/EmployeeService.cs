using CP4Enterprise.CrossCutting.Constants;
using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICLTService _cltService;
        private readonly IPJService _pjService;

        public EmployeeService(
            ICLTService cltService,
            IPJService pjService,
            IEmployeeRepository employeeRepository)
        {
            _cltService = cltService;
            _pjService = pjService;
            _employeeRepository = employeeRepository;
        }

        public Result<Employee> GetEmployeeById(int employeeId)
        {
            var repositoryResult = _employeeRepository.GetEmployee(employeeId);

            return new()
            {
                Success = repositoryResult.Success,
                Data = repositoryResult.Data,
                ErrorMessage = repositoryResult.ErrorMessage
            };
        }

        public decimal GetEmployeeMonthlyTotalCost(int employeeId, decimal extraHours = 0m)
        {
            if (employeeId <= 0)
                return -1;

            var repositoryResult = _employeeRepository.GetEmployee(employeeId);

            if (!repositoryResult.Success)
                return -1;

            var employee = repositoryResult.Data;

            if (employee is CLT cltEmployee)
            {
                decimal baseSalary = cltEmployee.Salary;

                decimal totalCost = baseSalary +
                    (baseSalary * Percents.VacationFraction / 100) +
                    (baseSalary * Percents.ThirteenthSalaryFraction / 100) +
                    (baseSalary * Percents.FGTS / 100) +
                    (baseSalary * Percents.FGTSResignationProvision / 100) +
                    (baseSalary * Percents.Previdential / 100);

                return totalCost;
            }
            else if (employee is PJ pjEmployee)
            {
                decimal baseCost = pjEmployee.HourValue * pjEmployee.HourWorked;
                decimal extraHoursCost = pjEmployee.HourValue * extraHours;

                return baseCost + extraHoursCost;
            }

            return -1;
        }

        public decimal GetTotalMonthlyCost()
        {
            var employees = _employeeRepository.GetAllEmployees();

            if (!employees.Any())
                return -1;

            decimal totalMonthlyCost = 0m;

            foreach (var employee in employees)
            {
                var monthlyCost = GetEmployeeMonthlyTotalCost(employee.Register);

                if (monthlyCost != -1)
                {
                    totalMonthlyCost += monthlyCost;
                }
            }

            return totalMonthlyCost;
        }

        public Result<bool> SaveEmployee(Employee employee)
        {
            var saveResult = _employeeRepository.SaveEmployee(employee);

            return new()
            {
                Success = saveResult.Success,
                ErrorMessage = saveResult.ErrorMessage,
                Data = saveResult.Data
            };
        }
    }
}