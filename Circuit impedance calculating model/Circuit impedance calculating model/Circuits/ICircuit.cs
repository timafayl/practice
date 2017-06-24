using System;
using System.Collections.Generic;


namespace Circuit_impedance_calculating_model.Circuits
{
    interface ICircuit: IComponent
    {
        List<IComponent> Circuit { get; set; }

        event EventHandler CircuitChanged;
    }
}
