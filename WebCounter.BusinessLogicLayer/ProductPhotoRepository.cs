using System;
using System.Collections.Generic;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class ProductPhotoRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductPhotoRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProductPhotoRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public List<tbl_ProductPhoto> SelectByProductId(Guid productId)
        {
            return _dataContext.tbl_ProductPhoto.Where(pp => pp.ProductId == productId).OrderByDescending(pp => pp.IsMain).ToList();
        }


        /// <summary>
        /// Selecs the main by product id.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public tbl_ProductPhoto SelectMainByProductId(Guid productId)
        {
            return _dataContext.tbl_ProductPhoto.Single(pp => pp.ProductId == productId && pp.IsMain);
        }




        /// <summary>
        /// Selects the specified product photo id.
        /// </summary>
        /// <param name="productPhotoId">The product photo id.</param>
        /// <returns></returns>
        public tbl_ProductPhoto Select(Guid productPhotoId)
        {
            return _dataContext.tbl_ProductPhoto.Single(pp => pp.Id == productPhotoId);
        }



        /// <summary>
        /// Deletes the specified product photo id.
        /// </summary>
        /// <param name="productPhotoId">The product photo id.</param>
        public void Delete(Guid productPhotoId)
        {
            var productPhoto = _dataContext.tbl_ProductPhoto.Single(pp => pp.Id == productPhotoId);            
            _dataContext.DeleteObject(productPhoto);
            _dataContext.SaveChanges();
        }


        /// <summary>
        /// Saves the specified product photo.
        /// </summary>
        /// <param name="productPhotoId">The product photo id.</param>
        /// <param name="productId">The product id.</param>
        /// <param name="photo">The photo.</param>
        /// <param name="preview">The preview.</param>
        /// <param name="description">The description.</param>
        /// <param name="isMain">if set to <c>true</c> [is main].</param>
        /// <returns></returns>
        public tbl_ProductPhoto Save(Guid productPhotoId, Guid productId, string photo, string preview, string description, bool isMain)
        {
            var productPhoto = _dataContext.tbl_ProductPhoto.Single(p => p.Id == productPhotoId) ?? new tbl_ProductPhoto();

            productPhoto.ProductId = productId;
            productPhoto.Photo = photo;
            productPhoto.Preview = preview;
            productPhoto.Description = description;
            productPhoto.IsMain = isMain;

            if (productPhotoId == Guid.Empty)
            {
                productPhoto.Id = Guid.NewGuid();
                _dataContext.tbl_ProductPhoto.AddObject(productPhoto);                
            }

            _dataContext.SaveChanges();

            return productPhoto;
        }



        /// <summary>
        /// Saves the specified product photo.
        /// </summary>
        /// <param name="productPhoto">The product photo.</param>
        /// <returns></returns>
        public tbl_ProductPhoto Save(tbl_ProductPhoto productPhoto)
        {
            var productPhotoToUpdate = _dataContext.tbl_ProductPhoto.Where(p => p.Id == productPhoto.Id).SingleOrDefault() ?? new tbl_ProductPhoto();

            productPhotoToUpdate.ProductId = productPhoto.ProductId;
            productPhotoToUpdate.Photo = productPhoto.Photo;
            productPhotoToUpdate.Preview = productPhoto.Preview;
            productPhotoToUpdate.Description = productPhoto.Description;
            productPhotoToUpdate.IsMain = productPhoto.IsMain;

            if (productPhotoToUpdate.Id == Guid.Empty)
            {
                productPhotoToUpdate.Id = Guid.NewGuid();
                _dataContext.tbl_ProductPhoto.AddObject(productPhotoToUpdate);
                _dataContext.SaveChanges();
            }

            return productPhotoToUpdate;
        }


    }
}