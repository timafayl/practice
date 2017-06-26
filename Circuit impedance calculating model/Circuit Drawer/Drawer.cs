#region - Using -

using Circuit_impedance_calculating_model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

#endregion


namespace Circuit_Drawer
{
    public class Drawer
    {
        private Pen _pen;

        public Drawer()
        {
            _pen = new Pen(Color.Black);
        }

        public Bitmap DrawResistor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 25, y);
            graph.DrawRectangle(_pen, x + 25, y + 10, x + 125, y - 10);
            graph.DrawLine(_pen, x + 125, y, x + 150, y);
            return bmp;
        }

        public Bitmap DrawInductor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 25, y);
            graph.DrawArc(_pen, x + 25, y + 20, 33, 20, 180, 0);
            graph.DrawArc(_pen, x + 58, y + 20, 33, 20, 180, 0);
            graph.DrawArc(_pen, x + 91, y + 20, 33, 20, 180, 0);
            graph.DrawLine(_pen, x + 124, y, x + 150, y);
            return bmp;
        }

        public Bitmap DrawCapacitor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 60, y);
            graph.DrawLine(_pen, x + 60, y + 25, x + 60, y - 25);
            graph.DrawLine(_pen, x + 90, y + 25, x + 90, y - 25);
            graph.DrawLine(_pen, x+90, y, x + 150, y);
            return bmp;
        }

        public Bitmap DrawSerialCircuit(List<IComponent> circuit, Bitmap bmp, int x, int y)
        {
            return bmp;
        }

        public Bitmap DrawParallelCircuit(Bitmap bmp, int x, int y)
        {
            return bmp;
        }
    }
}
