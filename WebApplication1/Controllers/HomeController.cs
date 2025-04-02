using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using Xunit;
using Syncfusion.Pdf.Parsing;
using Syncfusion.OCRProcessor;
namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    [Fact()]
    public void OCRPdf1()
    {
        try
        {
            byte[] bytes = System.IO.File.ReadAllBytes("page_rotation.pdf");
            Stream stream = new MemoryStream(bytes);
            PdfLoadedDocument document = new(stream);

            using (OCRProcessor processor = new())
            {
                processor.TessDataPath = Path.GetFullPath("runtimes/tessdata/");
                processor.Settings.Language = Languages.English;
                processor.PerformOCR(document, processor.TessDataPath, out OCRLayoutResult layoutResult);

                if (layoutResult.Pages[0].Lines[0].Text != null)
                {
                    Assert.True(true);
                }
                else
                {
                    Assert.True(false);
                }
            }
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
