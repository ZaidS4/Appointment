using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class ErrorObject
    {
        public string ErrorMessage { get; set; }
        public string FriendlyMessage { get; set; }
        public int HttpCode { get; set; }
    }
}
