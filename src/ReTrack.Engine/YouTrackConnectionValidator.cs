using System;
using System.Net;
using System.Security.Authentication;

namespace ReTrack.Engine
{
    public class YouTrackConnectionValidator
    {
        public bool TryValidateConnection(ReTrackSettings reTrackSettings, out string errorMessage)
        {
            try
            {
                using (var proxy = new YouTrackProxy(reTrackSettings))
                {
                    errorMessage = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLowerInvariant() == "not found" || ex is WebException)
                {
                    errorMessage = "An error occurred while connecting to the remote server. Verify the URL is correct and try again.";
                    return false;
                }
                else if (ex is AuthenticationException)
                {
                    errorMessage = "An error occurred while authenticating with the remote server. Verify your credentials and try again.";
                    return false;
                }
                else
                {
                    errorMessage = "An unspecified error occurred while connecting to the remote server.";
                    return false;
                }
            }
        }
    }
}