using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

        private const int RequestsCount = 500;
        private const string Url = "http://localhost:56360/Demo/";

        private void SyncBtn_Click(object sender, RoutedEventArgs e)
        {
            Requests(false);
        }

        private void AsyncBtn_Click(object sender, RoutedEventArgs e)
        {
            Requests(true);
        }

        private async void Requests(bool async)
        {
            var requests = new List<Task>();
            var results = new ConcurrentDictionary<int, long>();

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
                    }
                }));
            }
            
            await Task.WhenAll(requests);

            var chartResults = new List<KeyValuePair<int, long>>();

            foreach (var kvp in results)
            {
                chartResults.Add(new KeyValuePair<int, long>(kvp.Key, kvp.Value));
            }

            var lineNumber = async ? 1 : 0;
            
            var line = (LineSeries)mcChart.Series[lineNumber];
            line.ItemsSource = chartResults.ToArray();
        }
        
        private void ClearChartBtn_Click(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 2; i++)
            {
                var line = (LineSeries)mcChart.Series[i];
                line.ItemsSource = Enumerable.Empty<KeyValuePair<int, long>>();
            }
        }
    }
}
