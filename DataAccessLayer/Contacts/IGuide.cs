using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IGuide
    {
        Task<ServiceResponse.GeneralResponse> InsertGuide(GuideDTO guideDTO);
        Task<ServiceResponse.GeneralResponse> UpdateGuide(int id, GuideDTO guideDTO);
        Task<ServiceResponse.GeneralResponse> DeleteGuide(int guideId);
        Task<ServiceResponse.GeneralResponseData<List<GuideDTO>>> GetAllGuides();
        Task<ServiceResponse.GeneralResponseData<GuideDTO>> GetGuideById(int guideId);
    }
}
