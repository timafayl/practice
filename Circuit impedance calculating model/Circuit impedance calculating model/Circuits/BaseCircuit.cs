#region - Using -

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using CircuitModeling.Elements;

#endregion

namespace CircuitModeling.Circuits
{
    /// <summary>
    /// Базовай класс Circuit.
    /// </summary>
    public class BaseCircuit: ICircuit
    {
        #region - Private fields -

        /// <summary>
        /// Поле, содержащее наименование цепи.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле, содержащее список компонентов цепи.
        /// </summary>
        private ObservableCollection<IComponent> _circuit;

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее на изменения в цепи.
        /// </summary>
        public event EventHandler CircuitChanged;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        protected BaseCircuit()
        {
            Circuit = new ObservableCollection<IComponent>();
            Circuit.CollectionChanged += Circuit_CollectionChanged;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Свойство для наименования цепи.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                string pattern = @"^circuit\d{1,2}$"; //задает значение типа "circuit1" или "circuit10"
                value = value.ToLower();
                if (value.Length > 9)
                {
                    throw new ArgumentException("Наименование цепи не должно" +
                        " превышать девяти символов. Наименование цепи должно начинаться" +
                        " со слова 'circuit' после которого должен идти порядковый номер цепи в схеме.");
                }
                if (!Regex.IsMatch(value, pattern))
                {
                    throw new ArgumentException("Наименование цепи должно начинаться" +
                        " со слова 'circuit' после которого должен идти порядковый номер цепи в схеме.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство для списка компонентов цепи.
        /// </summary>
        public ObservableCollection<IComponent> Circuit
        {
            get { return _circuit; }
            set
            {
                _circuit = value;
            }
        }

        #endregion

        #region - Public methods-

        /// <summary>
        /// Метод рассчета импеданса цепи.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс компонента</returns>
        public virtual Complex CalculateZ(double frequency)
        {
            throw new NotImplementedException("Метод не реализован!");
        }

        #endregion

        #region - Private methods -

        private void Circuit_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems[0] is IElement)
                    {
                        e.NewItems.Cast<IElement>().ToList()[0].ValueChanged += OnCircuitChanged;
                    }
                    else if (e.NewItems[0] is ICircuit)
                    {
                        e.NewItems.Cast<ICircuit>().ToList()[0].CircuitChanged += OnCircuitChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems[0] is IElement)
                    {
                        e.OldItems.Cast<IElement>().ToList()[0].ValueChanged -= OnCircuitChanged;
                    }
                    else if (e.OldItems[0] is ICircuit)
                    {
                        e.OldItems.Cast<ICircuit>().ToList()[0].CircuitChanged -= OnCircuitChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.NewItems[0] is IElement)
                    {
                        e.OldItems.Cast<IElement>().ToList()[0].ValueChanged -= OnCircuitChanged;
                        e.NewItems.Cast<IElement>().ToList()[0].ValueChanged += OnCircuitChanged;
                    }
                    else if (e.NewItems[0] is ICircuit)
                    {
                        e.OldItems.Cast<ICircuit>().ToList()[0].CircuitChanged -= OnCircuitChanged;
                        e.NewItems.Cast<ICircuit>().ToList()[0].CircuitChanged += OnCircuitChanged;
                    }
                    break;
            }
        }

        private void OnCircuitChanged(object sender, EventArgs args)
        {
            CircuitChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
