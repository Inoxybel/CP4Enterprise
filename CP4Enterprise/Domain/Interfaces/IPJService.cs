using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface IPJService
    {
        bool IncreasePJSalaryByHourlyRate(decimal hourlyRateIncrease, int employeeRecordNumber = -1);
        List<PJ> GetAllPJEmployees();
    }
}
