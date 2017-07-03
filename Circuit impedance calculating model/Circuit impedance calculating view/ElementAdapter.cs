#region - Using -

using System;
using System.Windows.Forms;
using CircuitModeling.Elements;

#endregion

namespace CircuitView
{
    /// <summary>
    /// Адаптер для интерфейса IElement
    /// </summary>
    public class ElementAdapter
    {
        #region - Constructors -

        /// <summary>
        /// Конструктор инициализирующий элемент.
        /// </summary>
        /// <param name="element">Входной элемент</param>
        public ElementAdapter(IElement element)
        {
            _element = element;
        }

        #endregion

        /// <summary>
        /// Свойство для имени элемента
        /// </summary>
        public string Name
        {
            get { return _element.Name; }
            set
            {
                try
                {
                    _element.Name = value;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                } 
            }
        }

        /// <summary>
        /// Свойство для значения элемента
        /// </summary>
        public double Value
        {
            get { return _element.Value; }
            set
            {
                try
                {
                    _element.Value = value;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        /// <summary>
        /// Элемент цепи.
        /// </summary>
        private readonly IElement _element;
    }
}
