using System;

namespace DefineSample
{
    public class Program
    {
        public void Main(params string[] args)
        {
            #if DEBUG
                Console.WriteLine("Debug...");
            #endif
                
            #if FOO
                Console.WriteLine("Foo...");
            #endif
                
            #if RELEASE
                Console.WriteLine("Release...");
            #endif
        }
    }
}