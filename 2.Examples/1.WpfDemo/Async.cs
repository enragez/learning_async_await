using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _1.WpfDemo
{
    public partial class MainWindow
    {
        private async void ExecuteAsync_Click(object sender, RoutedEventArgs e)
        {
            var stopWatch = Stopwatch.StartNew();

            await RunDownloadAsync();

            stopWatch.Stop();

            ResultsWindow.Text += $"Общее время выполнения: {stopWatch.ElapsedMilliseconds}";
        }

        private async Task RunDownloadAsync()
        {
            var urls = GetUrls();
            ResultsWindow.Text = string.Empty;

            foreach (var url in urls)
            {
                var webSiteDto = await Task.Run(() => DownloadWebSite(url));
                AddWebSiteToResultsWindow(webSiteDto);
            }
        }

        #region Вариант получше
        private async Task<WebSiteDto> DownloadWebSiteAsync(string url)
        {
            using (var webClient = new WebClient())
            {
                return new WebSiteDto
                {
                    Url = url,
                    Data = await webClient.DownloadStringTaskAsync(url)
                };
            }
        }
        #endregion
    }
}
