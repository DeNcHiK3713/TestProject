using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;

namespace RtfParser
{
    class Program
    {
        static OperationInfo ReadDoc(string path)
        {
            var word = new Microsoft.Office.Interop.Word.Application();
            var currentDoc = word.Documents.Open(path);
            var table = currentDoc.Tables[1];
            var info = new OperationInfo()
            {
                TradeRegistrationNumber = table.Cell(6, 2).Range.Text.Replace("\r\a", ""),
                ContractNumber = table.Cell(1, 2).Range.Text.Replace("\r\a", ""),
                CounterpartyAccounts = table.Cell(13, 2).Range.Text.Replace("\r\a", ""),
                CounterpartyAddress = table.Cell(11, 2).Range.Text.Replace("\r\a", ""),
                ContractName = table.Cell(5, 2).Range.Text.Replace("\r\a", ""),
            };
            currentDoc.Close();
            word.Quit();
            return info;
        }
        static void WriteExcel(OperationInfo info)
        {
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workBook = excel.Workbooks.Add();
            Microsoft.Office.Interop.Excel.Worksheet sheet = workBook.Worksheets[1];
            sheet.Cells[1, 1] = "Регистрационный номер сделки";
            sheet.Cells[1, 2] = "Номер договора";
            sheet.Cells[1, 3] = "Счет контрагента";
            sheet.Cells[1, 4] = "Адрес контрагента";
            sheet.Cells[1, 5] = "Наименование договора";
            sheet.Cells[2, 1] = info.TradeRegistrationNumber;
            sheet.Cells[2, 2] = info.ContractNumber;
            sheet.Cells[2, 3] = info.CounterpartyAccounts;
            sheet.Cells[2, 4] = info.CounterpartyAddress;
            sheet.Cells[2, 5] = info.ContractName;
            excel.Visible = true;
            excel.UserControl = true;
        }
        static void Main(string[] args)
        {
            string path = null;
            if (args.Length > 0)
            {
                path = args[0];
            }
            else
            {
                Console.Write("Enter path: ");
                path = Console.ReadLine();
            }

            var info = ReadDoc(path);
            WriteExcel(info);

            Console.WriteLine("Done!");
        }
    }
}