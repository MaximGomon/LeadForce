using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Linq;
using WebCounter.BusinessLogicLayer;
using WebCounter.BusinessLogicLayer.Configuration;
using WebCounter.BusinessLogicLayer.Enumerations.CronJob;
using WebCounter.BusinessLogicLayer.Interfaces;

namespace WebCounter.AdminPanel
{
    public partial class CronJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var executionPath = Server.MapPath("bin");

            var dataManager = new DataManager();
            var cronJobs = dataManager.CronJob.SelectAll().ToList();

            foreach (var job in cronJobs)
            {
                if (HttpContext.Current.Application[string.Format("LeadForce.{0}AlreadyRunned", job.Name)] == null)
                    HttpContext.Current.Application[string.Format("LeadForce.{0}AlreadyRunned", job.Name)] = true;
                else
                    return;

                var assembly = Assembly.LoadFrom(Path.Combine(executionPath, job.Assembly));
                var type = assembly.GetType(job.Type);

                var cronJob = dataManager.CronJob.SelectById(job.ID);

                try
                {                    
                    var obj = (ICronJob) Activator.CreateInstance(type);
                    obj.Run();

                    cronJob.LastRunStatusID = (int)LastRunStatus.Success;

                    if (!cronJob.NextRunPlannedAt.HasValue)
                        cronJob.NextRunPlannedAt = DateTime.Now;

                    cronJob.NextRunPlannedAt = cronJob.NextRunPlannedAt.Value.AddMinutes(job.Period);                    
                }
                catch (Exception ex)
                {                    
                    job.LastRunStatusID = (int) LastRunStatus.Error;                    
                    Log.Error(string.Format("Задача \"{0}\" выполнилась с ошибками", job.Name), ex);
                }
                finally
                {
                    HttpContext.Current.Application[string.Format("LeadForce.{0}AlreadyRunned", job.Name)] = null;
                    cronJob.LastRunAt = DateTime.Now;
                    dataManager.CronJob.Update(cronJob);
                }
            }
        }
    }
}