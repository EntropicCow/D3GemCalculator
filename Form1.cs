using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace D3GemCalculator
{
    public partial class Form1 : Form
    {
        //instantiate the gem class as "GemCalculator" using "new" to invoke constructor and default values
        Gem GemCalculator = new Gem();

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        { 
            listBox1.Items.Clear(); // clear the listbox everytime the calculate button is pressed

            string[] output;
            output = GemCalculator.SanityCheck(comboBox1.SelectedIndex, comboBox2.SelectedIndex, textBox1.Text, comboBox1.SelectedItem, comboBox2.SelectedItem);

            foreach (string message in output)
            {
                if(message == "")
                {
                    ; //no deathsbreath text, so do nothing.
                }
                else
                listBox1.Items.Add(message);
            }
        }

        private void Aboutbutton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programmed by Jose A. Araujo 2014 \nThanks to Dale and William for inspiration.", "Diablo 3 Gem Calculator");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(sender, e);
                e.Handled = true;
            }

        }
    }
}
