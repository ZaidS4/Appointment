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

        ////////////////////////////////////////
        public static EmployeesGroupsViewModel EmployeeGroupsGetByID(int id)
        {
            using (RemindersEntities db = new RemindersEntities())
            {
                Group groups = db.Groups.Where(x => x.ID == id).FirstOrDefault();


                EmployeesGroupsViewModel EmployeegroupInfo = new EmployeesGroupsViewModel()
                {
                    ID = groups.ID,
                    GroupID=groups.ID,
                    Name = groups.Name,
                    SelectedEmployeesID = groups.EmployeesGroups.Select(x=>x.EmployeeID).ToArray()
                };
                EmployeegroupInfo.Employeelist = new List<EmployeesViewModel>();

                foreach (var emp in groups.EmployeesGroups)
                {
                    EmployeegroupInfo.Employeelist.Add(new EmployeesViewModel
                    {
                        Name = emp.Employee.Name
                    });
                }
                return EmployeegroupInfo;
            }
        }
       //////////////////// 
        public static bool HaveReminders(int id)
        {
            using (RemindersEntities Entities = new RemindersEntities())
            {
                Group entity = Entities.Groups.Find(id);
                var list = entity.RemindersGroups.ToList();

                if (list != null)
                    if(list.Count!=0)
                    return true;
                else
                    return false;
                else
                    return false;
            }
        } 
     

        /////////////////////////////////////////////////////
        public static void Delete(EmployeesGroupsViewModel group)
        {
            try
            {
                //DeleteEmployeesGroups(group);
                RemindersEntities Entities = new RemindersEntities();            
                Group entity = Entities.Groups.Find(group.ID);           
                var list = entity.EmployeesGroups;
                
                Entities.EmployeesGroups.RemoveRange(list);
                Entities.SaveChanges();
                Entities.Groups.Remove(entity);
                Entities.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /////////////////////////////////////////////////////////
        public static void EditGroup(EmployeesGroupsViewModel EmpGroup)
        {
            try
            {
                RemindersEntities Entities = new RemindersEntities();
                Group entity = Entities.Groups.Find(EmpGroup.ID);
                entity.Name = EmpGroup.Name;
                //entity.ModifyOn = EmpGroup.ModifyOn;
                //entity.CreatedOn = EmpGroup.CreatedOn;
                //entity.ModifyBy = EmpGroup.ModifyBy;
                //entity.CreatedBy = EmpGroup.CreatedBY;
                //;;
                Entities.Groups.Attach(entity);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.SaveChanges();
                //;;;;;
                var list = entity.EmployeesGroups;
                Entities.EmployeesGroups.RemoveRange(list);
                Entities.SaveChanges();
                foreach (var x in EmpGroup.SelectedEmployeesID)
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

        //////////////////////////////////////
        public void Dispose()
        {
            RemindersEntities Entities = new RemindersEntities();

            Entities.Dispose();
        }
    }
}
