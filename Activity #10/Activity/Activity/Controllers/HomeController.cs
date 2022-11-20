using iText.Barcodes;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Activity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Report()
        {
            string directory = Request.PhysicalApplicationPath + "Uploads\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string file = "File.pdf";

            PdfWriter pdfWriter = new PdfWriter(directory + file);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument, PageSize.A4.Rotate());

            Paragraph newLine = new Paragraph(new Text("\n"));
            document.Add(newLine);

            Paragraph text = new Paragraph();
            text.Add("Title").SetBold().SetItalic().SetFontSize(30).SetTextAlignment(TextAlignment.CENTER);
            document.Add(text);

            LineSeparator lineSeparator = new LineSeparator(new SolidLine());
            lineSeparator.SetMarginLeft(150).SetMarginRight(150);
            document.Add(lineSeparator);

            text = new Paragraph();
            text.Add("This is a just plain text.").SetTextAlignment(TextAlignment.CENTER);
            document.Add(text);

            ImageData image = ImageDataFactory.Create(Request.PhysicalApplicationPath + "Images\\github-logo.png");
            iText.Layout.Element.Image logo = new iText.Layout.Element.Image(image);
            logo.ScaleToFit(100, 100);
            logo.SetFixedPosition(20, 530);
            document.Add(logo);

            Link link = new Link("Hyperlink", PdfAction.CreateURI("www.github.com"));
            link.SetFontColor(ColorConstants.BLUE);
            document.Add(new Paragraph(link).SetFontSize(12).SetTextAlignment(TextAlignment.CENTER));

            BarcodeQRCode qrCode = new BarcodeQRCode("www.github.com");
            iText.Layout.Element.Image codeQrImage = new iText.Layout.Element.Image(qrCode.CreateFormXObject(pdfDocument));
            codeQrImage.ScaleAbsolute(100, 100).SetTextAlignment(TextAlignment.CENTER).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            document.Add(codeQrImage);

            document.Close();

            return File(directory + file, "application/pdf");
        }
    }
}
