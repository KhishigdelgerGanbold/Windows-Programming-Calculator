using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {

        private CalculatorBase calculator;
        private List<TableLayoutPanel> panels;

        double result = 0;
        string asmd_operator = "";
        bool insert_value = false;

        public Form1()
        {
            InitializeComponent();
            calculator = new CalculatorBase();
            panels = new List<TableLayoutPanel>();
            btnMClearAll.Enabled = false;
            btnMRecall.Enabled = false;
            btnMAdd.Enabled = false;
            btnMSub.Enabled = false;
        }

        private void numbers_0to9(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if ((txtBxResult.Text == "0") || insert_value)
                txtBxResult.Text = "";
            insert_value = false;
            txtBxResult.Text = txtBxResult.Text + b.Text;
        }

        private void ar_operations(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (result != 0)
            {
                btn_eql.PerformClick();
                insert_value = true;
                asmd_operator = b.Text;
                subbox_display.Text = result + " " + asmd_operator;
            }
            else
            {
                asmd_operator = b.Text;
                result = double.Parse(txtBxResult.Text);
                txtBxResult.Text = "";
                subbox_display.Text = System.Convert.ToString(result) + " " + asmd_operator;
            }
        }

        private void btn_eql_Click(object sender, EventArgs e)
        {
            subbox_display.Text = "";
            switch (asmd_operator)
            {
                case "-":
                    txtBxResult.Text = (result - double.Parse(txtBxResult.Text)).ToString();
                    break;
                case "+":
                    txtBxResult.Text = (result + double.Parse(txtBxResult.Text)).ToString();
                    break;
                case "×":
                    txtBxResult.Text = (result * double.Parse(txtBxResult.Text)).ToString();
                    break;
                case "÷":
                    txtBxResult.Text = (result / double.Parse(txtBxResult.Text)).ToString();
                    break;
                default:
                    break;
            }
            result = double.Parse(txtBxResult.Text);
            asmd_operator = "";
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBxResult.Text = "0";
            subbox_display.Text = "";
            result = 0;
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            txtBxResult.Text = "0";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtBxResult.Text.Length > 0)
            {
                txtBxResult.Text = txtBxResult.Text.Remove(txtBxResult.Text.Length - 1, 1);
            }
            if (txtBxResult.Text == "")
            {
                txtBxResult.Text = "0";
            }
        }

        private void btnMStore_Click(object sender, EventArgs e)
        {
            btnMClearAll.Enabled = true;
            btnMRecall.Enabled = true;
            btnMAdd.Enabled = true;
            btnMSub.Enabled = true;
            calculator.Store(Double.Parse(txtBxResult.Text));
            createMemory();
        }

        private void btnMClearAll_Click(object sender, EventArgs e)
        {
            btnMClearAll.Enabled = false;
            btnMRecall.Enabled = false;
            btnMAdd.Enabled = false;
            btnMSub.Enabled = false;
            calculator.ClearAllMemory();
            flowLPMemories.Controls.Clear();
        }

        private void button_Mouseclick(object sender, MouseEventArgs e)
        {
            Control parent = ((Button)sender).Parent;
            int index = panels.IndexOf((TableLayoutPanel)parent);
            
            switch(((Button)sender).Text)
            {
                case "MC":
                    calculator.ClearMemoryItem(index);
                    //createMemory();
                    if(calculator.Memories.Count == 0)
                    {
                        btnMClearAll.Enabled = false;
                        btnMRecall.Enabled = false;
                        btnMAdd.Enabled = false;
                        btnMSub.Enabled = false;
                    }
                    break;
                case "M+":
                    calculator.AddToMemoryItem(index, Double.Parse(txtBxResult.Text));
                    foreach (Control child in parent.Controls)
                    {
                        if (child is Label)
                        {
                            child.Text = calculator.Memories.ElementAt(index).Value.ToString();
                        }
                    }
                    break;
                case "M-":
                    calculator.SubFromMemoryItem(index, Double.Parse(txtBxResult.Text));
                    foreach (Control child in parent.Controls)
                    {
                        if (child is Label)
                        {
                            child.Text = calculator.Memories.ElementAt(index).Value.ToString();
                        }
                    }
                    break;
                default:
                    break;
                        
            }     

        }
  
        private void createMemory()
        {
            foreach (TableLayoutPanel panel in panels)
            {
                flowLPMemories.Controls.Remove(panel);
            };
            panels = new List<TableLayoutPanel>();
            
            foreach (Memory memory in calculator.Memories)
            {

                Label lblMemory = new Label()
                {
                    Text = memory.Value.ToString(),
                    AutoSize = true,
                    Anchor = AnchorStyles.Right,
                    Font = new Font(FontFamily.GenericSansSerif, 16)

                };
                Button clearBtn = new Button()
                {
                    Text = "MC",
                    Size = new Size(45, 30),
                };
                Button addBtn = new Button()
                {
                    Text = "M+",
                    Size = new Size(45, 30),
                };
                Button subBtn = new Button()
                {
                    Text = "M-",
                    Size = new Size(45, 30),
                };
                TableLayoutPanel panel = new TableLayoutPanel()
                {
                    Size = new Size(230, 60),
                    ColumnCount = 3,
                    RowCount = 2
                };

                addBtn.MouseClick += button_Mouseclick;
                subBtn.MouseClick += button_Mouseclick;
                clearBtn.MouseClick += button_Mouseclick;

                panel.Controls.Add(lblMemory, 0, 0);
                panel.Controls.Add(clearBtn, 0, 1);
                panel.Controls.Add(addBtn, 1, 1);
                panel.Controls.Add(subBtn, 2, 1);
                panel.SetColumnSpan(lblMemory, 3);
                panels.Add(panel);
                flowLPMemories.Controls.Add(panel);
            }
        }

        private void btnMemOperation_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnMAdd":
                  
                    calculator.AddToMemoryItem(0, Double.Parse(txtBxResult.Text));

                    foreach (Control child in panels.First().Controls)
                    {
                        if (child is Label)
                            child.Text = calculator.Memories.First().Value.ToString();
                    }
                    break;
                case "btnMSub":
                    calculator.SubFromMemoryItem(0, Double.Parse(txtBxResult.Text));
                    foreach (Control child in panels.First().Controls)
                    {
                        if (child is Label)
                            child.Text = calculator.Memories.First().Value.ToString();
                    }
                    break;
                case "btnMRecall":
                    txtBxResult.Text = calculator.RecallMemory().ToString();
                    break;
                default:
                    break;
            }
    }

        private void btn_frac_Click(object sender, EventArgs e)
        {
            if (txtBxResult.Text.IndexOf('.') == -1)
            {
                txtBxResult.Text = txtBxResult.Text + '.';
            }
            else
            {
                var charToRemove = new string[] { "." };
                foreach (var dot in charToRemove)
                {
                    txtBxResult.Text = txtBxResult.Text.Replace(dot, string.Empty);
                }
                txtBxResult.Text = txtBxResult.Text + '.';
            }
        }
    }
}

