using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
namespace CinemaScheduler.BusinessLogic.ExcelHelper
{
    public static class Importer
    {
        public static List<Vector> Import(string file)
        {
            string Episode = file;
            List<Vector> vectors = new List<Vector>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(file);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            for (int row = 2; row <= xlRange.Rows.Count; row++)
            {
                dynamic currentActor = xlWorksheet.Cells[row, 2].Value2;
                if (currentActor != null)
                {
                    string actorName = currentActor.ToString();
                    for (int col = 4; col <= xlRange.Columns.Count; col++)
                    {
                        dynamic currentRing = xlWorksheet.Cells[1, col].Value2;
                        dynamic currentElement = xlWorksheet.Cells[row, col].Value2;
                        if (currentRing != null && currentElement != null)
                        {
                            bool ringFound = false, linesFound = false;
                            float ringNumber = 0, actorRingLines = 0;
                            ringFound = float.TryParse(currentRing.ToString(), out ringNumber);
                            linesFound = float.TryParse(currentElement.ToString(), out actorRingLines);
                            if (ringFound && linesFound)
                            {
                                vectors.Add(new Vector(actorName, ringNumber, actorRingLines, Episode));
                            }
                        }
                    }
                }
            }
            xlWorkbook.Close(SaveChanges: false);
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            return vectors;
        }
    }
}
