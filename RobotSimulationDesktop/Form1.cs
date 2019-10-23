using RobotSimulationClass;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotSimulationDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public async void WaitSomeTime()
        {
            await Task.Delay(5000000);
            this.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Populate viewlist with commands
            RobotPosition robotPosition = new RobotPosition();
            foreach (var item in robotPosition.commandList())
            {
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Graphics gr = panel1.CreateGraphics();
            //Pen blackPen = new Pen(Brushes.Black, 1);
            //Pen redPen = new Pen(Brushes.Red, 1);
            //Font myFont = new Font("Arial", 10);

            //int lines = Desk.maxX+1;
            //float x = 0f;
            //float y = 0f;
            //float xSpace = panel1.Width / lines;
            //float ySpace = panel1.Height / lines;

            ////Vertical lines
            //for (int i = 0; i < lines + 1; i++)
            //{
            //    gr.DrawLine(blackPen, x, y, x, panel1.Height);
            //    x += xSpace;
            //}

            ////Horizontal lines
            //x = 0f;           
            //for (int i = 0; i < lines + 1; i++)
            //{
            //    gr.DrawLine(blackPen, x, y, panel1.Width,y);
            //    y += ySpace;
            //}

            ////text
            //x = 0f;
            //y = 0f;
            //for (int r = 0; r <= lines; r++)
            //{
            //    for (int c = 0; c <= lines; c++)
            //    {

            //        gr.DrawString(((r-4)*-1)+","+c, myFont, Brushes.Black, x, y);
            //        x += xSpace;
            //    }
            //    y += ySpace;
            //    x = 0f;
            //}

            //Highlight each item one at a time
            for (int a = 0; a < listView1.Items.Count; a++)
            {
                listView1.Items[a].Selected = true;
                WaitSomeTime();
                Application.DoEvents();
                
            }
        }
    }
}
