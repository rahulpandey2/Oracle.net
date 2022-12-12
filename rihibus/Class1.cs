using Microsoft.EntityFrameworkCore;

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

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> blogs { get; set; }
            public DbSet<Post>? Posts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

                string conStringUser = "User Id=" + user + ";Password=" + pwd + ";Data Source=" + db + ";";
                optionsBuilder.UseOracle(conStringUser);
            }
        }

        public class Blog
        {
            public int ID { get; set; }
            public string Url { get; set; }
            //public int? Rating { get; set; }

        }

        public class Post
        {
            public int PostId { get; set; }
            public string? Title { get; set; }
            public string? Content { get; set; }

            public int BlogId { get; set; }
            public Blog? Blog { get; set; }
        }

        static void Main(string[] args)
        {

            using (var db = new BloggingContext())
            {
                var blogs = db.blogs;
                foreach (var item in blogs!)
                {
                    Console.WriteLine(item.Url);
                    //Console.WriteLine(item.Url + " has rating " + item.Rating );
                }
            }
            using (var db = new BloggingContext())
            {
                var blog = new Blog { ID = 1, Url = "https://blogs.oracle.com" };
                //var blog = new Blog { Url = "https://blogs.oracle.com", Rating = 10 };
                db.blogs!.Add(blog);
                db.SaveChanges();
            }

          
            Console.ReadLine();
        }
    }
}

/* Copyright (c) 2018, 2022 Oracle and/or its affiliates. All rights reserved. */
/* Copyright (c) .NET Foundation and Contributors                              */

/******************************************************************************
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *   limitations under the License.
 * 
 *****************************************************************************/