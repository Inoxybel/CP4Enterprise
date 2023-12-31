﻿using CP4Enterprise.CrossCutting.Helpers;
using CP4Enterprise.Domain.Entities;
using CP4Enterprise.Domain.Interfaces;

namespace CP4Enterprise.Services
{
    public class MenuService : IMenuService
    {
        private readonly ICLTService _cltService;
        private readonly IPJService _pjService;
        private readonly IEmployeeService _employeeService;

        public MenuService(
            ICLTService cltService,
            IPJService pjService,
            IEmployeeService employeeService)
        {
            _cltService = cltService;
            _pjService = pjService;
            _employeeService = employeeService;
        }

        public List<CLT> GetAllCLTEmployees()
        {
            List<CLT> employeeList = _cltService.GetAllCLTEmployees();

            return employeeList;
            
        }

        public List<PJ> GetAllPJEmployees()
        {
            List<PJ> employeeList = _pjService.GetAllPJEmployees();

            return employeeList;
        }

        public Result<Employee> GetEmployeeById(int employeeId)
        {
            Result<Employee> employee = _employeeService.GetEmployeeById(employeeId);

            return employee;
        }

        public decimal GetEmployeeMonthlyTotalCost(int employeeId)
        {
            return _employeeService.GetEmployeeMonthlyTotalCost(employeeId);
        }

        public decimal GetTotalMonthlyCost()
        {
           return _employeeService.GetTotalMonthlyCost();
        }

        public decimal IncreaseCLTSalaryByPercentage(int employeeRecordNumber, decimal percentageIncrease)
        {
            var newSalary = _cltService.IncreaseCLTSalaryByPercentage(employeeRecordNumber, percentageIncrease);

            if (newSalary == -1)
                return 0;

            return newSalary;
        }

        public bool IncreasePJSalaryByHourlyRate(decimal hourlyRateIncrease, int employeeRecordNumber = -1)
        {
            return _pjService.IncreasePJSalaryByHourlyRate(hourlyRateIncrease, employeeRecordNumber);
        }

        public Result<bool> CreateEmployee(Employee employee)
        {
            var result = _employeeService.SaveEmployee(employee);

            return result;
        }
    }
}