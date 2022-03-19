using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebApp.Command.Commands
{
    /// <summary>
    /// Diyagrama baktığımız zaman Receiver kısmına yani asıl işi yapıcak olan kısım, bizim ExcelFile'ımız ve PdfFile'ımız
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PdfFile<T>
    {
        public readonly List<T> _list;
        public readonly HttpContext _httpContext;

        public PdfFile(List<T> list, HttpContext httpContext)
        {
            _list = list;
            _httpContext = httpContext;
        }

        public string FileName => $"{typeof(T).Name}.pdf";
        public string FileType => "application/octet-stream";
        public MemoryStream Create()
        {
            var type = typeof(T);
            var sb = new StringBuilder();

            sb.Append($@"<html>
                            <head></head>
                            <body>
                            <div class = 'text-center'><h1>{type.Name} tablo </h1></div>
                             <table class='table table-stripped' align='center'>");

            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(p =>
            {
                sb.Append($"<th>{p.Name}</th>");
            });
            sb.Append("</tr");

            _list.ForEach(p =>
            {
                var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(p, null)).ToList();

                sb.Append("<tr>");
                values.ForEach(value =>
                {
                    sb.Append($"<td>{value}</td>");
                });
                sb.Append("</tr>");
            });

            sb.Append("</table></body></html>");


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
            },
                Objects = {
                    new ObjectSettings() {
                         PagesCount = true,
                         HtmlContent = sb.ToString(),
                         WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                         HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
            };

            var converter = _httpContext.RequestServices.GetRequiredService<IConverter>();

            MemoryStream pdfMemory = new(converter.Convert(doc));
            return pdfMemory;




        }
    }
}
