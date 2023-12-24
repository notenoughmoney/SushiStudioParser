using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStudioParser
{
    internal class Init
    {
        public static void Main()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Parser parser = new Parser();
            var products = parser.GetProducts();
            foreach ( var product in products )
            {
                product.print();
            }

            ExcelFormatter formatter = new ExcelFormatter();
            string fileName = "C:\\Users\\Dmitrii Kriazhev\\source\\repos\\SushiStudioParser\\book.xlsx";
            formatter.SaveAs(products, fileName);
        }
    }
}
