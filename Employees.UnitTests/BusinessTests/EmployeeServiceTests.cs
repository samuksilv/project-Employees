using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Employees.Business.Service;
using Employees.Data.Dto;
using Employees.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Employees.UnitTests.BusinessTests
{
    [TestClass]
    public class EmployeeServiceTests
    {        
        #region Propesties
        private static IEmployeeService _service;
            
        #endregion

        #region Ctor
            
        public EmployeeServiceTests(){
            if(_service==null)
                _service = new EmployeeService();
        }

        #endregion

        #region Test's Service

        #region Get's Methods

        [Theory]
        [InlineData("Adam")]
        [InlineData("Ana")]
        [InlineData("Bertram")]
        public void GetEmployeeByNameAsync(string name){

            #region Act
            var result= _service.GetEmployeesByName(name);

            #endregion

            #region Assert 
            foreach (var item in result)
            {
                Xunit.Assert.True( item.Name.ToLower().Contains(name.ToLower()) );                    
            }          
            #endregion
        }
        
        [Theory]
        [InlineData("Analista")]
        [InlineData("Jr")]
        [InlineData("Dev Jr")]
        public void GetEmployeesByOffice(string office){
            #region Arrange            
            #endregion

            #region Act
            var result= _service.GetEmployeesByOffice(office);

            #endregion

            #region Assert 
            foreach (var item in result)
            {
                Xunit.Assert.True( item.Office.ToLower().Contains(office.ToLower()) );                    
            }    
            #endregion
        }
        
        [Theory]
        [InlineData("24/08/2018")]
        [InlineData("15/04/2017")]
        [InlineData("18/04/2017")]
        public void GetEmployeesByRegisterDate(string date){
            #region Arrange
            
            #endregion

            #region Act
            var result= _service.GetEmployeesByRegisterDate(date);
            #endregion

            #region Assert                        
            foreach (var item in result)
            {
                Xunit.Assert.True( item.RegisterDate.ToString("dd/MM/yyyy") == date );                    
            }   
            #endregion
        }
                
        [Theory]
        [InlineData("00117776300")]
        [InlineData("62924550840")]
        [InlineData("89726570859")]
        public void GetEmployeeByCPF(string cpf){
            #region Arrange
            #endregion

            #region Act
            var result= _service.GetEmployeeByCPF(cpf);

            #endregion

            #region Assert 
            Xunit.Assert.NotNull(result);        
            Xunit.Assert.Equal(  result?.Cpf, cpf );
            #endregion
        }

        [Theory]
        [InlineData("BLOQUEADO")]
        [InlineData("ATIVO")]
        [InlineData("INATIVO")]
        public void GetEmployeesByState(string state){
            #region Arrange
            #endregion

            #region Act
            var result= _service.GetEmployeesByState(state);

            #endregion

            #region Assert 
            foreach (var item in result)
            {
                Xunit.Assert.True( item.State.ToUpper() == state );                    
            } 
            #endregion
        }
        
        [Fact]
        public void GetEmployees(){
            #region Arrange
            #endregion

            #region Act
            var result= _service.GetEmployees(  );

            #endregion

            #region Assert 
                       
            Xunit.Assert.NotNull(result );
            #endregion
        }
            
        #endregion        

        #region Delete Method
        [Theory]
        [InlineData("95267437336")]
        [InlineData("17159509684")]
        [InlineData("85235708709")]
        void DeleteEmployee(string cpf){
            #region Arrange
            #endregion

            #region Act
            _service.DeleteEmployee(cpf);
            #endregion

            #region Assert      

            // verify that the employee has been deleted 
            Xunit.Assert.Null(_service.GetEmployeeByCPF(cpf) );
            #endregion
        }

        #endregion

        #region Add Method
        
        [Theory]
        [MemberData(nameof(EmployeesToAdd))]
        void AddEmployee(SaveEmployeeDTO model){
            #region Arrange           
            #endregion
            #region Act
            Employee employee= _service.SaveEmployee(model);
            #endregion

            #region Assert 
             
            Xunit.Assert.NotNull(employee);          

            // verify that the employee has been added 
            Xunit.Assert.NotNull(_service.GetEmployeeByCPF(model.Cpf) );

            // verify that the employee has been added 
            Xunit.Assert.Equal(employee, _service.GetEmployeeByCPF(model.Cpf) );

            #endregion
        }

        #endregion        

        #region Put Method
         [Theory]
        [MemberData(nameof(EmployeesToAdd))]
        void EditEmployee(SaveEmployeeDTO model){
            #region Arrange           
            #endregion
            #region Act
            Employee employee= _service.SaveEmployee(model);
            #endregion

            #region Assert 
             
            Xunit.Assert.NotNull(employee);          

            // verify that the employee has been added 
            Xunit.Assert.NotNull(_service.GetEmployeeByCPF(model.Cpf) );

            // verify that the employee has been added 
            Xunit.Assert.Equal(employee, _service.GetEmployeeByCPF(model.Cpf) );

            #endregion
        }
        #endregion

        #endregion
    
        #region Auxiliary methods

        public static IEnumerable<object[]> EmployeesToAdd() => 
            new List<object[]>
            {
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "77777777777",
                        Office= "AC Sr",
                        Name= "Bethann Age" ,
                        FederatedStateBirth="RR",
                        Salary=  2098.50M,
                        State= "ATIVO"
                    }
                },
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "17159666666",
                        Office= "Dev Pl",
                        Name= "Bernetta Aflalo" ,
                        FederatedStateBirth="PI",
                        Salary=  6686.30M,
                        State= "INATIVO"
                    }
                },
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "85555555709",
                        Office= "Dev Jr",
                        Name= "Aaron Aaberg" ,
                        FederatedStateBirth="AP",
                        Salary=  8965.30M,
                        State= "ATIVO"
                    }
                },

            };

            public static IEnumerable<object[]> EmployeesToUpdate() => 
            new List<object[]>
            {
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "95267437336",
                        Office= "AC Sr",
                        Name= "Bethann Age" ,
                        FederatedStateBirth="RR",
                        Salary=  2098.50M,
                        State= "BLOQUEADO"
                    }
                },
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "17159509684",
                        Office= "Dev Pl",
                        Name= "Bernetta Aflalo" ,
                        FederatedStateBirth="PI",
                        Salary=  6686.30M,
                        State= "BLOQUEADO"
                    }
                },
                new object[]{
                    new SaveEmployeeDTO{
                        Cpf= "85235708709",
                        Office= "Dev Jr",
                        Name= "Aaron Aaberg" ,
                        FederatedStateBirth="AP",
                        Salary=  8965.30M,
                        State= "BLOQUEADO"
                    }
                },

            };


        #endregion
    }
}