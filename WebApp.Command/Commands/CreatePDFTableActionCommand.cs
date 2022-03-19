using Microsoft.AspNetCore.Mvc;

namespace WebApp.Command.Commands
{
    public class CreatePDFTableActionCommand<T> : ITableActionCommand
    {
        private readonly PdfFile<T> _pdfFile;

        public CreatePDFTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            var pdfMemoryStream = _pdfFile.Create();
            return new FileContentResult(pdfMemoryStream.ToArray(), _pdfFile.FileType) { FileDownloadName=_pdfFile.FileName };
        }
    }
}
