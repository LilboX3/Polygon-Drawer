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

        private Stack<ICommand> undoStack = new();
        private Stack<ICommand> redoStack = new();

        public void StartPolygon(System.Windows.Point start)
        {
            Current = new Polygon();
            Current.AddVertex(start);
            Polygons.Add(Current);
        }

        public void FinishPolygon()
        {
            Current?.Close();
            Current = null;
        }

        public void ExecuteCommand(ICommand cmd)
        {
            cmd.Execute();
            undoStack.Push(cmd);
            redoStack.Clear();
        }

        public void Undo()
        {
            if (!undoStack.Any()) return;

            var cmd = undoStack.Pop();
            cmd.Undo();
            redoStack.Push(cmd);
        }

        public void Redo()
        {
            if (!redoStack.Any()) return;

            var cmd = redoStack.Pop();
            cmd.Execute();
            undoStack.Push(cmd);
        }
    }
}
