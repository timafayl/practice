using System;


namespace Circuit_impedance_calculating_model.Elements
{
    public interface IElement: IComponent
    {
        double Value { get; set; }

        event EventHandler ValueChanged;
    }
}
