using System;
using System.Threading.Tasks;

namespace ConsoleTaskSample 
{
    public class Program 
    {
        public async Task Main(string[] args)
        {
            await Task.Yield();
            Console.WriteLine("Hello world from ConsoleTaskSample...");
        }
    }
}