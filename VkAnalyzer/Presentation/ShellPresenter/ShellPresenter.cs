using System;
using Presentation.Authorization;
using Presentation.BaseInterfaces.BaseInterfaces.PresentationInterfaces;

namespace Presentation.ShellPresenter
{
  public class ShellPresenter : IShellPresenter, IBaseInitialize
    {
      public object DataSource { get; set; }

      private readonly IShellView _view;
      private readonly IAuthPresenter _authPresenter;

      public ShellPresenter(IShellView view, IAuthPresenter authPresenter)
      {
          _view = view;
          _authPresenter = authPresenter;

          _view.FormLoad += OnFormLoad;

           Initialize(new ShellPresenterViewModel());


      }

      private void OnFormLoad(object sender, EventArgs e)
      {
         _authPresenter.FormShow();
      }


      public void Run()
        {
            _view.Run();
        }

      public void Initialize(object dataSource)
      {
          DataSource = dataSource;

          _view.DataSource = dataSource;

          _authPresenter.Initialize(new AuthViewModel());
            
      }
    }
}