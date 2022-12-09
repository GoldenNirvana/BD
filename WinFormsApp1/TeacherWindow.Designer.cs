namespace WinFormsApp1
{
    partial class TeacherWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addMark = new System.Windows.Forms.Button();
            this.addDZ = new System.Windows.Forms.Button();
            this.changeMark = new System.Windows.Forms.Button();
            this.classes = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.surnameBox = new System.Windows.Forms.TextBox();
            this.FatherBox = new System.Windows.Forms.TextBox();
            this.averadgeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(352, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(468, 272);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // addMark
            // 
            this.addMark.Location = new System.Drawing.Point(194, 386);
            this.addMark.Name = "addMark";
            this.addMark.Size = new System.Drawing.Size(132, 52);
            this.addMark.TabIndex = 1;
            this.addMark.Text = "Проверить ДЗ";
            this.addMark.UseVisualStyleBackColor = true;
            this.addMark.Click += new System.EventHandler(this.addMark_Click);
            // 
            // addDZ
            // 
            this.addDZ.Location = new System.Drawing.Point(34, 386);
            this.addDZ.Name = "addDZ";
            this.addDZ.Size = new System.Drawing.Size(132, 52);
            this.addDZ.TabIndex = 2;
            this.addDZ.Text = "Задать ДЗ";
            this.addDZ.UseVisualStyleBackColor = true;
            this.addDZ.Click += new System.EventHandler(this.addDZ_Click);
            // 
            // changeMark
            // 
            this.changeMark.Location = new System.Drawing.Point(34, 301);
            this.changeMark.Name = "changeMark";
            this.changeMark.Size = new System.Drawing.Size(132, 52);
            this.changeMark.TabIndex = 4;
            this.changeMark.Text = "Исправить оценку";
            this.changeMark.UseVisualStyleBackColor = true;
            this.changeMark.Click += new System.EventHandler(this.changeMark_Click);
            // 
            // classes
            // 
            this.classes.FormattingEnabled = true;
            this.classes.Location = new System.Drawing.Point(194, 12);
            this.classes.Name = "classes";
            this.classes.Size = new System.Drawing.Size(132, 28);
            this.classes.TabIndex = 6;
            this.classes.SelectedIndexChanged += new System.EventHandler(this.classes_SelectedIndexChanged);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(34, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(132, 28);
            this.button5.TabIndex = 7;
            this.button5.Text = "Выход";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.exit);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(34, 93);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(292, 27);
            this.nameBox.TabIndex = 8;
            // 
            // surnameBox
            // 
            this.surnameBox.Location = new System.Drawing.Point(34, 126);
            this.surnameBox.Name = "surnameBox";
            this.surnameBox.Size = new System.Drawing.Size(292, 27);
            this.surnameBox.TabIndex = 9;
            // 
            // FatherBox
            // 
            this.FatherBox.Location = new System.Drawing.Point(34, 159);
            this.FatherBox.Name = "FatherBox";
            this.FatherBox.Size = new System.Drawing.Size(292, 27);
            this.FatherBox.TabIndex = 10;
            // 
            // averadgeBox
            // 
            this.averadgeBox.Location = new System.Drawing.Point(201, 222);
            this.averadgeBox.Name = "averadgeBox";
            this.averadgeBox.Size = new System.Drawing.Size(125, 27);
            this.averadgeBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Средний балл";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(352, 301);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 29;
            this.dataGridView2.Size = new System.Drawing.Size(468, 137);
            this.dataGridView2.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(201, 314);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 14;
            // 
            // TeacherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 453);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.averadgeBox);
            this.Controls.Add(this.FatherBox);
            this.Controls.Add(this.surnameBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.classes);
            this.Controls.Add(this.changeMark);
            this.Controls.Add(this.addDZ);
            this.Controls.Add(this.addMark);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TeacherWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.TeacherWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private Button addMark;
        private Button addDZ;
        private Button changeMark;
        private ComboBox classes;
        private Button button5;
        private TextBox nameBox;
        private TextBox surnameBox;
        private TextBox FatherBox;
        private TextBox averadgeBox;
        private Label label1;
        private DataGridView dataGridView2;
        private TextBox textBox1;
    }
}