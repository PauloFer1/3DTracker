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

          //  b = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
          //  b2 = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
          //  g = Graphics.FromImage(b);
           // g2 = Graphics.FromImage(b2);
            
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
           // Console.WriteLine("IDDDDD-» " + wc[0].ID.ToString());
         //   t = new Thread(loopIR);
         //   t.Start();

        }
        
        void loopIR()
        {
            int timeComp = timePassed;
            StringBuilder sb = new StringBuilder();
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StreamWriter outfile = new StreamWriter(mydocpath + @"\ir_coords.txt", true);
            b = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            b2 = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(b);
            g2 = Graphics.FromImage(b2); 
            while(true)
            {
               g.Clear(Color.Black);
               g2.Clear(Color.Black);
               
               if(wc[0].WiimoteState.IRState.IRSensors[0].Found && (wc[1].WiimoteState.IRState.IRSensors[0].Found))
               {
                   int posX = (int)(screenWidth - (wc[0].WiimoteState.IRState.IRSensors[0].Position.X * screenWidth));
                   int posY = (int)(screenWidth - (wc[0].WiimoteState.IRState.IRSensors[0].Position.Y * screenHeight));
                   int posZ = (int)(screenHeight - (wc[1].WiimoteState.IRState.IRSensors[0].Position.X * screenHeight));
                   sb.AppendLine("("+posX+","+posY+","+posZ+")=>"+timePassed);
                   sb.AppendLine();
                   Console.WriteLine(timePassed + " - " + timeComp);
                   if (timePassed > timeComp)
                   {
                       Console.WriteLine("writefile");
                       timeComp = timePassed;
                       writeToFile(outfile, sb);
                   }
                   Console.WriteLine("FOUND");
                   g2.DrawEllipse(new Pen(Color.Red), 100 / 2, posZ, 2, 2);
                   g.DrawEllipse(new Pen(Color.Red), posX, posY, 2, 2);
                   this.SetTextX((screenWidth-wc[0].WiimoteState.IRState.IRSensors[0].Position.X * screenWidth).ToString());
                   this.SetTextY((screenWidth-wc[0].WiimoteState.IRState.IRSensors[0].Position.Y * screenWidth).ToString());
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
               
            } 
        }
        private void writeToFile(StreamWriter file, StringBuilder sb) 
        {
            //string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

          //  using (file)//StreamWriter outfile = new StreamWriter(mydocpath + @"\ir_coords.txt", true))
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
        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs args)
        {
            if (args.Inserted)
                ((Wiimote)sender).SetReportType(InputReport.IRExtensionAccel, true);    // return extension data
            else
                ((Wiimote)sender).SetReportType(InputReport.IRAccel, true);            // back to original mode
        }

        void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            // current state information
            WiimoteState ws = args.WiimoteState;

            Wiimote w = (Wiimote)sender;

            if (this.ids.Count > 0)
            {
                //Console.WriteLine(w.ID.ToString().Equals(this.ids[0]));
                if (w.ID.ToString().Equals(this.ids[0]))
                {
                    Console.WriteLine("2222222222222222222222222");
                    g.Clear(Color.Black);
                    if (ws.IRState.IRSensors[0].Found)//&& ws.IRState.IRSensors[1].Found)
                    {
                        UpdateIR(ws.IRState.IRSensors[0], Color.Red);
                      //  Console.WriteLine("X:" + (ws.IRState.IRSensors[0].Position.X * 256) + ", Y:" + (ws.IRState.IRSensors[0].Position.Y * 192));
                        g.DrawEllipse(new Pen(Color.Red), (ws.IRState.IRSensors[0].Position.X * 256), (ws.IRState.IRSensors[0].Position.Y * 192), 2, 2);
                      // pbIR.Image = b;
                        this.SetTextX((ws.IRState.IRSensors[0].Position.X * 256).ToString());
                        this.SetTextY((ws.IRState.IRSensors[0].Position.Y * 256).ToString());
                    }
                    
                    pbIR.Image = b;
                   // try { 
                        //pbIR.Image = b; 
                   // }
                   /* catch (Exception e)
                    { Console.WriteLine(e.ToString()); }*/
                }
                else
                {
                    Console.WriteLine("2222222222222222222222222");
                    g2.Clear(Color.Black);
                    if (ws.IRState.IRSensors[0].Found)//&& ws.IRState.IRSensors[1].Found)
                    {
                        UpdateIR(ws.IRState.IRSensors[0], Color.Red);
                      //  Console.WriteLine("X:" + (ws.IRState.IRSensors[0].Position.X * 256) + ", Y:" + (ws.IRState.IRSensors[0].Position.Y * 192));
                        g2.DrawEllipse(new Pen(Color.Red), (ws.IRState.IRSensors[0].Position.X * 256), (ws.IRState.IRSensors[0].Position.Y * 192), 2, 2);
                       // pbIR2.Image = b2;
                        this.SetTextZ((ws.IRState.IRSensors[0].Position.X * 192).ToString());
                    }
                    
                    pbIR2.Image = b2;
                   
                    //try { 
                       // pbIR2.Image = b2;
                    //}
                    /*catch(Exception e)
                    { Console.WriteLine(e.ToString()); }
                   */
                }
            }
            // write out the state of the A button    
            //Console.WriteLine(ws.ButtonState.A);
        }
        private void UpdateIR(IRSensor irSensor, Color color)
        {
           // chkFound.Checked = irSensor.Found;

          /*  if (irSensor.Found)
            {
              //  lblNorm.Text = irSensor.Position.ToString() + ", " + irSensor.Size;
              //  lblRaw.Text = irSensor.RawPosition.ToString();
                g.DrawEllipse(new Pen(color), (int)(irSensor.RawPosition.X / 4), (int)(irSensor.RawPosition.Y / 4),
                             irSensor.Size + 1, irSensor.Size + 1);
            }*/
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
