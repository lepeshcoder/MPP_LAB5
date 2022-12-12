using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class InvalidTemplateException : Exception
    {
        public InvalidTemplateException(string template) : base("Invalid template: " + template)
        {

        }
    }
}
