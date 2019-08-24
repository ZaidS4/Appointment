using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.Models
{
    public static class LookupService
    {
        /// <summary>
        /// gets TypeId from DB by code
        /// </summary>
        /// <returns>list of TypeId</returns>
        public static int GetLookupIdByCode(int code)
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {

                    var Id = db.Lookups.Where(x => x.Code == code.ToString()).FirstOrDefault().ID;
                    return Id;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
