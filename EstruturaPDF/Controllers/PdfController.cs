using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures; // Para ViewDataDictionary e TempData
using Microsoft.AspNetCore.Mvc.Rendering; // Para ViewContext
using IronPdf;
using System.IO;

public class PdfController : Controller
{
    private readonly ICompositeViewEngine _viewEngine;

    public PdfController(ICompositeViewEngine viewEngine)
    {
        _viewEngine = viewEngine;
    }

    public IActionResult YourViewName()
    {
        return View(); // Certifique-se de que existe uma view correspondente
    }

    public IActionResult GeneratePdf()
    {
        var htmlContent = RenderViewToString("YourViewName", null);// Ajuste conforme necessário

        var renderer = new ChromePdfRenderer();
        var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

        return File(pdfDocument.Stream, "application/pdf", "output.pdf");
    }

    private string RenderViewToString(string viewName, object? model) // Permite null
    {
        ViewData.Model = model;
        using (var writer = new StringWriter())
        {
            var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
            if (viewResult.View == null)
            {
                throw new FileNotFoundException("View not found.");
            }

            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                TempData,
                writer,
                new HtmlHelperOptions() // Se necessário, remova essa linha
            );
            viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();
            return writer.ToString();
        }
    }
}
