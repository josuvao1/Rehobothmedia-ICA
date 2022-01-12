using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;

namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
        private void BirthDay_Click(object sender, EventArgs e)
        {

            var birthdayNameList = BirthdayList.Text.Replace("\r\n", "$").ToString().Split('$');
            var birthdayDateList = BirthdayDate.Text.Replace("\r\n", "$").ToString().Split('$');
            var birhtdayString = String.Empty;
            for (int i = 0; i < birthdayNameList.Count(); i++)
            {
                if (!String.IsNullOrWhiteSpace(birthdayNameList[i]))
                {
                    birhtdayString = $"{birhtdayString} <tr><td data-th =\"Name\"><b>{birthdayNameList[i]}<b></td><td data-th = \"Date\">{birthdayDateList[i]}</td></tr>";
                }
            }
            var birhtdayTemplate = new HtmlModel() { Header = "HAPPY BIRTHDAY", Lyrics = $"{birhtdayString}" };
            fileHelper.SaveasHtmlForBirthday(birhtdayTemplate, MainSelector.Text);
            chromeHelper.RefreshChrome();
        }

        private void Marriage_Click(object sender, EventArgs e)
        {
            var CoupleNameList = CoupleName.Text.Replace("\r\n", "$").ToString().Split('$');
            var birthdayDateList = MarriedDate.Text.Replace("\r\n", "$").ToString().Split('$');
            var marriage = String.Empty;
            for (int i = 0; i < CoupleNameList.Count(); i++)
            {
                if (!String.IsNullOrWhiteSpace(CoupleNameList[i]))
                {
                    marriage = $"{marriage} <tr><td data-th =\"Name\"><b>      {CoupleNameList[i]}      <b></td><td data-th = \"Date\">{birthdayDateList[i]}</td></tr>";
                }
            }
            var birhtdayTemplate = new HtmlModel() { Header = "HAPPY WEDDING ANNIVERSARY", Lyrics = $"{marriage}" };
            fileHelper.SaveasHtmlForBirthday(birhtdayTemplate, MainSelector.Text);
            chromeHelper.RefreshChrome();
        }


    //    private void BirthdayExcel()
    //    {
    //        Microsoft.Office.Interop.Excel.Application xlApp;
    //        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
    //        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
    //        Microsoft.Office.Interop.Excel.Range range;

    //        string str;
    //        int rCnt;
    //        int cCnt;
    //        int rw = 0;
    //        int cl = 0;

    //        xlApp = new Microsoft.Office.Interop.Excel.Application();
    //        xlWorkBook = xlApp.Workbooks.Open(@"d:\bd.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
    //        xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

    //        range = xlWorkSheet.UsedRange;
    //        rw = range.Rows.Count;
    //        cl = range.Columns.Count;


    //        for (rCnt = 1; rCnt <= rw; rCnt++)
    //        {
    //            for (cCnt = 1; cCnt <= cl; cCnt++)
    //            {
    //                str = (string)(range.Cells[rCnt, cCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
    //                MessageBox.Show(str);
    //            }
    //        }

    //        xlWorkBook.Close(true, null, null);
    //        xlApp.Quit();

    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
    //        Marshal.ReleaseComObject(xlWorkBook);
    //        Marshal.ReleaseComObject(xlApp);

    //    }

    }

}