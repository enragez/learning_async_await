using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaitDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Waiting";
            DelayAsync().Wait();
            label1.Text = "Waiting completed";
        }

        private async Task DelayAsync()
        {
            await Task.Delay(2000);
        }
    }
}
