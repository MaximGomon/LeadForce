using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class FormulaRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormulaRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public FormulaRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        public List<tbl_Formula> SelectAll()
        {
            return _dataContext.tbl_Formula.OrderBy(a => a.Order).ToList();
        }



        /// <summary>
        /// Selects the specified column type.
        /// </summary>
        /// <param name="columnType">Type of the column.</param>
        /// <returns></returns>
        public List<tbl_Formula> Select(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.String:
                case ColumnType.Text:
                    return _dataContext.tbl_Formula.Where(a => a.ID == 4 || a.ID == 5).ToList();
                case ColumnType.Date:
                    return _dataContext.tbl_Formula.Where(a => a.ID == 6 || a.ID == 7 || a.ID == 8 || a.ID == 9).OrderBy(a => a.Order).ToList();
                case ColumnType.Enum:
                    return _dataContext.tbl_Formula.Where(a => a.ID == 1 || a.ID == 2 || a.ID == 3).OrderBy(a => a.Order).ToList();
                case ColumnType.Number:
                    return _dataContext.tbl_Formula.Where(a => a.ID == 6 || a.ID == 7 || a.ID == 8 || a.ID == 9).OrderBy(a => a.Order).ToList();
            }

            return null;
        }
    }
}