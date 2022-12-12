using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static OracleEFCore6.Program;

namespace OracleEFCore6
{
    class Program
    {
        public static string user = "system";
        public static string pwd = "oracle123";

        //Set the net service name, Easy Connect, or connect descriptor of the pluggable DB, 
        // such as "localhost/XEPDB1" for 18c XE or higher
        public static string db = "localhost/orcl";
        //Demonstrates how to get started using Oracle Entity Framework Core 6 
        //Code connects to on-premises Oracle DB or walletless Oracle Autonomous DB

        public class EmployeeContext : DbContext
        {
            public DbSet<Employee>? EMP { get; set; }
            //public DbSet<DEPT>? DEPT { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";
                optionsBuilder.UseOracle(conStringUser);
            }
        }

        public class Employee
        {
            public int EMPNO { get; set; }
            public string ENAME { get; set; }            
        }

        

        public class DEPT
        {
            [Key]
            public int DEPTNO { get; set; }
            public string? DNAME { get; set; }
            public string? LOC { get; set; }           
        }

        static void Main(string[] args)
        {

            using (var db = new EmployeeContext())
            {
                foreach (var item in db.EMP)
                {
                    Console.WriteLine(item);
                }
            }
            using (var db = new EmployeeContext())
            {
                var emp = new Employee { EMPNO = 5, ENAME ="rahul", };
                db.EMP!.Add(emp);
                db.SaveChanges();
            }


            Console.ReadLine();
        }
    }
}