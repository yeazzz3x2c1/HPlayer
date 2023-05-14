namespace HPlayer.UI
{
    partial class Stream_Output
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
            this.Render_Block = new System.Windows.Forms.Integration.ElementHost();
            this.Render_Output_Block = new HPlayer.Controls.Stream_Output_Block();
            this.SuspendLayout();
            // 
            // Render_Block
            // 
            this.Render_Block.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Render_Block.Location = new System.Drawing.Point(0, 0);
            this.Render_Block.Name = "Render_Block";
            this.Render_Block.Size = new System.Drawing.Size(800, 450);
            this.Render_Block.TabIndex = 0;
            this.Render_Block.Text = "elementHost1";
            this.Render_Block.Child = this.Render_Output_Block;
            // 
            // Stream_Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Render_Block);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stream_Output";
            this.Opacity = 0D;
            this.Text = "Stream_Output";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost Render_Block;
        private Controls.Stream_Output_Block Render_Output_Block;
    }
}