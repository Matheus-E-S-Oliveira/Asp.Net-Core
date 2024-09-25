using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures; // Para ViewDataDictionary e TempData
using Microsoft.AspNetCore.Mvc.Rendering; // Para ViewContext
using IronPdf;
using System.IO;
using EstruturaPDF.Helpers;

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

        ChromePdfRenderer renderer = new ChromePdfRenderer();
        //cabeçalho

        renderer.RenderingOptions.TextHeader.DrawDividerLine = true;
        renderer.RenderingOptions.TextHeader.CenterText = "Dani Nails";
        renderer.RenderingOptions.TextHeader.LeftText = "DN";
        renderer.RenderingOptions.TextHeader.Font = IronSoftware.Drawing.FontTypes.Courier;
        renderer.RenderingOptions.TextHeader.FontSize = 12;
        //rodape
        renderer.RenderingOptions.TextFooter.DrawDividerLine = true;
        renderer.RenderingOptions.TextHeader.Font = IronSoftware.Drawing.FontTypes.Arial;
        renderer.RenderingOptions.TextFooter.FontSize = 10;
        renderer.RenderingOptions.TextFooter.LeftText = "{date} {time}";
        renderer.RenderingOptions.TextFooter.CenterText = "Dani Nails";
        renderer.RenderingOptions.TextFooter.RightText = "{page} de {total-pages}";
        //espaçamento entre as bordas da pagina
        renderer.RenderingOptions.MarginTop = 10;
        renderer.RenderingOptions.MarginLeft = 5;
        renderer.RenderingOptions.MarginRight = 5;
        renderer.RenderingOptions.MarginBottom = 5;

        var pdfDocument = renderer.RenderHtmlAsPdf(htmlContent);

        return File(pdfDocument.Stream, "application/pdf", "Ficha Anamnese.pdf");
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
