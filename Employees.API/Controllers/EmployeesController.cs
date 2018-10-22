using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employees.Business.Service;
using Employees.Data.Dto;
using Employees.Data.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Employees.API.Controllers
{
    [Route("api/Employees")]
    public class EmployeesController: Controller    
    {        
        #region Properties
        private static IEmployeeService _service;
        #endregion

        #region Ctor
        public EmployeesController(IEmployeeService service)
        {   
            if(_service== null)
                _service= service;            
        }
        #endregion

        #region Get's Methods
         /// <summary>
        /// get all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployee()
        {        
            return Ok(_service.GetEmployees());
        }

        /// <summary>
        /// get employees by cpf
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/{cpf}")]
        [ProducesResponseType(200,Type= typeof(Employee))]
        public IActionResult GetEmployeeByCPF(string cpf)
        {        
            return Ok(_service.GetEmployeeByCPF(cpf));
        }

        /// <summary>
        /// get employees by name
        /// </summary>
        /// <param name="name">name of employee</param>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/Name")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployeeByName([FromQuery]string name)
        {        
            return Ok(_service.GetEmployeesByName(name));
        }
        
        /// <summary>
        /// get employees by state
        /// </summary>
        /// <param name="state">state [ATIVO- INATIVO- BLOQUEADO]</param>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/State")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployeeByState([FromQuery] string state)
        {        
            return Ok(_service.GetEmployeesByState(state));
        }

        /// <summary>
        /// get employee by the date of registration
        /// </summary>
        /// <param name="date">date of registrater - format: [dd/MM/yyyy]</param>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/RegisterDate")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployeeByRegisterDate([FromQuery]string date)
        {        
            return Ok(_service.GetEmployeesByRegisterDate(date));
        }

        /// <summary>
        /// get employee by office
        /// </summary>
        /// <param name="office">office of employee</param>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/Office")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployeeByOffice([FromQuery]string office)
        {        
            return Ok(_service.GetEmployeesByOffice(office));
        }

        /// <summary>
        ///  get employee by salary
        /// </summary>
        /// <param name="minSalary">min salary to filter </param>
        /// <param name="maxSalary"> max salary to filter </param>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/Salary")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployeeBySalary([FromQuery]decimal? minSalary,[FromQuery]decimal? maxSalary)
        {        
            return Ok(_service.GetEmployeesBySalary(minSalary, maxSalary));
        }

        /// <summary>
        /// get employee groups by Federated State of Birth and then order by amount 
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        [Route("Employee/FederatedState")]
        [ProducesResponseType(200,Type= typeof(IEnumerable<EmployeeGroupByFederatedStateDTO>))]
        public IActionResult GetEmployeesByFederatedStateBirth()
        {        
            return Ok(_service.GetEmployeesByFederatedStateBirth());
        }
        

        #endregion
       
        #region Add Method
            
        /// <summary>
        /// Insert new employee 
        /// </summary>
        /// <param name="model">entity to insert</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Employee")]
        [ProducesResponseType(201,Type= typeof(Employee))]
        public IActionResult AddEmployee([FromBody]SaveEmployeeDTO model)
        {        
            var response = _service.SaveEmployee(model);

            return Created($"/employee/{response.Cpf}", response);
        }

        #endregion

        #region Update method
            
        /// <summary>
        /// Update existing employee 
        /// </summary>
        /// <param name="model">entity to insert</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Employee")]
        [ProducesResponseType(201,Type= typeof(Employee))]
        public IActionResult EditEmployee([FromBody]SaveEmployeeDTO model)
        {        
            var response = _service.SaveEmployee(model);

            return Created($"/employee/{response.Cpf}", response);
        }

        #endregion

        #region Delete Method
            
        /// <summary>
        /// Delete existing employee 
        /// </summary>
        /// <param name="cpf">cpf</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Employee/{cpf}")]
        [ProducesResponseType(204)]
        public IActionResult DeleteEmployee(string cpf)
        {        
            _service.DeleteEmployee(cpf);

            return NoContent();
        }
        
        #endregion
        
        
    }
}