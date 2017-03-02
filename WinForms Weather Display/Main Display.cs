using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Weather_Display
{
    public partial class Main_Display : Form
    {
        public List<string> ConfigList;
        public Main_Display()
        {
            InitializeComponent();
            // Pulls the command line arguments into a string array
            string[] args = Environment.GetCommandLineArgs();
            // The first argument is the file path so it checks for extra arguments
            if (args.Length >= 2)
            {
                // If the first added argument is anything like -f, /fullscreen or +nuhbtyihyihf, it will load the program fullscreen
                // This is designed for smaller displays such as the Raspberry Pi screen, 1080p screens look really bad with this
                if (args[1].Contains("f"))
                {
                    // Hides any windows underneath (If stuck, press Alt+F4 to close the program)
                    TopMost = true;
                    // Removes the border around the window
                    FormBorderStyle = FormBorderStyle.None;
                    // Maximizes the window
                    WindowState = FormWindowState.Maximized;
                }
            }
            // Reads the config file (Reads from where the program was loaded from, double clicking makes it the same folder as the program)
            string[] Config = File.ReadAllLines("DisplayConfig.cfg");
            // Checks to see if the Lat and Long are in the config file, if not, it will use geoip to find a lat and long
            // I recommend finding your lat and long using a service like Google Maps
            if (Config.Length < 4)
            {
                // These 4 lines use a Geoip service to get a lat and long to use, adding them to the list
                // If you have 3 lines for some reason, the third line will be used so use 2 or 4 lines
                ConfigList = new List<string>() { Config[0], Config[1] };
                Classes.GeoIp IpData = JsonConvert.DeserializeObject<Classes.GeoIp>(new WebClient().DownloadString("http://freegeoip.net/json/"));
                ConfigList.Add(IpData.latitude);
                ConfigList.Add(IpData.longitude);
            }
            else
            {
                // This is used when all 4 values are in the config
                ConfigList = new List<string>() { Config[0], Config[1], Config[2], Config[3] };
            }
            Loop();
        }

        public async void Loop()
        {
            while (true)
            {
                // This loops every 2 minutes, the await works, don't ask me why
                Data();
                await Task.Delay(120000);
            }
        }

        public void Data()
        {
            // Pulls the values from the api and sets the values in the display
            // The try/carch is in case there is an issue with downloading the json
            Classes.WeatherReport Result;
            try
            {
                Result = JsonConvert.DeserializeObject<Classes.WeatherReport>(new WebClient().DownloadString("https://api.darksky.net/forecast/" + ConfigList[0] + "/" + ConfigList[2] + "," + ConfigList[3] + "?units=" + ConfigList[1]));
            }
            catch (Exception)
            {
                return;
            }
            NowTime.Text = Classes.FromUnix(Result.currently.time).ToString("ddd hh:mm");
            NowIcon.ImageLocation = "Icons/" + Result.currently.icon + ".png";
            NowIconLabel.Text = Result.currently.icon;
            NowTemp.Text = Result.currently.temperature.ToString("0.0") + ((ConfigList[1] == "si") ? "°C" : "°F");
            Summery.Text = "Summary: " + Result.daily.summary;

            #region Day0
            Day0.Text = Classes.FromUnix(Result.daily.data[0].time).ToString("ddd");
            Icon0.ImageLocation = "Icons/" + Result.daily.data[0].icon + ".png";
            Max0.Text = "Max: " + Environment.NewLine + Result.daily.data[0].temperatureMax.ToString("0");
            Min0.Text = "Min: " + Environment.NewLine + Result.daily.data[0].temperatureMin.ToString("0");
            Rise0.Text = Classes.FromUnix(Result.daily.data[0].sunriseTime).ToString("hh:mm");
            Set0.Text = Classes.FromUnix(Result.daily.data[0].sunsetTime).ToString("hh:mm");
            #endregion
            #region Day1
            Day1.Text = Classes.FromUnix(Result.daily.data[1].time).ToString("ddd");
            Icon1.ImageLocation = "Icons/" + Result.daily.data[1].icon + ".png";
            Max1.Text = "Max: " + Environment.NewLine + Result.daily.data[1].temperatureMax.ToString("0");
            Min1.Text = "Min: " + Environment.NewLine + Result.daily.data[1].temperatureMin.ToString("0");
            Rise1.Text = Classes.FromUnix(Result.daily.data[1].sunriseTime).ToString("hh:mm");
            Set1.Text = Classes.FromUnix(Result.daily.data[1].sunsetTime).ToString("hh:mm");
            #endregion
            #region Day2
            Day2.Text = Classes.FromUnix(Result.daily.data[2].time).ToString("ddd");
            Icon2.ImageLocation = "Icons/" + Result.daily.data[2].icon + ".png";
            Max2.Text = "Max: " + Environment.NewLine + Result.daily.data[2].temperatureMax.ToString("0");
            Min2.Text = "Min: " + Environment.NewLine + Result.daily.data[2].temperatureMin.ToString("0");
            Rise2.Text = Classes.FromUnix(Result.daily.data[2].sunriseTime).ToString("hh:mm");
            Set2.Text = Classes.FromUnix(Result.daily.data[2].sunsetTime).ToString("hh:mm");
            #endregion
            #region Day3
            Day3.Text = Classes.FromUnix(Result.daily.data[3].time).ToString("ddd");
            Icon3.ImageLocation = "Icons/" + Result.daily.data[3].icon + ".png";
            Max3.Text = "Max: " + Environment.NewLine + Result.daily.data[3].temperatureMax.ToString("0");
            Min3.Text = "Min: " + Environment.NewLine + Result.daily.data[3].temperatureMin.ToString("0");
            Rise3.Text = Classes.FromUnix(Result.daily.data[3].sunriseTime).ToString("hh:mm");
            Set3.Text = Classes.FromUnix(Result.daily.data[3].sunsetTime).ToString("hh:mm");
            #endregion
            #region Day4
            Day4.Text = Classes.FromUnix(Result.daily.data[4].time).ToString("ddd");
            Icon4.ImageLocation = "Icons/" + Result.daily.data[4].icon + ".png";
            Max4.Text = "Max: " + Environment.NewLine + Result.daily.data[4].temperatureMax.ToString("0");
            Min4.Text = "Min: " + Environment.NewLine + Result.daily.data[4].temperatureMin.ToString("0");
            Rise4.Text = Classes.FromUnix(Result.daily.data[4].sunriseTime).ToString("hh:mm");
            Set4.Text = Classes.FromUnix(Result.daily.data[4].sunsetTime).ToString("hh:mm");
            #endregion
        }


        private void DarkSkyLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // When the hyperlink is clicked, it loads the page linked here
            Process.Start(new ProcessStartInfo("https://darksky.net/poweredby/"));
        }
    }
}
