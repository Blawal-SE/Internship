using DependancyInjection;
using Ninject;
using System;
using System.Windows.Forms;

namespace School.WindowForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var kernel = new StandardKernel(new NInjectBinding());
            Application.Run(new Form1(kernel));
        }
    }
}
