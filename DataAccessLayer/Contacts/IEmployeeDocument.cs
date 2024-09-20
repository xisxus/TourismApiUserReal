using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IEmployeeDocument
    {
        Task InsertEmployeeDocument(EmployeeDocumentDTO employeeDocumentDTO);
        Task<List<EmployeeDocumentDTO>> GetAllEmployeeDocuments(int employeeId);
    }
}
