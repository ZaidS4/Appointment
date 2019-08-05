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

        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<MailSender>().Build();

            int hour = Convert.ToInt32(StringResource.SchdulerStartHour24);
            int minute = Convert.ToInt32(StringResource.SchdulerStartMinute);
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
