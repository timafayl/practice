﻿#region - Using -

using System;
using System.Windows.Forms;

#endregion


namespace Circuit_impedance_calculating_view
{
    /// <summary>
    /// Класс запуска программы.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CircuitViewForm());
        }
    }
}
