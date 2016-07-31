using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using LinqToTwitter;

/**
 * class: formtarget
 * semua tweet terbaru akan diekstrak dari Twitter
 * dan kemudian diproses, termasuk memasukannya ke dalam list dan mencari lokasi tertentu
 */

namespace TestASPNETv0._0
{
    public partial class formtarget : System.Web.UI.Page
    {

        /**
         * inisiasi autentikasi
         */
        private SingleUserAuthorizer authorizer =
           new SingleUserAuthorizer
           {
               CredentialStore = new
              SingleUserInMemoryCredentialStore
               {
                   ConsumerKey =
                 "WjWW72hfNjJuPGwSlv2dNFqQD",
                   ConsumerSecret =
                 "XkNKAb6Hg64bQGk0p5js4kfBgGYjZqLeTI5YlHB4MGZQRh6kzU",
                   AccessToken =
                 "2729380164-fQvHtakqqd8Uuwi5BFAGb7bjANsV6FZzMDHfkZg",
                   AccessTokenSecret =
                 "fDBcQDeiaBH1BVUVJ1OduRdsHTs4ozZMScOY8KYqgd0It"
               }
           };


        /*
         * atribut 
         */
        public static List<Status> currentTweets = new List<Status>();
        public static List<string> currentTweets_STRINGFORMATTED = new List<string>();
        public static List<string> arrLOCATION = new List<string>();
        public static List<EmbeddedStatus> arrTweetsHTML = new List<EmbeddedStatus>();
       
        public static string jenisTAMPILAN_TWEET;
        
        private string bahasaTWEET_ISO639_1;
        private TwitterContext GLOBAL_TWITTER_CONTEXT;

        /*
         * masuk ke prosedur ini setelah button ExtractTwitter diklik
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            // inisiasi opsi action
            namelab.Text = ((TextBox)Page.PreviousPage.FindControl("tipetweet")).Text;

            // inisiasi variabel jenis tampilan tweet
            RadioButtonList radAlgo1 = ((RadioButtonList)Page.PreviousPage.FindControl("optTampilanTweet"));
            LabelJenisTampilanTweet.Text = radAlgo1.SelectedValue.ToString();
            jenisTAMPILAN_TWEET = LabelJenisTampilanTweet.Text;

            // inisiasi variabel bahasa tweet
            RadioButtonList radAlgo2 = ((RadioButtonList)Page.PreviousPage.FindControl("optBahasaTweet"));
            LabelBahasaTweet.Text = radAlgo2.SelectedValue.ToString();
            if (radAlgo2.SelectedValue.ToString() == "Bahasa Indonesia")
            {
                bahasaTWEET_ISO639_1 = "id";
            }
            else if (radAlgo2.SelectedValue.ToString() == "Basa Sunda")
            {
                bahasaTWEET_ISO639_1 = "su";
            }
            else
            {
                bahasaTWEET_ISO639_1 = "en";
            }
           
            // inisiasi list
            currentTweets.Clear();
            arrLOCATION.Clear();
            arrTweetsHTML.Clear();
            currentTweets_STRINGFORMATTED.Clear();

            // mencari tweet dengan keyword tertentu
            // tweet ditampilkan dalam bentuk berdasarkan pilihan user
            if (jenisTAMPILAN_TWEET == "Plain text")
            {
                currentTweets = SearchTwitter(((TextBox)Page.PreviousPage.FindControl("tipetweet")).Text);
            }
            else {
                currentTweets = SearchTwitter(((TextBox)Page.PreviousPage.FindControl("tipetweet")).Text);
                SearchTwitterHTML(GLOBAL_TWITTER_CONTEXT);
            }

            // mencari lokasi tertentu
            //arrLOCATION = SearchLocation();

            int cellCtr;
            int tweetCount = 1;

            foreach (var tweet in currentTweets)
            {
                // Create new row and add it to the table.
                TableRow tRow = new TableRow();
                TweetTable.Rows.Add(tRow);
                for (cellCtr = 0; cellCtr < 2; cellCtr++)
                {
                    // Create a new cell and add it to the row.
                    TableCell tCell = new TableCell();
                    if (cellCtr == 0)
                    {
                        tCell.Text = tweetCount.ToString();
                        tweetCount++;
                    }
                    else if (cellCtr == 1)
                    {
                        tCell.Text = TextAsHtml(tweet);
                        currentTweets_STRINGFORMATTED.Add(tCell.Text);
                    }
                    
                    tRow.Cells.Add(tCell);
                }
            }

        }

        /**
         * METHODS
         */

        private Regex _parseUrls = new Regex("\\b(([\\w-]+://?|www[.])[^\\s()<>]+(?:\\([\\w\\d]+\\)|([^\\p{P}\\s]|/)))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private Regex _parseMentions = new Regex("(^|\\W)@([A-Za-z0-9_]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private Regex _parseHashtags = new Regex("[#]+[A-Za-z0-9-_]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public string TextAsHtml(Status status)
        {
            string tweetText = status.Text;

            if (!String.IsNullOrEmpty(tweetText))
            {
                // Replace URLs
                foreach (var urlMatch in _parseUrls.Matches(tweetText))
                {
                    Match match = (Match)urlMatch;
                    tweetText = tweetText.Replace(match.Value, String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", match.Value));
                }

                // Replace Mentions
                foreach (var mentionMatch in _parseMentions.Matches(tweetText))
                {
                    Match match = (Match)mentionMatch;
                    if (match.Groups.Count == 3)
                    {
                        string value = match.Groups[2].Value;
                        string text = "@" + value;
                        tweetText = tweetText.Replace(text, String.Format("<a href=\"http://twitter.com/{0}\" target=\"_blank\">{1}</a>", value, text));
                    }
                }

                // Replace Hash Tags
                foreach (var hashMatch in _parseHashtags.Matches(tweetText))
                {
                    Match match = (Match)hashMatch;
                    string query = Uri.EscapeDataString(match.Value);
                    tweetText = tweetText.Replace(match.Value, String.Format("<a href=\"http://search.twitter.com/search?q={0}\" target=\"_blank\">{1}</a>", query, match.Value));
                }
            }

            return tweetText;
        }


        protected string FormatTweet(Status status)
        {
            var entities = new List<EntityBase>();
            entities.AddRange(status.Entities.HashTagEntities);
            entities.AddRange(status.Entities.UrlEntities);
            entities.AddRange(status.Entities.UserMentionEntities);
            entities = entities.OrderByDescending(item => item.Start).ToList();
            var linkedText = status.Text;
            foreach (var entity in entities)
            {
                if (entity is HashTagEntity)
                {
                    var tagEntity = (HashTagEntity)entity;
                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/search?q=%23{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.Start),
                        tagEntity.Tag,
                        linkedText.Substring(entity.End));
                }
                else if (entity is UserMentionEntity)
                {
                    var mentionEntity = (UserMentionEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/{1}\">@{1}</a>{2}",
                        linkedText.Substring(0, entity.Start),
                        mentionEntity.ScreenName,
                        linkedText.Substring(entity.End));
                }
                else if (entity is UrlEntity)
                {
                    var urlEntity = (UrlEntity)entity;
                    linkedText = string.Format(
                        "{0}<a href=\"{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.Start),
                        urlEntity.Url,
                        linkedText.Substring(entity.End));
                }
            }
            return linkedText;
        }

        private List<Status> SearchTwitter(string searchTerm)
        {
            var twitterContext = new TwitterContext(authorizer);

            GLOBAL_TWITTER_CONTEXT = twitterContext;

            var srch = Enumerable.SingleOrDefault((from search in twitterContext.Search
                                           where search.Type == SearchType.Search &&
                                           search.Query == searchTerm &&
                                           search.Count == 100 &&
                                           search.SearchLanguage == bahasaTWEET_ISO639_1
                                           select search));

            if (srch != null && srch.Statuses.Count > 0)
            {
                return srch.Statuses.ToList();
            }

            return new List<Status>();      //list kosong jika tidak ada tweet yang mengandung keyword 
        }

        private void SearchTwitterHTML(TwitterContext twtCtxt)
        {
            //var twitterContext = new TwitterContext(authorizer);
            foreach (var tweet in currentTweets)
            {
                var embd =
                    (from twt in twtCtxt.Status
                    where twt.Type == StatusType.Oembed &&
                    twt.ID == tweet.StatusID 
                    select twt.EmbeddedStatus)
                        .SingleOrDefault();

                arrTweetsHTML.Add(embd);
            }
        }

        private List<string> SearchLocation()
        {
            List<string> tmpLOC = new List<string>();
            int idxDIkeyword = -1;

            // mencari keyword 'di' dalam setiap tweet
            foreach (var tweet in currentTweets)
            {
                /*
                 * mencari keyword 'di' yang sesuai berarti haruslah memiliki
                 * format sebagai berikut: <space>di<space><lokasi>
                 */
                
                idxDIkeyword = GET_IDX_DIkeyword(tweet.Text, " di ", StringComparison.OrdinalIgnoreCase);
            }

            return tmpLOC;
        }

        private int GET_IDX_DIkeyword(string source, string toCheck, StringComparison comp)
        {
            // method untuk mencari indeks pertama ditemukannya keyword ' di '
            return source.IndexOf(toCheck, comp);
        }
       
    }
}
