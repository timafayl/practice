#region - Using -

using System.Collections.Generic;
using CircuitModeling;
using CircuitModeling.Circuits;
using CircuitModeling.Elements;

#endregion


namespace CircuitView
{
    /// <summary>
    /// Класс с тестовыми схемами.
    /// </summary>
    public class TestCircuitsFactory
    {
        #region - Public methods -

        /// <summary>
        /// Метод, возвращающий список с тестовыми схемами.
        /// </summary>
        /// <returns>Список с тестовыми цепями</returns>
        public List<ICircuit> TestCircuitsList()
        {
            var testCircuitsList = new List<ICircuit>
                { Сircuit1(), Сircuit2(), Сircuit3(), Сircuit4(), Сircuit5(), Сircuit6()};
            return testCircuitsList;
        }

        #endregion

        #region - Private methods | Test circuits-

        /// <summary>
        /// Тестовая схема №1.
        /// </summary>
        /// <returns>Первую тестовую схему</returns>
        public ICircuit Сircuit1()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            circuit2.CircuitComponents.Add(L1);
            circuit2.CircuitComponents.Add(C1);
            circuit1.CircuitComponents.Add(R1);
            circuit1.CircuitComponents.Add(circuit2);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №2.
        /// </summary>
        /// <returns>Ввторую тестовую схему</returns
        private ICircuit Сircuit2()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            circuit2.CircuitComponents.Add(R1);
            circuit2.CircuitComponents.Add(C1);
            circuit1.CircuitComponents.Add(circuit2);
            circuit1.CircuitComponents.Add(L1);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №3.
        /// </summary>
        /// <returns>Третью тестовую схему</returns
        private ICircuit Сircuit3()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var L2 = new Inductor("L2", 0.5);
            var L3 = new Inductor("L3", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            var circuit3 = new ParallelCircuit("circuit3");
            circuit3.CircuitComponents.Add(L1);
            circuit3.CircuitComponents.Add(R1);
            circuit2.CircuitComponents.Add(C1);
            circuit2.CircuitComponents.Add(L2);
            circuit1.CircuitComponents.Add(circuit2);
            circuit1.CircuitComponents.Add(L3);
            circuit1.CircuitComponents.Add(circuit3);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №4.
        /// </summary>
        /// <returns>Четвертую тестовую схему</returns>
        private ICircuit Сircuit4()
        {
            var R1 = new Resistor("R1", 100);
            var R2 = new Resistor("R2", 100);
            var C1 = new Capacitor("C1", 0.005);
            var C2 = new Capacitor("C2", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var L2 = new Inductor("L2", 0.5);
            var circuit1 = new ParallelCircuit("circuit1");
            var circuit2 = new SerialCircuit("circuit2");
            var circuit3 = new SerialCircuit("circuit3");
            var circuit4 = new ParallelCircuit("circuit4");
            var circuit5 = new ParallelCircuit("circuit5");
            circuit5.CircuitComponents.Add(L1);
            circuit5.CircuitComponents.Add(R1);
            circuit4.CircuitComponents.Add(L2);
            circuit4.CircuitComponents.Add(R2);
            circuit3.CircuitComponents.Add(circuit5);
            circuit3.CircuitComponents.Add(C1);
            circuit2.CircuitComponents.Add(C2);
            circuit2.CircuitComponents.Add(circuit4);
            circuit1.CircuitComponents.Add(circuit2);
            circuit1.CircuitComponents.Add(circuit3);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №5.
        /// </summary>
        /// <returns>Пятую тестовую схему</returns>
        private ICircuit Сircuit5()
        {
            var R1 = new Resistor("R1", 100);
            var R2 = new Resistor("R2", 100);
            var C1 = new Capacitor("C1", 0.005);
            var C2 = new Capacitor("C2", 0.005);
            var C3 = new Capacitor("C3", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var L2 = new Inductor("L2", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            var circuit3 = new SerialCircuit("circuit3");
            var circuit4 = new SerialCircuit("circuit4");
            var circuit5 = new ParallelCircuit("circuit5");
            circuit5.CircuitComponents.Add(C3);
            circuit5.CircuitComponents.Add(R1);
            circuit4.CircuitComponents.Add(R2);
            circuit4.CircuitComponents.Add(L2);
            circuit3.CircuitComponents.Add(L1);
            circuit3.CircuitComponents.Add(circuit5);
            circuit2.CircuitComponents.Add(C2);
            circuit2.CircuitComponents.Add(circuit3);
            circuit2.CircuitComponents.Add(circuit4);
            circuit1.CircuitComponents.Add(C1);
            circuit1.CircuitComponents.Add(circuit2);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №6.
        /// </summary>
        /// <returns>Шестую тестовую схему</returns>
        private ICircuit Сircuit6()
        {
            var R1 = new Resistor("R1", 100);
            var circuit1 = new ParallelCircuit("circuit1");
            var circuit2 = new SerialCircuit("circuit2");
            var circuit4 = new ParallelCircuit("circuit4");
            var circuit5 = new ParallelCircuit("circuit5");
            var circuit6 = new SerialCircuit("circuit6");
            var circuit7 = new ParallelCircuit("circuit7");
            var circuit8 = new ParallelCircuit("circuit8");
            var circuit9 = new SerialCircuit("circuit9");
            circuit7.CircuitComponents.Add(R1);
            circuit7.CircuitComponents.Add(R1);
            circuit6.CircuitComponents.Add(R1);
            circuit6.CircuitComponents.Add(circuit7);
            circuit5.CircuitComponents.Add(R1);
            circuit5.CircuitComponents.Add(circuit6);
            circuit8.CircuitComponents.Add(circuit6);
            circuit8.CircuitComponents.Add(R1);
            circuit4.CircuitComponents.Add(R1);
            circuit4.CircuitComponents.Add(R1);
            circuit2.CircuitComponents.Add(R1);
            circuit2.CircuitComponents.Add(circuit5);
            circuit9.CircuitComponents.Add(R1);
            circuit9.CircuitComponents.Add(circuit8);
            circuit1.CircuitComponents.Add(circuit2);
            circuit1.CircuitComponents.Add(circuit9);
            return circuit1;
        }

        #endregion
    }
}
