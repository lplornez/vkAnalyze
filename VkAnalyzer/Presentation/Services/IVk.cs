

using System.Collections.Generic;

namespace Presentation.Services
{
    public interface IVk
    {
        KeyValuePair<int, string>? Auth(string login, string password);
       
    }
}