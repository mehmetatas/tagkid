using System;
using System.Linq;
using Taga.Core.Repository;
using TagKid.Lib.Entities;
using TagKid.Lib.Repository;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var db = new Db(true))
                {
                    //var user = new User
                    //{
                    //    Id = 1,
                    //    Username = "taga",
                    //    FullName = "Mehmet Ataş",
                    //    Email = "mehmet@mehmetatas.net",
                    //    FacebookId = "12345678",
                    //    Status = UserStatus.Admin
                    //};

                    //db.Save(user);


                }

                Console.WriteLine("OK!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }
    }
}
