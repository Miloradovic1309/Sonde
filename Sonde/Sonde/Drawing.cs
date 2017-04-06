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
    class Drawing
    {
        public void drawCoordiantes(Graphics g, float panel_height, float panel_width,
            int separate_width, int separate_height)
        {
            // Y ordinate
            float distanceY = (float)panel_height / (float)separate_height;
            for (int i = 0; i < separate_height; i++)
            {
                g.DrawLine(Pens.Black, (float)0, (float)((float)panel_height - (float)i * (float)distanceY), (float)panel_width, (float)((float)panel_height - (float)i * (float)distanceY));
            }


            // X acis
            float distanceX = (float)panel_width / (float)separate_width;
            for (int i = 0; i < separate_width; i++)
            {
                g.DrawLine(Pens.Black, (float)((float)i * (float)distanceX), (float)((float)panel_height), (float)((float)i * (float)distanceX), (float)0);
            }
        }

        public void drawNumbersY(Graphics g, float panel_height, int separate_height)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceX = (float)panel_height / (float)separate_height;

            for (int i = 1; i < separate_height; i++)
            {
                g.DrawString(Convert.ToString(i * 10), font, s, new PointF(0, (float)((float)panel_height - (float)i * distanceX - 5)));
            }
        }

        public void drawNumbersX(Graphics g, float panel_width, int separate_width)
        {
            SolidBrush s = new SolidBrush(Color.Black);
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font = new System.Drawing.Font(ff, 8);

            float distanceY = (float)panel_width / (float)separate_width;

            for (int i = 1; i < separate_width; i++)
            {
                g.DrawString(Convert.ToString(i), font, s, new PointF((float)((float)i * distanceY - 5), 0));
            }
        }

        public void drawingGraphsTemperature(List<float> probe_temperaturelist, List<int> probe_hourlist, List<int> probe_minuteslist,
            List<int> probe_secundeslist, Pen p, Graphics g, float panel_height, float panel_width,
            int separate_width, int separate_height)
        {
            float distanceY = (float)((float)panel_height / (float)separate_height);
            float distanceX = (float)((float)panel_width / (float)separate_width);
            float point1Y = (float)((float)((float)probe_temperaturelist[probe_temperaturelist.Count-2] * (float)distanceY) / (float)separate_height);
            float point2Y = (float)((float)((float)probe_temperaturelist[probe_temperaturelist.Count-1] * (float)distanceY) / (float)10);
            float time1_proportion = (float)probe_hourlist[probe_hourlist.Count - 2] + (float)((float)((float)((float)(60 * probe_minuteslist[probe_minuteslist.Count - 2] + probe_secundeslist[probe_secundeslist.Count - 2])*(float)100)/(float)3600)/(float)100);
            float time2_proportion = (float)probe_hourlist[probe_hourlist.Count - 1] + (float)((float)((float)((float)(60 * probe_minuteslist[probe_minuteslist.Count - 1] + probe_secundeslist[probe_secundeslist.Count - 1]) * (float)100) / (float)3600)/(float)100);
            float point1X = (float)(time1_proportion * distanceX);
            float point2X = (float)(time2_proportion * distanceX);

            g.DrawLine(p, point1X, (float)panel_height - point1Y, point2X, (float)panel_height - point2Y);
        }

        public void drawingGraphsHumudity(List<int> probe_humiditylist, List<int> probe_hourlist, List<int> probe_minuteslist,
            List<int> probe_secundeslist, Pen p, Graphics g, float panel_height, float panel_width,
            int separate_width, int separate_height)
        {
            float distanceY = (float)((float)panel_height / (float)separate_height);
            float distanceX = (float)((float)panel_width / (float)separate_width);
            float point1Y = (float)((float)((float)probe_humiditylist[probe_humiditylist.Count - 2] * (float)distanceY) / (float)separate_height);
            float point2Y = (float)((float)((float)probe_humiditylist[probe_humiditylist.Count - 1] * (float)distanceY) / (float)10);
            float time1_proportion = (float)probe_hourlist[probe_hourlist.Count - 2] + (float)((float)((100 * probe_minuteslist[probe_minuteslist.Count - 2]) / 60) / 100);
            float time2_proportion = (float)probe_hourlist[probe_hourlist.Count - 1] + (float)((float)((100 * probe_minuteslist[probe_minuteslist.Count - 1]) / 60) / 100);
            float point1X = (float)(time1_proportion * distanceX);
            float point2X = (float)(time2_proportion * distanceX);

            g.DrawLine(p, point1X, (float)panel_height - point1Y, point2X, (float)panel_height - point2Y);

        }
    }
}
