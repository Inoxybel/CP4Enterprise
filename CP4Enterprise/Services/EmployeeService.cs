using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICLTService cltService;
        private readonly IPJService pjService;

        public EmployeeService()
        {
            cltService = new CLTService();
            pjService = new PJService();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
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