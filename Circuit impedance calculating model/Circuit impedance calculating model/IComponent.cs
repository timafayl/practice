#region - Using -

using System.Numerics;

#endregion

namespace CircuitModeling
{
    /// <summary>
    /// Интерфейс, служащий для объединения Circuits и Elements в одном списке. 
    /// </summary>
    public interface IComponent
    {
        #region - Properties - 
        
        /// <summary>
        /// Наименование компонента.
        /// </summary>
        string Name { get; set; }

        #endregion

        #region - Methods -

        /// <summary>
        /// Метод рассчета импеданса компонента.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс компонента</returns>
        Complex CalculateZ(double frequency);

        #endregion
    }
}
