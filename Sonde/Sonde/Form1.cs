using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Globalization;


namespace Sonde
{
    public partial class Sonde : Form
    {
        List<float> probe1_temperaturelist = new List<float>();
        List<float> probe2_temperaturelist = new List<float>();
        List<float> probe3_temperaturelist = new List<float>();
        List<float> probe4_temperaturelist = new List<float>();
        List<int> probe1_humiditylist = new List<int>();
        List<int> probe2_humiditylist = new List<int>();
        List<int> probe3_humiditylist = new List<int>();
        List<int> probe4_humiditylist = new List<int>();


        public Sonde()
        {
            InitializeComponent();
        }

        private void Sonde_Load(object sender, EventArgs e)
        {

        }
    }
}
