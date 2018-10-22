using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Employees.Data.Entity;

namespace Employees.Data.Repository
{    
    public interface IEmployeeRepository : IDisposable
    {                
        IEnumerable<Employee> GetEmployees( Func<Employee, bool> filterConditions= null );
        Employee GetEmployee(Func<Employee, bool> filterConditions= null );                                                    
        Employee AddEmployee(Employee model);
        Employee EditEmployee(Employee model);        
        void DeleteEmployee(string cpf);
    }
}