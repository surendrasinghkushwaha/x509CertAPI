using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace x509CertAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }

        public List<Student> GetAll()
        {

            return new List<Student> { new Student {Id=1,Age=40, Name="Surendra Singh kushwaha" },
            new Student {Id=2,Age=40, Name="Haervinder singh sinddu" },
            new Student {Id=3,Age=42, Name="mahesh vinchu" },
            new Student {Id=4,Age=44, Name="Vijay thakre" },
            new Student {Id=5,Age=32, Name="Chand babu singh" }
        };
        }
    }
}