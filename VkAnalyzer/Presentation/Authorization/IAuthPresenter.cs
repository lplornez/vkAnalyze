using Presentation.BaseInterfaces;
using Presentation.BaseInterfaces.BaseInterfaces.PresentationInterfaces;

namespace Presentation.Authorization
{
    public interface IAuthPresenter: IBaseDataSource, IBaseInitialize
    {
        void FormShow();
        void FormClose();

    }
}