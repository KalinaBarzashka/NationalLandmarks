namespace NationalLandmarks.Server.Data.Migrations
{
    //using ClosedXML.Excel;
    using Microsoft.EntityFrameworkCore;
    //using NationalLandmarks.Server.Data.Models;

    public static class Seeder 
    {
        public static void Seed(ModelBuilder builder)
        {
            //using FileStream fsSource = new FileStream("data.xlsx", FileMode.Open, FileAccess.Read);
            //Console.WriteLine(fsSource);
            //using (var excelWorkbook = new XLWorkbook("data.xlsx"))
            //{
            //    var rowCount = excelWorkbook.Worksheet("Area").LastRowUsed().RowNumber();
            //    var columnCount = excelWorkbook.Worksheet("Area").LastColumnUsed().ColumnNumber();
            //    int column = 1;
            //    int row = 1;
            //    int id = 1;
            //    List<Area> areas = new List<Area>();
            //    while (row <= rowCount)
            //    {
            //        while (column <= columnCount)
            //        {
            //            string name = excelWorkbook.Worksheets.Worksheet(1).Cell(row, column).GetString();
            //            areas.Add(new Area { Name = name, Id = id });
            //            id++;
            //            //Console.WriteLine(title);
            //            column++;
            //        }

            //        row++;
            //        column = 1;
            //    }

            //    builder.Entity<Area>().HasData(
            //        areas
            //    );
            //}
        }
    }
}
