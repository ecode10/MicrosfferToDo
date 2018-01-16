using System;
using System.Web;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;

namespace MicrosfferToDo.Library.Util
{
    /// <summary>
    /// Classe que converte HTML para Texto e vice versa
    /// <author>Mauricio Junior</author>
    /// </summary>
    public class HtmlToText
    {
        /// <summary>
        /// Método que pega string e transforma para HTML
        /// </summary>
        /// <param name="htmlText">string</param>
        /// <param name="decode">bool</param>
        /// <returns>string</returns>
        public static string StripHtml(string htmlText, bool decode = true)
        {
            if (htmlText == null) throw new ArgumentNullException(nameof(htmlText));

            Regex reg = new Regex("<[^>]+>", options: RegexOptions.IgnoreCase);
            var stripped = reg.Replace(htmlText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }

        /// <summary>
        /// Método que conver documentos
        /// </summary>
        /// <param name="path">string</param>
        /// <returns>string</returns>
        public static string Convert(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(path);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        /// <summary>
        /// Método que converte para html
        /// </summary>
        /// <param name="html">string</param>
        /// <returns>string</returns>
        public static string ConvertHtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        /// <summary>
        /// Método privado utilizado pelos outros métodos
        /// </summary>
        /// <param name="node"></param>
        /// <param name="outText"></param>
        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }

        /// <summary>
        /// Método que converte html para TextWriter
        /// </summary>
        /// <param name="node">HtmlNode</param>
        /// <param name="outText">TextWriter</param>
        public static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    //nao tem comentários
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script e estilo não podem sair
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // pega o texto
                    var html = ((HtmlTextNode)node).Text;

                    // especial que verifica se a saída é um texto
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // verifica o texto
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // trata parágrafos com ENTER ou CTRL
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }
    }
}
