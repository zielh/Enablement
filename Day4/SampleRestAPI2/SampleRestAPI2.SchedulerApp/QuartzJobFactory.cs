﻿using Quartz;
using Quartz.Spi;

namespace SampleRestAPI2.SchedulerApp
{
    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;


        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            //(job as IDisposable)?.Dispose();
        }
    }
}
