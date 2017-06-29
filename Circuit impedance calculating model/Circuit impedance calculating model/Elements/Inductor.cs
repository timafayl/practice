#region - Using -

using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace Circuit_impedance_calculating_model.Elements
{
    /// <summary>
    /// Класс, описывающий катушку индуктивности.
    /// </summary>
    public class Inductor: IElement
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
        /// Событие, србатываюшее при изменении значения элемента.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Inductor() { }

        /// <summary>
        /// Конструктор с входными параметрами.
        /// </summary>
        /// <param name="name">Наименование элемента</param>
        /// <param name="value">Значение элемента</param>
        public Inductor(string name, double value)
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
                string pattern = @"^L\d{1,2}$"; //задает значение типа "L1" или "L10"
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    throw new ArgumentException("Наименование катушки индуктивности не должно" +
                        " превышать трех символов. Наименование катушки инлуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
                }
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Наименование катушки индуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство для значения элемента.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Значение индуктивности не должно быть меньше нуля.");
                }
                if (double.IsNaN(value))
                {
                    throw new ArgumentException("Значение индуктивности не должно быть нулевым.");
                }
                if (double.IsNegativeInfinity(value) || double.IsPositiveInfinity(value))
                {
                    throw new ArgumentException("Значение индуктивности не должно быть равным бесконечности.");
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
            return new Complex(0, 2 * Math.PI * frequency * Value);
        }

        #endregion
    }
}
