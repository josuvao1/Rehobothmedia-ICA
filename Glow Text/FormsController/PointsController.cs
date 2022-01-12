using Glow_Text.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Point1_Click(object sender, EventArgs e)
        {
            templateName = MainSelector.Text;
            fileHelper.SaveasHtmlForNotice(templateName,HeaderText.Text, PointOneText.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }
        private void Point2_Click(object sender, EventArgs e)
        {
            templateName = MainSelector.Text;
            fileHelper.SaveasHtmlForNotice(templateName,HeaderText.Text, PointTwoText.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }

        private void Point3_Click(object sender, EventArgs e)
        {
            templateName = MainSelector.Text;
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point3Text.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }

        private void Point4_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point4Text.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }

        private void Point5_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point5Text.Text, FontSize.Text);
            chromeHelper.RefreshChrome();

        }

        private void Point6_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point6Text.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }
        private void Point7_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point7Text.Text, FontSize.Text);
            chromeHelper.RefreshChrome();
        }

        private void Point8_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForNotice(templateName, HeaderText.Text, Point8Text.Text,  FontSize.Text);
            chromeHelper.RefreshChrome();
        }

        private void AllPoints_Click(object sender, EventArgs e)
        {

        }
        private void Header_Click(object sender, EventArgs e)
        {
            fileHelper.SaveasHtmlForHeader(HeaderText.Text);
            chromeHelper.RefreshChrome();
        }


        private void AddFontSize_Click(object sender, EventArgs e)
        {
            FontSize.Text = (Convert.ToInt32(FontSize.Text) + 1).ToString();
        }
        private void FontSizeMinus_Click(object sender, EventArgs e)
        {
            FontSize.Text = (Convert.ToInt32(FontSize.Text) - 1).ToString();
        }
    }
}
