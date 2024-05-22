using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.EmployeeAttendences.Commands.Create;
using HumanResourceManagement.Application.EmployeeProfiles.Commands.Create;
using HumanResourceManagement.Domain.Enums;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;
using static ICSharpCode.SharpZipLib.Zip.ZipEntryFactory;

namespace HumanResourceManagement.Application.Common.Helpers;

public static class ExcelHelper
{
    public static List<T> ReadExcelToList<T>(Stream excelStream, Func<ISheet, int, T> mapFunction, int startRow = 1)
    {
        List<T> dataList = new List<T>();

        IWorkbook workbook = null;
        try
        {
            workbook = new XSSFWorkbook(excelStream); // For .xlsx files
        }
        catch
        {
            // If it's not an .xlsx file, try loading as .xls file
            try
            {
                excelStream.Seek(0, SeekOrigin.Begin);
                workbook = new HSSFWorkbook(excelStream); // For .xls files
            }
            catch
            {
                throw new Exception("Invalid Excel file format.");
            }
        }

        if (workbook == null)
        {
            throw new Exception("Failed to load Excel workbook.");
        }

        ISheet sheet = workbook.GetSheetAt(0); // Assuming we are reading the first sheet
        if (sheet == null)
        {
            throw new Exception("No sheet found in the Excel file.");
        }

        int rowCount = sheet.LastRowNum + 1;

        for (int row = startRow; row < rowCount; row++)
        {
            T obj = mapFunction(sheet, row);
            dataList.Add(obj);
        }

        return dataList;
    }

    public static CreateEmployeeProfileCommand MapRowToEmployeeProfile(ISheet worksheet, int row)
    {
        IRow excelRow = worksheet.GetRow(row);

        return excelRow == null ? null :
            new CreateEmployeeProfileCommand
            {
                Designation = new Models.EntityExternalIdentifier
                {
                    ExternalIdentifier = excelRow.GetCellToString(0),
                },
                EmployeeName = excelRow.GetCellToString(1),
                EmployeeCode = excelRow.GetCellToString(2),
                EmployeeType = EnumExtensions.ToEnum<EmployeeType>(excelRow.GetCellToString(3)),
                LineManager = excelRow.GetCellToString(4),
                Segment = excelRow.GetCellToString(5),
                Gender = EnumExtensions.ToEnum<Gender>(excelRow.GetCellToString(6)),
                MaritalStatus = EnumExtensions.ToEnum<MaritalStatus>(excelRow.GetCellToString(7)),
                Contact = excelRow.GetCellToString(8),
                EmailAddress = excelRow.GetCellToString(9),
                ActiveStatus = EmployeeStatus.Active,
            };
    }

    public static CreateEmployeeAttendenceCommand MapRowToEmployeeAttendence(ISheet worksheet, int row)
    {
        IRow excelRow = worksheet.GetRow(row);

        return excelRow == null ? null :
            new CreateEmployeeAttendenceCommand
            {
                EmployeeProfile = new Models.EntityExternalIdentifier
                {
                    ExternalIdentifier = excelRow.GetCellToString(0),
                },

                AttendanceDate = excelRow.GetCellToString(1),
                TimeIn = excelRow.GetCellTimeOnlyValue(2),
                TimeOut = excelRow.GetCellTimeOnlyValue(3),
            };
    }

    private static string GetCellToString(this IRow cells, int index) 
    {
        return cells?.GetCell(index)?.ToString();
    }

    private static double? GetCellToInt(this IRow cells, int index)
    {
        return cells?.GetCell(index)?.NumericCellValue;
    }

    private static string GetCellTimeOnlyValue(this IRow cells, int index)
    {
        return cells?.GetCell(index)?.TimeOnlyCellValue.ToString();
    }
}
