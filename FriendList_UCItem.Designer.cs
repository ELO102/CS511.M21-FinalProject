namespace CS511.M21_FinalProject
{
    partial class FriendList_UCItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CS511.M21_FinalProject.Properties.Resources.BlankAvata;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseEnter += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.FriendList_UCItem_MouseLeave);
            this.pictureBox2.MouseHover += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(79, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "label1";
            this.label1.MouseEnter += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            this.label1.MouseLeave += new System.EventHandler(this.FriendList_UCItem_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::CS511.M21_FinalProject.Properties.Resources.correct;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(79, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 25);
            this.button1.TabIndex = 19;
            this.button1.TabStop = false;
            this.button1.Text = "Online";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseHover);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseHover);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // FriendList_UCItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.MaximumSize = new System.Drawing.Size(252, 76);
            this.MinimumSize = new System.Drawing.Size(252, 76);
            this.Name = "FriendList_UCItem";
            this.Size = new System.Drawing.Size(250, 74);
            this.MouseEnter += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            this.MouseLeave += new System.EventHandler(this.FriendList_UCItem_MouseLeave);
            this.MouseHover += new System.EventHandler(this.FriendList_UCItem_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}
