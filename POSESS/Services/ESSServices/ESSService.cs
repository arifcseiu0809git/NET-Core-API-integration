using DMS.DAL.UnitOfWorks;
using POSESS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPIWT.Controllers;

namespace DMS.Services.ESSServices
{
    public class ESSService : IESSService
    {
        private readonly UnitOfWork _unitOfWork;
        public ESSService()
        {
            _unitOfWork = new UnitOfWork();
        } 

        public (bool Success, string Message, dynamic Data) InsertESS(List<ESSViewModel> ESSVM)
        {
            try
            {
                return _unitOfWork.ESSRepository.InsertESS(ESSVM);
                //return _unitOfWork.ESSRepository.InsertTransactionList(ESSVM);
            }
            catch (Exception ex)
            {

                return (false, ex?.Message?.ToString() ?? "",null);
            }
        }
        public (bool Success, string Message) GetUserInformation(LoginRequest model)
        {
            try
            {
                return _unitOfWork.ESSRepository.GetUserInformation(model);
            }
            catch (Exception ex)
            {

                return (false, ex?.Message?.ToString() ?? "");
            }
        }

    }
}
