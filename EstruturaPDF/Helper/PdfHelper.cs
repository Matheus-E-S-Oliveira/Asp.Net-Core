using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EstruturaPDF.Helpers;
public static class PdfHelper
{
    public static Dictionary<string, string> ConvertAllSvgsToBase64()
    {
        var base64Svgs = new Dictionary<string, string>();
        var svgDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img");

        if (!Directory.Exists(svgDirectoryPath))
            throw new DirectoryNotFoundException($"O diretório '{svgDirectoryPath}' não foi encontrado.");

        var svgFiles = Directory.GetFiles(svgDirectoryPath, "*.svg");

        foreach (var svgFile in svgFiles)
        {
            var svgContent = File.ReadAllText(svgFile);
            var base64Svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svgContent));
            var fileName = Path.GetFileNameWithoutExtension(svgFile);
            base64Svgs[fileName] = base64Svg;
        }

        return base64Svgs;
    }
    public static string RenderRadioButtonIcons(bool verifica, Dictionary<string, string> base64Svgs)
    {
        var radioButton = base64Svgs.ContainsKey("radioButton") ? base64Svgs["radioButton"] : null;
        var radioButtonCheck = base64Svgs.ContainsKey("radioButtonCheck") ? base64Svgs["radioButtonCheck"] : null;

        var html = new StringBuilder();

        if (!verifica)
        {
            html.Append($"<img src='data:image/svg+xml;base64,{radioButton}' alt='Check Box' /> Sim");
            html.Append($"<img src='data:image/svg+xml;base64,{radioButtonCheck}' alt='Check Box' /> Não");
        }
        else
        {
            html.Append($"<img src='data:image/svg+xml;base64,{radioButtonCheck}' alt='Check Box' /> Sim");
            html.Append($"<img src='data:image/svg+xml;base64,{radioButton}' alt='Check Box' /> Não");
        }

        return html.ToString();
    }
    public static string FormatarData(DateTime data)
    {
        return data.ToString("dd/MM/yyyy");
    }
    public static string PreencherCampoVazio (string resposta)
    {
        if(resposta == null){
            resposta = "*****************";
        }
        return resposta;

    }
}
