#region - Using -

using System;
using System.Drawing;
using System.Linq;
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

        /// <summary>
        /// Высота одного элемента.
        /// </summary>
        private const int ElementHeight = 30;

        /// <summary>
        /// Длина соеденительной линии между компонентами цепи.
        /// </summary>
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
        /// <param name="component">Входная схема</param>
        /// <returns>Изображение входной цепи</returns>
        public Bitmap Draw(IComponent component)
        {
            if (component is SerialCircuit)
            {
                return DrawCircuit((SerialCircuit)component);
            }
            if (component is ParallelCircuit)
            {
                return DrawCircuit((ParallelCircuit)component);
            }
            if (component is IElement)
            {
                return DrawElement((IElement)component);
            }
            throw new NotImplementedException();
        }

        #endregion

        #region - Private methods -

        #region - Circuits drawing -

        /// <summary>
        /// Метод, для отрисовки последовательной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns>Изображение входной последовательной цепи</returns>
        private Bitmap DrawCircuit(SerialCircuit circuit)
        {
            var bmpList = circuit.CircuitComponents.Select(Draw).ToList(); //объяснение ниже
            //var bmpList = new List<Bitmap>();
            //foreach (var component in circuit.CircuitComponents)
            //{
            //    bmpList.Add(Draw(component));
            //}

            int circuitLength = 0;
            int circuitHeight = 0;

            foreach (var bmpItem in bmpList)
            {
                circuitLength += bmpItem.Width;
                if (circuitHeight < bmpItem.Height)
                {
                    circuitHeight = bmpItem.Height;
                }
            }

            var bmp = new Bitmap(circuitLength, circuitHeight);
            var graph = Graphics.FromImage(bmp);

            int x = 0;
            int y = circuitHeight / 2;

            foreach (var component in circuit.CircuitComponents)
            {
                graph.DrawImage(Draw(component), x, y - Draw(component).Height / 2);
                x += Draw(component).Width;
          }
            return bmp;
        }

        /// <summary>
        /// Метод, для отрисовки параллельной цепи.
        /// </summary>
        /// <param name="circuit">Входная схема цепи</param>
        /// <returns>Изображение входной параллельной цепи</returns>
        private Bitmap DrawCircuit(ParallelCircuit circuit)
        {
            var bmpList = circuit.CircuitComponents.Select(Draw).ToList(); //объяснение ниже
            //List<Bitmap> bmpList = new List<Bitmap>();
            //foreach (var component in circuit.CircuitComponents)
            //{
            //    bmpList.Add(Draw(component));
            //}

            int circuitLength = 0;
            int circuitHeight = 0;
            int parallelCircuitLevelHeight = 0;

            foreach (var bmpItem in bmpList)
            {
                circuitHeight += bmpItem.Height;

                if (parallelCircuitLevelHeight < bmpItem.Height)
                {
                    parallelCircuitLevelHeight = bmpItem.Height;
                }

                if (circuitLength < bmpItem.Width)
                {
                    circuitLength = bmpItem.Width;
                }
            }

            circuitLength += ConnectingLineLength * 2;
            circuitHeight += 40 * (circuit.CircuitComponents.Count - 1);

            var bmp = new Bitmap(circuitLength, circuitHeight);
            var graph = Graphics.FromImage(bmp);

            int x = 0;
            int y = circuitHeight / 2;
            int startY = y; //для отрисовки второй горизонтальной соединяющей линии

            graph.DrawLine(_pen, x, y, x + ConnectingLineLength, y); //первая горизонтальная соединяющая линия

            y -= parallelCircuitLevelHeight * (circuit.CircuitComponents.Count - 1) / 2;
            x += ConnectingLineLength;

            var h = y;  //фиксация высоты для отрисовки вертикальных соединяющих линий

            foreach (IComponent component in circuit.CircuitComponents)
            {
                graph.DrawImage(Draw(component), x, y - Draw(component).Height / 2);

                if (x + Draw(component).Width < x + circuitLength)
                {
                    graph.DrawLine(_pen, x + Draw(component).Width, y,
                        x + circuitLength - ConnectingLineLength * 2, y);
                }
                y += parallelCircuitLevelHeight;
            }

            graph.DrawLine(_pen, x, h, x, y - parallelCircuitLevelHeight); //первая вертикальная соединительная линия
            graph.DrawLine(_pen, x + circuitLength - ConnectingLineLength * 2, h,
                x + circuitLength - ConnectingLineLength * 2, y - parallelCircuitLevelHeight); //вторая вертикальная соединительная линия
            graph.DrawLine(_pen, x + circuitLength - ConnectingLineLength * 2, startY,
                x + circuitLength - ConnectingLineLength, startY); //вторая горизонтльная соединяющая линия
            return bmp;
        }

        #endregion

        #region - Elements drawing -

        /// <summary>
        /// Метод, овтечающий за вызов отрисовки элементов.
        /// </summary>
        /// <param name="element">Входной элемент</param>
        /// <returns>Изображение входного элемента</returns>
        private Bitmap DrawElement(IElement element)
        {
            if (element is Resistor)
            {
                return DrawResistor();
            }
            if (element is Capacitor)
            {
                return DrawCapacitor();
            }
            if (element is Inductor)
            {
                return DrawInductor();
            }
            throw new NotImplementedException();
        }

        //TODO дорисовать клему.

        /// <summary>
        /// Метод для отрисовки резистора.
        /// </summary>
        /// <returns>Изображение резистора</returns>
        private Bitmap DrawResistor()
        {
            const int resistorLength = 50;
            const int resistorHeight = 10;
            Bitmap bmp = new Bitmap(ElementLength, ElementHeight);
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, 0, ElementHeight / 2, ConnectingLineLength, ElementHeight / 2);
            graph.DrawRectangle(_pen, ConnectingLineLength, ElementHeight / 2 - resistorHeight / 2, resistorLength, resistorHeight);
            graph.DrawLine(_pen, ConnectingLineLength + resistorLength, ElementHeight / 2, ElementLength, ElementHeight / 2);
            return bmp;
        }

        /// <summary>
        /// Метод для отрисовки катушки индуктивности.
        /// </summary>
        /// <returns>Изображение катушки индуктивности</returns>
        private Bitmap DrawInductor()
        {
            const int inductorLength = 48;
            const int inductorHeight = 10;
            const int inductorArcDiamter = 16;
            const int inductorConnectingLine = 16;
            Bitmap bmp = new Bitmap(ElementLength, ElementHeight);
            Graphics graph = Graphics.FromImage(bmp);
            graph.DrawLine(_pen, 0, ElementHeight / 2, inductorConnectingLine, ElementHeight / 2);
            graph.DrawArc(_pen, inductorConnectingLine, ElementHeight / 2 - inductorHeight / 2,
                inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawArc(_pen, inductorConnectingLine + inductorArcDiamter, ElementHeight / 2 - inductorHeight / 2,
                inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawArc(_pen, inductorConnectingLine + inductorArcDiamter * 2, ElementHeight / 2 - inductorHeight / 2,
                inductorArcDiamter, inductorHeight, 360, -180);
            graph.DrawLine(_pen, inductorConnectingLine + inductorLength, ElementHeight / 2, ElementLength, ElementHeight / 2);
            return bmp;
        }

        /// <summary>
        /// Метод для отрисовки кондесатора.
        /// </summary>
        /// <returns>Изображение конденсатора</returns>
        private Bitmap DrawCapacitor()
        {
            const int capacitorLength = 10;
            const int capacitorConnectingLine = 35;
            Bitmap bmp = new Bitmap(ElementLength, ElementHeight);
            Graphics graph = Graphics.FromImage(bmp);

            graph.DrawLine(_pen, 0, ElementHeight / 2, capacitorConnectingLine, ElementHeight / 2);

            graph.DrawLine(_pen, capacitorConnectingLine, 0,
                capacitorConnectingLine, ElementHeight);

            graph.DrawLine(_pen, capacitorConnectingLine + capacitorLength,
                0, capacitorConnectingLine + capacitorLength, ElementHeight);

            graph.DrawLine(_pen, capacitorConnectingLine + capacitorLength,
                ElementHeight / 2, ElementLength, ElementHeight / 2);
            return bmp;
        }

        #endregion

        #endregion
    }
}
