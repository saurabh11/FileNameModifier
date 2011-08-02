using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FilenNameModifier
{
    class Program
    {

        /* The CapitalizeFirstLetter function is used to detect if the
         * first letter of the filename is lowercase. If it is lowercase, 
         * than the letter will be changed to uppercase
         * E.g.: originalFilename = mahabharat.pdf
         * firstLetterCapitalized = Mahabharat.pdf */         
        static string CapitalizeFirstLetter(string originalFilename)
        {

            string firstCaseLower = originalFilename;
            string firstLetterCapitalized;
            char a = firstCaseLower[0];

            if ((a >= 'a') && (a <= 'z'))
            {

                StringBuilder capitalizer = new StringBuilder(firstCaseLower);
                capitalizer[0] = char.ToUpper(capitalizer[0]);
                firstLetterCapitalized = firstCaseLower.Replace(firstCaseLower[0], capitalizer[0]);

                return firstLetterCapitalized;

            }
            else
            {

                return firstCaseLower;

            }

        }

        /* The InsertUnderscoreBetweenWords function is used to 
         * insert an underscore ("_")before a capital letter in 
         * the filename except for the first letter of the filename.          
         * E.g.: originalFilename = AliceInWonderland.pdf
         * filenameWithUnderscore = Alice_In_Wonderland.pdf */
        static string InsertUnderscoreBetweenWords(string originalFilename)
        {

            string filenameWithUnderscore = originalFilename;

            // "i" is starting from 1 as first letter is already capitalized.
            // "i" is terminated at "filenameWithUnderscore.Length - 4" to avoid cycles in the .<extension>
            for (int i = 1; i <= filenameWithUnderscore.Length - 4; i++)
            {
                if ((int)filenameWithUnderscore[i] == '_') // To check if filename already has '_'
                {

                    break;

                }

                if ((filenameWithUnderscore[i] >= 'A') && (filenameWithUnderscore[i] <= 'Z'))
                {

                    filenameWithUnderscore = filenameWithUnderscore.Insert(i, "_");
                    i++;

                }
                
            }

            return filenameWithUnderscore;

        }

        /* The UpdateFileName function internally calls the
         * CapitalizeFirstLetter and InsertUnderscoreBetweenWords
         * functions which are explained above. */
        static string UpdateFileName(string originalFilename)
        {

            string updatedFilename;
            string filenameWithUnderscore;
            string firstCaseLower = originalFilename;
            string firstLetterCapitalized;

            firstLetterCapitalized = CapitalizeFirstLetter(firstCaseLower);

            filenameWithUnderscore = InsertUnderscoreBetweenWords(firstLetterCapitalized);

            updatedFilename = filenameWithUnderscore;

            return updatedFilename;

        }

        /* The FinalizeFileName function internally calls the
         * UpdateFileName function which modifies the filename.
         * The FinalizeFileName function then replaces the names
         * of the existing files with the modified names */
        static void FinalizeFileName()
        {
                        
            string updatedFilename;
            string finalFilename;
            const string folder = @"C:\Documents and Settings\sagupta\Desktop\Icons\Saurabh\Copy of BBC.Top.100.eBooks\";

            DirectoryInfo sourceDir = new DirectoryInfo(folder);
            FileInfo[] sourceFileArray = sourceDir.GetFiles();

            Debug.Assert(sourceFileArray.Count() > 0);

            foreach (FileInfo file in sourceFileArray)
            {

                updatedFilename = UpdateFileName(file.Name);
                finalFilename = folder + updatedFilename;
                file.MoveTo(finalFilename);                
                
            }                 
            
        }
        

        static void Main(string[] args)
        {

            FinalizeFileName();

        }
    }
}
