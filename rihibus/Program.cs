using System;
using System.Data;
using System.Reflection;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;

namespace ODP.NET_Core_Get_Started
{
    class GettingStarted
    {
        //Prerequisite: This app assumes the user has already been created with the
        // necessary privileges
        //Set the demo user id, such as DEMODOTNET and password
        public static List<string> CollumnNames { get; set; }

        public static string user = "system";
        public static string pwd = "oracle123";

        //Set the net service name, Easy Connect, or connect descriptor of the pluggable DB, 
        // such as "localhost/XEPDB1" for 18c XE or higher
        public static string db = "localhost/orcl";

        static void Main()
        {
            CollumnNames = new List<string>() { "ENAME", "JOB", "MGR", "HIREDATE", "SAL", "COMM", "DEPTNO" };
            string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";

            using (OracleConnection con = new OracleConnection(conStringUser))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        Console.WriteLine("Successfully connected to Oracle Database as " + user);
                        Console.WriteLine();

                        //Retrieve sample data
                        cmd.CommandText = "SELECT * FROM EMP";
                        OracleDataReader reader = cmd.ExecuteReader();
                        foreach (var item in CollumnNames)
                        {
                            Console.Write("{0} ", item);
                        }
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            foreach (string item in CollumnNames)
                            {
                                print(reader, item);
                            }
                            Console.WriteLine();
                        }


                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }
            }
        }

        private static void print(OracleDataReader reader, string item)
        {
            Console.Write("{0} ", reader[item]);
        }
    }
}