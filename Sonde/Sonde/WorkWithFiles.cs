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
    class WorkWithFiles
    {
        public void writeFile(System.IO.StreamWriter file, string path, int hour, int minutes, int seconds,
            float probe_temperature, int probe_humidity)
        {
            using (file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(Convert.ToString(hour) + ":" + Convert.ToString(minutes) + ":"
                   + Convert.ToString(seconds) + "|" + Convert.ToString((float)probe_temperature) + "|" + 
                   Convert.ToString(probe_humidity));
            }
        }
    }
}
