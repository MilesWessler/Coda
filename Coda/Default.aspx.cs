using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using Coda.Models;

namespace Coda
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateArticle();
            }
        }
        private void PopulateArticle()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                
                var v = (from a in db.Songs
                         join b in db.SongScores on a.Id equals b.SongId into bb
                         from b in bb.DefaultIfEmpty()
                         group new { a, b } by new { a.Id, a.Name } into AA
                         select new
                         {
                             AA.Key.Id,
                             AA.Key.Name,
                             Score = AA.Sum(a => a.b.Score) == null ? 0 : AA.Sum(a => a.b.Score),
                             Count = AA.Count()
                         });
                List<SongWithScore> SWC = new List<SongWithScore>();
                foreach (var i in v)
                {
                    SWC.Add(new SongWithScore
                    {
                        SongId = i.Id,
                        Title = i.Name,
                        Score = i.Score / i.Count
                    });
                    GridView1.DataSource = SWC;
                    GridView1.DataBind();
                }
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static int SaveRating(int songId, int rate)
        {
            int result = 0;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                dc.SongScores.Add(new SongScore
                {
                   
                    Id = songId,
                    SongId = 0,
                    Score = rate,
                   
                });
                dc.SaveChanges();

                var newScore = (from a in dc.SongScores
                                where a.SongId.Equals(songId)
                                group a by a.SongId into aa
                                select new
                                {
                                    Score = aa.Sum(a => a.Score) / aa.Count()
                                }).FirstOrDefault();
                result = newScore.Score;
            }
            return result;
        }

    }
}