using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace QnA
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                .RunConsoleAsync();
    }
}
