using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class PJService : IPJService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public PJService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<PJ> GetAllPJEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();

            return employees.OfType<PJ>().ToList();
        }

        public bool IncreasePJSalaryByHourlyRate(decimal hourlyRateIncrease, int employeeRecordNumber = -1)
        {
            if (hourlyRateIncrease <= 0)
                return false;

            if(employeeRecordNumber == -1)
            {
                var employees = GetAllPJEmployees();

                foreach(var employee in employees)
                {
                    employee.HourValue += hourlyRateIncrease;

                    var _ = _employeeRepository.UpdateEmployee(employee);
                }

                return true;
            }
            else
            {
                var repositoryResult = _employeeRepository.GetEmployee(employeeRecordNumber);

                if(repositoryResult.Success && repositoryResult.Data is PJ employee)
                {
                    employee.HourValue += hourlyRateIncrease;

                    var _ = _employeeRepository.UpdateEmployee(employee);

                    return true;
                }

                return false;
            }
        }
    }
}