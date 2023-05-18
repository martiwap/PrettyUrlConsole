using System.Net;

namespace PrettyURLConsole
{
    public class PrettyUrlsStored
    {
        // this would ideally come from a table or db where are stored
        public List<PrettyUrlModel> PrettyUrls { get; set; }

        public List<string> GetAliasStored()
        {
            if (this.PrettyUrls is null)
                this.PrettyUrls = new List<PrettyUrlModel>();

            return this.PrettyUrls.Select(x => x.Alias).ToList();
        }

        // and this would add the new item to the table
        public PrettyUrlModel AddNewPrettyURL(string longUrl, string alias)
        {
            var newPretty = new PrettyUrlModel
            {
                GuidId = Guid.NewGuid(),
                LogUrl = longUrl,
                Alias = alias,
                PrettyUrl = "https://prettyurl/" + alias,
            };

            this.PrettyUrls.Add(newPretty);

            return newPretty;
        }

        public string GetLongUrl(string prettyUrl)
        {
            return this.PrettyUrls
                .Where(x => x.PrettyUrl == prettyUrl)
                .Select(x => x.LogUrl)
                .FirstOrDefault();
        }

        public string Get(string prettyUrl)
        {
            var uri = this.GetLongUrl(prettyUrl);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}