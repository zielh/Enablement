using Microsoft.Extensions.Logging;
using Quartz;
using SampleRestAPI2.DAL.Repository;

namespace SampleRestAPI2.SchedulerApp.Job
{
    public class LogInsertJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public LogInsertJob(ILogger<LogInsertJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _unitOfWork.Logs.Add(new DAL.Models.Logs()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Text = $"Current Date : {DateTime.UtcNow}"
            });
            _unitOfWork.Complete();
            _logger.LogInformation($"Current Date : {DateTime.UtcNow}");
        }
    }
}
