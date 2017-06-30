#region - Using -

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

#endregion

namespace CircuitModeling.Circuits
{
    /// <summary>
    /// Класс, описывающий последовательные цепи.
    /// </summary>
    public class SerialCircuit: BaseCircuit
    {
        #region -Constructors-

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public SerialCircuit(): base () { }

        /// <summary>
        /// Конструктор с входными параметрами
        /// </summary>
        /// <param name="name">Наименование цепи</param>
        public SerialCircuit(string name): base ()
        {
            Name = name;
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод рассчета импеданса цепи.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс цепи</returns>
        public override Complex CalculateZ(double frequency)
        {
            if (frequency < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть меньше нуля.");
            }
            if (double.IsNaN(frequency))
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть нулевым.");
            }
            if (double.IsNegativeInfinity(frequency) || double.IsPositiveInfinity(frequency))
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency,
                    "Значение частоты не должно быть равным бесконечности.");
            }
            Complex impedance = new Complex();
            foreach (IComponent component in Circuit)
            {
                impedance += component.CalculateZ(frequency);
            }
            return impedance;
        }

        #endregion
    }
}
