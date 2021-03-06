﻿#region - Using -

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;
using CircuitModeling.Elements;

#endregion

namespace CircuitModeling.Circuits
{
    /// <summary>
    /// Класс, описывающий параллельные цепи.
    /// </summary>
    public class ParallelCircuit: BaseCircuit
    {
        #region - Constructors -

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public ParallelCircuit(): base() { }

        /// <summary>
        /// Конструктор с входными параметрами
        /// </summary>
        /// <param name="name">Наименование цепи</param>
        public ParallelCircuit(string name)//: base()
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
            Complex admittance = new Complex();
            foreach (IComponent component in CircuitComponents)
            {
                admittance += 1 / component.CalculateZ(frequency);
            }
            return 1 / admittance;
        }

        #endregion
    }
}
