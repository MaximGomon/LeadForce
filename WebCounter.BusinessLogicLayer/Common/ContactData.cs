using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Labitec.DataFeed;
using Labitec.DataFeed.Enum;
using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer.Common
{
    public class ContactData
    {
        private WebCounterServiceRepository _repository = new WebCounterServiceRepository();
        private WebCounterEntities _dataContext = new WebCounterEntities();
        private Guid _siteId;



        /// <summary>
        /// Initializes a new instance of the <see cref="ContactData"/> class.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        public ContactData(Guid siteId)
        {
            _siteId = siteId;
        }



        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <returns></returns>
        public List<FieldCollection> GetCollection(FormFieldType? fieldType = null, Guid? siteActivityRuleId = null, IQueryable<tbl_SiteColumns> siteColumns = null)
        {
            var fieldCollection = new List<FieldCollection>();

            if (!fieldType.HasValue || fieldType == FormFieldType.Input) // || fieldType == FormFieldType.Textarea
            {
                fieldCollection.Add(new FieldCollection { Value = "sys_fullname", Name = "Ф.И.О.", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_surname", Name = "Фамилия", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_name", Name = "Имя", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_patronymic", Name = "Отчество", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_email", Name = "E-mail", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_phone", Name = "Телефон", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_cellularphone", Name = "Сотовый", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_company", Name = "Компания", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_jobtitle", Name = "Должность", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_joblevel", Name = "Уровень должности", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_functionincompany", Name = "Функция в компании", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_country", Name = "Страна", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_city", Name = "Город", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_address", Name = "Адрес", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_comment", Name = "Комментарий", ColumnType = ColumnType.Text });
                fieldCollection.Add(new FieldCollection { Value = "sys_refferurl", Name = "Url", ColumnType = ColumnType.String });
                fieldCollection.Add(new FieldCollection { Value = "sys_advertisingplatform", Name = "Рекламная площадка", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_advertisingtype", Name = "Тип рекламы", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_advertisingcampaign", Name = "Рекламная кампания", ColumnType = ColumnType.Enum });
                fieldCollection.Add(new FieldCollection { Value = "sys_keywords", Name = "Ключевые слова", ColumnType = ColumnType.String });
            }

            if (siteColumns == null)
                siteColumns = _dataContext.tbl_SiteColumns.Where(a => a.SiteID == _siteId);

            siteColumns = siteActivityRuleId.HasValue
                              ? siteColumns.Where(a => a.SiteActivityRuleID == null || a.SiteActivityRuleID == siteActivityRuleId)
                              : siteColumns.Where(a => a.SiteActivityRuleID == null);

            if (fieldType.HasValue)
            {
                switch (fieldType)
                {
                    case FormFieldType.Input:
                        siteColumns = siteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.String || (ColumnType)a.TypeID == ColumnType.Number || (ColumnType)a.TypeID == ColumnType.Date));
                        break;
                    case FormFieldType.Textarea:
                        siteColumns = siteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.Text));
                        break;
                    case FormFieldType.Select:
                        siteColumns = siteColumns.Where(a => ((ColumnType)a.TypeID == ColumnType.Enum));
                        break;
                }
            }

            foreach (var siteColumn in siteColumns)
                fieldCollection.Add(new FieldCollection { Value = siteColumn.ID.ToString(), Name = siteColumn.Name, IsAdditional = true, ColumnType = (ColumnType)siteColumn.TypeID });   

            return fieldCollection;
        }



        public void CollectionToDropDownList(ref DropDownList dropDownList, FormFieldType? fieldType = null, Guid? siteActivityRuleId = null, IQueryable<tbl_SiteColumns> siteColumns = null)
        {
            var collection = GetCollection(fieldType, siteActivityRuleId, siteColumns);
            dropDownList.Items.Clear();
            foreach (var item in collection)
                dropDownList.Items.Add(new ListItem(item.Name, item.Value));
        }



        public string GetNameBySysName(string sysname)
        {
            return GetCollection().FirstOrDefault(a => a.Value == sysname).Name;
        }



        public FieldCollection GetFieldByValue(string value, Guid? siteActivityRuleId = null, IQueryable<tbl_SiteColumns> siteColumns = null)
        {
            return GetCollection(siteActivityRuleId: siteActivityRuleId, siteColumns: siteColumns).FirstOrDefault(a => a.Value == value);
        }


        public List<KeyValuePair<string, string>> GetContactData(Guid contactId)
        {
            var data = new List<KeyValuePair<string, string>>();

            var contact = _repository.Contact_SelectById(_siteId, contactId);
            data.Add(new KeyValuePair<string, string>("sys_fullname", contact.UserFullName));
            data.Add(new KeyValuePair<string, string>("sys_surname", contact.Surname));
            data.Add(new KeyValuePair<string, string>("sys_name", contact.Name));
            data.Add(new KeyValuePair<string, string>("sys_patronymic", contact.Patronymic));
            data.Add(new KeyValuePair<string, string>("sys_email", contact.Email));
            data.Add(new KeyValuePair<string, string>("sys_phone", contact.Phone));
            data.Add(new KeyValuePair<string, string>("sys_cellularphone", contact.CellularPhone));
            if (contact.CompanyID.HasValue)
            {
                var company = _repository.Company_SelectById((Guid)contact.CompanyID);
                data.Add(new KeyValuePair<string, string>("sys_company", company.Name));
            }
            else
                data.Add(new KeyValuePair<string, string>("sys_company", null));
            data.Add(new KeyValuePair<string, string>("sys_jobtitle", contact.JobTitle));
            data.Add(new KeyValuePair<string, string>("sys_joblevel", contact.ContactJobLevelID.ToString()));
            data.Add(new KeyValuePair<string, string>("sys_functionincompany", contact.ContactFunctionInCompanyID.ToString()));
            if (contact.AddressID.HasValue)
            {
                var address = _repository.Address_SelectById((Guid)contact.AddressID);
                data.Add(new KeyValuePair<string, string>("sys_country", address.CountryID != null ? address.CountryID.ToString() : null));
                data.Add(new KeyValuePair<string, string>("sys_city", address.CityID != null ? address.CityID.ToString() : null));
                data.Add(new KeyValuePair<string, string>("sys_address", address.Address));                
            }
            else
            {
                data.Add(new KeyValuePair<string, string>("sys_country", null));
                data.Add(new KeyValuePair<string, string>("sys_city", null));
                data.Add(new KeyValuePair<string, string>("sys_address", null));  
            }
            data.Add(new KeyValuePair<string, string>("sys_comment", contact.Comment));
            data.Add(new KeyValuePair<string, string>("sys_refferurl", contact.RefferURL));
            data.Add(new KeyValuePair<string, string>("sys_advertisingplatform", contact.AdvertisingPlatformID.ToString()));
            data.Add(new KeyValuePair<string, string>("sys_advertisingtype", contact.AdvertisingTypeID.ToString()));
            data.Add(new KeyValuePair<string, string>("sys_advertisingcampaign", contact.AdvertisingCampaignID.ToString()));
            //data.Add(new KeyValuePair<string, string>("sys_keywords", contact.));

            var contactColumnValues = _repository.GetContactColumnValues(contactId);
            foreach (var contactColumnValue in contactColumnValues)
            {
                switch ((ColumnType)contactColumnValue.TypeID)
                {
                    case ColumnType.String:
                    case ColumnType.Text:
                    case ColumnType.Number:
                        data.Add(new KeyValuePair<string, string>(contactColumnValue.SiteColumnID.ToString(), contactColumnValue.StringValue));
                        break;
                    case ColumnType.Date:
                        data.Add(new KeyValuePair<string, string>(contactColumnValue.SiteColumnID.ToString(), contactColumnValue.DateValue.ToString()));
                        break;
                    case ColumnType.Enum:
                        data.Add(new KeyValuePair<string, string>(contactColumnValue.SiteColumnID.ToString(), contactColumnValue.SiteColumnValueID.ToString()));
                        break;
                    case ColumnType.Logical:
                        data.Add(new KeyValuePair<string, string>(contactColumnValue.SiteColumnID.ToString(), contactColumnValue.LogicalValue.ToString()));
                        break;
                }
            }

            return data;
        }


        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <param name="siteColumnId">The site column id.</param>
        /// <returns></returns>
        public tbl_SiteColumns GetColumn(Guid siteColumnId)
        {
            return _repository.SiteColumns_SelectById(_siteId, siteColumnId);
        }



        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category id.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="code">The code.</param>
        /// <param name="siteActivityRuleId">The site activity rule id.</param>
        /// <returns></returns>
        public tbl_SiteColumns AddColumn(string name, Guid categoryId, int typeId, string code, Guid? siteActivityRuleId = null)
        {
            var siteColumn = new tbl_SiteColumns
                                 {
                                     ID = Guid.NewGuid(),
                                     SiteID = _siteId,
                                     Name = name,
                                     CategoryID = categoryId,
                                     Code = code
                                 };
            if (siteActivityRuleId.HasValue)
                siteColumn.SiteActivityRuleID = siteActivityRuleId;

            _dataContext.tbl_SiteColumns.AddObject(siteColumn);
            _dataContext.SaveChanges();

            return siteColumn;
        }



        /// <summary>
        /// Updates the column.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="categoryId">The category id.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public tbl_SiteColumns UpdateColumn(Guid id, string name, Guid categoryId, int typeId, string code)
        {
            var siteColumn = _dataContext.tbl_SiteColumns.FirstOrDefault(a => a.ID == id);
            if (siteColumn != null)
            {
                siteColumn.Name = name;
                siteColumn.CategoryID = categoryId;
                siteColumn.TypeID = typeId;
                siteColumn.Code = code;

                _dataContext.SaveChanges();
            }
            
            return siteColumn;
        }




        /// <summary>
        /// Deletes the column.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public bool DeleteColumn(Guid id)
        {
            var siteColumn = _dataContext.tbl_SiteColumns.FirstOrDefault(a => a.ID == id);
            if (siteColumn != null)
            {
                _dataContext.DeleteObject(siteColumn);
                _dataContext.SaveChanges();

                return true;
            }

            return false;
        }



        /// <summary>
        /// Gets the contact column value.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="siteColumnId">The site column id.</param>
        /// <returns></returns>
        public tbl_ContactColumnValues GetContactColumnValue(Guid contactId, Guid siteColumnId)
        {
            return _repository.ContactColumnValues_Select(contactId, siteColumnId);
        }



        /// <summary>
        /// Saves the form.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="values">The values.</param>
        /// <param name="saveSession">if set to <c>true</c> [save session].</param>
        /// <returns></returns>
        public tbl_Contact SaveForm(Guid contactId, IEnumerable<KeyValuePair<string, string>> values)
        {
            var keywords = string.Empty;

            var contact = _repository.Contact_SelectById(_siteId, contactId);
            if (contact != null)
            {
                foreach (var nameValuePair in values)
                {
                    Guid outId = Guid.Empty;
                    switch (nameValuePair.Key)
                    {
                        case "sys_fullname":
                            var nameChecker = new NameChecker(ConfigurationManager.AppSettings["ADONETConnectionString"]);
                            var nameCheck = nameChecker.CheckName(nameValuePair.Value, NameCheckerFormat.FIO, Correction.Correct);
                            if (!string.IsNullOrEmpty(nameCheck))
                            {
                                contact.UserFullName = nameCheck;
                                contact.Surname = nameChecker.Surname;
                                contact.Name = nameChecker.Name;
                                contact.Patronymic = nameChecker.Patronymic;
                                if (nameChecker.Gender.HasValue)
                                    contact.Gender = (int) nameChecker.Gender.Value;
                                contact.IsNameChecked = nameChecker.IsNameCorrect;
                            }
                            else
                                contact.UserFullName = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_surname":
                            contact.Surname = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                            break;
                        case "sys_name":
                            contact.Name = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                            break;
                        case "sys_patronymic":
                            contact.Patronymic = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            contact.UserFullName = string.Format("{0} {1} {2}", contact.Surname, contact.Name, contact.Patronymic);
                            break;
                        case "sys_email":
                            contact.Email = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_phone":
                            contact.Phone = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_cellularphone":
                            contact.CellularPhone = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_company":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                                contact.CompanyID = _repository.Company_SelectByNameAndCreate(contact.SiteID, nameValuePair.Value.Trim());
                            else
                                contact.CompanyID = null;
                            break;
                        case "sys_jobtitle":
                            contact.JobTitle = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_joblevel":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                            {
                                contact.ContactJobLevelID = Guid.TryParse(nameValuePair.Value.Trim(), out outId)
                                                                ? outId
                                                                : _repository.ContactJobLevel_SelectByNameAndCreate(contact.SiteID, nameValuePair.Value.Trim());
                            }
                            else
                                contact.ContactJobLevelID = null;
                            break;
                        case "sys_functionincompany":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                            {
                                contact.ContactFunctionInCompanyID = Guid.TryParse(nameValuePair.Value.Trim(), out outId)
                                                                         ? outId
                                                                         : _repository.ContactFunctionInCompany_SelectByNameAndCreate(contact.SiteID,nameValuePair.Value.Trim());
                            }
                            else
                                contact.ContactFunctionInCompanyID = null;
                            break;
                        case "sys_country":
                            if (contact.AddressID.HasValue)
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value))
                                    _repository.AddressCountry_Update((Guid)contact.AddressID, nameValuePair.Value.ToGuid());
                                else
                                    _repository.AddressCountry_Update((Guid)contact.AddressID, null);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value))
                                    contact.AddressID = _repository.AddressCountry_Add(nameValuePair.Value.ToGuid());
                            }
                            break;
                        case "sys_city":
                            if (contact.AddressID.HasValue)
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value))
                                    _repository.AddressCity_Update((Guid)contact.AddressID, nameValuePair.Value.ToGuid());
                                else
                                    _repository.AddressCity_Update((Guid)contact.AddressID, null);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value))
                                    contact.AddressID = _repository.AddressCity_Add(nameValuePair.Value.ToGuid());
                            }
                            break;
                        case "sys_address":
                            if (contact.AddressID.HasValue)
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                                    _repository.Address_Update((Guid)contact.AddressID, nameValuePair.Value.Trim());
                                else
                                    _repository.Address_Update((Guid)contact.AddressID, null);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(nameValuePair.Value))
                                    contact.AddressID = _repository.Address_Add(nameValuePair.Value);
                            }
                            break;
                        case "sys_comment":
                            contact.Comment = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        case "sys_refferurl":
                            contact.RefferURL = nameValuePair.Value;
                            break;
                        case "sys_advertisingplatform":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                            {
                                contact.AdvertisingPlatformID = Guid.TryParse(nameValuePair.Value.Trim(), out outId)
                                                                ? outId
                                                                : _repository.AdvertisingPlatform_SelectByTitleAndCreate(contact.SiteID, nameValuePair.Value.Trim());
                            }
                            else
                                contact.AdvertisingPlatformID = null;
                            break;
                        case "sys_advertisingtype":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                            {
                                contact.AdvertisingTypeID = Guid.TryParse(nameValuePair.Value.Trim(), out outId)
                                                                ? outId
                                                                : _repository.AdvertisingType_SelectByTitleAndCreate(contact.SiteID, nameValuePair.Value.Trim());
                            }

                            else
                                contact.AdvertisingTypeID = null;
                            break;
                        case "sys_advertisingcampaign":
                            if (!string.IsNullOrEmpty(nameValuePair.Value) && !string.IsNullOrEmpty(nameValuePair.Value.Trim()))
                            {
                                contact.AdvertisingCampaignID = Guid.TryParse(nameValuePair.Value.Trim(), out outId)
                                                                ? outId
                                                                : _repository.AdvertisingCampaign_SelectByTitleAndCreate(contact.SiteID, nameValuePair.Value.Trim());
                            }
                            else
                                contact.AdvertisingCampaignID = null;
                            break;
                        case "sys_keywords":
                            keywords = !string.IsNullOrEmpty(nameValuePair.Value) ? nameValuePair.Value.Trim() : null;
                            break;
                        default:
                            Guid outSiteColumnId;
                            if (Guid.TryParse(nameValuePair.Key.Replace("csf-", ""), out outSiteColumnId))
                            {
                                var siteColumn = GetColumn(outSiteColumnId);
                                if (siteColumn != null)
                                {
                                    var contactColumnValue = GetContactColumnValue(contact.ID, outSiteColumnId) ??
                                                             new tbl_ContactColumnValues
                                                                 {
                                                                     ContactID = contact.ID,
                                                                     SiteColumnID = outSiteColumnId
                                                                 };

                                    switch ((ColumnType) siteColumn.TypeID)
                                    {
                                        case ColumnType.String:
                                        case ColumnType.Number:
                                        case ColumnType.Text:
                                            contactColumnValue.StringValue = !string.IsNullOrEmpty(nameValuePair.Value) ? Regex.Replace(nameValuePair.Value.Trim(), @"\r\n", "<br />") : null;
                                            break;
                                        case ColumnType.Date:
                                            if (!string.IsNullOrEmpty(nameValuePair.Value))
                                                contactColumnValue.DateValue = DateTime.Parse(nameValuePair.Value.Trim());
                                            else
                                                contactColumnValue.DateValue = null;
                                            break;
                                        case ColumnType.Enum:
                                            if (!string.IsNullOrEmpty(nameValuePair.Value))
                                                contactColumnValue.SiteColumnValueID = Guid.Parse(nameValuePair.Value);
                                            else
                                                contactColumnValue.SiteColumnValueID = null;
                                            break;
                                        case ColumnType.Logical:
                                            contactColumnValue.LogicalValue = !string.IsNullOrEmpty(nameValuePair.Value) ? (bool?)bool.Parse(nameValuePair.Value) : null;
                                            break;
                                    }
                                    if (contactColumnValue.ID == Guid.Empty)
                                        _repository.ContactColumnValues_Add(contactColumnValue);
                                    else
                                        _repository.ContactColumnValues_Update(contactColumnValue);
                                }
                            }
                            break;
                    }
                }

                _repository.Contact_Update(contact);
            }

            return contact;
        }
    }



    public class FieldCollection
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public bool IsAdditional { get; set; }
        public FormFieldType FieldType { get; set; }
        public ColumnType ColumnType { get; set; }
    }
}