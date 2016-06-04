namespace FarthorlPacMan
{
    partial class GameWindows
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
            this.pacMan = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pacMan
            // 
            this.pacMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pacMan.Location = new System.Drawing.Point(0, 0);
            this.pacMan.MaximumSize = new System.Drawing.Size(1200, 800);
            this.pacMan.MinimumSize = new System.Drawing.Size(1200, 800);
            this.pacMan.Name = "pacMan";
            this.pacMan.Size = new System.Drawing.Size(1200, 800);
            this.pacMan.TabIndex = 0;
            // 
            // GameWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 861);
            this.Controls.Add(this.pacMan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1300, 900);
            this.MinimumSize = new System.Drawing.Size(1300, 900);
            this.Name = "GameWindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Farthorl PacMan Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pacMan;
    }
}

