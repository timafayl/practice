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
            
        }

        public event EventHandler CircuitChanged;
    }
}
