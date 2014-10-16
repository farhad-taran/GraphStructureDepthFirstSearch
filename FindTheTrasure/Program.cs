using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace FindTheTrasure
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var message = Console.ReadLine();

                    if (!string.IsNullOrEmpty(message))
                    {
                        MessageParser.ProcessMessage(message);
                    }
                    else if (MessageParser.MessageCount > 0)
                    {
                        MessageParser.SearchGraph();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format(ex.Message,Environment.NewLine));
                }

            }      
        }
    }
}
