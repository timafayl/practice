#region - Using -

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using Circuit_impedance_calculating_model;
using Circuit_impedance_calculating_model.Circuits;
using Circuit_impedance_calculating_model.Elements;

#endregion

namespace Circuit_impedance_calculating_view
{
    /// <summary>
    /// Форма отображения цепи.
    /// </summary>
    public partial class CircuitViewForm : Form
    {
        private double[] _frequency;
        private Complex[] _impedance;
        private List<IComponent> _circuits = new List<IComponent>();

        public CircuitViewForm()
        {
            InitializeComponent();
        }

        private void calculateImpedanceButton_Click(object sender, EventArgs e)
        {
            Resistor R1 = new Resistor("R1", 50);
            Resistor R2 = new Resistor("R2", 100);
            Inductor L1 = new Inductor("L1", 0.5);
            Capacitor C1 = new Capacitor("C1", 0.005);

            var circuit1 = new ParallelCircuit("circuit1");
            var circuit2 = new SerialCircuit("circuit2");
            var circuit3 = new ParallelCircuit("circuit3");

            circuit3.Circuit.Add(R1);
            circuit3.Circuit.Add(L1);

            circuit2.Circuit.Add(circuit3);
            circuit2.Circuit.Add(C1);

            circuit1.Circuit.Add(circuit2);
            circuit1.Circuit.Add(R2);

            _circuits.Add(circuit1);

            _frequency = new double[impedanceGridView.RowCount - 1];
            _impedance = new Complex[impedanceGridView.RowCount - 1];
            for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
            {
                _frequency[i] = Convert.ToDouble(impedanceGridView[0, i].Value.ToString());
            }
            for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
            {
                _impedance[i] = _circuits[0].CalculateZ(_frequency[i]);
                impedanceGridView[1, i].Value = Convert.ToString(_impedance[i]);
            }
        }
    }
}
