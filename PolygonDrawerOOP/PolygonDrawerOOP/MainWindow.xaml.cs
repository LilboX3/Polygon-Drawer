using PolygonDrawerOOP.Service;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolygonDrawerOOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PolygonDrawer drawer = new();
        private Line? previewLine;

        public MainWindow()
        {
            InitializeComponent();
        }

        void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(DrawingCanvas);

            if(e.ClickCount == 2)
            {
                FinishCurrentPolygon();
                return;
            }

            if (drawer.Current == null)
            {
                drawer.StartPolygon(p);
            }
            else
            {
                drawer.AddVertex(p);
                DrawLineBetweenLastVertices();
            }
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (drawer.Current == null) return;

            Point p = e.GetPosition(DrawingCanvas);
            UpdatePreviewLine(p);
        }

        void FinishCurrentPolygon()
        {
            if (drawer.Current == null) return;
            drawer.FinishPolygon();
            RemovePreviewLine();
        }

        private void DrawLineBetweenLastVertices()
        {
            var vertices = drawer.Current.Vertices;

            if (vertices.Count < 2)
                return;

            Point from = vertices[vertices.Count - 2];
            Point to = vertices[vertices.Count - 1];

            Line line = new Line
            {
                X1 = from.X,
                Y1 = from.Y,
                X2 = to.X,
                Y2 = to.Y,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            DrawingCanvas.Children.Add(line);
        }

        private void UpdatePreviewLine(Point mousePos)
        {
            var vertices = drawer.Current.Vertices;
            if (vertices.Count == 0) return;

            Point last = vertices[^1];

            if (previewLine == null)
            {
                previewLine = new Line
                {
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection { 4, 4 }
                };
                DrawingCanvas.Children.Add(previewLine);
            }

            previewLine.X1 = last.X;
            previewLine.Y1 = last.Y;
            previewLine.X2 = mousePos.X;
            previewLine.Y2 = mousePos.Y;
        }

        private void RemovePreviewLine()
        {
            previewLine = null;
        }
    }
}