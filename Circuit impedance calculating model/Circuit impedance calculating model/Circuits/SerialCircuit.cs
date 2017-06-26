#region - Using -

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace Circuit_impedance_calculating_model.Circuits
{
    public class SerialCircuit: BaseCircuit
    {
        #region -Constructors-

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public SerialCircuit()
        {
            Circuit = new List<IComponent>();
        }

        /// <summary>
        /// Конструктор с входными параметрами
        /// </summary>
        /// <param name="name">Наименование цепи</param>
        public SerialCircuit(string name)
        {
            Name = name;
            Circuit = new List<IComponent>();
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
