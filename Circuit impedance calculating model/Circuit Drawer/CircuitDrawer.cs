#region - Using -

using System.Collections.Generic;
using System.Drawing;
using CircuitModeling.Circuits;
using CircuitModeling.Elements;
using IComponent = CircuitModeling.IComponent;

#endregion


namespace CircuitDrawing
{
    /// <summary>
    /// Класс отрисовки цепей.
    /// </summary>
    public class CircuitDrawer
    {
        #region - Private fields-

        #region - Constants -

        /// <summary>
        /// Длина одного элемента.
        /// </summary>
        private const int ElementLength = 80;

        private const int ConnectingLineLength = 15;

        #endregion

        /// <summary>
        /// Переменная, задающая цвет линий.
        /// </summary>
        private readonly Pen _pen;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор. Инициализирует цвет линий.
        /// </summary>
        public CircuitDrawer(Color color)
        {
            _pen = new Pen(color);
        }

        /// <summary>
        /// Пустой конструктор. Иниализирует цвет ручки.
        /// </summary>
        public CircuitDrawer() : this(Color.Black) { }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод, овтечающий за вызов отрисовки схем и отрисовки клем.
        /// </summary>
        /// <param name="circuit">Входная схема</param>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <returns>Изображение входной цепи</returns>
        public Bitmap DrawCircuit(IComponent circuit, Bitmap bmp, int x, int y)
        {
            DrawKlemme(bmp, x, y);
            if (circuit is SerialCircuit)
            {
                DrawSerialCircuit((SerialCircuit) circuit, bmp, x, y);
                DrawKlemme(bmp, x + CalculateSerialCircuitLength((SerialCircuit) circuit) + 10, y);
            }
            else if (circuit is ParallelCircuit)
            {
                DrawParallelCircuit((ParallelCircuit) circuit, bmp, x, y);
                DrawKlemme(bmp, x + CalculateParallelCircuitLength((ParallelCircuit) circuit) + 10, y);
            }
            return bmp;
        }

        #endregion

        #region - Private methods -

        #region - Circuits drawing -

        /// <summary>
        /// Метод, для отрисовки последовательной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длинны, при вхождении цепи в параллельную схему</param>
        /// <returns>Изображение входной последовательной цепи</returns>
        private Bitmap DrawSerialCircuit(SerialCircuit circuit, Bitmap bmp, int x, int y, int length = 0)
        {
            int startX = x;
            foreach (IComponent component in circuit.Circuit)
            {
                if (component is Resistor)
                {
                    bmp = DrawResistor(bmp, x, y);
                    x += ElementLength;
                }
                else if (component is Inductor)
                {
                    bmp = DrawInductor(bmp, x, y);
                    x += ElementLength;
                }
                else if (component is Capacitor)
                {
                    bmp = DrawCapacitor(bmp, x, y);
                    x += ElementLength;
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
                graph.DrawLine(_pen, x, y, startX + length - ConnectingLineLength*2, y);
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
        /// <returns>Изображение входной параллельной цепи</returns>
        private Bitmap DrawParallelCircuit(ParallelCircuit circuit, Bitmap bmp, int x, int y)
        {
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + ConnectingLineLength, y);
            int startY = y;
            int height = CalculateParallelCircuitHeight(circuit);
            y -= height * (circuit.Circuit.Count - 1) / 2;
            x += ConnectingLineLength;
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
            graph.DrawLine(_pen, x, h, x, y - height);
            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit) - ConnectingLineLength*2, h,
                x + CalculateParallelCircuitLength(circuit) - ConnectingLineLength*2, y - height);
            graph.DrawLine(_pen, x + CalculateParallelCircuitLength(circuit) - ConnectingLineLength*2, startY,
                x + CalculateParallelCircuitLength(circuit) - ConnectingLineLength, startY);
            return bmp;
        }

        #endregion

        #region - Elements drawing -

        /// <summary>
        /// Метод для отрисовки клемы.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        private void DrawKlemme(Bitmap bmp, int x, int y)
        {
            const int klemmeDiameter = 10;
            const int klemmeCrossingLineHeight = 20;
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawArc(_pen, x - klemmeDiameter, y - klemmeDiameter / 2, klemmeDiameter, klemmeDiameter, 0, 360);
            graph.DrawLine(_pen, x, y - klemmeCrossingLineHeight/2, x - klemmeDiameter, y + klemmeCrossingLineHeight/2);
        }

        /// <summary>
        /// Метод для отрисовки резистора.
        /// </summary>
        /// <param name="bmp">Рисунок</param>
        /// <param name="x">Входное значение координаты по оси Ох</param>
        /// <param name="y">Входное значение координаты по оси Оу</param>
        /// <param name="length">Необязательный параметр длины, при вхождении элемента в параллельную схему</param>
        /// <returns>Изображение резистора</returns>
        private Bitmap DrawResistor(Bitmap bmp, int x, int y, int length = 0)
        {
            const int resistorHeight = 10;
            const int resistorLength = 50;
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + ConnectingLineLength, y);
            graph.DrawRectangle(_pen, x + ConnectingLineLength, y - resistorHeight/2, resistorLength, resistorHeight);
            graph.DrawLine(_pen, x + ConnectingLineLength + resistorLength, y, x + ElementLength, y);
            if (x + ElementLength < x + length)
            {
                graph.DrawLine(_pen, x + ElementLength, y, x + length - ConnectingLineLength*2, y);
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
        /// <returns>Изображение катушки индуктивности</returns>
        private Bitmap DrawInductor(Bitmap bmp, int x, int y, int length = 0)
        {
            const int inductorHeight = 10;
            const int inductorArcDiamter = 16;
            const int inductorLength = 48;
            const int inductorConnectingLine = 16;
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + inductorConnectingLine, y);
            graph.DrawArc(_pen, x + inductorConnectingLine, y - inductorHeight/2,
                inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawArc(_pen, x + inductorConnectingLine + inductorArcDiamter,
                y - inductorHeight/2, inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawArc(_pen, x + inductorConnectingLine + inductorArcDiamter*2,
                y - inductorHeight/2, inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawLine(_pen, x + inductorConnectingLine + inductorLength, y, x + ElementLength, y);
            if (x + ElementLength < x + length)
            {
                graph.DrawLine(_pen, x + ElementLength, y, x + length - ConnectingLineLength*2, y);
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
        /// <returns>Изображение конденсатора</returns>
        private Bitmap DrawCapacitor(Bitmap bmp, int x, int y, int length = 0)
        {
            const int capacitorConnectingLine = 35;
            const int capacitorHeight = 30;
            const int capacitorLength = 10;
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, x, y, x + capacitorConnectingLine, y);
            graph.DrawLine(_pen, x + capacitorConnectingLine, y + capacitorHeight/2,
                x + capacitorConnectingLine, y - capacitorHeight/2);
            graph.DrawLine(_pen, x + capacitorConnectingLine + capacitorLength,
                y + capacitorHeight/2, x + capacitorConnectingLine + capacitorLength, y - capacitorHeight/2);
            graph.DrawLine(_pen, x + capacitorConnectingLine + capacitorLength,
                y, x + ElementLength, y);
            if (x + ElementLength < x + length)
            {
                graph.DrawLine(_pen, x + ElementLength, y, x + length - ConnectingLineLength*2, y);
            }
            return bmp;
        }

        #endregion

        #region - Calculating circuits length | height -

        /// <summary>
        /// Метод, для вычисления длины последовательной цепи в схеме.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns>Длину входной последовательной цепи</returns>
        private int CalculateSerialCircuitLength(SerialCircuit circuit)
        {
            int length = 0;
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is IElement)
                {
                    length += ElementLength;
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
        /// <returns>Длину входной параллельной цепи</returns>
        private int CalculateParallelCircuitLength(ParallelCircuit circuit)
        {
            const int defaulLength = 110;
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
                return length + ConnectingLineLength*2;
            }
            return defaulLength;
        }

        /// <summary>
        /// Метод, для вычисления высоты между уровнями в параллельной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns>Ширину между ветвями входной параллельной цепи</returns>
        private int CalculateParallelCircuitHeight(ParallelCircuit circuit)
        {
            const int defaultHeight = 40;
            int height = 1;
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is SerialCircuit)
                {
                    SerialCircuit serial = circuit.Circuit[i] as SerialCircuit;
                    for (int j = 0; j < serial.Circuit.Count; j++)
                    {
                        if (serial.Circuit[j] is ParallelCircuit)
                        {
                            height += CalculateParallelCircuitHeight(serial.Circuit[j] as ParallelCircuit);
                        }
                    }
                    return height * 2;
                }
            }
            return defaultHeight;
        }

        #endregion

        #endregion
    }
}
