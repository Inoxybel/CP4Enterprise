using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;
using CP4Enterprise.Infra;

namespace CP4Enterprise.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICLTService cltService;
        private readonly IPJService pjService;

        public EmployeeService()
        {
            cltService = new CLTService();
            pjService = new PJService();
            employeeRepository = new EmployeeRepository();
        }

        public Result<Employee> GetEmployeeById(int employeeId)
        {
            var repositoryResult = employeeRepository.GetEmployee(employeeId);

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