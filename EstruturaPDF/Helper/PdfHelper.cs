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
            html.Append($"<img style='align-items;vertical-align: bottom; 'src='data:image/svg+xml;base64,{radioButton}' alt='Check Box' /> Sim");
            html.Append($"<img style='align-items;vertical-align: bottom; ' src='data:image/svg+xml;base64,{radioButtonCheck}' alt='Check Box' /> Não");
        }
        else
        {
            html.Append($"<img style='align-items: center; vertical-align: bottom;' src='data:image/svg+xml;base64,{radioButtonCheck}' alt='Check Box' /> Sim");
            html.Append($"<img style='align-items: center; vertical-align: bottom;' src='data:image/svg+xml;base64,{radioButton}' alt='Check Box' /> Não");
        }

        return html.ToString();
    }
    public static string FormatarData(DateTime data)
    {
        return data.ToString("dd/MM/yyyy");
    }
    public static string PreencherCampoVazio(string? resposta)
    {
        if (resposta == null)
        {
            resposta = "**********************";
        }
        return resposta;

    }
    public static string RederRadioGruop(Dictionary<string, string> list, Dictionary<string, string> base64Svgs, string response)
    {
        var radioButton = base64Svgs.ContainsKey("radioButton") ? base64Svgs["radioButton"] : null;
        var radioButtonCheck = base64Svgs.ContainsKey("radioButtonCheck") ? base64Svgs["radioButtonCheck"] : null;

        var html = new StringBuilder();

        foreach (var item in list)
        {
            // Verificar se o item atual é o selecionado
            bool isChecked = item.Key == response;

            // Usar operador ternário para determinar qual ícone usar
            var icon = isChecked ? radioButtonCheck : radioButton;

            // Adicionar a linha HTML para cada botão de rádio
            html.Append($"<img style='align-items: center; vertical-align: bottom;' src='data:image/svg+xml;base64,{icon}' alt='Check Box' /> {item.Value}");
            html.Append($"<input type='radio' name='leito' value='{item.Key}' {(isChecked ? "checked" : "")} style='display:none;' />");
        }

        return html.ToString();

    }
    public static string RenderCheckBox(bool autoriza, Dictionary<string,string> base64Svgs){
        var checkbox = base64Svgs.ContainsKey("checkbox") ? base64Svgs["checkbox"] : null;
        var checkbocCheck = base64Svgs.ContainsKey("checkbocCheck") ? base64Svgs["checkbocCheck"] : null;
        
        var html = new StringBuilder();

        var icon = autoriza ? checkbocCheck : checkbox;

        html.Append($"<img src='data:image/svg+xml;base64,{icon}' alt='Check Box' /> Autorizo");

        return html.ToString();
    }
}
