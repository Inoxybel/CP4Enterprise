using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class CLTService : ICLTService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CLTService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<CLT> GetAllCLTEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();

            return employees.OfType<CLT>().ToList();
        }

        public decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease)
        {
            if (percentageIncrease <= 0)
                return -1;

            var repositoryResult = _employeeRepository.GetEmployee(employeeRecordNumber);

            if (repositoryResult.Success && repositoryResult.Data is CLT employee)
            {
                employee.Salary += employee.Salary * percentageIncrease / 100;

                var updateResult = _employeeRepository.UpdateEmployee(employee);

                if (!updateResult.Success)
                    return 0;

                return employee.Salary;
            }

            return -1;
        }
    }
}