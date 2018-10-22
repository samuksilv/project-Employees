using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Employees.Data.Dto
{
    /// <summary>
    /// Class to save employee 
    /// </summary>
    public class SaveEmployeeDTO
    {
        /// <summary>
        /// Cpf of employee
        /// </summary>
        /// <value></value>
        [ StringLength(11), JsonProperty("cpf"), Required, JsonRequired] 
        public virtual string Cpf{get; set;}

        /// <summary>
        /// Office of employee
        /// </summary>
        /// <value></value>
        [Required, JsonRequired, JsonProperty("office"), StringLength(100)]
        public virtual string Office { get; set; }

        /// <summary>
        /// Name of employee
        /// </summary>
        /// <value></value>
        [Required, JsonRequired, JsonProperty("name"), StringLength(120)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Federative state of employee's birth
        /// </summary>
        /// <value></value>
        [Required, JsonRequired, JsonProperty("federatedStateBirth"), StringLength(5)]
        public virtual string FederatedStateBirth { get; set; }

        /// <summary>
        /// Salary of employee
        /// </summary>
        /// <value></value>
        [Required, JsonRequired, JsonProperty("salary")]    
        public virtual decimal Salary { get; set; }

        /// <summary>
        /// State of employee
        /// </summary>
        /// <value></value>
        [Required, JsonRequired, JsonProperty("state"), StringLength(20)]         
        public virtual string State { get; set; }
    }
}