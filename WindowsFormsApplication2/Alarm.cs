using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Alarm : Form
    {
        SoundPlayer player = new SoundPlayer();
        string message;
        

        public Alarm()
        {
            InitializeComponent();
        }
        public string passtext
        {

            get { return message; }
            set { message = value; }

        }
       
        private void Alarm_Load(object sender, EventArgs e)
        {
            label1.Text = message;
            Thread m = new Thread(openMuice);
            m.Start();
        }
        public void openMuice() {
            player.SoundLocation = "./sound/Windows Ding.wav";
            player.Load();
            //player.Play();
            player.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
