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
			this.tbTime = new System.Windows.Forms.TextBox();
			this.btnPolygon = new System.Windows.Forms.Button();
			this.lbMode = new System.Windows.Forms.Label();
			this.btnRotateOrScale = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.drawBoard)).BeginInit();
			this.SuspendLayout();
			// 
			// drawBoard
			// 
			this.drawBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.drawBoard.Cursor = System.Windows.Forms.Cursors.Cross;
			this.drawBoard.DrawFPS = false;
			this.drawBoard.Location = new System.Drawing.Point(0, 37);
			this.drawBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.drawBoard.Name = "drawBoard";
			this.drawBoard.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
			this.drawBoard.RenderContextType = SharpGL.RenderContextType.DIBSection;
			this.drawBoard.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
			this.drawBoard.Size = new System.Drawing.Size(668, 488);
			this.drawBoard.TabIndex = 0;
			this.drawBoard.OpenGLInitialized += new System.EventHandler(this.drawBoard_OpenGLInitialized);
			this.drawBoard.OpenGLDraw += new SharpGL.RenderEventHandler(this.drawBoard_OpenGLDraw);
			this.drawBoard.Resized += new System.EventHandler(this.drawBoard_Resized);
			this.drawBoard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drawBoard_KeyDown);
			this.drawBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseDown);
			this.drawBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseMove);
			this.drawBoard.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawBoard_MouseUp);
			// 
			// colorDialog
			// 
			this.colorDialog.Color = System.Drawing.Color.White;
			// 
			// btnLine
			// 
			this.btnLine.Location = new System.Drawing.Point(0, 4);
			this.btnLine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(68, 28);
			this.btnLine.TabIndex = 1;
			this.btnLine.Text = "Line";
			this.btnLine.UseVisualStyleBackColor = true;
			this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
			// 
			// btnCircle
			// 
			this.btnCircle.Location = new System.Drawing.Point(68, 4);
			this.btnCircle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnCircle.Name = "btnCircle";
			this.btnCircle.Size = new System.Drawing.Size(68, 28);
			this.btnCircle.TabIndex = 4;
			this.btnCircle.Text = "Circle";
			this.btnCircle.UseVisualStyleBackColor = true;
			this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
			// 
			// btnRectangle
			// 
			this.btnRectangle.Location = new System.Drawing.Point(135, 4);
			this.btnRectangle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnRectangle.Name = "btnRectangle";
			this.btnRectangle.Size = new System.Drawing.Size(68, 28);
			this.btnRectangle.TabIndex = 5;
			this.btnRectangle.Text = "Rectangle";
			this.btnRectangle.UseVisualStyleBackColor = true;
			this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
			// 
			// btnEllipse
			// 
			this.btnEllipse.Location = new System.Drawing.Point(202, 4);
			this.btnEllipse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnEllipse.Name = "btnEllipse";
			this.btnEllipse.Size = new System.Drawing.Size(68, 28);
			this.btnEllipse.TabIndex = 6;
			this.btnEllipse.Text = "Ellipse";
			this.btnEllipse.UseVisualStyleBackColor = true;
			this.btnEllipse.Click += new System.EventHandler(this.btnEllipse_Click);
			// 
			// btnTriangle
			// 
			this.btnTriangle.Location = new System.Drawing.Point(270, 4);
			this.btnTriangle.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnTriangle.Name = "btnTriangle";
			this.btnTriangle.Size = new System.Drawing.Size(68, 28);
			this.btnTriangle.TabIndex = 7;
			this.btnTriangle.Text = "Triangle";
			this.btnTriangle.UseVisualStyleBackColor = true;
			this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
			// 
			// btnPentagon
			// 
			this.btnPentagon.Location = new System.Drawing.Point(338, 4);
			this.btnPentagon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnPentagon.Name = "btnPentagon";
			this.btnPentagon.Size = new System.Drawing.Size(68, 28);
			this.btnPentagon.TabIndex = 8;
			this.btnPentagon.Text = "Pentagon";
			this.btnPentagon.UseVisualStyleBackColor = true;
			this.btnPentagon.Click += new System.EventHandler(this.btnPentagon_Click);
			// 
			// btnHexagon
			// 
			this.btnHexagon.Location = new System.Drawing.Point(405, 4);
			this.btnHexagon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnHexagon.Name = "btnHexagon";
			this.btnHexagon.Size = new System.Drawing.Size(68, 28);
			this.btnHexagon.TabIndex = 9;
			this.btnHexagon.Text = "Hexagon";
			this.btnHexagon.UseVisualStyleBackColor = true;
			this.btnHexagon.Click += new System.EventHandler(this.btnHexagon_Click);
			// 
			// btnPolygon
			// 
			this.btnPolygon.Location = new System.Drawing.Point(472, 4);
			this.btnPolygon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnPolygon.Name = "btnPolygon";
			this.btnPolygon.Size = new System.Drawing.Size(68, 28);
			this.btnPolygon.TabIndex = 17;
			this.btnPolygon.Text = "Polygon";
			this.btnPolygon.UseVisualStyleBackColor = true;
			this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Location = new System.Drawing.Point(600, 4);
			this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(68, 28);
			this.btnClear.TabIndex = 10;
			this.btnClear.Text = "ClearAll";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnWidth
			// 
			this.btnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.btnWidth.Location = new System.Drawing.Point(68, 528);
			this.btnWidth.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnWidth.Name = "btnWidth";
			this.btnWidth.Size = new System.Drawing.Size(68, 28);
			this.btnWidth.TabIndex = 3;
			this.btnWidth.Text = "Width: 1.0";
			this.btnWidth.UseVisualStyleBackColor = true;
			this.btnWidth.Click += new System.EventHandler(this.btnWidth_Click);
			// 
			// btnColor
			// 
			this.btnColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnColor.Location = new System.Drawing.Point(0, 528);
			this.btnColor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(68, 28);
			this.btnColor.TabIndex = 2;
			this.btnColor.Text = "Color";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// btnRotateOrScale
			// 
			this.btnRotateOrScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRotateOrScale.Location = new System.Drawing.Point(136, 528);
			this.btnRotateOrScale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnRotateOrScale.Name = "btnRotateOrScale";
			this.btnRotateOrScale.Size = new System.Drawing.Size(68, 28);
			this.btnRotateOrScale.TabIndex = 19;
			this.btnRotateOrScale.Text = "Rotate";
			this.btnRotateOrScale.UseVisualStyleBackColor = true;
			this.btnRotateOrScale.Click += new System.EventHandler(this.btnRotateOrScale_Click);
			// 
			// lbUndo
			// 
			this.lbUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbUndo.AutoSize = true;
			this.lbUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.lbUndo.Location = new System.Drawing.Point(209, 537);
			this.lbUndo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbUndo.Name = "lbUndo";
			this.lbUndo.Size = new System.Drawing.Size(73, 13);
			this.lbUndo.TabIndex = 11;
			this.lbUndo.Text = "Undo: Ctrl + Z";
			// 
			// lbTime
			// 
			this.lbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbTime.AutoSize = true;
			this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.lbTime.Location = new System.Drawing.Point(563, 537);
			this.lbTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbTime.Name = "lbTime";
			this.lbTime.Size = new System.Drawing.Size(33, 13);
			this.lbTime.TabIndex = 12;
			this.lbTime.Text = "Time:";
			// 
			// tbTime
			// 
			this.tbTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.tbTime.Location = new System.Drawing.Point(597, 534);
			this.tbTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tbTime.Name = "tbTime";
			this.tbTime.ReadOnly = true;
			this.tbTime.Size = new System.Drawing.Size(67, 20);
			this.tbTime.TabIndex = 14;
			// 
			// lbMode
			// 
			this.lbMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbMode.AutoSize = true;
			this.lbMode.Location = new System.Drawing.Point(286, 537);
			this.lbMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lbMode.Name = "lbMode";
			this.lbMode.Size = new System.Drawing.Size(75, 13);
			this.lbMode.TabIndex = 18;
			this.lbMode.Text = "Mode: Picking";
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(668, 561);
			this.Controls.Add(this.btnRotateOrScale);
			this.Controls.Add(this.lbMode);
			this.Controls.Add(this.btnPolygon);
			this.Controls.Add(this.tbTime);
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
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MinimumSize = new System.Drawing.Size(672, 566);
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
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Button btnPolygon;
		private System.Windows.Forms.Label lbMode;
		private System.Windows.Forms.Button btnRotateOrScale;
	}
}
