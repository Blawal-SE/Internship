using DependancyInjection;
using Ninject;
using System;
using System.Windows.Forms;

namespace School.WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CompositionRoot.Wire(new NInjectBinding());
           var kernel = new StandardKernel(new NInjectBinding());
            //var Iuser = kernel.Get<IUser>();
            Application.Run(new LoginPage(kernel));
        }

    }
}
