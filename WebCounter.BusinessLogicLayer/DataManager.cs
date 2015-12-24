using WebCounter.DataAccessLayer;

namespace WebCounter.BusinessLogicLayer
{
    public class DataManager
    {
        private WebCounterEntities _dataContext;

        public DataManager()
        {
            _dataContext = new WebCounterEntities();
            _dataContext.CommandTimeout = 120;
        }



        private CompanyRepository _companyRepository;
        public CompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_dataContext);

                return _companyRepository;
            }
        }



        private ContactRepository _contactRepository;
        public ContactRepository Contact
        {
            get
            {
                if (_contactRepository == null)
                    _contactRepository = new ContactRepository(_dataContext);

                return _contactRepository;
            }
        }



        private ContactRoleRepository _contactRoleRepository;
        public ContactRoleRepository ContactRole
        {
            get
            {
                if (_contactRoleRepository == null)
                    _contactRoleRepository = new ContactRoleRepository(_dataContext);

                return _contactRoleRepository;
            }
        }



        private ContactToContactRoleRepository _contactToContactRoleRepository;
        public ContactToContactRoleRepository ContactToContactRole
        {
            get
            {
                if (_contactToContactRoleRepository == null)
                    _contactToContactRoleRepository = new ContactToContactRoleRepository(_dataContext);

                return _contactToContactRoleRepository;
            }
        }



        private ContactActivityRepository _contactActivityRepository;
        public ContactActivityRepository ContactActivity
        {
            get
            {
                if (_contactActivityRepository == null)
                    _contactActivityRepository = new ContactActivityRepository(_dataContext);

                return _contactActivityRepository;
            }
        }



        private StatusRepository _statusRepository;
        public StatusRepository Status
        {
            get
            {
                if (_statusRepository == null)
                    _statusRepository = new StatusRepository(_dataContext);

                return _statusRepository;
            }
        }



        private SiteColumnsRepository _siteColumnsRepository;
        public SiteColumnsRepository SiteColumns
        {
            get
            {
                if (_siteColumnsRepository == null)
                    _siteColumnsRepository = new SiteColumnsRepository(_dataContext);

                return _siteColumnsRepository;
            }
        }



        private ColumnCategoriesRepository _columnCategoriesRepository;
        public ColumnCategoriesRepository ColumnCategories
        {
            get
            {
                if (_columnCategoriesRepository == null)
                    _columnCategoriesRepository = new ColumnCategoriesRepository(_dataContext);

                return _columnCategoriesRepository;
            }
        }



        private ColumnTypesRepository _columnTypesRepository;
        public ColumnTypesRepository ColumnTypes
        {
            get
            {
                if (_columnTypesRepository == null)
                    _columnTypesRepository = new ColumnTypesRepository(_dataContext);

                return _columnTypesRepository;
            }
        }



        private ColumnTypesExpressionRepository _columnTypesExpressionRepository;
        public ColumnTypesExpressionRepository ColumnTypesExpression
        {
            get
            {
                if (_columnTypesExpressionRepository == null)
                    _columnTypesExpressionRepository = new ColumnTypesExpressionRepository(_dataContext);

                return _columnTypesExpressionRepository;
            }
        }



        private SiteColumnValuesRepository _siteColumnValuesRepository;
        public SiteColumnValuesRepository SiteColumnValues
        {
            get
            {
                if (_siteColumnValuesRepository == null)
                    _siteColumnValuesRepository = new SiteColumnValuesRepository(_dataContext);

                return _siteColumnValuesRepository;
            }
        }



        private SitesRepository _sitesRepository;
        public SitesRepository Sites
        {
            get
            {
                if (_sitesRepository == null)
                    _sitesRepository = new SitesRepository(_dataContext);

                return _sitesRepository;
            }
        }



        private SiteActivityRulesRepository _siteActivityRulesRepository;
        public SiteActivityRulesRepository SiteActivityRules
        {
            get
            {
                if (_siteActivityRulesRepository == null)
                    _siteActivityRulesRepository = new SiteActivityRulesRepository(_dataContext);

                return _siteActivityRulesRepository;
            }
        }


        private LinksRepository _linksRepository;
        public LinksRepository Links
        {
            get
            {
                if (_linksRepository == null)
                    _linksRepository = new LinksRepository(_dataContext);

                return _linksRepository;
            }
        }


        private RuleTypesRepository _ruleTypesRepository;
        public RuleTypesRepository RuleTypes
        {
            get
            {
                if (_ruleTypesRepository == null)
                    _ruleTypesRepository = new RuleTypesRepository(_dataContext);

                return _ruleTypesRepository;
            }
        }



        private ActivityTypesRepository _activityTypesRepository;
        public ActivityTypesRepository ActivityTypes
        {
            get
            {
                if (_activityTypesRepository == null)
                    _activityTypesRepository = new ActivityTypesRepository(_dataContext);

                return _activityTypesRepository;
            }
        }



        private SiteActivityRuleFormColumnsRepository _siteActivityRuleFormColumnsRepository;
        public SiteActivityRuleFormColumnsRepository SiteActivityRuleFormColumns
        {
            get
            {
                if (_siteActivityRuleFormColumnsRepository == null)
                    _siteActivityRuleFormColumnsRepository = new SiteActivityRuleFormColumnsRepository(_dataContext);

                return _siteActivityRuleFormColumnsRepository;
            }
        }



        private ContactColumnValuesRepository _contactColumnValuesRepository;
        public ContactColumnValuesRepository ContactColumnValues
        {
            get
            {
                if (_contactColumnValuesRepository == null)
                    _contactColumnValuesRepository = new ContactColumnValuesRepository(_dataContext);

                return _contactColumnValuesRepository;
            }
        }



        private ReadyToSellRepository _readyToSellRepository;
        public ReadyToSellRepository ReadyToSell
        {
            get
            {
                if (_readyToSellRepository == null)
                    _readyToSellRepository = new ReadyToSellRepository(_dataContext);

                return _readyToSellRepository;
            }
        }


        private EventCategoriesRepository _eventCategoriesRepository;
        public EventCategoriesRepository EventCategories
        {
            get
            {
                if (_eventCategoriesRepository == null)
                    _eventCategoriesRepository = new EventCategoriesRepository(_dataContext);

                return _eventCategoriesRepository;
            }
        }



        private SiteEventTemplatesRepository _siteEventTemplatesRepository;
        public SiteEventTemplatesRepository SiteEventTemplates
        {
            get
            {
                if (_siteEventTemplatesRepository == null)
                    _siteEventTemplatesRepository = new SiteEventTemplatesRepository(_dataContext);

                return _siteEventTemplatesRepository;
            }
        }



        private SiteEventTemplateActivityRepository _siteEventTemplateActivityRepository;
        public SiteEventTemplateActivityRepository SiteEventTemplateActivity
        {
            get
            {
                if (_siteEventTemplateActivityRepository == null)
                    _siteEventTemplateActivityRepository = new SiteEventTemplateActivityRepository(_dataContext);

                return _siteEventTemplateActivityRepository;
            }
        }



        private FormulaRepository _formulaRepository;
        public FormulaRepository Formula
        {
            get
            {
                if (_formulaRepository == null)
                    _formulaRepository = new FormulaRepository(_dataContext);

                return _formulaRepository;
            }
        }



        private ActionTypesRepository _actionTypesRepository;
        public ActionTypesRepository ActionTypes
        {
            get
            {
                if (_actionTypesRepository == null)
                    _actionTypesRepository = new ActionTypesRepository(_dataContext);

                return _actionTypesRepository;
            }
        }



        private SiteActionTemplateRepository _siteActionTemplateRepository;
        public SiteActionTemplateRepository SiteActionTemplate
        {
            get
            {
                if (_siteActionTemplateRepository == null)
                    _siteActionTemplateRepository = new SiteActionTemplateRepository(_dataContext);

                return _siteActionTemplateRepository;
            }
        }



        private SiteActionTemplateRecipientRepository _siteActionTemplateRecipientRepository;
        public SiteActionTemplateRecipientRepository SiteActionTemplateRecipient
        {
            get
            {
                if (_siteActionTemplateRecipientRepository == null)
                    _siteActionTemplateRecipientRepository = new SiteActionTemplateRecipientRepository(_dataContext);

                return _siteActionTemplateRecipientRepository;
            }
        }



        private StartAfterTypesRepository _startAfterTypesRepository;
        public StartAfterTypesRepository StartAfterTypes
        {
            get
            {
                if (_startAfterTypesRepository == null)
                    _startAfterTypesRepository = new StartAfterTypesRepository(_dataContext);

                return _startAfterTypesRepository;
            }
        }



        private SiteActionRepository _siteActionRepository;
        public SiteActionRepository SiteAction
        {
            get
            {
                if (_siteActionRepository == null)
                    _siteActionRepository = new SiteActionRepository(_dataContext);

                return _siteActionRepository;
            }
        }



        private ActionStatusRepository _actionStatusRepository;
        public ActionStatusRepository ActionStatus
        {
            get
            {
                if (_actionStatusRepository == null)
                    _actionStatusRepository = new ActionStatusRepository(_dataContext);

                return _actionStatusRepository;
            }
        }



        private SiteEventActionTemplateRepository _siteEventActionTemplateRepository;
        public SiteEventActionTemplateRepository SiteEventActionTemplate
        {
            get
            {
                if (_siteEventActionTemplateRepository == null)
                    _siteEventActionTemplateRepository = new SiteEventActionTemplateRepository(_dataContext);

                return _siteEventActionTemplateRepository;
            }
        }



        private SiteActivityScoreTypeRepository _siteActivityScoreTypeRepository;
        public SiteActivityScoreTypeRepository SiteActivityScoreType
        {
            get
            {
                if (_siteActivityScoreTypeRepository == null)
                    _siteActivityScoreTypeRepository = new SiteActivityScoreTypeRepository(_dataContext);

                return _siteActivityScoreTypeRepository;
            }
        }



        private SiteEventTemplateScoreRepository _siteEventTemplateScoreRepository;
        public SiteEventTemplateScoreRepository SiteEventTemplateScore
        {
            get
            {
                if (_siteEventTemplateScoreRepository == null)
                    _siteEventTemplateScoreRepository = new SiteEventTemplateScoreRepository(_dataContext);

                return _siteEventTemplateScoreRepository;
            }
        }



        private OperationsRepository _operationsRepository;
        public OperationsRepository Operations
        {
            get
            {
                if (_operationsRepository == null)
                    _operationsRepository = new OperationsRepository(_dataContext);

                return _operationsRepository;
            }
        }



        private ObjectTypesRepository _objectTypesRepository;
        public ObjectTypesRepository ObjectTypes
        {
            get
            {
                if (_objectTypesRepository == null)
                    _objectTypesRepository = new ObjectTypesRepository(_dataContext);

                return _objectTypesRepository;
            }
        }



        private LogicConditionsRepository _logicConditionsRepository;
        public LogicConditionsRepository LogicConditions
        {
            get
            {
                if (_logicConditionsRepository == null)
                    _logicConditionsRepository = new LogicConditionsRepository(_dataContext);

                return _logicConditionsRepository;
            }
        }



        private ContactActivityScoreRepository _contactActivityScoreRepository;
        public ContactActivityScoreRepository ContactActivityScore
        {
            get
            {
                if (_contactActivityScoreRepository == null)
                    _contactActivityScoreRepository = new ContactActivityScoreRepository(_dataContext);

                return _contactActivityScoreRepository;
            }
        }



        private SiteActionTemplateUserColumnRepository _siteActionTemplateUserColumnRepository;
        public SiteActionTemplateUserColumnRepository SiteActionTemplateUserColumn
        {
            get
            {
                if (_siteActionTemplateUserColumnRepository == null)
                    _siteActionTemplateUserColumnRepository = new SiteActionTemplateUserColumnRepository(_dataContext);

                return _siteActionTemplateUserColumnRepository;
            }
        }


        private UserSettingsRepository _userSettingsRepository;
        public UserSettingsRepository UserSettings
        {
            get
            {
                if (_userSettingsRepository == null)
                    _userSettingsRepository = new UserSettingsRepository(_dataContext);

                return _userSettingsRepository;
            }
        }


        private PrioritiesRepository _prioritiesRepository;
        public PrioritiesRepository Priorities
        {
            get
            {
                if (_prioritiesRepository == null)
                    _prioritiesRepository = new PrioritiesRepository(_dataContext);

                return _prioritiesRepository;
            }
        }



        private SiteTagObjectsRepository _siteTagObjectsRepository;
        public SiteTagObjectsRepository SiteTagObjects
        {
            get
            {
                if (_siteTagObjectsRepository == null)
                    _siteTagObjectsRepository = new SiteTagObjectsRepository(_dataContext);

                return _siteTagObjectsRepository;
            }
        }



        private MassMailRepository _massMailRepository;
        public MassMailRepository MassMail
        {
            get
            {
                if (_massMailRepository == null)
                    _massMailRepository = new MassMailRepository(_dataContext);

                return _massMailRepository;
            }
        }



        private MassMailStatusRepository _massMailStatusRepository;
        public MassMailStatusRepository MassMailStatus
        {
            get
            {
                if (_massMailStatusRepository == null)
                    _massMailStatusRepository = new MassMailStatusRepository(_dataContext);

                return _massMailStatusRepository;
            }
        }



        private MassMailContactRepository _massMailContactRepository;
        public MassMailContactRepository MassMailContact
        {
            get
            {
                if (_massMailContactRepository == null)
                    _massMailContactRepository = new MassMailContactRepository(_dataContext);

                return _massMailContactRepository;
            }
        }



        private ContactSessionsRepository _contactSessionsRepository;
        public ContactSessionsRepository ContactSessions
        {
            get
            {
                if (_contactSessionsRepository == null)
                    _contactSessionsRepository = new ContactSessionsRepository(_dataContext);

                return _contactSessionsRepository;
            }
        }



        private BrowsersRepository _browsersRepository;
        public BrowsersRepository Browsers
        {
            get
            {
                if (_browsersRepository == null)
                    _browsersRepository = new BrowsersRepository(_dataContext);

                return _browsersRepository;
            }
        }



        private OperatingSystemsRepository _operatingSystemsRepository;
        public OperatingSystemsRepository OperatingSystems
        {
            get
            {
                if (_operatingSystemsRepository == null)
                    _operatingSystemsRepository = new OperatingSystemsRepository(_dataContext);

                return _operatingSystemsRepository;
            }
        }



        private ResolutionsRepository _resolutionsRepository;
        public ResolutionsRepository Resolutions
        {
            get
            {
                if (_resolutionsRepository == null)
                    _resolutionsRepository = new ResolutionsRepository(_dataContext);

                return _resolutionsRepository;
            }
        }



        private MobileDevicesRepository _mobileDevicesRepository;
        public MobileDevicesRepository MobileDevices
        {
            get
            {
                if (_mobileDevicesRepository == null)
                    _mobileDevicesRepository = new MobileDevicesRepository(_dataContext);

                return _mobileDevicesRepository;
            }
        }



        private SiteActionLinkRepository _siteActionLinkRepository;
        public SiteActionLinkRepository SiteActionLink
        {
            get
            {
                if (_siteActionLinkRepository == null)
                    _siteActionLinkRepository = new SiteActionLinkRepository(_dataContext);

                return _siteActionLinkRepository;
            }
        }



        private SiteActivityRuleLayoutRepository _siteActivityRuleLayoutRepository;
        public SiteActivityRuleLayoutRepository SiteActivityRuleLayout
        {
            get
            {
                if (_siteActivityRuleLayoutRepository == null)
                    _siteActivityRuleLayoutRepository = new SiteActivityRuleLayoutRepository(_dataContext);

                return _siteActivityRuleLayoutRepository;
            }
        }



        private DictionaryRepository _dictionaryRepository;
        public DictionaryRepository Dictionary
        {
            get
            {
                if (_dictionaryRepository == null)
                    _dictionaryRepository = new DictionaryRepository(_dataContext);

                return _dictionaryRepository;
            }
        }



        private SiteActivityRuleExternalFormsRepository _siteActivityRuleExternalFormsRepository;
        public SiteActivityRuleExternalFormsRepository SiteActivityRuleExternalForms
        {
            get
            {
                if (_siteActivityRuleExternalFormsRepository == null)
                    _siteActivityRuleExternalFormsRepository = new SiteActivityRuleExternalFormsRepository(_dataContext);

                return _siteActivityRuleExternalFormsRepository;
            }
        }



        private SiteActivityRuleExternalFormFieldsRepository _siteActivityRuleExternalFormFieldsRepository;
        public SiteActivityRuleExternalFormFieldsRepository SiteActivityRuleExternalFormFields
        {
            get
            {
                if (_siteActivityRuleExternalFormFieldsRepository == null)
                    _siteActivityRuleExternalFormFieldsRepository = new SiteActivityRuleExternalFormFieldsRepository(_dataContext);

                return _siteActivityRuleExternalFormFieldsRepository;
            }
        }



        private EmailActionsRepository _emailActionsRepository;
        public EmailActionsRepository EmailActions
        {
            get
            {
                if (_emailActionsRepository == null)
                    _emailActionsRepository = new EmailActionsRepository(_dataContext);

                return _emailActionsRepository;
            }
        }



        private UserRepository _userRepository;
        public UserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext);

                return _userRepository;
            }
        }


        private ImportFieldRepository _importFieldRepository;
        public ImportFieldRepository ImportField
        {
            get
            {
                if (_importFieldRepository == null)
                    _importFieldRepository = new ImportFieldRepository(_dataContext);

                return _importFieldRepository;
            }
        }


        private ImportKeyRepository _importKeyRepository;
        public ImportKeyRepository ImportKey
        {
            get
            {
                if (_importKeyRepository == null)
                    _importKeyRepository = new ImportKeyRepository(_dataContext);

                return _importKeyRepository;
            }
        }



        private ImportFieldDictionaryRepository _importFieldDictionaryRepository;
        public ImportFieldDictionaryRepository ImportFieldDictionary
        {
            get
            {
                if (_importFieldDictionaryRepository == null)
                    _importFieldDictionaryRepository = new ImportFieldDictionaryRepository(_dataContext);

                return _importFieldDictionaryRepository;
            }
        }



        private ImportColumnRepository _importColumnRepository;
        public ImportColumnRepository ImportColumn
        {
            get
            {
                if (_importColumnRepository == null)
                    _importColumnRepository = new ImportColumnRepository(_dataContext);

                return _importColumnRepository;
            }
        }



        private ImportRepository _importRepository;
        public ImportRepository Import
        {
            get
            {
                if (_importRepository == null)
                    _importRepository = new ImportRepository(_dataContext);

                return _importRepository;
            }
        }



        private ImportTagRepository _importTagRepository;
        public ImportTagRepository ImportTag
        {
            get
            {
                if (_importTagRepository == null)
                    _importTagRepository = new ImportTagRepository(_dataContext);

                return _importTagRepository;
            }
        }



        private ImportColumnRuleRepository _importColumnRulesRepository;
        public ImportColumnRuleRepository ImportColumnRules
        {
            get
            {
                if (_importColumnRulesRepository == null)
                    _importColumnRulesRepository = new ImportColumnRuleRepository(_dataContext);

                return _importColumnRulesRepository;
            }
        }



        private CountryRepository _countryRepository;
        public CountryRepository Country
        {
            get
            {
                if (_countryRepository == null)
                    _countryRepository = new CountryRepository(_dataContext);

                return _countryRepository;
            }
        }



        private DistrictRepository _districtRepository;
        public DistrictRepository District
        {
            get
            {
                if (_districtRepository == null)
                    _districtRepository = new DistrictRepository(_dataContext);

                return _districtRepository;
            }
        }



        private RegionRepository _regionRepository;
        public RegionRepository Region
        {
            get
            {
                if (_regionRepository == null)
                    _regionRepository = new RegionRepository(_dataContext);

                return _regionRepository;
            }
        }



        private CityRepository _cityRepository;
        public CityRepository City
        {
            get
            {
                if (_cityRepository == null)
                    _cityRepository = new CityRepository(_dataContext);

                return _cityRepository;
            }
        }



        private AddressRepository _addressRepository;
        public AddressRepository Address
        {
            get
            {
                if (_addressRepository == null)
                    _addressRepository = new AddressRepository(_dataContext);

                return _addressRepository;
            }
        }



        private CompanyTypeRepository _companyTypeRepository;
        public CompanyTypeRepository CompanyType
        {
            get
            {
                if (_companyTypeRepository == null)
                    _companyTypeRepository = new CompanyTypeRepository(_dataContext);

                return _companyTypeRepository;
            }
        }



        private CompanySizeRepository _companySizeRepository;
        public CompanySizeRepository CompanySize
        {
            get
            {
                if (_companySizeRepository == null)
                    _companySizeRepository = new CompanySizeRepository(_dataContext);

                return _companySizeRepository;
            }
        }



        private CompanySectorRepository _companySectorRepository;
        public CompanySectorRepository CompanySector
        {
            get
            {
                if (_companySectorRepository == null)
                    _companySectorRepository = new CompanySectorRepository(_dataContext);

                return _companySectorRepository;
            }
        }



        private ContactJobLevelRepository _contactJobLevelRepository;
        public ContactJobLevelRepository ContactJobLevel
        {
            get
            {
                if (_contactJobLevelRepository == null)
                    _contactJobLevelRepository = new ContactJobLevelRepository(_dataContext);

                return _contactJobLevelRepository;
            }
        }



        private ContactTypeRepository _contactTypeRepository;
        public ContactTypeRepository ContactType
        {
            get
            {
                if (_contactTypeRepository == null)
                    _contactTypeRepository = new ContactTypeRepository(_dataContext);

                return _contactTypeRepository;
            }
        }



        private ContactFunctionInCompanyRepository _contactFunctionInCompanyRepository;
        public ContactFunctionInCompanyRepository ContactFunctionInCompany
        {
            get
            {
                if (_contactFunctionInCompanyRepository == null)
                    _contactFunctionInCompanyRepository = new ContactFunctionInCompanyRepository(_dataContext);

                return _contactFunctionInCompanyRepository;
            }
        }



        private ProductRepository _productRepository;
        public ProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_dataContext);

                return _productRepository;
            }
        }



        private ProductCategoryRepository _productCategoryRepository;
        public ProductCategoryRepository ProductCategory
        {
            get
            {
                if (_productCategoryRepository == null)
                    _productCategoryRepository = new ProductCategoryRepository(_dataContext);

                return _productCategoryRepository;
            }
        }



        private ProductTypeRepository _productTypeRepository;
        public ProductTypeRepository ProductType
        {
            get
            {
                if (_productTypeRepository == null)
                    _productTypeRepository = new ProductTypeRepository(_dataContext);

                return _productTypeRepository;
            }
        }



        private BrandRepository _brandRepository;
        public BrandRepository Brand
        {
            get
            {
                if (_brandRepository == null)
                    _brandRepository = new BrandRepository(_dataContext);

                return _brandRepository;
            }
        }



        private UnitRepository _unitRepository;
        public UnitRepository Unit
        {
            get
            {
                if (_unitRepository == null)
                    _unitRepository = new UnitRepository(_dataContext);

                return _unitRepository;
            }
        }



        private ProductPhotoRepository _productPhotoRepository;
        public ProductPhotoRepository ProductPhoto
        {
            get
            {
                if (_productPhotoRepository == null)
                    _productPhotoRepository = new ProductPhotoRepository(_dataContext);

                return _productPhotoRepository;
            }
        }



        private ProductComplectationRepository _productComplectationRepository;
        public ProductComplectationRepository ProductComplectation
        {
            get
            {
                if (_productComplectationRepository == null)
                    _productComplectationRepository = new ProductComplectationRepository(_dataContext);

                return _productComplectationRepository;
            }
        }


        private SourceMonitoringRepository _sourceMonitoringRepository;
        public SourceMonitoringRepository SourceMonitoring
        {
            get
            {
                if (_sourceMonitoringRepository == null)
                    _sourceMonitoringRepository = new SourceMonitoringRepository(_dataContext);

                return _sourceMonitoringRepository;
            }
        }


        private SourceMonitoringFilterRepository _sourceMonitoringFilterRepository;
        public SourceMonitoringFilterRepository SourceMonitoringFilter
        {
            get
            {
                if (_sourceMonitoringFilterRepository == null)
                    _sourceMonitoringFilterRepository = new SourceMonitoringFilterRepository(_dataContext);

                return _sourceMonitoringFilterRepository;
            }
        }



        private ProductPriceRepository _productPriceRepository;
        public ProductPriceRepository ProductPrice
        {
            get
            {
                if (_productPriceRepository == null)
                    _productPriceRepository = new ProductPriceRepository(_dataContext);

                return _productPriceRepository;
            }
        }


        private PriceListRepository _priceListRepository;
        public PriceListRepository PriceList
        {
            get
            {
                if (_priceListRepository == null)
                    _priceListRepository = new PriceListRepository(_dataContext);

                return _priceListRepository;
            }
        }


        private OrderRepository _orderRepository;
        public OrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_dataContext);

                return _orderRepository;
            }
        }


        private OrderProductsRepository _orderProductsRepository;
        public OrderProductsRepository OrderProducts
        {
            get
            {
                if (_orderProductsRepository == null)
                    _orderProductsRepository = new OrderProductsRepository(_dataContext);

                return _orderProductsRepository;
            }
        }



        private OrderTypeRepository _orderTypeRepository;
        public OrderTypeRepository OrderType
        {
            get
            {
                if (_orderTypeRepository == null)
                    _orderTypeRepository = new OrderTypeRepository(_dataContext);

                return _orderTypeRepository;
            }
        }


        private InvoiceRepository _invoiceRepository;
        public InvoiceRepository Invoice
        {
            get
            {
                if (_invoiceRepository == null)
                    _invoiceRepository = new InvoiceRepository(_dataContext);

                return _invoiceRepository;
            }
        }


        private InvoiceHistoryRepository _invoiceHistoryRepository;
        public InvoiceHistoryRepository InvoiceHistory
        {
            get
            {
                if (_invoiceHistoryRepository == null)
                    _invoiceHistoryRepository = new InvoiceHistoryRepository(_dataContext);

                return _invoiceHistoryRepository;
            }
        }


        private InvoiceTypeRepository _invoiceTypeRepository;
        public InvoiceTypeRepository InvoiceType
        {
            get
            {
                if (_invoiceTypeRepository == null)
                    _invoiceTypeRepository = new InvoiceTypeRepository(_dataContext);

                return _invoiceTypeRepository;
            }
        }


        private InvoiceProductsRepository _invoiceProductsRepository;
        public InvoiceProductsRepository InvoiceProducts
        {
            get
            {
                if (_invoiceProductsRepository == null)
                    _invoiceProductsRepository = new InvoiceProductsRepository(_dataContext);

                return _invoiceProductsRepository;
            }
        }


        private InvoiceCommentRepository _invoiceCommentRepository;
        public InvoiceCommentRepository InvoiceComment
        {
            get
            {
                if (_invoiceCommentRepository == null)
                    _invoiceCommentRepository = new InvoiceCommentRepository(_dataContext);

                return _invoiceCommentRepository;
            }
        }


        private ShipmentCommentRepository _shipmentCommentRepository;
        public ShipmentCommentRepository ShipmentComment
        {
            get
            {
                if (_shipmentCommentRepository == null)
                    _shipmentCommentRepository = new ShipmentCommentRepository(_dataContext);

                return _shipmentCommentRepository;
            }
        }


        private ShipmentTypeRepository _shipmentTypeRepository;
        public ShipmentTypeRepository ShipmentType
        {
            get
            {
                if (_shipmentTypeRepository == null)
                    _shipmentTypeRepository = new ShipmentTypeRepository(_dataContext);

                return _shipmentTypeRepository;
            }
        }


        private ShipmentRepository _shipmentRepository;
        public ShipmentRepository Shipment
        {
            get
            {
                if (_shipmentRepository == null)
                    _shipmentRepository = new ShipmentRepository(_dataContext);

                return _shipmentRepository;
            }
        }


        private ShipmentHistoryRepository _shipmentHistoryRepository;
        public ShipmentHistoryRepository ShipmentHistory
        {
            get
            {
                if (_shipmentHistoryRepository == null)
                    _shipmentHistoryRepository = new ShipmentHistoryRepository(_dataContext);

                return _shipmentHistoryRepository;
            }
        }



        private ShipmentProductsRepository _shipmentProductsRepository;
        public ShipmentProductsRepository ShipmentProducts
        {
            get
            {
                if (_shipmentProductsRepository == null)
                    _shipmentProductsRepository = new ShipmentProductsRepository(_dataContext);

                return _shipmentProductsRepository;
            }
        }

        private TaskRepository _taskRepository;
        public TaskRepository Task
        {
            get
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_dataContext);

                return _taskRepository;
            }
        }


        private TaskTypeRepository _taskTypeRepository;
        public TaskTypeRepository TaskType
        {
            get
            {
                if (_taskTypeRepository == null)
                    _taskTypeRepository = new TaskTypeRepository(_dataContext);

                return _taskTypeRepository;
            }
        }


        private TaskMemberRepository _taskMemberRepository;
        public TaskMemberRepository TaskMember
        {
            get
            {
                if (_taskMemberRepository == null)
                    _taskMemberRepository = new TaskMemberRepository(_dataContext);

                return _taskMemberRepository;
            }
        }


        private TaskDurationRepository _taskDurationRepository;
        public TaskDurationRepository TaskDuration
        {
            get
            {
                if (_taskDurationRepository == null)
                    _taskDurationRepository = new TaskDurationRepository(_dataContext);

                return _taskDurationRepository;
            }
        }


        private TaskHistoryRepository _taskHistoryRepository;
        public TaskHistoryRepository TaskHistory
        {
            get
            {
                if (_taskHistoryRepository == null)
                    _taskHistoryRepository = new TaskHistoryRepository(_dataContext);

                return _taskHistoryRepository;
            }
        }


        private TaskPersonalCommentRepository _taskTaskPersonalCommentRepository;
        public TaskPersonalCommentRepository TaskPersonalComment
        {
            get
            {
                if (_taskTaskPersonalCommentRepository == null)
                    _taskTaskPersonalCommentRepository = new TaskPersonalCommentRepository(_dataContext);

                return _taskTaskPersonalCommentRepository;
            }
        }


        private ReminderRepository _reminderRepository;
        public ReminderRepository Reminder
        {
            get
            {
                if (_reminderRepository == null)
                    _reminderRepository = new ReminderRepository(_dataContext);

                return _reminderRepository;
            }
        }

        
        private CurrencyRepository _currencyRepository;
        public CurrencyRepository Currency
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_dataContext);

                return _currencyRepository;
            }
        }


        private SiteActionAttachmentRepository _siteActionAttachmentRepository;
        public SiteActionAttachmentRepository SiteActionAttachment
        {
            get
            {
                if (_siteActionAttachmentRepository == null)
                    _siteActionAttachmentRepository = new SiteActionAttachmentRepository(_dataContext);

                return _siteActionAttachmentRepository;
            }
        }


        private EmailToAnalysisRepository _emailToAnalysisRepository;
        public EmailToAnalysisRepository EmailToAnalysis
        {
            get
            {
                if (_emailToAnalysisRepository == null)
                    _emailToAnalysisRepository = new EmailToAnalysisRepository(_dataContext);

                return _emailToAnalysisRepository;
            }
        }



        private ModuleRepository _moduleRepository;
        public ModuleRepository Module
        {
            get
            {
                if (_moduleRepository == null)
                    _moduleRepository = new ModuleRepository(_dataContext);

                return _moduleRepository;
            }
        }



        private AccessProfileRepository _accessProfileRepository;
        public AccessProfileRepository AccessProfile
        {
            get
            {
                if (_accessProfileRepository == null)
                    _accessProfileRepository = new AccessProfileRepository(_dataContext);

                return _accessProfileRepository;
            }
        }



        private AccessProfileModuleRepository _accessProfileModuleRepository;
        public AccessProfileModuleRepository AccessProfileModule
        {
            get
            {
                if (_accessProfileModuleRepository == null)
                    _accessProfileModuleRepository = new AccessProfileModuleRepository(_dataContext);

                return _accessProfileModuleRepository;
            }
        }



        private AccessProfileRecordRepository _accessProfileRecordRepository;
        public AccessProfileRecordRepository AccessProfileRecord
        {
            get
            {
                if (_accessProfileRecordRepository == null)
                    _accessProfileRecordRepository = new AccessProfileRecordRepository(_dataContext);

                return _accessProfileRecordRepository;
            }
        }


        private PriceListTypeRepository _priceListTypeRepository;
        public PriceListTypeRepository PriceListType
        {
            get
            {
                if (_priceListTypeRepository == null)
                    _priceListTypeRepository = new PriceListTypeRepository(_dataContext);

                return _priceListTypeRepository;
            }
        }



        private PriceListStatusRepository _priceListStatusRepository;
        public PriceListStatusRepository PriceListStatus
        {
            get
            {
                if (_priceListStatusRepository == null)
                    _priceListStatusRepository = new PriceListStatusRepository(_dataContext);

                return _priceListStatusRepository;
            }
        }



        private PublicationRepository _publicationRepository;
        public PublicationRepository Publication
        {
            get
            {
                if (_publicationRepository == null)
                    _publicationRepository = new PublicationRepository(_dataContext);

                return _publicationRepository;
            }
        }



        private PublicationCategoryRepository _publicationCategoryRepository;
        public PublicationCategoryRepository PublicationCategory
        {
            get
            {
                if (_publicationCategoryRepository == null)
                    _publicationCategoryRepository = new PublicationCategoryRepository(_dataContext);

                return _publicationCategoryRepository;
            }
        }



        private RelatedPublicationRepository _relatedPublicationRepository;
        public RelatedPublicationRepository RelatedPublication
        {
            get
            {
                if (_relatedPublicationRepository == null)
                    _relatedPublicationRepository = new RelatedPublicationRepository(_dataContext);

                return _relatedPublicationRepository;
            }
        }


        private PublicationTermsRepository _publicationTermsRepository;
        public PublicationTermsRepository PublicationTerms
        {
            get
            {
                if (_publicationTermsRepository == null)
                    _publicationTermsRepository = new PublicationTermsRepository(_dataContext);

                return _publicationTermsRepository;
            }
        }



        private MenuRepository _menuRepository;
        public MenuRepository Menu
        {
            get
            {
                if (_menuRepository == null)
                    _menuRepository = new MenuRepository(_dataContext);

                return _menuRepository;
            }
        }


        private PublicationCommentRepository _publicationCommentRepository;
        public PublicationCommentRepository PublicationComment
        {
            get
            {
                if (_publicationCommentRepository == null)
                    _publicationCommentRepository = new PublicationCommentRepository(_dataContext);

                return _publicationCommentRepository;
            }
        }


        private PublicationMarkRepository _publicationMarkRepository;
        public PublicationMarkRepository PublicationMark
        {
            get
            {
                if (_publicationMarkRepository == null)
                    _publicationMarkRepository = new PublicationMarkRepository(_dataContext);

                return _publicationMarkRepository;
            }
        }


        private PublicationTypeRepository _publicationTypeRepository;
        public PublicationTypeRepository PublicationType
        {
            get
            {
                if (_publicationTypeRepository == null)
                    _publicationTypeRepository = new PublicationTypeRepository(_dataContext);

                return _publicationTypeRepository;
            }
        }


        private PublicationStatusRepository _publicationStatusRepository;
        public PublicationStatusRepository PublicationStatus
        {
            get
            {
                if (_publicationStatusRepository == null)
                    _publicationStatusRepository = new PublicationStatusRepository(_dataContext);

                return _publicationStatusRepository;
            }
        }


        private PortalSettingsRepository _portalSettingsRepository;
        public PortalSettingsRepository PortalSettings
        {
            get
            {
                if (_portalSettingsRepository == null)
                    _portalSettingsRepository = new PortalSettingsRepository(_dataContext);

                return _portalSettingsRepository;
            }
        }


        private SiteDomainRepository _siteDomainRepository;
        public SiteDomainRepository SiteDomain
        {
            get
            {
                if (_siteDomainRepository == null)
                    _siteDomainRepository = new SiteDomainRepository(_dataContext);

                return _siteDomainRepository;
            }
        }


        private WebSiteRepository _webSiteRepository;
        public WebSiteRepository WebSite
        {
            get
            {
                if (_webSiteRepository == null)
                    _webSiteRepository = new WebSiteRepository(_dataContext);

                return _webSiteRepository;
            }
        }


        private WebSitePageRepository _webSitePageRepository;
        public WebSitePageRepository WebSitePage
        {
            get
            {
                if (_webSitePageRepository == null)
                    _webSitePageRepository = new WebSitePageRepository(_dataContext);

                return _webSitePageRepository;
            }
        }


        private ExternalResourceRepository _externalResourceRepository;
        public ExternalResourceRepository ExternalResource
        {
            get
            {
                if (_externalResourceRepository == null)
                    _externalResourceRepository = new ExternalResourceRepository(_dataContext);

                return _externalResourceRepository;
            }
        }

        private MaterialRepository _materialRepository;
        public MaterialRepository Material
        {
            get
            {
                if (_materialRepository == null)
                    _materialRepository = new MaterialRepository(_dataContext);

                return _materialRepository;
            }
        }


        private WorkflowTemplateConditionEventRepository _workflowTemplateConditionEventRepository;
        public WorkflowTemplateConditionEventRepository WorkflowTemplateConditionEvent
        {
            get
            {
                if (_workflowTemplateConditionEventRepository == null)
                    _workflowTemplateConditionEventRepository = new WorkflowTemplateConditionEventRepository(_dataContext);

                return _workflowTemplateConditionEventRepository;
            }
        }



        private WorkflowTemplateRepository _workflowTemplateRepository;
        public WorkflowTemplateRepository WorkflowTemplate
        {
            get
            {
                if (_workflowTemplateRepository == null)
                    _workflowTemplateRepository = new WorkflowTemplateRepository(_dataContext);

                return _workflowTemplateRepository;
            }
        }


        private WorkflowTemplateGoalRepository _workflowTemplateGoalRepository;
        public WorkflowTemplateGoalRepository WorkflowTemplateGoal
        {
            get
            {
                if (_workflowTemplateGoalRepository == null)
                    _workflowTemplateGoalRepository = new WorkflowTemplateGoalRepository(_dataContext);

                return _workflowTemplateGoalRepository;
            }
        }

        private WorkflowTemplateParameterRepository _workflowTemplateParameterRepository;
        public WorkflowTemplateParameterRepository WorkflowTemplateParameter
        {
            get
            {
                if (_workflowTemplateParameterRepository == null)
                    _workflowTemplateParameterRepository = new WorkflowTemplateParameterRepository(_dataContext);

                return _workflowTemplateParameterRepository;
            }
        }



        private WorkflowTemplateElementRepository _workflowTemplateElementRepository;
        public WorkflowTemplateElementRepository WorkflowTemplateElement
        {
            get
            {
                if (_workflowTemplateElementRepository == null)
                    _workflowTemplateElementRepository = new WorkflowTemplateElementRepository(_dataContext);

                return _workflowTemplateElementRepository;
            }
        }



        private WorkflowTemplateElementRelationRepository _workflowTemplateElementRelationRepository;
        public WorkflowTemplateElementRelationRepository WorkflowTemplateElementRelation
        {
            get
            {
                if (_workflowTemplateElementRelationRepository == null)
                    _workflowTemplateElementRelationRepository = new WorkflowTemplateElementRelationRepository(_dataContext);

                return _workflowTemplateElementRelationRepository;
            }
        }



        private WorkflowRepository _workflowRepository;
        public WorkflowRepository Workflow
        {
            get
            {
                if (_workflowRepository == null)
                    _workflowRepository = new WorkflowRepository(_dataContext);

                return _workflowRepository;
            }
        }


        private WorkflowElementRepository _workflowElementRepository;
        public WorkflowElementRepository WorkflowElement
        {
            get
            {
                if (_workflowElementRepository == null)
                    _workflowElementRepository = new WorkflowElementRepository(_dataContext);

                return _workflowElementRepository;
            }
        }



        private WorkflowTemplateElementParameterRepository _workflowTemplateElementParameterRepository;
        public WorkflowTemplateElementParameterRepository WorkflowTemplateElementParameter
        {
            get
            {
                if (_workflowTemplateElementParameterRepository == null)
                    _workflowTemplateElementParameterRepository = new WorkflowTemplateElementParameterRepository(_dataContext);

                return _workflowTemplateElementParameterRepository;
            }
        }



        private WorkflowTemplateElementResultRepository _workflowTemplateElementResultRepository;
        public WorkflowTemplateElementResultRepository WorkflowTemplateElementResult
        {
            get
            {
                if (_workflowTemplateElementResultRepository == null)
                    _workflowTemplateElementResultRepository = new WorkflowTemplateElementResultRepository(_dataContext);

                return _workflowTemplateElementResultRepository;
            }
        }



        private WorkflowTemplateElementTagRepository _workflowTemplateElementTagRepository;
        public WorkflowTemplateElementTagRepository WorkflowTemplateElementTag
        {
            get
            {
                if (_workflowTemplateElementTagRepository == null)
                    _workflowTemplateElementTagRepository = new WorkflowTemplateElementTagRepository(_dataContext);

                return _workflowTemplateElementTagRepository;
            }
        }



        private WorkflowTemplateElementPeriodRepository _workflowTemplateElementPeriodRepository;
        public WorkflowTemplateElementPeriodRepository WorkflowTemplateElementPeriod
        {
            get
            {
                if (_workflowTemplateElementPeriodRepository == null)
                    _workflowTemplateElementPeriodRepository = new WorkflowTemplateElementPeriodRepository(_dataContext);

                return _workflowTemplateElementPeriodRepository;
            }
        }



        private WorkflowTemplateElementExternalRequestRepository _workflowTemplateElementExternalRequestRepository;
        public WorkflowTemplateElementExternalRequestRepository WorkflowTemplateElementExternalRequest
        {
            get
            {
                if (_workflowTemplateElementExternalRequestRepository == null)
                    _workflowTemplateElementExternalRequestRepository = new WorkflowTemplateElementExternalRequestRepository(_dataContext);

                return _workflowTemplateElementExternalRequestRepository;
            }
        }



        private MassWorkflowRepository _massWorkflowRepository;
        public MassWorkflowRepository MassWorkflow
        {
            get
            {
                if (_massWorkflowRepository == null)
                    _massWorkflowRepository = new MassWorkflowRepository(_dataContext);

                return _massWorkflowRepository;
            }
        }



        private MassWorkflowContactRepository _massWorkflowContactRepository;
        public MassWorkflowContactRepository MassWorkflowContact
        {
            get
            {
                if (_massWorkflowContactRepository == null)
                    _massWorkflowContactRepository = new MassWorkflowContactRepository(_dataContext);

                return _massWorkflowContactRepository;
            }
        }



        private ContactCommunicationRepository _contactCommunicationRepository;
        public ContactCommunicationRepository ContactCommunication
        {
            get
            {
                if (_contactCommunicationRepository == null)
                    _contactCommunicationRepository = new ContactCommunicationRepository(_dataContext);

                return _contactCommunicationRepository;
            }
        }


        private SiteTagsRepository _siteTagsRepository;
        public SiteTagsRepository SiteTags
        {
            get
            {
                if (_siteTagsRepository == null)
                    _siteTagsRepository = new SiteTagsRepository(_dataContext);

                return _siteTagsRepository;
            }
        }


        private SocialAuthorizationTokenRepository _socialAuthorizationTokenRepository;
        public SocialAuthorizationTokenRepository SocialAuthorizationToken
        {
            get
            {
                if (_socialAuthorizationTokenRepository == null)
                    _socialAuthorizationTokenRepository = new SocialAuthorizationTokenRepository(_dataContext);

                return _socialAuthorizationTokenRepository;
            }
        }


        private CompanyLegalAccountRepository _companyLegalAccountRepository;
        public CompanyLegalAccountRepository CompanyLegalAccount
        {
            get
            {
                if (_companyLegalAccountRepository == null)
                    _companyLegalAccountRepository = new CompanyLegalAccountRepository(_dataContext);

                return _companyLegalAccountRepository;
            }
        }


        private BankRepository _bankRepository;
        public BankRepository Bank
        {
            get
            {
                if (_bankRepository == null)
                    _bankRepository = new BankRepository(_dataContext);

                return _bankRepository;
            }
        }


        private RequestRepository _requestRepository;
        public RequestRepository Request
        {
            get
            {
                if (_requestRepository == null)
                    _requestRepository = new RequestRepository(_dataContext);

                return _requestRepository;
            }
        }

        private RequestHistoryRepository _requestHistoryRepository;
        public RequestHistoryRepository RequestHistory
        {
            get
            {
                if (_requestHistoryRepository == null)
                    _requestHistoryRepository = new RequestHistoryRepository(_dataContext);

                return _requestHistoryRepository;
            }
        }


        private RequestFileRepository _requestFileRepository;
        public RequestFileRepository RequestFile
        {
            get
            {
                if (_requestFileRepository == null)
                    _requestFileRepository = new RequestFileRepository(_dataContext);

                return _requestFileRepository;
            }
        }


        private ServiceLevelRepository _serviceLevelRepository;
        public ServiceLevelRepository ServiceLevel
        {
            get
            {
                if (_serviceLevelRepository == null)
                    _serviceLevelRepository = new ServiceLevelRepository(_dataContext);

                return _serviceLevelRepository;
            }
        }


        private ServiceLevelClientRepository _serviceLevelClientRepository;
        public ServiceLevelClientRepository ServiceLevelClient
        {
            get
            {
                if (_serviceLevelClientRepository == null)
                    _serviceLevelClientRepository = new ServiceLevelClientRepository(_dataContext);

                return _serviceLevelClientRepository;
            }
        }


        private ServiceLevelContactRepository _serviceLevelContactRepository;
        public ServiceLevelContactRepository ServiceLevelContact
        {
            get
            {
                if (_serviceLevelContactRepository == null)
                    _serviceLevelContactRepository = new ServiceLevelContactRepository(_dataContext);

                return _serviceLevelContactRepository;
            }
        }


        private WidgetCategoryRepository _widgetCategoryRepository;
        public WidgetCategoryRepository WidgetCategory
        {
            get
            {
                if (_widgetCategoryRepository == null)
                    _widgetCategoryRepository = new WidgetCategoryRepository(_dataContext);

                return _widgetCategoryRepository;
            }
        }


        private WidgetRepository _widgetRepository;
        public WidgetRepository Widget
        {
            get
            {
                if (_widgetRepository == null)
                    _widgetRepository = new WidgetRepository(_dataContext);

                return _widgetRepository;
            }
        }


        private WidgetToAccessProfileRepository _widgetToAccessProfileRepository;
        public WidgetToAccessProfileRepository WidgetToAccessProfile
        {
            get
            {
                if (_widgetToAccessProfileRepository == null)
                    _widgetToAccessProfileRepository = new WidgetToAccessProfileRepository(_dataContext);

                return _widgetToAccessProfileRepository;
            }
        }

        private StatisticDataRepository _statisticDataRepository;
        public StatisticDataRepository StatisticData
        {
            get {
                return _statisticDataRepository ??
                       (_statisticDataRepository = new StatisticDataRepository(_dataContext));
            }
        }

        private RequestSourceTypeRepository _requestSourceTypeRepository;
        public RequestSourceTypeRepository RequestSourceType
        {
            get
            {
                if (_requestSourceTypeRepository == null)
                    _requestSourceTypeRepository = new RequestSourceTypeRepository(_dataContext);

                return _requestSourceTypeRepository;
            }
        }


        private RequirementRepository _requirementRepository;
        public RequirementRepository Requirement
        {
            get
            {
                if (_requirementRepository == null)
                    _requirementRepository = new RequirementRepository(_dataContext);

                return _requirementRepository;
            }
        }


        private RequirementCommentRepository _requirementCommentRepository;
        public RequirementCommentRepository RequirementComment
        {
            get
            {
                if (_requirementCommentRepository == null)
                    _requirementCommentRepository = new RequirementCommentRepository(_dataContext);

                return _requirementCommentRepository;
            }
        }


        private RequestCommentRepository _requestCommentRepository;
        public RequestCommentRepository RequestComment
        {
            get
            {
                if (_requestCommentRepository == null)
                    _requestCommentRepository = new RequestCommentRepository(_dataContext);

                return _requestCommentRepository;
            }
        }


        private RequirementSeverityOfExposureRepository _requirementSeverityOfExposureRepository;
        public RequirementSeverityOfExposureRepository RequirementSeverityOfExposure
        {
            get
            {
                if (_requirementSeverityOfExposureRepository == null)
                    _requirementSeverityOfExposureRepository = new RequirementSeverityOfExposureRepository(_dataContext);

                return _requirementSeverityOfExposureRepository;
            }
        }


        private RequirementTypeRepository _requirementTypeRepository;
        public RequirementTypeRepository RequirementType
        {
            get
            {
                if (_requirementTypeRepository == null)
                    _requirementTypeRepository = new RequirementTypeRepository(_dataContext);

                return _requirementTypeRepository;
            }
        }


        private RequirementHistoryRepository _requirementHistoryRepository;
        public RequirementHistoryRepository RequirementHistory
        {
            get
            {
                if (_requirementHistoryRepository == null)
                    _requirementHistoryRepository = new RequirementHistoryRepository(_dataContext);

                return _requirementHistoryRepository;
            }
        }


        private RequirementTransitionRepository _requirementTransitionRepository;
        public RequirementTransitionRepository RequirementTransition
        {
            get
            {
                if (_requirementTransitionRepository == null)
                    _requirementTransitionRepository = new RequirementTransitionRepository(_dataContext);

                return _requirementTransitionRepository;
            }
        }


        private RequirementStatusRepository _requirementStatusRepository;
        public RequirementStatusRepository RequirementStatus
        {
            get
            {
                if (_requirementStatusRepository == null)
                    _requirementStatusRepository = new RequirementStatusRepository(_dataContext);

                return _requirementStatusRepository;
            }
        }        

        private AnalyticReportRepository _analyticReportRepository;
        public AnalyticReportRepository AnalyticReport
        {
            get
            {
                if (_analyticReportRepository == null)
                    _analyticReportRepository = new AnalyticReportRepository(_dataContext);

                return _analyticReportRepository;
            }
        }

        private AnalyticReportUserSettingsRepository _analyticReportUserSettingsRepository;
        public AnalyticReportUserSettingsRepository AnalyticReportUserSettings
        {
            get
            {
                if (_analyticReportUserSettingsRepository == null)
                    _analyticReportUserSettingsRepository = new AnalyticReportUserSettingsRepository(_dataContext);

                return _analyticReportUserSettingsRepository;
            }
        }        

        private AnalyticReportSystemRepository _analyticReportSystemRepository;
        public AnalyticReportSystemRepository AnalyticReportSystem
        {
            get
            {
                if (_analyticReportSystemRepository == null)
                    _analyticReportSystemRepository = new AnalyticReportSystemRepository(_dataContext);

                return _analyticReportSystemRepository;
            }
        }

        private AnalyticAxisRepository _analyticAxisRepository;
        public AnalyticAxisRepository AnalyticAxis
        {
            get
            {
                if (_analyticAxisRepository == null)
                    _analyticAxisRepository = new AnalyticAxisRepository(_dataContext);

                return _analyticAxisRepository;
            }
        }


        private AnalyticAxisFilterValuesRepository _analyticAxisFilterValuesRepository;
        public AnalyticAxisFilterValuesRepository AnalyticAxisFilterValues
        {
            get
            {
                if (_analyticAxisFilterValuesRepository == null)
                    _analyticAxisFilterValuesRepository = new AnalyticAxisFilterValuesRepository(_dataContext);

                return _analyticAxisFilterValuesRepository;
            }
        }


        private CronJobRepository _cronJobRepository;
        public CronJobRepository CronJob
        {
            get
            {
                if (_cronJobRepository == null)
                    _cronJobRepository = new CronJobRepository(_dataContext);

                return _cronJobRepository;
            }
        }


        private AdvertisingCampaignRepository _advertisingCampaignRepository;
        public AdvertisingCampaignRepository AdvertisingCampaign
        {
            get
            {
                if (_advertisingCampaignRepository == null)
                    _advertisingCampaignRepository = new AdvertisingCampaignRepository(_dataContext);

                return _advertisingCampaignRepository;
            }        
        }


        private AdvertisingTypeRepository _advertisingTypeRepository;
        public AdvertisingTypeRepository AdvertisingType
        {
            get
            {
                if (_advertisingTypeRepository == null)
                    _advertisingTypeRepository = new AdvertisingTypeRepository(_dataContext);

                return _advertisingTypeRepository;
            }
        }

        private AdvertisingPlatformRepository _advertisingPlatformRepository;
        public AdvertisingPlatformRepository AdvertisingPlatform
        {
            get
            {
                if (_advertisingPlatformRepository == null)
                    _advertisingPlatformRepository = new AdvertisingPlatformRepository(_dataContext);

                return _advertisingPlatformRepository;
            }
        }

        private ModuleEditionRepository _moduleEditionRepository;
        public ModuleEditionRepository ModuleEdition
        {
            get
            {
                if (_moduleEditionRepository == null)
                    _moduleEditionRepository = new ModuleEditionRepository(_dataContext);

                return _moduleEditionRepository;
            }
        }

        private ModuleEditionOptionRepository _moduleEditionOptionRepository;
        public ModuleEditionOptionRepository ModuleEditionOption
        {
            get
            {
                if (_moduleEditionOptionRepository == null)
                    _moduleEditionOptionRepository = new ModuleEditionOptionRepository(_dataContext);

                return _moduleEditionOptionRepository;
            }
        }

        private ModuleEditionActionRepository _moduleEditionActionRepository;
        public ModuleEditionActionRepository ModuleEditionAction
        {
            get
            {
                if (_moduleEditionActionRepository == null)
                    _moduleEditionActionRepository = new ModuleEditionActionRepository(_dataContext);

                return _moduleEditionActionRepository;
            }
        }


        private TermRepository _termRepository;
        public TermRepository Term
        {
            get
            {
                if (_termRepository == null)
                    _termRepository = new TermRepository(_dataContext);

                return _termRepository;
            }
        }


        private EmailStatsRepository _emailStatsRepository;
        public EmailStatsRepository EmailStats
        {
            get
            {
                if (_emailStatsRepository == null)
                    _emailStatsRepository = new EmailStatsRepository(_dataContext);

                return _emailStatsRepository;
            }
        }

        private PaymentRepository _paymentRepository;
        public PaymentRepository Payment
        {
            get
            {
                if (_paymentRepository == null)
                    _paymentRepository = new PaymentRepository(_dataContext);

                return _paymentRepository;
            }
        }
        private PaymentTransitionRepository _paymentTransitionRepository;
        public PaymentTransitionRepository PaymentTransition
        {
            get
            {
                if (_paymentTransitionRepository == null)
                    _paymentTransitionRepository = new PaymentTransitionRepository(_dataContext);

                return _paymentTransitionRepository;
            }
        }


        private PaymentStatusRepository _paymentStatusRepository;
        public PaymentStatusRepository PaymentStatus
        {
            get
            {
                if (_paymentStatusRepository == null)
                    _paymentStatusRepository = new PaymentStatusRepository(_dataContext);

                return _paymentStatusRepository;
            }
        }

        private PaymentCFORepository _paymentCFORepository;
        public PaymentCFORepository PaymentCFO
        {
            get
            {
                if (_paymentCFORepository == null)
                    _paymentCFORepository = new PaymentCFORepository(_dataContext);

                return _paymentCFORepository;
            }
        }

        private PaymentArticleRepository _paymentArticleRepository;
        public PaymentArticleRepository PaymentArticle
        {
            get
            {
                if (_paymentArticleRepository == null)
                    _paymentArticleRepository = new PaymentArticleRepository(_dataContext);

                return _paymentArticleRepository;
            }
        }


        private PaymentPassCategoryRepository _paymentPassCategoryRepository;
        public PaymentPassCategoryRepository PaymentPassCategory
        {
            get
            {
                if (_paymentPassCategoryRepository == null)
                    _paymentPassCategoryRepository = new PaymentPassCategoryRepository(_dataContext);

                return _paymentPassCategoryRepository;
            }
        }

        private PaymentPassRuleRepository _paymentPassRuleRepository;
        public PaymentPassRuleRepository PaymentPassRule
        {
            get
            {
                if (_paymentPassRuleRepository == null)
                    _paymentPassRuleRepository = new PaymentPassRuleRepository(_dataContext);

                return _paymentPassRuleRepository;
            }
        }

        private PaymentPassRulePassRepository _paymentPassRulePassRepository;
        public PaymentPassRulePassRepository PaymentPassRulePass
        {
            get
            {
                if (_paymentPassRulePassRepository == null)
                    _paymentPassRulePassRepository = new PaymentPassRulePassRepository(_dataContext);

                return _paymentPassRulePassRepository;
            }
        }


        private SiteActionTagValueRepository _siteActionTagValueRepository;
        public SiteActionTagValueRepository SiteActionTagValue
        {
            get
            {
                if (_siteActionTagValueRepository == null)
                    _siteActionTagValueRepository = new SiteActionTagValueRepository(_dataContext);

                return _siteActionTagValueRepository;
            }
        }

        private PaymentPassRuleCompanyRepository _paymentPassRuleCompanyRepository;
        public PaymentPassRuleCompanyRepository PaymentPassRuleCompany
        {
            get
            {
                if (_paymentPassRuleCompanyRepository == null)
                    _paymentPassRuleCompanyRepository = new PaymentPassRuleCompanyRepository(_dataContext);

                return _paymentPassRuleCompanyRepository;
            }
        }

        private PaymentPassRepository _paymentPassRepository;
        public PaymentPassRepository PaymentPass
        {
            get
            {
                if (_paymentPassRepository == null)
                    _paymentPassRepository = new PaymentPassRepository(_dataContext);

                return _paymentPassRepository;
            }
        }


        private PaymentBalanceRepository _paymentBalanceRepository;
        public PaymentBalanceRepository PaymentBalance
        {
            get
            {
                if (_paymentBalanceRepository == null)
                    _paymentBalanceRepository = new PaymentBalanceRepository(_dataContext);

                return _paymentBalanceRepository;
            }
        }


        private ResponsibleRepository _responsibleRepository;
        public ResponsibleRepository Responsible
        {
            get
            {
                if (_responsibleRepository == null)
                    _responsibleRepository = new ResponsibleRepository(_dataContext);

                return _responsibleRepository;
            }
        }
    }
}
