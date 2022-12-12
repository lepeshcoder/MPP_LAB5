using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }

            public User(int id,int age, string name,string surname)
            {
                Id = id;
                Age = age;
                Name = name;
                Surname = surname;    
            }
        }

        


    
}
