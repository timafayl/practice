using System;
using System.Collections.Generic;


namespace Circuit_impedance_calculating_model.Circuits
{
    /// <summary>
    /// Интерфейс, описывающий цепи.
    /// </summary>
    interface ICircuit: IComponent
    {
        /// <summary>
        /// Список, хранящий компоненты цепи.
        /// </summary>
        List<IComponent> Circuit { get; set; }

        /// <summary>
        /// Событие, срабатывающее на изменения в цепи.
        /// </summary>
        event EventHandler CircuitChanged;
    }
}
