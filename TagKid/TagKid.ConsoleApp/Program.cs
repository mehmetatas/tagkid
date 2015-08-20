using System;

namespace TagKid.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args?.Length > 0 && args[0] == "-postbuild")
                {
                    PostBuild.Run();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}