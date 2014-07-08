using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiimoteLib;
using System.Threading;
using System.Timers;
using System.IO;
using System.Text;


namespace WiiTest
{
    delegate void SetTextCallback(string text);
    public partial class Form1 : Form
    {
        private int screenWidth = 512;
        private int screenHeight = 384;
        private Bitmap b;// = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Bitmap b2;// = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Graphics g;
        private Graphics g2;
        private Thread t;
        private WiimoteCollection wc;
        private System.Timers.Timer aTimer;
        private int timePassed=0;
        private int inc = 0;
        private StreamWriter file;

        private List<string> ids;

        public Form1()
        {
            InitializeComponent();

           

            this.ids = new List<string>();
            
            //**********************************************
            int timeComp = timePassed;
            StringBuilder sb = new StringBuilder();
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //StreamWriter outfile = new StreamWriter(mydocpath + @"\ir_coords.txt", true);
            file = new StreamWriter(mydocpath + @"\ir_coords.txt", true);
            b = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            b2 = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(b);
            g2 = Graphics.FromImage(b2); 
            //**********************************************
            aTimer = new System.Timers.Timer(10);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;

            wc = new WiimoteCollection();
            try
            {
                wc.FindAllWiimotes();
            }
            catch (WiimoteNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WiimoteException ex)
            {
                MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            int index = 1;
            foreach (Wiimote wm in wc)
            {
               // wm.WiimoteChanged += wm_WiimoteChanged;

                wm.Connect();
                wm.SetReportType(InputReport.IRAccel, true);

                string id = (string)wm.ID.ToString();
                this.ids.Add(id);
                wm.SetLEDs(index);
                index++;
            }

        }
        private void writeToFile(StreamWriter file, StringBuilder sb) 
        {
            {
                file.WriteAsync(sb.ToString());
            }
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timePassed +=10;
            //»»»»»»»»»»»»»»»»»»»»»»»»»»»»
            StringBuilder sb = new StringBuilder();
            g.Clear(Color.Black);
            g2.Clear(Color.Black);

            if (wc[0].WiimoteState.IRState.IRSensors[0].Found && (wc[1].WiimoteState.IRState.IRSensors[0].Found))
            {
                int posX = (int)(screenWidth - (wc[0].WiimoteState.IRState.IRSensors[0].Position.X * screenWidth));
                int posY = (int)(screenWidth - (wc[0].WiimoteState.IRState.IRSensors[0].Position.Y * screenHeight));
                int posZ = (int)(screenHeight - (wc[1].WiimoteState.IRState.IRSensors[0].Position.X * screenHeight));

                sb.AppendLine("(" + posX + "," + posY + "," + posZ + ")=>" + timePassed);
                sb.AppendLine();
                Coordinate c = new Coordinate(inc, posX, posY, posZ);
                inc++;
                writeToFile(file, c.toString());
                Console.WriteLine("FOUND");
                g2.DrawEllipse(new Pen(Color.Red), 100 / 2, posZ, 2, 2);
                g.DrawEllipse(new Pen(Color.Red), posX, posY, 2, 2);
                this.SetTextX((screenWidth - wc[0].WiimoteState.IRState.IRSensors[0].Position.X * screenWidth).ToString());
                this.SetTextY((screenWidth - wc[0].WiimoteState.IRState.IRSensors[0].Position.Y * screenWidth).ToString());
                this.SetTextZ((screenHeight - (wc[1].WiimoteState.IRState.IRSensors[0].Position.X * screenHeight)).ToString()); ;

                pbIR.Image = b;
                pbIR2.Image = b2;
            }
            if (wc[0].WiimoteState.IRState.IRSensors[0].Found)
                SetStatus1("FOUND");
            else
                SetStatus1("LOST");
            if (wc[1].WiimoteState.IRState.IRSensors[0].Found)
                SetStatus2("FOUND");
            else
                SetStatus2("LOST");
           // Console.WriteLine("The Elapsed event was raised at {0}", timePassed);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void SetTextX(string text)
        {
            if (this.labelX.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextX);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelX.Text = text;
            }
        }
        private void SetTextY(string text)
        {
            if (this.labelY.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextY);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelY.Text = text;
            }
        }
        private void SetTextZ(string text)
        {
            if (this.labelZ.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTextZ);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelZ.Text = text;
            }
        }
        private void SetStatus1(string text)
        {
            if (this.status1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatus1);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.status1.Text = text;
            }
        }
        private void SetStatus2(string text)
        {
            if (this.status2.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetStatus2);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.status2.Text = text;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                 //   t.Abort();
                    break;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}
