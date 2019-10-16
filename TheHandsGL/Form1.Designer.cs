namespace TheHandsGL
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
			this.btnClear = new System.Windows.Forms.Button();
			this.lbUndo = new System.Windows.Forms.Label();
			this.lbTime = new System.Windows.Forms.Label();
			this.lbSelf = new System.Windows.Forms.Label();
			this.tbSelf = new System.Windows.Forms.TextBox();
			this.lbBuildIn = new System.Windows.Forms.Label();
			this.tbBuildIn = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.drawBoard)).BeginInit();
			this.SuspendLayout();
			// 
			// drawBoard
			// 
			this.drawBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawBoard.DrawFPS = false;
			this.drawBoard.Location = new System.Drawing.Point(0, 45);
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
			this.btnLine.Location = new System.Drawing.Point(0, 5);
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(90, 35);
			this.btnLine.TabIndex = 1;
			this.btnLine.Text = "Line";
			this.btnLine.UseVisualStyleBackColor = true;
			this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
			// 
			// btnCircle
			// 
			this.btnCircle.Location = new System.Drawing.Point(90, 5);
			this.btnCircle.Name = "btnCircle";
			this.btnCircle.Size = new System.Drawing.Size(90, 35);
			this.btnCircle.TabIndex = 4;
			this.btnCircle.Text = "Circle";
			this.btnCircle.UseVisualStyleBackColor = true;
			this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
			// 
			// btnRectangle
			// 
			this.btnRectangle.Location = new System.Drawing.Point(180, 5);
			this.btnRectangle.Name = "btnRectangle";
			this.btnRectangle.Size = new System.Drawing.Size(90, 35);
			this.btnRectangle.TabIndex = 5;
			this.btnRectangle.Text = "Rectangle";
			this.btnRectangle.UseVisualStyleBackColor = true;
			this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
			// 
			// btnEllipse
			// 
			this.btnEllipse.Location = new System.Drawing.Point(270, 5);
			this.btnEllipse.Name = "btnEllipse";
			this.btnEllipse.Size = new System.Drawing.Size(90, 35);
			this.btnEllipse.TabIndex = 6;
			this.btnEllipse.Text = "Ellipse";
			this.btnEllipse.UseVisualStyleBackColor = true;
			this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
			// 
			// btnTriangle
			// 
			this.btnTriangle.Location = new System.Drawing.Point(360, 5);
			this.btnTriangle.Name = "btnTriangle";
			this.btnTriangle.Size = new System.Drawing.Size(90, 35);
			this.btnTriangle.TabIndex = 7;
			this.btnTriangle.Text = "Triangle";
			this.btnTriangle.UseVisualStyleBackColor = true;
			this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
			// 
			// btnPentagon
			// 
			this.btnPentagon.Location = new System.Drawing.Point(450, 5);
			this.btnPentagon.Name = "btnPentagon";
			this.btnPentagon.Size = new System.Drawing.Size(90, 35);
			this.btnPentagon.TabIndex = 8;
			this.btnPentagon.Text = "Pentagon";
			this.btnPentagon.UseVisualStyleBackColor = true;
			this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
			// 
			// btnHexagon
			// 
			this.btnHexagon.Location = new System.Drawing.Point(540, 5);
			this.btnHexagon.Name = "btnHexagon";
			this.btnHexagon.Size = new System.Drawing.Size(90, 35);
			this.btnHexagon.TabIndex = 9;
			this.btnHexagon.Text = "Hexagon";
			this.btnHexagon.UseVisualStyleBackColor = true;
			this.btnHexagon.Click += new System.EventHandler(this.btnHexagon_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Location = new System.Drawing.Point(710, 5);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(90, 35);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "ClearAll";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnWidth
			// 
			this.btnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.btnWidth.Location = new System.Drawing.Point(90, 650);
			this.btnWidth.Name = "btnWidth";
			this.btnWidth.Size = new System.Drawing.Size(90, 35);
			this.btnWidth.TabIndex = 3;
			this.btnWidth.Text = "Width: 1.0";
			this.btnWidth.UseVisualStyleBackColor = true;
			this.btnWidth.Click += new System.EventHandler(this.btnWidth_Click);
			// 
			// btnColor
			// 
			this.btnColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnColor.Location = new System.Drawing.Point(0, 650);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(90, 35);
			this.btnColor.TabIndex = 2;
			this.btnColor.Text = "Color";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// lbUndo
			// 
			this.lbUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbUndo.AutoSize = true;
			this.lbUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.lbUndo.Location = new System.Drawing.Point(186, 659);
			this.lbUndo.Name = "lbUndo";
			this.lbUndo.Size = new System.Drawing.Size(96, 17);
			this.lbUndo.TabIndex = 11;
			this.lbUndo.Text = "Undo: Ctrl + Z";
			// 
			// lbTime
			// 
			this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTime.AutoSize = true;
			this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.lbTime.Location = new System.Drawing.Point(447, 659);
			this.lbTime.Name = "lbTime";
			this.lbTime.Size = new System.Drawing.Size(43, 17);
			this.lbTime.TabIndex = 12;
			this.lbTime.Text = "Time:";
			// 
			// lbSelf
			// 
			this.lbSelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbSelf.AutoSize = true;
			this.lbSelf.Location = new System.Drawing.Point(490, 659);
			this.lbSelf.Name = "lbSelf";
			this.lbSelf.Size = new System.Drawing.Size(32, 17);
			this.lbSelf.TabIndex = 13;
			this.lbSelf.Text = "Self";
			// 
			// tbSelf
			// 
			this.tbSelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSelf.Location = new System.Drawing.Point(520, 657);
			this.tbSelf.Name = "tbSelf";
			this.tbSelf.ReadOnly = true;
			this.tbSelf.Size = new System.Drawing.Size(100, 22);
			this.tbSelf.TabIndex = 14;
			// 
			// lbBuildIn
			// 
			this.lbBuildIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbBuildIn.AutoSize = true;
			this.lbBuildIn.Location = new System.Drawing.Point(627, 659);
			this.lbBuildIn.Name = "lbBuildIn";
			this.lbBuildIn.Size = new System.Drawing.Size(55, 17);
			this.lbBuildIn.TabIndex = 15;
			this.lbBuildIn.Text = "Build-in";
			// 
			// tbBuildIn
			// 
			this.tbBuildIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbBuildIn.Location = new System.Drawing.Point(680, 657);
			this.tbBuildIn.Name = "tbBuildIn";
			this.tbBuildIn.ReadOnly = true;
			this.tbBuildIn.Size = new System.Drawing.Size(100, 22);
			this.tbBuildIn.TabIndex = 16;
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 690);
			this.Controls.Add(this.tbBuildIn);
			this.Controls.Add(this.lbBuildIn);
			this.Controls.Add(this.tbSelf);
			this.Controls.Add(this.lbSelf);
			this.Controls.Add(this.lbTime);
			this.Controls.Add(this.lbUndo);
			this.Controls.Add(this.btnClear);
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
			this.MinimumSize = new System.Drawing.Size(800, 690);
			this.Name = "mainForm";
			this.Text = "TheHandsGL";
			((System.ComponentModel.ISupportInitialize)(this.drawBoard)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label lbUndo;
		private System.Windows.Forms.Label lbTime;
		private System.Windows.Forms.Label lbSelf;
		private System.Windows.Forms.TextBox tbSelf;
		private System.Windows.Forms.Label lbBuildIn;
		private System.Windows.Forms.TextBox tbBuildIn;
	}
}

