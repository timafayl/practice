#region - Using -

using System;
using System.Globalization;
using System.Net.Http;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace CircuitModeling.Elements
{
    /// <summary>
    /// Класс, описывающий коденсатор. 
    /// </summary>
    public class Capacitor: IElement
    {
        #region - Private fields -

        /// <summary>
        /// Поле, задающее наименование элемента.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле, задающее значение элемента.
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
        /// Пустой конструтор.
        /// </summary>
        public Capacitor() { }

        /// <summary>
        /// Конструктор с входными параметрами.
        /// </summary>
        /// <param name="name">Наименование элемента</param>
        /// <param name="value">Значение элемента</param>
        public Capacitor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Свойство для наименования элемента.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                string pattern = @"^C\d{1,2}$";   //задает значение типа "C1" или "C10"
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    throw new ArgumentException("Наименование конденсатора не должно" +
                        " превышать трех символов. Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство для значения элемента.
        /// </summary>
        public double Value
        {
            get
            { return _value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Значение ёмкости не должно быть меньше нуля.");
                }
                if (double.IsNaN(value))
                {
                    throw new ArgumentException("Значение ёмкости не должно быть нулевым.");
                }
                if (double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value))
                {
                    throw new ArgumentException("Значение ёмкости не должно быть равным бесконечности.");
                }
                if (Math.Abs(value - _value) < float.Epsilon)
                {
                    return;
                }
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод для рассчета импеданса элемента.
        /// </summary>
        /// <param name="frequency">Входная частота.</param>
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
            return new Complex(0, -1/(2 * Math.PI * frequency * _value));
        }

        #endregion
    }
}
