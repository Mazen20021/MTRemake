
namespace MTRemake
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SignUp = new System.Windows.Forms.Label();
            this.Loginbot = new System.Windows.Forms.Button();
            this.BinText = new System.Windows.Forms.TextBox();
            this.Emailtext = new System.Windows.Forms.TextBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.Version_text = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(34)))), ((int)(((byte)(73)))));
            this.panel1.Controls.Add(this.Version_text);
            this.panel1.Controls.Add(this.SignUp);
            this.panel1.Controls.Add(this.Loginbot);
            this.panel1.Controls.Add(this.BinText);
            this.panel1.Controls.Add(this.Emailtext);
            this.panel1.Controls.Add(this.Logo);
            this.panel1.Location = new System.Drawing.Point(-8, -32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 492);
            this.panel1.TabIndex = 0;
            // 
            // SignUp
            // 
            this.SignUp.AutoSize = true;
            this.SignUp.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignUp.ForeColor = System.Drawing.Color.White;
            this.SignUp.Location = new System.Drawing.Point(212, 378);
            this.SignUp.Name = "SignUp";
            this.SignUp.Size = new System.Drawing.Size(204, 23);
            this.SignUp.TabIndex = 4;
            this.SignUp.Text = "I don\'t have an account ";
            this.SignUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // Loginbot
            // 
            this.Loginbot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Loginbot.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loginbot.ForeColor = System.Drawing.Color.White;
            this.Loginbot.Location = new System.Drawing.Point(273, 299);
            this.Loginbot.Name = "Loginbot";
            this.Loginbot.Size = new System.Drawing.Size(75, 32);
            this.Loginbot.TabIndex = 3;
            this.Loginbot.Text = "LogIn";
            this.Loginbot.UseVisualStyleBackColor = true;
            this.Loginbot.Click += new System.EventHandler(this.button1_Click);
            // 
            // BinText
            // 
            this.BinText.Location = new System.Drawing.Point(205, 256);
            this.BinText.MaxLength = 5000;
            this.BinText.Name = "BinText";
            this.BinText.Size = new System.Drawing.Size(211, 20);
            this.BinText.TabIndex = 2;
            this.BinText.Text = "Password/Bin";
            // 
            // Emailtext
            // 
            this.Emailtext.BackColor = System.Drawing.Color.White;
            this.Emailtext.Location = new System.Drawing.Point(205, 230);
            this.Emailtext.Name = "Emailtext";
            this.Emailtext.Size = new System.Drawing.Size(211, 20);
            this.Emailtext.TabIndex = 1;
            this.Emailtext.Text = "Email/UserName";
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.Transparent;
            this.Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Logo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Logo.Image = global::MTRemake.Properties.Resources.Account;
            this.Logo.Location = new System.Drawing.Point(205, 44);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(211, 166);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Logo.TabIndex = 0;
            this.Logo.TabStop = false;
            // 
            // Version_text
            // 
            this.Version_text.AutoSize = true;
            this.Version_text.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Version_text.Location = new System.Drawing.Point(600, 460);
            this.Version_text.Name = "Version_text";
            this.Version_text.Size = new System.Drawing.Size(23, 13);
            this.Version_text.TabIndex = 5;
            this.Version_text.Text = "V 1";
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(637, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Money Transfer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SignUp;
        private System.Windows.Forms.Button Loginbot;
        private System.Windows.Forms.TextBox BinText;
        private System.Windows.Forms.TextBox Emailtext;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label Version_text;
    }
}

