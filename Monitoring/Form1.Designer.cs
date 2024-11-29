namespace Monitoring
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            richTextBox2 = new RichTextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            richTextBox1 = new RichTextBox();
            tabPage2 = new TabPage();
            label4 = new Label();
            comboBox1 = new ComboBox();
            tabPage3 = new TabPage();
            textBox1 = new TextBox();
            label5 = new Label();
            checkBox4 = new CheckBox();
            button1 = new Button();
            checkBox5 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 19);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 56);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 1;
            label2.Text = "label1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 116);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 1;
            label3.Text = "label1";
            // 
            // richTextBox2
            // 
            richTextBox2.BackColor = Color.FloralWhite;
            richTextBox2.Location = new Point(296, 42);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(494, 363);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            tabControl1.Location = new Point(0, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(818, 447);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Ivory;
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(810, 414);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Аппаратная часть";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.Ivory;
            richTextBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(6, 6);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(798, 405);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Ivory;
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(richTextBox2);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(810, 414);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Мониторинг системы";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(435, 14);
            label4.Name = "label4";
            label4.Size = new Size(124, 20);
            label4.TabIndex = 3;
            label4.Text = "Сортировать по:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "", "Потребляемой памяти", "Загрузке процессора " });
            comboBox1.Location = new Point(565, 11);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(225, 28);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.TextChanged += comboBox1_TextChanged;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.Ivory;
            tabPage3.Controls.Add(textBox1);
            tabPage3.Controls.Add(label5);
            tabPage3.Controls.Add(checkBox4);
            tabPage3.Controls.Add(button1);
            tabPage3.Controls.Add(checkBox5);
            tabPage3.Controls.Add(checkBox3);
            tabPage3.Controls.Add(checkBox2);
            tabPage3.Controls.Add(checkBox1);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(810, 414);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Логирование";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(482, 54);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 27);
            textBox1.TabIndex = 4;
            textBox1.Enter += textBox1_Enter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(397, 54);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 3;
            label5.Text = "Интервал:";
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(406, 14);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(164, 24);
            checkBox4.TabIndex = 2;
            checkBox4.Text = "Логирование: Выкл";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(366, 315);
            button1.Name = "button1";
            button1.Size = new Size(110, 58);
            button1.TabIndex = 1;
            button1.Text = "Открыть логи";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox5.Location = new Point(8, 109);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(232, 29);
            checkBox5.TabIndex = 0;
            checkBox5.Text = "Запущенные процессы";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox3.Location = new Point(8, 74);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(149, 29);
            checkBox3.TabIndex = 0;
            checkBox3.Text = "Жёсткий диск";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = CheckState.Checked;
            checkBox2.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox2.Location = new Point(8, 44);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(214, 29);
            checkBox2.TabIndex = 0;
            checkBox2.Text = "Оперативная память";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(8, 9);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(128, 29);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Процессор";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(820, 453);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private RichTextBox richTextBox2;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private RichTextBox richTextBox1;
        private TabPage tabPage3;
        private Label label4;
        private ComboBox comboBox1;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private CheckBox checkBox5;
        private Button button1;
        private CheckBox checkBox4;
        private TextBox textBox1;
        private Label label5;
    }
}
