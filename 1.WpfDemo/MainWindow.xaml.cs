using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1.WpfDemo
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
        
        private List<string> GetUrls()
        {
            return new List<string>
            {
                "https://www.yahoo.com",
                "https://www.google.com",
                "https://www.microsoft.com",
                "https://www.yandex.ru",
                "https://www.mail.ru",
                "https://www.stackoverflow.com"
            };
        }

        private WebSiteDto DownloadWebSite(string url)
        {
            using (var webClient = new WebClient())
            {
                return new WebSiteDto
                {
                    Url = url,
                    Data = webClient.DownloadString(url)
                };
            }
        }

        private void AddWebSiteToResultsWindow(WebSiteDto dto)
        {
            ResultsWindow.Text += $"{dto.Url} загружен: {dto.Data.Length} количество символов. {Environment.NewLine}";
        }
    }
}
