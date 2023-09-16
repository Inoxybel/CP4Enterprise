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

        public decimal GetEmployeeMonthlyTotalCost(int employeeId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalMonthlyCost()
        {
            throw new NotImplementedException();
        }
    }
}