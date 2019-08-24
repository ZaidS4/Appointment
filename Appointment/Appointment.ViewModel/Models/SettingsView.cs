﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.ViewModel.Models
{
    public class SettingsView
    {
        [Display(Name = "Birthday Email Text")]
        public String BirthDayEmailText { get; set; }


        [Display(Name = "Anniversary Email Text")]
        public String AnniversaryEmailText { get; set; }


        [Display(Name = "Birthday Reminder")]
        public string BirthdayReminder { get; set; }


        [Display(Name = "Anniversary Reminder")]
        public string AnniversaryReminder { get; set; }


        [Display(Name = "Event Reminder")]
        public string EventReminder { get; set; }


        //[DataType(DataType.Time)]
        //[Display(Name = "Remind Me At")]
        //public DateTime RemindMeAt { get; set; }


        [Display(Name = "Send Birthday Email")]
        public string SendBirthday { get; set; }


        [Display(Name = "Send Anniversary Email")]
        public string SendAnniversary { get; set; }
        

        [Display(Name = "Send Event Email")]
        public string SendEvent { get; set; }


        //[DataType(DataType.Time)]
        //[Display(Name = "Send Reminder Email At")]
        //public DateTime SendEmailAt { get; set; }

        [Display(Name = "Upcoming Reminders")]
        public string UpComingReminder { get; set; }


        [Display(Name = "Sender Email")]
        [DataType(DataType.EmailAddress)]
        public String EmailSender { get; set; }



        [Display(Name = "Sender Password")]
        [DataType(DataType.Password)]
        public String PasswordSender { get; set; }



        [Display(Name = "smtp Address")]
        public String smtpaddress { get; set; }

        [Display(Name = "Port Number")]
        public int portnumber { get; set; }



        [Display(Name = "Admin Email")]
        [DataType(DataType.EmailAddress)]
        public String EmailAdmin { get; set; }
    }
}