#region - Using -

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using CircuitModeling;
using CircuitModeling.Circuits;
using CircuitModeling.Elements;
using Circuit_Drawer;

#endregion

namespace CircuitView
{
    /// <summary>
    /// Форма отображения цепи.
    /// </summary>
    public partial class CircuitViewForm : Form
    {
        #region - Private fields -

        private int _selecetedCircuitIndex = -1;

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
        private readonly List<IComponent> _circuits;

        /// <summary>
        /// Переменная класса с тестовыми схемами.
        /// </summary>
        private readonly TestCircuits _testCircuits = new TestCircuits();

        #endregion

        #region - Constructors -

        /// <summary>
        /// Конструктор главной формы.
        /// </summary>
        public CircuitViewForm()
        {
            InitializeComponent();
            _circuits = _testCircuits.TestCircuitsList();
            InitializeCircuitsList();
        }

        #endregion

        #region - Controls events -

        /// <summary>
        /// Кнопка рассчета импеданса цепи по заданным частотам.
        /// </summary>
        private void calculateImpedanceButton_Click(object sender, EventArgs e)
        {
            CalculateImpedance();
            if (_frequency.Length == 0)
            {
                MessageBox.Show(@"Список входных частот пуст. Введите частоту!",
                    @"Frequency Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Событие для вызова метода отрисовки схем.
        /// </summary>
        private void circuitsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw(_circuits[circuitsListBox.SelectedIndex]);
            InitializeCircuitElementsList(_circuits[circuitsListBox.SelectedIndex]);
            CalculateImpedance();
            if (_selecetedCircuitIndex == -1)
            {
                _circuits.Cast<ICircuit>().ToList()[circuitsListBox.SelectedIndex].CircuitChanged +=
                    CircuitChangedEventHadler;
            }
            else if (_selecetedCircuitIndex != -1 && circuitsListBox.SelectedIndex != _selecetedCircuitIndex)
            {
                _circuits.Cast<ICircuit>().ToList()[_selecetedCircuitIndex].CircuitChanged -=
                    CircuitChangedEventHadler;
                _circuits.Cast<ICircuit>().ToList()[circuitsListBox.SelectedIndex].CircuitChanged +=
                    CircuitChangedEventHadler;
                _selecetedCircuitIndex = circuitsListBox.SelectedIndex;
            }
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

        private void InitializeCircuitElementsList(IComponent component)
        {
            ICircuit circuit = component as ICircuit;
            circuitElementsGridView.DataSource = (from el in GetCircuitElements(circuit)
                                        select new ElementAdapter(el)).ToList();
            //GetCircuitElements(circuit).Select(t => new ElementAdapter(t)).ToList();

        }

        /// <summary>
        /// Вызывает метод отрисовки входной цепи.
        /// </summary>
        /// <param name="component">Цепь для отрисовки</param>
        private void Draw(IComponent component)
        {
            Bitmap bmp = new Bitmap(circuitView.Width, circuitView.Height);
            CircuitDrawer drawer = new CircuitDrawer();
            circuitView.Image = drawer.DrawCircuit(component, bmp, 20, circuitView.Height / 2);
        }

        /// <summary>
        /// Возвращает список элементов цепи.
        /// </summary>
        /// <param name="circuit">Входная цепь</param>
        /// <returns>Список элементов цепи</returns>
        private List<IElement> GetCircuitElements(ICircuit circuit)
        {
            var elementsList = new List<IElement>();
            for (int i = 0; i < circuit.Circuit.Count; i++)
            {
                if (circuit.Circuit[i] is IElement)
                {
                    elementsList.Add((IElement)circuit.Circuit[i]);
                }
                else if (circuit.Circuit[i] is ICircuit)
                {
                    elementsList.AddRange(GetCircuitElements((ICircuit)circuit.Circuit[i]));
                }
            }
            return elementsList;
        }

        private void CircuitChangedEventHadler(object sender, EventArgs args)
        {
            if (_frequency.Length != 0)
            {
                CalculateImpedance();
            }
            else
            {
                return;
            }
        }

        private void CalculateImpedance()
        {
            _frequency = new double[impedanceGridView.RowCount - 1];
            _impedance = new Complex[impedanceGridView.RowCount - 1];
            if (_frequency.Length > 0)
            {
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
            else
            {
                return;
            }
        }

        #endregion
    }
}
