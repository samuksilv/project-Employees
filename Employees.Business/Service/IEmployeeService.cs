using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Data.Dto;
using Employees.Data.Entity;

namespace Employees.Business.Service
{
    public  interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesByName(string name);
        Employee GetEmployeeByCPF(string cpf);
        IEnumerable<Employee> GetEmployeesByOffice(string  office);
        IEnumerable<Employee> GetEmployeesByRegisterDate(string date);
        IEnumerable<Employee> GetEmployeesBySalary(decimal? initialTrack, decimal? finalTrack);        
        IEnumerable<Employee> GetEmployeesByState(string state);        
        IEnumerable<EmployeeGroupByFederatedStateDTO> GetEmployeesByFederatedStateBirth();                
        Employee SaveEmployee(SaveEmployeeDTO model);
        void DeleteEmployee(string cpf);
    }
}