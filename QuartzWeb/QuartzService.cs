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
              .WithCronSchedule("0 0 10 * * ?")
              .ForJob(job)
              .Build();

            scheduler.AddJob(job, true);
            scheduler.ScheduleJob(job, trigger1);
            scheduler.Start();
        }
        #endregion


        #region 配置
        public static void AddQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzFactory>();
            services.Add(jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));

            services.AddSingleton(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }
        #endregion
    }
}
