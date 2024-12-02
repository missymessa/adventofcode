using System;
using System.Reflection;

namespace adventofcode2024
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: be able to look up the day from user input

            // Get day in UTC-5. Run that day's code. 
            int day = DateTime.UtcNow.AddHours(-5).Day;

            Assembly asm = Assembly.GetExecutingAssembly();

            if (day > 25) day = 1;

            Type type = asm.GetType($"adventofcode2024.day_{day}");

            if (type != null)
            {
                // Create an instance of the class
                object instance = Activator.CreateInstance(type);

                // Get the method info for the method you want to invoke
                MethodInfo methodInfo = type.GetMethod("Execute");

                if (methodInfo != null)
                {
                    // Invoke the method on the instance
                    methodInfo.Invoke(instance, null);
                }
                else
                {
                    Console.WriteLine("Method not found");
                }
            }
            else
            {
                Console.WriteLine("Type not found");
            }
        }
    }
}
