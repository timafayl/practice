#region - Using -

using System;
using System.Collections.Generic;

#endregion

namespace Circuit_impedance_calculating_model.Circuits
{
    /// <summary>
    /// Интерфейс, описывающий цепи.
    /// </summary>
    public interface ICircuit: IComponent
    {
        #region - Fields -

        List<IComponent> Circuit { get; set; }

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее на изменения в цепи.
        /// </summary>
        event EventHandler CircuitChanged;

        #endregion
    }
}
