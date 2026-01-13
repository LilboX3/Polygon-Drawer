using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PolygonDrawerOOP.Model
{
    public class AddVertexCommand : ICommand
    {
        private Polygon polygon;
        private Point point;

        public AddVertexCommand(Polygon polygon, Point point)
        {
            this.polygon = polygon;
            this.point = point;
        }

        public void Execute()
        {
            polygon.AddVertex(point);
        }

        public void Undo()
        {
            polygon.Vertices.RemoveAt(polygon.Vertices.Count - 1);
        }
    }
}
