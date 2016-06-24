using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI;
using LightInject;
using Model;
using Presentation.Authorization;
using Presentation.Services;
using Presentation.ShellPresenter;

namespace VkAnalyzer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var container = new ServiceContainer();

            container.Register<IShellView,MainForm>(new PerContainerLifetime());
            container.Register<IShellPresenter,ShellPresenter>(new PerContainerLifetime());
            container.Register<IAuthView,AuthForm>(new PerContainerLifetime());
            container.Register<IAuthPresenter,AuthPresenter>(new PerContainerLifetime());
            container.Register<IVk,Vk>(new PerContainerLifetime());
            container.Register<IUser,User>(new PerContainerLifetime());

            var application = container.GetInstance<IShellPresenter>();

            application.Run();


        }
    }
}
