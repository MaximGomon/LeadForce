using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ObjectTypesRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectTypesRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ObjectTypesRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public tbl_ObjectTypes SelectByName(string name)
        {
            return _dataContext.tbl_ObjectTypes.SingleOrDefault(a => a.Name == name);
        }

    }
}