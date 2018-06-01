namespace Diplom
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Voice_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Text_Button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Tools = new System.Windows.Forms.Button();
            this.Help = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(720, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(727, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Голосовой ввод";
            // 
            // Voice_button
            // 
            this.Voice_button.Location = new System.Drawing.Point(730, 46);
            this.Voice_button.Name = "Voice_button";
            this.Voice_button.Size = new System.Drawing.Size(147, 36);
            this.Voice_button.TabIndex = 2;
            this.Voice_button.Text = "Надиктовать";
            this.Voice_button.UseVisualStyleBackColor = true;
            this.Voice_button.Click += new System.EventHandler(this.Voice_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(727, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Текст";
            // 
            // Text_Button
            // 
            this.Text_Button.Location = new System.Drawing.Point(730, 121);
            this.Text_Button.Name = "Text_Button";
            this.Text_Button.Size = new System.Drawing.Size(147, 36);
            this.Text_Button.TabIndex = 5;
            this.Text_Button.Text = "Выбрать файл";
            this.Text_Button.UseVisualStyleBackColor = true;
            this.Text_Button.Click += new System.EventHandler(this.Text_Button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Tools
            // 
            this.Tools.Location = new System.Drawing.Point(730, 298);
            this.Tools.Name = "Tools";
            this.Tools.Size = new System.Drawing.Size(147, 36);
            this.Tools.TabIndex = 6;
            this.Tools.Text = "Найстроки";
            this.Tools.UseVisualStyleBackColor = true;
            this.Tools.Click += new System.EventHandler(this.Tools_Click);
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(730, 354);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(147, 36);
            this.Help.TabIndex = 7;
            this.Help.Text = "Справка";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(730, 189);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(147, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(730, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Прогресс";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(884, 402);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.Tools);
            this.Controls.Add(this.Text_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Voice_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Voice_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Text_Button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Tools;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
    }
}

