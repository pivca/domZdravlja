using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomZdravlja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Doktori forma2 = new Doktori();
            forma2.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Pregled forma3 = new Pregled();
            forma3.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Pretraga forma4 = new Pretraga();
            forma4.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Statistika forma4 = new Statistika();
            forma4.ShowDialog();
        }
    }
}
