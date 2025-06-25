using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StefanoMulas_Progetto_scacchi
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 250;
            if (progressBar1.Value >= 1000)
            {
                timer1.Stop();
                timer1.Enabled = false;
            }
        }

       
    }
}
