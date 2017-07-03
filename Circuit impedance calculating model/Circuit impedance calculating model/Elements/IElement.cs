#region - Using -

using System;

#endregion

namespace CircuitModeling.Elements
{
    /// <summary>
    /// Интерфейс, описывающий простейшие элементы цепи.
    /// </summary>
    public interface IElement: IComponent
    {
        #region - Properties - 

        /// <summary>
        /// Значение элемента.
        /// </summary>
        double Value { get; set; }

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее на изменеие значения элемента.
        /// </summary>
        event EventHandler ValueChanged;

        #endregion
    }
}
