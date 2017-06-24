using System.Numerics;


namespace Circuit_impedance_calculating_model
{
    /// <summary>
    /// Интерфейс, служащий для объединения Circuits и Elements в одном списке. 
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Наименование компонента.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Метод рассчета импеданса компонента.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс компонента</returns>
        Complex CalculateZ(double frequency);
    }
}
