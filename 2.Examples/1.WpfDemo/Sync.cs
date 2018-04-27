using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _1.WpfDemo
{
    public partial class MainWindow
    {
        private void ExecuteSync_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();

            RunDownloadSync();

            stopWatch.Stop();

            ResultsWindow.Text += $"Общее время выполнения: {stopWatch.ElapsedMilliseconds}";
        }

        private void RunDownloadSync()
        {
            var urls = GetUrls();
            ResultsWindow.Text = string.Empty;

            foreach (var url in urls)
            {
                var webSiteDto = DownloadWebSite(url);
                AddWebSiteToResultsWindow(webSiteDto);
            }
        }
    }
}
