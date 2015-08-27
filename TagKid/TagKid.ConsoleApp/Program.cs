using System;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //args = new[] {@"E:\github\mehmetatas\tagkid\TagKid"};
                if (args.Length == 1)
                {
                    PostBuild.Run(args[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}