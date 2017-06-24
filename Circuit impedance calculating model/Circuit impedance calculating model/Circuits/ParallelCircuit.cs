using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Circuit_impedance_calculating_model.Circuits
{
    class ParallelCircuit: ICircuit
    {
        #region - Private fields -

        private string _name;

        private List<IComponent> _circuit;

        #endregion

        #region - Events -

        public event EventHandler CircuitChanged;

        #endregion

        #region - Constructors -
        #endregion

        #region - Public Properties -

        public string Name
        {
            get { return _name; }
            set
            {
                string pattern1 = @"^circuit\d$";
                string pattern2 = @"^circuit\d{2}$";
                value = value.ToLower();
                if (value.Length > 9)
                {
                    throw new ArgumentException("Наименование цепи не должно" +
                        " превышать девяти символов. Наименование цепи должно начинаться" +
                        " со слова 'circuit' после которого должен идти порядковый номер цепи в схеме.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException(" Наименование цепи должно начинаться" +
                        " со слова 'circuit' после которого должен идти порядковый номер цепи в схеме.");
                }
                _name = value;
            }
        }

        public List<IComponent> Circuit { get; set; }

        #endregion

        #region - Public methods -

        public Complex CalculateZ(double frequency)
        {
            Complex impedance = new Complex();
            foreach (IComponent component in _circuit)
            {
                impedance += 1 / component.CalculateZ(frequency);
            }
            return 1 / impedance;
        }

        #endregion
    }
}
