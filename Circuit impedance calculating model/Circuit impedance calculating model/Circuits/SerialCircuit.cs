using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;


namespace Circuit_impedance_calculating_model.Circuits
{
    class SerialCircuit: ICircuit
    {
        #region - Private fields -

        private string _name;

        private List<IComponent> _circuit;

        #endregion

        #region -Events-

        public event EventHandler CircuitChanged;

        #endregion

        #region -Constructors-



        #endregion

        #region - Public properties - 

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

        public List<IComponent> Circuit
        {
            get { return _circuit; }
            set { _circuit = value; }
        }

        #endregion

        #region - Public methods -

        public Complex CalculateZ(double frequency)
        {
            Complex impedance = new Complex();
            foreach (IComponent component in _circuit)
            {
                impedance += component.CalculateZ(frequency);
            }
            return impedance;
        }

        #endregion
    }
}
