using System;
using WebCounter.BusinessLogicLayer.Interfaces;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Services.TemplateTagsReplacers
{
    public class TaskNotificationTagsReplacer : TemplateTagsReplacer
    {
        public TaskNotificationTagsReplacer(tbl_SiteAction siteAction) : base(siteAction) {}

        public override void Replace(ref string subject, ref string body)
        {
            if (!SiteAction.ObjectID.HasValue)
                return;

            var task = DataManager.Task.SelectById(SiteAction.SiteID, (Guid)SiteAction.ObjectID);
            var creator = DataManager.Contact.SelectById(SiteAction.SiteID, task.CreatorID);

            body = body.Replace("#Task.Title#", task.Title)
                       .Replace("#Author.UserFullName#", creator.UserFullName);

            subject = subject.Replace("#Task.Title#", task.Title)
                       .Replace("#Author.UserFullName#", creator.UserFullName);

            var company = creator.tbl_Company1;
            body = body.Replace("#Author.Company#", company != null ? company.Name : string.Empty);
            subject = subject.Replace("#Author.Company#", company != null ? company.Name : string.Empty);            
        }
    }
}
