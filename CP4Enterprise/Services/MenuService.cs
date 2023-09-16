using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class MenuService : IMenuService
    {
        public List<CLT> GetAllCLTEmployees()
        {
            throw new NotImplementedException();
        }

        public List<PJ> GetAllPJEmployees()
        {
            throw new NotImplementedException();
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

        public decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease)
        {
            throw new NotImplementedException();
        }

        public decimal IncreasePJSalaryByHourlyRate(int employeeRecordNumber, decimal hourlyRateIncrease)
        {
            throw new NotImplementedException();
        }
    }
}