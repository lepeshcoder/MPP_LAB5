using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class InvalidPropertyNameException : Exception
    {
        public InvalidPropertyNameException(string fieldName) : base("Invalid field name:" + fieldName)
        {

        }
    }
}
