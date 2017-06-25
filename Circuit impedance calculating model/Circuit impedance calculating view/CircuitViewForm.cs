#region - Using -

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;
using Circuit_impedance_calculating_model;

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
        private List<IComponent> _circuits;
        private TestCircuits _testCircuits = new TestCircuits();

        public CircuitViewForm()
        {
            InitializeComponent();
            _circuits = _testCircuits.TestCircuitsList();
            InitializeCircuitsList();
        }

    private void calculateImpedanceButton_Click(object sender, EventArgs e)
        {
            _frequency = new double[impedanceGridView.RowCount - 1];
            _impedance = new Complex[impedanceGridView.RowCount - 1];
            for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
            {
                _frequency[i] = Convert.ToDouble(impedanceGridView[0, i].Value.ToString());
            }
            for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
            {
                _impedance[i] = _circuits[circuitsListBox.SelectedIndex].CalculateZ(_frequency[i]);
                impedanceGridView[1, i].Value = Convert.ToString(_impedance[i]);
            }
        }

        /// <summary>
        /// Инициализирует список схем в circuitsListBox.
        /// </summary>
        private void InitializeCircuitsList()
        {
            for (int i = 0; i < _circuits.Count; i++)
            {
                circuitsListBox.Items.Add("Тестовая схема #" + (i+1));
            }
        }
    }
}
