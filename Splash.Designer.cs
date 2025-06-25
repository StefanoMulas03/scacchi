
namespace StefanoMulas_Progetto_scacchi
{
    partial class Splash
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersione = new System.Windows.Forms.Label();
            this.lblTitoloApplicazione = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Gainsboro;
            this.lblCopyright.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCopyright.ForeColor = System.Drawing.Color.Red;
            this.lblCopyright.Location = new System.Drawing.Point(333, 848);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(214, 19);
            this.lblCopyright.TabIndex = 5;
            this.lblCopyright.Text = "© Copyright 2023 Stefano Mulas";
            // 
            // lblVersione
            // 
            this.lblVersione.AutoSize = true;
            this.lblVersione.BackColor = System.Drawing.Color.Gainsboro;
            this.lblVersione.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVersione.ForeColor = System.Drawing.Color.Red;
            this.lblVersione.Location = new System.Drawing.Point(17, 848);
            this.lblVersione.Name = "lblVersione";
            this.lblVersione.Size = new System.Drawing.Size(90, 19);
            this.lblVersione.TabIndex = 4;
            this.lblVersione.Text = "Versione 1.0";
            // 
            // lblTitoloApplicazione
            // 
            this.lblTitoloApplicazione.AutoSize = true;
            this.lblTitoloApplicazione.BackColor = System.Drawing.Color.Transparent;
            this.lblTitoloApplicazione.Font = new System.Drawing.Font("Imprint MT Shadow", 43.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitoloApplicazione.ForeColor = System.Drawing.Color.Red;
            this.lblTitoloApplicazione.Location = new System.Drawing.Point(145, 52);
            this.lblTitoloApplicazione.Name = "lblTitoloApplicazione";
            this.lblTitoloApplicazione.Size = new System.Drawing.Size(275, 86);
            this.lblTitoloApplicazione.TabIndex = 3;
            this.lblTitoloApplicazione.Text = "Scacchi";
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Lime;
            this.progressBar1.Location = new System.Drawing.Point(17, 748);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(530, 40);
            this.progressBar1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 976);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 7;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StefanoMulas_Progetto_scacchi.Properties.Resources.sfondoSplashScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(564, 1002);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVersione);
            this.Controls.Add(this.lblTitoloApplicazione);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersione;
        private System.Windows.Forms.Label lblTitoloApplicazione;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}