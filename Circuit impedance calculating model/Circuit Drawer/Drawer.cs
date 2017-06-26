#region - Using -

using Circuit_impedance_calculating_model;
using System.Drawing;
using Circuit_impedance_calculating_model.Circuits;
using Circuit_impedance_calculating_model.Elements;

#endregion


namespace Circuit_Drawer
{
    public class Drawer
    {
        #region - Private fields-

        private Pen _pen;

        #endregion

        #region - Constructors -

        public Drawer()
        {
            _pen = new Pen(Color.Black);
        }

        #endregion

        #region - Public methods -

        public Bitmap DrawResistor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 15, y);
            graph.DrawRectangle(_pen, x + 15, y - 5, 50, 10);
            graph.DrawLine(_pen, x + 65, y, x + 80, y);
            return bmp;
        }

        public Bitmap DrawInductor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 16, y);
            graph.DrawArc(_pen, x + 16, y - 5, 16, 10, 360, -180);
            graph.DrawArc(_pen, x + 32, y - 5, 16, 10, 360, -180);
            graph.DrawArc(_pen, x + 48, y - 5, 16, 10, 360, -180);
            graph.DrawLine(_pen, x + 64, y, x + 80, y);
            return bmp;
        }

        public Bitmap DrawCapacitor(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 35, y);
            graph.DrawLine(_pen, x + 35, y + 15, x + 35, y - 15);
            graph.DrawLine(_pen, x + 45, y + 15, x + 45, y - 15);
            graph.DrawLine(_pen, x + 45, y, x + 80, y);
            return bmp;
        }

        public Bitmap DrawCircuit(IComponent circuit, Bitmap bmp, int x, int y)
        {
            if (circuit is SerialCircuit)
            {
                DrawSerialCircuit(circuit as SerialCircuit, bmp, x, y);
            }
            else if (circuit is ParallelCircuit)
            {
                DrawParallelCircuit(circuit as ParallelCircuit, bmp, x, y);
            }
            return bmp;
        }

        public Bitmap DrawSerialCircuit(SerialCircuit circuit, Bitmap bmp, int x, int y)
        {
            foreach (IComponent component in circuit.Circuit)
            {
                if (component is Resistor)
                {
                    bmp = DrawResistor(bmp, x, y);
                    x += 80;
                }
                else if (component is Inductor)
                {
                    bmp = DrawInductor(bmp, x, y);
                    x += 80;
                }
                else if (component is Capacitor)
                {
                    bmp = DrawCapacitor(bmp, x, y);
                    x += 80;
                }
                else
                {
                    bmp = DrawParallelCircuit(component as ParallelCircuit, bmp, x, y);
                }
            }
            return bmp;
        }

        public Bitmap DrawParallelCircuit(ParallelCircuit circuit, Bitmap bmp, int x, int y)
        {
            y -= (30 * circuit.Circuit.Count)/2;
            var h = y;
            foreach (IComponent component in circuit.Circuit)
            {
                if (component is Resistor)
                {
                    bmp = DrawResistor(bmp, x, y);
                    y += 30;
                }
                else if (component is Inductor)
                {
                    bmp = DrawInductor(bmp, x, y);
                    y += 30;
                }
                else if (component is Capacitor)
                {
                    bmp = DrawCapacitor(bmp, x, y);
                    y += 30;
                }
                else
                {
                    bmp = DrawSerialCircuit(component as SerialCircuit, bmp, x, y);
                    y += 30;
                }
            }
            return bmp;
        }

        #endregion
    }
}
