using System.Collections.Generic;
using System.IO;
using System.Linq;
using Employees.Data.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace Employees.UnitTests.DataTests
{    
    [TestClass]
    public class EmployeeRepositoryTests
    {       
        [Fact] 
        [TestMethod]
        public void GenerateNewRepository(){ 

            #region Arrange
            string dir = Path.Combine(Directory.GetCurrentDirectory (), "funcionarios.txt");
            //count lines with register(remove first line with descriptions)
            IEnumerable<string> lines= File.ReadLines(dir);
            var countLines= lines.Skip(1).Count();
            #endregion

            #region Act
            var repository = new EmployeeRepository();
            #endregion

            #region Assert
            Xunit.Assert.NotNull(repository);
            Xunit.Assert.Equal(repository.Employees.Count, countLines  );
            #endregion
        }
    }
}