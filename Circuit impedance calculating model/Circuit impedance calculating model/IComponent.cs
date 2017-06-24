using System.Numerics;


namespace Circuit_impedance_calculating_model
{
    public interface IComponent
    {
        string Name { get; set; }
        Complex CalculateZ(double frequency);
    }
}
