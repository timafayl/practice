#region - Using -

using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace Circuit_impedance_calculating_model.Elements
{
    /// <summary>
    /// Класс, описывающий катушку индуктивности.
    /// </summary>
    public class Inductor: IElement
    {
        #region - Private fields -

        /// <summary>
        /// Поле, содержащее наименование элемента.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле, содержащее значение элемента.
        /// </summary>
        private double _value;

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, србатываюшее при изменении значения элемента.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Inductor() { }

        /// <summary>
        /// Конструктор с входными параметрами.
        /// </summary>
        /// <param name="name">Наименование элемента</param>
        /// <param name="value">Значение элемента</param>
        public Inductor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region - Public properties -

        /// <summary>
        /// Свойство-аксессор для поля _name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                //TODO: Комментарии к регуляркам
                //TODO: Можно объеденить в ^L\d{1,2}$
                string pattern1 = @"^L\d$";
                string pattern2 = @"^L\d{2}$";
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    //TODO: Сложно. Катушка инЛуктивности
                    throw new ArgumentException("Наименование катушки инлуктивности не должно" +
                        " превышать трех символов. Наименование катушки инлуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Наименование катушки индуктивности в цепи должно начинаться" +
                        " с латинской буквы 'L' после которой должен идти порядковый номер катушки в цепи.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Свойство-аксессор для поля _value.
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                //TODO: Валидация. NaN. +inf - inf
                if (value < 0)
                {
                    throw new ArgumentException("Значение индуктивности не должно быть меньше нуля.");
                }
                _value = value;
                //TODO: Нужна проверка. Если _val = val то не должно вызываться событие
                OnValueChanged();
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод рассчета импеданса элемента.
        /// </summary>
        /// <param name="frequency">Входная частота</param>
        /// <returns>Импеданс элемента</returns>
        public Complex CalculateZ(double frequency)
        {
            //TODO: Валидация. NaN. +inf - inf
            return new Complex(0, 2 * Math.PI * frequency * Value);
        }

        /// <summary>
        /// Вызывает событие ValueChanged, если оно не пустое.
        /// </summary>
        public void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
