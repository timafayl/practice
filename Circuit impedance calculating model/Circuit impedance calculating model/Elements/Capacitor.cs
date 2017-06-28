﻿#region - Using -

using System;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

#endregion

namespace Circuit_impedance_calculating_model.Elements
{
    /// <summary>
    /// Класс, описывающий коденсатор. 
    /// </summary>
    public class Capacitor: IElement
    {
        #region - Private fields -

        /// <summary>
        /// Поле, задающее наименование элемента.
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле, задающее значение элемента.
        /// </summary>
        private double _value;

        #endregion

        #region - Events -

        /// <summary>
        /// Событие, срабатывающее при изменении значения элемента.
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion

        #region - Constructors -

        /// <summary>
        /// Пустой конструтор.
        /// </summary>
        public Capacitor() { }

        /// <summary>
        /// Конструктор с входными параметрами.
        /// </summary>
        /// <param name="name">Наименование элемента</param>
        /// <param name="value">Значение элемента</param>
        public Capacitor(string name, double value)
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
                //TODO: Регекспы нуждаются в комментариях
                string pattern1 = @"^C\d$";
                string pattern2 = @"^C\d{2}$";
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                value = ti.ToTitleCase(value);
                if (value.Length > 3)
                {
                    //TODO: Сложно. Мб разделить на 3 строки и складывать строки ? 
                    throw new ArgumentException("Наименование конденсатора не должно" +
                        " превышать трех символов. Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                if (!(Regex.IsMatch(value, pattern1) || Regex.IsMatch(value, pattern2)))
                {
                    throw new ArgumentException("Наименование конденсатора в цепи должно начинаться" +
                        " с латинской буквы 'C' после которой должен идти порядковый номер конденсатора в цепи.");
                }
                _name = value;
            }
        }

        //TODO: Некорректный комментарий. Это у тебя емкость, а не поле _value
        /// <summary>
        /// Свойство-аксессор для поля _value.
        /// </summary>
        public double Value
        {
            get
            { return _value; }
            set
            {
                //TODO: Валидация. NaN. +inf - inf
                if (value < 0)
                {
                    throw new ArgumentException("Значение конденсатора не должно быть меньше нуля.");
                }
                _value = value;
                //TODO: Нужна проверка. Если _val = val то не должно вызываться событие
                OnValueChanged();
            }
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Метод для рассчета импеданса элемента.
        /// </summary>
        /// <param name="frequency">Входная частота.</param>
        /// <returns>Импеданс элемента</returns>
        public Complex CalculateZ(double frequency)
        {
            //TODO: Валидация на frequency  NaN. +inf - inf
            return new Complex(0, -1/(2 * Math.PI * frequency * _value));
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
