using System;
using System.Numerics;


namespace Circuit_impedance_calculating_model.Elements
{
    public class Capacitor: IElement
    {
        private string _name;
        private double _value;
        public string Name { get; set; }
        public double Value { get; set; }

        public Complex CalculateZ(double frequency)
        {
            return 0 + Complex.ImaginaryOne * 1/(Math.PI * frequency * _value);
        }

        public event EventHandler ValueChanged;

    }
}
