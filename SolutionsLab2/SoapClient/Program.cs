using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Once added as a service reference, the namespace in which you will find the SOAP Client constructor is the name of your service
// (if you don't remember, you can find it in Connected Services). I used Calculator but I think the default is ServiceReference1.
using SoapClient.Calculator;

namespace SoapClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // The name of the client class can be found by double clicking the connected service.
            CalculatorSoapClient client = new CalculatorSoapClient();
            while (true)
            {
                // Thanks to @PaulineDevictor for a smart implementation of this client.
                Console.WriteLine("Enter an operation, or exit to leave.");
                string operation = Console.ReadLine();

                if (operation == "exit") break;

                string[] subs = operation.Split(' ');
                double result = 0;

                if (subs.Length != 3) {
                    Console.WriteLine("Incorrect operation. Example: a + b");
                    continue;
                }

                switch (subs[1])
                {
                    case "+":
                        result = client.Add(Int32.Parse(subs[0]), Int32.Parse(subs[2]));
                        break;
                    case "-":
                        result = client.Subtract(Int32.Parse(subs[0]), Int32.Parse(subs[2]));
                        break;
                    case "*":
                        result = client.Multiply(Int32.Parse(subs[0]), Int32.Parse(subs[2]));
                        break;
                    case "/":
                        result = client.Divide(Int32.Parse(subs[0]), Int32.Parse(subs[2]));
                        break;
                }

                Console.WriteLine(operation + " = " + result);
            }
        }
    }
}
