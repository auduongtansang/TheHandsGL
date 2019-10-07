﻿namespace TheHandsGL
{
	partial class mainForm
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
			this.drawBoard = new SharpGL.OpenGLControl();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.btnLine = new System.Windows.Forms.Button();
			this.btnColor = new System.Windows.Forms.Button();
			this.btnWidth = new System.Windows.Forms.Button();
			this.btnCircle = new System.Windows.Forms.Button();
			this.btnRectangle = new System.Windows.Forms.Button();
			this.btnEllipse = new System.Windows.Forms.Button();
			this.btnTriangle = new System.Windows.Forms.Button();
			this.btnPentagon = new System.Windows.Forms.Button();
			this.btnHexagon = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.drawBoard)).BeginInit();
			this.SuspendLayout();
			// 
			// drawBoard
			// 
			this.drawBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawBoard.DrawFPS = false;
			this.drawBoard.Location = new System.Drawing.Point(35, 0);
			this.drawBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.drawBoard.Name = "drawBoard";
			this.drawBoard.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
			this.drawBoard.RenderContextType = SharpGL.RenderContextType.DIBSection;
			this.drawBoard.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
			this.drawBoard.Size = new System.Drawing.Size(800, 600);
			this.drawBoard.TabIndex = 0;
			this.drawBoard.OpenGLInitialized += new System.EventHandler(this.drawBoard_OpenGLInitialized);
			this.drawBoard.OpenGLDraw += new SharpGL.RenderEventHandler(this.drawBoard_OpenGLDraw);
			this.drawBoard.Resized += new System.EventHandler(this.drawBoard_Resized);
			this.drawBoard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drawBoard_KeyDown);
			this.drawBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseDown);
			this.drawBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseMove);
			this.drawBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseUp);
			// 
			// btnLine
			// 
			this.btnLine.Location = new System.Drawing.Point(0, 0);
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(35, 35);
			this.btnLine.TabIndex = 1;
			this.btnLine.Text = "Lin";
			this.btnLine.UseVisualStyleBackColor = true;
			this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
			// 
			// btnColor
			// 
			this.btnColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnColor.Location = new System.Drawing.Point(0, 565);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(35, 35);
			this.btnColor.TabIndex = 2;
			this.btnColor.Text = "CL";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// btnWidth
			// 
			this.btnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
			this.btnWidth.Location = new System.Drawing.Point(0, 530);
			this.btnWidth.Name = "btnWidth";
			this.btnWidth.Size = new System.Drawing.Size(35, 35);
			this.btnWidth.TabIndex = 3;
			this.btnWidth.Text = "2.0";
			this.btnWidth.UseVisualStyleBackColor = true;
			this.btnWidth.Click += new System.EventHandler(this.btnWidth_Click);
			// 
			// btnCircle
			// 
			this.btnCircle.Location = new System.Drawing.Point(0, 35);
			this.btnCircle.Name = "btnCircle";
			this.btnCircle.Size = new System.Drawing.Size(35, 35);
			this.btnCircle.TabIndex = 4;
			this.btnCircle.Text = "Cir";
			this.btnCircle.UseVisualStyleBackColor = true;
			this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
			// 
			// btnRectangle
			// 
			this.btnRectangle.Location = new System.Drawing.Point(0, 70);
			this.btnRectangle.Name = "btnRectangle";
			this.btnRectangle.Size = new System.Drawing.Size(35, 35);
			this.btnRectangle.TabIndex = 5;
			this.btnRectangle.Text = "Re";
			this.btnRectangle.UseVisualStyleBackColor = true;
			this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
			// 
			// btnEllipse
			// 
			this.btnEllipse.Location = new System.Drawing.Point(0, 105);
			this.btnEllipse.Name = "btnEllipse";
			this.btnEllipse.Size = new System.Drawing.Size(35, 35);
			this.btnEllipse.TabIndex = 6;
			this.btnEllipse.Text = "Ell";
			this.btnEllipse.UseVisualStyleBackColor = true;
			this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
			// 
			// btnTriangle
			// 
			this.btnTriangle.Location = new System.Drawing.Point(0, 140);
			this.btnTriangle.Name = "btnTriangle";
			this.btnTriangle.Size = new System.Drawing.Size(35, 35);
			this.btnTriangle.TabIndex = 7;
			this.btnTriangle.Text = "Tri";
			this.btnTriangle.UseVisualStyleBackColor = true;
			this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
			// 
			// btnPentagon
			// 
			this.btnPentagon.Location = new System.Drawing.Point(0, 175);
			this.btnPentagon.Name = "btnPentagon";
			this.btnPentagon.Size = new System.Drawing.Size(35, 35);
			this.btnPentagon.TabIndex = 8;
			this.btnPentagon.Text = "Pe";
			this.btnPentagon.UseVisualStyleBackColor = true;
			this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
			// 
			// btnHexagon
			// 
			this.btnHexagon.Location = new System.Drawing.Point(0, 210);
			this.btnHexagon.Name = "btnHexagon";
			this.btnHexagon.Size = new System.Drawing.Size(35, 35);
			this.btnHexagon.TabIndex = 9;
			this.btnHexagon.Text = "He";
			this.btnHexagon.UseVisualStyleBackColor = true;
			this.btnHexagon.Click += new System.EventHandler(this.btnHexagon_Click);
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(835, 600);
			this.Controls.Add(this.btnHexagon);
			this.Controls.Add(this.btnPentagon);
			this.Controls.Add(this.btnTriangle);
			this.Controls.Add(this.btnEllipse);
			this.Controls.Add(this.btnRectangle);
			this.Controls.Add(this.btnCircle);
			this.Controls.Add(this.btnWidth);
			this.Controls.Add(this.btnColor);
			this.Controls.Add(this.btnLine);
			this.Controls.Add(this.drawBoard);
			this.MinimumSize = new System.Drawing.Size(835, 600);
			this.Name = "mainForm";
			this.Text = "TheHandsGL";
			((System.ComponentModel.ISupportInitialize)(this.drawBoard)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private SharpGL.OpenGLControl drawBoard;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.Button btnLine;
		private System.Windows.Forms.Button btnColor;
		private System.Windows.Forms.Button btnWidth;
		private System.Windows.Forms.Button btnCircle;
		private System.Windows.Forms.Button btnRectangle;
		private System.Windows.Forms.Button btnEllipse;
		private System.Windows.Forms.Button btnTriangle;
		private System.Windows.Forms.Button btnPentagon;
		private System.Windows.Forms.Button btnHexagon;
	}
}
