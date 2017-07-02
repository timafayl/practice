#region - Using -

using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#endregion


namespace CircuitView
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
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CircuitViewForm());
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message);
            //}
        }
    }
}
