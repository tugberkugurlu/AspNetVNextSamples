using System;
using Microsoft.Dnx.Runtime;

namespace Beta7Sample
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
            Console.WriteLine(_appEnv.ApplicationName);
            Console.WriteLine(_appEnv.ApplicationVersion);
            Console.WriteLine(_appEnv.ApplicationBasePath);
            Console.WriteLine(_appEnv.Configuration);
            Console.WriteLine(_appEnv.RuntimeFramework);
        }    
    }
}