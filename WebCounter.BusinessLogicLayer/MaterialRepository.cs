using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.BusinessLogicLayer.Mapping;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class MaterialRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MaterialRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by workflow template id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Material> SelectByWorkflowTemplateId(Guid siteId, Guid workflowTemplateId)
        {
            return _dataContext.tbl_Material.Where(a => a.SiteID == siteId && a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Selects the by workflow template id.
        /// </summary>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        /// <returns></returns>
        public IQueryable<tbl_Material> SelectByWorkflowTemplateId(Guid workflowTemplateId)
        {
            return _dataContext.tbl_Material.Where(a => a.WorkflowTemplateID == workflowTemplateId);
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <returns></returns>
        public tbl_Material SelectById(Guid materialId)
        {
            return _dataContext.tbl_Material.SingleOrDefault(a => a.ID == materialId);
        }



        /// <summary>
        /// Adds the specified contact role.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns></returns>
        public tbl_Material Add(tbl_Material material)
        {
            if (material.ID == Guid.Empty)
                material.ID = Guid.NewGuid();
            _dataContext.tbl_Material.AddObject(material);
            _dataContext.SaveChanges();

            return material;
        }



        /// <summary>
        /// Updates the specified contact role.
        /// </summary>
        /// <param name="material">The material.</param>
        public void Update(tbl_Material material)
        {
            _dataContext.SaveChanges();
        }



        /// <summary>
        /// Deletes the specified material id.
        /// </summary>
        /// <param name="materialId">The material id.</param>
        public void Delete(Guid materialId)
        {
            var material = SelectById(materialId);
            if (material != null)
            {
                _dataContext.DeleteObject(material);
                _dataContext.SaveChanges();
            }
        }



        /// <summary>
        /// Saves the specified workflow template parameter list.
        /// </summary>
        /// <param name="materialList">The material list.</param>
        /// <param name="workflowTemplateId">The workflow template id.</param>
        public void Save(List<MaterialMap> materialList, Guid workflowTemplateId)
        {
            var existsParameters = SelectByWorkflowTemplateId(workflowTemplateId).ToList();

            foreach (var parameter in materialList)
            {
                var existParameter = existsParameters.SingleOrDefault(a => a.ID == parameter.ID);

                if (existParameter == null)
                {
                    _dataContext.tbl_Material.AddObject(new tbl_Material
                    {
                        ID = parameter.ID,
                        SiteID = parameter.SiteID,
                        Name = parameter.Name,
                        Type = parameter.Type,
                        Description = parameter.Description,
                        Value = parameter.Value,
                        WorkflowTemplateID = workflowTemplateId,
                    });
                }
                else
                {
                    existParameter.ID = parameter.ID;
                    existParameter.SiteID = parameter.SiteID;
                    existParameter.Name = parameter.Name;
                    existParameter.Type = parameter.Type;
                    existParameter.Description = parameter.Description;
                    existParameter.Value = parameter.Value;
                    existParameter.WorkflowTemplateID = workflowTemplateId;
                }
            }

            foreach (var existsParameter in existsParameters)
            {
                if (materialList.SingleOrDefault(op => op.ID == existsParameter.ID) == null)
                    _dataContext.tbl_Material.DeleteObject(existsParameter);
            }

            _dataContext.SaveChanges();
        }
    }
}