using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Glow_Text.Helpers;
using System.Configuration;
using Glow_Text.Models;


namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
        private Form prompt { get; set; }
        public string Result { get; }
        public string templateName = "Song";
        public List<string> songList = new List<string>();
        public static int songCounter = 0;
        public static int maxCounter = 0;
        public ChromeHelper chromeHelper = new ChromeHelper();
        public FileHelper fileHelper = new FileHelper();
        public List<Button> SongLyricsbuttons;
        public List<HtmlModel> FinalHtmlModels;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public RehobothMedia()
        {
            InitializeComponent();
        }

        private void RehobothMedia_Load(object sender, EventArgs e)
        {
            
            InitializationOfbuttons();
            HideButtons();
            SetDropDown();
            SetBibleBookDrop();
            SongDropdownList.DataSource = FiletoList("SongDataBaseList").OrderBy(x => x).ToList();
            LowerThirdDropDown.DataSource = FiletoList("LowerThird");
            AnimatedLowerThird.DataSource = FiletoList("AnimatedVideoLibrary").OrderBy(x => x).ToList();
            AnimatedVideoLibary2.DataSource = FiletoList("AnimatedVideoLibrary").OrderBy(x => x).ToList();
            LowerThirdVideoLibrary.DataSource = FiletoList("LowerThirdVideoLibrary");
            MainSelector.Text = Constants.Song;
            FontSize.Text = "100";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SumbitAction();
        }
        private void button30_Click(object sender, EventArgs e)
        {
            chromeHelper.OpenChrome();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            chromeHelper.CloseChrome();
        }
    
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            templateName = MainSelector.Text;
            var list = MainSelector.Items;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            templateName = MainSelector.Text = "LowerThird";
        }

        private void PointTab_Click(object sender, EventArgs e)
        {
            templateName = MainSelector.Text = "notice2";
        }

        private void SumbitAction()
        {
            FinalHtmlModels = new List<HtmlModel>();
            songList = new List<string>();
           
            this.HideButtons();
            var htmlSongContainer = textBox1.Text.Replace("\r\n","</br>").ToString();
            htmlSongContainer = htmlSongContainer.Replace("</br> </br>","$");
            htmlSongContainer = htmlSongContainer.Replace("</br></br>", "$");
            var listofStanza = htmlSongContainer.Split('$').ToList();
            var colour = new List<bool>();
            var setColourRed = true;
            if (Presentation.Checked == true)
            {
                MainSelector.Text = Constants.Presentation;
                songList.AddRange(listofStanza);
                foreach (var stanza in listofStanza)
                {
                    FinalHtmlModels.Add(new HtmlModel() { Lyrics = stanza, FontSize = "35px" });
                }
            }
            else
            {
                MainSelector.Text = Constants.Song;
                foreach (var song in listofStanza)
                {

                    var listOfLines = song.Replace("</br>", "$").Split('$').ToList();
                    List<string> z = new List<string>();
                    List<HtmlModel> htmlModels = new List<HtmlModel>();
                    string lineInHtmlFormat = String.Empty;
                    int lineCounter = 0;
                    foreach (var Line in listOfLines)
                    {
                        ++lineCounter;
                        if (lineCounter <= 1)
                        {
                            lineInHtmlFormat = $"{lineInHtmlFormat}{Line}</br>";
                        }
                        else
                        {
                            htmlModels.Add(new HtmlModel() { Lyrics = lineInHtmlFormat, FontSize = "35px" });
                            z.Add(lineInHtmlFormat);
                            colour.Add(setColourRed);
                            lineInHtmlFormat = $"{Line}</br>";
                            lineCounter = 1;
                        }

                    }
                    if ((templateName != "Lower Third") && lineCounter == 1)
                    {
                        htmlModels.Add(new HtmlModel() { Lyrics = lineInHtmlFormat, FontSize = "49px" });
                        z.Add($"</br>{lineInHtmlFormat}");
                    }
                    else
                    {
                        htmlModels.Add(new HtmlModel() { Lyrics = lineInHtmlFormat, FontSize = "35px" });
                        z.Add(lineInHtmlFormat);
                    }
                    colour.Add(setColourRed);
                    setColourRed = !setColourRed;
                    songList.AddRange(z);
                    FinalHtmlModels.AddRange(htmlModels);

                }

            }
            
            maxCounter = songList.Count() - 1;

            int counterforbutton = 0;

            foreach (var button in SongLyricsbuttons)
            {
                if (songList.Count() != 0 && (maxCounter >= counterforbutton))
                {
                    if (Presentation.Checked == true)
                    {
                        button.BackColor = Color.LightBlue;
                    }
                    else if (colour.FirstOrDefault())
                    {
                        button.BackColor = Color.LightBlue;
                        colour.RemoveAt(0);
                    }
                    else
                    {
                        button.BackColor = Color.LightGray;
                        colour.RemoveAt(0);
                    }
                    templateName = MainSelector.Text;
                    button.Visible = true;
                    button.Text = songList[counterforbutton] != null ? songList[counterforbutton].Replace("</br>", "\r\n") : String.Empty;
                }

                ++counterforbutton;
            }
        }

        private void SetDropDown()
        {
            var mainList = new List<string>();
            var imageList = new List<string>();
            var chapterlist = new List<string>();
            var verserlist = new List<string>();
            mainList = ConfigurationManager.AppSettings["DropDown"].Split(';').ToList();
            imageList = ConfigurationManager.AppSettings["ImageDropDown"].Split(';').ToList();
            MainSelector.DataSource = mainList;
           
            MainSelector.Text = mainList.FirstOrDefault();
            ImageDropDown.DataSource = imageList;
            ImageDropDown.Text = imageList.FirstOrDefault();
            //Chapter
            for (var i = 1; i <= 150; i++)
            {
                chapterlist.Add($"{i}");
                verserlist.Add($"{i}");

            }
            Chapter.DataSource = chapterlist;
            Verse.DataSource = verserlist;
            Chapter.Text = chapterlist.FirstOrDefault();
            Verse.Text = verserlist.FirstOrDefault();

        }


        private void Tabs_SelectedIndexChanged(object sender, TabControlEventArgs Tabsevent)
        {
                
            if (Tabsevent.TabPage == SongTab)
            {
                MainSelector.Text = Constants.Song;
            }
            else if (Tabsevent.TabPage == LowerThirdTab)
            {
                MainSelector.Text = Constants.OverLay;
            }
            else if (Tabsevent.TabPage == PointTab)
            {
                MainSelector.Text = Constants.Notice;
            }
            else if (Tabsevent.TabPage == BibleTab)
            {
                MainSelector.Text = Constants.Bible;
            }
            else if (Tabsevent.TabPage == BirthDayTab)
            {
                MainSelector.Text = Constants.BirthDay;
            }
        }

        private void OpenImage_Click(object sender, EventArgs e)
        {
            var Image = new HtmlModel() { Lyrics = $"{ ImageDropDown.Text}.png" };
         //   fileHelper.SaveasHtmlForLowerThird(Image, "Transprentlogo");
            fileHelper.OpenTemplate(Constants.RehobothMediaLoop);
            chromeHelper.RefreshChrome();
        }

        private void GreenScreen_Click(object sender, EventArgs e)
        {
            fileHelper.OpenTemplate(Constants.GreenScreen);
            chromeHelper.RefreshChrome();
        }

        private void SaveSong(object sender, EventArgs e)
        {
            var song = textBox1.Text;
            if (!String.IsNullOrEmpty(song))
            {
                var songName = ShowDialog("Save Song", "Enter Song Name");

                if (!String.IsNullOrEmpty(songName))
                {
                    var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\DropDownList\SongDataBaseList.txt"));
                   //  temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\SongDataBaseList.txt");
                    var nameText = File.ReadAllText(fileName);
                    nameText = $"{nameText};{songName}";
                    fileHelper.CreateFile(@"DropDownList\SongDataBaseList", nameText, "txt");
                    SongDropdownList.DataSource = FiletoList("SongDataBaseList").OrderBy(x => x).ToList();
                    fileHelper.CreateFileSong(songName, song, "txt");
                }
                else
                {
                    MessageBox.Show("Song Name Cannot be empty!", "Invalid Song Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Song Cannot be empty", "Invalid Song", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public static string ShowDialog(string caption, string text)
        {
            Form prompt = new Form();
            prompt.Width = 280;
            prompt.Height = 200;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 16, Top = 20, Width = 240, Text = text };
            TextBox textBox = new TextBox() { Left = 16, Top = 40, Width = 240, TabIndex = 0, TabStop = true, Multiline = true, BorderStyle = BorderStyle.None, Location = new System.Drawing.Point(15, 50), Font = new System.Drawing.Font("Arial",10)};
           
            Button confirmation = new Button() { Text = "Okay!", Left = 16, Width = 80, Top = 88, TabIndex = 1, TabStop = true };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
          
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.ShowDialog();
            return $"{textBox.Text}";
        }

        private void SongDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fileName = (SongDropdownList.Text).Replace("\r\n", "");
            fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\SongDataBase\\{fileName}.txt"));
           // fileName = temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\SongDataBase\\{fileName}.txt");
            textBox1.Text = File.ReadAllText(fileName);
        }

 
    }
}

