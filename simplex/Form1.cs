using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace simplex
{
    public partial class Form1 : Form
    {

        private TextBox[,] textBoxArray;  // Массив для хранения текстбоксов матрицы

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "3";
            textBox2.Text = "3";
            GenerateMatrix();
        }

        private void GenerateMatrix()
        {
            int rows, cols;
            if (int.TryParse(textBox1.Text, out cols) && int.TryParse(textBox2.Text, out rows))
            {
                panel1.Controls.Clear();

                textBoxArray = new TextBox[rows + 1, cols + 1];

                int textBoxWidth = 50;
                int textBoxHeight = 30;
                int paddingHorizont = 40; 


                for (int i = 0; i < rows + 1; i++)
                {
                    int textBoxCount = cols;

                    int totalRowWidth = textBoxCount * textBoxWidth + textBoxCount * paddingHorizont;

                    int startX = (panel1.Width - totalRowWidth) / 2;

                    if (i > 0)
                    {
                        for (int j = 0; j <= textBoxCount ; j++)
                        {
                            if (j == textBoxCount - 1)
                            {
                                Label lblX = new Label
                                {
                                    Width = 35,
                                    Height = 20,
                                    Text = "x" + (j + 1) + " <= ",
                                    Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont) + 55, 20 + i * (textBoxHeight))
                                };
                                panel1.Controls.Add(lblX);
                            }
                            if (j < textBoxCount)
                            {
                                Label lblX = new Label
                                {
                                    Width = 30,
                                    Height = 20,
                                    Text = "x" + (j + 1) + " + ",
                                    Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont) + 55, 20 + i * (textBoxHeight))
                                };
                                panel1.Controls.Add(lblX);
                            }

                                TextBox textBox = new TextBox
                                {
                                    Width = textBoxWidth,
                                    Height = textBoxHeight,
                                    Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont), 20 + i * (textBoxHeight)),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    BackColor = Color.LavenderBlush
                                };

                                panel1.Controls.Add(textBox);
                                textBoxArray[i, j] = textBox;

                            
                        }
                    }
                    else
                    {
                        for (int j = 0; j < textBoxCount; j++)
                        {

                            TextBox textBox = new TextBox
                            {
                                Width = textBoxWidth,
                                Height = textBoxHeight,
                                Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont), i * (textBoxHeight)),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.LavenderBlush
                            };

                            if (j == textBoxCount - 1)
                            {
                                Label lblX = new Label
                                {
                                    Width = 35,
                                    Height = 20,
                                    Text = "x" + (j + 1),
                                    Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont) + 55,  i * (textBoxHeight))
                                };
                                panel1.Controls.Add(lblX);
                            }
                            if (j < textBoxCount-1)
                            {
                                Label lblX = new Label
                                {
                                    Width = 30,
                                    Height = 20,
                                    Text = "x" + (j + 1) + " + ",
                                    Location = new System.Drawing.Point(startX + j * (textBoxWidth + paddingHorizont) + 55,  i * (textBoxHeight))
                                };
                                panel1.Controls.Add(lblX);
                            }

                            panel1.Controls.Add(textBox);
                            textBoxArray[i, j] = textBox;
                        }
                    }
                }
                Label lblOgran = new Label
                {
                    Width = 200,
                    Height = 30,
                    Text = "Ограничения: ",
                    Location = new System.Drawing.Point(500, 25),
                    Font = new Font("Times New Roman", 16)
                };
                panel1.Controls.Add(lblOgran);

                panel1.AutoSize = true;
            }
            else
            {
                panel1.Controls.Clear();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox1.Text) > 9)
                {
                    textBox1.Text = "9";
                }
                else
                {
                    GenerateMatrix();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            };
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox2.Text) > 9)
                {
                    textBox2.Text = "9";
                }
                else
                {
                    GenerateMatrix();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            };
        }
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Solve();
        }
         private void Solve()
        {
            try
            {
                double[,] table = ReadMatrixValues();
                double[] result = new double[int.Parse(textBox1.Text)];
                double[,] table_result;
                Simplex S = new Simplex(table);
                table_result = S.Calculate(result);

                int[] function = new int[int.Parse(textBox1.Text)];
                for (int i = 0; i < int.Parse(textBox1.Text); i++)
                {
                    function[i] = int.Parse(textBoxArray[0, i].Text);
                }

                double F = 0;
                for (int i = 0; i < result.Length; i++)
                {
                    F += function[i] * result[i];
                }

                /*
                    Console.WriteLine("Решенная симплекс-таблица:");
                    for (int i = 0; i < table_result.GetLength(0); i++)
                    {
                        for (int j = 0; j < table_result.GetLength(1); j++)
                            Console.Write(table_result[i, j] + " ");
                        Console.WriteLine();
                    }
                 */

                if (S.IsUnbounded())
                {
                    label3.Text = "Оптимальное решение не существует: функция неограниченна";
                }
                else
                {
                    label3.Text = ("Решение: \n");
                    for (int i = 0; i < result.Length; i++)
                    {
                        int num = i + 1;
                        label3.Text += "x" + num + " = " + (Math.Round(result[i], 3) + "\n");
                    }
                    label3.Text += "Целевая функция = " + Math.Round(F, 3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }



        }

        private double[,] ReadMatrixValues()
        {
            int rows = textBoxArray.GetLength(0);
            int cols = textBoxArray.GetLength(1);
            double[,] matrixValues = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (textBoxArray[i, j] != null)
                    {
                        if (double.TryParse(textBoxArray[i, j].Text, out double result))
                        {
                            matrixValues[i, j] = result;
                        }
                        else
                        {
                            MessageBox.Show($"Некорректное значение в ячейке ({i + 1}, {j + 1}). Введите целое число.");
                        }
                    }
                }
            }

            double[] firstRowTransformed = new double[cols];
            for (int j = 0; j < cols; j++)
            {
                firstRowTransformed[j] = -matrixValues[0, j]; 
            }

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrixValues[i, j] = matrixValues[i + 1, j]; 
                }
            }

            for (int j = 0; j < cols; j++)
            {
                matrixValues[rows - 1, j] = firstRowTransformed[j];
            }


            for (int i = 0; i < rows; i++)
            {
                double lastElement = matrixValues[i, cols - 1]; 
                for (int j = cols - 1; j > 0; j--)
                {
                    matrixValues[i, j] = matrixValues[i, j - 1]; 
                }
                matrixValues[i, 0] = lastElement; 
            }



            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols - 1; j++)
                {
                    Console.Write(matrixValues[i, j] + " ");
                }
                Console.Write("\n");
            }

            return matrixValues;
        }



        public class Simplex
        {
            double[,] table; // Симплекс-таблица
            int m, n;
            List<int> basis; // Список базисных переменных

            public Simplex(double[,] source)
            {
                m = source.GetLength(0);
                n = source.GetLength(1);
                table = new double[m, n + m - 1];
                basis = new List<int>();

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < table.GetLength(1); j++)
                    {
                        if (j < n)
                            table[i, j] = source[i, j];
                        else
                            table[i, j] = 0;
                    }
                    // Установка коэффициента 1 перед базисной переменной
                    if ((n + i) < table.GetLength(1))
                    {
                        table[i, n + i] = 1;
                        basis.Add(n + i);
                    }
                }

                n = table.GetLength(1);
            }

            // Метод для вычисления результата
            public double[,] Calculate(double[] result)
            {
                int mainCol, mainRow; // Ведущие столбец и строка

                while (!IsItEnd())
                {
                    mainCol = findMainCol();

                    // Проверка на неограниченность
                    if (IsUnbounded(mainCol))
                    {
                        // Возвращаем null, если решение неограниченно
                        return null;
                    }

                    mainRow = findMainRow(mainCol);
                    basis[mainRow] = mainCol;

                    double[,] new_table = new double[m, n];

                    // Обновление симплекс-таблицы
                    for (int j = 0; j < n; j++)
                        new_table[mainRow, j] = table[mainRow, j] / table[mainRow, mainCol];

                    for (int i = 0; i < m; i++)
                    {
                        if (i == mainRow)
                            continue;

                        for (int j = 0; j < n; j++)
                            new_table[i, j] = table[i, j] - table[i, mainCol] * new_table[mainRow, j];
                    }
                    table = new_table;
                }

                // Заносим в result найденные значения X
                for (int i = 0; i < result.Length; i++)
                {
                    int k = basis.IndexOf(i + 1);
                    if (k != -1)
                        result[i] = table[k, 0];
                    else
                        result[i] = 0;
                }

                return table;
            }

            // Метод для проверки на неограниченность
            public bool IsUnbounded()
            {
                int mainCol = findMainCol();
                return IsUnbounded(mainCol); // Вызов внутреннего метода для проверки
            }

            private bool IsItEnd()
            {
                // Проверяем, если в последней строке (стоимости) есть отрицательные значения
                for (int j = 1; j < n; j++)
                {
                    if (table[m - 1, j] < 0)
                    {
                        return false; // Продолжаем, если есть отрицательные значения
                    }
                }
                return true;
            }

            private int findMainCol()
            {
                int mainCol = 1;
                for (int j = 2; j < n; j++)
                {
                    if (table[m - 1, j] < table[m - 1, mainCol])
                        mainCol = j;
                }
                return mainCol;
            }

            private int findMainRow(int mainCol)
            {
                int mainRow = -1;

                for (int i = 0; i < m - 1; i++)
                {
                    if (table[i, mainCol] > 0)
                    {
                        if (mainRow == -1)
                            mainRow = i;
                        else if ((table[i, 0] / table[i, mainCol]) < (table[mainRow, 0] / table[mainRow, mainCol]))
                            mainRow = i;
                    }
                }

                return mainRow;
            }

            // Внутренний метод для проверки неограниченности
            private bool IsUnbounded(int mainCol)
            {
                // Проверка на неограниченность: если во всем столбце ведущей переменной нет положительных значений, то функция неограниченна
                for (int i = 0; i < m - 1; i++)
                {
                    if (table[i, mainCol] > 0)
                        return false; // Есть положительное значение, функция ограничена
                }
                return true; // Все значения в столбце отрицательные или нулевые, функция неограниченна
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl  in panel1.Controls )
            {
               if( ctrl is TextBox textbox)
                {
                    textbox.Text = string.Empty;   
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            switch(comboBox1.SelectedIndex)
            {

                case 0:
                   
                    textBox1.Text = "2";
                    textBox2.Text = "2";
                    var case0 = new[,] { {2,3,0}, {1,2,8}, {2,1,6} };
                    GenerateMatrix();

                    if (textBoxArray == null)
                    {
                        MessageBox.Show("Ошибка: массив TextBox не создан.");
                        return;
                    }

                    for (int i = 0; i < int.Parse(textBox2.Text) + 1; i++)
                    {
                        for (int j = 0; j < int.Parse(textBox1.Text) + 1; j++)
                        {
                            if (textBoxArray[i, j] != null) 
                            {
                                textBoxArray[i, j].Text = case0[i, j].ToString();
                            }
                           
                        }
                    }
                    Solve();
                    break;

                    case 1:
                    textBox1.Text = "3";
                    textBox2.Text = "3";
                    var case1 = new[,] { { 4, 5, 4, 0 }, { 2, 3, 6, 240 }, { 4, 2, 4, 200 }, { 4, 6, 8, 160 } };
                    GenerateMatrix();

                    if (textBoxArray == null)
                    {
                        MessageBox.Show("Ошибка: массив TextBox не создан.");
                        return;
                    }

                    for (int i = 0; i < int.Parse(textBox2.Text) + 1; i++)
                    {
                        for (int j = 0; j < int.Parse(textBox1.Text) + 1; j++)
                        {
                            if (textBoxArray[i, j] != null)
                            {
                                textBoxArray[i, j].Text = case1[i, j].ToString();
                            }

                        }
                    }
                    Solve();
                    break;

                    case 2:
                    textBox1.Text = "3";
                    textBox2.Text = "3";
                    var case2 = new[,] { { 4, 5, 4, 0 }, { 2, 3, -6, 240 }, { 4, 2,-4, 200 }, { 4, 6, -8, 160 } };
                    GenerateMatrix();

                    if (textBoxArray == null)
                    {
                        MessageBox.Show("Ошибка: массив TextBox не создан.");
                        return;
                    }

                    for (int i = 0; i < int.Parse(textBox2.Text) + 1; i++)
                    {
                        for (int j = 0; j < int.Parse(textBox1.Text) + 1; j++)
                        {
                            if (textBoxArray[i, j] != null)
                            {
                                textBoxArray[i, j].Text = case2[i, j].ToString();
                            }

                        }
                    }
                    Solve();
                    break;
            }


        }
    }
}
