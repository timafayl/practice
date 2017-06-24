using System;


namespace Circuit_impedance_calculating_model.Elements
{
    /// <summary>
    /// Интерфейс, описывающий простейшие элементы цепи.
    /// </summary>
    public interface IElement: IComponent
    {
        /// <summary>
        /// Значение элемента.
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Событие, срабатывающее на изменеие значения элемента.
        /// </summary>
        event EventHandler ValueChanged;
    }
}
