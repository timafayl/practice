#region - Using -

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using CircuitDrawing;
using CircuitModeling;
using CircuitModeling.Circuits;
using CircuitModeling.Elements;

#endregion

namespace CircuitView
{
    /// <summary>
    /// Форма отображения цепи.
    /// </summary>
    public partial class CircuitViewForm : Form
    {

        #region - Private fields -
        //TODO изменить класс Drawer 
        /// <summary>
        /// Список всех схем.
        /// </summary>
        private readonly List<ICircuit> _circuits;

        /// <summary>
        /// Индекс выбранной из списка цепи.
        /// </summary>
        private int _selecetedCircuitIndex = -1;

        /// <summary>
        /// Массив с выходными частотами.
        /// </summary>
        private double[] _frequencies;

        /// <summary>
        /// Массив с рассчитаными импедансами для каждой частоты.
        /// </summary>
        private Complex[] _selectedCircuitImpedance;

        /// <summary>
        /// Выбранный элемент цепи.
        /// </summary>
        private IElement _selectedElement;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Конструктор главной формы.
        /// </summary>
        public CircuitViewForm()
        {
            InitializeComponent();
            _circuits = new TestCircuitsFactory().TestCircuitsList();
            InitializeCircuitsList();
        }

        #endregion

        #region - Controls events -

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
                _circuits[circuitsListBox.SelectedIndex].CircuitChanged +=
                    ElementValueChangedEventHandler;

                _circuits[circuitsListBox.SelectedIndex].CircuitChanged +=
                    CircuitChangedEventHandler;
            }
            else if (_selecetedCircuitIndex != -1 && circuitsListBox.SelectedIndex != _selecetedCircuitIndex)
            {
                _circuits[_selecetedCircuitIndex].CircuitChanged -=
                    ElementValueChangedEventHandler;
                _circuits[circuitsListBox.SelectedIndex].CircuitChanged +=
                    ElementValueChangedEventHandler;

                _circuits[_selecetedCircuitIndex].CircuitChanged -=
                    CircuitChangedEventHandler;
                _circuits[circuitsListBox.SelectedIndex].CircuitChanged +=
                    CircuitChangedEventHandler;
            }
            _selecetedCircuitIndex = circuitsListBox.SelectedIndex;
        }

        /// <summary>
        /// Кнопка рассчета импеданса цепи по заданным частотам.
        /// </summary>
        private void calculateImpedanceButton_Click(object sender, EventArgs e)
        {
            CalculateImpedance();
            if (_frequencies.Length == 0)
            {
                MessageBox.Show(@"Список входных частот пуст. Введите частоту!",
                    @"Frequency Error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Удаляет элемент из выбранной цепи.
        /// </summary>
        private void deleteElementButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Вы действительно хотите удалить выбранный элемент?",
                    @"Element deleting", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DeleteElementInCircuit(_circuits[circuitsListBox.SelectedIndex]);
            }
        }

        #endregion

        #region - Private methods -

        /// <summary>
        /// Инициализирует список схем на форме.
        /// </summary>
        private void InitializeCircuitsList()
        {
            for (int i = 0; i < _circuits.Count; i++)
            {
                circuitsListBox.Items.Add("Тестовая схема #" + (i + 1));
            }
        }

        /// <summary>
        /// Вызывает метод отрисовки входной цепи.
        /// </summary>
        /// <param name="component">Цепь для отрисовки</param>
        private void Draw(IComponent component)
        {
            circuitView.Image = new CircuitDrawer().Draw(component);
        }

        /// <summary>
        /// Рассчитывает импеданс цепи во входным частотам и выводит его на форму.
        /// </summary>
        private void CalculateImpedance()
        {
            _frequencies = new double[impedanceGridView.RowCount - 1];
            if (_frequencies.Length > 0)
            {
                _selectedCircuitImpedance = new Complex[impedanceGridView.RowCount - 1];
                for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
                {
                    _frequencies[i] = Convert.ToDouble(impedanceGridView[0, i].Value.ToString());
                }
                for (int i = 0; i < impedanceGridView.RowCount - 1; i++)
                {
                    _selectedCircuitImpedance[i] = _circuits[circuitsListBox.SelectedIndex].CalculateZ(_frequencies[i]);
                    impedanceGridView[1, i].Value = Convert.ToString(Math.Round(_selectedCircuitImpedance[i].Real, 7)
                                                                     + " + " + Math.Round(_selectedCircuitImpedance[i].Imaginary, 7) + "i");
                }
            }
        }

        /// <summary>
        /// Инициализирует список элементов выбранной цепи на форме.
        /// </summary>
        /// <param name="circuit">Входная цепь</param>
        private void InitializeCircuitElementsList(ICircuit circuit)
        {
            circuitElementsGridView.DataSource = (from el in GetCircuitElements(circuit)
                select new ElementAdapter(el)).ToList();
            //GetCircuitElements(circuit).Select(t => new ElementAdapter(t)).ToList();
        }

        /// <summary>
        /// Возвращает список элементов цепи.
        /// </summary>
        /// <param name="circuit">Входная цепь</param>
        /// <returns>Список элементов цепи</returns>
        private List<IElement> GetCircuitElements(ICircuit circuit)
        {
            var elementsList = new List<IElement>();
            foreach (IComponent component in circuit.CircuitComponents)
            {
                if (component is IElement)
                {
                    elementsList.Add((IElement)component);
                }
                else if (component is ICircuit)
                {
                    elementsList.AddRange(GetCircuitElements((ICircuit)component));
                }
            }
            return elementsList;
        }

        /// <summary>
        /// Удаляет выбранный элемент из цепи.
        /// </summary>
        /// <param name="circuit">Входная цепь</param>
        private void DeleteElementInCircuit(ICircuit circuit)
        {
            List<IElement> elementsList = GetCircuitElements(_circuits[circuitsListBox.SelectedIndex]);
            _selectedElement = elementsList[circuitElementsGridView.CurrentCell.RowIndex];
            foreach (IComponent component in circuit.CircuitComponents)
            {
                if (component.Equals(_selectedElement))
                {
                    circuit.CircuitComponents.Remove(component);
                    return;
                }
                if (component is ICircuit)
                {
                    DeleteElementInCircuit((ICircuit)component);
                }
            }
        }

        /// <summary>
        /// Обработчик события выбранной цепи.
        /// </summary>
        private void ElementValueChangedEventHandler(object sender, EventArgs args)
        {
            if (_frequencies.Length != 0)
            {
                CalculateImpedance();
            }
        }

        /// <summary>
        /// Обработчик события выбранной цепи.
        /// </summary>
        private void CircuitChangedEventHandler(object sender, EventArgs args)
        {
            Draw(_circuits[circuitsListBox.SelectedIndex]);
            InitializeCircuitElementsList(_circuits[circuitsListBox.SelectedIndex]);
        }

        #endregion
    }
}
