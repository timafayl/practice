using System;
using System.Numerics;


namespace Circuit_impedance_calculating_model.Elements
{
    public class Resistor: IElement
    {
        private string _name;
        private double _value;
        public string Name { get; set; }
        public double Value { get; set; }

        public Complex CalculateZ(double frequency)
        {
            return (Value + 0*Complex.ImaginaryOne);
        }

        public event EventHandler ValueChanged;
    }
}
