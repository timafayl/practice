using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;


namespace Circuit_impedance_calculating_model.Elements
{
    public class Inductor: IElement
    {
        #region - Private fields -

        private string _name;

        private double _value;

        #endregion

        #region - Events -

        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        public Inductor() { }

        public Inductor(string name, double value)
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
                string pattern1 = @"^L\d$";
                string pattern2 = @"^L\d{2}$";
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    throw new ArgumentException("Наименование катушки инлуктивности не должно" +
                        " превышать трех симвллов. Наименование катушки инлуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Наименование катушки индуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
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
                    throw new ArgumentException("Значение индуктивности не должно быть меньше нуля.");
                }
                _value = value;
            }
        }

        #endregion

        #region - Public methods -

        public Complex CalculateZ(double frequency)
        {
            return 0 + Math.PI * frequency * Value * Complex.ImaginaryOne;
        }

        #endregion
    }
}
