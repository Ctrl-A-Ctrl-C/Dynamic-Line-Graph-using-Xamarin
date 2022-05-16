using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;
using System.Timers;
using Entry = Microcharts.ChartEntry;
using SkiaSharp;
using Microcharts;
using Xamarin.Forms.PlatformConfiguration;
using System.Runtime.CompilerServices;

namespace AccelerometerCollector
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string axText = "AX: 0 (RED)";
        private float axValue;
        private string ayText = "AY: 0 (BLUE)";
        private float ayValue;
        private string azText = "AZ: 0 (GREEN)";
        private float azValue;
        private string gxText = "GX: 0 (RED)";
        private float gxValue;
        private string gyText = "GY: 0 (BLUE)";
        private float gyValue;
        private string gzText = "GZ: 0 (GREEN)";
        private float gzValue;
        private string entryValue = "Default";
        public int ACount = 0;
        public int AEntry = 0;
        public int GCount = 0;
        public int GEntry = 0;

        public string AxText
        {
            get => axText;

            set
            {
                if (value != axText)
                {
                    axText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string AyText
        {
            get => ayText;

            set
            {
                if (value != ayText)
                {
                    ayText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string AzText
        {
            get => azText;

            set
            {
                if (value != azText)
                {
                    azText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GxText
        {
            get => gxText;

            set
            {
                if (value != gxText)
                {
                    axText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GyText
        {
            get => gyText;

            set
            {
                if (value != gyText)
                {
                    ayText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GzText
        {
            get => gzText;

            set
            {
                if (value != gzText)
                {
                    azText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            entryValue = newText;
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")  
        {  
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        List<Entry> _entriesAX = new List<Entry> { };
        List<Entry> _entriesAY = new List<Entry> { };
        List<Entry> _entriesAZ = new List<Entry> { };
        List<Entry> _entriesGX = new List<Entry> { };
        List<Entry> _entriesGY = new List<Entry> { };
        List<Entry> _entriesGZ = new List<Entry> { };


        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs args)
        {
            axText = $"AX: {args.Reading.Acceleration.X.ToString("#.##")} (RED)";
            axValue = args.Reading.Acceleration.X;
            NotifyPropertyChanged("AxText");
            ayText = $"AY: {args.Reading.Acceleration.Y.ToString("#.##")} (BLUE)";
            ayValue = args.Reading.Acceleration.Y;
            NotifyPropertyChanged("AyText");
            azText= $"AZ: {args.Reading.Acceleration.Z.ToString("#.##")} (GREEN)";
            azValue = args.Reading.Acceleration.Z;
            NotifyPropertyChanged("AzText");
            if (ACount == 1)
            {
                AEntry++;
                if (_entriesAX.Count < 50)
                {
                    _entriesAX.Add(new Entry(axValue) { Label = $"{AEntry}", ValueLabel = $"{axValue}", Color = SKColor.Parse("#ff0000") });
                    _entriesAY.Add(new Entry(ayValue) { Label = $"{AEntry}", ValueLabel = $"{ayValue}", Color = SKColor.Parse("#0000ff") });
                    _entriesAZ.Add(new Entry(azValue) { Label = $"{AEntry}", ValueLabel = $"{azValue}", Color = SKColor.Parse("#00ff00") });
                }
                else if (_entriesAX.Count == 50)
                {
                    _entriesAX.RemoveAt(0);
                    _entriesAX.Add(new Entry(axValue) { Label = $"{AEntry}", ValueLabel = $"{axValue}", Color = SKColor.Parse("#ff0000") });
                    _entriesAY.RemoveAt(0);
                    _entriesAY.Add(new Entry(ayValue) { Label = $"{AEntry}", ValueLabel = $"{ayValue}", Color = SKColor.Parse("#0000ff") });
                    _entriesAZ.RemoveAt(0);
                    _entriesAZ.Add(new Entry(azValue) { Label = $"{AEntry}", ValueLabel = $"{azValue}", Color = SKColor.Parse("#00ff00") });

                }
                ChartViewAX.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesAX,
                    BackgroundColor = SKColor.Parse("#ffffff")
                };
                ChartViewAY.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesAY,
                    BackgroundColor = SKColors.Transparent
                };
                ChartViewAZ.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesAZ,
                    BackgroundColor = SKColors.Transparent
                };
                ACount = 0;
            }
            ACount++;
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs args)
        {
            gxText = $"GX: {args.Reading.AngularVelocity.X.ToString("#.##")} (RED)";
            gxValue = args.Reading.AngularVelocity.X;
            NotifyPropertyChanged("GxText");
            gyText = $"GY: {args.Reading.AngularVelocity.Y.ToString("#.##")} (BLUE)";
            gyValue = args.Reading.AngularVelocity.Y;
            NotifyPropertyChanged("GyText");
            gzText = $"GZ: {args.Reading.AngularVelocity.Z.ToString("#.##")} (GREEN)";
            gzValue = args.Reading.AngularVelocity.Z;
            NotifyPropertyChanged("GzText");
            DependencyService.Get<IWirteService>().wirteFile($"{entryValue}.csv", $"\r\n{axValue.ToString("#.##")}, {ayValue.ToString("#.##")}, {azValue.ToString("#.##")}, {gxValue.ToString("#.##")}, {gyValue.ToString("#.##")}, {gzValue.ToString("#.##")}");
            if (GCount == 1)
            {
                GEntry++;
                if (_entriesGX.Count < 50)
                {
                    _entriesGX.Add(new Entry(gxValue) { Label = $"{GEntry}", ValueLabel = $"{gxValue}", Color = SKColor.Parse("#ff0000") });
                    _entriesGY.Add(new Entry(gyValue) { Label = $"{GEntry}", ValueLabel = $"{gyValue}", Color = SKColor.Parse("#0000ff") });
                    _entriesGZ.Add(new Entry(gzValue) { Label = $"{GEntry}", ValueLabel = $"{gzValue}", Color = SKColor.Parse("#00ff00") });
                }
                else if (_entriesGX.Count == 50)
                {
                    _entriesGX.RemoveAt(0);
                    _entriesGX.Add(new Entry(gxValue) { Label = $"{GEntry}", ValueLabel = $"{gxValue}", Color = SKColor.Parse("#ff0000") });
                    _entriesGY.RemoveAt(0);
                    _entriesGY.Add(new Entry(gyValue) { Label = $"{GEntry}", ValueLabel = $"{gyValue}", Color = SKColor.Parse("#0000ff") });
                    _entriesGZ.RemoveAt(0);
                    _entriesGZ.Add(new Entry(gzValue) { Label = $"{GEntry}", ValueLabel = $"{gzValue}", Color = SKColor.Parse("#00ff00") });

                }
                ChartViewGX.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesGX,
                    BackgroundColor = SKColor.Parse("#ffffff")
                };
                ChartViewGY.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesGY,
                    BackgroundColor = SKColors.Transparent
                };
                ChartViewGZ.Chart = new LineChart()
                {
                    LabelTextSize = 18,
                    Margin = 10,
                    LineMode = LineMode.Spline,
                    AnimationDuration = new TimeSpan(0),
                    LineSize = 2,
                    MinValue = -1,
                    MaxValue = 1,
                    Entries = _entriesGZ,
                    BackgroundColor = SKColors.Transparent
                };
                GCount = 0;
            }
            GCount++;
        }

        public MainPage()
        {
            InitializeComponent();

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;

            DependencyService.Get<IWirteService>().wirteFile($"{entryValue}.csv", $"{AxResults.Text}, {AyResults.Text}, {AzResults.Text}, {GxResults.Text}, {GyResults.Text}, {GzResults.Text}");

            BindingContext = this;

            ChartViewAX.Chart = new LineChart()
            {
                LabelTextSize = 18,
                Margin = 10,
                LineMode = LineMode.Spline,
                AnimationDuration = new TimeSpan(0),
                LineSize = 2,
                Entries = _entriesAX,
                BackgroundColor = SKColor.Parse("#ffffff")
            };

            ChartViewGX.Chart = new LineChart()
            {
                LabelTextSize = 18,
                Margin = 10,
                LineMode = LineMode.Spline,
                AnimationDuration = new TimeSpan(0),
                LineSize = 2,
                Entries = _entriesGX,
                BackgroundColor = SKColor.Parse("#ffffff")
            };

        }
        
        void Sensor_Clicked(System.Object sender, System.EventArgs e)
        {
            // checks if accelerometer is already turned on when button is pushed
            if (Accelerometer.IsMonitoring)
            {
                // turn off if already on
                Accelerometer.Stop();
            }
            else
            {
                // turn on if accelerometer is off
                // starts accelerometer on UI thread.
                Accelerometer.Start(SensorSpeed.UI);
            }
            
            if (Gyroscope.IsMonitoring)
            {
                // turn off if already on
                Gyroscope.Stop();
            }
            else
            {
                // turn on if accelerometer is off
                // starts accelerometer on UI thread.
                Gyroscope.Start(SensorSpeed.UI);
            }
        }

        public interface IWirteService
        {
            void wirteFile(string FileName, string text);
        }
    }
}
