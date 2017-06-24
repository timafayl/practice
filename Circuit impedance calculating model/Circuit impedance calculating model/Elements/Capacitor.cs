using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace Circuit_impedance_calculating_model.Elements
{
    public class Capacitor: IElement
    {
        #region - Private fields -

        private string _name;

        private double _value;

        #endregion

        #region - Events -

        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        public Capacitor() { }

        public Capacitor(string name, double value)
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
                    throw new ArgumentException("Наименование конденсатора не должно" +
                        " превышать трех символов. Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                _name = value;
            }
        }

        public double Value
        {
            get
            { return _value; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Значение конденсатора не должно быть меньше нуля.");
                }
                _value = value;
            }
        }

        #endregion

        #region - Public methods -

        public Complex CalculateZ(double frequency)
        {
            return 0 + Complex.ImaginaryOne * 1 / (Math.PI * frequency * _value);
        }

        #endregion
    }
}
