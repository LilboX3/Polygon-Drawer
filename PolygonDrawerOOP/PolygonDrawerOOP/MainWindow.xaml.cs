using PolygonDrawerOOP.Model;
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

        void OnUndo(object sender, RoutedEventArgs e)
        {
            drawer.Undo();
            RedrawCanvas();
        }

        void OnRedo(object sender, RoutedEventArgs e)
        {
            drawer.Redo();
            RedrawCanvas();
        }

        void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(DrawingCanvas);

            if(e.ClickCount == 2)
            {
                FinishCurrentPolygon();
                RedrawCanvas();
                return;
            }

            if (drawer.Current == null)
            {
                drawer.StartPolygon(p);
            }
            else
            {
                drawer.ExecuteCommand(
                    new AddVertexCommand(drawer.Current!, p)
                );
            }
            RedrawCanvas();
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

            drawer.ExecuteCommand(
                new ClosePolygonCommand(drawer.Current)
            );

            drawer.FinishPolygon();
            RemovePreviewLine();
        }

        private void UpdatePreviewLine(Point mousePos)
        {
            var vertices = drawer.Current!.Vertices;
            if (vertices.Count == 0) return;

            Point last = vertices[^1]; // ^1 == vertices.Count - 1

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

        private void RedrawCanvas()
        {
            DrawingCanvas.Children.Clear();

            foreach (var polygon in drawer.Polygons)
            {
                DrawPolygon(polygon);
            }

            if (drawer.Current != null && previewLine != null)
            {
                DrawingCanvas.Children.Add(previewLine);
            }
        }

        private void DrawPolygon(PolygonDrawerOOP.Model.Polygon polygon)
        {
            var vertices = polygon.Vertices;
            if (vertices.Count < 2) return;

            for (int i = 1; i < vertices.Count; i++)
            {
                DrawingCanvas.Children.Add(new Line
                {
                    X1 = vertices[i - 1].X,
                    Y1 = vertices[i - 1].Y,
                    X2 = vertices[i].X,
                    Y2 = vertices[i].Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                });
            }
        }


        private void RemovePreviewLine()
        {
            if (previewLine != null)
            {
                DrawingCanvas.Children.Remove(previewLine);
                previewLine = null;
            }
        }
    }
}