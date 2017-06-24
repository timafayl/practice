using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace Circuit_impedance_calculating_model.Elements
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
        /// Свойство-аксессор для поля _name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                string pattern1 = @"^C\d$";
                string pattern2 = @"^C\d{2}$";
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    throw new ArgumentException("Наименование резистора не должно" +
                        " превышать трех символов. Наименование резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Наименование резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство-аксессор для поля _value.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Значение резистора не должно быть меньше нуля.");
                }
                _value = value;
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
            return (Value + 0 * Complex.ImaginaryOne);
        }

        #endregion
    }
}
