#region - Using -

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using Circuit_Drawer;
using Circuit_impedance_calculating_model;
using Circuit_impedance_calculating_model.Circuits;

#endregion

namespace Circuit_impedance_calculating_view
{
    /// <summary>
    /// Форма отображения цепи.
    /// </summary>
    public partial class CircuitViewForm : Form
    {
        #region - Private fields -

        /// <summary>
        /// Массив с выходными частотами.
        /// </summary>
        private double[] _frequency;

        /// <summary>
        /// Массив с рассчитаными импедансами для каждой частоты.
        /// </summary>
        private Complex[] _impedance;

        /// <summary>
        /// Список всех схем.
        /// </summary>
        private List<IComponent> _circuits;

        /// <summary>
        /// Переменная класса с тестовыми схемами.
        /// </summary>
        private TestCircuits _testCircuits = new TestCircuits();

        #endregion

        #region - Constructors -

        public CircuitViewForm()
        {
            InitializeComponent();
            _circuits = _testCircuits.TestCircuitsList();
            InitializeCircuitsList();
        }

        #endregion

        #region - Event handlers-

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
                impedanceGridView[1, i].Value = Convert.ToString(Math.Round(_impedance[i].Real, 7)
                                              + " + " + Math.Round(_impedance[i].Imaginary, 7) + "i");
            }
        }

        private void circuitsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw(_circuits[circuitsListBox.SelectedIndex]);
        }

        #endregion

        #region - Private methods -

        /// <summary>
        /// Инициализирует список схем в circuitsListBox.
        /// </summary>
        private void InitializeCircuitsList()
        {
            for (int i = 0; i < _circuits.Count; i++)
            {
                circuitsListBox.Items.Add("Тестовая схема #" + (i + 1));
            }
        }

        private void Draw(IComponent component)
        {
            Bitmap bmp = new Bitmap(circuitView.Width, circuitView.Height);
            Drawer drawer = new Drawer();
            circuitView.Image = drawer.DrawCircuit(component, bmp, 20, circuitView.Height / 2);
        }

        #endregion
    }
}
