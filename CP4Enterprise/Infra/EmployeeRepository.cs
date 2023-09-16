using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Infra
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employees = new();
        private int RegisterCount = 1;

        public Result<bool> SaveEmployee(Employee employee)
        {
            try
            {
                employee.Register = RegisterCount++;

                employees.Add(employee);

                return new()
                {
                    Success = true,
                    Data = true
                };
            }
            catch (Exception)
            {
                return new()
                {
                    Success = false,
                    ErrorMessage = "An error occurred while saving the employee"
                };
            }
        }

        public Result<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                var employeeIndex = employees.IndexOf(employee);

                if (employeeIndex == -1)
                    return new()
                    {
                        Success = false,
                        Data = false,
                        ErrorMessage = "Employee not founded."
                    };

                employees[employeeIndex] = employee;

                return new()
                {
                    Success = true,
                    Data = true
                };
            }
            catch(Exception ex)
            {
                return new()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public Result<Employee> GetEmployee(int register)
        {
            var employee = employees.FirstOrDefault(e => e.Register == register);

            if (employee is null)
                return new()
                {
                    Success = false,
                    ErrorMessage = "Employee doesn't exists."
                };

            return new()
            {
                Success = true,
                Data = employee
            };
        }

        public List<Employee> GetAllEmployees() => employees;
    }
}
