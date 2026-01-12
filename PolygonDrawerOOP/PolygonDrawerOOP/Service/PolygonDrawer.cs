using PolygonDrawerOOP.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PolygonDrawerOOP.Service
{
    public class PolygonDrawer
    {
        public List<Polygon> Polygons { get; } = new();
        public Polygon? Current { get; private set; }

        public void StartPolygon(System.Windows.Point start)
        {
            Current = new Polygon();
            Current.AddVertex(start);
            Polygons.Add(Current);
        }

        public void AddVertex(System.Windows.Point p)
        {
            Current?.AddVertex(p);
        }

        public void FinishPolygon()
        {
            Current?.Close();
            Current = null;
        }
    }
}
