using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GenericBackend.DataModels.Plan;

namespace GenericBackend.Excel
{
    public class ParsePlanActual
    {
        public const string ActualSheetName = "plan";
        private const string TotalCellText = "TOTAL By Months";
        private string _docPath;

        public ParsePlanActual(string docPath)
        {
            _docPath = docPath;
        }

        private static Tuple<int, string> GetCellRowColunmReferences(string cellReference)
        {
            var match = Regex.Match(cellReference, @"([A-Z]+)(\d+)");

            return new Tuple<int, string>(int.Parse(match.Groups[2].Value), match.Groups[1].Value);
        }

        public PlanSheet ParsePlanSheet()
        {
            var planSheet = new PlanSheet();
            using (var document = SpreadsheetDocument.Open(_docPath, true))
            {
                var sheet =
                    (Sheet)document.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                        .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals(ActualSheetName, StringComparison.CurrentCultureIgnoreCase));
                planSheet.Name = sheet.Name.Value.ToLower();
                var planItems = new List<PlanSheetItem>();
                
                var workSheetPart =
                    (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);

                var sheetData = workSheetPart.Worksheet.ChildElements.First<SheetData>();
                
                var rows = sheetData.Elements<Row>().ToArray();
                int rowIndex = 0;
                foreach (var row in rows)
                {
                    int rowNumber = 0;
                    var planItem = new PlanSheetItem();
                    var cells = row.Descendants<Cell>().ToArray();
                    var cellIndex = 0;
                    
                    for (int i = 0; i < cells.Count(); i++)
                    {
                        
                        var cellReference = GetCellRowColunmReferences(cells[i].CellReference.Value);
                        var cellName = cellReference.Item2;
                        var cellNumber = cellReference.Item1;
                        var cellValue = GetCellValue(document.WorkbookPart, cells[i]);
                        var isEnd = string.Compare(cellValue, TotalCellText, StringComparison.OrdinalIgnoreCase) == 0;
                        switch (cellName)
                        {
                            case "A":
                                int.TryParse(cellValue, out rowNumber);
                                break;
                            case "E":
                                if (rowNumber > 0 && !isEnd)
                                {
                                    planItem.Subject = cellValue;
                                }
                                break;
                            case "J":
                                if (rowNumber > 0)
                                {
                                    planItem.FirstUknknown = cellValue;
                                }
                                break;
                            case "K":
                                if (rowNumber > 0)
                                {
                                    planItem.SecondUknknown = cellValue;
                                }
                                break;
                            case "M":
                                if (rowNumber > 0)
                                {
                                    planItem.ThirdUknknown = cellValue;
                                }
                                break;
                            case "N":
                                if (rowNumber > 0)
                                {
                                    planItem.Nis = cellValue;
                                }
                                break;
                            case "O":
                                if (rowNumber > 0)
                                {
                                    planItem.CummulativePActualEachMonth = cellValue;
                                }
                                break;
                            case "P":
                                if (rowNumber > 0)
                                {
                                    planItem.CummulativePlan = cellValue;
                                }
                                break;
                            case "Q":
                                if (rowNumber > 0)
                                {
                                    planItem.Diff = cellValue;
                                }
                                break;
                            case "R":
                                if (cellNumber == 2)
                                {
                                    var s = GetCellValue(document.WorkbookPart, cells[i]);
                                }
                                if (cellNumber >= 4)
                                {
                                    var list = new List<PlanTimelineData>();
                                    for (int j = cellIndex; j <= cells.Count() - 3; j= j+3)
                                    {
                                        var timeLine = new PlanTimelineData();
                                        if (rowNumber > 0 && !isEnd)
                                        {
                                            timeLine.Plan = GetCellValue(document.WorkbookPart, cells[j]);
                                            timeLine.AccumulatedPlan = GetCellValue(document.WorkbookPart, cells[j + 1]);
                                            timeLine.SupervisorComments = GetCellValue(document.WorkbookPart, cells[j + 2]);
                                        }

                                        list.Add(timeLine);
                                    }
                                    planItem.TimelineData = list;
                                }
                                break;
                        }
                        cellIndex++;
                    }
                    planItems.Add(planItem);
                    rowIndex++;
                }
                planSheet.PlanItems = planItems;

            }
            
            return planSheet;
        }

       
        private static void ParsePlanItems(List<PlanSheetItem> planItems, SpreadsheetDocument document, Cell cell, string columnName)
        {
           
            
            
        }

        // Retrieve the value of a cell, given a file name, sheet name,  
        // and address name. 
        public static string GetCellValue(
            string addressName, WorkbookPart wbPart, Sheet theSheet)
        {
            string value = null;
            
            // Throw an exception if there is no sheet. 
            if (theSheet == null)
            {
                throw new ArgumentException("sheetName");
            }

            // Retrieve a reference to the worksheet part. 
            WorksheetPart wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            // Use its Worksheet property to get a reference to the cell  
            // whose address matches the address you supplied. 
            Cell theCell = wsPart.Worksheet.
              Descendants<Cell>().FirstOrDefault(c => c.CellReference == addressName);

            // If the cell does not exist, return an empty string. 
            //value = GetCellValue(wbPart, theCell, value);
            return value;
        }

        private static string GetCellValue(WorkbookPart wbPart, Cell theCell)
        {
            string value = string.Empty;
            if (theCell == null)
                return value;

            value = theCell.InnerText;

            // If the cell represents an integer number, you are done.  
            // For dates, this code returns the serialized value that  
            // represents the date. The code handles strings and  
            // Booleans individually. For shared strings, the code  
            // looks up the corresponding value in the shared string  
            // table. For Booleans, the code converts the value into  
            // the words TRUE or FALSE. 
            if (theCell.DataType == null)
                return value;

            switch (theCell.DataType.Value)
            {
                case CellValues.SharedString:

                    // For shared strings, look up the value in the 
                    // shared strings table. 
                    var stringTable =
                        wbPart.GetPartsOfType<SharedStringTablePart>()
                            .FirstOrDefault();

                    // If the shared string table is missing, something  
                    // is wrong. Return the index that is in 
                    // the cell. Otherwise, look up the correct text in  
                    // the table. 
                    if (stringTable != null)
                    {
                        value =
                            stringTable.SharedStringTable
                                .ElementAt(int.Parse(value)).InnerText;
                    }
                    break;

                case CellValues.Boolean:
                    switch (value)
                    {
                        case "0":
                            value = "FALSE";
                            break;
                        default:
                            value = "TRUE";
                            break;
                    }
                    break;
            }
            return value;
        }

        public static void Parse(string docName)
        {
            using (var document = SpreadsheetDocument.Open(docName, true))
            {
                var sheet =
                    (Sheet)document.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                        .ChildElements.First(x => x is Sheet && ((Sheet)x).Name.Value.Equals(ActualSheetName, StringComparison.CurrentCultureIgnoreCase));
                
                var workSheetPart =
                    (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);
                
                var stringParts = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>();

                var sheetData = workSheetPart.Worksheet.ChildElements.First<SheetData>();
                
                var rows = sheetData.Elements<Row>();
            }
        }
    }
}
