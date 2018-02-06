using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslateTelegrammBot
{
    public partial class Form1 : Form
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var s in LogicDB.GetAll())
            {
                listBox1.Items.Add(s.En);
                listBox2.Items.Add(s.Ru);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LogicDB.saveData("mouse", "мышь");
            foreach (var s in LogicDB.GetAll())
            {
                listBox1.Items.Add(s.En);
                listBox2.Items.Add(s.Ru);
            } 
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var s in LogicDB.GetAll())
            {
                listBox1.Items.Add(s.En);
                listBox2.Items.Add(s.Ru);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
