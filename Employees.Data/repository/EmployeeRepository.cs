using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Employees.Data.Entity;
using Employees.Data.Repository;

namespace Employees.Data.Repository {
    public class EmployeeRepository : IEmployeeRepository {

        #region props

        private IDictionary<string, Employee> _dictionaryOfEmployees = null;
        public IDictionary<string, Employee> Employees {
            get {
                return _dictionaryOfEmployees;
            }
        }

        #endregion

        #region Ctor        
        public EmployeeRepository () {
            CreateDictionary ();
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Read file "funcionarios.txt" and 
        /// converts the data into a dictionary to 
        /// facilitate reading of the data.
        /// The dictionary key is the cpf of the employee 
        /// and the value is the employee himself.
        /// </summary>
        /// <returns></returns>
        private void CreateDictionary () {

            try {
                _dictionaryOfEmployees = new ConcurrentDictionary<string, Employee> ();

                //get path of file 
                string dir = Path.Combine (Directory.GetCurrentDirectory (), "funcionarios.txt");

                //reads the data from the file
                using (TextReader reader = File.OpenText ("funcionarios.txt")) {

                    //read first line of file to remove descriptions
                    string line = reader.ReadLine ();

                    while ((line = reader.ReadLine ()) != null) {
                        string[] array = line.Split (';');

                        //convert array in Entity Employee
                        Employee employee = new Employee {
                            RegisterDate = DateTime.Parse (array[0], System.Globalization.CultureInfo.GetCultureInfo ("pt-BR")),
                            Office = array[1]?.Trim(),
                            Cpf = array[2]?.Trim(),
                            Name = array[3]?.Trim(),
                            FederatedStateBirth = array[4]?.Trim(),
                            Salary = decimal.Parse (array[5], System.Globalization.CultureInfo.GetCultureInfo ("pt-BR")),
                            State = array[6]?.Trim()
                        };                        

                        //fills the dictionary with employees, the key of each value is the employee's cpf
                        _dictionaryOfEmployees.Add (employee.Cpf, employee);
                    }
                    reader.Close();
                }
            } catch (Exception ex) {
                throw ex;
            }

        }

        #region Methods for CRUD

        /// <summary>
        /// Get employees that meets the filter.
        /// If no filter returns all employees.
        /// </summary>
        /// <param name="filterConditions">Condition for clause "Where" to filter employees</param>       
        /// <returns>Enumerable of employees</returns>
        public IEnumerable<Employee> GetEmployees (Func<Employee, bool> filterConditions = null) {

            try {
                IEnumerable<Employee> response = null;

                if (filterConditions != null)
                    response = this._dictionaryOfEmployees.Values.Where (filterConditions);
                else
                    response = this._dictionaryOfEmployees.Values;

                return response.OrderBy (x => x.Name);
            } catch (Exception ex) {
                throw ex;
            }

        }

        /// <summary>
        /// Get first employee that meets the filter.
        /// If no filter returns first employee.
        /// </summary>
        /// <param name="filterConditions">Condition for clause "Where" to filter employee</param>
        /// <returns>First employee</returns>        
        public Employee GetEmployee (Func<Employee, bool> filterConditions = null) {

            Employee response = null;

            if (filterConditions != null)
                response = this._dictionaryOfEmployees.Values.FirstOrDefault (filterConditions);
            else
                response = this._dictionaryOfEmployees.Values.FirstOrDefault ();

            return response;

        }

        /// <summary>
        /// Insert new employee
        /// </summary>
        /// <param name="model">Employee to insert</param>
        /// <returns>employee</returns>
        public Employee AddEmployee (Employee model) {

            try {                

                //insert data on the dictionary
                this._dictionaryOfEmployees.Add (model.Cpf, model);

                return model;
            } catch (System.Exception ex) {
                throw ex;
            }

        }

        /// <summary>
        /// Edit register of employee 
        /// </summary>
        /// <param name="model">employee</param>
        /// <returns>employee</returns>
        public Employee EditEmployee (Employee model) {

            try {

                //update ditionary with new value
                this._dictionaryOfEmployees[model.Cpf] = model;

                return model;
            } catch (System.Exception ex) {
                throw ex;
            }

        }

        /// <summary>
        /// Employee to remove
        /// </summary>
        /// <param name="cpf">cpf of employee to remove</param>
        /// <returns></returns>
        public void DeleteEmployee (string cpf) {         
            try {

                //remove employee of dictionary                                    
                this._dictionaryOfEmployees.Remove (cpf);

            } catch (Exception ex) {
                throw ex;
            }
          
        }

        #endregion

        public void Dispose () {
            GC.SuppressFinalize (this);
        }
        #endregion
    }
}