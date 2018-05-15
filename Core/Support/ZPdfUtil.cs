using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebAutomation.Core.Support
{
    public static class ZPdfUtil
    {
        /// <summary>
        /// See http://weblogs.sqlteam.com/mladenp/archive/2014/01/10/simple-merging-of-pdf-documents-with-itextsharp-5-4-5.aspx
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destPdf"></param>
        public static void MergeAll(string sourceDir, string destPdf)
        {
            using (FileStream stream = new FileStream(destPdf, FileMode.Create))
            {
                Document pdfDoc = new Document();
                PdfCopy pdf = new PdfCopy(pdfDoc, stream);
                Regex numpattern = new Regex(@"\d+(?=\.pdf)");
                pdfDoc.Open();
                List<string> files = Directory.GetFiles(sourceDir).ToList();
                files.Sort((a, b) => int.Parse(numpattern.Match(a).Value).CompareTo(int.Parse(numpattern.Match(b).Value)));

                Console.WriteLine("Merging files count: " + files.Count);
                int i = 1;
                foreach (string file in files)
                {
                    Console.WriteLine(i + ". Adding: " + file);
                    pdf.AddDocument(new PdfReader(file));
                    i++;
                }

                if (pdfDoc != null)
                    pdfDoc.Close();

                Console.WriteLine("SpeedPASS PDF merge complete.");
            }
        }

        public static void TrimLastPage(string inputPdf, string outputPdf)
        {
            using (PdfReader reader = new PdfReader(inputPdf))
            {
                reader.SelectPages($"1-{reader.NumberOfPages - 1}");

                using (PdfStamper stamper = new PdfStamper(reader, File.Create(outputPdf)))
                {
                    stamper.Close();
                }
            }
        }

        public static void TrimFirstPage(string inputPdf, string outputPdf)
        {
            using (PdfReader reader = new PdfReader(inputPdf))
            {
                reader.SelectPages($"2-{reader.NumberOfPages}");

                using (PdfStamper stamper = new PdfStamper(reader, File.Create(outputPdf)))
                {
                    stamper.Close();
                }
            }
        }

        /// <summary>
        /// See https://stackoverflow.com/questions/7246137/itextsharp-trimming-pdf-documents-pages#answer-29149552
        /// </summary>
        /// <param name="inputPdf">the full file name of hte input pdf</param>
        /// <param name="pageSelection">the pattern for page selection</param>
        /// <param name="outputPdf">the full file name of the output pdf</param>
        public static void SelectPages(string inputPdf, string pageSelection, string outputPdf)
        {
            using (PdfReader reader = new PdfReader(inputPdf))
            {
                reader.SelectPages(pageSelection);

                using (PdfStamper stamper = new PdfStamper(reader, File.Create(outputPdf)))
                {
                    stamper.Close();
                }
            }
        }
    }
}
