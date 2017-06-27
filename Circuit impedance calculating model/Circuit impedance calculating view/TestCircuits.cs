#region - Using -

using System.Collections.Generic;
using Circuit_impedance_calculating_model;
using Circuit_impedance_calculating_model.Circuits;
using Circuit_impedance_calculating_model.Elements;

#endregion


namespace Circuit_impedance_calculating_view
{
    /// <summary>
    /// Класс с тестовыми схемами
    /// </summary>
    public class TestCircuits
    {
        #region - Public methods -

        /// <summary>
        /// Метод, возвращающий список с тестовыми схемами.
        /// </summary>
        /// <returns></returns>
        public List<IComponent> TestCircuitsList()
        {
            var testCircuitsList = new List<IComponent>
                { _circuit1(), _circuit2(), _circuit3(), _circuit4(), _circuit5()};
            return testCircuitsList;
        }

        #endregion

        #region - Private methods | Test circuits-

        /// <summary>
        /// Тестовая схема №1.
        /// </summary>
        /// <returns></returns>
        public IComponent _circuit1()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            circuit2.Circuit.Add(L1);
            circuit2.Circuit.Add(C1);
            circuit1.Circuit.Add(R1);
            circuit1.Circuit.Add(circuit2);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №2.
        /// </summary>
        /// <returns></returns
        private IComponent _circuit2()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            circuit2.Circuit.Add(R1);
            circuit2.Circuit.Add(C1);
            circuit1.Circuit.Add(circuit2);
            circuit1.Circuit.Add(L1);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №3.
        /// </summary>
        /// <returns></returns
        private IComponent _circuit3()
        {
            var R1 = new Resistor("R1", 100);
            var C1 = new Capacitor("C1", 0.005);
            var L1 = new Inductor("L1", 0.5);
            var L2 = new Inductor("L2", 0.5);
            var L3 = new Inductor("L3", 0.5);
            var circuit1 = new SerialCircuit("circuit1");
            var circuit2 = new ParallelCircuit("circuit2");
            var circuit3 = new ParallelCircuit("circuit3");
            circuit3.Circuit.Add(L1);
            circuit3.Circuit.Add(R1);
            circuit2.Circuit.Add(C1);
            circuit2.Circuit.Add(L2);
            circuit1.Circuit.Add(circuit2);
            circuit1.Circuit.Add(L3);
            circuit1.Circuit.Add(circuit3);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №4.
        /// </summary>
        /// <returns></returns>
        private IComponent _circuit4()
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
            circuit5.Circuit.Add(L1);
            circuit5.Circuit.Add(R1);
            circuit4.Circuit.Add(L2);
            circuit4.Circuit.Add(R2);
            circuit3.Circuit.Add(circuit5);
            circuit3.Circuit.Add(C1);
            circuit2.Circuit.Add(C2);
            circuit2.Circuit.Add(circuit4);
            circuit1.Circuit.Add(circuit2);
            circuit1.Circuit.Add(circuit3);
            return circuit1;
        }

        /// <summary>
        /// Тестовая схема №5.
        /// </summary>
        /// <returns></returns
        private IComponent _circuit5()
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
            circuit5.Circuit.Add(C3);
            circuit5.Circuit.Add(R1);
            circuit4.Circuit.Add(R2);
            circuit4.Circuit.Add(L2);
            circuit3.Circuit.Add(L1);
            circuit3.Circuit.Add(circuit5);
            circuit2.Circuit.Add(C2);
            circuit2.Circuit.Add(circuit3);
            circuit2.Circuit.Add(circuit4);
            circuit1.Circuit.Add(C1);
            circuit1.Circuit.Add(circuit2);
            return circuit1;
        }

        #endregion
    }
}
