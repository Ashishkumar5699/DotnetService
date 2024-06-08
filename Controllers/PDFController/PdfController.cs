using APIServices.Controllers;
using APIServices.Data;
using APIServices.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using PunjabOrnaments.Common.Bills;
using static iTextSharp.text.Font;
using Document = iTextSharp.text.Document;
using Rectangle = iTextSharp.text.Rectangle;

namespace PunjabOrnaments.Service.APi.Controllers.PDFController
{
    public class PdfController : BaseApiController
    {
        public static BaseColor darkcolor = BaseColor.BLACK;
        public static FontFamily font = FontFamily.HELVETICA;

        private bool isInvoice = false;
        public PdfController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }


        [HttpPost("GeneratePDFGPT")]
        public byte[] GeneratePDFGPT(PrintBillModel printBillModel)
        {
            // Creating a Document   
            //Document document = new();
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document document = new Document())
                    {
                        var writer = PdfWriter.GetInstance(document, ms);
                        document.Open();

                        // Set fonts
                        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Font titleFont = new Font(baseFont, 20, Font.BOLD);
                        Font headingFont = new Font(baseFont, 14, Font.BOLD);
                        Font normalFont = new Font(baseFont, 12, Font.NORMAL);

                        // Add invoice title
                        Paragraph title = new Paragraph("Invoice", titleFont);
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);
                        AddBoarder(document, writer);
                        // Create a table for invoice details
                        PdfPTable invoiceDetailsTable = new PdfPTable(2);
                        invoiceDetailsTable.WidthPercentage = 100;
                        invoiceDetailsTable.DefaultCell.Border = Rectangle.NO_BORDER;

                        PdfPCell invoiceNumberCell = new PdfPCell(new Phrase("Quatation Number: 01", normalFont));
                        invoiceNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        invoiceNumberCell.Border = Rectangle.NO_BORDER;
                        invoiceDetailsTable.AddCell(invoiceNumberCell);

                        PdfPCell dateCell = new PdfPCell(new Phrase("Date of Billing: " + DateTime.Now.ToString("yyyy-MM-dd"), normalFont));
                        dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        dateCell.Border = Rectangle.NO_BORDER;
                        invoiceDetailsTable.AddCell(dateCell);

                        document.Add(invoiceDetailsTable);

                        // Create a table for company and seller details
                        PdfPTable companySellerTable = new PdfPTable(2);
                        companySellerTable.WidthPercentage = 100;
                        companySellerTable.DefaultCell.Border = Rectangle.NO_BORDER;

                        if (isInvoice)
                        {
                            PdfPCell gstNumberCell = new PdfPCell(new Phrase("GST Number: 123456789", normalFont));
                            gstNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            gstNumberCell.Border = Rectangle.NO_BORDER;
                            companySellerTable.AddCell(gstNumberCell);
                        }

                        PdfPCell mobileNumberCell = new PdfPCell(new Phrase("Mobile Number: 123-456-7890", normalFont));
                        mobileNumberCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        mobileNumberCell.Border = Rectangle.NO_BORDER;
                        companySellerTable.AddCell(mobileNumberCell);

                        document.Add(companySellerTable);

                        // Add details inside a box
                        PdfPTable detailsTable = new PdfPTable(1);
                        detailsTable.WidthPercentage = 100;
                        detailsTable.SpacingBefore = 10f;

                        PdfPCell detailsCell = new PdfPCell();
                        detailsCell.Border = Rectangle.BOX;

                        // Add name, address, Aadhar number, GST number, PAN number, State, and State code
                        LeftRightTextforConsumer(normalFont, "Name: Your Name\n", "Mobile: 1234567890", detailsCell);
                        LeftRightTextforConsumer(normalFont, "Address: Your Address\n", "City : GKP", detailsCell);
                        LeftRightTextforConsumer(normalFont, "Aadhar Number: 1234 5678 9012\n", "GST Number: 123456789012345\n", detailsCell);
                        LeftRightTextforConsumer(normalFont, "PAN Number: ABCDE1234F\n", "Mobile: 1234567890", detailsCell);
                        LeftRightTextforConsumer(normalFont, "State: Your State\n", "State Code: 12", detailsCell);

                        detailsTable.AddCell(detailsCell);
                        document.Add(detailsTable);

                        // Add payment details
                        PdfPTable table = new PdfPTable(9);
                        table.WidthPercentage = 100;
                        //table.SpacingBefore = 20f;

                        string[] header = { "Description", "HSN Code", "Purity", "Weight", "Rate", "Making Charge", "Amount" };

                        int columnIndex = 0;
                        foreach (var item in header)
                        {
                            AddCells(normalFont, table, item.ToString(), columnIndex == 0 ? 3 : 0, BaseColor.LIGHT_GRAY);
                            columnIndex++;
                        }

                        var items = new List<ProductModel>(printBillModel.ProductList);

                        foreach (var item in items)
                        {
                            columnIndex = 0;
                            foreach (var properties in typeof(ProductModel).GetProperties())
                            {
                                AddCells(normalFont, table, properties.GetValue(item).ToString(), columnIndex == 0 ? 3 : 0);
                                columnIndex++;
                            }
                            columnIndex = 0;
                        }

                        //Amount Section
                        //var amount = $" Amount in Words : {ConvertAmountToWords(145700)}";
                        var AmountinWordHeading = new Chunk("Amount in Words : ", titleFont);
                        var amountvalue = new Chunk($"{ConvertAmountToWords((int)printBillModel.GSTAmount.GrandTotal)} ₹ INR Only", normalFont);
                        var AmountinWordFulltext = new Paragraph
                        {
                            AmountinWordHeading,
                            "\n",
                            amountvalue
                        };
                        //AddCells(normalFont, table, amount.ToString(), 5);
                        PdfPCell paymentHeaderCells = new PdfPCell(AmountinWordFulltext);
                        paymentHeaderCells.Rowspan = 6;
                        paymentHeaderCells.Colspan = 5;
                        paymentHeaderCells.BorderColorTop = BaseColor.WHITE;
                        table.AddCell(paymentHeaderCells);

                        AddCells(normalFont, table, "Discount:", 2);
                        AddCells(normalFont, table, "amount", 2);

                        AddCells(normalFont, table, "total:", 2);
                        AddCells(normalFont, table, "amount", 2);

                        AddCells(normalFont, table, "CGST 1.5%:", 2);
                        AddCells(normalFont, table, "amount", 2);

                        AddCells(normalFont, table, "SGST 1.5%", 2);
                        AddCells(normalFont, table, "amount", 2);

                        AddCells(normalFont, table, "IGST 1.5%:", 2);
                        AddCells(normalFont, table, "amount", 2);

                        AddCells(normalFont, table, "Total", 2);
                        AddCells(normalFont, table, "amount", 2);

                        //PdfPCell amountinWordCells = new PdfPCell(new Phrase(ConvertAmountToWords(145700), normalFont));
                        //amountinWordCells.Colspan = 5;
                        //amountinWordCells.BorderColorTop = BaseColor.WHITE;
                        //table.AddCell(amountinWordCells);
                        ////document.Add(amountinWordCells);
                        //detailsTable.AddCell(table);
                        document.Add(table);

                        // Add total amount
                        Paragraph totalAmount = new Paragraph("\nTotal Amount: $1150", titleFont);
                        document.Add(totalAmount);
                        document.Close();
                        return ms.ToArray();
                    }
                }
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return new MemoryStream().ToArray();
        }

        // Staic Functions
        private static void AddCells(Font normalFont, PdfPTable table, string data, int Colspan, BaseColor baseColor = null)
        {
            PdfPCell paymentHeaderCells = new PdfPCell(new Phrase(data, normalFont));
            paymentHeaderCells.Colspan = Colspan;
            paymentHeaderCells.BorderColorTop = BaseColor.WHITE;
            paymentHeaderCells.BackgroundColor = baseColor?? BaseColor.WHITE;
            table.AddCell(paymentHeaderCells);
        }

        private static void LeftRightTextforConsumer(Font normalFont,string lefttext, string Righttext, PdfPCell document)
        {
            PdfPTable conumerdetailsTable = new PdfPTable(2);
            conumerdetailsTable.WidthPercentage = 100;
            conumerdetailsTable.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell nameCell = new PdfPCell(new Phrase(lefttext, normalFont));
            nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nameCell.Border = Rectangle.NO_BORDER;
            conumerdetailsTable.AddCell(nameCell);

            PdfPCell mobileCell = new PdfPCell(new Phrase(Righttext, normalFont));
            mobileCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            mobileCell.Border = Rectangle.NO_BORDER;
            conumerdetailsTable.AddCell(mobileCell);
            document.AddElement(conumerdetailsTable);
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

        private static string ConvertAmountToWords(int amount)
        {
            if (amount == 0)
                return "Zero";

            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string words = "";

            // Billion
            if (amount >= 1000000000)
            {
                words += ConvertAmountToWords(amount / 1000000000) + " Billion ";
                amount %= 1000000000;
            }

            // Million
            if (amount >= 1000000)
            {
                words += ConvertAmountToWords(amount / 1000000) + " Million ";
                amount %= 1000000;
            }

            // Thousand
            if (amount >= 1000)
            {
                words += ConvertAmountToWords(amount / 1000) + " Thousand ";
                amount %= 1000;
            }

            // Hundred
            if (amount >= 100)
            {
                words += ConvertAmountToWords(amount / 100) + " Hundred ";
                amount %= 100;
            }

            // Tens and Units
            if (amount >= 20)
            {
                words += tens[amount / 10] + " ";
                amount %= 10;
            }
            else if (amount >= 10)
            {
                words += teens[amount - 10] + " ";
                amount = 0;
            }

            if (amount > 0)
            {
                words += units[amount] + " ";
            }

            return words.Trim();
        }
    }
}

