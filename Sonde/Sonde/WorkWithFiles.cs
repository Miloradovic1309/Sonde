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

        public List<int> takeHour(string current_directory, string date_string, string probe_txt)
        {
            List<int> take_hour = new List<int>();
            List<string> lines = new List<string>();
            string line;
            System.IO.StreamReader file =
                   new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + probe_txt);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                int index_h = lines[i].IndexOf(':');
                string hour_string = lines[i].Substring(0, index_h);
                take_hour.Add(int.Parse(hour_string));
            }
            return take_hour;
        }
        public List<int> takeMinutes(string current_directory, string date_string, string probe_txt)
        {
            List<int> take_minutes = new List<int>();
            List<string> lines = new List<string>();
            string line;
            System.IO.StreamReader file =
                   new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + probe_txt);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                int index_h = lines[i].IndexOf(':');
                string new_string = lines[i].Substring(index_h+1);
                int index_m = new_string.IndexOf(':');
                string minutes_string = new_string.Substring(0, index_m);
                take_minutes.Add(int.Parse(minutes_string));
            }
            return take_minutes;
        }
        public List<int> takeSeconds(string current_directory, string date_string, string probe_txt)
        {
            List<int> take_seconds = new List<int>();
            List<string> lines = new List<string>();
            string line;
            System.IO.StreamReader file =
                   new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + probe_txt);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                int index_h = lines[i].IndexOf(':');
                string new_string = lines[i].Substring(index_h + 1);
                int index_m = new_string.IndexOf(':');
                new_string = new_string.Substring(index_m + 1);
                int index_s = new_string.IndexOf('|');
                string seconds_string = new_string.Substring(0, index_s);
                take_seconds.Add(int.Parse(seconds_string));
            }
            return take_seconds;
        }
        public List<float> takeTemperature(string current_directory, string date_string, string probe_txt)
        {
            List<float> take_temperature = new List<float>();
            List<string> lines = new List<string>();
            string line;
            System.IO.StreamReader file =
                   new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + probe_txt);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                int index_b = lines[i].IndexOf('|');
                string new_string = lines[i].Substring(index_b + 1);
                int index_t = new_string.IndexOf('|');
                string temperature_string = new_string.Substring(0, index_t);
                take_temperature.Add(float.Parse(temperature_string));
            }
            return take_temperature;
        }
        public List<int> takeHumidity(string current_directory, string date_string, string probe_txt)
        {
            List<int> take_humidity = new List<int>();
            List<string> lines = new List<string>();
            string line;
            System.IO.StreamReader file =
                   new System.IO.StreamReader(current_directory + "\\database\\" + date_string + "\\" + probe_txt);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();

            for (int i = 0; i < lines.Count; i++)
            {
                int index_b = lines[i].IndexOf('|');
                string new_string = lines[i].Substring(index_b + 1);
                int index_t = new_string.IndexOf('|');
                string humidity_string = new_string.Substring(index_t + 1);
                take_humidity.Add(int.Parse(humidity_string));
            }
            return take_humidity;
        }
    }
}
