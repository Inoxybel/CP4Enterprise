using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface ICLTService
    {
        decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease);
        List<CLT> GetAllCLTEmployees();
    }
}
