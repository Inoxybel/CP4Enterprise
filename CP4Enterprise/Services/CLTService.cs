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

            var clts = employees.OfType<CLT>().ToList();

            return clts;
        }

        public decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease)
        {
            throw new NotImplementedException();
        }
    }
}