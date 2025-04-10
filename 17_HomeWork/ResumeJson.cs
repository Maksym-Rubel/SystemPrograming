using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17_HomeWork
{
    internal class ResumeJson
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public int ExpriencYear { get; set; }
        public double Salary { get; set; }
        public override string ToString()
        {
            return $"{Name,-25}{Surname,-25}{Age,-15}{Country,-20}{ExpriencYear,-10}{Salary,-15}";
        }
    }
}
