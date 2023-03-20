using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private bool IsNotOperation(char item)
        {
            if (!(item == 'S' || item == ',' || item == '(' || item == ')'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (IsNotOperation(textBox1.Text[i]))
                        {

                        }
                    }

                        textBox1.Text = "Hello";
                    //выполняется обработка входной строки
                }
                catch
                {
                    //добавляется информация о некорректной команде в историю команд
                }
                textBox1.Text = "";
            }

        }
    }
}
