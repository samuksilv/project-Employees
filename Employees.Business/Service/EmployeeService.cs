using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Data.Dto;
using Employees.Data.Entity;
using Employees.Data.Repository;

namespace Employees.Business.Service
{
    public class EmployeeService : IEmployeeService
    {     
        #region Properties
        private static IEmployeeRepository _repository;    

        #endregion 

        #region Ctor
        public EmployeeService()
        {
            if(_repository== null)
                _repository= new EmployeeRepository();
        }

        #endregion 

        #region Methods

        #region Get's
        public IEnumerable<Employee> GetEmployees( )
        {
            try{
                return _repository.GetEmployees();
            }catch(Exception ex){
                throw ex;
            }
        }
        public   Employee GetEmployeeByCPF(string cpf)
        {
            try{                
                return _repository.GetEmployee(emp=> emp.Cpf==cpf);
            }catch(Exception ex){
                throw ex;
            }
        }

        public IEnumerable<EmployeeGroupByFederatedStateDTO> GetEmployeesByFederatedStateBirth()
        {
            try{
                var response= _repository.GetEmployees() ; 
                //groups by Federated State of Birth and then order by amount            
                return response.GroupBy(x=> new {x.FederatedStateBirth} )
                        .Select(x=> new EmployeeGroupByFederatedStateDTO{ 
                            Count= x.Count(),
                            FederatedState= x.Key.FederatedStateBirth
                        });
            }catch(Exception ex){
                throw ex;
            }            
        }

        public IEnumerable<Employee> GetEmployeesByName(string name)
        {
            try{
                return _repository.GetEmployees(emp=> emp.Name.ToLower().Contains(name.ToLower()));
            }catch(Exception ex){
                throw ex;
            }
        }

        public IEnumerable<Employee> GetEmployeesByOffice(string office)
        {
            try{
                return _repository.GetEmployees(emp=> emp.Office.ToLower().Contains(office.ToLower()));
            }catch(Exception ex){
                throw ex;
            }
        }

        public IEnumerable<Employee> GetEmployeesByState(string state)
        {
            return _repository.GetEmployees(emp=> state.ToLower() == emp.State.ToLower());
        }

        public  IEnumerable<Employee> GetEmployeesByRegisterDate(string date)
        {
            try{
                if(string.IsNullOrEmpty(date))
                    return _repository.GetEmployees();

                DateTime dateFilter=DateTime.Parse(date,System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));            
                return _repository.GetEmployees(emp=> emp.RegisterDate==dateFilter);                    
            }catch(Exception ex){
                throw ex;
            }
        }

        public  IEnumerable<Employee> GetEmployeesBySalary(decimal? initialTrack, decimal? finalTrack)
        {
            try{
                if((initialTrack== null|| initialTrack == 0 ) && (finalTrack== null || finalTrack==0))
                    return _repository.GetEmployees();
                else if((initialTrack!= null|| initialTrack > 0 ) && (finalTrack== null || finalTrack==0))
                    return  _repository.GetEmployees(emp=> emp.Salary>= initialTrack );
                else if((initialTrack== null|| initialTrack == 0 ) && (finalTrack!= null || finalTrack>0))
                    return  _repository.GetEmployees(emp=> emp.Salary<= finalTrack);
                else
                    return  _repository.GetEmployees(emp=> 
                        emp.Salary>= initialTrack &&
                        emp.Salary<= finalTrack
                    );
            }catch(Exception ex){
                throw ex;
            }
        }
        #endregion
            
        #region Post/Put
        public  Employee SaveEmployee(SaveEmployeeDTO model)
        {
            try{
                if(this.GetEmployeeByCPF(model.Cpf)== null)
                    return _repository.AddEmployee(ConvertDto(model));
                else
                    return _repository.EditEmployee(ConvertDto(model));
            }catch(Exception ex){
                throw ex;
            }

        }            
        #endregion

        #region Delete
        
        public void DeleteEmployee(string cpf)
        {
            try{
                if(! string.IsNullOrEmpty(cpf))
                    _repository.DeleteEmployee(cpf);
            }catch(Exception ex){
                throw ex;
            }            
        }

        #endregion

        #region Auxiliary methods
        private Employee ConvertDto (SaveEmployeeDTO dto){  
            Employee employee= this.GetEmployeeByCPF(dto.Cpf); 
            if(employee== null)                    
                return new Employee{
                    Cpf= dto.Cpf,
                    Salary= dto.Salary,
                    Name= dto.Name,
                    State= dto.State,
                    Office= dto.Office,
                    RegisterDate= DateTime.Now,
                    FederatedStateBirth= dto.FederatedStateBirth                                      
                };
            else
                return new Employee{
                    Cpf= dto.Cpf,
                    Salary= dto.Salary,
                    Name= dto.Name,
                    State= dto.State,
                    Office= dto.Office,
                    RegisterDate= employee.RegisterDate ,
                    FederatedStateBirth= dto.FederatedStateBirth 
                };
        }
        
        #endregion

        #endregion

    }
}