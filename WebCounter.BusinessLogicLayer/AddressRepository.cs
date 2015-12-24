using System;
using System.Linq;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class AddressRepository
    {
        private WebCounterEntities _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public AddressRepository(WebCounterEntities dataContext)
        {
            _dataContext = dataContext;
        }



        /// <summary>
        /// Selects the by id.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        /// <returns></returns>
        public tbl_Address SelectById(Guid addressId)
        {
            return _dataContext.tbl_Address.Where(a => a.ID == addressId).SingleOrDefault();
        }



        /// <summary>
        /// Adds the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Guid Add(tbl_Address address)
        {
            address.ID = Guid.NewGuid();
            _dataContext.tbl_Address.AddObject(address);
            _dataContext.SaveChanges();
            return address.ID;
        }



        /// <summary>
        /// Updates the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void Update(tbl_Address address)
        {
            var toUpdate = _dataContext.tbl_Address.Where(a => a.ID == address.ID).SingleOrDefault();
            if (toUpdate != null)
            {
                toUpdate.CountryID = address.CountryID;
                toUpdate.RegionID = address.RegionID;
                toUpdate.DistrictID = address.DistrictID;
                toUpdate.CityID = address.CityID;
                toUpdate.Address = address.Address;
                _dataContext.SaveChanges();
            }
        }
    }
}