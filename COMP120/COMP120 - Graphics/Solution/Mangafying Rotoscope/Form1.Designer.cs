
namespace Mangafying_Rotoscope
{
    partial class ApplicationWindow
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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.Load_Button = new System.Windows.Forms.Button();
            this.Manga_Effect_Button = new System.Windows.Forms.Button();
            this.Original_Image = new System.Windows.Forms.PictureBox();
            this.Loading_Text = new System.Windows.Forms.TextBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Anime_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Original_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(-1, -1);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(651, 453);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // Load_Button
            // 
            this.Load_Button.Location = new System.Drawing.Point(685, 40);
            this.Load_Button.Name = "Load_Button";
            this.Load_Button.Size = new System.Drawing.Size(80, 25);
            this.Load_Button.TabIndex = 1;
            this.Load_Button.Text = "Load Image";
            this.Load_Button.UseVisualStyleBackColor = true;
            this.Load_Button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Load_Button_MouseClick);
            // 
            // Manga_Effect_Button
            // 
            this.Manga_Effect_Button.Location = new System.Drawing.Point(685, 70);
            this.Manga_Effect_Button.Name = "Manga_Effect_Button";
            this.Manga_Effect_Button.Size = new System.Drawing.Size(80, 25);
            this.Manga_Effect_Button.TabIndex = 2;
            this.Manga_Effect_Button.Text = "Apply Manga!";
            this.Manga_Effect_Button.UseVisualStyleBackColor = true;
            this.Manga_Effect_Button.Click += new System.EventHandler(this.Manga_Effect_Button_Click);
            // 
            // Original_Image
            // 
            this.Original_Image.Location = new System.Drawing.Point(660, 270);
            this.Original_Image.Name = "Original_Image";
            this.Original_Image.Size = new System.Drawing.Size(130, 170);
            this.Original_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Original_Image.TabIndex = 3;
            this.Original_Image.TabStop = false;
            // 
            // Loading_Text
            // 
            this.Loading_Text.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loading_Text.Location = new System.Drawing.Point(656, 174);
            this.Loading_Text.Multiline = true;
            this.Loading_Text.Name = "Loading_Text";
            this.Loading_Text.ReadOnly = true;
            this.Loading_Text.Size = new System.Drawing.Size(140, 80);
            this.Loading_Text.TabIndex = 4;
            this.Loading_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(685, 130);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(80, 25);
            this.Save_Button.TabIndex = 5;
            this.Save_Button.Text = "Save Image";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Anime_Button
            // 
            this.Anime_Button.Location = new System.Drawing.Point(685, 100);
            this.Anime_Button.Name = "Anime_Button";
            this.Anime_Button.Size = new System.Drawing.Size(80, 25);
            this.Anime_Button.TabIndex = 6;
            this.Anime_Button.Text = "Apply Anime!";
            this.Anime_Button.UseVisualStyleBackColor = true;
            this.Anime_Button.Click += new System.EventHandler(this.Anime_Button_Click);
            // 
            // ApplicationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Anime_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Loading_Text);
            this.Controls.Add(this.Original_Image);
            this.Controls.Add(this.Manga_Effect_Button);
            this.Controls.Add(this.Load_Button);
            this.Controls.Add(this.ImageBox);
            this.Name = "ApplicationWindow";
            this.Text = "Mangafying Rotoscope";
            this.Load += new System.EventHandler(this.ApplicationWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Original_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Button Load_Button;
        private System.Windows.Forms.Button Manga_Effect_Button;
        private System.Windows.Forms.PictureBox Original_Image;
        private System.Windows.Forms.TextBox Loading_Text;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Anime_Button;
    }
}

