using ms.employees.application.HttpCommunications.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.employees.application.HttpCommunications
{
    public interface IAttendanceApiCommunication
    {
        [Get("/Attendances/GetAllAttendances")]
        Task<IEnumerable<AttendanceApiCommunicationResponse>> GetAllAttendances(string userName, [Header("Authorization")] string token);
    }
}
