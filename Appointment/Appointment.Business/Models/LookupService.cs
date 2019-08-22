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
        /// gets TypeId from DB 
        /// </summary>
        /// <returns>list of TypeId</returns>
        public static Lookup GetLookupIdByCode(Reminder reminder)
        {
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {

                    var ID = db.Lookups.Where(x => x.Code == reminder.TypeID.ToString()).First(x=>x.ID==reminder.ID);
                    return ID;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
