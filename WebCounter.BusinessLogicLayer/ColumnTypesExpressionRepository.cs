using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ColumnTypesExpressionRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnTypesExpressionRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ColumnTypesExpressionRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Seelects all column categories.
        /// </summary>
        /// <returns></returns>
        public IQueryable<tbl_ColumnTypesExpression> SelectByColumnTypeId(int columnTypeId)
        {
            return _dataContext.tbl_ColumnTypesExpression.Where(a => a.ColumnTypesID == columnTypeId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public tbl_ColumnTypesExpression SelectById(Guid id)
        {
            return _dataContext.tbl_ColumnTypesExpression.FirstOrDefault(a => a.ID == id);
        }
    }
}