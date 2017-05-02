using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JSONBeautify.Form;
using Newtonsoft.Json;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            HotKeyManager.RegisterHotKey(Keys.B, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
        }

        void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                var text = Clipboard.GetText();

                try
                {
                    dynamic parsedJson = JsonConvert.DeserializeObject(text);
                    var beauty = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                    Clipboard.SetText(beauty);
                    ShowNotofication(true);
                }
                catch (Exception ex)
                {
                    ShowNotofication(false);
                }
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            // Quick and dirty to keep the main window invisible      
            base.SetVisibleCore(false);
        }

        private void ShowNotofication(bool sucess)
        {

            var notification = new NotifyIcon()
            {
                Visible = true,
                Icon = SystemIcons.Information,
                BalloonTipTitle = "Json Beautifier",
                BalloonTipText = sucess ? "JSON formatted" : "Could not format JSON"
            };

            notification.ShowBalloonTip(2);
            notification.Dispose();

            Hide();
        }
    }
}
