using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace Circuit_impedance_calculating_model.Elements
{
    public class Resistor: IElement
    {
        #region - Private fields -

        private string _name;

        private double _value;

        #endregion

        #region - Events -

        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        public Resistor() { }

        public Resistor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region - Public properties -

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
                        " превышать трех симвллов. Наименование резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Имя резистора в цепи должно начинаться" +
                        " с латинской буквы 'R' после которой должен идти порядковый номер резистора в цепи.");
                }
                _name = value;
            }
        }

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

        public Complex CalculateZ(double frequency)
        {
            return (Value + 0 * Complex.ImaginaryOne);
        }

        #endregion
    }
}
