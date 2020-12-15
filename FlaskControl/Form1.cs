using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlaskControl
{
    public partial class Form1 : Form
    {
        private Color cache;
        List<Color> colors = new List<Color>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Exec()
        {
            colorDialog1.ShowDialog();
            var col = colorDialog1.Color;
            var ip = textBox1.Text;
            if (!col.IsEmpty)
            {
                label1.BackColor = col;
                cache = col;
                //if (ip != "")
                //    Program.Submit(col, ip);
            }
        }

        private void AddColor()
        {
            if (!cache.IsEmpty)
            {
                colors.Add(cache);
                colorList.Items.Add(cache.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Exec();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ip = textBox1.Text;
            if(ip != "")
                Program.PatternSubmit(colors, ip);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = colorList.SelectedIndex;
            if (i < colorList.Items.Count && i >= 0)
            {
                colorList.Items.RemoveAt(i);
                colors.RemoveAt(i);
            }
        }
    }
}
