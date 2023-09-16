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
            throw new NotImplementedException();
        }

        public decimal IncreasePJSalaryByHourlyRate(int employeeRecordNumber, decimal hourlyRateIncrease)
        {
            throw new NotImplementedException();
        }
    }
}