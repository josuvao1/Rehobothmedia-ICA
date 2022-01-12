using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Glow_Text.Models.CommonEnums;

namespace Glow_Text
{
    public partial class RehobothMedia : Form
    {
        static int VerseCounter = 1;
       // static string ApplicationUrl = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace(@"Glow Text\bin\Debug", "");
        string ApplicationUrl = Path.GetFullPath(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\"));
        string bibleVersion = "tamilBible";

        private void bibleBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            VerseCounter = Convert.ToInt32(Verse.Text);
            BibleVerse();
        }

        private void BibleVerse()
        {
            var bible = CallBibleOffline(Book: bibleBook.Text, Chapter: Chapter.Text, Verse: VerseCounter);
            if (bible.isVerseAvailable || String.IsNullOrEmpty(bible.Lyrics))
            {
                BiblePreviewText.Text = bible.Lyrics.Replace("</span>",String.Empty);
                fileHelper.SaveasHtmlForBible(bible, MainSelector.Text);
            }
            else
            {
                var template = "header";
                BiblePreview.Text = "Verse Not available";
              

            }
            chromeHelper.RefreshChrome();
        }

        private void Chapter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Verse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void SetBibleBookDrop()
        {
            var list = new List<string>();
            list = ConfigurationManager.AppSettings["BibleChapterDropDown"].Split(';').ToList();
            bibleBook.DataSource = list;
            bibleBook.Text = list.FirstOrDefault();
        }

        private void VersePrevious_Click(object sender, EventArgs e)
        {
            VerseCounter = VerseCounter - 1;

            Verse.Text = $"{VerseCounter}";
            BibleVerse();
        }

        private void NextVerse_Click(object sender, EventArgs e)
        {
            VerseCounter = VerseCounter + 1;

            Verse.Text = $"{VerseCounter}";
            BibleVerse();
        }
        //public HtmlModel CallBible(string Book, string Chapter, int Verse, string Kjv = "0")
        //{
        //    var BibleBookTemp = Book;
        //    if (Book.Contains("1"))
        //    {
        //        Book = Book.Replace("1 ", "I+");
        //    }
        //    else if (Book.Contains("2"))
        //    {

        //        Book = Book.Replace("2 ", "II+");
        //    }
        //    else if (Book.Contains("3"))
        //    {
        //        Book = Book.Replace("3 ", "III+");
        //    }
        //    var wr = WebRequest.Create($"http://www.tamil-bible.com/lookup.php?Book={Book}&Chapter={Chapter}&Verse={Verse}&Kjv={Kjv}");
        //    wr.Timeout = 3500;
        //    HtmlModel BibleVerseModel;
        //    VerseCounter = Verse;
        //    try
        //    {
        //        var response = (HttpWebResponse)wr.GetResponse();
        //        using (var stream = response.GetResponseStream())
        //        {
        //            var reader = new StreamReader(stream, Encoding.UTF8);
        //            var responseString = reader.ReadToEnd();
        //            var responseStringList = responseString.Replace("<ol>", "</ol>").Replace("</ol>", "$").Split('$');
        //            var bibleverse = responseStringList[1].Replace("$", "...");
        //            BibleVerseModel = new HtmlModel() { Header = $"{BibleBookTemp} {Chapter}: {Verse}", Lyrics = $"{bibleverse}" };
        //            return BibleVerseModel;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BibleVerseModel = new HtmlModel() { Header = $"{BibleBookTemp} {Chapter}: {Verse}", Lyrics = $"{ex.Message}", isVerseAvailable = false };
        //        return BibleVerseModel;
        //    }
        //}


        public HtmlModel CallBibleOffline(string Book, string Chapter, int Verse, string Kjv = "0")
        {
            var BibleBookTemp = Book;
            var fileName = String.Empty;
            if (Book.Contains("1"))
            {
                Book = Book.Replace("1 ", "I");
            }
            else if (Book.Contains("2"))
            {

                Book = Book.Replace("2 ", "II");
            }
            else if (Book.Contains("3"))
            {
                Book = Book.Replace("3 ", "III");
            }
            Enum.TryParse(Book.Replace(" ", String.Empty), out BibleChapters BileChapter);
            var BileChapterIndex = (int)BileChapter + 1;
            if (BileChapterIndex < 10)
            {
                fileName = $"{ApplicationUrl}BibleDataBase\\{bibleVersion}\\0{BileChapterIndex}\\{Chapter}.htm";
            }
            else
            {
                fileName = $"{ApplicationUrl}\\BibleDataBase\\{bibleVersion}\\{BileChapterIndex}\\{Chapter}.htm";
            }
            string BibleTemplate = File.ReadAllText($"{fileName}");

            HtmlModel BibleVerseModel;
            VerseCounter = Verse;
            try
            {
                if (bibleVersion == "marathiBible")
                {
                    var marResponseStringList = BibleTemplate.Replace($"id=\"{Verse}\"> {Verse}", "$").Split('$');
                    var marResponseString = marResponseStringList[1].Replace("<br />", "$").Split('$');
                    var marBibleVerseEndIndex = marResponseString[0].IndexOf("\r\n");

                    var marBibleverse = marResponseString[0].Remove(marBibleVerseEndIndex);
                    BibleVerseModel = new HtmlModel() { Header = $"{BibleBookTemp} {Chapter}: {Verse}", Lyrics = $"{marBibleverse}" };
                    return BibleVerseModel;
                }
                var responseStringList = BibleTemplate.Replace($"id=\"{Verse}\">{Verse}", "$").Split('$');
                var responseString = responseStringList[1].Replace("<br />", "$").Split('$');
                var bibleVerseEndIndex = responseString[0].IndexOf("\r\n");
                var bibleverse = responseString[0].Remove(bibleVerseEndIndex);

                BibleVerseModel = new HtmlModel() { Header = $"{BibleBookTemp} {Chapter}: {Verse}", Lyrics = $"{bibleverse}" };
                return BibleVerseModel;

            }
            catch (Exception ex)
            {
                BibleVerseModel = new HtmlModel() { Header = $"Verse Not Available", Lyrics = $"{ex.Message}", isVerseAvailable = false };
                return BibleVerseModel;
            }
        }



        private void tamil_CheckedChanged(object sender, EventArgs e)
        {
            bibleVersion = "tamilBible";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bibleVersion = "englishBible";
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            bibleVersion = "hindiBible";
        }

        private void marathi_CheckedChanged(object sender, EventArgs e)
        {
            bibleVersion = "marathiBible";
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Enter)
            {
                VerseCounter = Convert.ToInt32(Verse.Text);
                BibleVerse();
            }
        }



    }
}

