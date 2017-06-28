#region - Using -

using System;
using System.Collections.Generic;

#endregion

namespace Circuit_impedance_calculating_model.Circuits
{
    //NOTE: Какую роль в арихитектуре играет интрфейс, если у тебя есть базовый класс ? 
    /// <summary>
    /// Интерфейс, описывающий цепи.
    /// </summary>
    public interface ICircuit: IComponent
    {
        #region - Fields -
        //TODO:Xml Комментарии
        List<IComponent> Circuit { get; set; }

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее на изменения в цепи.
        /// </summary>
        event EventHandler CircuitChanged;

        #endregion

        #region - OnCircuitChanged -
        //NOTE: есть смысл выносить детали в интерфейс ? 
        /// <summary>
        /// Вызывает событие CircuitChanged, если оно не пустое.
        /// </summary>
        void OnCircuitChanged();

        #endregion
    }
}
