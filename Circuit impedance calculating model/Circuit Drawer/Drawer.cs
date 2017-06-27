#region - Using -

using System.Collections.Generic;
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

        public Bitmap DrawCircuit(IComponent circuit, Bitmap bmp, int x, int y)
        {
            DrawKlemme(bmp, x, y);
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 15, y);
            if (circuit is SerialCircuit)
            {
                DrawSerialCircuit(circuit as SerialCircuit, bmp, x + 15, y);
                graph.DrawLine(_pen, x + CalculateSerialCircuitLength(circuit as SerialCircuit) + 15, y,
                    x + CalculateSerialCircuitLength(circuit as SerialCircuit) + 30, y);
                DrawKlemme(bmp, x + CalculateSerialCircuitLength(circuit as SerialCircuit) + 40, y);
            }
            else if (circuit is ParallelCircuit)
            {
                DrawParallelCircuit(circuit as ParallelCircuit, bmp, x + 15, y);
                graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit as ParallelCircuit), y,
                    x + CalculateParallelCircuitLength(circuit as ParallelCircuit) + 15, y);
                DrawKlemme(bmp, x + CalculateParallelCircuitLength(circuit as ParallelCircuit) + 35, y);
            }
            return bmp;
        }

        #endregion

        #region - Private methods -

        private void DrawKlemme(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawArc(_pen, x - 10, y - 5, 10, 10, 0, 360);
            graph.DrawLine(_pen, x, y - 10, x - 10, y + 10);
        }

        private Bitmap DrawResistor(Bitmap bmp, int x, int y, int length = 0)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 15, y);
            graph.DrawRectangle(_pen, x + 15, y - 5, 50, 10);
            graph.DrawLine(_pen, x + 65, y, x + 80, y);
            if (x + 80 < x + length)
            {
                graph.DrawLine(_pen, x + 80, y, x + length, y);
            }
            return bmp;
        }

        private Bitmap DrawInductor(Bitmap bmp, int x, int y, int length = 0)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 16, y);
            graph.DrawArc(_pen, x + 16, y - 5, 16, 10, 360, -180);
            graph.DrawArc(_pen, x + 32, y - 5, 16, 10, 360, -180);
            graph.DrawArc(_pen, x + 48, y - 5, 16, 10, 360, -180);
            graph.DrawLine(_pen, x + 64, y, x + 80, y);
            if (x + 80 < x + length)
            {
                graph.DrawLine(_pen, x + 80, y, x + length, y);
            }
            return bmp;
        }

        private Bitmap DrawCapacitor(Bitmap bmp, int x, int y, int length = 0)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 35, y);
            graph.DrawLine(_pen, x + 35, y + 15, x + 35, y - 15);
            graph.DrawLine(_pen, x + 45, y + 15, x + 45, y - 15);
            graph.DrawLine(_pen, x + 45, y, x + 80, y);
            if (x + 80 < x + length)
            {
                graph.DrawLine(_pen, x + 80, y, x + length, y);
            }
            return bmp;
        }

        private Bitmap DrawSerialCircuit(SerialCircuit circuit, Bitmap bmp, int x, int y, int length = 0)
        {
            int startX = x;
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
                    x += CalculateParallelCircuitLength(component as ParallelCircuit);
                }
            }
            if (x < startX + length)
            {
                Graphics graph = Graphics.FromImage(bmp);
                graph.DrawLine(_pen, x, y, startX + length, y);
            }
            return bmp;
        }

        private Bitmap DrawParallelCircuit(ParallelCircuit circuit, Bitmap bmp, int x, int y)
        {
            int k = y;
            int height = circuit.Circuit.Count * 15;
            y -= height * (circuit.Circuit.Count - 1)/2;
            var h = y;
            foreach (IComponent component in circuit.Circuit)
            {
                if (component is Resistor)
                {
                    bmp = DrawResistor(bmp, x, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else if (component is Inductor)
                {
                    bmp = DrawInductor(bmp, x, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else if (component is Capacitor)
                {
                    bmp = DrawCapacitor(bmp, x, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else
                {
                    bmp = DrawSerialCircuit(component as SerialCircuit, bmp, x, y, 
                        CalculateParallelCircuitLength(circuit));
                    y += height;
                }
            }
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, h, x, y - height);

            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit), h,
                x + CalculateParallelCircuitLength(circuit), y - height);

            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit), k,
                x + CalculateParallelCircuitLength(circuit) + 15, k);
            return bmp;
        }

        private int CalculateParallelCircuitLength(ParallelCircuit circuit)
        {
            List<SerialCircuit> serials = new List<SerialCircuit>();
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is SerialCircuit)
                {
                    serials.Add(circuit.Circuit[i] as SerialCircuit);
                }
            }
            if (serials.Count > 0)
            {
                int length = CalculateSerialCircuitLength(serials[0]);
                for (int i = 1; i < serials.Count; i++)
                {
                    if (CalculateSerialCircuitLength(serials[i]) > length)
                    {
                        length = CalculateSerialCircuitLength(serials[i]);
                    }
                }
                return length;
            }
            return 80;
        }

        private int CalculateSerialCircuitLength(SerialCircuit circuit)
        {
            int length = 0;
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is IElement)
                {
                    length += 80;
                }
                else if (circuit.Circuit[i] is ParallelCircuit)
                {
                    length += CalculateParallelCircuitLength(circuit.Circuit[i] as ParallelCircuit);
                }
            }
            return length;
        }

        #endregion
    }
}
