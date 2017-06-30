#region - Using -

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace CircuitModeling.Circuits
{ 
    /// <summary>
    /// Интерфейс, описывающий цепи.
    /// </summary>
    public interface ICircuit: IComponent
    {
        #region - Properties -

        /// <summary>
        /// Свойство, 
        /// </summary>
        ObservableCollection<IComponent> Circuit { get; set; }

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее на изменения в цепи.
        /// </summary>
        event EventHandler CircuitChanged;

        #endregion
    }
}
