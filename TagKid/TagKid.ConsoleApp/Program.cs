using System;
using TagKid.Lib.PetaPoco;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CreateTableUtil.GenerateCreateTableSql("TagKid.Lib.dll", "TagKid.Lib.Entities");

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
