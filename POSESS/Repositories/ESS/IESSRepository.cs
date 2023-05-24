using DMS.Common.OracleHelperClass;
using POSESS.ViewModels;
using System;
using System.Collections.Generic;
using WebAPIWT.Controllers;

namespace DMS.DAL.Repositories.PromotionRepository
{
    public interface IESSRepository
    { 
        (bool Success, string Message, dynamic Data) InsertESS(List<ESSViewModel> ESSViewModel);
        (bool Success, string Message) InsertTransactionList(List<ESSViewModel> itemlist);
        (bool Success, string Message) GetUserInformation(LoginRequest model);
    }
}
