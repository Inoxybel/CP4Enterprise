using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface IEmployeeService
    {
        decimal GetTotalMonthlyCost();
        Employee GetEmployeeById(int employeeId);
        decimal GetEmployeeMonthlyTotalCost(int employeeId);
    }
}
