using System.Web;
using System.Web.Mvc;

public static class HtmlHelperExtensions
{
    public static HtmlString Check(this HtmlHelper htmlHelper, double lower, double upper, double toCheck)
    {
        return toCheck > lower && toCheck <= upper ? new HtmlString(" checked=\"checked\"") : null;
    }
}