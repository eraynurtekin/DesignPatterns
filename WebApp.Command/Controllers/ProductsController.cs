using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using WebApp.Command.Commands;
using WebApp.Command.Models;

namespace WebApp.Command.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public ProductsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        public async Task<IActionResult> CreateFile(int type)
        {
            var products = await _context.Products.ToListAsync();

            FileCreateInvoker fileCreateInvoker = new FileCreateInvoker();

            

            EFileType fileType = (EFileType)type;

            switch (fileType)
            {
                case EFileType.Excel:
                    
                    ExcelFile<Product> excelFile = new ExcelFile<Product>(products);
                    fileCreateInvoker.SetCommand(new CreateExcelTableActionCommand<Product>(excelFile));
                    break;

                case EFileType.Pdf:
                    
                    PdfFile<Product> pdfFile = new(products, HttpContext);
                    fileCreateInvoker.SetCommand(new CreatePDFTableActionCommand<Product>(pdfFile));
                    break;
                default:
                    break;
            }

            return fileCreateInvoker.CreateFile();
        }

        public async Task<IActionResult> CreateFiles()
        {
            var products = await _context.Products.ToListAsync();
            ExcelFile<Product> excelFile = new ExcelFile<Product>(products);
            PdfFile<Product> pdfFile = new(products, HttpContext);

            FileCreateInvoker fileCreateInvoker = new FileCreateInvoker();


            fileCreateInvoker.AddCommand(new CreateExcelTableActionCommand<Product>(excelFile));
            fileCreateInvoker.AddCommand(new CreatePDFTableActionCommand<Product>(pdfFile));

            var fileResult = fileCreateInvoker.CreateFiles();

            using(var zipMemoryStream = new MemoryStream())
            {

                using(var archive = new ZipArchive(zipMemoryStream,ZipArchiveMode.Create))
                {
                    foreach(var item in fileResult)
                    {
                        var fileContent = item as FileContentResult;

                        var zipFile = archive.CreateEntry(fileContent.FileDownloadName);

                        using (var zipEntryStream = zipFile.Open())
                        {
                            await new MemoryStream(fileContent.FileContents).CopyToAsync(zipEntryStream);
                        }
                    }

                }

                return File(zipMemoryStream.ToArray(), "application/zip", "all.zip");
            }

        }
    }
}
