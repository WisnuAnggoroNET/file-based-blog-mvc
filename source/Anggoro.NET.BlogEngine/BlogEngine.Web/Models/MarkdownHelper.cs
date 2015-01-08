using MarkdownSharp;
using System.Web;
using System.Web.Mvc;

namespace BlogEngine.Web.Models
{
    public static class MarkdownHelper
    {
        static readonly Markdown MarkdownTransformer = new Markdown();

        public static IHtmlString Markdown(this HtmlHelper helper, string text)
        {
            var html = MarkdownTransformer.Transform(text);
            return MvcHtmlString.Create(html);
        }
    }
}