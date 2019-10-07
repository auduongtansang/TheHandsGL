using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheHandsGL
{
	public partial class mainForm : Form
	{
		//Biến đánh dấu rằng người dùng đang vẽ hình (quá trình vẽ bắt đầu khi nhấn giữ chuột và kéo đi, kết thúc khi nhả chuột)
		bool isDrawing = false;
		//Màu người dùng đang chọn
		Color userColor = Color.Black;
		//Độ dày nét vẽ người dùng đang chọn
		float userWidth = 2.0f;
		//Loại hình vẽ người dùng đang chọn
		Shape.shapeType userType = Shape.shapeType.NONE;
		//Tọa độ chuột
		Point pStart = new Point(0, 0), pEnd = new Point(0, 0);
		//Danh sách các hình đã vẽ
		List<Shape> shapes = new List<Shape>();
		//Biến đánh dấu danh sách hình vẽ bị thay đổi, cần phải vẽ lại
		bool isShapesChanged = true;

		public mainForm()
		{
			InitializeComponent();
		}

		private void drawBoard_OpenGLInitialized(object sender, EventArgs e)
		{
			//Sự kiện "khởi tạo", xảy ra khi chương trình vừa được khởi chạy

			//Lấy đối tượng OpenGL
			OpenGL gl = drawBoard.OpenGL;

			//Set màu nền (trắng)
			gl.ClearColor(1, 1, 1, 1);

			//Xóa toàn bộ drawBoard
			gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
		}

		private void drawBoard_Resized(object sender, EventArgs e)
		{
			//Sự kiện "thay đổi kích thước cửa sổ"

			//Lấy đối tượng OpenGL
			OpenGL gl = drawBoard.OpenGL;

			//Chọn chế độ chiếu
			gl.MatrixMode(OpenGL.GL_PROJECTION);
			//Chọn ma trận chiếu là mà trận đơn vị
			gl.LoadIdentity();
			//Set viewport theo kích thước mới
			gl.Viewport(0, 0, drawBoard.Width, drawBoard.Height);
			//Chiếu lên viewport này
			gl.Ortho2D(0, drawBoard.Width, 0, drawBoard.Height);
			//Set biến đánh dấu để vẽ lại hình
			isShapesChanged = true;
		}

		private void drawBoard_OpenGLDraw(object sender, RenderEventArgs args)
		{
			//Sự kiện "vẽ", xảy ra liên tục và lặp vô hạn lần
			
			if (isDrawing == false && isShapesChanged == true)
			{
				//Lấy đối tượng OpenGL
				OpenGL gl = drawBoard.OpenGL;

				//Xóa toàn bộ drawBoard
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				//Vẽ lại tất cả hình
				foreach (Shape shape in shapes)
					shape.draw(gl);

				gl.Flush();
				isShapesChanged = false;
			}
		}

		private void drawBoard_MouseDown(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn giữ chuột", bắt đầu quá trình vẽ
			pStart = pEnd = e.Location;
			isDrawing = true;
		}

		private void drawBoard_MouseUp(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhả chuột", kết thúc quá trình vẽ
			pEnd = e.Location;
			isDrawing = false;

			//Tạo hình vẽ mới và thêm vào danh sách, đánh dấu danh sách đã bị thay đổi
			Shape newShape = new Shape(userColor, userWidth, userType);
			switch (userType)
			{
				case Shape.shapeType.LINE:
					newShape.points.Add(pStart);
					newShape.points.Add(pEnd);
					break;
			}
			shapes.Add(newShape);
			isShapesChanged = true;
		}

		private void drawBoard_MouseMove(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn giữ chuột và kéo"
			if (isDrawing)
			{
				pEnd = e.Location;

				//Lấy đối tượng OpenGL
				OpenGL gl = drawBoard.OpenGL;

				//Xóa toàn bộ drawBoard
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				//Vẽ lại tất cả hình
				foreach (Shape shape in shapes)
					shape.draw(gl);

				//Tạo hình vẽ mới và vẽ hình này ra (không thêm hình này vào list vì chưa vẽ xong)
				Shape newShape = new Shape(userColor, userWidth, userType);
				switch (userType)
				{
					case Shape.shapeType.LINE:
						newShape.points.Add(pStart);
						newShape.points.Add(pEnd);
						break;
				}
				newShape.draw(gl);
				gl.Flush();
			}
		}

		private void btnLine_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường thẳng"
			userType = Shape.shapeType.LINE;
		}

		private void btnWidth_Click(object sender, EventArgs e)
		{
			//Sự kiện "thay đổi độ dày nét vẽ"
			userWidth += 0.5f;
			if (userWidth > 5)
				userWidth = 1.0f;
			btnWidth.Text = userWidth.ToString("0.0");
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn màu"
			if (colorDialog.ShowDialog() == DialogResult.OK)
				userColor = colorDialog.Color;
		}
	}

	class Shape
	{
		//Lớp "Shape", định nghĩa một hình vẽ
		public enum shapeType { NONE, LINE, CIRCLE, RECTANGLE, ELLIPSE, TRIANGLE, PENTAGON, HEXAGON }

		//Màu nét vẽ
		public Color lineColor;
		//Độ dày nét vẽ
		public float lineWidth;
		//Loại hình vẽ
		public shapeType type;
		//Danh sách các điểm neo
		public List<Point> points;

		public Shape(Color userColor, float userWidth, shapeType userType)
		{
			lineColor = userColor;
			lineWidth = userWidth;
			type = userType;
			points = new List<Point>();
		}

		public void draw(OpenGL gl)
		{
			gl.LineWidth(lineWidth);
			gl.Color(lineColor.R / 255, lineColor.G / 255, lineColor.B / 255);

			if (type == shapeType.LINE)
				gl.Begin(OpenGL.GL_LINES);
			else
				gl.Begin(OpenGL.GL_LINE_LOOP);

			foreach (Point point in points)
				gl.Vertex(point.X, gl.RenderContextProvider.Height - point.Y);

			gl.End();
		}
	}
}
