using System.Web.Mvc;

namespace MvcHtmlHelpers {
    public static class HtmlHelperExtensions {
        private const string Nbsp = "&nbsp;";
        private const string SelectedAttribute = " selected='selected'";

        public static MvcHtmlString NbspIfEmpty(this HtmlHelper helper, string value) {
            return new MvcHtmlString(string.IsNullOrEmpty(value) ? Nbsp : value);
        }

        public static MvcHtmlString SelectedIfMatch(this HtmlHelper helper, object expected, object actual) {
            return new MvcHtmlString(Equals(expected, actual) ? SelectedAttribute : string.Empty);
        }

        public static MvcHtmlString DivBegin(this HtmlHelper helper,int i) {
            if (i%3==0 || i==0)
                return new  MvcHtmlString("<div class=\"blog-small-home\">");
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString DivEnd(this HtmlHelper helper, int i) {
            if ((i+1)%3==0)
                return new MvcHtmlString("</div>");
            return MvcHtmlString.Empty;
        }
    }
}