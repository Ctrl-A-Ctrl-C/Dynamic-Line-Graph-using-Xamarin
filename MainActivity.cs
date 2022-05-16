using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.OS;
using Microcharts;
using Prism.Navigation;
using System.Text;
using static AccelerometerCollector.MainPage;
using AccelerometerCollector.Droid;
using SkiaSharp;
using System.Collections.Generic;
using Orientation = Microcharts.Orientation;

[assembly: Xamarin.Forms.Dependency(typeof(WirteFileService))]

namespace AccelerometerCollector.Droid
{
    [Activity(Label = "AccelerometerCollector", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private LineChart lineChart;
        public LineChart LineChart
        {
            get => lineChart;
            set => SetProperty(ref lineChart, value);
        }

        private void SetProperty(ref LineChart lineChart, LineChart value)
        {
            throw new NotImplementedException();
        }

        private string[] months = new string[] { "Jan", "Feb", "Mar" };

        private float[] turnoverData = new float[] { 100, 500, 100 };

        private SKColor blueColor = SKColor.Parse("#09C");
        
        private void InitData()
        {
            var turnoverEntries = new List<ChartEntry>();

            foreach (var data in turnoverData)
            {
                turnoverEntries.Add(new ChartEntry(data)
                {
                    Color = blueColor,
                    ValueLabel = $"{data/100} k",
                    Label = "TurnOver"
                });
            }

            LineChart = new LineChart { Entries = turnoverEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
        }

        readonly string[] Permission =
        {
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.AccessNetworkState
        };

        const int RequestID = 0;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Essentials.Platform.Init(this, bundle);
            RequestPermissions(Permission, RequestID);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    public class WirteFileService : IWirteService
    {


        void IWirteService.wirteFile(string FileName, string text)
        {
            //string DownloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            //string filePath = Path.Combine(DownloadsPath, FileName);

            string filePath = Application.Context.GetExternalFilesDir("").AbsolutePath + FileName;

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, text);
            }
            File.AppendAllText(filePath, text);
        }
    }
}

