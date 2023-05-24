using POSESS.ViewModels;
using System.Collections.Generic;
using WebAPIWT.Controllers;

namespace DMS.Services.ESSServices
{
    public interface IESSService
    {
        (bool Success, string Message, dynamic Data) InsertESS(List<ESSViewModel> ESSVM);
        (bool Success, string Message) GetUserInformation(LoginRequest model);
    }
}
