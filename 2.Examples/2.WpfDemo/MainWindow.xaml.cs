using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2.WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int RequestsCount = 100;
        private const string Url = "http://localhost:56360/Demo/";

        private void SyncBtn_Click(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ProgressOnProgressChanged;

            RequestsParallel(false, progress);
        }

        private void AsyncBtn_Click(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<ProgressReportModel>();
            progress.ProgressChanged += ProgressOnProgressChanged;

            RequestsParallel(true, progress);
        }
        
        private async void RequestsParallel(bool async, IProgress<ProgressReportModel> progress)
        {
            var requests = new List<Task>();
            var results = new ConcurrentDictionary<int, long>();
            var progressModel = new ProgressReportModel { Async = async };

            var requestUrl = async ? $"{Url}GetAsync" : $"{Url}GetSync";
            
            for (int i = 0; i < RequestsCount; i++)
            {
                var i1 = i;
                requests.Add(Task.Run(() =>
                {
                    var request = WebRequest.Create(requestUrl);
                    var stopWatch = Stopwatch.StartNew();
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        reader.ReadToEnd();

                        stopWatch.Stop();
                        results.TryAdd(i1, stopWatch.ElapsedMilliseconds);
                        
                        progressModel.ChartResults = results;
                        progress.Report(progressModel);
                    }
                }));
            }
            
            await Task.WhenAll(requests);
        }

        private void ProgressOnProgressChanged(object sender, ProgressReportModel progressReportModel)
        {
            var lineNumber = progressReportModel.Async ? 1 : 0;

            var line = (LineSeries)mcChart.Series[lineNumber];
            line.ItemsSource = progressReportModel.ChartResults.ToArray();
        }
    }
}
