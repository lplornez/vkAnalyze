using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Presentation.Authorization
{
    public class AuthViewModel :INotifyPropertyChanged
    {
        private string _urlWithAccessToken = null;
        private string _url;
        private string _coockie;

        public string UrlWithAccessToken
        {
            get { return _urlWithAccessToken; }
            set
            {
                if (value == _urlWithAccessToken) return;
                _urlWithAccessToken = value;
                OnPropertyChanged();
            }
        }

        public string Url {
            get { return _url; }
            set
            {
                if (value == _url) return;
                _url = value;
                OnPropertyChanged();

            } }

         public string Cookie
        {
            get { return _coockie; }
            set
            {
                if (value == _url) return;
                _coockie = value;
                OnPropertyChanged();

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}