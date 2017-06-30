#region - Using -

using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace CircuitModeling.Elements
{
    /// <summary>
    /// Класс, описывающий резистор.
    /// </summary>
    public class Resistor: IElement
    {
        #region - Private fields -

        /// <summary>
        /// Поле, содержащее наименование элемента.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле, содержащее значение элемента.
        /// </summary>
        private double _value;

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее при изменении значения элемента.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Resistor() { }

        /// <summary>
        /// Конструктор с входными параметрами.
        /// </summary>
        /// <param name="name">Наименование элемента</param>
        /// <param name="value">Значение элемента</param>
        public Resistor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region - Public properties -

        /// <summary>
        /// Свойство для наименования элемента.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                string pattern = @"^R\d{1,2}$"; //задает значение типа "R1" или "R10"
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    throw new ArgumentException("Наименование резистора не должно" +
                        " превышать трех символов. Наименование резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Наименование резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство для сопротивления.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Значение сопротивления не должно быть меньше нуля.");
                }
                if (double.IsNaN(value))
                {
                    throw new ArgumentException("Значение сопротивления не должно быть нулевым.");
                }
                if (double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value))
                {
                    throw new ArgumentException("Значение сопротивления не должно быть равным бесконечности.");
                }
                if (_value == value)
                {
                    throw new ArgumentException("Вы пытаетесь присвоить переменной существующее значение.");
                }
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод рассчета импеданса элемента.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс элемента</returns>
        public Complex CalculateZ(double frequency)
        {
            if (frequency < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть меньше нуля.");
            }
            if (double.IsNaN(frequency))
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть нулевым.");
            }
            if (double.IsNegativeInfinity(frequency) || double.IsPositiveInfinity(frequency))
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть равным бесконечности.");
            }
            return new Complex(Value, 0);
        }

        #endregion
    }
}
