using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;

namespace Appointment.Business.Models
{
    public class GroupService : IDisposable
    {
        private static bool UpdateDatabase = false;

        public static List<GroupsViewModel> GetAll()
        {
            List<GroupsViewModel> groupViews = new List<GroupsViewModel>();

            try
            {
                using (AppointmentContext db = new AppointmentContext())
                {
                    var groups = db.Groups.ToList();

                    foreach (var item in groups)
                    {
                        groupViews.Add(new GroupsViewModel
                        {
                            ID = item.ID,
                            Name = item.Name,
                            // CreatedOn = item.CreatedOn
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return groupViews;
        }

        public static List<GroupsViewModel> Read()
        {
            return GetAll();
        }

        public static void Create(EmployeesGroupsViewModel group)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();

                var entity = new Groups();
                //entity.ID = group.ID;
                entity.Name = group.Name;
                //entity.CreatedOn = group.CreatedOn;   
                //if(group.EmployeeID.HasValue)
                //   entity.EmployeesGroups.Add(new EmployeesGroups { EmployeeID = group.EmployeeID.Value });
                Entities.Groups.Add(entity);
                Entities.SaveChanges();
                group.ID = entity.ID;
                Entities.EmployeesGroups.Add(new EmployeesGroups { EmployeeID = group.EmployeeID.Value, GroupID = entity.ID });
                Entities.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(GroupsViewModel group)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();

                var entity = new Groups();

                //entity.ID = group.ID;
                entity.Name = group.Name;
                entity.CreatedOn = group.CreatedOn;
                Entities.Groups.Attach(entity);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.SaveChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Delete(GroupsViewModel group)
        {
            try
            {
                AppointmentContext Entities = new AppointmentContext();

                var entity = new Groups();

                entity.ID = group.ID;
                Entities.Groups.Attach(entity);
                Entities.Groups.Remove(entity);
                var groupDetails = Entities.Groups.Where(pd => pd.ID == entity.ID);

                foreach (var groupDetail in groupDetails)
                {
                    Entities.Groups.Remove(groupDetail);
                }


                Entities.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// ///////////////////////////////////////////////////////////
        /// 

        //public static List <EmployeesGroupsViewModel> GetAllEmployee()
        //{
        //    List<EmployeesGroupsViewModel> Employeelist = new List<EmployeesGroupsViewModel>();
        //    try
        //    {

        //        using (AppointmentContext db = new AppointmentContext())
        //        {
        //            var employees = db.Employees.ToList();
        //            foreach (var item in employees)
        //            {
        //                Employeelist.Add(new EmployeesGroupsViewModel
        //                {
        //                    ID = item.ID,
        //                    EmployeeName=item.Name,
        //                    EmployeeEmail=item.Email,
        //                    CreatedBY=item.CreatedBy,
        //                    CreatedOn = item.CreatedOn,
        //                    //ModifyBy=item.ModifyBy,
        //                   // ModifyOn=item.ModifyOn,
        //                    BirthDate=item.BirthDate
        //                    //CreatedOn=item.CreatedOn

        //                });

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Employeelist;
        //}




        //public static GroupsViewModel One(Func<GroupsViewModel, bool> predicate)
        //{
        //    try
        //    {
        //        AppointmentContext Entities = new AppointmentContext();

        //        return GetAll().FirstOrDefault(predicate);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public static List<SelectListItem> GetAllEmployee()
        {
            using (AppointmentContext db = new AppointmentContext())
            {
                var list = db.Employees.Select(m => new SelectListItem
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                }).ToList();
                return list;
            }


        }
        /// <summary>
        /// ////////////////////////////////////////////////
        /// </summary>
        public void Dispose()
        {
            AppointmentContext Entities = new AppointmentContext();

            Entities.Dispose();
        }
    }
}
