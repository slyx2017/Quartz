using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzWeb
{
    public static class QuartzService
    {
        #region 单任务
        public static void StartJob<TJob>() where TJob : IJob
        {
            var scheduler = new StdSchedulerFactory().GetScheduler().Result;           

            var job = JobBuilder.Create<TJob>()
              .WithIdentity("deletefilejob")
              .Build();

            var trigger1 = TriggerBuilder.Create()
              .WithIdentity("deletefilejob.trigger")
              .StartNow()
              //.WithCronSchedule("0 0 10 * * ?")
              .WithSimpleSchedule(x=>x.WithIntervalInSeconds(10).RepeatForever())
              .ForJob(job)
              .Build();

            scheduler.AddJob(job, true);
            scheduler.ScheduleJob(job, trigger1);
            scheduler.Start();
        }
        #endregion
    }
}
