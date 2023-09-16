using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface IMenuService
    {
        List<CLT> GetAllCLTEmployees();
        List<PJ> GetAllPJEmployees();
        decimal GetTotalMonthlyCost();
        decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease);
        decimal IncreasePJSalaryByHourlyRate(int employeeRecordNumber, decimal hourlyRateIncrease);
        Employee GetEmployeeById(int employeeId);
        decimal GetEmployeeMonthlyTotalCost(int employeeId);
    }

}
