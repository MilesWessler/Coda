using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coda.Models;

public static class HtmlHelperExtensions
{

    public static HtmlString Check(this HtmlHelper htmlHelper, double lower, double upper, double toCheck)
    {
        return toCheck > lower && toCheck <= upper ? new HtmlString(" checked=\"checked\"") : null;
    }

    public static IHtmlString DisplayFormattedData(this HtmlHelper htmlHelper, string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return MvcHtmlString.Empty;
        }

        var result = string.Join(
            "<br/>",
            data
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(htmlHelper.Encode)
        );
        return new HtmlString(result);
    }

    public static Song FindSong(int? id)
    {
        ApplicationDbContext db = new ApplicationDbContext();

        Song song = db.Songs.Find(id);
        return song;
    }
}
