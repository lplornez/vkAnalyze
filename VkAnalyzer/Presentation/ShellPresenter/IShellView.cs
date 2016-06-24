using System;
using Presentation.BaseInterfaces;

namespace Presentation.ShellPresenter
{
    public interface IShellView: IBaseDataSource
    {
        void Run();

        void Host();

        event EventHandler FormLoad;
        
    }
}