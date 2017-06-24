using System;
using System.Collections.Generic;
using System.Numerics;


namespace Circuit_impedance_calculating_model.Circuits
{
    class SerialCircuit: ICircuit
    {
        private string _name;
        private List<IComponent> _circuit;

        public string Name { get; set; }

        public List<IComponent> Circuit { get; set; }

        public Complex CalculateZ(double frequency)
        {
            Complex impedance = new Complex();
            foreach (IComponent component in _circuit)
            {
                impedance += component.CalculateZ(frequency);
            }
            return impedance;
        }

        public event EventHandler CircuitChanged;
    }
}
