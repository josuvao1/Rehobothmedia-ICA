using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
       
        private void tabPage1_Click(object sender, EventArgs e)
        {
            templateName = "song";
            
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            songCounter = songCounter != 0 ? --songCounter : songCounter;
            ChoosLyrics();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            songCounter = 0;
            ChoosLyrics();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            songCounter = 1;
            ChoosLyrics();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            songCounter = 2;
            ChoosLyrics();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            songCounter = 3;
            ChoosLyrics();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            songCounter = 4;
            ChoosLyrics();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            songCounter = 5;
            ChoosLyrics();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            songCounter = 6;
            ChoosLyrics();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            songCounter = 7;
            ChoosLyrics();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            songCounter = 8;
            ChoosLyrics();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            songCounter = 9;
            ChoosLyrics();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            songCounter = 10;
            ChoosLyrics();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            songCounter = 11;
            ChoosLyrics();
        }


        private void button15_Click(object sender, EventArgs e)
        {
            songCounter = 12;
            ChoosLyrics();
        }


        private void button16_Click(object sender, EventArgs e)
        {
            songCounter = 13;
            ChoosLyrics();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            songCounter = 14;
            ChoosLyrics();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            songCounter = 15;
            ChoosLyrics();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            songCounter = 16;
            ChoosLyrics();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            songCounter = 17;
            ChoosLyrics();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            songCounter = 18;
            ChoosLyrics();
        }
        private void button22_Click(object sender, EventArgs e)
        {
            songCounter = 19;
            ChoosLyrics();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            songCounter = 20;
            ChoosLyrics();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            songCounter = 21;
            ChoosLyrics();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            songCounter = 22;
            ChoosLyrics();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            songCounter = 23;
            ChoosLyrics();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            songCounter = 24;
            ChoosLyrics();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            songCounter = 25;
            ChoosLyrics();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            songCounter = 26;
            ChoosLyrics();
        }

        private void InitializationOfbuttons()
        {
            SongLyricsbuttons = new List<Button>() { button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25, button26, button27, button28, button29 };
        }

        private void HideButtons()
        {
            button3.Visible = false;
            foreach (var button in SongLyricsbuttons)
            {
                button.Visible = false;
                button.BackColor = Color.LightGray;
                button.Font = new System.Drawing.Font("Century Gothic", 8.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            }

        }
        private void ChoosLyrics()
        {
            fileHelper.SaveasHtmlForSong(templateName, FinalHtmlModels[songCounter]);
            chromeHelper.RefreshChrome();
        }

    }
}
