using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Coda.Models;
using Coda.Objects;

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
    public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
    {
        return helper.ActionLink(post.Title, "Post", "Blog",
            new
            {
                year = post.PostedOn.Year,
                month = post.PostedOn.Month,
                title = post.UrlSlug
            },
            new
            {
                title = post.Title
            });
    }

    public static MvcHtmlString CategoryLink(this HtmlHelper helper,
        Category category)
    {
        return helper.ActionLink(category.Name, "Category", "Blog",
            new
            {
                category = category.UrlSlug
            },
            new
            {
                title = String.Format("See all posts in {0}", category.Name)
            });
    }

    public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
    {
        return helper.ActionLink(tag.Name, "Tag", "Blog", new { tag = tag.UrlSlug },
            new
            {
                title = String.Format("See all posts in {0}", tag.Name)
            });
    }
}
