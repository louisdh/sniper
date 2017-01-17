using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyDiving {

    public partial class SplashScreenForm : Form {

        private Timer timer;

        public SplashScreenForm() {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.BackColor = Color.DarkRed;
            this.TransparencyKey = Color.DarkRed;

            // http://stackoverflow.com/questions/648700/is-there-a-way-to-delay-an-event-handler-say-for-1-sec-in-windows-forms
            timer = new Timer();
            timer.Interval = 4000;
            timer.Tick += delegate {
                // This will be executed on a single (UI) thread, so lock is not necessary
                // but multiple ticks may have been queued, so check for enabled.
                if (timer.Enabled) {
                    CloseSplash();
                }
            };

            timer.Start();


        }

        private void CloseSplash() {

            timer.Stop();

            Form1 f = new Form1();
            this.Hide();

            f.ShowDialog();

            timer.Dispose();
            this.Close();
        }

        private void SplashScreenForm_Click(object sender, EventArgs e) {
            if (timer != null) {
                CloseSplash();
            }
        }
    }
}
