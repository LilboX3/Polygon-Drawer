using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonDrawerOOP.Model
{
    public class Polygon
    {
        public List<System.Windows.Point> Vertices { get; } = new();
        public bool IsClosed { get; set; }

        public void AddVertex(System.Windows.Point p)
        {
            if (!IsClosed)
                Vertices.Add(p);
        }

        public void Close()
        {
            IsClosed = true;
        }
    }
}
