using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.CronJob;
using WebCounter.BusinessLogicLayer.Interfaces;

namespace Labitec.LeadForce.CronJobs.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {            
            var executionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var dataManager = new DataManager();
            var cronJobs = dataManager.CronJob.SelectAll().ToList();

            foreach (var job in cronJobs)
            {                
                var assembly = Assembly.LoadFrom(Path.Combine(executionPath, job.Assembly));
                var type = assembly.GetType(job.Type);

                var cronJob = dataManager.CronJob.SelectById(job.ID);

                var start = DateTime.Now;
                try
                {                    
                    var obj = (ICronJob)Activator.CreateInstance(type);
                    start = DateTime.Now;
                    obj.Run();

                    cronJob.LastRunStatusID = (int)LastRunStatus.Success;

                    if (!cronJob.NextRunPlannedAt.HasValue)
                        cronJob.NextRunPlannedAt = DateTime.Now;

                    while (cronJob.NextRunPlannedAt <= DateTime.Now)
                    {
                        cronJob.NextRunPlannedAt = cronJob.NextRunPlannedAt.Value.AddMinutes(job.Period);
                    }                    
                }
                catch (Exception ex)
                {
                    job.LastRunStatusID = (int)LastRunStatus.Error;
                    Log.Error(string.Format("Задача \"{0}\" выполнилась с ошибками", job.Name), ex);
                }
                finally
                {         
                    cronJob.LastRunAt = DateTime.Now;
                    cronJob.ExecutionTime = (int)(DateTime.Now - start).TotalSeconds;
                    dataManager.CronJob.Update(cronJob);
                }
            }
        }
    }
}
