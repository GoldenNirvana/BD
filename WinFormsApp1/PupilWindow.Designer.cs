namespace WinFormsApp1
{
    partial class PupilWindow
    {
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
            this.checkMarks = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.addAnswer = new System.Windows.Forms.Button();
            this.raiting = new System.Windows.Forms.Button();
            this.addMonye = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkMarks
            // 
            this.checkMarks.Location = new System.Drawing.Point(12, 386);
            this.checkMarks.Name = "checkMarks";
            this.checkMarks.Size = new System.Drawing.Size(162, 52);
            this.checkMarks.TabIndex = 0;
            this.checkMarks.Text = "Посмотреть оценки";
            this.checkMarks.UseVisualStyleBackColor = true;
            this.checkMarks.Click += new System.EventHandler(this.checkMarks_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(776, 338);
            this.dataGridView1.TabIndex = 3;
            // 
            // addAnswer
            // 
            this.addAnswer.Location = new System.Drawing.Point(216, 386);
            this.addAnswer.Name = "addAnswer";
            this.addAnswer.Size = new System.Drawing.Size(162, 52);
            this.addAnswer.TabIndex = 4;
            this.addAnswer.Text = "Ответить на ДЗ";
            this.addAnswer.UseVisualStyleBackColor = true;
            this.addAnswer.Click += new System.EventHandler(this.addAnswer_Click);
            // 
            // raiting
            // 
            this.raiting.Location = new System.Drawing.Point(419, 386);
            this.raiting.Name = "raiting";
            this.raiting.Size = new System.Drawing.Size(162, 52);
            this.raiting.TabIndex = 5;
            this.raiting.Text = "Рейтинг";
            this.raiting.UseVisualStyleBackColor = true;
            this.raiting.Click += new System.EventHandler(this.raiting_Click);
            // 
            // addMonye
            // 
            this.addMonye.Location = new System.Drawing.Point(626, 386);
            this.addMonye.Name = "addMonye";
            this.addMonye.Size = new System.Drawing.Size(162, 52);
            this.addMonye.TabIndex = 6;
            this.addMonye.Text = "Пополнить счёт";
            this.addMonye.UseVisualStyleBackColor = true;
            this.addMonye.Click += new System.EventHandler(this.addMonye_Click);
            // 
            // PupilWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.addMonye);
            this.Controls.Add(this.raiting);
            this.Controls.Add(this.addAnswer);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.checkMarks);
            this.Name = "PupilWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PupilWindow";
            this.Load += new System.EventHandler(this.PupilWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView1;
        private Button checkMarks;
        private Button addAnswer;
        private Button raiting;
        private Button addMonye;
    }
}