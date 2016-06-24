using System.Collections.Generic;
using xNet;

namespace Presentation.Services
{
    public interface IParserPage
    {
        Dictionary<string,string> AuthPageDataDictionary { get; set; }

        Dictionary<string,string> AuthPatternDictionary { get; set; }

        Dictionary<string,string> AuthPostDataDictionary { get; set; }

        Dictionary<string,string > LinkToQueryDictionary { get; set; }
        
        string GetAuthPage();
        string GetAuthLink();

        string GetAccessToken(HttpRequest api,string htmlPage);

        RequestParams GetAuthParams(string htmlPage,string login,string password);
        RequestParams GetAuthPageParams();
        bool IsCapcha(string dataFromSite);
        bool IsCorrect(string dataFromSite);
        RequestParams GetAuthParamsWithCaptcha(string dataFromSite, string login, string password,string captcha);
        string ParseImg(string dataFromSite);
    }
}