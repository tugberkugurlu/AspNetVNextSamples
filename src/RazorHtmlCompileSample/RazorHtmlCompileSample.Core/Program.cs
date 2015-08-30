using System;
using System.IO;
using Microsoft.Framework.Runtime;

namespace RazorHtmlCompileSample.Core
{
    public class Program
    {
        private readonly IApplicationEnvironment _appEnv;

        public Program(IApplicationEnvironment appEnv)
        {
            _appEnv = appEnv;
        }
        
        public void Main(string[] args)
        {
            var viewsFolderPath = Path.Combine(_appEnv.ApplicationBasePath, "Views");
            
            Console.WriteLine(_appEnv.ApplicationBasePath);
            foreach(var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}