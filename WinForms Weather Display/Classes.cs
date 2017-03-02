using System;
using System.Collections.Generic;
using System.Text;

namespace WinForms_Weather_Display
{
    class Classes
    {
        /// <summary>
        /// The data for the current moment
        /// </summary>
        public class RightNow
        {
            public string summary;
            public string icon;
            public double temperature;
            public long time;
        }
        /// <summary>
        /// The summary for the week as well as collecting all of the
        /// data for each day for the next 5 days
        /// </summary>
        public class ByDay
        {
            public string summary;
            public DayData[] data = new DayData[4];
        }
        /// <summary>
        /// All of the data for that day
        /// </summary>
        public class DayData
        {
            public string summary;
            public string icon;
            public long time;
            public double temperatureMin;
            public double temperatureMax;
            public long sunriseTime;
            public long sunsetTime;
        }
        /// <summary>
        /// Converts a long to a date
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static DateTime FromUnix(long Time)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(Time);
        }
        /// <summary>
        /// All of the data collected together
        /// </summary>
        public class WeatherReport
        {
            public RightNow currently;
            public ByDay daily;
        }
        /// <summary>
        /// The GeoIp data to get the Lat and Long
        /// </summary>
        public class GeoIp
        {
            public string latitude;
            public string longitude;
        }
    }
}
