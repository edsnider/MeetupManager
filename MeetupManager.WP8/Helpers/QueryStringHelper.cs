using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeetupManager.WP8.Helpers
{
    public static class QueryStringHelper
    {
        public static Dictionary<string, string> ParseQueryString(string s)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();

            // only need what comes after the ? ... getting rid of everything else here
            if (s.Contains("?"))
                s = s.Remove(0, s.IndexOf("?") + 1);

            foreach (string kv in Regex.Split(s, "&"))
            {
                string[] pair = Regex.Split(kv, "=");

                if (pair.Length == 2)
                    query.Add(pair[0], pair[1]);
                else
                    query.Add(pair[0], string.Empty);
            }

            return query;
        }

        public static string BuildQueryString(Dictionary<string, string> d)
        {
            if (d == null)
                throw new ArgumentNullException("d");

            StringBuilder qs = new StringBuilder();

            foreach (var pair in d)
            {
                qs.Append("&");
                qs.Append(Uri.EscapeDataString(pair.Key));
                qs.Append("=");
                qs.Append(Uri.EscapeDataString(pair.Value));
            }
            qs.Remove(0, 1); // Remove the first &

            return qs.ToString();
        }
    }
}
