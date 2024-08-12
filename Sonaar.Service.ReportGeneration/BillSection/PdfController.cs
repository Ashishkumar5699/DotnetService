using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Sonaar.Domain.Bills;
using Sonaar.Domain.Response;
using Sonaar.Controllers;
using Sonaar.Data;
using Sonaar.Domain.Dto;
using Sonaar.Interface;
using static iTextSharp.text.Font;
using Sonaar.Domain.Dto.ReportGeneration;

namespace Sonaar.Service.ReportGeneration.BillSection
{
    public class PdfController : BaseApiController
    {
        public static BaseColor darkcolor = BaseColor.BLACK;
        public readonly FontFamily font = FontFamily.HELVETICA;

        private bool isInvoice = false;

        public PdfController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpPost("GeneratePDFGPT")]
        public ResponseResult<byte[]> GeneratePDFGPT(PrintBillDto printBillModel)
        {
            // Creating a Document   
            //Document document = new();
            var response = new ResponseResult<byte[]>();
            try
            {
                using var ms = new MemoryStream();
                using var document = new Document();
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Set fonts
                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                var titleFont = new Font(baseFont, 20, BOLD);
                var headingFont = new Font(baseFont, 14, BOLD);
                var normalFont = new Font(baseFont, 12, NORMAL);

                // Add invoice title
                var title = new Paragraph(printBillModel.BillType.ToString(), titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);
                AddBoarder(document, writer);
                // Create a table for invoice details
                var invoiceDetailsTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };

                invoiceDetailsTable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell invoiceNumberCell = new PdfPCell(new Phrase($"{printBillModel.BillType} Number: {printBillModel.Billid}", normalFont));
                invoiceNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                invoiceNumberCell.Border = Rectangle.NO_BORDER;
                invoiceDetailsTable.AddCell(invoiceNumberCell);

                var dateCell = new PdfPCell(new Phrase("Date of Billing: " + printBillModel.DateofBill.ToString("yyyy-MM-dd"), normalFont));
                dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                dateCell.Border = Rectangle.NO_BORDER;
                invoiceDetailsTable.AddCell(dateCell);

                document.Add(invoiceDetailsTable);

                // Create a table for company and seller details
                var companySellerTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };

                companySellerTable.DefaultCell.Border = Rectangle.NO_BORDER;

                if (isInvoice)
                {
                    PdfPCell gstNumberCell = new PdfPCell(new Phrase($"GST Number: {printBillModel.FirmDetail.FirmGSTNumber}", normalFont));
                    gstNumberCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    gstNumberCell.Border = Rectangle.NO_BORDER;
                    companySellerTable.AddCell(gstNumberCell);
                }

                var mobileNumberCell = new PdfPCell(new Phrase($"Phone Number: {printBillModel.FirmDetail.FirmPhoneNumber}", normalFont))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    Border = Rectangle.NO_BORDER
                };

                companySellerTable.AddCell(mobileNumberCell);

                document.Add(companySellerTable);

                // Add details inside a box
                PdfPTable detailsTable = new PdfPTable(1);
                detailsTable.WidthPercentage = 100;
                detailsTable.SpacingBefore = 10f;

                PdfPCell detailsCell = new PdfPCell();
                detailsCell.Border = Rectangle.BOX;

                // Add name, address, Aadhar number, GST number, PAN number, State, and State code
                LeftRightTextforConsumer(normalFont, $"Name: {printBillModel.Consumer.ContactFirstName}", $"Mobile: {printBillModel.Consumer.ContactPhoneNumber}", detailsCell);
                LeftRightTextforConsumer(normalFont, $"Address: {printBillModel.Consumer.ContactAddress1}", $"City : {printBillModel.Consumer.ContactCity}", detailsCell);
                LeftRightTextforConsumer(normalFont, $"Aadhar Number: {printBillModel.Consumer.AdharNumber}", $"GST Number: {printBillModel.Consumer.CustmorGSTNumber}", detailsCell);
                LeftRightTextforConsumer(normalFont, $"PAN Number: {printBillModel.Consumer.PanNumber}", $"Zip Code: {printBillModel.Consumer.CustmorZipCode}", detailsCell);
                LeftRightTextforConsumer(normalFont, $"State: {printBillModel.Consumer.ContactState}", $"Country: {printBillModel.Consumer.CustmorCountry}", detailsCell);

                detailsTable.AddCell(detailsCell);
                document.Add(detailsTable);

                // Add payment details
                var table = new PdfPTable(9)
                {
                    WidthPercentage = 100
                };
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
                var paymentHeaderCells = new PdfPCell(AmountinWordFulltext)
                {
                    Rowspan = 6,
                    Colspan = 5,
                    BorderColorTop = BaseColor.WHITE
                };

                table.AddCell(paymentHeaderCells);

                var gstamount = printBillModel.GSTAmount;
                AddCells(normalFont, table, "Total Before Discount:", 2);
                AddCells(normalFont, table, $"{gstamount.TotalBeforeDiscount}", 2);

                AddCells(normalFont, table, "Discount:", 2);
                AddCells(normalFont, table, $"{gstamount.Discount}", 2);

                AddCells(normalFont, table, "total:", 2);
                AddCells(normalFont, table, $"{gstamount.TotalAfterDiscount}", 2);

                AddCells(normalFont, table, "CGST 1.5%:", 2);
                AddCells(normalFont, table, $"{gstamount.CGSt}", 2);

                AddCells(normalFont, table, "SGST 1.5%", 2);
                AddCells(normalFont, table, $"{gstamount.SGST}", 2);

                //AddCells(normalFont, table, "IGST 1.5%:", 2);
                //AddCells(normalFont, table, "amount", 2);

                AddCells(normalFont, table, "Total", 2);
                AddCells(normalFont, table, $"{gstamount.GrandTotal}", 2);

                //PdfPCell amountinWordCells = new PdfPCell(new Phrase(ConvertAmountToWords(145700), normalFont));
                //amountinWordCells.Colspan = 5;
                //amountinWordCells.BorderColorTop = BaseColor.WHITE;
                //table.AddCell(amountinWordCells);
                ////document.Add(amountinWordCells);
                //detailsTable.AddCell(table);
                document.Add(table);

                // Add total amount
                var totalAmount = new Paragraph($"\nTotal Amount: {gstamount.GrandTotal}", titleFont);
                document.Add(totalAmount);
                document.Close();
                response.HasErrors = false;
                response.IsSystemError = false;
                response.Data = ms.ToArray();
            }
            catch (DocumentException de)
            {
                response.Message = de.Message;
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                response.Message = ioe.Message;
                Console.Error.WriteLine(ioe.Message);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                Console.Error.WriteLine(ex.Message);
            }

            return response;
        }

        private static void AddCells(Font normalFont, PdfPTable table, string data, int Colspan, BaseColor? baseColor = null)
        {
            PdfPCell paymentHeaderCells = new PdfPCell(new Phrase(data, normalFont));
            paymentHeaderCells.Colspan = Colspan;
            paymentHeaderCells.BorderColorTop = BaseColor.WHITE;
            paymentHeaderCells.BackgroundColor = baseColor ?? BaseColor.WHITE;
            table.AddCell(paymentHeaderCells);
        }

        private static void LeftRightTextforConsumer(Font normalFont, string lefttext, string Righttext, PdfPCell document)
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

        //private static void AddNewLine(iTextSharp.text.Document document)
        //{
        //    document.Add(new Paragraph("\n"));
        //}

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

