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
		//Biến đánh dấu rằng người dùng đang vẽ hình (quá trình vẽ bắt đầu khi nhấn chuột, kết thúc khi nhả chuột)
		bool isDrawing = false;
		//Biến đanh dấu rằng người dùng đang vẽ hình đa giác
		bool isPolygonDrawing = false;
		//Màu người dùng đang chọn
		Color userColor = Color.White;
		//Độ dày nét vẽ người dùng đang chọn
		float userWidth = 1.0f;
		//Loại hình vẽ người dùng đang chọn
		Shape.shapeType userType = Shape.shapeType.NONE;
		//Tọa độ chuột
		Point pStart = new Point(0, 0), pEnd = new Point(0, 0);
		//Tập các hình đã vẽ
		List<Shape> shapes = new List<Shape>();
		//Biến đánh dấu rằng tập hình vẽ đã bị thay đổi, cần phải vẽ lại
		bool isShapesChanged = true;
		//Sai số pixel dùng trong chức năng chọn hình vẽ
		const float epsilon = 10.0f;
		//Số thứ tự của hình đang được chọn, -1 nghĩa là không chọn gì
		int choosing = -1;

		public mainForm()
		{
			InitializeComponent();
		}

		private void drawBoard_OpenGLInitialized(object sender, EventArgs e)
		{
			//Sự kiện "khởi tạo", xảy ra khi chương trình vừa được khởi chạy

			//Lấy đối tượng OpenGL
			OpenGL gl = drawBoard.OpenGL;

			//Set màu nền (đen)
			gl.ClearColor(0, 0, 0, 1);

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

			//Lấy đối tượng OpenGL
			OpenGL gl = drawBoard.OpenGL;

			//Tuy nhiên, chỉ cần vẽ khi biến cờ isShapesChanged được bật lên
			if (isShapesChanged)
			{
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

			//Vẽ điểm điều khiển của hình đang được chọn
			if (choosing >= 0)
			{
				gl.PointSize(10.0f);
				gl.Color(230.0, 230.0, 0);

				gl.Begin(OpenGL.GL_POINTS);
				for (int i = 0; i < shapes[choosing].controlPoints.Count; i++)
					gl.Vertex(shapes[choosing].controlPoints[i].X, gl.RenderContextProvider.Height - shapes[choosing].controlPoints[i].Y);
				gl.End();

				gl.Flush();
			}
		}

		private void drawBoard_MouseDown(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn chuột", bắt đầu quá trình vẽ hoặc chọn một hình đã vẽ
			choosing = -1;

			if (userType == Shape.shapeType.NONE)
			{
				//Duyệt tất cả điểm vẽ để tìm điểm gần tọa độ chuột nhất
				double minDistance = 999999999999999999999999.0;
				for (int i = 0; i < shapes.Count; i++)
					for (int j = 0; j < shapes[i].rasterPoints.Count; j++)
					{
						int dx = shapes[i].rasterPoints[j].X - e.Location.X;
						int dy = shapes[i].rasterPoints[j].Y - e.Location.Y;
						double distance = Math.Sqrt(dx * dx + dy * dy);
						if (distance < minDistance)
						{
							minDistance = distance;
							choosing = i;
						}
					}
				if (minDistance > epsilon)
					choosing = -1;

				isShapesChanged = true;
				return;
			}

			if (e.Button == MouseButtons.Left)
			{
				isDrawing = true;
				tbSelf.Text = "";
				pStart = pEnd = e.Location;

				/*
				Không tạo ra hình vẽ mới nếu đang trong quá trình vẽ đa giác
				Mà chỉ cần thêm tọa độ chuột vào tập điểm điều khiển
				*/
				if (userType == Shape.shapeType.POLYGON && isPolygonDrawing == false)
				{
					//Tạo hình vẽ mới
					isPolygonDrawing = true;
					shapes.Add(new Shape(userColor, userWidth, userType));
					shapes.Last().controlPoints.Add(pStart);
				}
				else if (userType != Shape.shapeType.POLYGON)
				{
					//Tạo hình vẽ mới
					shapes.Add(new Shape(userColor, userWidth, userType));
					shapes.Last().controlPoints.Add(pStart);
				}

				shapes.Last().controlPoints.Add(pEnd);
			}
			else if (isPolygonDrawing)
			{
				isDrawing = isPolygonDrawing = false;
			}
		}

		private void drawBoard_MouseUp(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhả chuột", kết thúc quá trình vẽ
			if (userType != Shape.shapeType.POLYGON)
				isDrawing = false;
		}

		private void drawBoard_MouseMove(object sender, MouseEventArgs e)
		{
			//Sự kiện "kéo chuột", xảy ra liên tục khi người dùng nhấn giữ chuột và kéo đi
			if (isDrawing)
			{
				pEnd = e.Location;

				//Cập nhật điểm điều kiển cuối cùng
				shapes.Last().controlPoints[shapes.Last().controlPoints.Count - 1] = pEnd;

				//Xóa tập điểm vẽ của hình đang vẽ, và tạo lại tập điểm vẽ mới
				shapes.Last().rasterPoints.Clear();
				switch (userType)
				{
					case Shape.shapeType.LINE:
						DrawingAlgorithms.Line(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.CIRCLE:
						DrawingAlgorithms.Circle(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.RECTANGLE:
						DrawingAlgorithms.Rectangle(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.ELLIPSE:
						DrawingAlgorithms.Ellipse(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.TRIANGLE:
						DrawingAlgorithms.Triangle(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.PENTAGON:
						DrawingAlgorithms.Pengtagon(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.HEXAGON:
						DrawingAlgorithms.Hexagon(shapes.Last(), pStart, pEnd);
						break;
					case Shape.shapeType.POLYGON:
						DrawingAlgorithms.Polygon(shapes.Last());
						break;
				}
				isShapesChanged = true;
			}
		}

		private void btnLine_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường thẳng"
			userType = Shape.shapeType.LINE;
			lbMode.Text = "Mode: Line";
		}

		private void btnCircle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường tròn"
			userType = Shape.shapeType.CIRCLE;
			lbMode.Text = "Mode: Circle";
		}

		private void btnRectangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình chữ nhật"
			userType = Shape.shapeType.RECTANGLE;
			lbMode.Text = "Mode: Rectangle";
		}

		private void btnEllipse_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường ellipse"
			userType = Shape.shapeType.ELLIPSE;
			lbMode.Text = "Mode: Ellipse";
		}

		private void btnTriangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình tam giác đều"
			userType = Shape.shapeType.TRIANGLE;
			lbMode.Text = "Mode: Triangle";
		}

		private void btnPentagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình ngũ giác đều"
			userType = Shape.shapeType.PENTAGON;
			lbMode.Text = "Mode: Pentagon";
		}

		private void btnHexagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình lục giác đều"
			userType = Shape.shapeType.HEXAGON;
			lbMode.Text = "Mode: Hexagon";
		}

		private void btnPolygon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình đa giác"
			userType = Shape.shapeType.POLYGON;
			lbMode.Text = "Mode: Polygon";
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			//Sự kiện "xóa toàn bộ"
			choosing = -1;
			shapes.Clear();
			isShapesChanged = true;
			isDrawing = isPolygonDrawing = false;
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
				//Ctrl + Z
				choosing = -1;
				shapes.RemoveAt(shapes.Count - 1);
				isShapesChanged = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				//Esc
				if (isDrawing)
				{
					isDrawing = isPolygonDrawing = false;
					shapes.RemoveAt(shapes.Count - 1);
					isShapesChanged = true;
				}
				else
				{
					choosing = -1;
					userType = Shape.shapeType.NONE;
					lbMode.Text = "Mode: Picking";
				}
			}
		}
	}

	class Shape
	{
		//Lớp "Shape", định nghĩa một hình vẽ
		public enum shapeType { NONE, LINE, CIRCLE, RECTANGLE, ELLIPSE, TRIANGLE, PENTAGON, HEXAGON, POLYGON }

		//Màu nét vẽ
		public Color color;
		//Độ dày nét vẽ
		public float width;
		//Loại hình vẽ
		public shapeType type;
		//Tập các điểm điều kiển. Lưu ý, để các hàm build-in của OpenGL vẽ đúng, các điểm điều khiển phải đúng thứ tự
		public List<Point> controlPoints;
		//Tập các điểm vẽ
		public List<Point> rasterPoints;

		public Shape(Color userColor, float userWidth, shapeType userType)
		{
			//Hàm khởi tạo
			color = userColor;
			width = userWidth;
			type = userType;
			rasterPoints = new List<Point>();
			controlPoints = new List<Point>();
		}

		public void draw(OpenGL gl)
		{
			//Set màu nét vẽ
			gl.Color((byte)color.R, (byte)color.G, (byte)color.B, color.A);

			gl.PointSize(width);
			gl.Begin(OpenGL.GL_POINTS);

			//Liệt kê tập điểm vẽ cho OpenGL
			for (int i = 0; i < rasterPoints.Count; i++)
				gl.Vertex(rasterPoints[i].X, gl.RenderContextProvider.Height - rasterPoints[i].Y);

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
					newShape.rasterPoints.Add(new Point(move.X, i + move.Y));
				return;
			}

			//Thuật toán Bresenham
			int dy2 = 2 * pEnd.Y, dx2 = 2 * pEnd.X;
			float m = (float)dy2 / dx2;
			bool negativeM = false, largeM = false;

			//Nếu m < 0, lấy đối xứng qua x = 0
			if (m < 0)
			{
				pEnd.Y = -pEnd.Y;
				dy2 = -dy2;
				m = -m;
				negativeM = true;
			}

			//Nếu m > 1, lấy đối xứng qua y = x
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

			//Đối xứng lại qua y = x
			if (largeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].Y, points[i].X);

			//Đối xứng lại qua y = 0
			if (negativeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].X, -points[i].Y);

			//Thêm tất cả vào tập điểm vẽ
			for (int i = 0; i < points.Count; i++)
				newShape.rasterPoints.Add(new Point(points[i].X + move.X, points[i].Y + move.Y));

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
			newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));

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
				newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			//Lấy đối xứng qua trục y = x và tịnh tiến theo vector (xC, yC)
			Point p;
			int i, size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.Y, p.X));
				newShape.rasterPoints.Add(new Point(p.Y + pCenter.X, p.X + pCenter.Y));
			}

			//Lấy đối xứng 1/4 qua trục y = 0 và tịnh tiến theo vector (xC, yC)
			size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.X, -p.Y));
				newShape.rasterPoints.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}

			//Lấy đối xứng 1/2 qua trục x = 0 và tịnh tiến theo vector (xC, yC)
			size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				newShape.rasterPoints.Add(new Point(-p.X + pCenter.X, p.Y + pCenter.Y));
			}
			partsOfCircle.Clear();
		}

		public static void Rectangle(Shape newShape, Point pStart, Point pEnd)
		{
			//Tạo hình chữ nhật bằng 4 đường thẳng
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
			//Tính tâm ellipse
			Point pCenter = new Point((pStart.X + pEnd.X) / 2, (pStart.Y + pEnd.Y) / 2);
			
			//Tính đường kính rX
			double rX = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pStart.Y, 2)) / 2;
			
			//Tính đường kính Ry
			double rY = Math.Sqrt(Math.Pow(pStart.X - pStart.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;
			
			//Điểm đầu (0, Ry)
			int x = 0;
			int y = (int)rY;
			
			//List chứa điểm ở 1/4
			List<Point> oneFourth = new List<Point>();
   
			oneFourth.Add(new Point(x, y));

			newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));

			//Các thông số cơ bản
			double rX2 = rX * rX, rY2 = rY * rY;
			double rX2y = 2 * rX2 * y;
			double rY2x = 2 * rY2 * x;
			double decision = rY2 - rX2 * rY + rX2 / 4;
   
			while (rY2x < rX2y)
			{
				if (decision < 0)
				{
					x++;
					rY2x += 2 * rY2;
					decision += rY2x + rY2;
				}
				else
				{
					x++;
					y--;
					rY2x += 2 * rY2;
					rX2y -= 2 * rX2;
					decision += rY2x - rX2y + rY2;
				}
				
				//Nhập điểm vào và tịnh tiến
				oneFourth.Add(new Point(x, y));
				newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}
			
			//xLast, yLast
			rX2y = 2 * rX2 * y;
			rY2x = 2 * rY2 * x;
			decision = rY2 * Math.Pow((x + (1 / 2)), 2) + rX2 * Math.Pow((y - 1), 2) - rX2 * rY2;
   
			while (y >= 0)
			{
				if (decision > 0)
				{
					y--;
					rX2y -= 2 * rX2;
					decision -= rX2y + rX2;
				}
				else
				{
					x++;
					y--;
					rY2x += 2 * rY2;
					rX2y -= 2 * rX2;
					decision += rY2x - rX2y + rX2;
				}
				
				//Nhập điểm vào và tịnh tiến
				oneFourth.Add(new Point(x, y));
				newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}
			
			//Chiếu đường cong 1/4 qua trục x = 0
			int size = oneFourth.Count();
			for (int i = size - 1; i >= 0; i--)
			{
				Point p = oneFourth[i];
				oneFourth.Add(new Point(p.X, -p.Y));
				newShape.rasterPoints.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}
			
			// Chiếu đường cong 1/2 qua trục y = 0
			size = oneFourth.Count();
			for (int i = size - 1; i >= 0; i--)
			{
				Point p = oneFourth[i];
				oneFourth.Add(new Point(-p.X, p.Y));
				newShape.rasterPoints.Add(new Point(-p.X + pCenter.X, p.Y + pCenter.Y));
			}
		}

		public static void Hexagon(Shape newShape, Point pStart, Point pEnd)
		{
			//Code ở đây
		}

		public static void Polygon(Shape newShape)
		{
			for (int i = 0; i < newShape.controlPoints.Count - 1; i++)
				Line(newShape, newShape.controlPoints[i], newShape.controlPoints[i + 1]);

			Line(newShape, newShape.controlPoints.Last(), newShape.controlPoints[0]);
		}
	}
}
