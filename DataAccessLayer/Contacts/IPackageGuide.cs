using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace DataAccessLayer.Contacts
{
    public interface IPackageGuide
    {
        Task<GeneralResponse> InsertPackageGuide(PackageGuideDTO packageGuideDTO);
        Task<GeneralResponse> UpdatePackageGuide(int id, PackageGuideDTO packageGuideDTO);
        Task<GeneralResponse> DeletePackageGuide(int id);
        Task<GeneralResponseData<List<PackageGuideDTO>>> GetAllPackageGuides();
        Task<GeneralResponseData<List<PackageGuideDTO>>> GetPackageGuideByGuideId(int guideId);
    }
}
