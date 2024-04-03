using APIServices.Controllers;
using APIServices.Data;
using APIServices.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using static iTextSharp.text.Font;

namespace PunjabOrnaments.Service.APi.Controllers.PDFController
{
    public class PdfController : BaseApiController
    {
        public PdfController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpGet("Getallgoldpurchaserequests")]
        public string GeneratePDF()
        {
            try
            {
                var filesPath = Path.Combine(Directory.GetCurrentDirectory());
                var file = filesPath + "/Pdf"+ "/test.pdf";
                if (!System.IO.Directory.Exists(filesPath))//create path 
                {
                    Directory.CreateDirectory(filesPath);
                }
                else
                {
                    System.IO.File.Exists(file);
                    System.IO.File.Delete(file);
                }

                FileStream fs = new FileStream(file, FileMode.Create);

                // Creating a Document   
                Document document = new();

                // Creating a PdfWriter 
                PdfWriter writer = PdfWriter.GetInstance(document,fs);

                document.Open();

                var content = writer.DirectContent;
                var pageBorderRect = new Rectangle(document.PageSize);

                pageBorderRect.Left += document.LeftMargin;
                pageBorderRect.Right -= document.RightMargin;
                pageBorderRect.Top -= document.TopMargin;
                pageBorderRect.Bottom += document.BottomMargin;

                content.SetColorStroke(BaseColor.RED);
                content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
                content.Stroke();

                //add Default invoice text
                Font headerfont = new (FontFamily.HELVETICA, 25, Font.BOLD, BaseColor.WHITE);
                Chunk headerchunk = new("Invoice text", headerfont);
                headerchunk.SetBackground(BaseColor.DARK_GRAY);
                Paragraph invoice = new(headerchunk)
                {
                    Alignment = Element.ALIGN_CENTER,
                };
                document.Add(invoice);

                //add firm title
                Paragraph firmtitle = new("Title Header", new Font(Font.FontFamily.HELVETICA, 25, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(firmtitle);

                //add address1
                Paragraph firmAddress1 = new("Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit...\"\n", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(firmAddress1);

                //add address2
                Paragraph firmAddress2 = new("There is no one who loves pain itself, who seeks after it and wants to have it, simply because it is pain...", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(firmAddress2);

                // Closing the document 
                document.Close();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}

