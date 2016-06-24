using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Model;
using Presentation.Services;


namespace Presentation.Authorization
{
    public class AuthPresenter : IAuthPresenter
    {
        public object DataSource { get; set; }

        private readonly IAuthView _viewAuth;

        private readonly IUser _user;

        private readonly IVk _api;
        public AuthPresenter(IAuthView viewAuth, IUser user, IVk api)
        {

            _viewAuth = viewAuth;
            _user = user;
            _api = api;
        }
        public void Initialize(object dataSource)
        {
            DataSource = dataSource;
            _viewAuth.DataSource = dataSource;
        }

        public void FormShow()
        {
            _viewAuth.FormShow();

           
        }

        public void FormClose()
        {
            _viewAuth.FormClose();
        }
    }
}