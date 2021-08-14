using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace adobe_font_extractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(
                "*******************\n" +
                "Adobe Font Revealer\n" +
                "*******************\n\n" +

                "Select an option:\n" +
                "1) Copy Files\n" +
                "2) Extract Font Names"
            );
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    CopyFonts();
                    break;
                case "2":
                    NameFonts();
                    break;
                default:
                    Console.WriteLine("Please input an option.");
                    Main(args);
                    break;
            }
        }

        static void CopyFonts()
        {
            Console.WriteLine("Specify Adobe Font folder? (y/n)");
            string userInputFont = Console.ReadLine();
            
            string fontFolderPath = "";
            switch (userInputFont)
            {
                case "y":
                    Console.WriteLine("Adobe Fonts folder path:");
                    fontFolderPath = Console.ReadLine();
                    break;
                case "n":
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    fontFolderPath = appDataFolder + @"\Adobe\CoreSync\plugins\livetype\r";
                    break;
            }

            Console.WriteLine("Specify output folder? (will default to Documents\\Adobe\\Fonts) (y/n)");
            string userInputOutput = Console.ReadLine();
            
            string outputFolder = "";
            switch (userInputOutput)
            {
                case "y":
                    Console.WriteLine("Adobe Fonts folder path:");
                    outputFolder = Console.ReadLine();
                    if (!outputFolder.EndsWith(@"\")) {
                        outputFolder += @"\";
                    }
                    break;
                case "n":
                    string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    outputFolder = documentsFolder + @"\Adobe\Fonts\";
                    break;
            }
            
            string[] fontList = System.IO.Directory.GetFiles(fontFolderPath);
            foreach (string fontPath in fontList) {
                PrivateFontCollection fontCollection = new PrivateFontCollection();

                fontCollection.AddFontFile(fontPath);

                string fontName = fontCollection.Families[0].Name;

                string newFontPath = Path.ChangeExtension(outputFolder + fontName, ".otf");

                File.Copy(fontPath, newFontPath, true);

                string[] fontPathArray = fontPath.Split('\\');

                Console.WriteLine(fontPathArray[fontPathArray.Length - 1] + "\t c→ \t" + fontName + ".otf");

                fontCollection.Dispose();
            }
        }

        static void NameFonts()
        {
            Console.WriteLine("Specify Adobe Fonts folder? (y/n)");
            string userInputFont = Console.ReadLine();
            
            string fontFolderPath = "";
            switch (userInputFont)
            {
                case "y":
                    Console.WriteLine("Adobe Fonts folder path:");
                    fontFolderPath = Console.ReadLine();
                    break;
                case "n":
                    string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    fontFolderPath = appDataFolder + @"\Adobe\CoreSync\plugins\livetype\r";
                    break;
            }

            string[] fontList = System.IO.Directory.GetFiles(fontFolderPath);
            foreach (string fontPath in fontList) {
                PrivateFontCollection fontCollection = new PrivateFontCollection();

                fontCollection.AddFontFile(fontPath);

                string fontName = fontCollection.Families[0].Name;

                string[] fontPathArray = fontPath.Split('\\');

                Console.WriteLine(fontPathArray[fontPathArray.Length - 1] + "\t → \t" + fontName + ".otf");

                fontCollection.Dispose();
            }
        }
    }
}