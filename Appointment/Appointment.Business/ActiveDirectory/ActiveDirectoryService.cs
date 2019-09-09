using Appointment.Business.Models;
using Appointment.DAL.Models;
using Appointment.ViewModel.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.ActiveDirectory
{
    public class ActiveDirectoryService : IJob
    {
        
        public void Execute(IJobExecutionContext context)
        {
          var List=  GetAllActiveUsers();
           

        }
        private PrincipalContext Context { get; set; }
        private List<ActiveDirectoryUsersVM> ActivDirectoryusers;
        private List<UsersViewModel> SystemUsers;



        public ActiveDirectoryService()
        {
            Context = new PrincipalContext(ContextType.Domain, ConfigurationManager.AppSettings["DomainName"].ToString(), ConfigurationManager.AppSettings["UserName"].ToString(), ConfigurationManager.AppSettings["Password"].ToString() );
        }

        public List<ActiveDirectoryUsersVM> GetAllActiveUsers(string EmployeeName = "")
        {
            ActivDirectoryusers = new List<ActiveDirectoryUsersVM>();
            UserPrincipal userPrincipal = new UserPrincipal(Context);

            //UserService sUser = null;
            //SystemUsers = sUser.UserAccount();

            using (var searcher = new PrincipalSearcher(userPrincipal))
            {
                foreach (var result in searcher.FindAll())
                {
                    DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                    string aDEmployeeName = de.Properties["cn"].Value.ToString();
                    string aDuserName = de.Properties["sAMAccountName"].Value.ToString();
                    string email = "";

                    if (de.Properties["mail"] != null &&
        
                        de.Properties["mail"].Count > 0)
                    {
                       email = de.Properties["mail"].Value.ToString();
                    }
                    
                    
                    
                    //bool isAvaliable = false;
                    //int ID = 0;                                        
                    //var EmployeeInfo = SystemUsers.Where(s => s.UserName.Contains(aDuserName));

                    //if (EmployeeInfo.Select(s => s.UserName).Any())
                    //{
                    //    isAvaliable = true;
                    //    ID = EmployeeInfo.Select(s => s.ID).First();
                    //}
                    ActivDirectoryusers.Add(new ActiveDirectoryUsersVM() {  Name = aDEmployeeName, Email = email });
                }
            }
            SaveEmployeesData(ActivDirectoryusers.ToList());

            return ActivDirectoryusers.ToList();

        }


        public static void SaveEmployeesData(List<ActiveDirectoryUsersVM> ActivDirectoryusers)
        {

            var ADemployees = ActivDirectoryusers;
            try
            {
                using (RemindersEntities db = new RemindersEntities())
                {
                    var employees = db.Employees.ToList();

                    foreach (var item in ADemployees)
                    {
                        if (!db.Employees.Any(x => x.Name == item.Name))
                        {
                            employees.Add(new Employee
                            {
                                Name = item.Name,
                                Email = item.Email
                            });
                        }
                        db.SaveChanges();

                    }
                    //foreach (var x in employees)
                    //{
                    //    db.Employees.Add(new Employee { Name = x.Name,Email=x.Email,CreatedOn=DateTime.Now.Date,CreatedBy=1 });
                    //    db.SaveChanges();
                    //}
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //public static List<ActiveDirectoryUsersVM> reademployeesinfo()
        //{
        //    return getemployeesinfo();
        //}




    }

}

