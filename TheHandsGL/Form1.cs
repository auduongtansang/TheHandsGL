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
		//Biến đánh dấu đang vẽ hình
		bool isDrawing = false;
		//Biến đanh dấu đang vẽ đa giác
		bool isPolygonDrawing = false;
		//Biến đánh dấu đang biến đổi Affine
		bool isTransforming = false;

		//Màu đang chọn
		Color userColor = Color.White;
		//Độ dày nét vẽ đang chọn
		float userWidth = 1.0f;
		//Loại hình vẽ đang chọn để vẽ
		Shape.shapeType userType = Shape.shapeType.NONE;

		//Tọa độ chuột
		Point pStart = new Point(0, 0), pEnd = new Point(0, 0);
		//Tập các hình đã vẽ
		List<Shape> shapes = new List<Shape>();
		//Biến đánh dấu tập hình vẽ bị thay đổi => phải vẽ lại
		bool isShapesChanged = true;

		//Sai số pixel, dùng trong chức năng chọn lại hình
		const double epsilon = 50.0;
		//Số thứ tự của hình đang được chọn lại, -1 nghĩa là không chọn gì
		int choosingShape = -1;
		//Điểm vẽ đang được chọn lại, dùng trong biến đổi Affine
		Point choosingPoint = new Point(-1, -1);

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
			//Bật biến đánh dấu để vẽ lại
			isShapesChanged = true;
		}

		private void drawBoard_OpenGLDraw(object sender, RenderEventArgs args)
		{
			//Sự kiện "vẽ", xảy ra liên tục và lặp vô hạn lần

			//Lấy đối tượng OpenGL
			OpenGL gl = drawBoard.OpenGL;

			//Chỉ vẽ khi tập hình vẽ bị thay đổi
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

			//Vẽ điểm điều khiển của hình đã vẽ đang được chọn lại
			if (choosingShape >= 0)
			{
				gl.PointSize(5.0f);
				gl.Color(230.0, 230.0, 0);

				gl.Begin(OpenGL.GL_POINTS);
				for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
					gl.Vertex(shapes[choosingShape].controlPoints[i].X, gl.RenderContextProvider.Height - shapes[choosingShape].controlPoints[i].Y);
				gl.End();

				gl.Flush();
			}
		}

		private void drawBoard_MouseDown(object sender, MouseEventArgs e)
		{
			//Sự kiện "nhấn chuột"
			pStart = pEnd = e.Location;

			//Nhấn chuột trái => vẽ hình mới, hoặc chọn lại hình đã vẽ, hoặc biến đổi Affine
			if (e.Button == MouseButtons.Left)
			{
				//Nếu userType là NONE => chọn lại hình đã vẽ, hoặc biến đổi Affine
				if (userType == Shape.shapeType.NONE)
				{
					//Nếu một hình nào đó đang được chọn => biến đổi Affine
					if (choosingShape >= 0)
					{
						//Tìm điểm điều khiển gần với tọa độ chuột nhất
						double minDistance = 999999999999999999999999.0;
						Point closestControl = new Point(-1, -1);

						for (int j = 0; j < shapes[choosingShape].controlPoints.Count; j++)
						{
							int dx = shapes[choosingShape].controlPoints[j].X - e.Location.X;
							int dy = shapes[choosingShape].controlPoints[j].Y - e.Location.Y;
							double distance = dx * dx + dy * dy;

							if (distance < minDistance)
							{
								minDistance = distance;
								closestControl = shapes[choosingShape].controlPoints[j];
							}
						}

						//Nếu khoảng cách nhỏ nhất nhỏ hơn epsilon => chọn điểm điều khiển này
						if (minDistance <= epsilon)
						{
							choosingPoint = closestControl;
							isTransforming = true;
							return;
						}

						//Ngược lại => hủy chọn hình và chọn lại hình khác
						choosingShape = -1;
					}

					//Nếu không hình nào đang được chọn => chọn lại hình đã vẽ
					if (choosingShape == -1)
					{
						//Tìm điểm vẽ gần với tọa độ chuột nhất
						double minDistance = 999999999999999999999999.0;
						Point closestRaster = new Point(-1, -1);

						for (int i = 0; i < shapes.Count; i++)
							for (int j = 0; j < shapes[i].rasterPoints.Count; j++)
							{
								int dx = shapes[i].rasterPoints[j].X - e.Location.X;
								int dy = shapes[i].rasterPoints[j].Y - e.Location.Y;
								double distance = dx * dx + dy * dy;

								if (distance < minDistance)
								{
									choosingShape = i;
									minDistance = distance;
									closestRaster = shapes[i].rasterPoints[j];
								}
							}

						//Nếu khoảng cách nhỏ nhất nhỏ hơn epsilon => chọn hình này
						if (minDistance <= epsilon)
						{
							choosingPoint = closestRaster;
							isTransforming = true;
							isShapesChanged = true;
							return;
						}

						//Ngược lại => không chọn hình nào hết
						choosingShape = -1;
						isShapesChanged = true;
						return;
					}
				}

				//Nếu userType khác NONE => vẽ hình mới
				choosingShape = -1;
				isDrawing = true;
				tbSelf.Text = "";

				//Nếu userType khác POLYGON => tạo hình vẽ mới, thêm vào danh sách
				if (userType != Shape.shapeType.POLYGON)
				{
					shapes.Add(new Shape(userColor, userWidth, userType));
					shapes.Last().controlPoints.Add(pStart);
					shapes.Last().controlPoints.Add(pEnd);
				}

				//Nếu userType là POLYGON => xét 2 trường hợp bên dưới
				if (userType == Shape.shapeType.POLYGON)
				{
					//Nếu bắt đầu vẽ => tạo hình vẽ mới
					if (isPolygonDrawing == false)
					{
						isPolygonDrawing = true;
						shapes.Add(new Shape(userColor, userWidth, userType));
						shapes.Last().controlPoints.Add(pStart);
						shapes.Last().controlPoints.Add(pEnd);
					}
					//Nếu đang vẽ => không tạo ra hình mới, mà thêm điểm chuột vào tập điểm điều khiển
					else
					{
						shapes.Last().controlPoints.Add(pEnd);
					}
				}
			}

			//Nhấn chuột phải => nếu đang vẽ POLYGON => kết thúc quá trình vẽ
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
			isTransforming = false;
		}

		private void drawBoard_MouseMove(object sender, MouseEventArgs e)
		{
			//Sự kiện "kéo chuột", xảy ra liên tục khi người dùng nhấn giữ chuột và kéo đi
			pEnd = e.Location;

			//Nếu đang vẽ => chỉ cập nhật điểm điều khiển cuối cùng
			if (isDrawing)
			{
				//Cập nhật điểm điều kiển cuối cùng ứng với pEnd
				shapes.Last().controlPoints[shapes.Last().controlPoints.Count - 1] = pEnd;

				//Xóa tập điểm vẽ, vẽ lại tập điểm mới
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

			//Nếu đang biến đổi Affine => thực hiện các phép biến đổi lên tập điểm điều khiển
			else if (isTransforming)
			{
				int controlIndex = shapes[choosingShape].controlPoints.IndexOf(choosingPoint);

				//Nếu đang chọn điểm điều khiển => cập nhật điểm điều khiển này
				if (controlIndex >= 0)
				{
					shapes[choosingShape].controlPoints[controlIndex] = choosingPoint = pEnd;
				}
				//Ngược lại => tịnh tiến tất cả điểm điều khiển theo vector (pEnd - pStart)
				else
				{
					AffineTransform transformer = new AffineTransform();
					transformer.Translate(pEnd.X - pStart.X, pEnd.Y - pStart.Y);

					choosingPoint = transformer.Transform(choosingPoint);
					pStart = pEnd;

					for (int i = 0; i < shapes[choosingShape].controlPoints.Count; i++)
						shapes[choosingShape].controlPoints[i] = transformer.Transform(shapes[choosingShape].controlPoints[i]);
				}

				//Xóa tập điểm vẽ, vẽ lại tập điểm mới
				shapes[choosingShape].rasterPoints.Clear();

				Point controlPoint0 = shapes[choosingShape].controlPoints[0];
				Point controlPoint1 = shapes[choosingShape].controlPoints[1];

				switch (shapes[choosingShape].type)
				{
					case Shape.shapeType.LINE:
						DrawingAlgorithms.Line(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.CIRCLE:
						DrawingAlgorithms.Circle(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.RECTANGLE:
						DrawingAlgorithms.Rectangle(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.ELLIPSE:
						DrawingAlgorithms.Ellipse(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.TRIANGLE:
						DrawingAlgorithms.Triangle(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.PENTAGON:
						DrawingAlgorithms.Pengtagon(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.HEXAGON:
						DrawingAlgorithms.Hexagon(shapes[choosingShape], controlPoint0, controlPoint1);
						break;
					case Shape.shapeType.POLYGON:
						DrawingAlgorithms.Polygon(shapes[choosingShape]);
						break;
				}
				isShapesChanged = true;
			}
		}

		private void btnLine_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường thẳng"
			userType = Shape.shapeType.LINE;
			lbMode.Text = "Mode: Line (ESC to cancel)";
		}

		private void btnCircle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đường tròn"
			userType = Shape.shapeType.CIRCLE;
			lbMode.Text = "Mode: Circle (ESC to cancel)";
		}

		private void btnRectangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ hình chữ nhật"
			userType = Shape.shapeType.RECTANGLE;
			lbMode.Text = "Mode: Rectangle (ESC to cancel)";
		}

		private void btnEllipse_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ ellipse"
			userType = Shape.shapeType.ELLIPSE;
			lbMode.Text = "Mode: Ellipse (ESC to cancel)";
		}

		private void btnTriangle_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ tam giác đều"
			userType = Shape.shapeType.TRIANGLE;
			lbMode.Text = "Mode: Triangle (ESC to cancel)";
		}

		private void btnPentagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ ngũ giác đều"
			userType = Shape.shapeType.PENTAGON;
			lbMode.Text = "Mode: Pentagon (ESC to cancel)";
		}

		private void btnHexagon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ lục giác đều"
			userType = Shape.shapeType.HEXAGON;
			lbMode.Text = "Mode: Hexagon (ESC to cancel)";
		}

		private void btnPolygon_Click(object sender, EventArgs e)
		{
			//Sự kiện "chọn vẽ đa giác"
			userType = Shape.shapeType.POLYGON;
			lbMode.Text = "Mode: Polygon (ESC to cancel)";
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			//Sự kiện "xóa toàn bộ"
			choosingShape = -1;
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
				//Ctrl + Z => undo
				choosingShape = -1;
				shapes.RemoveAt(shapes.Count - 1);
				isShapesChanged = true;
				isDrawing = isPolygonDrawing = false;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				//Esc => hủy bỏ thao tác
				if (isDrawing)
				{
					choosingShape = -1;
					shapes.RemoveAt(shapes.Count - 1);
					isShapesChanged = true;
					isDrawing = isPolygonDrawing = false;
				}
				else
				{
					choosingShape = -1;
					userType = Shape.shapeType.NONE;
					isShapesChanged = true;
					isDrawing = isPolygonDrawing = false;
					lbMode.Text = "Mode: Picking";
				}
			}
		}
	}

	class Shape
	{
		//Lớp "Shape", đối tượng hình vẽ
		public enum shapeType { NONE, LINE, CIRCLE, RECTANGLE, ELLIPSE, TRIANGLE, PENTAGON, HEXAGON, POLYGON }

		//Màu nét vẽ
		public Color color;
		//Độ dày nét vẽ
		public float width;
		//Loại hình vẽ
		public shapeType type;

		//Tập điểm điều kiển. Lưu ý, để vẽ đúng, các điểm điều khiển phải đúng thứ tự
		public List<Point> controlPoints;
		//Tập điểm vẽ
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
			gl.Color(color.R, color.G, color.B, color.A);
			//Set độ dày nét vẽ
			gl.PointSize(width);

			//Liệt kê tập điểm vẽ cho OpenGL
			gl.Begin(OpenGL.GL_POINTS);
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

			//Tịnh tiến sao cho pStart trùng với (0, 0), vector tịnh tiến là move
			Point move = new Point(pStart.X, pStart.Y);
			(pStart.X, pStart.Y) = (0, 0);
			(pEnd.X, pEnd.Y) = (pEnd.X - move.X, pEnd.Y - move.Y);

			//Nếu dx bằng 0, đường thẳng đứng
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

			//Nếu m < 0, đối xứng qua trục x = 0
			if (m < 0)
			{
				pEnd.Y = -pEnd.Y;
				dy2 = -dy2;
				m = -m;
				negativeM = true;
			}

			//Nếu m > 1, đối xứng qua trục y = x
			if (m > 1)
			{
				(pEnd.X, pEnd.Y) = (pEnd.Y, pEnd.X);
				(dy2, dx2) = (dx2, dy2);
				largeM = true;
			}

			//Tính p0
			int p = dy2 - pEnd.X;

			//List chứa các điểm vẽ
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

			//Đối xứng lại qua trục y = x (nếu cần)
			if (largeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].Y, points[i].X);

			//Đối xứng lại qua trục y = 0 (nếu cần)
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
			//Tính tập điểm theo chiều kim đồng hồ
			//Nhưng winform tính điểm (0, 0) từ góc trên xuống => ngược chiều kim đồng hồ

			//Tính bán kính r
			double r = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;

			//Tính p0
			double decision = 5 / 4 - r;

			//Điểm đầu (0, r)
			int x = 0;
			int y = (int)r;

			//Tâm đường tròn, cũng chính là vector tịnh tiến
			Point pCenter = new Point((pStart.X + pEnd.X) / 2, (pStart.Y + pEnd.Y) / 2);

			//Tập điểm vẽ ở 1/8 và đối xứng của 1/8 qua trục y = x
			List<Point> partsOfCircle = new List<Point>();

			//Thêm điểm đầu vào tập điểm vẽ
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

				//Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
				partsOfCircle.Add(new Point(x, y));
				newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			//Đối xứng qua trục y = x, rồi tịnh tiến theo tâm
			Point p;
			int i, size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.Y, p.X));
				newShape.rasterPoints.Add(new Point(p.Y + pCenter.X, p.X + pCenter.Y));
			}

			//Đối xứng 1/4 qua trục y = 0, rồi tịnh tiến theo tâm
			size = partsOfCircle.Count();
			for (i = size - 1; i >= 0; i--)
			{
				p = partsOfCircle[i];
				partsOfCircle.Add(new Point(p.X, -p.Y));
				newShape.rasterPoints.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}

			//Đối xứng 1/2 qua trục x = 0, rồi tịnh tiến theo tâm
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
			//Vẽ hình chữ nhật bằng 4 đường thẳng
			Point p1 = new Point(pEnd.X, pStart.Y);
			Point p2 = new Point(pStart.X, pEnd.Y);

			Line(newShape, pStart, p1);
			Line(newShape, p1, pEnd);
			Line(newShape, pEnd, p2);
			Line(newShape, p2, pStart);
		}

		public static void Triangle(Shape newShape, Point pStart, Point pEnd)
		{
			//Tịnh tiến sao cho pStart trùng với (0, 0), vector tịnh tiến là move
			Point move = new Point(pStart.X, pStart.Y);
			(pStart.X, pStart.Y) = (0, 0);
			(pEnd.X, pEnd.Y) = (pEnd.X - move.X, pEnd.Y - move.Y);

			//Quay pEnd quanh tâm 60 độ để được điểm mới
			Point p = new Point();
			double cosPhi = Math.Cos(60 * Math.PI / 180);
			double sinPhi = Math.Sin(60 * Math.PI / 180);

			p.X = (int)(pEnd.X * cosPhi - pEnd.Y * sinPhi);
			p.Y = (int)(pEnd.X * sinPhi + pEnd.Y * cosPhi);

			//Tịnh tiến về như cũ
			(pStart.X, pStart.Y) = (pStart.X + move.X, pStart.Y + move.Y);
			(pEnd.X, pEnd.Y) = (pEnd.X + move.X, pEnd.Y + move.Y);
			(p.X, p.Y) = (p.X + move.X, p.Y + move.Y);

			Line(newShape, pStart, pEnd);
			Line(newShape, pEnd, p);
			Line(newShape, p, pStart);
		}

		public static void Pengtagon(Shape newShape, Point pStart, Point pEnd)
		{
			//Tịnh tiến sao cho pStart trùng với (0, 0)
			//Vector tịnh tiến là move
			Point move = new Point(pStart.X, pStart.Y);
			(pStart.X, pStart.Y) = (0, 0);
			(pEnd.X, pEnd.Y) = (pEnd.X - move.X, pEnd.Y - move.Y);

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
			(pStart.X, pStart.Y) = (pStart.X + move.X, pStart.Y + move.Y);
			(pEnd.X, pEnd.Y) = (pEnd.X + move.X, pEnd.Y + move.Y);
			(p1.X, p1.Y) = (p1.X + move.X, p1.Y + move.Y);
			(p2.X, p2.Y) = (p2.X + move.X, p2.Y + move.Y);
			(p3.X, p3.Y) = (p3.X + move.X, p3.Y + move.Y);
			(p4.X, p4.Y) = (p4.X + move.X, p4.Y + move.Y);

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
			
			//Tính đường kính rY
			double rY = Math.Sqrt(Math.Pow(pStart.X - pStart.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;
			
			//Điểm đầu (0, rY)
			int x = 0;
			int y = (int)rY;
			
			//Tập điểm ở 1/4
			List<Point> oneFourth = new List<Point>();
   
			//Thêm điểm đầu vào tập điểm vẽ
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
				
				//Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
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

				//Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
				oneFourth.Add(new Point(x, y));
				newShape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}
			
			//Đối xứng 1/4 qua trục x = 0, rồi thêm vào tập điểm vẽ
			int size = oneFourth.Count();
			for (int i = size - 1; i >= 0; i--)
			{
				Point p = oneFourth[i];
				oneFourth.Add(new Point(p.X, -p.Y));
				newShape.rasterPoints.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}

			//Đối xứng 1/2 qua trục y = 0, rồi thêm vào tập điểm vẽ
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
			//Nối các điểm kề nhau
			for (int i = 0; i < newShape.controlPoints.Count - 1; i++)
				Line(newShape, newShape.controlPoints[i], newShape.controlPoints[i + 1]);
			//Nối điểm cuối với điểm đầu
			Line(newShape, newShape.controlPoints.Last(), newShape.controlPoints[0]);
		}
	}

	public class AffineTransform
	{
		//Lớp "AffineTransform", định nghĩa các phép biến đổi Affine 2D (3x3)

		//Ma trận biến đổi Affine
		public List<double> transformMatrix;

		public AffineTransform()
		{
			//Hàm khởi tạo (khởi tạo bằng ma trận đơn vị)
			transformMatrix = new List<double> { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
		}

		public void Multiply(List<double> matrix)
		{
			//Hàm nhân một ma trận khác với ma trận hiện hành
			List<double> retMatrix = new List<double> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					for (int k = 0; k < 3; k++)
						retMatrix[i * 3 + j] += matrix[i * 3 + k] * transformMatrix[k * 3 + j];
			transformMatrix = retMatrix;
		}

		public void LoadIdentity()
		{
			//Reset ma trận hiện hành về ma trận đơn vị
			transformMatrix = new List<double> { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
		}

		public void Translate(double dx, double dy)
		{
			//Hàm tạo ma trận tịnh tiến và nhân với ma trận hiện hành
			List<double> transformMatrix = new List<double> { 1, 0, dx, 0, 1, dy, 0, 0, 1 };
			Multiply(transformMatrix);
		}

		public void Rotate(double phi)
		{
			//Hàm tạo ma trận xoay và nhân với mà trận hiện hành
			double cosPhi = Math.Cos(phi), sinPhi = Math.Sin(phi);
			List<double> rotateMatrix = new List<double> { cosPhi, -sinPhi, 0, sinPhi, cosPhi, 0, 0, 0, 1 };
			Multiply(rotateMatrix);
		}

		public Point Transform(Point p)
		{
			//Hàm áp dụng phép biến đổi lên một điểm
			List<double> oriPoint = new List<double> { p.X, p.Y, 1.0 };
			List<double> retPoint = new List<double> { 0, 0, 0 };
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					retPoint[i] += transformMatrix[i * 3 + j] * oriPoint[j];
			return new Point((int)(retPoint[0]), (int)(retPoint[1]));
		}
	}
}
