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
        private bool isOperationClicked = false;
        private bool isOperationEqualClicked = false;
        private bool isOperationClearClicked = false;
        private bool isOperationClearEntryClicked = false;
        private bool isOperationStoreClicked = false;
        private bool isMemoryOperationClicked = false;
        private List<TableLayoutPanel> panels;
        private List<String> subbox;

        public Form1()
        {
            InitializeComponent();
            calculator = new CalculatorBase();
            panels = new List<TableLayoutPanel>();
            subbox = new List<string>();
            btnMClearAll.Enabled = false;
            btnMRecall.Enabled = false;
            btnMAdd.Enabled = false;
            btnMSub.Enabled = false;
        }

        private void btnNum_Click(object sender, EventArgs e)
        {
            if (!isOperationClicked & !isOperationClearClicked & !isOperationClearEntryClicked & !isOperationEqualClicked & !isMemoryOperationClicked)
            {
                if (txtBxResult.Text.Equals("0"))
                {
                    txtBxResult.Text = ((Button)sender).Text;
                    calculator.Num2 = Double.Parse(txtBxResult.Text);
                }
                else
                {
                    txtBxResult.Text += ((Button)sender).Text;
                    calculator.Num2 = Double.Parse(txtBxResult.Text);
                }
            }
            else if ((isOperationEqualClicked & isOperationClearEntryClicked & !isOperationClicked & !isMemoryOperationClicked) | (isOperationClearClicked & !isOperationClicked & !isMemoryOperationClicked))
            {
                if (txtBxResult.Text.Equals("0"))
                {
                    txtBxResult.Text = ((Button)sender).Text;
                    calculator.Num2 = Double.Parse(txtBxResult.Text);
                }
                else
                {
                    txtBxResult.Text += ((Button)sender).Text;
                    calculator.Num2 = Double.Parse(txtBxResult.Text);
                }
                calculator.Num1 = null;
                Console.WriteLine("---------- " + calculator.Num1 + " num2 " + calculator.Num2);
            }
            else
            {
                txtBxResult.Text = ((Button)sender).Text;
                calculator.Num2 = Double.Parse(txtBxResult.Text);
            }
            isOperationClicked = false;
            isOperationClearClicked = false;
            isOperationClearEntryClicked = false;
            isOperationEqualClicked = false;
            isOperationStoreClicked = false;
            isMemoryOperationClicked = false;
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            Console.WriteLine(calculator.Num1 + " num2 " + calculator.Num2);
            if (isOperationEqualClicked)
            {
                isOperationEqualClicked = false;
                calculator.Num2 = calculator.Num1;
            }
            else if (!isOperationClicked | isOperationStoreClicked)
            {
                if (calculator.Num1 != null & calculator.Num2 != null)
                {
                    subbox.Add(calculator.Num2.ToString());
                    txtBxDetails.Text = "";
                    subbox.ForEach(x => txtBxDetails.Text += x);
                    
                    switch (calculator.Operation)
                    {
                        case "btnAdd":
                            calculator.AddNums();
                            break;
                        case "btnSub":
                            calculator.SubNums();
                            break;
                        case "btnMult":
                            calculator.MultNums();
                            break;
                        case "btnDiv":
                            calculator.DivNums();
                            break;
                        default:
                            break;
                    }
                    
                    txtBxResult.Text = calculator.Num1.ToString();
                    calculator.Num2 = null;
                    
                }
                else if (calculator.Num1 != null & calculator.Num2 == null) { }
                else
                {
                    calculator.Num1 = calculator.Num2;
                    calculator.Num2 = null;
                    subbox.Add(calculator.Num1.ToString());
                    txtBxDetails.Text = "";
                    subbox.ForEach(x => txtBxDetails.Text += x);
                }
            }
            

            calculator.Operation = ((Button)sender).Name;
            switch (calculator.Operation)
            {
                case "btnAdd":
                    subbox.Add("+");
                    break;
                case "btnSub":
                    subbox.Add("-");
                    break;
                case "btnMult":
                    subbox.Add("×");
                    break;
                case "btnDiv":
                    subbox.Add("÷");
                    break;
                default:
                    break;
            }

            txtBxDetails.Text = "";
            subbox.ForEach(x => txtBxDetails.Text += x);
            isOperationClicked = true;
            Console.WriteLine(calculator.Num1 + " num2 " + calculator.Num2);
            isOperationStoreClicked = false;
        }

        private void btnOperationEqual_Click(object sender, EventArgs e)
        {
            
            if (calculator.Num1 == null)
                calculator.Num1 = 0;
            if (isOperationClicked & !isOperationStoreClicked)
            {
                isOperationClicked = false;
                calculator.Num2 = calculator.Num1;
            }
            subbox.Clear();
            subbox.Add(calculator.Num1.ToString());
            switch (calculator.Operation)
            {
                case "btnAdd":
                    subbox.Add("+");
                    break;
                case "btnSub":
                    subbox.Add("-");
                    break;
                case "btnMult":
                    subbox.Add("×");
                    break;
                case "btnDiv":
                    subbox.Add("÷");
                    break;
                default:
                    break;
            }
            subbox.Add(calculator.Num2.ToString());
            txtBxDetails.Text = "";
            subbox.ForEach(x => txtBxDetails.Text += x);
            switch (calculator.Operation)
            {
                case "btnAdd":
                    calculator.AddNums();
                    break;
                case "btnSub":
                    calculator.SubNums();
                    break;
                case "btnMult":
                    calculator.MultNums();
                    break;
                case "btnDiv":
                    calculator.DivNums();
                    break;
                default:
                    break;
            }

            txtBxResult.Text = calculator.Num1.ToString();
            isOperationEqualClicked = true;
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnClearEntry":
                    if (isOperationEqualClicked)
                    {
                        calculator.ClearAll();
                        txtBxDetails.Text = "";
                        subbox.Clear();
                        isOperationClearClicked = true;
                        isOperationEqualClicked = false;
                    }
                    else
                    {
                        calculator.ClearEntry();
                        isOperationClearEntryClicked = true;
                    }
                    break;
                case "btnClear":
                    calculator.ClearAll();
                    txtBxDetails.Text = "";
                    subbox.Clear();
                    isOperationClearClicked = true;
                    break;
                default:
                    break;
            }
            txtBxResult.Text = calculator.Num2.ToString();
        }

        private void btnMStore_Click(object sender, EventArgs e)
        {
            btnMClearAll.Enabled = true;
            btnMRecall.Enabled = true;
            btnMAdd.Enabled = true;
            btnMSub.Enabled = true;
            isOperationClicked = true;
            isOperationStoreClicked = true;
            calculator.Store(Double.Parse(txtBxResult.Text));
            createMemory();
        }

        private void button_Mouseclick(object sender, MouseEventArgs e)
        {
            Control parent = ((Button)sender).Parent;
            int index = panels.IndexOf((TableLayoutPanel)parent);
            
            switch(((Button)sender).Text)
            {
                case "MC":
                    calculator.ClearMemoryItem(index);
                    createMemory();
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

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Control child in ((TableLayoutPanel)sender).Controls)
            {
                if (child is Label)
                {
                    txtBxResult.Text = child.Text;
                    calculator.Num2 = Double.Parse(txtBxResult.Text);
                    Console.WriteLine("****" + calculator.Num1 + " num2 " + calculator.Num2);
                    isOperationClicked = false;
                    isOperationClearClicked = false;
                    isOperationClearEntryClicked = false;
                    isOperationEqualClicked = false;
                    isOperationStoreClicked = false;
                }
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
                panel.MouseClick += panel_MouseClick;

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
            isMemoryOperationClicked = true;
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
    }
}

