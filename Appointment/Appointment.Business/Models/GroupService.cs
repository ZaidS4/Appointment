using Appointment.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Appointment.DAL;
using Appointment.DAL.Models;

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
                using (RemindersEntities db = new RemindersEntities())
                {
                    var groups = db.Groups.ToList();

                    foreach (var item in groups)
                    {
                        groupViews.Add(new GroupsViewModel
                        {
                            ID = item.ID,
                            Name = item.Name,
                            EmployeesNumber = item.EmployeesGroups != null ? item.EmployeesGroups.Count() : 0
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
        /// ///////////////////////////////////////////////////////////
        /// 


        public static List<SelectListItem> GetAllEmployee()
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list = db.Employees.Select(m => new SelectListItem
                {
                    Value = m.ID.ToString(),
                    Text = m.Name,
                }).ToList();
                return list;
            }
        }
        /// ////////////////////////////////////////////////

        public static void Create(EmployeesGroupsViewModel group)
        {
            try
            {
                RemindersEntities Entities = new RemindersEntities();

                var entity = new Group();
                //entity.ID = group.ID;
                entity.Name = group.Name;
                entity.ModifyOn = group.ModifyOn;
                entity.CreatedOn = group.CreatedOn;
                entity.ModifyBy = group.ModifyBy;
                entity.CreatedBy = group.CreatedBY;
                Entities.Groups.Add(entity);
                Entities.SaveChanges();
                group.ID = entity.ID;
                foreach (var x in group.SelectedEmployeesID)
                {
                    Entities.EmployeesGroups.Add(new EmployeesGroup { EmployeeID = x, GroupID = entity.ID, CreatedOn = DateTime.Now, CreatedBY = 1, ModifyOn = DateTime.Now, ModifyBy = 1 });
                    Entities.SaveChanges();
                }


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
                RemindersEntities Entities = new RemindersEntities();

                var entity = new Group();
            
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
                RemindersEntities Entities = new RemindersEntities();

                var entity = new Group();

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



        public void Dispose()
        {
            RemindersEntities Entities = new RemindersEntities();

            Entities.Dispose();
        }
    }
}
