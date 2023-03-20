using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CliGame1
{
    public partial class Form1 : Form
    {
        string hostUrl;
        public Form1()
        {
            InitializeComponent();
            hostUrl = ConfigurationManager.AppSettings.Get("url");
            LoadBut();
        }
        Button[] buttons = new Button[9];
        private void LoadBut()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i] = new Button();
                buttons[i].Tag= i;
                buttons[i].Font=new Font("Tahoma", 22);
                tableLayoutPanel1.Controls.Add(buttons[i]);
                buttons[i].Dock = DockStyle.Fill;
                buttons[i].Click += new EventHandler(button_Click);
            }
        }
        private async void button_Click(object sender, EventArgs e)
        {              
            Button b = (Button)sender;
            string url = $"{hostUrl}/Tictactoe/api/GetStep?idx={b.Tag}";
            var client = new HttpClient();
            await client.GetStringAsync(url);
            string val = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/GetValue");
            b.Text = val.Replace("\"", "");
            var content = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/TestWin");
            if (content.Contains("Winner"))
            {
                timer1.Stop();
                MessageBox.Show(content.Replace("\"", ""));
            }           
        }

        
        private async Task<string> SendReqAsync(string url)
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync(url);

            return content;  //.Replace("\"", "");
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            NewGame();
            tableLayoutPanel1.Visible = true;
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Text = "";
            }
            timer1.Start();
        }

        private async void NewGame()
        {
            var client = new HttpClient();
            var content = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/NewGame");

            //MessageBox.Show(content);
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var _idx = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/GetIdx");
                int idx = int.Parse(_idx);
                string val = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/GetValue");
                if (val != "null")
                    buttons[idx].Text = val.Replace("\"", "");
                var content = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/TestWin");
                if (content.Contains("Winner"))
                {
                    timer1.Stop();
                    MessageBox.Show(content.Replace("\"", ""));
                }
            }
            catch { }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var _idx = await client.GetStringAsync($"{hostUrl}/Tictactoe/api/GetMap");

           var map = JsonConvert.DeserializeObject<Mapmodel>(_idx);
            int[] idx = map.map;
            foreach(var i in idx)
            {
                MessageBox.Show($"{i}");
            }
        }       
    }

    class Mapmodel
    {
        public int[] map { get; set; }
    }
}
