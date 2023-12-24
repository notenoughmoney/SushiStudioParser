using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SushiStudioParser
{
    public class ExcelFormatter
    {
        public void SaveAs(List<Product> data, string fileName)
        {
            // Создание нового файла Excel
            FileInfo newFile = new FileInfo(fileName);
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // Проверка наличия листа с таким именем и удаление, если он уже существует
                ExcelWorksheet existingWorksheet = package.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == "Products");
                if (existingWorksheet != null)
                {
                    package.Workbook.Worksheets.Delete(existingWorksheet);
                }

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");

                // Заголовки
                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Description";
                worksheet.Cells[1, 3].Value = "Type";
                worksheet.Cells[1, 4].Value = "Cost";
                worksheet.Cells[1, 5].Value = "Weight";
                worksheet.Cells[1, 6].Value = "Ratio";

                // Запись данных
                int row = 2;
                foreach (Product product in data)
                {
                    worksheet.Cells[row, 1].Value = product.Name;
                    worksheet.Cells[row, 2].Value = product.Description;
                    worksheet.Cells[row, 3].Value = product.Type;
                    worksheet.Cells[row, 4].Value = product.Cost;
                    worksheet.Cells[row, 5].Value = product.Weight;
                    worksheet.Cells[row, 6].Value = product.Ratio;
                    row++;
                }

                // Сохранение файла
                package.Save();
            }
        }
    }
}
