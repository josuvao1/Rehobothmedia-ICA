using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
       // string temp = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static HtmlModel Image = new HtmlModel();

        private void LowerThird_Dropdown(object sender, EventArgs e)
        {
            var lowerThird = new HtmlModel() { Lyrics = $"{LowerThirdDropDown.Text}" };
            fileHelper.SaveasHtmlForLowerThird(lowerThird, MainSelector.Text);
            LowerThirdDropDown.Text = String.IsNullOrWhiteSpace(LowerThirdAdd.Text)? LowerThirdDropDown.Text : LowerThirdAdd.Text;
            //  chromeHelper.RefreshChrome();  -- commented this line to solve the name refresh bug 
        }

        private void Enter_LowerThird_Click(object sender, EventArgs e)
        {
            var lowerThird = new HtmlModel() { Lyrics = $"{LowerThirdDropDown.Text}" };
            fileHelper.SaveasHtmlForLowerThird(lowerThird, MainSelector.Text);
            chromeHelper.RefreshChrome();
        }

        private void Add_LowerThird_Click(object sender, EventArgs e)
        {
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\DropDownList\LowerThird.txt"));
            // var fileName = temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\LowerThird.txt");
            var nameText = File.ReadAllText(fileName);
            nameText = $"{nameText};{LowerThirdAdd.Text}";
            
            fileHelper.CreateFile(@"DropDownList\LowerThird", nameText, "txt");
            LowerThirdDropDown.DataSource = FiletoList("LowerThird");

        }
        public List<string> FiletoList(string filename)
        {
          
            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\DropDownList\\{filename}.txt"));
            var nameList = File.ReadAllText(fileName).Replace("\r\n", "").Split(';').ToList();
           
            return nameList;
        }

        private void Remove_LowerThird_Click(object sender, EventArgs e)
        {
            
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\DropDownList\LowerThird.txt"));
            var nameList = File.ReadAllText(fileName).Replace("\r\n",String.Empty).Split(';').ToList();

            nameList.Remove(LowerThirdDropDown.Text);
            string nameText = nameList.FirstOrDefault();
            nameList.RemoveAt(0);
            foreach (var names in nameList)
            {
                nameText = $"{nameText};{names}";
            }
            fileHelper.CreateFile(@"DropDownList\LowerThird", nameText, "txt");
            LowerThirdDropDown.DataSource = FiletoList("LowerThird");
        }

        private void BrowseImageButton(object sender, EventArgs e)
        {
          
           // var path = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\images\###"));

            //   var fileName = @"..\WebTemplate\images\###";
            //var fileName = temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\images\\###");
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\images\###"));
         
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                

                try
                {
                    string file = openFileDialog1.FileName;

                    var ImageName = file.Replace("\\", "$").Split('$').LastOrDefault();
                    fileName = fileName.Replace("###", ImageName);

                    System.IO.File.Copy(file, fileName, true);
                   // ImageName = file;
                     Image = new HtmlModel() { Lyrics = $"{ImageName}" };
                 
                    if (Potrait_Mode.CheckState == CheckState.Checked)
                    {
                        fileHelper.SaveasHtmlForLowerThird(Image, "PotraitImage");
                    }
                    else {

                        fileHelper.SaveasHtmlForLowerThird(Image, "Transprentlogo");

                    }
                  
                    chromeHelper.RefreshChrome();
                }
                catch (IOException ex)
                {
                }

            }
          

        }

        private void Potrait_Mode_CheckedChanged(object sender, EventArgs e)
        { if (!String.IsNullOrEmpty(Image.Lyrics))
            {
                if (Potrait_Mode.CheckState == CheckState.Checked)
                {
                    fileHelper.SaveasHtmlForLowerThird(Image, "PotraitImage");

                }
                else
                {
                    fileHelper.SaveasHtmlForLowerThird(Image, "Transprentlogo");
                }
                chromeHelper.RefreshChrome();

            }

        }
        private void BrowseVideoButton(object sender, EventArgs e)
        {
            var fileName =  Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\Video\###"));
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {


                try
                {
                    string file = openFileDialog1.FileName;

                    var ImageName = file.Replace("\\", "$").Split('$').LastOrDefault();
                    fileName = fileName.Replace("###", ImageName);

                    System.IO.File.Copy(file, fileName, true);

                    var Video = new HtmlModel() { Lyrics = $"{ImageName}" };
                    fileHelper.SaveasHtmlForLowerThird(Video, "VideoTemplate");
                    chromeHelper.RefreshChrome();
                }
                catch (IOException ex)
                {
                }

            }
        }

    


        private void Enter_AnimatedLowerThird(object sender, EventArgs e)
        {
            var Video = new HtmlModel() { Lyrics = $"{AnimatedLowerThird.Text}.mp4" };
            fileHelper.SaveasHtmlForLowerThird(Video, "AnimatedVideoLibrary");
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            chromeHelper.RefreshChrome();
            chromeHelper.LeftMouseClick(1410, 10);
            chromeHelper.SetCursorPosition(x, y);
        }

        private void Add_AnimatedlowerThird(object sender, EventArgs e)
        {
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\AnimatedVideoLibrary\###"));

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {


                try
                {
                    string file = openFileDialog1.FileName;

                    var VideoName = file.Replace("\\", "$").Split('$').LastOrDefault();
                    fileName = fileName.Replace("###", VideoName);
                    VideoName = VideoName.Replace(".mp4", String.Empty);
                    var LowerThirdFile = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\DropDownList\AnimatedVideoLibrary.txt"));
                    // var fileName = temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\LowerThird.txt");
                    var nameText = File.ReadAllText(LowerThirdFile);
                    nameText = $"{nameText};{VideoName}";
                    fileHelper.CreateFile(@"DropDownList\AnimatedVideoLibrary", nameText, "txt");
                    AnimatedLowerThird.DataSource = FiletoList("AnimatedVideoLibrary").OrderBy(x => x).ToList();
                    AnimatedVideoLibary2.DataSource = FiletoList("AnimatedVideoLibrary").OrderBy(x => x).ToList();
                    System.IO.File.Copy(file, fileName, true);

                    //var Video = new HtmlModel() { Lyrics = $"{VideoName}" };
                    //fileHelper.SaveasHtmlForLowerThird(Video, "VideoTemplate");
                    //chromeHelper.RefreshChrome();
                }
                catch (IOException ex)
                {

                }


            }
        }



        private void Enter_LowerThirdVideoLibrary(object sender, EventArgs e)
        {
            var Video = new HtmlModel() { Lyrics = $"{LowerThirdVideoLibrary.Text}.mp4" };
            fileHelper.SaveasHtmlForLowerThird(Video, "LowerThirdVideoLibrary");
            chromeHelper.RefreshChrome();
        }

        private void Add_LowerThirdVideoLibrary(object sender, EventArgs e)
        {
            var fileName = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\LowerThirdVideoLibrary\###"));

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {


                try
                {
                    string file = openFileDialog1.FileName;

                    var VideoName = file.Replace("\\", "$").Split('$').LastOrDefault();
                    fileName = fileName.Replace("###", VideoName);
                    VideoName = VideoName.Replace(".mp4", String.Empty);
                    var LowerThirdFile = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\DropDownList\LowerThirdVideoLibrary.txt"));
                    // var fileName = temp.Replace(@"Glow Text\bin\Debug", $"WebTemplate\\LowerThird.txt");
                    var nameText = File.ReadAllText(LowerThirdFile);
                    nameText = $"{nameText};{VideoName}";
                    fileHelper.CreateFile(@"DropDownList\LowerThirdVideoLibrary", nameText, "txt");
                    LowerThirdVideoLibrary.DataSource = FiletoList("LowerThirdVideoLibrary");

                    System.IO.File.Copy(file, fileName, true);

                    //var Video = new HtmlModel() { Lyrics = $"{VideoName}" };
                    //fileHelper.SaveasHtmlForLowerThird(Video, "VideoTemplate");
                    //chromeHelper.RefreshChrome();
                }
                catch (IOException ex)
                {

                }


            }
        }


        private void Enter1_Click(object sender, EventArgs e)
        {
            var Video = new HtmlModel() { Lyrics = $"{AnimatedVideoLibary2.Text}.mp4" };
            fileHelper.SaveasHtmlForLowerThird(Video, "AnimatedVideoLibrary");
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            chromeHelper.RefreshChrome();
            chromeHelper.LeftMouseClick(1410, 10);
            chromeHelper.SetCursorPosition(x, y);
        }


        private void PlayPauseButton_Click(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
           
            chromeHelper.LeftMouseClick(1410, 10);
            chromeHelper.SetCursorPosition(x, y);

            if (PlayPauseButton.Text == "Pause")
            {
                PlayPauseButton.Text = "Play";
            }
            else
            {
                PlayPauseButton.Text = "Pause";
            }
        }


        private void PlayPauseButton2_Click(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;

            chromeHelper.LeftMouseClick(1410, 10);
            chromeHelper.SetCursorPosition(x, y);

            if (PlayPauseButton.Text == "Pause")
            {
                PlayPauseButton.Text = "Play";
            }
            else
            {
                PlayPauseButton.Text = "Pause";
            }
        }



      
    }
}
