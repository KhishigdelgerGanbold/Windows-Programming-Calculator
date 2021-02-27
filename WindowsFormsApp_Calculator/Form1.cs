using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Calculator
{
        public partial class CalculatorBase : Form
        {
            double result = 0;
            char input_operator;
            string asmd_operator = ""; // ASMD - Addition, Subtraction, Multiplication, Division
            bool insert_value = false;

            public CalculatorBase()
            {
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {

            }

        private void numbers_zerotonine(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if((box_display.Text == "0") || insert_value)
                box_display.Text = "";
            insert_value = false;
            box_display.Text = box_display.Text + b.Text;
        }

        private void common_operators(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if(result != 0)
            {

            }

            asmd_operator = b.Text;
            result = double.Parse(box_display.Text);
            box_display.Text = "";
            subbox_display.Text = System.Convert.ToString(result) + " " + asmd_operator;
        }
    }
}
