using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStudioParser
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int? Cost { get; set; }
        public int? Weight { get; set; }
        public float? Ratio { get; set; }

        public void print() { 
            Console.WriteLine(Name + " " + Description + " " + Type + " " + Cost + " " + Weight + " " + Ratio);    
        } 
    }
}
