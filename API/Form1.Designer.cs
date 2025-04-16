namespace API
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NameCityTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.CityTemp = new System.Windows.Forms.Label();
            this.cityRain = new System.Windows.Forms.Label();
            this.cityWeather = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Ink Free", 27.84906F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(342, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путеводитель";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(332, 36);
            this.label2.TabIndex = 0;
            this.label2.Text = "Введите  желаемый город";
            // 
            // NameCityTxt
            // 
            this.NameCityTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "Москва"});
            this.NameCityTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.NameCityTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.NameCityTxt.BackColor = System.Drawing.Color.SkyBlue;
            this.NameCityTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameCityTxt.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameCityTxt.Location = new System.Drawing.Point(379, 91);
            this.NameCityTxt.Name = "NameCityTxt";
            this.NameCityTxt.Size = new System.Drawing.Size(379, 36);
            this.NameCityTxt.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(797, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Поиск";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CityTemp
            // 
            this.CityTemp.AutoSize = true;
            this.CityTemp.BackColor = System.Drawing.Color.Transparent;
            this.CityTemp.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CityTemp.ForeColor = System.Drawing.Color.White;
            this.CityTemp.Location = new System.Drawing.Point(74, 146);
            this.CityTemp.Name = "CityTemp";
            this.CityTemp.Size = new System.Drawing.Size(183, 36);
            this.CityTemp.TabIndex = 0;
            this.CityTemp.Text = "Температура:";
            // 
            // cityRain
            // 
            this.cityRain.AutoSize = true;
            this.cityRain.BackColor = System.Drawing.Color.Transparent;
            this.cityRain.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cityRain.ForeColor = System.Drawing.Color.White;
            this.cityRain.Location = new System.Drawing.Point(358, 146);
            this.cityRain.Name = "cityRain";
            this.cityRain.Size = new System.Drawing.Size(279, 36);
            this.cityRain.TabIndex = 0;
            this.cityRain.Text = "Вероятность осадков:";
            // 
            // cityWeather
            // 
            this.cityWeather.AutoSize = true;
            this.cityWeather.BackColor = System.Drawing.Color.Transparent;
            this.cityWeather.Font = new System.Drawing.Font("Ink Free", 19.69811F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cityWeather.ForeColor = System.Drawing.Color.White;
            this.cityWeather.Location = new System.Drawing.Point(752, 146);
            this.cityWeather.Name = "cityWeather";
            this.cityWeather.Size = new System.Drawing.Size(228, 36);
            this.cityWeather.TabIndex = 0;
            this.cityWeather.Text = "Описание погоды:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.SkyBlue;
            this.checkedListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Музеи",
            "Исторические здания",
            "Достопримечательности",
            "Парки",
            "Театры"});
            this.checkedListBox1.Location = new System.Drawing.Point(880, 222);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(253, 142);
            this.checkedListBox1.TabIndex = 4;
            this.checkedListBox1.Leave += new System.EventHandler(this.checkedListBox1_Leave);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(1059, 43);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 31);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Ink Free", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(929, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Что вы ищите";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Ink Free", 16.30189F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(998, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "Количество";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(18, 202);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(846, 345);
            this.tableLayoutPanel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::API.Properties.Resources.фон;
            this.ClientSize = new System.Drawing.Size(1145, 559);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NameCityTxt);
            this.Controls.Add(this.cityWeather);
            this.Controls.Add(this.cityRain);
            this.Controls.Add(this.CityTemp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Путеводитель по городу";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameCityTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label CityTemp;
        private System.Windows.Forms.Label cityRain;
        private System.Windows.Forms.Label cityWeather;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    }
}

