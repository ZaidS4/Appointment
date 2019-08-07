using Appointment.DAL.Models;
using Appointment.Resource;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Business.Job
{
    public class JobScheduler
    {

        public static void Start(int h,int m)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<MailSender>().Build();

            int hour = h;
            int minute = m;
            ITrigger trigger = TriggerBuilder.Create()
                //.StartNow()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(hour, minute))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }


    }
}
