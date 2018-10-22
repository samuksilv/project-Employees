using System;
using System.ComponentModel.DataAnnotations;

namespace Employees.Data.Entity
{
    /// <summary>
    /// Class to represent employees
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Cpf of employee
        /// </summary>
        /// <value></value>
        [Key()]
        [StringLength(11)]
        [Required]        
        public string Cpf { get; set; }

        /// <summary>
        /// Employee registration date
        /// </summary>
        /// <value></value>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// Office of employee
        /// </summary>
        /// <value></value>
        [Required]
        public string Office { get; set; }

        /// <summary>
        /// Name of employee
        /// </summary>
        /// <value></value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Federative state of employee's birth
        /// </summary>
        /// <value></value>
        [Required]
        public string FederatedStateBirth { get; set; }

        /// <summary>
        /// Salary of employee
        /// </summary>
        /// <value></value>
        [Required]        
        public decimal Salary { get; set; }

        /// <summary>
        /// State of employee
        /// </summary>
        /// <value></value>
        [Required]                
        public string State { get; set; }
       
    }
}