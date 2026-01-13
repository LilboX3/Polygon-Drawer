using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonDrawerOOP.Model
{
    public class ClosePolygonCommand: ICommand
    {
        private Polygon polygon;

        public ClosePolygonCommand(Polygon polygon)
        {
            this.polygon = polygon;
        }

        public void Execute()
        {
            polygon.Close();
        }

        public void Undo()
        {
            polygon.IsClosed = false;
        }
    }
}
