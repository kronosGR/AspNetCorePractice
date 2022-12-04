using System.Collections.Generic;

namespace ConferenceApp.Models
{
    public static class Repository
    {
        private static List<WebinarInvites> respones = new List<WebinarInvites>();
        public static IEnumerable<WebinarInvites> Responses => respones;

        public static void AddResponse(WebinarInvites response)
        {
            respones.Add(response);
        }
    }
}
