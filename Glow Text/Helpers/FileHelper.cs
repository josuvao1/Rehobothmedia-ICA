using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glow_Text.Helpers
{
   public class FileHelper
    {
       
        private readonly string fileLocation = Path.GetFullPath(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\WebTemplate\"));
        private readonly string fileLocationSong = Path.GetFullPath(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\WebTemplate\SongDataBase\")); 
       // string temp = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static string dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string LiveSongHtml = Path.GetFullPath(Path.Combine(dirPath, @"..\..\..\WebTemplate\SongLive.html"));
        public void SaveasHtmlForSong(string templateName, HtmlModel content)
        {
            
            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void OpenTemplate(string templateName)
        {
            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForNotice(string templateName, string header, string points, string fontsize = "55")
        {
            var content = new HtmlModel() { Header = header, Lyrics = points, FontSize = fontsize };

            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForHeader(string header)
        {
            var content = new HtmlModel() { Header = header};
           
            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\header.html"));
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForBible(HtmlModel content,string templateName)
        {

            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            //     fileName = temp.Replace(@"Glow Text\bin\Release", $"WebTemplate\\{template}.html");
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            content.Lyrics = content.Lyrics.Replace("<br>", "");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForBirthday(HtmlModel content, string templateName)
        {

            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            //     fileName = temp.Replace(@"Glow Text\bin\Release", $"WebTemplate\\{template}.html");
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
       
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForLowerThird(HtmlModel content, string templateName)
        {

            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            //     fileName = temp.Replace(@"Glow Text\bin\Release", $"WebTemplate\\{template}.html");
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            content.Lyrics = content.Lyrics.Replace("<br>", "");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }

        public void SaveasHtmlForBibleHeader(HtmlModel content, string templateName)
        {


            var fileName = Path.GetFullPath(Path.Combine(dirPath, $"..\\..\\..\\WebTemplate\\{templateName}.html"));
            // var fileName = SwitchCase(templateName);
            string songTemplate = File.ReadAllText($"{fileName}");
            songTemplate = songTemplate.Replace("{{Lyrics}}", content.Lyrics);
            songTemplate = songTemplate.Replace("{{Header}}", content.Header);
            songTemplate = songTemplate.Replace("{{FontSize}}", content.FontSize);
            CreateFile(ConfigurationManager.AppSettings["SongLive"], songTemplate, "html");
        }


        private string SwitchCase(string templateName)
        {
            switch (templateName)
            {
                case "Lower Third":
                    return ConfigurationManager.AppSettings["LowerThird"];
                case "Notice":
                    return ConfigurationManager.AppSettings["Notice"];
                case "Song 2":
                    return ConfigurationManager.AppSettings["Song2"];
                default:
                    return ConfigurationManager.AppSettings["Song"];
            }


        }

        public void CreateFile(string fileName, string songTemplate, string type)
        {
            using (FileStream fs = new FileStream($"{fileLocation}{fileName}.{type}", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(songTemplate);
                }
            }
        }

        public void CreateFileSong(string fileName, string songTemplate, string type)
        {
            using (FileStream fs = new FileStream($"{fileLocationSong}{fileName}.{type}", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(songTemplate);
                }
            }
        }



    }
}
