using APIServices.Controllers;
using APIServices.Data;
using APIServices.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.AspNetCore.Mvc;
using static iTextSharp.text.Font;
using Document = iTextSharp.text.Document;
using Rectangle = iTextSharp.text.Rectangle;

namespace PunjabOrnaments.Service.APi.Controllers.PDFController
{
    public class PdfController : BaseApiController
    {
        public static BaseColor darkcolor = BaseColor.BLACK;
        public static FontFamily font = FontFamily.HELVETICA;
        public PdfController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpGet("GeneratePDF")]
        public string GeneratePDF()
        {
            try
            {
                var filesPath = Path.Combine(Directory.GetCurrentDirectory());
                var file = filesPath + "/Pdf" + "/test.pdf";
                if (!System.IO.Directory.Exists(filesPath))//create path 
                {
                    Directory.CreateDirectory(filesPath);
                }
                else
                {
                    System.IO.File.Exists(file);
                    System.IO.File.Delete(file);
                }

                FileStream fs = new(file, FileMode.Create);

                // Creating a Document   
                Document document = new();

                // Creating a PdfWriter 
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                AddBoarder(document, writer);

                //title address type
                CreateDefaultText(document);

                //SNo and Date
                AddSnoanddate(document);

                //Add Blank line
                AddNewLine(document);

                //Consumer Address and payment detail
                AddConsumerDetailandPayment(document);

                // Closing the document 
                document.Close();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        private static void AddConsumerDetailandPayment(Document document)
        {
            PdfPTable table = new PdfPTable(2);

            table.AddCell("Name");
            table.AddCell("Bank Name");

            table.AddCell("Name");
            table.AddCell("Address");

            table.AddCell("Cheque No.");

            table.AddCell("Mobile");
            table.AddCell("Amount");

            table.AddCell("Payment Type");
            table.AddCell("Payment Reference");

            document.Add(table);
        }

        private static void AddSnoanddate(iTextSharp.text.Document document)
        {
            Paragraph para = new ();
            Chunk glue = new (new VerticalPositionMark());
            Phrase ph1 = new()
            {
                new Chunk(Environment.NewLine)
            };
            string Sno = "S.No" + "{12345}".ToString();
            string billdate = "Date" + "-" + "{EndDate}".ToString();
            Paragraph main = new ();
            ph1.Add(new Chunk(Sno)); // Here I add projectname as a chunk into Phrase.    
            ph1.Add(glue);  // Here I add special chunk to the same phrase.    
            ph1.Add(new Chunk(billdate)); // Here I add date as a chunk into same phrase.    
            main.Add(ph1);
            para.Add(main);
            document.Add(para);
        }

        private static void CreateDefaultText(iTextSharp.text.Document document)
        {
            //add Default invoice text
            Font headerfont = new(font, 25, Font.BOLD, BaseColor.WHITE);
            Chunk headerchunk = new("Invoice text", headerfont);
            headerchunk.SetBackground(darkcolor);
            Paragraph invoice = new(headerchunk)
            {
                Alignment = Element.ALIGN_CENTER,
            };
            document.Add(invoice);

            //add firm title
            Paragraph firmtitle = new("Title Header", new Font(font, 25, Font.BOLD))
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(firmtitle);

            //add address1
            Paragraph firmAddress1 = new("Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...\"\n", new Font(font, 16, Font.BOLD))
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(firmAddress1);

            //add address2
            Paragraph firmAddress2 = new("There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...", new Font(font, 16, Font.BOLD))
            {
                Alignment = Element.ALIGN_CENTER
            };
            document.Add(firmAddress2);
        }

        private static void AddBoarder(iTextSharp.text.Document document, PdfWriter writer)
        {
            var content = writer.DirectContent;
            var pageBorderRect = new Rectangle(document.PageSize);

            pageBorderRect.Left += document.LeftMargin;
            pageBorderRect.Right -= document.RightMargin;
            pageBorderRect.Top -= document.TopMargin;
            pageBorderRect.Bottom += document.BottomMargin;

            content.SetColorStroke(darkcolor);
            content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            content.Stroke();
        }

        private static void AddNewLine(iTextSharp.text.Document document)
        {
            document.Add(new Paragraph("\n"));
        }
    }
}

