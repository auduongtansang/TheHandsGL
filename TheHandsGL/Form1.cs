using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace TheHandsGL
{
	public partial class mainForm : Form
	{
		//Biến đánh dấu rằng người dùng đang vẽ hình (quá trình vẽ bắt đầu khi nhấn giữ chuột và kéo đi, kết thúc khi nhả chuột)
		bool isDrawing = false;
		//Màu người dùng đang chọn
		Color userColor = Color.Black;
		//Độ dày nét vẽ người dùng đang chọn
		float userWidth = 1.0f;
		//Loại hình vẽ người dùng đang chọn
		Shape.shapeType userType = Shape.shapeType.NONE;
		//Tọa độ chuột
		Point pStart = new Point(0, 0), pEnd = new Point(0, 0);
		//Danh sách các hình đã vẽ
		List<Shape> shapes = new List<Shape>();
		//Biến đánh dấu rằng danh sách hình vẽ đã bị thay đổi, cần phải vẽ lại
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
			//Chọn ma trận chiếu là ma trận đơn vị
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
				//Chỉ vẽ khi người dùng đã nhả chuột, và danh sách hình vẽ đã bị thay đổi

				//Lấy đối tượng OpenGL
				OpenGL gl = drawBoard.OpenGL;

				//Xóa toàn bộ drawBoard
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				//Vẽ lại tất cả hình
				if (shapes.Count > 0)
				{
					for (int i = 0; i < shapes.Count - 1; i++)
						shapes[i].draw(gl);

					//Đo thời gian vẽ hình cuối cùng
					Stopwatch watch = Stopwatch.StartNew();
					shapes.Last().draw(gl);
					watch.Stop();
					tbSelf.Text = watch.ElapsedTicks.ToString() + " ticks";
				}

				gl.Flush();
				isShapesChanged = false;
			}
		}

		private void drawBoard_MouseDown(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn giữ chuột", bắt đầu quá trình vẽ
			pStart = pEnd = e.Location;
			isDrawing = true;
			tbSelf.Text = "";
		}

		private void drawBoard_MouseUp(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhả chuột", kết thúc quá trình vẽ
			pEnd = e.Location;
			isDrawing = false;

			//Tạo hình vẽ mới và thêm vào danh sách, đánh dấu danh sách đã bị thay đổi để vẽ lại
			Shape newShape = new Shape(userColor, userWidth, userType);
			switch (userType)
			{
				case Shape.shapeType.LINE:
					DrawingAlgorithms.Line(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.CIRCLE:
					DrawingAlgorithms.Circle(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.RECTANGLE:
					DrawingAlgorithms.Rectangle(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.ELLIPSE:
					DrawingAlgorithms.Ellipse(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.TRIANGLE:
					DrawingAlgorithms.Triangle(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.PENTAGON:
					DrawingAlgorithms.Pengtagon(newShape, pStart, pEnd);
					break;
				case Shape.shapeType.HEXAGON:
					DrawingAlgorithms.Hexagon(newShape, pStart, pEnd);
					break;
			}
			shapes.Add(newShape);
			isShapesChanged = true;
		}

		private void drawBoard_MouseMove(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn giữ chuột và kéo", xảy ra liên tục khi người dùng nhấn giữ chuột và kéo đi
			if (isDrawing)
			{
				pEnd = e.Location;
				
				//Lấy đối tượng OpenGL
				OpenGL gl = drawBoard.OpenGL;

				//Xóa toàn bộ drawBoard
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				//Vẽ lại tất cả hình
				for (int i = 0; i < shapes.Count; i++)
					shapes[i].draw(gl);

				//Tạo hình vẽ mới và vẽ hình này ra (không thêm hình này vào danh sách vì chưa nhả chuột)
				Shape newShape = new Shape(userColor, userWidth, userType);
				switch (userType)
				{
					case Shape.shapeType.LINE:
						DrawingAlgorithms.Line(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.CIRCLE:
						DrawingAlgorithms.Circle(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.RECTANGLE:
						DrawingAlgorithms.Rectangle(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.ELLIPSE:
						DrawingAlgorithms.Ellipse(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.TRIANGLE:
						DrawingAlgorithms.Triangle(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.PENTAGON:
						DrawingAlgorithms.Pengtagon(newShape, pStart, pEnd);
						break;
					case Shape.shapeType.HEXAGON:
						DrawingAlgorithms.Hexagon(newShape, pStart, pEnd);
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

		private void btnCircle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường tròn"
			userType = Shape.shapeType.CIRCLE;
		}

		private void btnRectangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình chữ nhật"
			userType = Shape.shapeType.RECTANGLE;
		}

		private void btnEllipse_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường ellipse"
			userType = Shape.shapeType.ELLIPSE;
		}

		private void btnTriangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình tam giác đều"
			userType = Shape.shapeType.TRIANGLE;
		}

		private void btnPentagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình ngũ giác đều"
			userType = Shape.shapeType.PENTAGON;
		}

		private void btnHexagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình lục giác đều"
			userType = Shape.shapeType.HEXAGON;
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			//Sự kiện "xóa toàn bộ"
			shapes.Clear();
			isShapesChanged = true;
		}

		private void btnWidth_Click(object sender, EventArgs e)
		{
			//Sự kiện "thay đổi độ dày nét vẽ"
			userWidth += 0.5f;
			if (userWidth > 3)
				userWidth = 1.0f;
			btnWidth.Text = "Width: " + userWidth.ToString("0.0");
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn màu"
			if (colorDialog.ShowDialog() == DialogResult.OK)
				userColor = colorDialog.Color;
		}

		private void drawBoard_KeyDown(object sender, KeyEventArgs e)
		{
			//Sự kiện "bấm bàn phím"
			if (e.KeyCode == Keys.Z && e.Control && shapes.Count > 0)
			{
				shapes.RemoveAt(shapes.Count - 1);
				isShapesChanged = true;
			}
		}
	}

	class Shape
	{
		//Lớp "Shape", định nghĩa một hình vẽ
		public enum shapeType { NONE, LINE, CIRCLE, RECTANGLE, ELLIPSE, TRIANGLE, PENTAGON, HEXAGON }

		//Màu nét vẽ
		public Color color;
		//Độ dày nét vẽ
		public float width;
		//Loại hình vẽ
		public shapeType type;
		//Danh sách các điểm neo. Lưu ý, thứ tự các điểm neo phải chính xác (ngược hay thuận đồng hồ đều được) để OpenGL vẽ cho đúng
		public List<Point> points;

		/*
		 * Điểm neo là những điểm nối lại với nhau sẽ thành hình cần vẽ. Vi dụ:
		 * - Đường thẳng có 2 điểm neo: điểm đầu và điểm cuối
		 * - Tam giác có 3 điểm neo (if you know what i mean)
		 * - Đường tròn có rất nhiều điểm neo sát nhau, nối lại với nhau bằng các đường thẳng rất ngắn => gần tròn
		 * - Vân vân...
		*/

		public Shape(Color userColor, float userWidth, shapeType userType)
		{
			//Hàm khởi tạo
			color = userColor;
			width = userWidth;
			type = userType;
			points = new List<Point>();
		}

		public void draw(OpenGL gl)
		{
			//Set màu nét vẽ (đang bị lỗi)
			gl.Color(color.R / 255, color.G / 255, color.B / 255);

			gl.PointSize(width);
			gl.Begin(OpenGL.GL_POINTS);

			//Liệt kê tập điểm
			for (int i = 0; i < points.Count; i++)
				gl.Vertex(points[i].X, gl.RenderContextProvider.Height - points[i].Y);

			gl.End();
		}
	}

	static class DrawingAlgorithms
	{
		//Lớp "DrawingAlgorithms", định nghĩa các thuật toán vẽ hình

		public static void Line(Shape newShape, Point pStart, Point pEnd)
		{
			//Vẽ từ điểm có hoành độ nhỏ hơn
			if (pStart.X > pEnd.X)
				(pStart, pEnd) = (pEnd, pStart);

			//Tịnh tiến sao cho pStart trùng với (0, 0)
			//Vector tịnh tiến là move
			Point move = new Point(pStart.X, pStart.Y);
			(pStart.X, pStart.Y) = (0, 0);
			(pEnd.X, pEnd.Y) = (pEnd.X - move.X, pEnd.Y - move.Y);

			//dx == 0, đường thẳng đứng
			if (pEnd.X == 0)
			{
				for (int i = Math.Min(0, pEnd.Y); i <= Math.Max(0, pEnd.Y); i++)
					newShape.points.Add(new Point(move.X, i + move.Y));
				return;
			}

			//Thuật toán Bresenham
			int dy2 = 2 * pEnd.Y, dx2 = 2 * pEnd.X;
			float m = (float)dy2 / dx2;
			bool negativeM = false, largeM = false;

			//Nếu m < 0, lấy đối xứng qua x=0
			if (m < 0)
			{
				pEnd.Y = -pEnd.Y;
				dy2 = -dy2;
				m = -m;
				negativeM = true;
			}

			//Nếu m > 1, lấy đối xứng qua y=x
			if (m > 1)
			{
				(pEnd.X, pEnd.Y) = (pEnd.Y, pEnd.X);
				(dy2, dx2) = (dx2, dy2);
				largeM = true;
			}

			//Tính p0
			int p = dy2 - pEnd.X;

			//List chứa các điểm
			List<Point> points = new List<Point>();

			int x = 0, y = 0;
			points.Add(new Point(x, y));

			while (x < pEnd.X)
			{
				if (p > 0)
				{
					x++;
					y++;
					p += dy2 - dx2;
				}
				else
				{
					x++;
					p += dy2;
				}
				points.Add(new Point(x, y));
			}

			//Đối xứng lại qua y=x
			if (largeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].Y, points[i].X);

			//Đối xứng lại qua y=0
			if (negativeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].X, -points[i].Y);

			//Add vào kết quả
			for (int i = 0; i < points.Count; i++)
				newShape.points.Add(new Point(points[i].X + move.X, points[i].Y + move.Y));

			points.Clear();
		}

		public static void Circle(Shape newShape, Point pStart, Point pEnd)
		{
			//Thực ra lấy theo chiều kim đồng hồ nhưng màn hình console 
			//Tính điểm (0, 0) ở gốc trên và tăng dần từ trên xuống nên thành ra ngược chiều kim đồng hồ

			//Tính bán kính r
			double r = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;

			//Tính p0
			double decision = 5 / 4 - r;

			//Điểm đầu (0, r)
			int x = 0;
			int y = (int)r;

			//Tính tâm để tịnh tiến hình tròn theo vector (xC, yC)
			Point pCenter = new Point((pStart.X + pEnd.X) / 2, (pStart.Y + pEnd.Y) / 2);

			//List chứa điểm ở 1/8 và đối xứng của 1/8 qua y = x
			List<Point> partsOfCircle = new List<Point>();

			//Add điểm đầu vào 
			partsOfCircle.Add(new Point(x, y));

			newShape.points.Add(new Point(x + pCenter.X, y + pCenter.Y));

			int x2 = x * 2, y2 = y * 2;

			//Thuật toán Midpoint
			while (y > x)
			{
				if (decision < 0)
				{
					decision += x2 + 3;
					x++;
					x2 += 2;
				}
				else
				{
					decision += x2 - y2 + 5;
					x++;
					y--;
					x2 += 2;
					y2 -= 2;
				}

				//Add mỗi điểm tìm được đã tịnh tiến theo vector (xC, yC) vào
				partsOfCircle.Add(new Point(x, y));
				newShape.points.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			//Lấy đối xứng qua trục y = x và tịnh tiến theo vector (xC, yC)
			Point p;
			int i, size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.Y, p.X));
				newShape.points.Add(new Point(p.Y + pCenter.X, p.X + pCenter.Y));
			}

			//Lấy đối xứng 1/4 qua trục y = 0 và tịnh tiến theo vector (xC, yC)
			size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.X, -p.Y));
				newShape.points.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}

			//Lấy đối xứng 1/2 qua trục x = 0 và tịnh tiến theo vector (xC, yC)
			size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				newShape.points.Add(new Point(-p.X + pCenter.X, p.Y + pCenter.Y));
			}
			partsOfCircle.Clear();
		}

		public static void Rectangle(Shape newShape, Point pStart, Point pEnd)
		{
			//Tạo hình chữ nhật bằng 4 đường thẳng Bressenham
			Point p1 = new Point(pEnd.X, pStart.Y);
			Point p2 = new Point(pStart.X, pEnd.Y);
			Line(newShape, pStart, p1);
			Line(newShape, p1, pEnd);
			Line(newShape, pEnd, p2);
			Line(newShape, p2, pStart);
		}

		public static void Triangle(Shape newShape, Point pStart, Point pEnd)
		{
			//Tịnh tiến pStart, pEnd về (0, 0)
			//pStart = (0, 0), tuy nhiên phải giữ lại giá trị cũ, xem như pStart là vector tịnh tiến
			(pEnd.X, pEnd.Y) = (pEnd.X - pStart.X, pEnd.Y - pStart.Y);

			//Quay pEnd quanh tâm 60 độ để được điểm mới
			Point p = new Point();
			double cosPhi = Math.Cos(60 * Math.PI / 180);
			double sinPhi = Math.Sin(60 * Math.PI / 180);

			p.X = (int)(pEnd.X * cosPhi - pEnd.Y * sinPhi);
			p.Y = (int)(pEnd.X * sinPhi + pEnd.Y * cosPhi);

			//Tịnh tiến về như cũ
			(pEnd.X, pEnd.Y) = (pEnd.X + pStart.X, pEnd.Y + pStart.Y);
			(p.X, p.Y) = (p.X + pStart.X, p.Y + pStart.Y);

			Line(newShape, pStart, pEnd);
			Line(newShape, pEnd, p);
			Line(newShape, p, pStart);
		}

		public static void Pengtagon(Shape newShape, Point pStart, Point pEnd)
		{
			//Tịnh tiến pStart, pEnd về (0, 0)
			//pStart = (0, 0), tuy nhiên phải giữ lại giá trị cũ, xem như pStart là vector tịnh tiến
			(pEnd.X, pEnd.Y) = (pEnd.X - pStart.X, pEnd.Y - pStart.Y);

			//Quay pEnd quanh tâm 72 độ để được điểm mới
			Point p1 = new Point();
			Point p2 = new Point();
			Point p3 = new Point();
			Point p4 = new Point();
			double cosPhi = Math.Cos(72 * Math.PI / 180);
			double sinPhi = Math.Sin(72 * Math.PI / 180);

			p1.X = (int)(pEnd.X * cosPhi - pEnd.Y * sinPhi);
			p1.Y = (int)(pEnd.X * sinPhi + pEnd.Y * cosPhi);

			p2.X = (int)(p1.X * cosPhi - p1.Y * sinPhi);
			p2.Y = (int)(p1.X * sinPhi + p1.Y * cosPhi);

			p3.X = (int)(p2.X * cosPhi - p2.Y * sinPhi);
			p3.Y = (int)(p2.X * sinPhi + p2.Y * cosPhi);

			p4.X = (int)(p3.X * cosPhi - p3.Y * sinPhi);
			p4.Y = (int)(p3.X * sinPhi + p3.Y * cosPhi);

			//Tịnh tiến về như cũ
			(pEnd.X, pEnd.Y) = (pEnd.X + pStart.X, pEnd.Y + pStart.Y);
			(p1.X, p1.Y) = (p1.X + pStart.X, p1.Y + pStart.Y);
			(p2.X, p2.Y) = (p2.X + pStart.X, p2.Y + pStart.Y);
			(p3.X, p3.Y) = (p3.X + pStart.X, p3.Y + pStart.Y);
			(p4.X, p4.Y) = (p4.X + pStart.X, p4.Y + pStart.Y);

			Line(newShape, pEnd, p1);
			Line(newShape, p1, p2);
			Line(newShape, p2, p3);
			Line(newShape, p3, p4);
			Line(newShape, p4, pEnd);
		}

		public static void Ellipse(Shape newShape, Point pStart, Point pEnd)
		{
			//Code ở đây
		}

		public static void Hexagon(Shape newShape, Point pStart, Point pEnd)
		{
			//Code ở đây
		}
	}
}
