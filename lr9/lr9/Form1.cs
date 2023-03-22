using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LR8lib;
using Rectangle = LR8lib.Rectangle;

namespace lr9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            Pen pen = new Pen(Color.Black, 5);
            Init.bitmap = bitmap;
            Init.pictureBox = pictureBox1;
            Init.pen = pen;
            Init.listBox = listBox1;
        }
        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();
        private ShapeContainer shapeContainer = new ShapeContainer();
        private bool IsNotOperation(char item)
        {
            if (!(item == 'S' || item == 'M' || item == 'D' || item == ',' || item == '(' || item == ')' || item==' '))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int ConvertCharToInt(char a)
        {
            return Convert.ToInt32(a.ToString());

        }
        private int ConvertCharToInt(object a)
        {
            return Convert.ToInt32(a.ToString());
        }
        private void SelectingPerformingOperation(Operator op)
        {
            if (op.symbolOperator == 'S')
            {
                Square figure = new Square(Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToInt32
                (Convert.ToString(operands.Pop().value)), Convert.ToString(operands.Pop().value));
                ShapeContainer.AddFigure(figure);
                operators.Pop();
                listBox1.Items.Add(textBox1.Text + " выполнено");
                figure.Draw();
          }

            else if (op.symbolOperator == 'D')
            {
                string tempName = Convert.ToString(operands.Pop().value);
                foreach (Figure f in ShapeContainer.figureList)
                {

                    if (f.name == tempName)
                    {
                        f.DeleteF(f);
                    }
                }
              
                operators.Pop();
                listBox1.Items.Add(textBox1.Text + " выполнено");
            }
            else if (op.symbolOperator == 'M')
            {
                string tempName = Convert.ToString(operands.Pop().value);
                foreach (Figure f in ShapeContainer.figureList)
                {

                    if (f.name == tempName)
                    {
                        f.MoveTo(Convert.ToInt32(Convert.ToString(operands.Pop().value)), (Convert.ToInt32
                 (Convert.ToString(operands.Pop().value))));
                    }
                }
                operators.Pop();
                listBox1.Items.Add(textBox1.Text + " выполнено");
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool flag = true;
                try
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (IsNotOperation(textBox1.Text[i]))
                        {

                            if (!(Char.IsDigit(textBox1.Text[i])))
                            {
                                if (((textBox1.Text[i + 1] == ',') && ((textBox1.Text[i - 1] == '(') || (textBox1.Text[i - 1] == ' ')))
                                    || ((textBox1.Text[i + 1] == ')') && ((textBox1.Text[i - 1] == ',') || (textBox1.Text[i - 1] == ' ') || (textBox1.Text[i-1]=='('))))
                                {
                                    this.operands.Push(new Operand(Convert.ToString(textBox1.Text[i])));
                                    continue;
                                }
                                else if (!(Char.IsDigit(textBox1.Text[i + 1])) && ((textBox1.Text[i + 1] != ',') && (textBox1.Text[i + 1] != ')')))
                                {
                                    if (flag)
                                        this.operands.Push(new Operand(textBox1.Text[i]));
                                    this.operands.Push(new Operand(Convert.ToString(this.operands.Pop().value) + Convert.ToString(textBox1.Text[i + 1])));
                                    flag = false;
                                    continue;
                                }

                            }

                            else if (Char.IsDigit(textBox1.Text[i]))
                            {
                                if (Char.IsDigit(textBox1.Text[i + 1]))
                                {
                                    if (flag)
                                        this.operands.Push(new Operand(textBox1.Text[i]));
                                    this.operands.Push(new Operand(ConvertCharToInt(this.operands.Pop().value) * 10 + ConvertCharToInt(textBox1.Text[i + 1])));
                                    flag = false;
                                    continue;
                                }
                                else if ((textBox1.Text[i + 1] == ','
                                || textBox1.Text[i + 1] == ')')
                                && !(Char.IsDigit(textBox1.Text[i - 1])))
                                {
                                    this.operands.Push(new Operand(ConvertCharToInt(textBox1.Text[i])));
                                    continue;
                                }
                            }
                        }
                        else if (textBox1.Text[i] == 'S')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator
                                (textBox1.Text[i]));
                            }
                        }
                        else if (textBox1.Text[i] == 'M')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator
                                (textBox1.Text[i]));
                            }
                        }
                        else if (textBox1.Text[i] == 'D')
                        {
                            if (this.operators.Count == 0)
                            {
                                this.operators.Push(OperatorContainer.FindOperator
                                (textBox1.Text[i]));
                            }
                        }
                        else if (textBox1.Text[i] == ',')
                        {
                            flag = true;
                        }
                        else if (textBox1.Text[i] == '(')
                        {
                            this.operators.Push(OperatorContainer.FindOperator
                            (textBox1.Text[i]));
                        }
                        else if (textBox1.Text[i] == ')')
                        {
                            do
                            {
                                if (operators.Peek().symbolOperator == '(')
                                {
                                    operators.Pop();
                                    break;
                                }
                                if (operators.Count == 0)
                                {
                                    break;
                                }
                            }
                            while (operators.Peek().symbolOperator != '(');
                        }
                    }
                    if (operators.Peek() != null)
                    {
                        this.SelectingPerformingOperation(operators.Peek());
                    }
                    else
                    {
                        MessageBox.Show("Введенной операции не существует");
                    }
                }
                catch (InvalidOperationException)
                {
                    listBox1.Items.Add(textBox1.Text + " выполнено");
                    for (int i=0; i < operators.Count; i++)
                    {
                        operators.Pop();
                    }

                }
                catch
                {
                    listBox1.Items.Add(textBox1.Text + " не выполнено");
                    for (int i = 0; i < operators.Count; i++)
                    {
                        operators.Pop();
                    }
                    for (int i = 0; i < operands.Count; i++)
                    {
                        operands.Pop();
                    }
                }
            }
        }
    }
}
