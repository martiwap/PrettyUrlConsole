using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PrettyURLConsole
{
    public class ShortenService
    {
        public PrettyUrlsStored _storedUrls = new PrettyUrlsStored();

        public bool IsAliasValid(string alias)
        {
            if (string.IsNullOrEmpty(alias))
                return false;

            if (alias.Length < 2) // at least 3 digits 
                return false;

            if (IsAlreadyUsed(alias))
                return false;

            return true;
        }

        private bool IsAlreadyUsed(string alias)
        {
            var aliasStored = this._storedUrls.GetAliasStored();

            foreach (var aliasUsed in aliasStored)
            {
                if (aliasUsed == alias)
                    return true;
            }

            return false;
        }

        public PrettyUrlModel CreateNewPrettyURL(string longUrl, string alias)
        {
            if (string.IsNullOrEmpty(longUrl))
                throw new Exception("Long URL must be provided");

            if (!IsAliasValid(alias))
                throw new Exception("Alias is already in use, use a different one");

            var prettyThing = this._storedUrls.AddNewPrettyURL(longUrl, alias);

            return prettyThing;
        }

        public void GoToURL(string prettyUrl)
        {
            var url = this._storedUrls.GetLongUrl(prettyUrl);
            this.OpenBrowser(url);
        }

        // copied
        public void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
