using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using xNet;

namespace Presentation.Services
{
    public class ParserPage : IParserPage
    {
        public ParserPage()
        {
            AuthPageDataDictionary = new Dictionary<string, string>()
            {
                  
                ["client_id"] = "5514681",
                ["redirect_uri"] = "https://oauth.vk.com/blank.html",
                ["scope"] =
                    "friends,notify, photos,audio,video,docs,notes,pages,status,offers,questions,wall,groups,messages,email,notifications,stats,offline",
                ["response_type"] = "token",
                ["v"] = "5.52"
                
            };

            ParseDictionary= new Dictionary<string, string>()
            {
                ["href"] = ".*\"(?<href>.*grant_access.*)\"",
                ["captcha"] = ".*Код с картинки:.*",
                ["captcha_sid"] = "",
                ["captcha_key"] = ""
                
            };

            
            /*********************************************************************/
            /*  Парсим строки такого вида
            /*      <input type="hidden" name="ip_h" value="10efba443c95e76ccf" />
            /*      <input type="hidden" name="lg_h" value="5f3d35b38c191035d4" />
            /*      <input type="hidden" name="_origin" value="https://oauth.vk.com">
            /*      <input type="hidden" name="to" value="aHR0cHM6Ly9vYXV0aC52ay5jb20vYXV0aG9yaXplP2NsaWVudF9pZD01NTE0NjgxJnJlZGlyZWN0X3VyaT1odHRwcyUzQSUyRiUyRm9hdXRoLnZrLmNvbSUyRmJsYW5rLmh0bWwmcmVzcG9uc2VfdHlwZT10b2tlbiZzY29wZT02MjQxNTM1JnY9NS41MiZzdGF0ZT0mbm9odHRwcz0xJmRpc3BsYXk9cGFnZQ--">
            /*      <input type="hidden" id="expire" name="expire" value="0">
            *********************************************************************/
                    AuthPatternDictionary = new Dictionary<string, string>()
            {

                ["ip_h"] = "<input.*name=\"ip_h\".*value=\"(?<ip_h>[a-z0-9]+)\".*",
                ["lg_h"] = "<input.*name=\"lg_h\".*value=\"(?<lg_h>[a-z0-9]+)\".*",
                ["_origin"] = "<input.*name=\"_origin\".*value=\"(?<origin>.+)\".*",
                ["to"] = "<input.*name=\"to\".*value=\"(?<to>.+)\".*",
                ["expire"] = "<input.*name=\"expire\".*value=\"(?<expire>.+)\".*"
               
                    };

            LinkToQueryDictionary = new Dictionary<string, string>()
            {
                ["authPage"] ="https://oauth.vk.com/authorize?",
                ["auth"] = "https://login.vk.com/?act=login&soft=1"
            };


        }
        public  Dictionary<string, string> AuthPageDataDictionary { get; set; }
        public Dictionary<string, string> AuthPatternDictionary { get; set; }

        public Dictionary<string,string> ParseDictionary { get; set; } 
        public Dictionary<string, string> AuthPostDataDictionary { get; set; }
        public Dictionary<string, string> LinkToQueryDictionary { get; set; }
        public string GetAccessToken(HttpRequest api,string htmlPage)
        {
           var linkPattern = ParseDictionary.First(pattern => pattern.Key == "href");

            var res = Regex.Match(htmlPage, linkPattern.Value);
            var res1 = res.Groups["href"].Value;
                var findedLink = Regex.Match(htmlPage, linkPattern.Value).Groups[linkPattern.Key].Value + "&notify = 1";

           if (string.IsNullOrWhiteSpace(findedLink)) return null;

           api.Get(findedLink);
            var accessToken = api.Address.ToString().Split(new[] { "#", "=", "&" }, StringSplitOptions.None)[2];
            return accessToken;
        }

        public RequestParams GetAuthParams(string htmlPage,string login, string pass)
        {
            var result = new RequestParams();
            foreach (var pattern in AuthPatternDictionary)
            {
                result[pattern.Key] = Regex.Match(htmlPage, pattern.Value).Groups[pattern.Key];
            }
            result["email"] = login;
            result["qwe"] = null;
            result["pass"] = pass;

            return result;
        }

        public RequestParams GetAuthPageParams()
        {
            var result = new RequestParams();
            foreach (var obj in AuthPageDataDictionary)
            {
                result[obj.Key] = obj.Value;
            }
            return result;
        }

        public bool IsCapcha(string dataFromSite)
        {
            var capcha = ParseDictionary.First(pattern => pattern.Key == "captcha").Value;
            return Regex.IsMatch(dataFromSite, capcha);
        }

        public bool IsCorrect(string dataFromSite)
        {
            var pattern = ".*Неверный./";
            return !Regex.IsMatch(dataFromSite, pattern);
        }

        public string ParseImg(string dataFromSite)
        {
            var pattern = ".*\"(?<img>.*captcha.php.*)\"";

           return Regex.Match(dataFromSite, pattern).Groups["img"].Value;
        }

        public RequestParams GetAuthParamsWithCaptcha(string dataFromSite, string login, string password,string captcha)
        {
            var result = GetAuthParams(dataFromSite, login, password);

            var captchaSidPattern = AuthPatternDictionary.First(p => p.Key == "captcha_sid");

            result["captcha_sid"] = Regex.Match(dataFromSite, captchaSidPattern.Value).Groups[captchaSidPattern.Key];

            result["captcha_key"] = captcha;

            return result;

        }


        public string GetAuthPage()
        {
            return LinkToQueryDictionary["authPage"];
        }

        public string GetAuthLink()
        {
            return LinkToQueryDictionary["auth"];
        }
    }
}