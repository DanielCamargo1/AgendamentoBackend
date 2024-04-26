using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PDFGenerator _pdfGenerator;

        public PdfController(PDFGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPdf()
        {
            byte[] pdfBytes = await _pdfGenerator.GeneratePdfBytes();

            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return NotFound(); 
            }

            return File(pdfBytes, "application/pdf", "Relatorio.pdf");
        }
        [HttpGet("/RelatotioDoDia")]
        public async Task<IActionResult> DownloadPdfToday()
        {
            return Ok();
        }
    }
}

