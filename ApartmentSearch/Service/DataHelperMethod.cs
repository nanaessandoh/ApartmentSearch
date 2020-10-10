using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentSearch.Service
{
    public class DataHelperMethod
    {
        public static string TimeAgo(DateTime dateTime)
        {

            // Subtract the time passed in DateTime from the current time
            TimeSpan ts = DateTime.Now.Subtract(dateTime);

            if (ts <= TimeSpan.FromSeconds(60))
            {
                return string.Format($"Posted {ts.Seconds} seconds ago");
            }
            else if (ts <= TimeSpan.FromMinutes(60))
            {
                return ts.Minutes > 1 ?
                    String.Format($"Posted about {ts.Minutes} minutes ago") : "Posted about a minute ago";
            }
            else if (ts <= TimeSpan.FromHours(24))
            {
                return ts.Hours > 1 ?
                    String.Format($"Posted about {ts.Hours} hours ago") : "Posted about an hour ago";
            }
            else if (ts <= TimeSpan.FromDays(30))
            {
                return ts.Days > 1 ? String.Format($"Posted about {ts.Days} days ago") : "Posted yesterday";
            }
            else if (ts <= TimeSpan.FromDays(365))
            {
                return ts.Days > 30 ? String.Format($"Posted about {ts.Days/30} months ago") : "Posted about a month ago";
            }
            else
            {
                return ts.Days > 365 ? String.Format($"Posted about {ts.Days/365} years ago") : "Posted about a year ago";
            }

        }
    }
}
