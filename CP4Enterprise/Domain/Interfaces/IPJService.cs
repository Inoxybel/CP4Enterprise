using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface IPJService
    {
        decimal IncreasePJSalaryByHourlyRate(int employeeRecordNumber, decimal hourlyRateIncrease);
        List<PJ> GetAllPJEmployees();
    }
}
