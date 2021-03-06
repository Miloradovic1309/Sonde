﻿using System;
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
        SerialPort comPort = new SerialPort();
        DateTime dt = DateTime.Now;
        Drawing draw = new Drawing();
        WorkWithFiles filework = new WorkWithFiles();

        byte ADR1 = 10;
        byte ADR2 = 20;
        byte ADR3 = 30;
        byte ADR4 = 40;
        int number_of_ports = 100;

        List<float> probe1_temperaturelist = new List<float>();
        List<float> probe2_temperaturelist = new List<float>();
        List<float> probe3_temperaturelist = new List<float>();
        List<float> probe4_temperaturelist = new List<float>();
        List<int> probe1_humiditylist = new List<int>();
        List<int> probe2_humiditylist = new List<int>();
        List<int> probe3_humiditylist = new List<int>();
        List<int> probe4_humiditylist = new List<int>();

        List<int> probe1_hourlist = new List<int>();
        List<int> probe2_hourlist = new List<int>();
        List<int> probe3_hourlist = new List<int>();
        List<int> probe4_hourlist = new List<int>();
        List<int> probe1_minuteslist = new List<int>();
        List<int> probe2_minuteslist = new List<int>();
        List<int> probe3_minuteslist = new List<int>();
        List<int> probe4_minuteslist = new List<int>();
        List<int> probe1_secondslist = new List<int>();
        List<int> probe2_secondslist = new List<int>();
        List<int> probe3_secondslist = new List<int>();
        List<int> probe4_secondslist = new List<int>();

        List<float> probe1_temperaturelist_saved = new List<float>();
        List<float> probe2_temperaturelist_saved = new List<float>();
        List<float> probe3_temperaturelist_saved = new List<float>();
        List<float> probe4_temperaturelist_saved = new List<float>();
        List<int> probe1_humiditylist_saved = new List<int>();
        List<int> probe2_humiditylist_saved = new List<int>();
        List<int> probe3_humiditylist_saved = new List<int>();
        List<int> probe4_humiditylist_saved = new List<int>();

        List<int> probe1_hourlist_saved = new List<int>();
        List<int> probe2_hourlist_saved = new List<int>();
        List<int> probe3_hourlist_saved = new List<int>();
        List<int> probe4_hourlist_saved = new List<int>();
        List<int> probe1_minuteslist_saved = new List<int>();
        List<int> probe2_minuteslist_saved = new List<int>();
        List<int> probe3_minuteslist_saved = new List<int>();
        List<int> probe4_minuteslist_saved = new List<int>();
        List<int> probe1_secondslist_saved = new List<int>();
        List<int> probe2_secondslist_saved = new List<int>();
        List<int> probe3_secondslist_saved = new List<int>();
        List<int> probe4_secondslist_saved = new List<int>();

        string message_port = "Došlo je do greške.";

        float probe1_temperature;
        float probe2_temperature;
        float probe3_temperature;
        float probe4_temperature;
        int probe1_humidity;
        int probe2_humidity;
        int probe3_humidity;
        int probe4_humidity;

        int hour;
        int minutes;
        int seconds;

        int measure;
        int measure_probe;
        int measure_counter;
        int probe1_measure;
        int probe2_measure;
        int probe3_measure;
        int probe4_measure;
        int just_measured;

        int realTimeDrawGraph;
        int add_values_to_list;

        int separate_width;
        int separate_height;
        int separate_width_s;
        int separate_height_s;
        int time_from;
        int time_to;

        System.IO.StreamWriter fileW;
        string folder;
        string path1;
        string path2;
        string path3;
        string path4;
        string now_or_previous_day;
        string current_directory = Directory.GetCurrentDirectory();
        string[] probe_txt = {"sonda1.txt", "sonda2.txt", "sonda3.txt", "sonda4.txt" };
        string[] probe_chose = { "Sonda 1", "Sonda 2", "Sonda 3", "Sonda 4" };


        public void drawCoordinateSystem()
        {
            Graphics g1 = panel1.CreateGraphics();
            Graphics g2 = panel2.CreateGraphics();
            Graphics g3 = panel3.CreateGraphics();
            Graphics g4 = panel4.CreateGraphics();
            Graphics g5 = panel5.CreateGraphics();
            Graphics g6 = panel6.CreateGraphics();
            Graphics g7 = panel7.CreateGraphics();
            Graphics g8 = panel8.CreateGraphics();
            Graphics g9 = panel9.CreateGraphics();
            Graphics g10 = panel10.CreateGraphics();
            Graphics g11 = panel11.CreateGraphics();
            Graphics g12 = panel12.CreateGraphics();
            g1.Clear(Color.FloralWhite);
            g2.Clear(Color.FloralWhite);
            g3.Clear(Color.FloralWhite);
            g4.Clear(Color.FloralWhite);

            draw.drawCoordiantes(g1, panel1.Height, panel1.Width, separate_width, separate_height);
            draw.drawCoordiantes(g2, panel2.Height, panel2.Width, separate_width, separate_height);
            draw.drawCoordiantes(g3, panel3.Height, panel3.Width, separate_width, separate_height);
            draw.drawCoordiantes(g4, panel4.Height, panel4.Width, separate_width, separate_height);

            draw.drawNumbersY(g5, panel5.Height, separate_height);
            draw.drawNumbersY(g6, panel6.Height, separate_height);
            draw.drawNumbersY(g7, panel7.Height, separate_height);
            draw.drawNumbersY(g8, panel8.Height, separate_height);

            draw.drawNumbersX(g9, panel9.Width, separate_width);
            draw.drawNumbersX(g10, panel10.Width, separate_width);
            draw.drawNumbersX(g11, panel11.Width, separate_width);
            draw.drawNumbersX(g12, panel12.Width, separate_width);
        }

        public void drawCoordinateSystem2(int separate_width_s, int time_from, int time_to)
        {
            Graphics gg1 = panel13.CreateGraphics();
            Graphics gg2 = panel14.CreateGraphics();
            Graphics gg3 = panel15.CreateGraphics();
            gg1.Clear(Color.FloralWhite);
            gg2.Clear(Color.FloralWhite);
            gg3.Clear(Color.FloralWhite);

            
            if (separate_width_s == 24)
            {
                draw.drawCoordiantes(gg1, panel13.Height, panel13.Width, separate_width_s, separate_height);
                draw.drawNumbersY(gg2, panel14.Height, separate_height);
                draw.drawNumbersX(gg3, panel15.Width, separate_width_s);
            }
            else
            {
                draw.drawCoordiantes(gg1, panel13.Height, panel13.Width, separate_width_s, separate_height);
                draw.drawNumbersYC(gg2, panel14.Height, separate_height);
                draw.drawNumbersXC(gg3, panel15.Width, separate_width_s, time_from, time_to);
            }
            
        }

        public Sonde()
        {
            InitializeComponent();
            measure = 0;
            measure_probe = 0;
            measure_counter = 0;
            probe1_measure = 1;
            probe2_measure = 1;
            probe3_measure = 1;
            probe4_measure = 1;
            just_measured = 1;
            realTimeDrawGraph = 0;
            add_values_to_list = 0;
        }

        private void Sonde_Load(object sender, EventArgs e)
        {
            separate_width = 24;
            separate_height = 10;

            for (int i = 1; i <= number_of_ports; i++)
            {
                cbPort.Items.Add("COM" + Convert.ToString(i));
            }
            for(int i=0; i < 4; i++)
            {
                cbChoseGraphic.Items.Add(probe_chose[i]);
            }
            for(int i=0; i<25; i++)
            {
                cbFrom.Items.Add(Convert.ToString(i));
                cbTo.Items.Add(Convert.ToString(i));
            }
            cbFrom.Text = "0";
            cbTo.Text = "24";

            cbPort.Text = Properties.Settings.Default.comPortName;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd.MM.yyyy";
            dateTimePicker2.Value = new DateTime(dt.Year, dt.Month, dt.Day);

            cbTemperature.Checked = true;
            cbHumidity.Checked = true;

            timer1.Start();
            timer2.Start();

            folder = dateTimePicker1.Text;
            now_or_previous_day = dateTimePicker1.Text;
            path1 = current_directory + "\\database\\" + folder + "\\sonda1.txt";
            path2 = current_directory + "\\database\\" + folder + "\\sonda2.txt";
            path3 = current_directory + "\\database\\" + folder + "\\sonda3.txt";
            path4 = current_directory + "\\database\\" + folder + "\\sonda4.txt";
            Directory.CreateDirectory("database\\" + folder);
        }

        private void bPort_Click(object sender, EventArgs e)
        {
            try
            {
                if (comPort.IsOpen)
                {
                    comPort.Close();
                    rbPort.Checked = false;
                    bStart.Enabled = false;
                    cbPort.Enabled = true;
                }
                else
                {
                    comPort.DataBits = 8;
                    comPort.Parity = Parity.None;
                    comPort.StopBits = StopBits.One;
                    comPort.BaudRate = 9600;
                    comPort.Handshake = Handshake.None;
                    comPort.PortName = cbPort.Text;

                    comPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                    comPort.Open();


                    rbPort.Checked = true;
                    bStart.Enabled = true;
                    cbPort.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(message_port);
            }
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            realTimeDrawGraph = 1;
            drawCoordinateSystem();
        }

        private void bShowGrapic_Click(object sender, EventArgs e)
        {
            string date_string = dateTimePicker1.Value.ToString("dd.MM.yyyy");

            probe1_hourlist_saved.Clear();
            probe1_minuteslist_saved.Clear();
            probe1_secondslist_saved.Clear();
            probe1_temperaturelist_saved.Clear();
            probe1_humiditylist_saved.Clear();

            probe2_hourlist_saved.Clear();
            probe2_minuteslist_saved.Clear();
            probe2_secondslist_saved.Clear();
            probe2_temperaturelist_saved.Clear();
            probe2_humiditylist_saved.Clear();

            probe3_hourlist_saved.Clear();
            probe3_minuteslist_saved.Clear();
            probe3_secondslist_saved.Clear();
            probe3_temperaturelist_saved.Clear();
            probe3_humiditylist_saved.Clear();

            probe4_hourlist_saved.Clear();
            probe4_minuteslist_saved.Clear();
            probe4_secondslist_saved.Clear();
            probe4_temperaturelist_saved.Clear();
            probe4_humiditylist_saved.Clear();

            probe1_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[0]);
            probe1_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[0]);
            probe1_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[0]);
            probe1_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[0]);
            probe1_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[0]);

            probe2_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[1]);
            probe2_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[1]);
            probe2_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[1]);
            probe2_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[1]);
            probe2_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[1]);

            probe3_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[2]);
            probe3_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[2]);
            probe3_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[2]);
            probe3_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[2]);
            probe3_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[2]);

            probe4_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[3]);
            probe4_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[3]);
            probe4_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[3]);
            probe4_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[3]);
            probe4_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[3]);

            drawCoordinateSystem();
            Pen pt1 = new Pen(Color.Red, 1.5f);
            Pen ph1 = new Pen(Color.Green, 1f);
            Pen pt2 = new Pen(Color.Red, 1.5f);
            Pen ph2 = new Pen(Color.Green, 1f);
            Pen pt3 = new Pen(Color.Red, 1.5f);
            Pen ph3 = new Pen(Color.Green, 1f);
            Pen pt4 = new Pen(Color.Red, 1.5f);
            Pen ph4 = new Pen(Color.Green, 1f);
            Graphics gr1 = panel1.CreateGraphics();
            Graphics gr2 = panel2.CreateGraphics();
            Graphics gr3 = panel3.CreateGraphics();
            Graphics gr4 = panel4.CreateGraphics();

            for (int i = 1; i < probe1_temperaturelist_saved.Count; i++)
            {
                draw.drawingGraphsTemperatureSaved(probe1_temperaturelist_saved, probe1_hourlist_saved,
                                probe1_minuteslist_saved, probe1_secondslist_saved, pt1, gr1, panel1.Height, panel1.Width,
                                separate_width, separate_height, i);
            }
            for (int i = 1; i < probe1_humiditylist_saved.Count; i++)
            {
                draw.drawingGraphsHumuditySaved(probe1_humiditylist_saved, probe1_hourlist_saved,
                           probe1_minuteslist_saved, probe1_secondslist_saved, ph1, gr1, panel1.Height, panel1.Width,
                           separate_width, separate_height, i);
            }

            for (int i = 1; i < probe2_temperaturelist_saved.Count; i++)
            {
                draw.drawingGraphsTemperatureSaved(probe2_temperaturelist_saved, probe2_hourlist_saved,
                                probe2_minuteslist_saved, probe2_secondslist_saved, pt2, gr2, panel2.Height, panel2.Width,
                                separate_width, separate_height, i);
            }
            for (int i = 1; i < probe2_humiditylist_saved.Count; i++)
            {
                draw.drawingGraphsHumuditySaved(probe2_humiditylist_saved, probe2_hourlist_saved,
                           probe2_minuteslist_saved, probe2_secondslist_saved, ph2, gr2, panel2.Height, panel2.Width,
                           separate_width, separate_height, i);
            }

            for (int i = 1; i < probe3_temperaturelist_saved.Count; i++)
            {
                draw.drawingGraphsTemperatureSaved(probe3_temperaturelist_saved, probe3_hourlist_saved,
                                probe3_minuteslist_saved, probe3_secondslist_saved, pt3, gr3, panel3.Height, panel3.Width,
                                separate_width, separate_height, i);
            }
            for (int i = 1; i < probe3_humiditylist_saved.Count; i++)
            {
                draw.drawingGraphsHumuditySaved(probe3_humiditylist_saved, probe3_hourlist_saved,
                           probe3_minuteslist_saved, probe3_secondslist_saved, ph3, gr3, panel3.Height, panel3.Width,
                           separate_width, separate_height, i);
            }

            for (int i = 1; i < probe4_temperaturelist_saved.Count; i++)
            {
                draw.drawingGraphsTemperatureSaved(probe4_temperaturelist_saved, probe4_hourlist_saved,
                                probe4_minuteslist_saved, probe4_secondslist_saved, pt4, gr4, panel4.Height, panel4.Width,
                                separate_width, separate_height, i);
            }
            for (int i = 1; i < probe4_humiditylist_saved.Count; i++)
            {
                draw.drawingGraphsHumuditySaved(probe4_humiditylist_saved, probe4_hourlist_saved,
                           probe4_minuteslist_saved, probe4_secondslist_saved, ph4, gr4, panel4.Height, panel4.Width,
                           separate_width, separate_height, i);
            }
        }

        private void bDraw_Click(object sender, EventArgs e)
        {
            string date_string = dateTimePicker2.Value.ToString("dd.MM.yyyy.");

            separate_height_s = 10;
            time_from = int.Parse(cbFrom.Text);
            time_to = int.Parse(cbTo.Text);

            if (time_to - time_from > 0)
            {
                separate_width_s = time_to - time_from;
            }
            else
            {
                cbFrom.Text = "0";
                cbTo.Text = "24";
                separate_width_s = 24;
            }
                       
            drawCoordinateSystem2(separate_width_s, time_from, time_to);
            Pen pt = new Pen(Color.Red, 1.5f);
            Pen ph = new Pen(Color.Green, 1f);
            Graphics gg = panel13.CreateGraphics();

            try
            {

                if (cbChoseGraphic.Text == probe_chose[0])
                {
                    probe1_hourlist_saved.Clear();
                    probe1_minuteslist_saved.Clear();
                    probe1_secondslist_saved.Clear();
                    probe1_temperaturelist_saved.Clear();
                    probe1_humiditylist_saved.Clear();

                    probe1_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[0]);
                    probe1_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[0]);
                    probe1_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[0]);
                    probe1_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[0]);
                    probe1_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[0]);

                    if (cbTemperature.Checked)
                    {
                        for (int i = 1; i < probe1_temperaturelist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsTemperatureSaved(probe1_temperaturelist_saved, probe1_hourlist_saved,
                                    probe1_minuteslist_saved, probe1_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                    separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsTemperatureSavedC(probe1_temperaturelist_saved, probe1_hourlist_saved,
                                   probe1_minuteslist_saved, probe1_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                   separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                    if (cbHumidity.Checked)
                    {
                        for (int i = 1; i < probe1_humiditylist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsHumuditySaved(probe1_humiditylist_saved, probe1_hourlist_saved,
                                probe1_minuteslist_saved, probe1_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsHumuditySavedC(probe1_humiditylist_saved, probe1_hourlist_saved,
                                probe1_minuteslist_saved, probe1_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                }

                else if (cbChoseGraphic.Text == probe_chose[1])
                {
                    probe2_hourlist_saved.Clear();
                    probe2_minuteslist_saved.Clear();
                    probe2_secondslist_saved.Clear();
                    probe2_temperaturelist_saved.Clear();
                    probe2_humiditylist_saved.Clear();

                    probe2_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[1]);
                    probe2_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[1]);
                    probe2_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[1]);
                    probe2_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[1]);
                    probe2_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[1]);

                    if (cbTemperature.Checked)
                    {
                        for (int i = 1; i < probe2_temperaturelist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsTemperatureSaved(probe2_temperaturelist_saved, probe2_hourlist_saved,
                                probe2_minuteslist_saved, probe2_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsTemperatureSavedC(probe2_temperaturelist_saved, probe2_hourlist_saved,
                                probe2_minuteslist_saved, probe2_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                    if (cbHumidity.Checked)
                    {
                        for (int i = 1; i < probe2_humiditylist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsHumuditySaved(probe2_humiditylist_saved, probe2_hourlist_saved,
                                probe2_minuteslist_saved, probe2_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsHumuditySavedC(probe2_humiditylist_saved, probe2_hourlist_saved,
                                probe2_minuteslist_saved, probe2_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                }

                else if (cbChoseGraphic.Text == probe_chose[2])
                {
                    probe3_hourlist_saved.Clear();
                    probe3_minuteslist_saved.Clear();
                    probe3_secondslist_saved.Clear();
                    probe3_temperaturelist_saved.Clear();
                    probe3_humiditylist_saved.Clear();

                    probe3_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[2]);
                    probe3_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[2]);
                    probe3_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[2]);
                    probe3_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[2]);
                    probe3_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[2]);

                    if (cbTemperature.Checked)
                    {
                        for (int i = 1; i < probe3_temperaturelist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsTemperatureSaved(probe3_temperaturelist_saved, probe3_hourlist_saved,
                                probe3_minuteslist_saved, probe3_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsTemperatureSavedC(probe3_temperaturelist_saved, probe3_hourlist_saved,
                                probe3_minuteslist_saved, probe3_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                    if (cbHumidity.Checked)
                    {
                        for (int i = 1; i < probe3_humiditylist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsHumuditySaved(probe3_humiditylist_saved, probe3_hourlist_saved,
                                probe3_minuteslist_saved, probe3_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsHumuditySavedC(probe3_humiditylist_saved, probe3_hourlist_saved,
                                probe3_minuteslist_saved, probe3_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                }

                else if (cbChoseGraphic.Text == probe_chose[3])
                {
                    probe4_hourlist_saved.Clear();
                    probe4_minuteslist_saved.Clear();
                    probe4_secondslist_saved.Clear();
                    probe4_temperaturelist_saved.Clear();
                    probe4_humiditylist_saved.Clear();

                    probe4_hourlist_saved = filework.takeHour(current_directory, date_string, probe_txt[3]);
                    probe4_minuteslist_saved = filework.takeMinutes(current_directory, date_string, probe_txt[3]);
                    probe4_secondslist_saved = filework.takeSeconds(current_directory, date_string, probe_txt[3]);
                    probe4_temperaturelist_saved = filework.takeTemperature(current_directory, date_string, probe_txt[3]);
                    probe4_humiditylist_saved = filework.takeHumidity(current_directory, date_string, probe_txt[3]);

                    if (cbTemperature.Checked)
                    {
                        for (int i = 1; i < probe4_temperaturelist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsTemperatureSaved(probe4_temperaturelist_saved, probe4_hourlist_saved,
                                probe4_minuteslist_saved, probe4_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsTemperatureSavedC(probe4_temperaturelist_saved, probe4_hourlist_saved,
                                probe4_minuteslist_saved, probe4_secondslist_saved, pt, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                    if (cbHumidity.Checked)
                    {
                        for (int i = 1; i < probe4_humiditylist_saved.Count; i++)
                        {
                            if (separate_width_s == 24)
                            {
                                draw.drawingGraphsHumuditySaved(probe4_humiditylist_saved, probe4_hourlist_saved,
                                probe4_minuteslist_saved, probe4_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width, separate_height, i);
                            }
                            else
                            {
                                draw.drawingGraphsHumuditySavedC(probe4_humiditylist_saved, probe4_hourlist_saved,
                                probe4_minuteslist_saved, probe4_secondslist_saved, ph, gg, panel13.Height, panel13.Width,
                                separate_width_s, separate_height, i, time_from, time_to);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ne postoje podaci za izabranu sondu");
            }
            

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            tbTimeDate.Text = localDate.ToString("dd.MM.yyyy.  HH:mm:ss");
            hour = localDate.Hour;
            minutes = localDate.Minute;
            seconds = localDate.Second;

            now_or_previous_day = dt.ToString("dd.MM.yyyy");
            if(now_or_previous_day != folder)
            {
                folder = now_or_previous_day;
                Directory.CreateDirectory("database\\" + folder);
                path1 = current_directory + "\\database\\" + folder + "\\sonda1.txt";
                path2 = current_directory + "\\database\\" + folder + "\\sonda2.txt";
                path3 = current_directory + "\\database\\" + folder + "\\sonda3.txt";
                path4 = current_directory + "\\database\\" + folder + "\\sonda4.txt";
            }
            tbProbe1Temperature.Text = Convert.ToString(probe1_temperature) + "°C";
            tbProbe2Temperature.Text = Convert.ToString(probe2_temperature) + "°C";
            tbProbe3Temperature.Text = Convert.ToString(probe3_temperature) + "°C";
            tbProbe4Temperature.Text = Convert.ToString(probe4_temperature) + "°C";
            tbProbe1Humidity.Text = Convert.ToString(probe1_humidity) + "%";
            tbProbe2Humidity.Text = Convert.ToString(probe2_humidity) + "%";
            tbProbe3Humidity.Text = Convert.ToString(probe3_humidity) + "%";
            tbProbe4Humidity.Text = Convert.ToString(probe4_humidity) + "%";

            if(realTimeDrawGraph == 1)
            {
                if(add_values_to_list == 1)
                {
                    probe1_hourlist.Add(hour);
                    probe1_minuteslist.Add(minutes);
                    probe1_secondslist.Add(seconds);
                    probe1_temperaturelist.Add(probe1_temperature);
                    probe1_humiditylist.Add(probe1_humidity);
                    add_values_to_list = 0;

                    Pen p1t = new Pen(Color.Red, 1.5f);
                    Pen p1h = new Pen(Color.Green, 1f);

                    Graphics g1 = panel1.CreateGraphics();

                    if (probe1_temperaturelist.Count >= 2)
                    {
                        draw.drawingGraphsTemperature(probe1_temperaturelist, probe1_hourlist, probe1_minuteslist, probe1_secondslist,
                            p1t, g1, panel1.Height, panel1.Width, separate_width, separate_height);
                        draw.drawingGraphsHumudity(probe1_humiditylist, probe1_hourlist, probe1_minuteslist, probe1_secondslist,
                            p1h, g1, panel1.Height, panel1.Width, separate_width, separate_height);
                    }

                    filework.writeFile(fileW, path1, hour, minutes, seconds, probe1_temperature, probe1_humidity);
                }
                else if (add_values_to_list == 2)
                {
                    probe2_hourlist.Add(hour);
                    probe2_minuteslist.Add(minutes);
                    probe2_secondslist.Add(seconds);
                    probe2_temperaturelist.Add(probe2_temperature);
                    probe2_humiditylist.Add(probe2_humidity);
                    add_values_to_list = 0;

                    Pen p2t = new Pen(Color.Red, 1.5f);
                    Pen p2h = new Pen(Color.Green, 1f);

                    Graphics g2 = panel2.CreateGraphics();

                    if (probe2_temperaturelist.Count >= 2)
                    {
                        draw.drawingGraphsTemperature(probe2_temperaturelist, probe2_hourlist, probe2_minuteslist, probe2_secondslist,
                            p2t, g2, panel2.Height, panel2.Width, separate_width, separate_height);
                        draw.drawingGraphsHumudity(probe2_humiditylist, probe2_hourlist, probe2_minuteslist, probe2_secondslist,
                            p2h, g2, panel2.Height, panel2.Width, separate_width, separate_height);
                    }

                    filework.writeFile(fileW, path2, hour, minutes, seconds, probe2_temperature, probe2_humidity);
                }
                else if (add_values_to_list == 3)
                {
                    probe3_hourlist.Add(hour);
                    probe3_minuteslist.Add(minutes);
                    probe3_secondslist.Add(seconds);
                    probe3_temperaturelist.Add(probe3_temperature);
                    probe3_humiditylist.Add(probe3_humidity);
                    add_values_to_list = 0;

                    Pen p3t = new Pen(Color.Red, 1.5f);
                    Pen p3h = new Pen(Color.Green, 1f);

                    Graphics g3 = panel3.CreateGraphics();

                    if (probe3_temperaturelist.Count >= 2)
                    {
                        draw.drawingGraphsTemperature(probe3_temperaturelist, probe3_hourlist, probe3_minuteslist, probe3_secondslist,
                            p3t, g3, panel3.Height, panel3.Width, separate_width, separate_height);
                        draw.drawingGraphsHumudity(probe3_humiditylist, probe3_hourlist, probe3_minuteslist, probe3_secondslist,
                            p3h, g3, panel3.Height, panel3.Width, separate_width, separate_height);
                    }

                    filework.writeFile(fileW, path3, hour, minutes, seconds, probe3_temperature, probe3_humidity);
                }
                else if (add_values_to_list == 4)
                {
                    probe4_hourlist.Add(hour);
                    probe4_minuteslist.Add(minutes);
                    probe4_secondslist.Add(seconds);
                    probe4_temperaturelist.Add(probe4_temperature);
                    probe4_humiditylist.Add(probe4_humidity);
                    add_values_to_list = 0;

                    Pen p4t = new Pen(Color.Red, 1.5f);
                    Pen p4h = new Pen(Color.Green, 1f);

                    Graphics g4 = panel4.CreateGraphics();

                    if (probe4_temperaturelist.Count >= 2)
                    {
                        draw.drawingGraphsTemperature(probe4_temperaturelist, probe4_hourlist, probe4_minuteslist, probe4_secondslist,
                            p4t, g4, panel4.Height, panel4.Width, separate_width, separate_height);
                        draw.drawingGraphsHumudity(probe4_humiditylist, probe4_hourlist, probe4_minuteslist, probe4_secondslist,
                            p4h, g4, panel4.Height, panel4.Width, separate_width, separate_height);
                    }

                    filework.writeFile(fileW, path4, hour, minutes, seconds, probe4_temperature, probe4_humidity);
                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(measure == 0) {
                measure_counter++;
                if(measure_counter >= 39)
                {
                    measure = 1;
                    measure_counter = 0;
                }
            }
            else
            {
                try
                {
                    if ((comPort.IsOpen)&&(just_measured == 1))
                    {

                        if (measure_probe == 0)
                        {
                            if (probe1_measure == 1) {
                                comPort.Write("1");
                                just_measured = 0;
                            }
                            measure_probe++;
                        }
                        else if (measure_probe == 1)
                        {
                            if (probe2_measure == 1) {
                                comPort.Write("2");
                                just_measured = 0;
                            }
                            measure_probe++;
                        }
                        else if (measure_probe == 2)
                        {
                            if (probe3_measure == 1) {
                                comPort.Write("3");
                                just_measured = 0;
                            }
                            measure_probe++;
                        }
                        else if (measure_probe == 3)
                        {
                            if (probe4_measure == 1) {
                                comPort.Write("4");
                                just_measured = 0;
                            }
                            measure_probe = 0;
                            measure = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške. Proveri port");
                }
            }
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(150);
           
            try
            {

                int bytestoread = comPort.BytesToRead;
                byte[] received_buffer = new byte[9];

                comPort.Read(received_buffer, 0, bytestoread);

                if ((received_buffer[0] == 0x21) && (received_buffer[8] == 0x1B))
                {
                    if (received_buffer[1] == ADR1)
                    {
                        probe1_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe1_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                        add_values_to_list = 1;                        
                    }
                    else if (received_buffer[1] == ADR2)
                    {
                        probe2_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe2_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                        add_values_to_list = 2;
                    }
                    else if (received_buffer[1] == ADR3)
                    {
                        probe3_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe3_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                        add_values_to_list = 3;
                    }
                    else if (received_buffer[1] == ADR4)
                    {
                        probe4_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe4_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                        add_values_to_list = 4;
                    }
                    just_measured = 1;
                }
                
            }
            catch (Exception ex)
            {

            }

        }

        private void Sonde_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.comPortName = cbPort.Text;
            Properties.Settings.Default.Save();
        }

        private void Sonde_ResizeEnd(object sender, EventArgs e)
        {
            drawCoordinateSystem();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawCoordinateSystem();
        }
    }
}
