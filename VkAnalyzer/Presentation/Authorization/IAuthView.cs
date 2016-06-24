using System;
using Presentation.BaseInterfaces;

namespace Presentation.Authorization
{
    public interface IAuthView: IBaseDataSource
    {   void FormShow();
        void FormClose();
    }
}