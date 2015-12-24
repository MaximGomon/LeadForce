using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class TaskMemberRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskMemberRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public TaskMemberRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="taskMemberId">The task member id.</param>
        /// <returns></returns>
        public tbl_TaskMember SelectById(Guid taskMemberId)
        {
            return _dataContext.tbl_TaskMember.Where(o => o.ID == taskMemberId).SingleOrDefault();
        }



        /// <summary>
        /// Selects the by contact id.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <param name="contactId">The contact id.</param>
        /// <returns></returns>
        public tbl_TaskMember SelectByContactId(Guid taskId, Guid contactId)
        {
            return _dataContext.tbl_TaskMember.Where(o => o.TaskID == taskId && o.ContactID == contactId).FirstOrDefault();
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        /// <returns></returns>
        public IQueryable<tbl_TaskMember> SelectAll(Guid taskId)
        {
            return _dataContext.tbl_TaskMember.Where(o => o.TaskID == taskId);
        }



        /// <summary>
        /// Adds the specified task member.
        /// </summary>
        /// <param name="taskMember">The task member.</param>
        /// <returns></returns>
        public tbl_TaskMember Add(tbl_TaskMember taskMember)
        {
            taskMember.ID = Guid.NewGuid();
            _dataContext.tbl_TaskMember.AddObject(taskMember);
            _dataContext.SaveChanges();

            return taskMember;
        }



        /// <summary>
        /// Adds the specified task members.
        /// </summary>
        /// <param name="taskMembers">The task members.</param>
        /// <param name="taskId">The task id.</param>
        public void Add(List<tbl_TaskMember> taskMembers, Guid taskId)
        {
            foreach (var taskMember in taskMembers)
                _dataContext.tbl_TaskMember.AddObject(new tbl_TaskMember()
                {
                    ID = taskMember.ID,
                    TaskID = taskId,
                    ContractorID = taskMember.ContractorID,
                    ContactID = taskMember.ContactID,
                    OrderID = taskMember.OrderID,
                    OrderProductsID = taskMember.OrderProductsID,
                    TaskMemberRoleID = taskMember.TaskMemberRoleID,
                    TaskMemberStatusID = taskMember.TaskMemberStatusID,
                    Comment = taskMember.Comment,
                    UserComment = taskMember.UserComment,
                    IsInformed = taskMember.IsInformed
                });

            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="taskId">The task id.</param>
        public void DeleteAll(Guid taskId)
        {
            _dataContext.ExecuteStoreCommand("DELETE FROM tbl_TaskMember WHERE TaskID = @TaskID",
                                             new SqlParameter { ParameterName = "TaskID", Value = taskId });
        }



        /// <summary>
        /// Updates the specified task member.
        /// </summary>
        /// <param name="taskMember">The task member.</param>
        public void Update(tbl_TaskMember taskMember)
        {
            _dataContext.SaveChanges();
        }
    }
}