using BasicModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnit_BasicModules_Tests
{
    //public class DbContextFixture : IDisposable
    //{
    //    public DbContext Conext;

    //    public DbContextFixture()
    //    {
    //        this.Conext = DbOptionBuilder.CreateDbConext().InMemory();
    //    }
    //    public void Dispose()
    //    {
    //        Conext.Dispose();
    //    }
    //}

    //public class EmpployeeTests : IClassFixture<DbContextFixture>
    //{
    //    private readonly DbContextFixture dbContextFixture;

    //    public EmpployeeTests(DbContextFixture dbContextFixture)
    //    {
    //        this.dbContextFixture = dbContextFixture;
    //    }
    //}

    public class LoggerFixture : IDisposable
    {
        public LoggerFixture()
        {
            // create text file.
        }
        public void Log(string msg)
        {
            // use the text to log
        }
        public void Dispose()
        {
           // Create report
           // send mail of report
           // delete the file
        }
    }

    public class FileCleanUpFixture : IDisposable
    {
        public FileManagement FileObj;

        public FileCleanUpFixture()
        {
            FileObj = new FileManagement();
        }

        public void Dispose()
        {
            File.Delete(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt");
        }
    }

    [CollectionDefinition("File CleanUp Collection")]
    public class FileCleanUpCollection : ICollectionFixture<FileCleanUpFixture>
    {

    }

    //[Collection("File CleanUp Collection")]
    //public class FileTests
    //{
    //    private readonly FileCleanUpFixture fileCleanUp;

    //    public FileTests(FileCleanUpFixture fileCleanUp)
    //    {
    //        this.fileCleanUp = fileCleanUp;
    //    }

    //    [Fact]
    //    public void CreateFile()
    //    {
    //        // Arrange


    //        // Act
    //        fileCleanUp.FileObj.CreateFile(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt");

    //        // Assert
    //        Assert.True(File.Exists(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt"));
    //    }

    //    [Fact]
    //    public void WriteFile()
    //    {
    //        // Arrange


    //        // Act
    //        fileCleanUp.FileObj.WriteFile(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt");

    //        // Assert
    //        Assert.True(File.Exists(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt"));
    //    }
    //}

    public class FileTests : IClassFixture<FileCleanUpFixture>
    {
        private readonly FileCleanUpFixture fileCleanUp;

        public FileTests(FileCleanUpFixture fileCleanUp)
        {
            this.fileCleanUp = fileCleanUp;
        }

        [Fact]
        public void CreateFile()
        {
            // Arrange


            // Act
            fileCleanUp.FileObj.CreateFile(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt");

            // Assert
            Assert.True(File.Exists(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt"));
        }

        [Fact]
        public void WriteFile()
        {
            // Arrange


            // Act
            fileCleanUp.FileObj.WriteFile(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt");

            // Assert
            Assert.True(File.Exists(@"C:\_Ashutosh\Trainings\AdvanceNetConcepts\_Ashutosh\TDD\XUnit_BasicModules_Tests\bin\Debug\net5.0\text1.txt"));
        }
    }
}
