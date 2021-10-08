using Discord.Commands;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace YAMB
{
    class Program
    {
        //static void Main(string[] args) =>
        //    MainAsync().GetAwaiter().GetResult();
        //public static async Task MainAsync()
        //{
        //    await new Client().Login();
        //    await new CommandService().AddModulesAsync(Assembly.GetExecutingAssembly(), null);
        //}

        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}
