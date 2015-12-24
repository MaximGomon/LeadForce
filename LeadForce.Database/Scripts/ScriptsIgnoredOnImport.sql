
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/28/2015 20:56:13
-- Generated from EDMX file: C:\Users\Andrey\Desktop\LeadForce\WebCounter.DataAccessLayer\WebCounterModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

USE [LeadForce-demo];
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfile_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfile] DROP CONSTRAINT [FK_tbl_AccessProfile_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfile_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfile] DROP CONSTRAINT [FK_tbl_AccessProfile_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileModule_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileModule] DROP CONSTRAINT [FK_tbl_AccessProfileModule_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileModule_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileModule] DROP CONSTRAINT [FK_tbl_AccessProfileModule_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileModule_tbl_ModuleEdition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileModule] DROP CONSTRAINT [FK_tbl_AccessProfileModule_tbl_ModuleEdition];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileModuleEditionOption_tbl_AccessProfileModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption] DROP CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_AccessProfileModule];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption] DROP CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileRecord_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileRecord] DROP CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AccessProfileRecord_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AccessProfileRecord] DROP CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AdvertisingCampaign_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AdvertisingCampaign] DROP CONSTRAINT [FK_tbl_AdvertisingCampaign_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AdvertisingPlatform_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AdvertisingPlatform] DROP CONSTRAINT [FK_tbl_AdvertisingPlatform_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AdvertisingType] DROP CONSTRAINT [FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AdvertisingType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AdvertisingType] DROP CONSTRAINT [FK_tbl_AdvertisingType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticAxis_tbl_Analytic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticAxis] DROP CONSTRAINT [FK_tbl_AnalyticAxis_tbl_Analytic];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis]', 'F') IS NOT NULL
    ALTER TABLE [WebCounterModelStoreContainer].[tbl_AnalyticAxisFilterValues] DROP CONSTRAINT [FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReport_tbl_Analytic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReport] DROP CONSTRAINT [FK_tbl_AnalyticReport_tbl_Analytic];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReport_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReport] DROP CONSTRAINT [FK_tbl_AnalyticReport_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReportSystem] DROP CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReportSystem_tbl_AnalyticReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReportSystem] DROP CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticReport];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings] DROP CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_AnalyticReportUserSetting_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings] DROP CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Bank_tbl_City]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Bank] DROP CONSTRAINT [FK_tbl_Bank_tbl_City];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Brand_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Brand] DROP CONSTRAINT [FK_tbl_Brand_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Browsers_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Browsers] DROP CONSTRAINT [FK_tbl_Browsers_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_City_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_City] DROP CONSTRAINT [FK_tbl_City_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_City_tbl_District]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_City] DROP CONSTRAINT [FK_tbl_City_tbl_District];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_City_tbl_Region]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_City] DROP CONSTRAINT [FK_tbl_City_tbl_Region];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[FK_tbl_CityIP_tbl_City]', 'F') IS NOT NULL
    ALTER TABLE [WebCounterModelStoreContainer].[tbl_CityIP] DROP CONSTRAINT [FK_tbl_CityIP_tbl_City];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ColumnCategories_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ColumnCategories] DROP CONSTRAINT [FK_tbl_ColumnCategories_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ColumnTypesExpression_tbl_ColumnTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ColumnTypesExpression] DROP CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ColumnTypesExpression_tbl_ColumnTypesExpression]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ColumnTypesExpression] DROP CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypesExpression];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_LocationAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_LocationAddress];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_PostalAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_PostalAddress];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_CompanySector]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_CompanySector];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_CompanySize]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_CompanySize];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_CompanyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_CompanyType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_Priorities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_Priorities];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_ReadyToSell]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_ReadyToSell];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Company_tbl_Status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Company] DROP CONSTRAINT [FK_tbl_Company_tbl_Status];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyLegalAccount_tbl_Bank]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyLegalAccount] DROP CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Bank];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyLegalAccount_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyLegalAccount] DROP CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyLegalAccount] DROP CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyLegalAccount_tbl_Contact_Head]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyLegalAccount] DROP CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Head];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyLegalAccount_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyLegalAccount] DROP CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanySector_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanySector] DROP CONSTRAINT [FK_tbl_CompanySector_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanySize_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanySize] DROP CONSTRAINT [FK_tbl_CompanySize_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CompanyType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CompanyType] DROP CONSTRAINT [FK_tbl_CompanyType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Address]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Address];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_AdvertisingCampaign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingCampaign];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_AdvertisingPlatform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingPlatform];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_AdvertisingType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_ContactFunctionInCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_ContactFunctionInCompany];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_ContactJobLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_ContactJobLevel];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_ContactType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_ContactType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Priorities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Priorities];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_ReadyToSell]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_ReadyToSell];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contact_tbl_Status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contact] DROP CONSTRAINT [FK_tbl_Contact_tbl_Status];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivity_tbl_ActivityTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivity] DROP CONSTRAINT [FK_tbl_ContactActivity_tbl_ActivityTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivity_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivity] DROP CONSTRAINT [FK_tbl_ContactActivity_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivity_tbl_ContactSessions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivity] DROP CONSTRAINT [FK_tbl_ContactActivity_tbl_ContactSessions];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivity_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivity] DROP CONSTRAINT [FK_tbl_ContactActivity_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivity_tbl_SourceMonitoring]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivity] DROP CONSTRAINT [FK_tbl_ContactActivity_tbl_SourceMonitoring];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivityScore] DROP CONSTRAINT [FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivityScore_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivityScore] DROP CONSTRAINT [FK_tbl_ContactActivityScore_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivityScoreHistory] DROP CONSTRAINT [FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactColumnValues_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactColumnValues] DROP CONSTRAINT [FK_tbl_ContactColumnValues_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactColumnValues_tbl_SiteColumns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactColumnValues] DROP CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactColumnValues_tbl_SiteColumnValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactColumnValues] DROP CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumnValues];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactCommunication_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactCommunication] DROP CONSTRAINT [FK_tbl_ContactCommunication_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactFunctionInCompany_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactFunctionInCompany] DROP CONSTRAINT [FK_tbl_ContactFunctionInCompany_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactJobLevel_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactJobLevel] DROP CONSTRAINT [FK_tbl_ContactJobLevel_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactRole_tbl_ContactRoleType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactRole] DROP CONSTRAINT [FK_tbl_ContactRole_tbl_ContactRoleType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactRole_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactRole] DROP CONSTRAINT [FK_tbl_ContactRole_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactScore_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactActivityScore] DROP CONSTRAINT [FK_tbl_ContactScore_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_AdvertisingCampaign]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingCampaign];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_AdvertisingPlatform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingPlatform];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_AdvertisingType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_Browsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_Browsers];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_City]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_City];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_MobileDevices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_MobileDevices];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_OperatingSystems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_OperatingSystems];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_Resolutions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_Resolutions];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactSessions_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactSessions] DROP CONSTRAINT [FK_tbl_ContactSessions_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactToContactRole_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactToContactRole] DROP CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactToContactRole_tbl_ContactRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactToContactRole] DROP CONSTRAINT [FK_tbl_ContactToContactRole_tbl_ContactRole];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactToContactRole_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactToContactRole] DROP CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ContactType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ContactType] DROP CONSTRAINT [FK_tbl_ContactType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contract_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contract] DROP CONSTRAINT [FK_tbl_Contract_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Contract_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Contract] DROP CONSTRAINT [FK_tbl_Contract_tbl_Sites];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[FK_tbl_CountryIP_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [WebCounterModelStoreContainer].[tbl_CountryIP] DROP CONSTRAINT [FK_tbl_CountryIP_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Currency_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Currency] DROP CONSTRAINT [FK_tbl_Currency_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_CurrencyCourse_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_CurrencyCourse] DROP CONSTRAINT [FK_tbl_CurrencyCourse_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Dictionary_tbl_DictionaryGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Dictionary] DROP CONSTRAINT [FK_tbl_Dictionary_tbl_DictionaryGroup];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_DictionaryGroup_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_DictionaryGroup] DROP CONSTRAINT [FK_tbl_DictionaryGroup_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_District_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_District] DROP CONSTRAINT [FK_tbl_District_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_District_tbl_Region]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_District] DROP CONSTRAINT [FK_tbl_District_tbl_Region];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_EmailStatsUnsubscribe_tbl_EmailStats]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe] DROP CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_EmailStats];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_EmailStatsUnsubscribe_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe] DROP CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_EmailToAnalysis_tbl_SourceMonitoring]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_EmailToAnalysis] DROP CONSTRAINT [FK_tbl_EmailToAnalysis_tbl_SourceMonitoring];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Import_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Import] DROP CONSTRAINT [FK_tbl_Import_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ImportTag_tbl_Import]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ImportTag] DROP CONSTRAINT [FK_tbl_ImportTag_tbl_Import];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Company_Buyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Company_Buyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Company_Executor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Company_Executor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Contact_Buyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Buyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Contact_Executor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Executor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_InvoiceStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_InvoiceStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Invoice_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Invoice] DROP CONSTRAINT [FK_tbl_Invoice_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceComment_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceComment] DROP CONSTRAINT [FK_tbl_InvoiceComment_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceComment_tbl_InvoiceComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceComment] DROP CONSTRAINT [FK_tbl_InvoiceComment_tbl_InvoiceComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceComment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceComment] DROP CONSTRAINT [FK_tbl_InvoiceComment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceComment_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceComment] DROP CONSTRAINT [FK_tbl_InvoiceComment_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceComment_tbl_User_Destination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceComment] DROP CONSTRAINT [FK_tbl_InvoiceComment_tbl_User_Destination];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceCommentMark_tbl_InvoiceComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceCommentMark] DROP CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_InvoiceComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceCommentMark_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceCommentMark] DROP CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceHistory_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceHistory] DROP CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceHistory_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceHistory] DROP CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceProducts_tbl_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceProducts] DROP CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Unit];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceToShipment_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceToShipment] DROP CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceToShipment_tbl_Shipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceToShipment] DROP CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Shipment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceType_tbl_Direction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceType] DROP CONSTRAINT [FK_tbl_InvoiceType_tbl_Direction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceType] DROP CONSTRAINT [FK_tbl_InvoiceType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_InvoiceType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_InvoiceType] DROP CONSTRAINT [FK_tbl_InvoiceType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Links_tbl_RuleTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Links] DROP CONSTRAINT [FK_tbl_Links_tbl_RuleTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Links_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Links] DROP CONSTRAINT [FK_tbl_Links_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMail_tbl_SiteActionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMail] DROP CONSTRAINT [FK_tbl_MassMail_tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMail_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMail] DROP CONSTRAINT [FK_tbl_MassMail_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMailContact_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMailContact] DROP CONSTRAINT [FK_tbl_MassMailContact_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMailContact_tbl_MassMail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMailContact] DROP CONSTRAINT [FK_tbl_MassMailContact_tbl_MassMail];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMailContact_tbl_SiteAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMailContact] DROP CONSTRAINT [FK_tbl_MassMailContact_tbl_SiteAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassMailContact_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassMailContact] DROP CONSTRAINT [FK_tbl_MassMailContact_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassWorkflow_tbl_MassWorkflowType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassWorkflow] DROP CONSTRAINT [FK_tbl_MassWorkflow_tbl_MassWorkflowType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassWorkflow_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassWorkflow] DROP CONSTRAINT [FK_tbl_MassWorkflow_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MassWorkflowContact_tbl_MassWorkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MassWorkflowContact] DROP CONSTRAINT [FK_tbl_MassWorkflowContact_tbl_MassWorkflow];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Material_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Material] DROP CONSTRAINT [FK_tbl_Material_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Menu_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Menu] DROP CONSTRAINT [FK_tbl_Menu_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Menu_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Menu] DROP CONSTRAINT [FK_tbl_Menu_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Menu_tbl_ModuleEditionAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Menu] DROP CONSTRAINT [FK_tbl_Menu_tbl_ModuleEditionAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_MobileDevices_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_MobileDevices] DROP CONSTRAINT [FK_tbl_MobileDevices_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ModuleEdition_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ModuleEdition] DROP CONSTRAINT [FK_tbl_ModuleEdition_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ModuleEditionAction_tbl_ModuleEdition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ModuleEditionAction] DROP CONSTRAINT [FK_tbl_ModuleEditionAction_tbl_ModuleEdition];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ModuleEditionOption_tbl_ModuleEdition]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ModuleEditionOption] DROP CONSTRAINT [FK_tbl_ModuleEditionOption_tbl_ModuleEdition];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Numerator_tbl_NumeratorPeriod]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Numerator] DROP CONSTRAINT [FK_tbl_Numerator_tbl_NumeratorPeriod];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Numerator_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Numerator] DROP CONSTRAINT [FK_tbl_Numerator_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_NumeratorUsage_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_NumeratorUsage] DROP CONSTRAINT [FK_tbl_NumeratorUsage_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OperatingSystems_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OperatingSystems] DROP CONSTRAINT [FK_tbl_OperatingSystems_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_CompanyBuyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_CompanyBuyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_CompanyExecutor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_CompanyExecutor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_ContactBuyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_ContactBuyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_ContactExecutor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_ContactExecutor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_OrderStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_OrderStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_OrderType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_OrderType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Order_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Order] DROP CONSTRAINT [FK_tbl_Order_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_OrderProductsParent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_OrderProductsParent];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_PriceListSpecialOffer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceListSpecialOffer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderProducts_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderProducts] DROP CONSTRAINT [FK_tbl_OrderProducts_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderType_tbl_ExpirationAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderType] DROP CONSTRAINT [FK_tbl_OrderType_tbl_ExpirationAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_OrderType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_OrderType] DROP CONSTRAINT [FK_tbl_OrderType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Company1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Company1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_CompanyLegalAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_CompanyLegalAccount1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_PaymentPassRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_PaymentPassRule];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_PaymentStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_PaymentStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_PaymentType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_PaymentType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Payment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Payment] DROP CONSTRAINT [FK_tbl_Payment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentArticle_tbl_PaymentPassCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentArticle] DROP CONSTRAINT [FK_tbl_PaymentArticle_tbl_PaymentPassCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentArticle_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentArticle] DROP CONSTRAINT [FK_tbl_PaymentArticle_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentBalance_tbl_PaymentArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentBalance] DROP CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentArticle];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentBalance_tbl_PaymentBalance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentBalance] DROP CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentBalance];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentBalance_tbl_PaymentCFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentBalance] DROP CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentCFO];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentBalance_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentBalance] DROP CONSTRAINT [FK_tbl_PaymentBalance_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentCFO_tbl_PaymentPassCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentCFO] DROP CONSTRAINT [FK_tbl_PaymentCFO_tbl_PaymentPassCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentCFO_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentCFO] DROP CONSTRAINT [FK_tbl_PaymentCFO_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_Payment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_Payment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentArticle1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentCFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentCFO1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentPassCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_PaymentPassCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPass_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPass] DROP CONSTRAINT [FK_tbl_PaymentPass_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassCategory_tbl_Sites1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassCategory] DROP CONSTRAINT [FK_tbl_PaymentPassCategory_tbl_Sites1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRule_tbl_PaymentType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRule] DROP CONSTRAINT [FK_tbl_PaymentPassRule_tbl_PaymentType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRule_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRule] DROP CONSTRAINT [FK_tbl_PaymentPassRule_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_Company1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRuleCompany_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany] DROP CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentCFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentPassRulePass_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentPassRulePass] DROP CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentStatus_tbl_PaymentStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentStatus] DROP CONSTRAINT [FK_tbl_PaymentStatus_tbl_PaymentStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentStatus_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentStatus] DROP CONSTRAINT [FK_tbl_PaymentStatus_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentTransition_tbl_PaymentStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentTransition] DROP CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentTransition_tbl_PaymentStatus1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentTransition] DROP CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PaymentTransition_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PaymentTransition] DROP CONSTRAINT [FK_tbl_PaymentTransition_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PortalSettings_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PortalSettings] DROP CONSTRAINT [FK_tbl_PortalSettings_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PriceList_tbl_PriceListStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PriceList] DROP CONSTRAINT [FK_tbl_PriceList_tbl_PriceListStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PriceList_tbl_PriceListType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PriceList] DROP CONSTRAINT [FK_tbl_PriceList_tbl_PriceListType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PriceList_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PriceList] DROP CONSTRAINT [FK_tbl_PriceList_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Priorities_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Priorities] DROP CONSTRAINT [FK_tbl_Priorities_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_Company1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_Company1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_ProductStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_ProductStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_ProductType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Product_tbl_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Product] DROP CONSTRAINT [FK_tbl_Product_tbl_Unit];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductCategory_tbl_ProductCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductCategory] DROP CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductCategory_tbl_ProductCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductCategory] DROP CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductComplectation_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductComplectation] DROP CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductComplectation_tbl_Product1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductComplectation] DROP CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductPhoto_tbl_ProductPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductPhoto] DROP CONSTRAINT [FK_tbl_ProductPhoto_tbl_ProductPhoto];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductPrice_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductPrice] DROP CONSTRAINT [FK_tbl_ProductPrice_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductPrice_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductPrice] DROP CONSTRAINT [FK_tbl_ProductPrice_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductPrice_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductPrice] DROP CONSTRAINT [FK_tbl_ProductPrice_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ProductType_tbl_ProductWorkWithComplectation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ProductType] DROP CONSTRAINT [FK_tbl_ProductType_tbl_ProductWorkWithComplectation];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_PublicationCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_PublicationCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_PublicationStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_PublicationStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_PublicationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_PublicationType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Publication_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Publication] DROP CONSTRAINT [FK_tbl_Publication_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationCategory_tbl_PublicationCategory1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationCategory] DROP CONSTRAINT [FK_tbl_PublicationCategory_tbl_PublicationCategory1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationCategory_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationCategory] DROP CONSTRAINT [FK_tbl_PublicationCategory_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationComment_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationComment] DROP CONSTRAINT [FK_tbl_PublicationComment_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationComment_tbl_Publication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationComment] DROP CONSTRAINT [FK_tbl_PublicationComment_tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationMark_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationMark] DROP CONSTRAINT [FK_tbl_PublicationMark_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationMark_tbl_Publication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationMark] DROP CONSTRAINT [FK_tbl_PublicationMark_tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationMark_tbl_PublicationComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationMark] DROP CONSTRAINT [FK_tbl_PublicationMark_tbl_PublicationComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType] DROP CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType] DROP CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationStatusToPublicationType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType] DROP CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationTerms_tbl_Publication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationTerms] DROP CONSTRAINT [FK_tbl_PublicationTerms_tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_PublicationAccessComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_PublicationAccessRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessRecord];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_PublicationKind]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationKind];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_RequestSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_PublicationType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_PublicationType] DROP CONSTRAINT [FK_tbl_PublicationType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Region_tbl_Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Region] DROP CONSTRAINT [FK_tbl_Region_tbl_Country];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RelatedPublication_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RelatedPublication] DROP CONSTRAINT [FK_tbl_RelatedPublication_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RelatedPublication_tbl_Publication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RelatedPublication] DROP CONSTRAINT [FK_tbl_RelatedPublication_tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Reminder_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Reminder] DROP CONSTRAINT [FK_tbl_Reminder_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Reminder_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Reminder] DROP CONSTRAINT [FK_tbl_Reminder_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Contact_Owner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Contact_Owner];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Contact_Responsible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Contact_Responsible];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_RequestSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_RequestStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_RequestStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_ServiceLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_ServiceLevel];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Request_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Request] DROP CONSTRAINT [FK_tbl_Request_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestComment_tbl_Request]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestComment] DROP CONSTRAINT [FK_tbl_RequestComment_tbl_Request];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestComment_tbl_RequestComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestComment] DROP CONSTRAINT [FK_tbl_RequestComment_tbl_RequestComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestComment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestComment] DROP CONSTRAINT [FK_tbl_RequestComment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestComment_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestComment] DROP CONSTRAINT [FK_tbl_RequestComment_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestComment_tbl_User_Destination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestComment] DROP CONSTRAINT [FK_tbl_RequestComment_tbl_User_Destination];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestCommentMark_tbl_RequestComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestCommentMark] DROP CONSTRAINT [FK_tbl_RequestCommentMark_tbl_RequestComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestCommentMark_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestCommentMark] DROP CONSTRAINT [FK_tbl_RequestCommentMark_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestFile_tbl_Request]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestFile] DROP CONSTRAINT [FK_tbl_RequestFile_tbl_Request];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestHistory_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestHistory] DROP CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestHistory_tbl_Contact_Responsible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestHistory] DROP CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact_Responsible];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestHistory_tbl_Request]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestHistory] DROP CONSTRAINT [FK_tbl_RequestHistory_tbl_Request];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestSourceType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestSourceType] DROP CONSTRAINT [FK_tbl_RequestSourceType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestSourceType_tbl_RequestSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestSourceType] DROP CONSTRAINT [FK_tbl_RequestSourceType_tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestSourceType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestSourceType] DROP CONSTRAINT [FK_tbl_RequestSourceType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestToRequirement_tbl_Request]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestToRequirement] DROP CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Request];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequestToRequirement_tbl_Requirement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequestToRequirement] DROP CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Requirement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Contact_Owner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Owner];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Contact_Responsible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Responsible];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Contract]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Contract];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Invoice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Product_EvaluationRequirements]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Product_EvaluationRequirements];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_PublicationCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_PublicationCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequestSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Requirement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Requirement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementComplexity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementComplexity];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementImplementationComplete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementImplementationComplete];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementPriority]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementPriority];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementSatisfaction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSatisfaction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementSeverityOfExposure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSeverityOfExposure];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementSpeedTime]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSpeedTime];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_RequirementType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_RequirementType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_ServiceLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_ServiceLevel];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Unit];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Requirement_tbl_Unit_Internal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Requirement] DROP CONSTRAINT [FK_tbl_Requirement_tbl_Unit_Internal];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComment_tbl_Requirement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComment] DROP CONSTRAINT [FK_tbl_RequirementComment_tbl_Requirement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComment_tbl_RequirementComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComment] DROP CONSTRAINT [FK_tbl_RequirementComment_tbl_RequirementComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComment] DROP CONSTRAINT [FK_tbl_RequirementComment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComment_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComment] DROP CONSTRAINT [FK_tbl_RequirementComment_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComment_tbl_User_Destination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComment] DROP CONSTRAINT [FK_tbl_RequirementComment_tbl_User_Destination];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementCommentMark_tbl_RequirementComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementCommentMark] DROP CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_RequirementComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementCommentMark_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementCommentMark] DROP CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementComplexity_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementComplexity] DROP CONSTRAINT [FK_tbl_RequirementComplexity_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementHistory_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementHistory] DROP CONSTRAINT [FK_tbl_RequirementHistory_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementHistory_tbl_Requirement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementHistory] DROP CONSTRAINT [FK_tbl_RequirementHistory_tbl_Requirement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementHistory_tbl_RequirementStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementHistory] DROP CONSTRAINT [FK_tbl_RequirementHistory_tbl_RequirementStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementHistory_tbl_ResponsibleContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementHistory] DROP CONSTRAINT [FK_tbl_RequirementHistory_tbl_ResponsibleContact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementImplementationComplete_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementImplementationComplete] DROP CONSTRAINT [FK_tbl_RequirementImplementationComplete_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementPriority_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementPriority] DROP CONSTRAINT [FK_tbl_RequirementPriority_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementSatisfaction_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementSatisfaction] DROP CONSTRAINT [FK_tbl_RequirementSatisfaction_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure] DROP CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure] DROP CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementSeverityOfExposure_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure] DROP CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementSpeedTime_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementSpeedTime] DROP CONSTRAINT [FK_tbl_RequirementSpeedTime_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementStatus_tbl_ServiceLevelRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementStatus] DROP CONSTRAINT [FK_tbl_RequirementStatus_tbl_ServiceLevelRole];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementStatus_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementStatus] DROP CONSTRAINT [FK_tbl_RequirementStatus_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementTransition_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementTransition] DROP CONSTRAINT [FK_tbl_RequirementTransition_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementTransition_tbl_RequirementStatus_Final]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementTransition] DROP CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Final];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementTransition] DROP CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementTransition_tbl_RequirementType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementTransition] DROP CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementTransition_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementTransition] DROP CONSTRAINT [FK_tbl_RequirementTransition_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementType] DROP CONSTRAINT [FK_tbl_RequirementType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_RequirementType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_RequirementType] DROP CONSTRAINT [FK_tbl_RequirementType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Resolutions_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Resolutions] DROP CONSTRAINT [FK_tbl_Resolutions_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Responsible_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Responsible] DROP CONSTRAINT [FK_tbl_Responsible_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Responsible_tbl_Contact1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Responsible] DROP CONSTRAINT [FK_tbl_Responsible_tbl_Contact1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Responsible_tbl_ContactRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Responsible] DROP CONSTRAINT [FK_tbl_Responsible_tbl_ContactRole];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Responsible_tbl_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Responsible] DROP CONSTRAINT [FK_tbl_Responsible_tbl_Workflow];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevel_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevel] DROP CONSTRAINT [FK_tbl_ServiceLevel_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelClient_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelClient] DROP CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelClient_tbl_ServiceLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelClient] DROP CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevel];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelClient] DROP CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelContact] DROP CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ServiceLevelRole_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ServiceLevelRole] DROP CONSTRAINT [FK_tbl_ServiceLevelRole_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Company_Buyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Company_Buyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Company_Executor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Company_Executor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Contact_Buyer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Buyer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Contact_Executor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Executor];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_ShipmentStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_ShipmentType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Shipment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Shipment] DROP CONSTRAINT [FK_tbl_Shipment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentComment_tbl_Shipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentComment] DROP CONSTRAINT [FK_tbl_ShipmentComment_tbl_Shipment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentComment_tbl_ShipmentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentComment] DROP CONSTRAINT [FK_tbl_ShipmentComment_tbl_ShipmentComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentComment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentComment] DROP CONSTRAINT [FK_tbl_ShipmentComment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentComment_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentComment] DROP CONSTRAINT [FK_tbl_ShipmentComment_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentComment_tbl_User_Destination]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentComment] DROP CONSTRAINT [FK_tbl_ShipmentComment_tbl_User_Destination];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentCommentMark_tbl_ShipmentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentCommentMark] DROP CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_ShipmentComment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentCommentMark_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentCommentMark] DROP CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentHistory_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentHistory] DROP CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentHistory_tbl_Shipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentHistory] DROP CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Shipment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentHistory_tbl_ShipmentStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentHistory] DROP CONSTRAINT [FK_tbl_ShipmentHistory_tbl_ShipmentStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_Currency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_Shipment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Shipment];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentProducts_tbl_Unit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentProducts] DROP CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Unit];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentType_tbl_Numerator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentType] DROP CONSTRAINT [FK_tbl_ShipmentType_tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_ShipmentType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_ShipmentType] DROP CONSTRAINT [FK_tbl_ShipmentType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_ActionStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_ActionStatus];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_Contact_Sender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_Contact_Sender];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_SiteActionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteAction_tbl_SourceMonitoring]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteAction] DROP CONSTRAINT [FK_tbl_SiteAction_tbl_SourceMonitoring];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionAttachment_tbl_SiteAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionAttachment] DROP CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_SiteAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionAttachment_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionAttachment] DROP CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionLink_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionLink] DROP CONSTRAINT [FK_tbl_SiteActionLink_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionLink_tbl_Links]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionLink] DROP CONSTRAINT [FK_tbl_SiteActionLink_tbl_Links];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionLink_tbl_SiteAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionLink] DROP CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionLink_tbl_SiteActionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionLink] DROP CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTagValue_tbl_SiteAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTagValue] DROP CONSTRAINT [FK_tbl_SiteActionTagValue_tbl_SiteAction];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplate_tbl_ActionTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplate] DROP CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_ActionTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplate_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplate] DROP CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateRecipient_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient] DROP CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient] DROP CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn] DROP CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn] DROP CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn] DROP CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActionTemplateUserColumn_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn] DROP CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields] DROP CONSTRAINT [FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalForms] DROP CONSTRAINT [FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns] DROP CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns] DROP CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout] DROP CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout] DROP CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleLayout_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout] DROP CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleOption] DROP CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleOption_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleOption] DROP CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRuleOption_tbl_ViewTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRuleOption] DROP CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_ViewTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRules_tbl_RuleTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRules] DROP CONSTRAINT [FK_tbl_SiteActivityRules_tbl_RuleTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRules] DROP CONSTRAINT [FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityRules_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityRules] DROP CONSTRAINT [FK_tbl_SiteActivityRules_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteActivityScoreType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActivityScoreType] DROP CONSTRAINT [FK_tbl_SiteActivityScoreType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteColumns_tbl_ColumnCategories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteColumns] DROP CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnCategories];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteColumns_tbl_ColumnTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteColumns] DROP CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteColumns_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteColumns] DROP CONSTRAINT [FK_tbl_SiteColumns_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteColumnValues_tbl_SiteColumns]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteColumnValues] DROP CONSTRAINT [FK_tbl_SiteColumnValues_tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteDomain_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteDomain] DROP CONSTRAINT [FK_tbl_SiteDomain_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventActionTemplate] DROP CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventActionTemplate] DROP CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventActionTemplate_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventActionTemplate] DROP CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventActionTemplate] DROP CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity] DROP CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateActivity_tbl_EventCategories]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity] DROP CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_EventCategories];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateActivity_tbl_Formula]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity] DROP CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Formula];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity] DROP CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateActivity_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity] DROP CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplates_tbl_LogicConditions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplates] DROP CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_LogicConditions];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplates_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplates] DROP CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateScore_tbl_Operations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateScore] DROP CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Operations];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateScore] DROP CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateScore] DROP CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteEventTemplateScore_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteEventTemplateScore] DROP CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_EmailActions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_EmailActions];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_EmailActions1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_EmailActions1];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_PriceList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Sites_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Sites] DROP CONSTRAINT [FK_tbl_Sites_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteTagObjects_tbl_SiteTags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteTagObjects] DROP CONSTRAINT [FK_tbl_SiteTagObjects_tbl_SiteTags];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteTags_tbl_ObjectTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteTags] DROP CONSTRAINT [FK_tbl_SiteTags_tbl_ObjectTypes];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SiteTags_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteTags] DROP CONSTRAINT [FK_tbl_SiteTags_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SocialAuthorizationToken_tbl_PortalSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SocialAuthorizationToken] DROP CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_PortalSettings];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SocialAuthorizationToken_tbl_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SocialAuthorizationToken] DROP CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_User];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SourceMonitoring_tbl_ReceiverContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SourceMonitoring] DROP CONSTRAINT [FK_tbl_SourceMonitoring_tbl_ReceiverContact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SourceMonitoring_tbl_RequestSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SourceMonitoring] DROP CONSTRAINT [FK_tbl_SourceMonitoring_tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SourceMonitoring_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SourceMonitoring] DROP CONSTRAINT [FK_tbl_SourceMonitoring_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SourceMonitoringFilter_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SourceMonitoringFilter] DROP CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SourceMonitoringFilter] DROP CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_StatisticData_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_StatisticData] DROP CONSTRAINT [FK_tbl_StatisticData_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_CompanyMainMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_CompanyMainMember];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_ContactCreator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_ContactCreator];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_ContactMainMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_ContactMainMember];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_ContactResponsible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_ContactResponsible];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_TaskResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_TaskResult];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Task_tbl_TaskType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Task] DROP CONSTRAINT [FK_tbl_Task_tbl_TaskType];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskDuration_tbl_ContactResponsible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskDuration] DROP CONSTRAINT [FK_tbl_TaskDuration_tbl_ContactResponsible];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskDuration_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskDuration] DROP CONSTRAINT [FK_tbl_TaskDuration_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskHistory_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskHistory] DROP CONSTRAINT [FK_tbl_TaskHistory_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskHistory_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskHistory] DROP CONSTRAINT [FK_tbl_TaskHistory_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskHistory_tbl_TaskResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskHistory] DROP CONSTRAINT [FK_tbl_TaskHistory_tbl_TaskResult];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskMember_tbl_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskMember] DROP CONSTRAINT [FK_tbl_TaskMember_tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskMember_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskMember] DROP CONSTRAINT [FK_tbl_TaskMember_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskMember_tbl_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskMember] DROP CONSTRAINT [FK_tbl_TaskMember_tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskMember_tbl_OrderProducts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskMember] DROP CONSTRAINT [FK_tbl_TaskMember_tbl_OrderProducts];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskMember_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskMember] DROP CONSTRAINT [FK_tbl_TaskMember_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskPersonalComment_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskPersonalComment] DROP CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskPersonalComment_tbl_Task]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskPersonalComment] DROP CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskResult_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskResult] DROP CONSTRAINT [FK_tbl_TaskResult_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_TaskMembersCount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_TaskMembersCount];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_TaskTypeAdjustDuration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeAdjustDuration];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_TaskTypeCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_TaskType_tbl_TaskTypePaymentScheme]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_TaskType] DROP CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypePaymentScheme];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SiteActionTemplate] DROP CONSTRAINT [FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Term_tbl_Publication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Term] DROP CONSTRAINT [FK_tbl_Term_tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Term_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Term] DROP CONSTRAINT [FK_tbl_Term_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_User_tbl_Contact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_User] DROP CONSTRAINT [FK_tbl_User_tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_User_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_User] DROP CONSTRAINT [FK_tbl_User_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WebSite_tbl_SiteDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WebSite] DROP CONSTRAINT [FK_tbl_WebSite_tbl_SiteDomain];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WebSite_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WebSite] DROP CONSTRAINT [FK_tbl_WebSite_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WebSitePage_tbl_WebSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WebSitePage] DROP CONSTRAINT [FK_tbl_WebSitePage_tbl_WebSite];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WebSitePageExternalResource_tbl_ExternalResource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WebSitePageExternalResource] DROP CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_ExternalResource];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WebSitePageExternalResource_tbl_WebSitePage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WebSitePageExternalResource] DROP CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_WebSitePage];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Widget_tbl_WidgetCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Widget] DROP CONSTRAINT [FK_tbl_Widget_tbl_WidgetCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WidgetCategory_tbl_WidgetCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WidgetCategory] DROP CONSTRAINT [FK_tbl_WidgetCategory_tbl_WidgetCategory];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WidgetToAccessProfile_tbl_AccessProfile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WidgetToAccessProfile] DROP CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WidgetToAccessProfile_tbl_Module]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WidgetToAccessProfile] DROP CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WidgetToAccessProfile_tbl_Widget]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WidgetToAccessProfile] DROP CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Widget];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Workflow_tbl_MassWorkflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Workflow] DROP CONSTRAINT [FK_tbl_Workflow_tbl_MassWorkflow];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Workflow_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Workflow] DROP CONSTRAINT [FK_tbl_Workflow_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_Workflow_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_Workflow] DROP CONSTRAINT [FK_tbl_Workflow_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowElement_tbl_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowElement] DROP CONSTRAINT [FK_tbl_WorkflowElement_tbl_Workflow];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowElement] DROP CONSTRAINT [FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowParameter_tbl_Workflow]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowParameter] DROP CONSTRAINT [FK_tbl_WorkflowParameter_tbl_Workflow];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowParameter] DROP CONSTRAINT [FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplate_tbl_Sites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplate] DROP CONSTRAINT [FK_tbl_WorkflowTemplate_tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent] DROP CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent] DROP CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElement] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementEvent] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementParameter] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementPeriod] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementRelation] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementResult] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateElementTag] DROP CONSTRAINT [FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateGoal] DROP CONSTRAINT [FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement] DROP CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement] DROP CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateParameter] DROP CONSTRAINT [FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_WorkflowTemplateRole] DROP CONSTRAINT [FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbl_AccessProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AccessProfile];
GO

IF OBJECT_ID(N'[dbo].[tbl_AccessProfileModule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AccessProfileModule];
GO

IF OBJECT_ID(N'[dbo].[tbl_AccessProfileModuleEditionOption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AccessProfileModuleEditionOption];
GO

IF OBJECT_ID(N'[dbo].[tbl_AccessProfileRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AccessProfileRecord];
GO

IF OBJECT_ID(N'[dbo].[tbl_ActionStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ActionStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_ActionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ActionTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_ActivityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ActivityTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Address];
GO

IF OBJECT_ID(N'[dbo].[tbl_AdvertisingCampaign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AdvertisingCampaign];
GO

IF OBJECT_ID(N'[dbo].[tbl_AdvertisingPlatform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AdvertisingPlatform];
GO

IF OBJECT_ID(N'[dbo].[tbl_AdvertisingType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AdvertisingType];
GO

IF OBJECT_ID(N'[dbo].[tbl_AdvertisingTypeCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AdvertisingTypeCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_Analytic]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Analytic];
GO

IF OBJECT_ID(N'[dbo].[tbl_AnalyticAxis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AnalyticAxis];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[tbl_AnalyticAxisFilterValues]', 'U') IS NOT NULL
    DROP TABLE [WebCounterModelStoreContainer].[tbl_AnalyticAxisFilterValues];
GO

IF OBJECT_ID(N'[dbo].[tbl_AnalyticReport]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AnalyticReport];
GO

IF OBJECT_ID(N'[dbo].[tbl_AnalyticReportSystem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AnalyticReportSystem];
GO

IF OBJECT_ID(N'[dbo].[tbl_AnalyticReportUserSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AnalyticReportUserSettings];
GO

IF OBJECT_ID(N'[dbo].[tbl_Bank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Bank];
GO

IF OBJECT_ID(N'[dbo].[tbl_Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Brand];
GO

IF OBJECT_ID(N'[dbo].[tbl_Browsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Browsers];
GO

IF OBJECT_ID(N'[dbo].[tbl_City]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_City];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[tbl_CityIP]', 'U') IS NOT NULL
    DROP TABLE [WebCounterModelStoreContainer].[tbl_CityIP];
GO

IF OBJECT_ID(N'[dbo].[tbl_ColumnCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ColumnCategories];
GO

IF OBJECT_ID(N'[dbo].[tbl_ColumnTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ColumnTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_ColumnTypesExpression]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ColumnTypesExpression];
GO

IF OBJECT_ID(N'[dbo].[tbl_Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Company];
GO

IF OBJECT_ID(N'[dbo].[tbl_CompanyLegalAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CompanyLegalAccount];
GO

IF OBJECT_ID(N'[dbo].[tbl_CompanySector]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CompanySector];
GO

IF OBJECT_ID(N'[dbo].[tbl_CompanySize]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CompanySize];
GO

IF OBJECT_ID(N'[dbo].[tbl_CompanyType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CompanyType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Contact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Contact];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactActivity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactActivity];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactActivityScore]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactActivityScore];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactActivityScoreHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactActivityScoreHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactColumnValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactColumnValues];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactCommunication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactCommunication];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactFunctionInCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactFunctionInCompany];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactJobLevel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactJobLevel];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactRole];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactRoleType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactRoleType];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactSessions];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactToContactRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactToContactRole];
GO

IF OBJECT_ID(N'[dbo].[tbl_ContactType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ContactType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Contract]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Contract];
GO

IF OBJECT_ID(N'[dbo].[tbl_Country]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Country];
GO

IF OBJECT_ID(N'[WebCounterModelStoreContainer].[tbl_CountryIP]', 'U') IS NOT NULL
    DROP TABLE [WebCounterModelStoreContainer].[tbl_CountryIP];
GO

IF OBJECT_ID(N'[dbo].[tbl_CronJob]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CronJob];
GO

IF OBJECT_ID(N'[dbo].[tbl_Currency]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Currency];
GO

IF OBJECT_ID(N'[dbo].[tbl_CurrencyCourse]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_CurrencyCourse];
GO

IF OBJECT_ID(N'[dbo].[tbl_Dictionary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Dictionary];
GO

IF OBJECT_ID(N'[dbo].[tbl_DictionaryGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_DictionaryGroup];
GO

IF OBJECT_ID(N'[dbo].[tbl_Direction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Direction];
GO

IF OBJECT_ID(N'[dbo].[tbl_District]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_District];
GO

IF OBJECT_ID(N'[dbo].[tbl_EmailActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_EmailActions];
GO

IF OBJECT_ID(N'[dbo].[tbl_EmailStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_EmailStats];
GO

IF OBJECT_ID(N'[dbo].[tbl_EmailStatsUnsubscribe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_EmailStatsUnsubscribe];
GO

IF OBJECT_ID(N'[dbo].[tbl_EmailToAnalysis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_EmailToAnalysis];
GO

IF OBJECT_ID(N'[dbo].[tbl_EventCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_EventCategories];
GO

IF OBJECT_ID(N'[dbo].[tbl_ExpirationAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ExpirationAction];
GO

IF OBJECT_ID(N'[dbo].[tbl_ExternalResource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ExternalResource];
GO

IF OBJECT_ID(N'[dbo].[tbl_Filters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Filters];
GO

IF OBJECT_ID(N'[dbo].[tbl_Formula]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Formula];
GO

IF OBJECT_ID(N'[dbo].[tbl_Import]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Import];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportColumn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportColumn];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportColumnRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportColumnRule];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportField]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportField];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportFieldDictionary]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportFieldDictionary];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportKey]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportKey];
GO

IF OBJECT_ID(N'[dbo].[tbl_ImportTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ImportTag];
GO

IF OBJECT_ID(N'[dbo].[tbl_Invoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Invoice];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceCommentMark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceCommentMark];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceInformCatalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceInformCatalog];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceInformForm]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceInformForm];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceProducts];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceToShipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceToShipment];
GO

IF OBJECT_ID(N'[dbo].[tbl_InvoiceType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_InvoiceType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Links]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Links];
GO

IF OBJECT_ID(N'[dbo].[tbl_LogicConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_LogicConditions];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassMail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassMail];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassMailContact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassMailContact];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassMailStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassMailStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassWorkflow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassWorkflow];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassWorkflowContact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassWorkflowContact];
GO

IF OBJECT_ID(N'[dbo].[tbl_MassWorkflowType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MassWorkflowType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Material]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Material];
GO

IF OBJECT_ID(N'[dbo].[tbl_Menu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Menu];
GO

IF OBJECT_ID(N'[dbo].[tbl_MobileDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MobileDevices];
GO

IF OBJECT_ID(N'[dbo].[tbl_Module]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Module];
GO

IF OBJECT_ID(N'[dbo].[tbl_ModuleEdition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ModuleEdition];
GO

IF OBJECT_ID(N'[dbo].[tbl_ModuleEditionAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ModuleEditionAction];
GO

IF OBJECT_ID(N'[dbo].[tbl_ModuleEditionOption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ModuleEditionOption];
GO

IF OBJECT_ID(N'[dbo].[tbl_NamesList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_NamesList];
GO

IF OBJECT_ID(N'[dbo].[tbl_Numerator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Numerator];
GO

IF OBJECT_ID(N'[dbo].[tbl_NumeratorPeriod]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_NumeratorPeriod];
GO

IF OBJECT_ID(N'[dbo].[tbl_NumeratorUsage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_NumeratorUsage];
GO

IF OBJECT_ID(N'[dbo].[tbl_ObjectTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ObjectTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_OperatingSystems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_OperatingSystems];
GO

IF OBJECT_ID(N'[dbo].[tbl_Operations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Operations];
GO

IF OBJECT_ID(N'[dbo].[tbl_Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Order];
GO

IF OBJECT_ID(N'[dbo].[tbl_OrderProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_OrderProducts];
GO

IF OBJECT_ID(N'[dbo].[tbl_OrderStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_OrderStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_OrderType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_OrderType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Payment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Payment];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentArticle]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentArticle];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentBalance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentBalance];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentCFO]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentCFO];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentPass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentPass];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentPassCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentPassCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentPassRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentPassRule];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentPassRuleCompany]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentPassRuleCompany];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentPassRulePass]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentPassRulePass];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentTransition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentTransition];
GO

IF OBJECT_ID(N'[dbo].[tbl_PaymentType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PaymentType];
GO

IF OBJECT_ID(N'[dbo].[tbl_PortalSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PortalSettings];
GO

IF OBJECT_ID(N'[dbo].[tbl_PriceList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PriceList];
GO

IF OBJECT_ID(N'[dbo].[tbl_PriceListStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PriceListStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_PriceListType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PriceListType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Priorities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Priorities];
GO

IF OBJECT_ID(N'[dbo].[tbl_Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Product];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductComplectation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductComplectation];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductPhoto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductPhoto];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductPrice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductPrice];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductType];
GO

IF OBJECT_ID(N'[dbo].[tbl_ProductWorkWithComplectation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ProductWorkWithComplectation];
GO

IF OBJECT_ID(N'[dbo].[tbl_Publication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Publication];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationAccessComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationAccessComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationAccessRecord]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationAccessRecord];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationKind]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationKind];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationMark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationMark];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationStatusToPublicationType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationStatusToPublicationType];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationTerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationTerms];
GO

IF OBJECT_ID(N'[dbo].[tbl_PublicationType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PublicationType];
GO

IF OBJECT_ID(N'[dbo].[tbl_ReadyToSell]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ReadyToSell];
GO

IF OBJECT_ID(N'[dbo].[tbl_Region]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Region];
GO

IF OBJECT_ID(N'[dbo].[tbl_RelatedPublication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RelatedPublication];
GO

IF OBJECT_ID(N'[dbo].[tbl_Reminder]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Reminder];
GO

IF OBJECT_ID(N'[dbo].[tbl_Request]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Request];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestCommentMark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestCommentMark];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestFile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestFile];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestSourceCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestSourceCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestSourceType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestSourceType];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequestToRequirement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequestToRequirement];
GO

IF OBJECT_ID(N'[dbo].[tbl_Requirement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Requirement];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementCommentMark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementCommentMark];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementComplexity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementComplexity];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementImplementationComplete]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementImplementationComplete];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementPriority]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementPriority];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementSatisfaction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementSatisfaction];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementSeverityOfExposure]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementSeverityOfExposure];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementSpeedTime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementSpeedTime];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementTransition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementTransition];
GO

IF OBJECT_ID(N'[dbo].[tbl_RequirementType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RequirementType];
GO

IF OBJECT_ID(N'[dbo].[tbl_Resolutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Resolutions];
GO

IF OBJECT_ID(N'[dbo].[tbl_Responsible]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Responsible];
GO

IF OBJECT_ID(N'[dbo].[tbl_RuleTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_RuleTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevel];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelClient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelClient];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelContact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelContact];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelIncludeToInform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelIncludeToInform];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelInform]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelInform];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelInformComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelInformComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelOutOfListServiceContacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelOutOfListServiceContacts];
GO

IF OBJECT_ID(N'[dbo].[tbl_ServiceLevelRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ServiceLevelRole];
GO

IF OBJECT_ID(N'[dbo].[tbl_SessionSourceRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SessionSourceRule];
GO

IF OBJECT_ID(N'[dbo].[tbl_Shipment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Shipment];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentCommentMark]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentCommentMark];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentProducts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentProducts];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentStatus];
GO

IF OBJECT_ID(N'[dbo].[tbl_ShipmentType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ShipmentType];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteAction];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionAttachment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionAttachment];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionLink]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionLink];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionTagValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionTagValue];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionTemplateRecipient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionTemplateRecipient];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActionTemplateUserColumn]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActionTemplateUserColumn];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRuleExternalFormFields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRuleExternalForms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRuleExternalForms];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRuleFormColumns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRuleFormColumns];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRuleLayout]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRuleLayout];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRuleOption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRuleOption];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityRules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityRules];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteActivityScoreType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteActivityScoreType];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteColumns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteColumns];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteColumnValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteColumnValues];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteDomain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteDomain];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteEventActionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteEventActionTemplate];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteEventTemplateActivity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteEventTemplateActivity];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteEventTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteEventTemplates];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteEventTemplateScore]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteEventTemplateScore];
GO

IF OBJECT_ID(N'[dbo].[tbl_Sites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Sites];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteTagObjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteTagObjects];
GO

IF OBJECT_ID(N'[dbo].[tbl_SiteTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SiteTags];
GO

IF OBJECT_ID(N'[dbo].[tbl_SocialAuthorizationToken]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SocialAuthorizationToken];
GO

IF OBJECT_ID(N'[dbo].[tbl_SourceMonitoring]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SourceMonitoring];
GO

IF OBJECT_ID(N'[dbo].[tbl_SourceMonitoringFilter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SourceMonitoringFilter];
GO

IF OBJECT_ID(N'[dbo].[tbl_StartAfterTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_StartAfterTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_StatisticData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_StatisticData];
GO

IF OBJECT_ID(N'[dbo].[tbl_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Status];
GO

IF OBJECT_ID(N'[dbo].[tbl_Task]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Task];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskDuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskDuration];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskHistory];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskMember]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskMember];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskMembersCount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskMembersCount];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskPersonalComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskPersonalComment];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskResult]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskResult];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskType];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskTypeAdjustDuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskTypeAdjustDuration];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskTypeCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskTypeCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_TaskTypePaymentScheme]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TaskTypePaymentScheme];
GO

IF OBJECT_ID(N'[dbo].[tbl_Term]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Term];
GO

IF OBJECT_ID(N'[dbo].[tbl_Unit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Unit];
GO

IF OBJECT_ID(N'[dbo].[tbl_User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_User];
GO

IF OBJECT_ID(N'[dbo].[tbl_UserSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_UserSettings];
GO

IF OBJECT_ID(N'[dbo].[tbl_ViewTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ViewTypes];
GO

IF OBJECT_ID(N'[dbo].[tbl_WebSite]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WebSite];
GO

IF OBJECT_ID(N'[dbo].[tbl_WebSitePage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WebSitePage];
GO

IF OBJECT_ID(N'[dbo].[tbl_WebSitePageExternalResource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WebSitePageExternalResource];
GO

IF OBJECT_ID(N'[dbo].[tbl_Widget]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Widget];
GO

IF OBJECT_ID(N'[dbo].[tbl_WidgetCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WidgetCategory];
GO

IF OBJECT_ID(N'[dbo].[tbl_WidgetToAccessProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WidgetToAccessProfile];
GO

IF OBJECT_ID(N'[dbo].[tbl_Workflow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Workflow];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowElement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowElement];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowParameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowParameter];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplate];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateConditionEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateConditionEvent];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElement];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementEvent];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementExternalRequest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementParameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementParameter];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementPeriod]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementPeriod];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementRelation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementRelation];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementResult]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementResult];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateElementTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateElementTag];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateGoal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateGoal];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateGoalElement]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateGoalElement];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateParameter]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateParameter];
GO

IF OBJECT_ID(N'[dbo].[tbl_WorkflowTemplateRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_WorkflowTemplateRole];
GO


-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

GO
