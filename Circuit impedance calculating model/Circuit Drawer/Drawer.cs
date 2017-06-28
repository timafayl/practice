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

        //TODO: Вроде как можно ридонли сделать
        /// <summary>
        /// Переменная, задающая цвет линий.
        /// </summary>
        private Pen _pen;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор. Инициализирует цвет линий.
        /// </summary>
        public Drawer()
        {
            _pen = new Pen(Color.Black);
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод, овтечающий за вызов отрисовки схем и отрисовки клем.
        /// </summary>
        /// <param name="circuit">Входная схема</param>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <returns></returns>
        public Bitmap DrawCircuit(IComponent circuit, Bitmap bmp, int x, int y)
        {
            DrawKlemme(bmp, x, y);
            Graphics graph = Graphics.FromImage(bmp);

            if (circuit is SerialCircuit)
            {
                DrawSerialCircuit(circuit as SerialCircuit, bmp, x, y);

                DrawKlemme(bmp, x + CalculateSerialCircuitLength(circuit as SerialCircuit) + 10, y);
            }

            else if (circuit is ParallelCircuit)
            {
                DrawParallelCircuit(circuit as ParallelCircuit, bmp, x, y);

                DrawKlemme(bmp, x + CalculateParallelCircuitLength(circuit as ParallelCircuit) + 10, y);
            }
            return bmp;
        }

        #endregion

        #region - Private methods -

        #region - Elements' drawing -

        /// <summary>
        /// Метод для отрисовки клемы.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        private void DrawKlemme(Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawArc(_pen, x - 10, y - 5, 10, 10, 0, 360);
            graph.DrawLine(_pen, x, y - 10, x - 10, y + 10);
        }

        /// <summary>
        /// Метод для отрисовки резистора.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длинны, при вхождении элемента в параллельную схему</param>
        /// <returns></returns>
        private Bitmap DrawResistor(Bitmap bmp, int x, int y, int length = 0)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 15, y);
            graph.DrawRectangle(_pen, x + 15, y - 5, 50, 10);
            graph.DrawLine(_pen, x + 65, y, x + 80, y);
            if (x + 80 < x + length)
            {
                graph.DrawLine(_pen, x + 80, y, x + length - 30, y);
            }
            return bmp;
        }

        /// <summary>
        /// Метод для отрисовки катушки индуктивности.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длинны, при вхождении элемента в параллельную схему</param>
        /// <returns></returns>
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
                graph.DrawLine(_pen, x + 80, y, x + length - 30, y);
            }
            return bmp;
        }

        /// <summary>
        /// Метод для отрисовки кондесатора.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длинны, при вхождении элемента в параллельную схему</param>
        /// <returns></returns>
        private Bitmap DrawCapacitor(Bitmap bmp, int x, int y, int length = 0)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 35, y);
            graph.DrawLine(_pen, x + 35, y + 15, x + 35, y - 15);
            graph.DrawLine(_pen, x + 45, y + 15, x + 45, y - 15);
            graph.DrawLine(_pen, x + 45, y, x + 80, y);
            if (x + 80 < x + length)
            {
                graph.DrawLine(_pen, x + 80, y, x + length - 30, y);
            }
            return bmp;
        }

        #endregion

        #region - Circuits' drawing -

        /// <summary>
        /// Метод, для отрисовки последовательной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длинны, при вхождении цепи в параллельную схему</param>
        /// <returns></returns>
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
                graph.DrawLine(_pen, x, y, startX + length - 30, y);
            }
            return bmp;
        }

        /// <summary>
        /// Метод, для отрисовки параллельной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <returns></returns>
        private Bitmap DrawParallelCircuit(ParallelCircuit circuit, Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + 15, y);
            int startY = y;
            int height = CalculateParallelCircuitHeight(circuit);
            y -= height * (circuit.Circuit.Count - 1) / 2;
            var h = y;
            foreach (IComponent component in circuit.Circuit)
            {
                if (component is Resistor)
                {
                    bmp = DrawResistor(bmp, x + 15, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else if (component is Inductor)
                {
                    bmp = DrawInductor(bmp, x + 15, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else if (component is Capacitor)
                {
                    bmp = DrawCapacitor(bmp, x + 15, y, CalculateParallelCircuitLength(circuit));
                    y += height;
                }
                else
                {
                    bmp = DrawSerialCircuit(component as SerialCircuit, bmp, x + 15, y,
                        CalculateParallelCircuitLength(circuit));
                    y += height;
                }
            }
            graph.DrawLine(_pen, x + 15, h, x + 15, y - height);
            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit) - 15, h,
                x + CalculateParallelCircuitLength(circuit) - 15, y - height);
            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit) - 15, startY,
                x + CalculateParallelCircuitLength(circuit), startY);
            return bmp;
        }

        #endregion

        #region - Calculating circuits' length | height -

        /// <summary>
        /// Метод, для вычисления длины последовательной цепи в схеме.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод, для вычисления длины параллельной цепи в схеме.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns></returns>
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
                return length + 30;
            }
            return 110;
        }

        /// <summary>
        /// Метод, для вычисления высоты между уровнями в параллельной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns></returns>
        private int CalculateParallelCircuitHeight(ParallelCircuit circuit)
        {
            int count = 1;
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is SerialCircuit)
                {
                    SerialCircuit serial = circuit.Circuit[i] as SerialCircuit;
                    for (int j = 0; j < serial.Circuit.Count; j++)
                    {
                        if (serial.Circuit[j] is ParallelCircuit)
                        {
                            count += CalculateParallelCircuitHeight(serial.Circuit[j] as ParallelCircuit);
                        }
                    }
                    if (count > 4)
                    {
                        return count * 2;
                    }
                    if (count <= 4 && count > 2)
                    {
                        return count;
                    }
                }
            }
            return 30;
        }

        #endregion

        #endregion
    }
}
