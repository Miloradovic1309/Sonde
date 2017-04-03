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
        SerialPort comPort = new SerialPort();
        DateTime dt = DateTime.Now;


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

        float probe1_temperature;
        float probe2_temperature;
        float probe3_temperature;
        float probe4_temperature;
        float probe1_humidity;
        float probe2_humidity;
        float probe3_humidity;
        float probe4_humidity;

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
        }

        private void Sonde_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= number_of_ports; i++)
            {
                cbPort.Items.Add("COM" + Convert.ToString(i));
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy";
            dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);

            timer1.Start();
            timer2.Start();
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
                MessageBox.Show("Došlo je do greške.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            tbTimeDate.Text = localDate.ToString("dd.MM.yyyy.  HH:mm:ss");
            hour = localDate.Hour;
            minutes = localDate.Minute;
            seconds = localDate.Second;

            tbProbe1Temperature.Text = Convert.ToString(probe1_temperature) + "°C";
            tbProbe2Temperature.Text = Convert.ToString(probe2_temperature) + "°C";
            tbProbe3Temperature.Text = Convert.ToString(probe3_temperature) + "°C";
            tbProbe4Temperature.Text = Convert.ToString(probe4_temperature) + "°C";
            tbProbe1Humidity.Text = Convert.ToString(probe1_humidity) + "%";
            tbProbe2Humidity.Text = Convert.ToString(probe2_humidity) + "%";
            tbProbe3Humidity.Text = Convert.ToString(probe3_humidity) + "%";
            tbProbe4Humidity.Text = Convert.ToString(probe4_humidity) + "%";
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
                        
                    }
                    else if (received_buffer[1] == ADR2)
                    {
                        probe2_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe2_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                    }
                    else if (received_buffer[1] == ADR3)
                    {
                        probe3_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe3_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                    }
                    else if (received_buffer[1] == ADR4)
                    {
                        probe4_temperature = (float)(((float)received_buffer[3] * 100 + (float)received_buffer[4] * 10
                            + (float)received_buffer[4]) / 10);
                        probe4_humidity = (int)(received_buffer[6] * 10 + received_buffer[7]);
                    }
                    just_measured = 1;
                }
                
            }
            catch (Exception ex)
            {

            }

        }

    }
}
