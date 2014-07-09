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
        private List<Coordinate> coords;

        private List<string> ids;

        private const int LATENCY = 10;
        private int cameraOrder = 1;


        public Form1()
        {
            InitializeComponent();

           

            this.ids = new List<string>();
            this.coords = new List<Coordinate>();

            //**********************************************
            int timeComp = timePassed;
            StringBuilder sb = new StringBuilder();
            
            b = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            b2 = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(b);
            g2 = Graphics.FromImage(b2); 
            //**********************************************
           

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
                //file.WriteAsync(sb.ToString());
                file.Write(sb.ToString());
            }
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            timePassed +=LATENCY;
            //»»»»»»»»»»»»»»»»»»»»»»»»»»»»
            StringBuilder sb = new StringBuilder();
            g.Clear(Color.Black);
            g2.Clear(Color.Black);

            if (wc[0].WiimoteState.IRState.IRSensors[0].Found && (wc[1].WiimoteState.IRState.IRSensors[0].Found))
            {
                int cam1 = 0;
                int cam2 = 1;
                if(this.cameraOrder==2)
                {
                    cam1 = 1;
                    cam2 = 0;
                }
                decimal posX = (decimal)((screenWidth - (wc[cam1].WiimoteState.IRState.IRSensors[0].Position.X * screenWidth)) - screenWidth/2 );
                decimal posY = (decimal)(screenHeight - ((wc[cam1].WiimoteState.IRState.IRSensors[0].Position.Y * screenHeight)) - screenHeight/2 );
                decimal posZ = (decimal)(-((screenHeight - (wc[cam2].WiimoteState.IRState.IRSensors[0].Position.X * screenHeight)) - screenHeight/2 ));

                Coordinate c = new Coordinate(inc, posX, posY, posZ, this.timePassed);
                this.coords.Add(c);
                inc++;
                g2.DrawEllipse(new Pen(Color.Red), 100 / 2, (int)((-posZ+screenHeight/2)), 2, 2);
                g.DrawEllipse(new Pen(Color.Red), (int)(posX+screenWidth/2), (int)(posY + screenHeight/2), 2, 2);
                this.SetTextX(posX.ToString());
                this.SetTextY(posY.ToString());
                this.SetTextZ(posZ.ToString()); ;

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
                try
                {
                    this.Invoke(d, new object[] { text });
                }
                catch(Exception error)
                { }

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
                    aTimer.Elapsed -= OnTimedEvent;
                 //   t.Abort();
                    break;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
          /*  string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            file = new StreamWriter(mydocpath + @"\" + DateTime.Now.ToString("MM-dd-yyyy_h-mm_tt") + ".txt", true);*/
            StringBuilder s = new StringBuilder(); 
            s.Append("/JOB").AppendLine().Append("//NAME Teste_robot").AppendLine().Append("//POS").AppendLine().Append("///NPOS 22,0,0,0,0,0");
            s.AppendLine().Append("///USER 1").AppendLine().Append("///TOOL 1").AppendLine().Append("///POSTYPE USER").AppendLine();
            s.Append("///RECTAN").AppendLine().Append("///RCONF 0,0,0,0,0,0").AppendLine();
            for(int i =0; i<this.coords.Count; i++)
            {
                s.Append(this.coords[i].toString());
            }
            s.Append("//INST").AppendLine().Append("///DATE").Append(DateTime.Now.ToString("yyyy/MM/dd h:mm")).AppendLine();
            s.Append("///COMM GENERATED BY DELCAM").AppendLine().Append("///ATTR SC,RW,RJ").AppendLine().Append("////FRAME USER 1").AppendLine();
            s.Append("///GROUP1 RB1, BS1").AppendLine().Append("LVARS 2.0.0.0.0.0.0.0").AppendLine();
            s.Append("NOP").AppendLine().Append("' Program Start");
         //   this.writeToFile(file, s);

           // file.Close();

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(fs);
                writer.Write(s.ToString());
                writer.Close();
            }
        }

        private void initBtn_Click(object sender, EventArgs e)
        {
            pbIR.Image = b;
            pbIR2.Image = b2;
            aTimer = new System.Timers.Timer(LATENCY);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            this.inc = 0;
            this.coords.Clear();
            this.coords = new List<Coordinate>();
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cameraOrder == 1)
                this.cameraOrder = 2;
            else
                this.cameraOrder = 1;
        }


    }
}
