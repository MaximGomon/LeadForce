using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ExternalResourceRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalResourceRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ExternalResourceRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="externalResourceId">The external resource id.</param>
        /// <returns></returns>
        public tbl_ExternalResource SelectById(Guid externalResourceId)
        {
            return _dataContext.tbl_ExternalResource.SingleOrDefault(o => o.ID == externalResourceId);
        }



        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="destinationId">The destination id.</param>
        /// <returns></returns>
        public IQueryable<tbl_ExternalResource> SelectAll(Guid destinationId)
        {
            return _dataContext.tbl_ExternalResource.Where(o => o.DestinationID == destinationId);
        }



        /// <summary>
        /// Adds the specified web site external resource.
        /// </summary>
        /// <param name="externalResource">The external resource.</param>
        /// <returns></returns>
        public tbl_ExternalResource Add(tbl_ExternalResource externalResource)
        {
            externalResource.ID = Guid.NewGuid();
            _dataContext.tbl_ExternalResource.AddObject(externalResource);
            _dataContext.SaveChanges();

            return externalResource;        
        }



        /// <summary>
        /// Updates the specified to update web site external resource.
        /// </summary>
        /// <param name="toUpdateExternalResource">To update web site external resource.</param>
        /// <param name="destinationId">The destination id.</param>
        public void Update(List<ExternalResourceMap> toUpdateExternalResource, Guid destinationId)
        {
            var existsExternalResources = SelectAll(destinationId).ToList();
            
            foreach (var updateExternalResource in toUpdateExternalResource)
            {
                var existExternalResource = existsExternalResources.SingleOrDefault(op => op.ID == updateExternalResource.ID);

                if (existExternalResource == null)
                {        
                    _dataContext.tbl_ExternalResource.AddObject(new tbl_ExternalResource()
                    {
                        ID = updateExternalResource.ID,
                        DestinationID = destinationId,
                        Title = updateExternalResource.Title,
                        ResourcePlaceID = updateExternalResource.ResourcePlaceID,
                        ExternalResourceTypeID = updateExternalResource.ExternalResourceTypeID,
                        File = updateExternalResource.File,
                        Text = updateExternalResource.Text,
                        Url = updateExternalResource.Url
                    }); 
                }
                else
                {
                    existExternalResource.Title = updateExternalResource.Title;
                    existExternalResource.ResourcePlaceID = updateExternalResource.ResourcePlaceID;
                    existExternalResource.ExternalResourceTypeID = updateExternalResource.ExternalResourceTypeID;
                    existExternalResource.File = updateExternalResource.File;
                    existExternalResource.Text = updateExternalResource.Text;
                    existExternalResource.Url = updateExternalResource.Url;                    
                }
            }

            foreach (var existsExternalResource in existsExternalResources)
            {
                if (toUpdateExternalResource.SingleOrDefault(op => op.ID == existsExternalResource.ID) == null)
                    _dataContext.tbl_ExternalResource.DeleteObject(existsExternalResource);
            }

            _dataContext.SaveChanges();
        }
    }
}