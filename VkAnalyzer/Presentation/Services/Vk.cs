using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xNet;

namespace Presentation.Services
{
    public class Vk : IVk
    {
       public string AccessToken {
            get { return _accessToken; }
            set { if(_accessToken != value) _accessToken = value; } }
       public string Captcha { get; set; }

       private HttpRequest _api;

       private string _accessToken;

       private IParserPage _parser;

        



        public void SetSettings()
        {
            _api.Cookies = new CookieDictionary();

            _api.AllowAutoRedirect = true;

            _api.UserAgent = Http.ChromeUserAgent();

            _parser = new ParserPage();
        }
        public KeyValuePair<int, string>? Auth(string login, string password)
        {
            var incorrectLoginPassword = new KeyValuePair<int, string>(1, "Неверный логин или пароль");
            
            var successLogin = new KeyValuePair<int,string>(0,"Успешно авторизовались");
          
            using (_api = new HttpRequest())
            {
                SetSettings();

                var dataFromSite =  _api.Get(_parser.GetAuthPage(), _parser.GetAuthPageParams()).ToString();
               
                dataFromSite =_api.Post(_parser.GetAuthLink(),_parser.GetAuthParams(dataFromSite,login,password)).ToString();
                var data = _api.Response;
                if (_parser.IsCapcha(dataFromSite))
                {
                    return new KeyValuePair<int, string>(2, _parser.ParseImg(dataFromSite));
                   
                }

                if (!_parser.IsCorrect(dataFromSite))
                    return incorrectLoginPassword;
               
                AccessToken = _parser.GetAccessToken(_api, dataFromSite);
            };
            return successLogin;
        }

        public KeyValuePair<int, string> Auth(string login, string password, string captcha)
        {
            var incorrectLoginPassword = new KeyValuePair<int, string>(1, "Неверный логин или пароль");
        
            var successLogin = new KeyValuePair<int, string>(0, "Успешно авторизовались");

            Captcha = captcha;
            using (_api = new HttpRequest())
            {
                SetSettings();

             //   var dataFromSite = _api.Get(_parser.GetAuthPage(), _parser.GetAuthPageParams()).ToString();
            //    dataFromSite = _api.Post(_parser.GetAuthLink(), _parser.GetAuthParams(dataFromSite, login, password)).ToString();

              //  if (_parser.IsCapcha(dataFromSite))
             // var  dataFromSite = _api.Post(_parser.GetAuthLink(), _parser.GetAuthParamsWithCaptcha(dataFromSite, login, password,captcha)).ToString();

             //   if (!_parser.IsCorrect(dataFromSite)) return incorrectLoginPassword;

              //  AccessToken = _parser.GetAccessToken(_api, dataFromSite);

                return successLogin;
            };
            
        }
        
    }
}