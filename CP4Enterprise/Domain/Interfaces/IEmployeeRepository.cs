using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;

namespace CP4Enterprise.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Result<int> SaveEmployee(Employee employee);
        Result<Employee> GetEmployee(int register);
        List<Employee> GetAllEmployees();
    }
}
