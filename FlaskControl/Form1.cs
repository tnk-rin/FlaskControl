using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            Delete();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadlist.Items.Clear();
            foreach (Color c in colors)
            {
                loadlist.Items.Add(c.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String path = "C:\\FlaskPatterns\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            saveFileDialog1.InitialDirectory = path;
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                String s = "";
                foreach (Color c in colors)
                {
                    s += ColorTranslator.ToHtml(c) + "\n";
                }
                byte[] data = new UTF8Encoding(true).GetBytes(s);
                fs.Write(data);
                fs.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String path = "C:\\FlaskPatterns\\";
            openFileDialog1.InitialDirectory = path;
            try
            {
                openFileDialog1.ShowDialog();
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
                return;
            }            
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                FileStream fs = (FileStream)openFileDialog1.OpenFile();
                String[] file = File.ReadAllLines(fs.Name);
                fs.Close();
                List<Color> cacheList = new List<Color>();
                foreach (String l in file)
                {
                    cacheList.Add(ColorTranslator.FromHtml(l));
                }
                UpdateLists(cacheList);
            }
        }

        private void UpdateLists(List<Color> cacheList)
        {
            colors.Clear();
            colorList.Items.Clear();
            loadlist.Items.Clear();
            foreach (Color c in cacheList)
            {
                colors.Add(c);
                colorList.Items.Add(c.ToString());
                loadlist.Items.Add(c.ToString());
            }
        }

        private void Delete()
        {
            int i = colorList.SelectedIndex;
            if (i < colorList.Items.Count && i >= 0)
            {
                colorList.Items.RemoveAt(i);
                loadlist.Items.RemoveAt(i);
                colors.RemoveAt(i);
            }
            if (i > 0)
                colorList.SelectedIndex = i - 1;
            else if (colorList.Items.Count > 0)
                colorList.SelectedIndex = 0;
            else if (colorList.Items.Count == 0)
                label1.BackColor = Color.Transparent;
        }

        private void colorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = colorList.SelectedIndex;
            if (index >= 0 && index < colorList.Items.Count)
            {
                label1.BackColor = colors[index];
                cache = colors[index];
            }
        }

        private void ColorList_KeyDown(object sender, KeyEventArgs e)
        {
            // KeyValue 46 = Delete
            // KeyValue 8 = Backspace
            if (e.KeyValue == 46 || e.KeyValue == 8)
                Delete();
        }
    }
}
