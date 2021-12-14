
namespace ControllerTest
{
    partial class ControllerWindow
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
            this.controller1 = new Controller();
            this.SuspendLayout();
            // 
            // controller1
            // 
            this.controller1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.controller1.BackColor = System.Drawing.Color.Transparent;
            this.controller1.Location = new System.Drawing.Point(10, 10);
            this.controller1.Margin = new System.Windows.Forms.Padding(0);
            this.controller1.MaximumSize = new System.Drawing.Size(460, 315);
            this.controller1.MinimumSize = new System.Drawing.Size(460, 315);
            this.controller1.Name = "controller1";
            this.controller1.RefreshRate = 10;
            this.controller1.Size = new System.Drawing.Size(460, 315);
            this.controller1.Symbols = false;
            this.controller1.TabIndex = 0;
            this.controller1.Update += new System.EventHandler(this.controller1_Update);
            // 
            // ControllerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(480, 342);
            this.Controls.Add(this.controller1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControllerWindow";
            this.Text = "Controller";
            this.ResumeLayout(false);

        }

        #endregion

        private Controller controller1;
    }
}

