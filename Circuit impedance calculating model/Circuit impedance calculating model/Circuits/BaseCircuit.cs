#region - Using -

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion
//TODO поменять список List<IComponent> на ObservableCollection
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
        private List<IComponent> _circuit;

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
        protected BaseCircuit() { }

        #endregion

        #region - Public Properties -

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
        /// Свойство для списка элементов цепи.
        /// </summary>
        public List<IComponent> Circuit
        {
            get { return _circuit; }
            set
            {
                _circuit = value;
                CircuitChanged?.Invoke(this, EventArgs.Empty);
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
    }
}
