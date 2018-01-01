using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using TextApp.EntityFrameworkContext;

namespace TextApp.Models
{
    public class OrderPDF
    {
        CarsDBEntities db = new CarsDBEntities();
        public void CreateDocument(TOrders order)
        {
            var client = db.TClients.Where(u => u.Id == order.Client_id).FirstOrDefault();
            var car = db.TCars.Where(u => u.Id == order.Car_id).FirstOrDefault();
            Document doc = new Document(PageSize.A4);
            var output = new FileStream(@"D:\ASP.MVC\TextApp\TextApp\Mails\order.pdf", FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);

            doc.Open();

            var logo = iTextSharp.text.Image.GetInstance(@"D:\ASP.MVC\TextApp\TextApp\Mails\logo.png");
            logo.SetAbsolutePosition(430, 770);
            logo.ScaleAbsoluteHeight(50);
            logo.ScaleAbsoluteWidth(100);
            doc.Add(logo);

            Paragraph Title = new Paragraph("Zamowienie nr: " + order.Id);
            Title.Font.SetFamily(BaseFont.COURIER);
            Title.Font.Size = 23;
            Title.PaddingTop = 100;
            Title.Alignment = Element.ALIGN_CENTER;
            doc.Add(Title);

            PdfPTable CustomerData = new PdfPTable(1);
            PdfPCell TableTitle = new PdfPCell(new Phrase("Customer data"));
            TableTitle.HorizontalAlignment = 1;
            CustomerData.AddCell(TableTitle);
            CustomerData.AddCell("Name: " + client.Name);
            CustomerData.AddCell("Surname: " + client.Surname);
            CustomerData.AddCell("Email: " + client.Email);
            CustomerData.AddCell("Telephone: " + client.Telephone);
            doc.Add(CustomerData);

            doc.Close();
        }
        public void RemoveDocument()
        {
            File.Delete(@"D:\ASP.MVC\TextApp\TextApp\Mails\order.pdf");
        }
    }
}