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
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_AccessProfile'
CREATE TABLE [dbo].[tbl_AccessProfile] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(255)  NOT NULL,
    [DomainsCount] int  NULL,
    [ContactsPageUrl] nvarchar(2048)  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ActiveContactsCount] int  NULL,
    [EmailPerContactCount] int  NULL,
    [DurationPeriod] int  NULL
);
GO

-- Creating table 'tbl_AccessProfileModule'
CREATE TABLE [dbo].[tbl_AccessProfileModule] (
    [ID] uniqueidentifier  NOT NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [Read] bit  NOT NULL,
    [Write] bit  NOT NULL,
    [Delete] bit  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_AccessProfileRecord'
CREATE TABLE [dbo].[tbl_AccessProfileRecord] (
    [ID] uniqueidentifier  NOT NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [CompanyRuleID] tinyint  NOT NULL,
    [CompanyID] uniqueidentifier  NULL,
    [OwnerRuleID] tinyint  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [Read] bit  NOT NULL,
    [Write] bit  NOT NULL,
    [Delete] bit  NOT NULL
);
GO

-- Creating table 'tbl_ActionStatus'
CREATE TABLE [dbo].[tbl_ActionStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_ActionTypes'
CREATE TABLE [dbo].[tbl_ActionTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_ActivityTypes'
CREATE TABLE [dbo].[tbl_ActivityTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_Address'
CREATE TABLE [dbo].[tbl_Address] (
    [ID] uniqueidentifier  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [CountryID] uniqueidentifier  NULL,
    [CityID] uniqueidentifier  NULL,
    [DistrictID] uniqueidentifier  NULL,
    [RegionID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_AdvertisingCampaign'
CREATE TABLE [dbo].[tbl_AdvertisingCampaign] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Code] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_AdvertisingPlatform'
CREATE TABLE [dbo].[tbl_AdvertisingPlatform] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Code] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_AdvertisingType'
CREATE TABLE [dbo].[tbl_AdvertisingType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Code] nvarchar(256)  NOT NULL,
    [AdvertisingTypeCategoryID] int  NOT NULL
);
GO

-- Creating table 'tbl_AdvertisingTypeCategory'
CREATE TABLE [dbo].[tbl_AdvertisingTypeCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_Analytic'
CREATE TABLE [dbo].[tbl_Analytic] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Query] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_AnalyticAxis'
CREATE TABLE [dbo].[tbl_AnalyticAxis] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] nvarchar(256)  NOT NULL,
    [AxisRoleID] int  NOT NULL,
    [DataSet] varchar(256)  NULL,
    [Query] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_AnalyticAxisFilterValues'
CREATE TABLE [dbo].[tbl_AnalyticAxisFilterValues] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticAxisID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ColumnName] varchar(100)  NULL,
    [Value] nvarchar(256)  NULL,
    [DisplayOrder] int  NOT NULL,
    [FilterOperatorID] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [FilterType] int  NOT NULL,
    [Query] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_AnalyticReport'
CREATE TABLE [dbo].[tbl_AnalyticReport] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Description] nvarchar(1024)  NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_AnalyticReportSystem'
CREATE TABLE [dbo].[tbl_AnalyticReportSystem] (
    [ID] uniqueidentifier  NOT NULL,
    [AnalyticReportID] uniqueidentifier  NOT NULL,
    [AnalyticAxisID] uniqueidentifier  NOT NULL,
    [AxisTypeID] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_AnalyticReportUserSettings'
CREATE TABLE [dbo].[tbl_AnalyticReportUserSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [AnalyticReportID] uniqueidentifier  NOT NULL,
    [AxisToBuildID] uniqueidentifier  NULL,
    [DataSetValues] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_Bank'
CREATE TABLE [dbo].[tbl_Bank] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [BIK] nvarchar(256)  NULL,
    [KS] nvarchar(256)  NULL,
    [CityID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Brand'
CREATE TABLE [dbo].[tbl_Brand] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_Browsers'
CREATE TABLE [dbo].[tbl_Browsers] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Version] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_City'
CREATE TABLE [dbo].[tbl_City] (
    [ImportID] int  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportCountryID] int  NOT NULL,
    [Latitude] decimal(10,6)  NOT NULL,
    [Longitude] decimal(10,6)  NOT NULL,
    [ImportRegionID] int  NULL,
    [ImportDistrictID] int  NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL,
    [RegionID] uniqueidentifier  NOT NULL,
    [DistrictID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_CityIP'
CREATE TABLE [dbo].[tbl_CityIP] (
    [ImportCityID] int  NOT NULL,
    [BeginIP] bigint  NOT NULL,
    [EndIP] bigint  NOT NULL,
    [CityID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_ColumnCategories'
CREATE TABLE [dbo].[tbl_ColumnCategories] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'tbl_ColumnTypes'
CREATE TABLE [dbo].[tbl_ColumnTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'tbl_ColumnTypesExpression'
CREATE TABLE [dbo].[tbl_ColumnTypesExpression] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ColumnTypesID] int  NOT NULL,
    [Expression] nvarchar(512)  NOT NULL
);
GO

-- Creating table 'tbl_Company'
CREATE TABLE [dbo].[tbl_Company] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [CompanyTypeID] uniqueidentifier  NULL,
    [ParentID] uniqueidentifier  NULL,
    [CompanySizeID] uniqueidentifier  NULL,
    [CompanySectorID] uniqueidentifier  NULL,
    [Phone1] nvarchar(250)  NULL,
    [Phone2] nvarchar(250)  NULL,
    [Fax] nvarchar(250)  NULL,
    [Web] nvarchar(250)  NULL,
    [Email] nvarchar(250)  NULL,
    [EmailStatusID] int  NULL,
    [LocationAddressID] uniqueidentifier  NULL,
    [PostalAddressID] uniqueidentifier  NULL,
    [ReadyToSellID] uniqueidentifier  NULL,
    [PriorityID] uniqueidentifier  NULL,
    [StatusID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [BehaviorScore] int  NOT NULL,
    [CharacteristicsScore] int  NOT NULL
);
GO

-- Creating table 'tbl_CompanyLegalAccount'
CREATE TABLE [dbo].[tbl_CompanyLegalAccount] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [CompanyID] uniqueidentifier  NOT NULL,
    [OfficialTitle] nvarchar(256)  NULL,
    [LegalAddress] nvarchar(2048)  NULL,
    [OGRN] nvarchar(256)  NULL,
    [RegistrationDate] datetime  NULL,
    [INN] nvarchar(256)  NULL,
    [KPP] nvarchar(256)  NULL,
    [RS] nvarchar(256)  NULL,
    [BankID] uniqueidentifier  NULL,
    [IsPrimary] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [HeadSignatureFileName] nvarchar(512)  NULL,
    [AccountantID] uniqueidentifier  NULL,
    [AccountantSignatureFileName] nvarchar(512)  NULL,
    [HeadID] uniqueidentifier  NULL,
    [StampFileName] nvarchar(512)  NULL
);
GO

-- Creating table 'tbl_CompanySector'
CREATE TABLE [dbo].[tbl_CompanySector] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_CompanySize'
CREATE TABLE [dbo].[tbl_CompanySize] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_CompanyType'
CREATE TABLE [dbo].[tbl_CompanyType] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_Contact'
CREATE TABLE [dbo].[tbl_Contact] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [LastActivityAt] datetime  NULL,
    [RefferURL] nvarchar(2000)  NOT NULL,
    [UserIP] varchar(15)  NOT NULL,
    [UserFullName] nvarchar(255)  NULL,
    [Email] nvarchar(255)  NULL,
    [Phone] varchar(50)  NULL,
    [ReadyToSellID] uniqueidentifier  NULL,
    [PriorityID] uniqueidentifier  NULL,
    [StatusID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [BehaviorScore] int  NOT NULL,
    [CharacteristicsScore] int  NOT NULL,
    [IsNameChecked] bit  NOT NULL,
    [Surname] nvarchar(255)  NULL,
    [Name] nvarchar(255)  NULL,
    [Patronymic] nvarchar(255)  NULL,
    [CellularPhone] varchar(50)  NULL,
    [CellularPhoneStatusID] int  NULL,
    [EmailStatusID] int  NULL,
    [ContactTypeID] uniqueidentifier  NULL,
    [JobTitle] nvarchar(250)  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [BirthDate] datetime  NULL,
    [ContactFunctionInCompanyID] uniqueidentifier  NULL,
    [ContactJobLevelID] uniqueidentifier  NULL,
    [AddressID] uniqueidentifier  NULL,
    [RefferID] uniqueidentifier  NULL,
    [AdvertisingTypeID] uniqueidentifier  NULL,
    [AdvertisingPlatformID] uniqueidentifier  NULL,
    [AdvertisingCampaignID] uniqueidentifier  NULL,
    [Gender] int  NULL,
    [Category] int  NOT NULL,
    [RegistrationSourceID] int  NOT NULL,
    [Comment] nvarchar(2048)  NULL,
    [CameFromURL] nvarchar(2000)  NULL
);
GO

-- Creating table 'tbl_ContactActivity'
CREATE TABLE [dbo].[tbl_ContactActivity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ActivityTypeID] int  NOT NULL,
    [ActivityCode] nvarchar(255)  NULL,
    [ContactSessionID] uniqueidentifier  NULL,
    [SourceMonitoringID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_ContactActivityScore'
CREATE TABLE [dbo].[tbl_ContactActivityScore] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityScoreTypeID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [Score] int  NOT NULL,
    [ScoreCategory] int  NOT NULL
);
GO

-- Creating table 'tbl_ContactActivityScoreHistory'
CREATE TABLE [dbo].[tbl_ContactActivityScoreHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactActivityScoreID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL,
    [Score] int  NOT NULL,
    [ActivityDate] datetime  NOT NULL,
    [Comment] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_ContactColumnValues'
CREATE TABLE [dbo].[tbl_ContactColumnValues] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [StringValue] nvarchar(512)  NULL,
    [DateValue] datetime  NULL,
    [SiteColumnValueID] uniqueidentifier  NULL,
    [LogicalValue] bit  NULL
);
GO

-- Creating table 'tbl_ContactCommunication'
CREATE TABLE [dbo].[tbl_ContactCommunication] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [CommunicationType] int  NOT NULL,
    [CommunicationNumber] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ContactFunctionInCompany'
CREATE TABLE [dbo].[tbl_ContactFunctionInCompany] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_ContactJobLevel'
CREATE TABLE [dbo].[tbl_ContactJobLevel] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_ContactRole'
CREATE TABLE [dbo].[tbl_ContactRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [RoleTypeID] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Email] nvarchar(255)  NULL,
    [DisplayName] nvarchar(255)  NULL,
    [SiteTagID] uniqueidentifier  NULL,
    [MethodAssigningResponsible] int  NULL,
    [LastAssignmentResponsible] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_ContactRoleType'
CREATE TABLE [dbo].[tbl_ContactRoleType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tbl_ContactSessions'
CREATE TABLE [dbo].[tbl_ContactSessions] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SessionDate] datetime  NOT NULL,
    [RefferURL] nvarchar(2000)  NULL,
    [UserIP] varchar(15)  NULL,
    [BrowserID] uniqueidentifier  NULL,
    [OperatingSystemID] uniqueidentifier  NULL,
    [ResolutionID] uniqueidentifier  NULL,
    [MobileDeviceID] uniqueidentifier  NULL,
    [UserAgent] nvarchar(500)  NULL,
    [UserSessionNumber] int  NOT NULL,
    [EnterPointUrl] nvarchar(2000)  NULL,
    [Keywords] nvarchar(2000)  NULL,
    [Content] nvarchar(2000)  NULL,
    [ImportCityID] int  NULL,
    [ImportCountryID] int  NULL,
    [CityID] uniqueidentifier  NULL,
    [CountryID] uniqueidentifier  NULL,
    [RefferID] uniqueidentifier  NULL,
    [AdvertisingTypeID] uniqueidentifier  NULL,
    [AdvertisingPlatformID] uniqueidentifier  NULL,
    [AdvertisingCampaignID] uniqueidentifier  NULL,
    [CameFromURL] nvarchar(2000)  NULL
);
GO

-- Creating table 'tbl_ContactToContactRole'
CREATE TABLE [dbo].[tbl_ContactToContactRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactRoleID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_ContactType'
CREATE TABLE [dbo].[tbl_ContactType] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_Contract'
CREATE TABLE [dbo].[tbl_Contract] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Number] nvarchar(256)  NULL,
    [SerialNumber] int  NULL,
    [ClientID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Country'
CREATE TABLE [dbo].[tbl_Country] (
    [ImportID] int  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Code] nchar(2)  NOT NULL,
    [ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_CountryIP'
CREATE TABLE [dbo].[tbl_CountryIP] (
    [ImportCountryID] int  NOT NULL,
    [BeginIP] bigint  NOT NULL,
    [EndIP] bigint  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_CronJob'
CREATE TABLE [dbo].[tbl_CronJob] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Assembly] nvarchar(256)  NOT NULL,
    [Type] nvarchar(256)  NOT NULL,
    [Period] int  NOT NULL,
    [LastRunAt] datetime  NULL,
    [LastRunStatusID] int  NULL,
    [NextRunPlannedAt] datetime  NULL,
    [ExecutionTime] int  NOT NULL
);
GO

-- Creating table 'tbl_Currency'
CREATE TABLE [dbo].[tbl_Currency] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [NumCode] nvarchar(3)  NOT NULL,
    [CharCode] nvarchar(3)  NOT NULL,
    [Symbol] nvarchar(10)  NOT NULL,
    [IsBaseCurrency] bit  NOT NULL,
    [IsUpdateInternalCourse] bit  NOT NULL,
    [InternalCoursePercent] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'tbl_CurrencyCourse'
CREATE TABLE [dbo].[tbl_CurrencyCourse] (
    [ID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Nominal] int  NOT NULL,
    [Course] decimal(19,4)  NOT NULL,
    [InternalCourse] decimal(19,4)  NULL
);
GO

-- Creating table 'tbl_Dictionary'
CREATE TABLE [dbo].[tbl_Dictionary] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(128)  NOT NULL,
    [DataSet] nvarchar(50)  NOT NULL,
    [AccessLevelID] int  NOT NULL,
    [DictionaryGroupID] uniqueidentifier  NULL,
    [EditFormUserControl] nvarchar(1024)  NULL
);
GO

-- Creating table 'tbl_DictionaryGroup'
CREATE TABLE [dbo].[tbl_DictionaryGroup] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Direction'
CREATE TABLE [dbo].[tbl_Direction] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_District'
CREATE TABLE [dbo].[tbl_District] (
    [ImportID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportCountryID] int  NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL,
    [RegionID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_EmailActions'
CREATE TABLE [dbo].[tbl_EmailActions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_EmailStats'
CREATE TABLE [dbo].[tbl_EmailStats] (
    [ID] uniqueidentifier  NOT NULL,
    [Email] nvarchar(256)  NOT NULL,
    [ReturnCount] int  NOT NULL
);
GO

-- Creating table 'tbl_EmailStatsUnsubscribe'
CREATE TABLE [dbo].[tbl_EmailStatsUnsubscribe] (
    [EmailStatsID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL
);
GO

-- Creating table 'tbl_EmailToAnalysis'
CREATE TABLE [dbo].[tbl_EmailToAnalysis] (
    [ID] uniqueidentifier  NOT NULL,
    [SourceMonitoringID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [From] nvarchar(255)  NOT NULL,
    [Name] nvarchar(255)  NULL,
    [Subject] nvarchar(255)  NOT NULL,
    [MessageText] nvarchar(max)  NOT NULL,
    [POPMessageID] nvarchar(150)  NULL
);
GO

-- Creating table 'tbl_EventCategories'
CREATE TABLE [dbo].[tbl_EventCategories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_ExpirationAction'
CREATE TABLE [dbo].[tbl_ExpirationAction] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_ExternalResource'
CREATE TABLE [dbo].[tbl_ExternalResource] (
    [ID] uniqueidentifier  NOT NULL,
    [DestinationID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ResourcePlaceID] int  NOT NULL,
    [ExternalResourceTypeID] int  NOT NULL,
    [File] nvarchar(256)  NULL,
    [Text] nvarchar(max)  NULL,
    [Url] nvarchar(1024)  NULL
);
GO

-- Creating table 'tbl_Filters'
CREATE TABLE [dbo].[tbl_Filters] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ClassName] nvarchar(255)  NULL,
    [Expressions] nvarchar(max)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_Formula'
CREATE TABLE [dbo].[tbl_Formula] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_Import'
CREATE TABLE [dbo].[tbl_Import] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ImportTable] tinyint  NOT NULL,
    [SheetName] nvarchar(255)  NOT NULL,
    [FirstRow] int  NOT NULL,
    [FirstColumn] int  NOT NULL,
    [IsFirstRowAsColumnNames] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [Type] int  NOT NULL,
    [CsvSeparator] nvarchar(10)  NULL
);
GO

-- Creating table 'tbl_ImportColumn'
CREATE TABLE [dbo].[tbl_ImportColumn] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Source] nvarchar(255)  NOT NULL,
    [SystemName] nvarchar(255)  NOT NULL,
    [PrimaryKey] bit  NOT NULL,
    [SecondaryKey] bit  NOT NULL,
    [Order] tinyint  NOT NULL
);
GO

-- Creating table 'tbl_ImportColumnRule'
CREATE TABLE [dbo].[tbl_ImportColumnRule] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [ImportFieldID] uniqueidentifier  NOT NULL,
    [ImportColumnID] uniqueidentifier  NOT NULL,
    [IsRequired] bit  NOT NULL,
    [SQLCode] nvarchar(max)  NULL,
    [ImportFieldDictionaryID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_ImportField'
CREATE TABLE [dbo].[tbl_ImportField] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportTable] int  NOT NULL,
    [FieldTitle] nvarchar(255)  NOT NULL,
    [TableName] varchar(50)  NULL,
    [FieldName] varchar(50)  NULL,
    [IsDictionary] bit  NOT NULL,
    [IsAddress] bit  NOT NULL,
    [Order] tinyint  NOT NULL,
    [ParentTableName] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_ImportFieldDictionary'
CREATE TABLE [dbo].[tbl_ImportFieldDictionary] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportFieldID] uniqueidentifier  NOT NULL,
    [TableName] varchar(50)  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Order] tinyint  NOT NULL
);
GO

-- Creating table 'tbl_ImportKey'
CREATE TABLE [dbo].[tbl_ImportKey] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [TableName] nvarchar(255)  NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [LeadForceID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_ImportTag'
CREATE TABLE [dbo].[tbl_ImportTag] (
    [ID] uniqueidentifier  NOT NULL,
    [ImportID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL
);
GO

-- Creating table 'tbl_Invoice'
CREATE TABLE [dbo].[tbl_Invoice] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [InvoiceTypeID] uniqueidentifier  NOT NULL,
    [InvoiceStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerCompanyLegalAccountID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorCompanyLegalAccountID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [InvoiceAmount] decimal(19,4)  NOT NULL,
    [Paid] decimal(19,4)  NOT NULL,
    [PaymentDatePlanned] datetime  NULL,
    [PaymentDateActual] datetime  NULL,
    [OrderID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [IsExistBuyerComplaint] bit  NOT NULL,
    [IsPaymentDateFixedByContract] bit  NOT NULL,
    [ModifiedAt] datetime  NULL
);
GO

-- Creating table 'tbl_InvoiceComment'
CREATE TABLE [dbo].[tbl_InvoiceComment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(256)  NULL,
    [IsOfficialAnswer] bit  NULL,
    [DestinationUserID] uniqueidentifier  NULL,
    [ReplyToID] uniqueidentifier  NULL,
    [IsInternal] bit  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceCommentMark'
CREATE TABLE [dbo].[tbl_InvoiceCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceHistory'
CREATE TABLE [dbo].[tbl_InvoiceHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [InvoiceID] uniqueidentifier  NOT NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [PaymentDatePlanned] datetime  NULL,
    [PaymentDateActual] datetime  NULL,
    [InvoiceAmount] decimal(19,4)  NOT NULL,
    [InvoiceStatusID] int  NOT NULL,
    [IsExistBuyerComplaint] bit  NOT NULL,
    [Note] nvarchar(1024)  NULL
);
GO

-- Creating table 'tbl_InvoiceInformCatalog'
CREATE TABLE [dbo].[tbl_InvoiceInformCatalog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceInformForm'
CREATE TABLE [dbo].[tbl_InvoiceInformForm] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceProducts'
CREATE TABLE [dbo].[tbl_InvoiceProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [InvoiceID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [SerialNumber] nvarchar(255)  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [SpecialOfferPriceListID] uniqueidentifier  NULL,
    [Discount] decimal(18,4)  NULL,
    [CurrencyDiscountAmount] decimal(19,4)  NULL,
    [DiscountAmount] decimal(19,4)  NULL,
    [CurrencyTotalAmount] decimal(19,4)  NOT NULL,
    [TotalAmount] decimal(19,4)  NOT NULL,
    [TaskID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_InvoiceStatus'
CREATE TABLE [dbo].[tbl_InvoiceStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceType'
CREATE TABLE [dbo].[tbl_InvoiceType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL,
    [DirectionID] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_Links'
CREATE TABLE [dbo].[tbl_Links] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [RuleTypeID] int  NOT NULL,
    [Code] nvarchar(50)  NULL,
    [URL] nvarchar(255)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [FileSize] bigint  NULL,
    [Description] nvarchar(500)  NULL
);
GO

-- Creating table 'tbl_LogicConditions'
CREATE TABLE [dbo].[tbl_LogicConditions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_MassMail'
CREATE TABLE [dbo].[tbl_MassMail] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [MailDate] datetime  NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [MassMailStatusID] int  NOT NULL,
    [FocusGroup] int  NULL,
    [MessageText] nvarchar(2000)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SiteTagID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_MassMailContact'
CREATE TABLE [dbo].[tbl_MassMailContact] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [MassMailID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_MassMailStatus'
CREATE TABLE [dbo].[tbl_MassMailStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_MassWorkflow'
CREATE TABLE [dbo].[tbl_MassWorkflow] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Status] int  NOT NULL,
    [StartDate] datetime  NULL,
    [MassWorkflowTypeID] int  NULL
);
GO

-- Creating table 'tbl_MassWorkflowContact'
CREATE TABLE [dbo].[tbl_MassWorkflowContact] (
    [ID] uniqueidentifier  NOT NULL,
    [MassWorkflowID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_MassWorkflowType'
CREATE TABLE [dbo].[tbl_MassWorkflowType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_Material'
CREATE TABLE [dbo].[tbl_Material] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Type] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Value] nvarchar(2000)  NULL,
    [WorkflowTemplateID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Menu'
CREATE TABLE [dbo].[tbl_Menu] (
    [ID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NULL,
    [TabName] nvarchar(255)  NULL,
    [ModuleID] uniqueidentifier  NULL,
    [Order] int  NOT NULL,
    [ModuleEditionActionID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_MobileDevices'
CREATE TABLE [dbo].[tbl_MobileDevices] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_Module'
CREATE TABLE [dbo].[tbl_Module] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [TableName] varchar(50)  NULL
);
GO

-- Creating table 'tbl_ModuleEdition'
CREATE TABLE [dbo].[tbl_ModuleEdition] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ModuleEditionAction'
CREATE TABLE [dbo].[tbl_ModuleEditionAction] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] varchar(256)  NOT NULL,
    [IconPath] nvarchar(1024)  NULL,
    [UserControl] nvarchar(1024)  NOT NULL
);
GO

-- Creating table 'tbl_ModuleEditionOption'
CREATE TABLE [dbo].[tbl_ModuleEditionOption] (
    [ID] uniqueidentifier  NOT NULL,
    [ModuleEditionID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [SystemName] varchar(256)  NOT NULL,
    [ModuleEditionOptionType] int  NOT NULL
);
GO

-- Creating table 'tbl_NamesList'
CREATE TABLE [dbo].[tbl_NamesList] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [PatronymicMask] nvarchar(255)  NULL,
    [Gender] nchar(1)  NOT NULL
);
GO

-- Creating table 'tbl_Numerator'
CREATE TABLE [dbo].[tbl_Numerator] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Mask] nvarchar(256)  NOT NULL,
    [NumeratorPeriodID] int  NOT NULL
);
GO

-- Creating table 'tbl_NumeratorPeriod'
CREATE TABLE [dbo].[tbl_NumeratorPeriod] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_NumeratorUsage'
CREATE TABLE [dbo].[tbl_NumeratorUsage] (
    [ID] uniqueidentifier  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL,
    [DataSet] varchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ObjectTypes'
CREATE TABLE [dbo].[tbl_ObjectTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_OperatingSystems'
CREATE TABLE [dbo].[tbl_OperatingSystems] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Version] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_Operations'
CREATE TABLE [dbo].[tbl_Operations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_Order'
CREATE TABLE [dbo].[tbl_Order] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(255)  NOT NULL,
    [SerialNumber] int  NULL,
    [CreatedAt] datetime  NOT NULL,
    [OrderTypeID] uniqueidentifier  NOT NULL,
    [OrderStatusID] int  NOT NULL,
    [Note] nvarchar(2000)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [Ordered] decimal(19,4)  NOT NULL,
    [Paid] decimal(19,4)  NOT NULL,
    [Shipped] decimal(19,4)  NOT NULL,
    [ExpirationDateBegin] datetime  NULL,
    [ExpirationDateEnd] datetime  NULL,
    [PlannedDeliveryDate] datetime  NULL,
    [ActualDeliveryDate] datetime  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_OrderProducts'
CREATE TABLE [dbo].[tbl_OrderProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [OrderID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [SerialNumber] nvarchar(255)  NULL,
    [PriceListID] uniqueidentifier  NOT NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [SpecialOfferPriceListID] uniqueidentifier  NULL,
    [Discount] decimal(18,4)  NULL,
    [CurrencyDiscountAmount] decimal(19,4)  NULL,
    [DiscountAmount] decimal(19,4)  NULL,
    [CurrencyTotalAmount] decimal(19,4)  NOT NULL,
    [TotalAmount] decimal(19,4)  NOT NULL,
    [ParentOrderProductID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_OrderStatus'
CREATE TABLE [dbo].[tbl_OrderStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_OrderType'
CREATE TABLE [dbo].[tbl_OrderType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsPhysicalDelivery] bit  NOT NULL,
    [ExpirationActionID] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Payment'
CREATE TABLE [dbo].[tbl_Payment] (
    [ID] uniqueidentifier  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Assignment] nvarchar(250)  NOT NULL,
    [DatePlan] datetime  NOT NULL,
    [DateFact] datetime  NULL,
    [PaymentTypeID] int  NULL,
    [StatusID] uniqueidentifier  NULL,
    [PayerID] uniqueidentifier  NULL,
    [PayerLegalAccountID] uniqueidentifier  NULL,
    [RecipientID] uniqueidentifier  NULL,
    [RecipientLegalAccountID] uniqueidentifier  NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Course] float  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Total] decimal(19,4)  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NULL,
    [OrderID] uniqueidentifier  NULL,
    [InvoiceID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_PaymentArticle'
CREATE TABLE [dbo].[tbl_PaymentArticle] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [PaymentPassCategoryID] uniqueidentifier  NOT NULL,
    [Note] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_PaymentBalance'
CREATE TABLE [dbo].[tbl_PaymentBalance] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassCategoryID] uniqueidentifier  NULL,
    [CFOID] uniqueidentifier  NULL,
    [PaymentArticleID] uniqueidentifier  NULL,
    [Date] datetime  NULL,
    [BalancePlan] decimal(19,4)  NULL,
    [BalanceFact] decimal(19,4)  NULL
);
GO

-- Creating table 'tbl_PaymentCFO'
CREATE TABLE [dbo].[tbl_PaymentCFO] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [PaymentPassCategoryID] uniqueidentifier  NOT NULL,
    [Note] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_PaymentPass'
CREATE TABLE [dbo].[tbl_PaymentPass] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentID] uniqueidentifier  NULL,
    [CreatedAt] datetime  NULL,
    [OutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OutgoCFOID] uniqueidentifier  NULL,
    [OutgoPaymentArticleID] uniqueidentifier  NULL,
    [IncomePaymentPassCategoryID] uniqueidentifier  NULL,
    [IncomeCFOID] uniqueidentifier  NULL,
    [IncomePaymentArticleID] uniqueidentifier  NULL,
    [FormulaID] int  NULL,
    [Value] float  NULL,
    [Amount] float  NULL,
    [ProcessedByCron] bit  NOT NULL,
    [ToDelete] bit  NOT NULL,
    [OldOutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OldOutgoCFOID] uniqueidentifier  NULL,
    [OldOutgoPaymentArticleID] uniqueidentifier  NULL,
    [OldAmount] float  NULL,
    [OldCreatedAt] datetime  NULL,
    [IsFact] bit  NULL
);
GO

-- Creating table 'tbl_PaymentPassCategory'
CREATE TABLE [dbo].[tbl_PaymentPassCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Note] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_PaymentPassRule'
CREATE TABLE [dbo].[tbl_PaymentPassRule] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [PaymentTypeID] int  NULL,
    [IsActive] bit  NULL,
    [IsAutomatic] bit  NULL
);
GO

-- Creating table 'tbl_PaymentPassRuleCompany'
CREATE TABLE [dbo].[tbl_PaymentPassRuleCompany] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NOT NULL,
    [PayerID] uniqueidentifier  NULL,
    [PayerLegalAccountID] uniqueidentifier  NULL,
    [RecipientID] uniqueidentifier  NULL,
    [RecipientLegalAccountID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_PaymentPassRulePass'
CREATE TABLE [dbo].[tbl_PaymentPassRulePass] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PaymentPassRuleID] uniqueidentifier  NULL,
    [OutgoPaymentPassCategoryID] uniqueidentifier  NULL,
    [OutgoCFOID] uniqueidentifier  NULL,
    [OutgoPaymentArticleID] uniqueidentifier  NULL,
    [IncomePaymentPassCategoryID] uniqueidentifier  NULL,
    [IncomeCFOID] uniqueidentifier  NULL,
    [IncomePaymentArticleID] uniqueidentifier  NULL,
    [FormulaID] int  NULL,
    [Value] float  NULL
);
GO

-- Creating table 'tbl_PaymentStatus'
CREATE TABLE [dbo].[tbl_PaymentStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsLast] bit  NOT NULL
);
GO

-- Creating table 'tbl_PaymentTransition'
CREATE TABLE [dbo].[tbl_PaymentTransition] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [InitialPaymentStatusID] uniqueidentifier  NOT NULL,
    [FinalPaymentStatusID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_PaymentType'
CREATE TABLE [dbo].[tbl_PaymentType] (
    [ID] int  NOT NULL,
    [Title] nvarchar(250)  NULL
);
GO

-- Creating table 'tbl_PortalSettings'
CREATE TABLE [dbo].[tbl_PortalSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Domain] nvarchar(256)  NULL,
    [Logo] nvarchar(256)  NULL,
    [CompanyMessage] nvarchar(1024)  NULL,
    [HeaderTemplate] nvarchar(4000)  NULL,
    [Title] nvarchar(256)  NOT NULL,
    [WelcomeMessage] nvarchar(256)  NOT NULL,
    [MainMenuBackground] nvarchar(7)  NULL,
    [BlockTitleBackground] nvarchar(7)  NULL,
    [FacebookProfile] nvarchar(512)  NULL,
    [VkontakteProfile] nvarchar(512)  NULL,
    [TwitterProfile] nvarchar(512)  NULL
);
GO

-- Creating table 'tbl_PriceList'
CREATE TABLE [dbo].[tbl_PriceList] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [PriceListTypeID] int  NOT NULL,
    [PriceListStatusID] int  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_PriceListStatus'
CREATE TABLE [dbo].[tbl_PriceListStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PriceListType'
CREATE TABLE [dbo].[tbl_PriceListType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_Priorities'
CREATE TABLE [dbo].[tbl_Priorities] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [MinScore] int  NOT NULL,
    [MaxScore] int  NOT NULL,
    [Image] nvarchar(250)  NULL
);
GO

-- Creating table 'tbl_Product'
CREATE TABLE [dbo].[tbl_Product] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [SKU] nvarchar(250)  NULL,
    [ProductStatusID] int  NULL,
    [ProductCategoryID] uniqueidentifier  NULL,
    [BrandID] uniqueidentifier  NULL,
    [ProductTypeID] uniqueidentifier  NULL,
    [UnitID] uniqueidentifier  NULL,
    [Price] decimal(19,4)  NULL,
    [WholesalePrice] decimal(19,4)  NULL,
    [CostPrice] decimal(19,4)  NULL,
    [SupplierID] uniqueidentifier  NULL,
    [SupplierSKU] nvarchar(250)  NULL,
    [ManufacturerID] uniqueidentifier  NULL,
    [CountryID] uniqueidentifier  NULL,
    [Description] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ModifiedAt] datetime  NULL,
    [CreatedAt] datetime  NULL
);
GO

-- Creating table 'tbl_ProductCategory'
CREATE TABLE [dbo].[tbl_ProductCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_ProductComplectation'
CREATE TABLE [dbo].[tbl_ProductComplectation] (
    [ID] uniqueidentifier  NOT NULL,
    [BaseProductID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [Quantity] float  NOT NULL,
    [Price] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'tbl_ProductPhoto'
CREATE TABLE [dbo].[tbl_ProductPhoto] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [Photo] nvarchar(250)  NOT NULL,
    [Preview] nvarchar(250)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [IsMain] bit  NOT NULL
);
GO

-- Creating table 'tbl_ProductPrice'
CREATE TABLE [dbo].[tbl_ProductPrice] (
    [ID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [PriceListID] uniqueidentifier  NOT NULL,
    [SupplierID] uniqueidentifier  NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [QuantityFrom] float  NULL,
    [QuantityTo] float  NULL,
    [Discount] float  NULL,
    [Price] float  NULL
);
GO

-- Creating table 'tbl_ProductStatus'
CREATE TABLE [dbo].[tbl_ProductStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ProductType'
CREATE TABLE [dbo].[tbl_ProductType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL,
    [ProductWorkWithComplectationID] int  NULL
);
GO

-- Creating table 'tbl_ProductWorkWithComplectation'
CREATE TABLE [dbo].[tbl_ProductWorkWithComplectation] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_Publication'
CREATE TABLE [dbo].[tbl_Publication] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Code] nvarchar(250)  NULL,
    [Date] datetime  NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [PublicationStatusID] uniqueidentifier  NULL,
    [PublicationTypeID] uniqueidentifier  NOT NULL,
    [PublicationCategoryID] uniqueidentifier  NOT NULL,
    [Img] varbinary(max)  NULL,
    [FileName] nvarchar(250)  NULL,
    [Noun] nvarchar(max)  NULL,
    [Text] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [AccessRecord] int  NULL,
    [AccessComment] int  NULL,
    [AccessCompanyID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_PublicationAccessComment'
CREATE TABLE [dbo].[tbl_PublicationAccessComment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PublicationAccessRecord'
CREATE TABLE [dbo].[tbl_PublicationAccessRecord] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PublicationCategory'
CREATE TABLE [dbo].[tbl_PublicationCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [InHelp] bit  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PublicationComment'
CREATE TABLE [dbo].[tbl_PublicationComment] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(250)  NOT NULL,
    [isOfficialAnswer] bit  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tbl_PublicationKind'
CREATE TABLE [dbo].[tbl_PublicationKind] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PublicationMark'
CREATE TABLE [dbo].[tbl_PublicationMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [PublicationCommentID] uniqueidentifier  NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO

-- Creating table 'tbl_PublicationStatus'
CREATE TABLE [dbo].[tbl_PublicationStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [isFirst] bit  NULL,
    [isActive] bit  NULL,
    [isLast] bit  NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_PublicationStatusToPublicationType'
CREATE TABLE [dbo].[tbl_PublicationStatusToPublicationType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [PublicationStatusID] uniqueidentifier  NOT NULL,
    [PublicationTypeID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_PublicationTerms'
CREATE TABLE [dbo].[tbl_PublicationTerms] (
    [ID] uniqueidentifier  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [Term] nvarchar(250)  NOT NULL,
    [PublicationCode] nvarchar(250)  NOT NULL,
    [ElementCode] nvarchar(250)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_PublicationType'
CREATE TABLE [dbo].[tbl_PublicationType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [NumeratorID] uniqueidentifier  NULL,
    [Logo] nvarchar(250)  NULL,
    [TextAdd] nvarchar(250)  NOT NULL,
    [TextMarkToAdd] nvarchar(250)  NULL,
    [TextLike] nvarchar(250)  NOT NULL,
    [TextLikeComment] nvarchar(250)  NOT NULL,
    [PublicationKindID] int  NULL,
    [PublicationAccessRecordID] int  NULL,
    [PublicationAccessCommentID] int  NULL,
    [Order] int  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsSearchable] bit  NOT NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_ReadyToSell'
CREATE TABLE [dbo].[tbl_ReadyToSell] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [MinScore] int  NOT NULL,
    [MaxScore] int  NOT NULL,
    [Image] nvarchar(250)  NULL
);
GO

-- Creating table 'tbl_Region'
CREATE TABLE [dbo].[tbl_Region] (
    [ImportID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ID] uniqueidentifier  NOT NULL,
    [CountryID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_RelatedPublication'
CREATE TABLE [dbo].[tbl_RelatedPublication] (
    [ID] uniqueidentifier  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [RecordID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Reminder'
CREATE TABLE [dbo].[tbl_Reminder] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(2000)  NOT NULL,
    [ReminderDate] datetime  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [ObjectID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_Request'
CREATE TABLE [dbo].[tbl_Request] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [RequestSourceID] uniqueidentifier  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ProductSeriesNumber] nvarchar(256)  NULL,
    [RequestStatusID] int  NOT NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [ServiceLevelID] uniqueidentifier  NULL,
    [ReactionDatePlanned] datetime  NULL,
    [ReactionDateActual] datetime  NULL,
    [LongDescription] nvarchar(max)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ShortDescription] nvarchar(2048)  NULL
);
GO

-- Creating table 'tbl_RequestComment'
CREATE TABLE [dbo].[tbl_RequestComment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(256)  NULL,
    [IsOfficialAnswer] bit  NULL,
    [DestinationUserID] uniqueidentifier  NULL,
    [ReplyToID] uniqueidentifier  NULL,
    [IsInternal] bit  NOT NULL
);
GO

-- Creating table 'tbl_RequestCommentMark'
CREATE TABLE [dbo].[tbl_RequestCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO

-- Creating table 'tbl_RequestFile'
CREATE TABLE [dbo].[tbl_RequestFile] (
    [ID] uniqueidentifier  NOT NULL,
    [RequestID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequestHistory'
CREATE TABLE [dbo].[tbl_RequestHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [RequestID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [RequestStatusID] int  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [ResponsibleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_RequestSourceCategory'
CREATE TABLE [dbo].[tbl_RequestSourceCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequestSourceType'
CREATE TABLE [dbo].[tbl_RequestSourceType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [RequestSourceCategoryID] int  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_RequestStatus'
CREATE TABLE [dbo].[tbl_RequestStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_Requirement'
CREATE TABLE [dbo].[tbl_Requirement] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShortDescription] nvarchar(2048)  NOT NULL,
    [RequestID] uniqueidentifier  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ProductSeriesNumber] nvarchar(256)  NULL,
    [RequirementTypeID] uniqueidentifier  NOT NULL,
    [ServiceLevelID] uniqueidentifier  NOT NULL,
    [RequirementSeverityOfExposureID] uniqueidentifier  NULL,
    [ParentID] uniqueidentifier  NULL,
    [RequirementPriorityID] uniqueidentifier  NULL,
    [RequirementComplexityID] uniqueidentifier  NULL,
    [PublicationCategoryID] uniqueidentifier  NULL,
    [RequirementStatusID] uniqueidentifier  NOT NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [RealizationDatePlanned] datetime  NULL,
    [RealizationDateActual] datetime  NULL,
    [ContractID] uniqueidentifier  NULL,
    [OrderID] uniqueidentifier  NULL,
    [InvoiceID] uniqueidentifier  NULL,
    [EvaluationRequirementsProductID] uniqueidentifier  NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NULL,
    [CurrencyID] uniqueidentifier  NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [RequirementImplementationCompleteID] uniqueidentifier  NULL,
    [RequirementSpeedTimeID] uniqueidentifier  NULL,
    [RequirementSatisfactionID] uniqueidentifier  NULL,
    [EstimationComment] nvarchar(2048)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [LongDescription] nvarchar(max)  NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [InternalQuantity] decimal(18,4)  NOT NULL,
    [InternalUnitID] uniqueidentifier  NULL,
    [EstimateCommentInternal] nvarchar(2048)  NULL,
    [EstimateCommentForClient] nvarchar(2048)  NULL
);
GO

-- Creating table 'tbl_RequirementComment'
CREATE TABLE [dbo].[tbl_RequirementComment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(256)  NULL,
    [IsOfficialAnswer] bit  NULL,
    [DestinationUserID] uniqueidentifier  NULL,
    [ReplyToID] uniqueidentifier  NULL,
    [IsInternal] bit  NOT NULL
);
GO

-- Creating table 'tbl_RequirementCommentMark'
CREATE TABLE [dbo].[tbl_RequirementCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO

-- Creating table 'tbl_RequirementComplexity'
CREATE TABLE [dbo].[tbl_RequirementComplexity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequirementHistory'
CREATE TABLE [dbo].[tbl_RequirementHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [RequirementID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [RequirementStatusID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NULL,
    [ResponsibleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_RequirementImplementationComplete'
CREATE TABLE [dbo].[tbl_RequirementImplementationComplete] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequirementPriority'
CREATE TABLE [dbo].[tbl_RequirementPriority] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequirementSatisfaction'
CREATE TABLE [dbo].[tbl_RequirementSatisfaction] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequirementSeverityOfExposure'
CREATE TABLE [dbo].[tbl_RequirementSeverityOfExposure] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [RequirementTypeID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_RequirementSpeedTime'
CREATE TABLE [dbo].[tbl_RequirementSpeedTime] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_RequirementStatus'
CREATE TABLE [dbo].[tbl_RequirementStatus] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [IsLast] bit  NOT NULL,
    [ServiceLevelRoleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_RequirementTransition'
CREATE TABLE [dbo].[tbl_RequirementTransition] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [InitialRequirementStatusID] uniqueidentifier  NOT NULL,
    [FinalRequirementStatusID] uniqueidentifier  NOT NULL,
    [AllowedAccessProfileID] uniqueidentifier  NULL,
    [IsPortalAllowed] bit  NOT NULL,
    [RequirementTypeID] uniqueidentifier  NULL,
    [IsReviewRequired] bit  NOT NULL
);
GO

-- Creating table 'tbl_RequirementType'
CREATE TABLE [dbo].[tbl_RequirementType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_Resolutions'
CREATE TABLE [dbo].[tbl_Resolutions] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Value] varchar(15)  NOT NULL
);
GO

-- Creating table 'tbl_Responsible'
CREATE TABLE [dbo].[tbl_Responsible] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactRoleID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_RuleTypes'
CREATE TABLE [dbo].[tbl_RuleTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevel'
CREATE TABLE [dbo].[tbl_ServiceLevel] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ReactionTime] int  NOT NULL,
    [ResponseTime] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelClient'
CREATE TABLE [dbo].[tbl_ServiceLevelClient] (
    [ID] uniqueidentifier  NOT NULL,
    [ServiceLevelID] uniqueidentifier  NOT NULL,
    [ClientID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [CountOfServiceContacts] int  NOT NULL,
    [OutOfListServiceContactsID] int  NULL
);
GO

-- Creating table 'tbl_ServiceLevelContact'
CREATE TABLE [dbo].[tbl_ServiceLevelContact] (
    [ID] uniqueidentifier  NOT NULL,
    [ServiceLevelClientID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [InformRequestID] int  NULL,
    [IsAutomateDownload] bit  NOT NULL,
    [IncludeToInformID] int  NOT NULL,
    [InformCommentID] int  NOT NULL,
    [Comment] nvarchar(1024)  NULL,
    [IsInformByRequest] bit  NOT NULL,
    [ServiceLevelRoleID] uniqueidentifier  NULL,
    [IsInformAboutInvoice] bit  NOT NULL,
    [IsInformInvoiceComments] bit  NOT NULL,
    [InvoiceInformCatalogID] int  NOT NULL,
    [InvoiceInformFormID] int  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelIncludeToInform'
CREATE TABLE [dbo].[tbl_ServiceLevelIncludeToInform] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelInform'
CREATE TABLE [dbo].[tbl_ServiceLevelInform] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelInformComment'
CREATE TABLE [dbo].[tbl_ServiceLevelInformComment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelOutOfListServiceContacts'
CREATE TABLE [dbo].[tbl_ServiceLevelOutOfListServiceContacts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ServiceLevelRole'
CREATE TABLE [dbo].[tbl_ServiceLevelRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_SessionSourceRule'
CREATE TABLE [dbo].[tbl_SessionSourceRule] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [SessionRuleTypeID] tinyint  NOT NULL,
    [Pattern] nvarchar(2000)  NOT NULL
);
GO

-- Creating table 'tbl_Shipment'
CREATE TABLE [dbo].[tbl_Shipment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShipmentTypeID] uniqueidentifier  NOT NULL,
    [ShipmentStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL,
    [BuyerCompanyID] uniqueidentifier  NULL,
    [BuyerCompanyLegalAccountID] uniqueidentifier  NULL,
    [BuyerContactID] uniqueidentifier  NULL,
    [ExecutorCompanyID] uniqueidentifier  NULL,
    [ExecutorCompanyLegalAccountID] uniqueidentifier  NULL,
    [ExecutorContactID] uniqueidentifier  NULL,
    [ShipmentAmount] decimal(19,4)  NOT NULL,
    [SendDate] datetime  NULL,
    [OrderID] uniqueidentifier  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [ModifiedAt] datetime  NULL
);
GO

-- Creating table 'tbl_ShipmentComment'
CREATE TABLE [dbo].[tbl_ShipmentComment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [FileName] nvarchar(256)  NULL,
    [IsOfficialAnswer] bit  NULL,
    [DestinationUserID] uniqueidentifier  NULL,
    [ReplyToID] uniqueidentifier  NULL,
    [IsInternal] bit  NOT NULL
);
GO

-- Creating table 'tbl_ShipmentCommentMark'
CREATE TABLE [dbo].[tbl_ShipmentCommentMark] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ContentCommentID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Rank] int  NOT NULL
);
GO

-- Creating table 'tbl_ShipmentHistory'
CREATE TABLE [dbo].[tbl_ShipmentHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShipmentID] uniqueidentifier  NOT NULL,
    [AuthorID] uniqueidentifier  NOT NULL,
    [SendDate] datetime  NULL,
    [ShipmentAmount] decimal(19,4)  NOT NULL,
    [ShipmentStatusID] int  NOT NULL,
    [Note] nvarchar(1024)  NULL
);
GO

-- Creating table 'tbl_ShipmentProducts'
CREATE TABLE [dbo].[tbl_ShipmentProducts] (
    [ID] uniqueidentifier  NOT NULL,
    [ShipmentID] uniqueidentifier  NOT NULL,
    [ProductID] uniqueidentifier  NOT NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [SerialNumber] nvarchar(255)  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NOT NULL,
    [CurrencyID] uniqueidentifier  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [SpecialOfferPriceListID] uniqueidentifier  NULL,
    [Discount] decimal(18,4)  NULL,
    [CurrencyDiscountAmount] decimal(19,4)  NULL,
    [DiscountAmount] decimal(19,4)  NULL,
    [CurrencyTotalAmount] decimal(19,4)  NOT NULL,
    [TotalAmount] decimal(19,4)  NOT NULL,
    [TaskID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_ShipmentStatus'
CREATE TABLE [dbo].[tbl_ShipmentStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_ShipmentType'
CREATE TABLE [dbo].[tbl_ShipmentType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_SiteAction'
CREATE TABLE [dbo].[tbl_SiteAction] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ActionStatusID] int  NOT NULL,
    [ActionDate] datetime  NOT NULL,
    [ResponseDate] datetime  NULL,
    [Comments] nvarchar(max)  NULL,
    [ObjectID] uniqueidentifier  NULL,
    [MessageTypeID] int  NULL,
    [MessageTitle] nvarchar(255)  NULL,
    [SourceMonitoringID] uniqueidentifier  NULL,
    [DirectionID] int  NOT NULL,
    [SenderID] uniqueidentifier  NULL,
    [MessageText] nvarchar(max)  NULL,
    [IsHidden] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [POPMessageID] nvarchar(150)  NULL
);
GO

-- Creating table 'tbl_SiteActionAttachment'
CREATE TABLE [dbo].[tbl_SiteActionAttachment] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_SiteActionLink'
CREATE TABLE [dbo].[tbl_SiteActionLink] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NULL,
    [SiteActionTemplateID] uniqueidentifier  NULL,
    [SiteActivityRuleID] uniqueidentifier  NULL,
    [LinkURL] nvarchar(2000)  NULL,
    [ActionLinkDate] datetime  NULL
);
GO

-- Creating table 'tbl_SiteActionTagValue'
CREATE TABLE [dbo].[tbl_SiteActionTagValue] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActionID] uniqueidentifier  NOT NULL,
    [Tag] nvarchar(256)  NOT NULL,
    [Value] nvarchar(2048)  NOT NULL
);
GO

-- Creating table 'tbl_SiteActionTemplate'
CREATE TABLE [dbo].[tbl_SiteActionTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [ActionTypeID] int  NOT NULL,
    [FromEmail] nvarchar(255)  NULL,
    [ToEmail] nvarchar(255)  NULL,
    [MessageCaption] nvarchar(255)  NULL,
    [MessageBody] nvarchar(max)  NULL,
    [FromName] nvarchar(255)  NULL,
    [ReplyToEmail] nvarchar(255)  NULL,
    [ReplyToName] nvarchar(255)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [SystemName] nvarchar(50)  NULL,
    [ReplaceLinksID] int  NOT NULL,
    [SiteActionTemplateCategoryID] int  NULL,
    [ParentID] uniqueidentifier  NULL,
    [UsageID] uniqueidentifier  NULL,
    [FromContactRoleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_SiteActionTemplateRecipient'
CREATE TABLE [dbo].[tbl_SiteActionTemplateRecipient] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NULL,
    [ContactRoleID] uniqueidentifier  NULL,
    [Email] nvarchar(255)  NULL,
    [DisplayName] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_SiteActionTemplateUserColumn'
CREATE TABLE [dbo].[tbl_SiteActionTemplateUserColumn] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [StringValue] nvarchar(255)  NULL,
    [DateValue] datetime  NULL,
    [SiteColumnValueID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_SiteActivityRuleExternalFormFields'
CREATE TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleExternalFormID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NULL,
    [SiteColumnID] uniqueidentifier  NULL,
    [FieldType] int  NOT NULL,
    [SysField] varchar(50)  NULL
);
GO

-- Creating table 'tbl_SiteActivityRuleExternalForms'
CREATE TABLE [dbo].[tbl_SiteActivityRuleExternalForms] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_SiteActivityRuleFormColumns'
CREATE TABLE [dbo].[tbl_SiteActivityRuleFormColumns] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_SiteActivityRuleLayout'
CREATE TABLE [dbo].[tbl_SiteActivityRuleLayout] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [SiteColumnID] uniqueidentifier  NULL,
    [IsRequired] bit  NOT NULL,
    [IsExtraField] bit  NOT NULL,
    [IsAdmin] bit  NOT NULL,
    [CSSStyle] varchar(max)  NULL,
    [DefaultValue] nvarchar(255)  NULL,
    [Name] nvarchar(255)  NULL,
    [Order] int  NULL,
    [ShowName] bit  NOT NULL,
    [Orientation] int  NULL,
    [OutputFormat] int  NULL,
    [OutputFormatFields] int  NULL,
    [Description] nvarchar(max)  NULL,
    [LayoutType] int  NOT NULL,
    [LayoutParams] nvarchar(max)  NULL,
    [SysField] nvarchar(50)  NULL,
    [LayoutTypeBackup] int  NULL,
    [ColumnTypeExpressionID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_SiteActivityRuleOption'
CREATE TABLE [dbo].[tbl_SiteActivityRuleOption] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ViewTypeID] int  NOT NULL,
    [LastViewDate] datetime  NOT NULL,
    [Views] int  NULL,
    [Result] int  NULL,
    [Conversion] float  NULL,
    [HtmlTemplate] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_SiteActivityRules'
CREATE TABLE [dbo].[tbl_SiteActivityRules] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [RuleTypeID] int  NOT NULL,
    [Code] nvarchar(50)  NULL,
    [URL] nvarchar(255)  NULL,
    [UserFullName] bit  NOT NULL,
    [Email] bit  NOT NULL,
    [Phone] bit  NOT NULL,
    [FormWidth] int  NULL,
    [CountExtraFields] int  NULL,
    [ExternalFormURL] nvarchar(255)  NULL,
    [RepostURL] nvarchar(255)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [CSSButton] nvarchar(500)  NULL,
    [TextButton] nvarchar(255)  NULL,
    [FileSize] bigint  NULL,
    [Description] nvarchar(500)  NULL,
    [CSSForm] nvarchar(500)  NULL,
    [TemplateID] uniqueidentifier  NULL,
    [WufooName] nvarchar(512)  NULL,
    [WufooAPIKey] nvarchar(512)  NULL,
    [WufooRevisionDate] datetime  NULL,
    [WufooUpdatePeriod] int  NULL,
    [ErrorMessage] nvarchar(256)  NULL,
    [Skin] int  NOT NULL,
    [ActionOnFillForm] int  NOT NULL,
    [SendFields] bit  NOT NULL,
    [SuccessMessage] nvarchar(max)  NULL,
    [YandexGoals] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_SiteActivityScoreType'
CREATE TABLE [dbo].[tbl_SiteActivityScoreType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_SiteColumns'
CREATE TABLE [dbo].[tbl_SiteColumns] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NULL,
    [Name] nvarchar(255)  NOT NULL,
    [CategoryID] uniqueidentifier  NOT NULL,
    [TypeID] int  NOT NULL,
    [Code] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_SiteColumnValues'
CREATE TABLE [dbo].[tbl_SiteColumnValues] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [Value] nvarchar(50)  NULL
);
GO

-- Creating table 'tbl_SiteDomain'
CREATE TABLE [dbo].[tbl_SiteDomain] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Domain] nvarchar(256)  NOT NULL,
    [Aliases] nvarchar(max)  NOT NULL,
    [Note] nvarchar(2048)  NOT NULL,
    [StatusID] int  NOT NULL,
    [PageCountWithCounter] int  NOT NULL,
    [TotalPageCount] int  NOT NULL,
    [PageCountWithForms] int  NOT NULL,
    [PageCountWithOnClosingForms] int  NOT NULL
);
GO

-- Creating table 'tbl_SiteEventActionTemplate'
CREATE TABLE [dbo].[tbl_SiteEventActionTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteActionTemplateID] uniqueidentifier  NOT NULL,
    [StartAfter] int  NOT NULL,
    [StartAfterTypeID] int  NOT NULL,
    [MessageText] nvarchar(2000)  NULL
);
GO

-- Creating table 'tbl_SiteEventTemplateActivity'
CREATE TABLE [dbo].[tbl_SiteEventTemplateActivity] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [EventCategoryID] int  NULL,
    [ActivityTypeID] int  NULL,
    [ActivityCode] nvarchar(255)  NULL,
    [ActualPeriod] int  NULL,
    [Option] nvarchar(255)  NULL,
    [FormulaID] int  NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_SiteEventTemplates'
CREATE TABLE [dbo].[tbl_SiteEventTemplates] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [LogicConditionID] int  NOT NULL,
    [ActionCount] int  NULL,
    [EventConditions] nvarchar(max)  NULL,
    [FrequencyPeriod] int  NOT NULL,
    [OwnerID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_SiteEventTemplateScore'
CREATE TABLE [dbo].[tbl_SiteEventTemplateScore] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteActivityScoreTypeID] uniqueidentifier  NOT NULL,
    [OperationID] int  NOT NULL,
    [Score] int  NOT NULL
);
GO

-- Creating table 'tbl_Sites'
CREATE TABLE [dbo].[tbl_Sites] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [SmtpHost] nvarchar(255)  NULL,
    [SmtpUsername] nvarchar(255)  NULL,
    [SmtpPassword] nvarchar(255)  NULL,
    [SmtpPort] int  NULL,
    [SystemEmail] nvarchar(255)  NULL,
    [IsAllowUseSystemEmail] bit  NOT NULL,
    [IsSendEmailToSubscribedUser] bit  NOT NULL,
    [IsUnsubscribe] bit  NOT NULL,
    [UnsubscribeActionID] int  NULL,
    [IsServiceAdvertising] bit  NOT NULL,
    [ServiceAdvertisingActionID] int  NULL,
    [MaxFileSize] int  NULL,
    [FileQuota] int  NULL,
    [SessionTimeout] int  NOT NULL,
    [AccessProfileID] uniqueidentifier  NULL,
    [IsSendFromLeadForce] bit  NOT NULL,
    [ShowHiddenMessages] bit  NOT NULL,
    [LinkProcessingURL] nvarchar(3000)  NULL,
    [HtmlEditorModeID] int  NOT NULL,
    [IsTemplate] bit  NOT NULL,
    [UserSessionTimeout] int  NOT NULL,
    [IsBlockAccessFromDomainsOutsideOfList] bit  NOT NULL,
    [MainUserID] uniqueidentifier  NULL,
    [ActiveUntilDate] datetime  NULL,
    [PriceListID] uniqueidentifier  NULL,
    [PayerCompanyID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_SiteTagObjects'
CREATE TABLE [dbo].[tbl_SiteTagObjects] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [ObjectID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_SiteTags'
CREATE TABLE [dbo].[tbl_SiteTags] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ObjectTypeID] int  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Description] nvarchar(250)  NULL
);
GO

-- Creating table 'tbl_SocialAuthorizationToken'
CREATE TABLE [dbo].[tbl_SocialAuthorizationToken] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [PortalSettingsID] uniqueidentifier  NOT NULL,
    [ExpirationDate] datetime  NOT NULL
);
GO

-- Creating table 'tbl_SourceMonitoring'
CREATE TABLE [dbo].[tbl_SourceMonitoring] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [SourceTypeID] int  NOT NULL,
    [StatusID] int  NOT NULL,
    [Comment] nvarchar(2000)  NULL,
    [PopHost] nvarchar(255)  NOT NULL,
    [PopUserName] nvarchar(255)  NOT NULL,
    [PopPassword] nvarchar(255)  NOT NULL,
    [PopPort] int  NOT NULL,
    [IsSsl] bit  NOT NULL,
    [IsLeaveOnServer] bit  NOT NULL,
    [LastReceivedAt] datetime  NULL,
    [DaysToDelete] int  NULL,
    [SenderProcessingID] int  NOT NULL,
    [ProcessingOfReturnsID] int  NOT NULL,
    [IsRemoveReturns] bit  NOT NULL,
    [ProcessingOfAutoRepliesID] int  NOT NULL,
    [IsRemoveAutoReplies] bit  NOT NULL,
    [OwnerID] uniqueidentifier  NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [ReceiverContactID] uniqueidentifier  NULL,
    [StartDate] datetime  NULL
);
GO

-- Creating table 'tbl_SourceMonitoringFilter'
CREATE TABLE [dbo].[tbl_SourceMonitoringFilter] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SourceMonitoringID] uniqueidentifier  NOT NULL,
    [SourcePropertyID] int  NOT NULL,
    [Mask] nvarchar(2000)  NOT NULL,
    [MonitoringActionID] int  NOT NULL
);
GO

-- Creating table 'tbl_StartAfterTypes'
CREATE TABLE [dbo].[tbl_StartAfterTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [Order] int  NULL
);
GO

-- Creating table 'tbl_StatisticData'
CREATE TABLE [dbo].[tbl_StatisticData] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Code] varchar(256)  NOT NULL,
    [Value] decimal(18,2)  NOT NULL,
    [RecalculateDate] datetime  NULL,
    [PreviousValue] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'tbl_Status'
CREATE TABLE [dbo].[tbl_Status] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_Task'
CREATE TABLE [dbo].[tbl_Task] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(2000)  NOT NULL,
    [TaskTypeID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [DateOfControl] datetime  NOT NULL,
    [IsImportantTask] bit  NOT NULL,
    [PlanDurationMinutes] int  NOT NULL,
    [PlanDurationHours] int  NOT NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL,
    [CreatorID] uniqueidentifier  NOT NULL,
    [ResponsibleReminderDate] datetime  NULL,
    [CreatorReminderDate] datetime  NULL,
    [IsUrgentTask] bit  NOT NULL,
    [TaskStatusID] int  NOT NULL,
    [TaskResultID] uniqueidentifier  NULL,
    [DetailedResult] nvarchar(2000)  NULL,
    [ActualDurationMinutes] int  NULL,
    [ActualDurationHours] int  NULL,
    [CompletePercentage] decimal(18,4)  NULL,
    [OrderID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [MainMemberContactID] uniqueidentifier  NULL,
    [MainMemberCompanyID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_TaskDuration'
CREATE TABLE [dbo].[tbl_TaskDuration] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [SectionDateStart] datetime  NULL,
    [SectionDateEnd] datetime  NULL,
    [ActualDurationMinutes] int  NULL,
    [ActualDurationHours] int  NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [Comment] nvarchar(1024)  NULL
);
GO

-- Creating table 'tbl_TaskHistory'
CREATE TABLE [dbo].[tbl_TaskHistory] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [DateStart] datetime  NULL,
    [DateEnd] datetime  NULL,
    [DateOfControl] datetime  NULL,
    [IsImportantTask] bit  NOT NULL,
    [PlanDurationMinutes] int  NULL,
    [PlanDurationHours] int  NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [TaskStatusID] int  NOT NULL,
    [TaskResultID] uniqueidentifier  NULL,
    [DetailedResult] nvarchar(2000)  NULL
);
GO

-- Creating table 'tbl_TaskMember'
CREATE TABLE [dbo].[tbl_TaskMember] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [ContractorID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [TaskMemberRoleID] int  NULL,
    [TaskMemberStatusID] int  NULL,
    [Comment] nvarchar(1024)  NULL,
    [OrderID] uniqueidentifier  NULL,
    [UserComment] nvarchar(1024)  NULL,
    [OrderProductsID] uniqueidentifier  NULL,
    [IsInformed] bit  NOT NULL
);
GO

-- Creating table 'tbl_TaskMembersCount'
CREATE TABLE [dbo].[tbl_TaskMembersCount] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_TaskPersonalComment'
CREATE TABLE [dbo].[tbl_TaskPersonalComment] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(4000)  NULL,
    [RefusedComment] nvarchar(4000)  NULL
);
GO

-- Creating table 'tbl_TaskResult'
CREATE TABLE [dbo].[tbl_TaskResult] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_TaskType'
CREATE TABLE [dbo].[tbl_TaskType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [StandardDurationHours] int  NOT NULL,
    [StandardDurationMinutes] int  NOT NULL,
    [TaskTypeCategoryID] int  NOT NULL,
    [TaskTypeAdjustDurationID] int  NOT NULL,
    [IsPublicEvent] bit  NOT NULL,
    [TaskTypePaymentSchemeID] int  NOT NULL,
    [IsTimePayment] bit  NOT NULL,
    [ProductID] uniqueidentifier  NULL,
    [TaskMembersCountID] int  NULL
);
GO

-- Creating table 'tbl_TaskTypeAdjustDuration'
CREATE TABLE [dbo].[tbl_TaskTypeAdjustDuration] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_TaskTypeCategory'
CREATE TABLE [dbo].[tbl_TaskTypeCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_TaskTypePaymentScheme'
CREATE TABLE [dbo].[tbl_TaskTypePaymentScheme] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'tbl_Term'
CREATE TABLE [dbo].[tbl_Term] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [PublicationID] uniqueidentifier  NULL,
    [Code] nvarchar(100)  NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_Unit'
CREATE TABLE [dbo].[tbl_Unit] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO

-- Creating table 'tbl_User'
CREATE TABLE [dbo].[tbl_User] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NULL,
    [Login] nvarchar(255)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [AccessProfileID] uniqueidentifier  NULL,
    [AccessLevelID] int  NOT NULL
);
GO

-- Creating table 'tbl_UserSettings'
CREATE TABLE [dbo].[tbl_UserSettings] (
    [ID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [ClassName] nvarchar(255)  NULL,
    [UserSettings] nvarchar(max)  NULL,
    [ShowGroupPanel] bit  NOT NULL,
    [ShowFilterPanel] bit  NOT NULL,
    [ShowAlternativeControl] bit  NOT NULL
);
GO

-- Creating table 'tbl_ViewTypes'
CREATE TABLE [dbo].[tbl_ViewTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WebSite'
CREATE TABLE [dbo].[tbl_WebSite] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Description] nvarchar(2048)  NULL,
    [SiteDomainID] uniqueidentifier  NULL,
    [FavIcon] nvarchar(50)  NULL
);
GO

-- Creating table 'tbl_WebSitePage'
CREATE TABLE [dbo].[tbl_WebSitePage] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Url] nvarchar(1024)  NULL,
    [WebSiteElementStatusID] int  NOT NULL,
    [MetaTitle] nvarchar(256)  NOT NULL,
    [MetaKeywords] nvarchar(2048)  NULL,
    [MetaDescription] nvarchar(2048)  NULL,
    [Body] nvarchar(max)  NULL,
    [IsHomePage] bit  NOT NULL,
    [WebSiteID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_Widget'
CREATE TABLE [dbo].[tbl_Widget] (
    [ID] uniqueidentifier  NOT NULL,
    [WidgetCategoryID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [UserControl] nvarchar(2048)  NULL
);
GO

-- Creating table 'tbl_WidgetCategory'
CREATE TABLE [dbo].[tbl_WidgetCategory] (
    [ID] uniqueidentifier  NOT NULL,
    [ParentID] uniqueidentifier  NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'tbl_WidgetToAccessProfile'
CREATE TABLE [dbo].[tbl_WidgetToAccessProfile] (
    [ID] uniqueidentifier  NOT NULL,
    [WidgetID] uniqueidentifier  NOT NULL,
    [AccessProfileID] uniqueidentifier  NOT NULL,
    [Order] int  NOT NULL,
    [ModuleID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_Workflow'
CREATE TABLE [dbo].[tbl_Workflow] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Author] uniqueidentifier  NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NULL,
    [Status] int  NOT NULL,
    [MassWorkflowID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_WorkflowElement'
CREATE TABLE [dbo].[tbl_WorkflowElement] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [ControlDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Result] nvarchar(255)  NULL,
    [Status] int  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WorkflowParameter'
CREATE TABLE [dbo].[tbl_WorkflowParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateParameterID] uniqueidentifier  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplate'
CREATE TABLE [dbo].[tbl_WorkflowTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NULL,
    [Status] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [ManualStart] bit  NOT NULL,
    [AutomaticMethod] int  NULL,
    [Event] int  NULL,
    [Frequency] int  NOT NULL,
    [Condition] int  NULL,
    [ActivityCount] int  NULL,
    [DenyMultipleRun] bit  NOT NULL,
    [WorkflowXml] nvarchar(max)  NULL,
    [DataBaseStatusID] int  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateConditionEvent'
CREATE TABLE [dbo].[tbl_WorkflowTemplateConditionEvent] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NULL,
    [WorkflowTemplateElementEventID] uniqueidentifier  NULL,
    [Category] int  NOT NULL,
    [ActivityType] int  NULL,
    [Code] nvarchar(max)  NULL,
    [ActualPeriod] int  NULL,
    [Requisite] nvarchar(2000)  NULL,
    [Formula] int  NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElement'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElement] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ElementType] int  NOT NULL,
    [Optional] bit  NOT NULL,
    [ResultName] nvarchar(255)  NULL,
    [AllowOptionalTransfer] bit  NOT NULL,
    [ShowCurrentUser] bit  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [StartAfter] int  NOT NULL,
    [StartPeriod] int  NOT NULL,
    [DurationHours] int  NULL,
    [DurationMinutes] int  NULL,
    [ControlAfter] int  NULL,
    [ControlPeriod] int  NULL,
    [ControlFromBeginProccess] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementEvent'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementEvent] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Condition] int  NOT NULL,
    [ActivityCount] int  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementExternalRequest'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementParameter'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Value] nvarchar(255)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementPeriod'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementPeriod] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [DayWeek] int  NOT NULL,
    [FromTime] time  NULL,
    [ToTime] time  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementRelation'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementRelation] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [StartElementID] uniqueidentifier  NOT NULL,
    [StartElementResult] nvarchar(255)  NULL,
    [EndElementID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementResult'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementResult] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsSystem] bit  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateElementTag'
CREATE TABLE [dbo].[tbl_WorkflowTemplateElementTag] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateElementID] uniqueidentifier  NOT NULL,
    [SiteTagID] uniqueidentifier  NOT NULL,
    [Operation] int  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateGoal'
CREATE TABLE [dbo].[tbl_WorkflowTemplateGoal] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Description] nvarchar(2048)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateParameter'
CREATE TABLE [dbo].[tbl_WorkflowTemplateParameter] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NULL,
    [ModuleID] uniqueidentifier  NULL,
    [IsSystem] bit  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateRole'
CREATE TABLE [dbo].[tbl_WorkflowTemplateRole] (
    [ID] uniqueidentifier  NOT NULL,
    [WorkflowTemplateID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_AccessProfileModuleEditionOption'
CREATE TABLE [dbo].[tbl_AccessProfileModuleEditionOption] (
    [tbl_AccessProfileModule_ID] uniqueidentifier  NOT NULL,
    [tbl_ModuleEditionOption_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_InvoiceToShipment'
CREATE TABLE [dbo].[tbl_InvoiceToShipment] (
    [tbl_Invoice_ID] uniqueidentifier  NOT NULL,
    [tbl_Shipment_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_RequestToRequirement'
CREATE TABLE [dbo].[tbl_RequestToRequirement] (
    [tbl_Request_ID] uniqueidentifier  NOT NULL,
    [tbl_Requirement_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_WebSitePageExternalResource'
CREATE TABLE [dbo].[tbl_WebSitePageExternalResource] (
    [tbl_ExternalResource_ID] uniqueidentifier  NOT NULL,
    [tbl_WebSitePage_ID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'tbl_WorkflowTemplateGoalElement'
CREATE TABLE [dbo].[tbl_WorkflowTemplateGoalElement] (
    [tbl_WorkflowTemplateElement_ID] uniqueidentifier  NOT NULL,
    [tbl_WorkflowTemplateGoal_ID] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [PK_tbl_AccessProfile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [PK_tbl_AccessProfileModule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [PK_tbl_AccessProfileRecord]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ActionStatus'
ALTER TABLE [dbo].[tbl_ActionStatus]
ADD CONSTRAINT [PK_tbl_ActionStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ActionTypes'
ALTER TABLE [dbo].[tbl_ActionTypes]
ADD CONSTRAINT [PK_tbl_ActionTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ActivityTypes'
ALTER TABLE [dbo].[tbl_ActivityTypes]
ADD CONSTRAINT [PK_tbl_ActivityTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Address'
ALTER TABLE [dbo].[tbl_Address]
ADD CONSTRAINT [PK_tbl_Address]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AdvertisingCampaign'
ALTER TABLE [dbo].[tbl_AdvertisingCampaign]
ADD CONSTRAINT [PK_tbl_AdvertisingCampaign]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AdvertisingPlatform'
ALTER TABLE [dbo].[tbl_AdvertisingPlatform]
ADD CONSTRAINT [PK_tbl_AdvertisingPlatform]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [PK_tbl_AdvertisingType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AdvertisingTypeCategory'
ALTER TABLE [dbo].[tbl_AdvertisingTypeCategory]
ADD CONSTRAINT [PK_tbl_AdvertisingTypeCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Analytic'
ALTER TABLE [dbo].[tbl_Analytic]
ADD CONSTRAINT [PK_tbl_Analytic]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AnalyticAxis'
ALTER TABLE [dbo].[tbl_AnalyticAxis]
ADD CONSTRAINT [PK_tbl_AnalyticAxis]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [AnalyticAxisID], [Title], [DisplayOrder], [FilterOperatorID], [IsDefault], [FilterType] in table 'tbl_AnalyticAxisFilterValues'
ALTER TABLE [dbo].[tbl_AnalyticAxisFilterValues]
ADD CONSTRAINT [PK_tbl_AnalyticAxisFilterValues]
    PRIMARY KEY CLUSTERED ([ID], [AnalyticAxisID], [Title], [DisplayOrder], [FilterOperatorID], [IsDefault], [FilterType] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [PK_tbl_AnalyticReport]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [PK_tbl_AnalyticReportSystem]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [PK_tbl_AnalyticReportUserSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Bank'
ALTER TABLE [dbo].[tbl_Bank]
ADD CONSTRAINT [PK_tbl_Bank]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Brand'
ALTER TABLE [dbo].[tbl_Brand]
ADD CONSTRAINT [PK_tbl_Brand]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Browsers'
ALTER TABLE [dbo].[tbl_Browsers]
ADD CONSTRAINT [PK_tbl_Browsers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [PK_tbl_City]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ImportCityID], [BeginIP], [EndIP], [CityID] in table 'tbl_CityIP'
ALTER TABLE [dbo].[tbl_CityIP]
ADD CONSTRAINT [PK_tbl_CityIP]
    PRIMARY KEY CLUSTERED ([ImportCityID], [BeginIP], [EndIP], [CityID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ColumnCategories'
ALTER TABLE [dbo].[tbl_ColumnCategories]
ADD CONSTRAINT [PK_tbl_ColumnCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ColumnTypes'
ALTER TABLE [dbo].[tbl_ColumnTypes]
ADD CONSTRAINT [PK_tbl_ColumnTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [PK_tbl_ColumnTypesExpression]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [PK_tbl_Company]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [PK_tbl_CompanyLegalAccount]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CompanySector'
ALTER TABLE [dbo].[tbl_CompanySector]
ADD CONSTRAINT [PK_tbl_CompanySector]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CompanySize'
ALTER TABLE [dbo].[tbl_CompanySize]
ADD CONSTRAINT [PK_tbl_CompanySize]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CompanyType'
ALTER TABLE [dbo].[tbl_CompanyType]
ADD CONSTRAINT [PK_tbl_CompanyType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [PK_tbl_Contact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [PK_tbl_ContactActivity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [PK_tbl_ContactActivityScore]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactActivityScoreHistory'
ALTER TABLE [dbo].[tbl_ContactActivityScoreHistory]
ADD CONSTRAINT [PK_tbl_ContactActivityScoreHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [PK_tbl_ContactColumnValues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactCommunication'
ALTER TABLE [dbo].[tbl_ContactCommunication]
ADD CONSTRAINT [PK_tbl_ContactCommunication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactFunctionInCompany'
ALTER TABLE [dbo].[tbl_ContactFunctionInCompany]
ADD CONSTRAINT [PK_tbl_ContactFunctionInCompany]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactJobLevel'
ALTER TABLE [dbo].[tbl_ContactJobLevel]
ADD CONSTRAINT [PK_tbl_ContactJobLevel]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [PK_tbl_ContactRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactRoleType'
ALTER TABLE [dbo].[tbl_ContactRoleType]
ADD CONSTRAINT [PK_tbl_ContactRoleType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [PK_tbl_ContactSessions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [PK_tbl_ContactToContactRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ContactType'
ALTER TABLE [dbo].[tbl_ContactType]
ADD CONSTRAINT [PK_tbl_ContactType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [PK_tbl_Contract]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Country'
ALTER TABLE [dbo].[tbl_Country]
ADD CONSTRAINT [PK_tbl_Country]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ImportCountryID], [BeginIP], [EndIP], [CountryID] in table 'tbl_CountryIP'
ALTER TABLE [dbo].[tbl_CountryIP]
ADD CONSTRAINT [PK_tbl_CountryIP]
    PRIMARY KEY CLUSTERED ([ImportCountryID], [BeginIP], [EndIP], [CountryID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CronJob'
ALTER TABLE [dbo].[tbl_CronJob]
ADD CONSTRAINT [PK_tbl_CronJob]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Currency'
ALTER TABLE [dbo].[tbl_Currency]
ADD CONSTRAINT [PK_tbl_Currency]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_CurrencyCourse'
ALTER TABLE [dbo].[tbl_CurrencyCourse]
ADD CONSTRAINT [PK_tbl_CurrencyCourse]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Dictionary'
ALTER TABLE [dbo].[tbl_Dictionary]
ADD CONSTRAINT [PK_tbl_Dictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_DictionaryGroup'
ALTER TABLE [dbo].[tbl_DictionaryGroup]
ADD CONSTRAINT [PK_tbl_DictionaryGroup]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Direction'
ALTER TABLE [dbo].[tbl_Direction]
ADD CONSTRAINT [PK_tbl_Direction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [PK_tbl_District]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_EmailActions'
ALTER TABLE [dbo].[tbl_EmailActions]
ADD CONSTRAINT [PK_tbl_EmailActions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_EmailStats'
ALTER TABLE [dbo].[tbl_EmailStats]
ADD CONSTRAINT [PK_tbl_EmailStats]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmailStatsID], [SiteID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [PK_tbl_EmailStatsUnsubscribe]
    PRIMARY KEY CLUSTERED ([EmailStatsID], [SiteID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_EmailToAnalysis'
ALTER TABLE [dbo].[tbl_EmailToAnalysis]
ADD CONSTRAINT [PK_tbl_EmailToAnalysis]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_EventCategories'
ALTER TABLE [dbo].[tbl_EventCategories]
ADD CONSTRAINT [PK_tbl_EventCategories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ExpirationAction'
ALTER TABLE [dbo].[tbl_ExpirationAction]
ADD CONSTRAINT [PK_tbl_ExpirationAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ExternalResource'
ALTER TABLE [dbo].[tbl_ExternalResource]
ADD CONSTRAINT [PK_tbl_ExternalResource]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Filters'
ALTER TABLE [dbo].[tbl_Filters]
ADD CONSTRAINT [PK_tbl_Filters]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Formula'
ALTER TABLE [dbo].[tbl_Formula]
ADD CONSTRAINT [PK_tbl_Formula]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Import'
ALTER TABLE [dbo].[tbl_Import]
ADD CONSTRAINT [PK_tbl_Import]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportColumn'
ALTER TABLE [dbo].[tbl_ImportColumn]
ADD CONSTRAINT [PK_tbl_ImportColumn]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportColumnRule'
ALTER TABLE [dbo].[tbl_ImportColumnRule]
ADD CONSTRAINT [PK_tbl_ImportColumnRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportField'
ALTER TABLE [dbo].[tbl_ImportField]
ADD CONSTRAINT [PK_tbl_ImportField]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportFieldDictionary'
ALTER TABLE [dbo].[tbl_ImportFieldDictionary]
ADD CONSTRAINT [PK_tbl_ImportFieldDictionary]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportKey'
ALTER TABLE [dbo].[tbl_ImportKey]
ADD CONSTRAINT [PK_tbl_ImportKey]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ImportTag'
ALTER TABLE [dbo].[tbl_ImportTag]
ADD CONSTRAINT [PK_tbl_ImportTag]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [PK_tbl_Invoice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [PK_tbl_InvoiceComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [PK_tbl_InvoiceCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [PK_tbl_InvoiceHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceInformCatalog'
ALTER TABLE [dbo].[tbl_InvoiceInformCatalog]
ADD CONSTRAINT [PK_tbl_InvoiceInformCatalog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceInformForm'
ALTER TABLE [dbo].[tbl_InvoiceInformForm]
ADD CONSTRAINT [PK_tbl_InvoiceInformForm]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [PK_tbl_InvoiceProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceStatus'
ALTER TABLE [dbo].[tbl_InvoiceStatus]
ADD CONSTRAINT [PK_tbl_InvoiceStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [PK_tbl_InvoiceType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [PK_tbl_Links]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_LogicConditions'
ALTER TABLE [dbo].[tbl_LogicConditions]
ADD CONSTRAINT [PK_tbl_LogicConditions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [PK_tbl_MassMail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [PK_tbl_MassMailContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassMailStatus'
ALTER TABLE [dbo].[tbl_MassMailStatus]
ADD CONSTRAINT [PK_tbl_MassMailStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [PK_tbl_MassWorkflow]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassWorkflowContact'
ALTER TABLE [dbo].[tbl_MassWorkflowContact]
ADD CONSTRAINT [PK_tbl_MassWorkflowContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MassWorkflowType'
ALTER TABLE [dbo].[tbl_MassWorkflowType]
ADD CONSTRAINT [PK_tbl_MassWorkflowType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Material'
ALTER TABLE [dbo].[tbl_Material]
ADD CONSTRAINT [PK_tbl_Material]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [PK_tbl_Menu]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MobileDevices'
ALTER TABLE [dbo].[tbl_MobileDevices]
ADD CONSTRAINT [PK_tbl_MobileDevices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Module'
ALTER TABLE [dbo].[tbl_Module]
ADD CONSTRAINT [PK_tbl_Module]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ModuleEdition'
ALTER TABLE [dbo].[tbl_ModuleEdition]
ADD CONSTRAINT [PK_tbl_ModuleEdition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ModuleEditionAction'
ALTER TABLE [dbo].[tbl_ModuleEditionAction]
ADD CONSTRAINT [PK_tbl_ModuleEditionAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ModuleEditionOption'
ALTER TABLE [dbo].[tbl_ModuleEditionOption]
ADD CONSTRAINT [PK_tbl_ModuleEditionOption]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_NamesList'
ALTER TABLE [dbo].[tbl_NamesList]
ADD CONSTRAINT [PK_tbl_NamesList]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [PK_tbl_Numerator]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_NumeratorPeriod'
ALTER TABLE [dbo].[tbl_NumeratorPeriod]
ADD CONSTRAINT [PK_tbl_NumeratorPeriod]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_NumeratorUsage'
ALTER TABLE [dbo].[tbl_NumeratorUsage]
ADD CONSTRAINT [PK_tbl_NumeratorUsage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ObjectTypes'
ALTER TABLE [dbo].[tbl_ObjectTypes]
ADD CONSTRAINT [PK_tbl_ObjectTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_OperatingSystems'
ALTER TABLE [dbo].[tbl_OperatingSystems]
ADD CONSTRAINT [PK_tbl_OperatingSystems]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Operations'
ALTER TABLE [dbo].[tbl_Operations]
ADD CONSTRAINT [PK_tbl_Operations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [PK_tbl_Order]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [PK_tbl_OrderProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_OrderStatus'
ALTER TABLE [dbo].[tbl_OrderStatus]
ADD CONSTRAINT [PK_tbl_OrderStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [PK_tbl_OrderType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [PK_tbl_Payment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentArticle'
ALTER TABLE [dbo].[tbl_PaymentArticle]
ADD CONSTRAINT [PK_tbl_PaymentArticle]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [PK_tbl_PaymentBalance]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [PK_tbl_PaymentCFO]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [PK_tbl_PaymentPass]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentPassCategory'
ALTER TABLE [dbo].[tbl_PaymentPassCategory]
ADD CONSTRAINT [PK_tbl_PaymentPassCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [PK_tbl_PaymentPassRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [PK_tbl_PaymentPassRuleCompany]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [PK_tbl_PaymentPassRulePass]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [PK_tbl_PaymentStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [PK_tbl_PaymentTransition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PaymentType'
ALTER TABLE [dbo].[tbl_PaymentType]
ADD CONSTRAINT [PK_tbl_PaymentType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PortalSettings'
ALTER TABLE [dbo].[tbl_PortalSettings]
ADD CONSTRAINT [PK_tbl_PortalSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [PK_tbl_PriceList]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PriceListStatus'
ALTER TABLE [dbo].[tbl_PriceListStatus]
ADD CONSTRAINT [PK_tbl_PriceListStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PriceListType'
ALTER TABLE [dbo].[tbl_PriceListType]
ADD CONSTRAINT [PK_tbl_PriceListType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Priorities'
ALTER TABLE [dbo].[tbl_Priorities]
ADD CONSTRAINT [PK_tbl_Priorities]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [PK_tbl_Product]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [PK_tbl_ProductCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [PK_tbl_ProductComplectation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_ProductPhoto'
ALTER TABLE [dbo].[tbl_ProductPhoto]
ADD CONSTRAINT [PK_tbl_ProductPhoto]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [PK_tbl_ProductPrice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductStatus'
ALTER TABLE [dbo].[tbl_ProductStatus]
ADD CONSTRAINT [PK_tbl_ProductStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductType'
ALTER TABLE [dbo].[tbl_ProductType]
ADD CONSTRAINT [PK_tbl_ProductType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ProductWorkWithComplectation'
ALTER TABLE [dbo].[tbl_ProductWorkWithComplectation]
ADD CONSTRAINT [PK_tbl_ProductWorkWithComplectation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [PK_tbl_Publication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationAccessComment'
ALTER TABLE [dbo].[tbl_PublicationAccessComment]
ADD CONSTRAINT [PK_tbl_PublicationAccessComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationAccessRecord'
ALTER TABLE [dbo].[tbl_PublicationAccessRecord]
ADD CONSTRAINT [PK_tbl_PublicationAccessRecord]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [PK_tbl_PublicationCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [PK_tbl_PublicationComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationKind'
ALTER TABLE [dbo].[tbl_PublicationKind]
ADD CONSTRAINT [PK_tbl_PublicationKind]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [PK_tbl_PublicationMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationStatus'
ALTER TABLE [dbo].[tbl_PublicationStatus]
ADD CONSTRAINT [PK_tbl_PublicationStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [PK_tbl_PublicationStatusToPublicationType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationTerms'
ALTER TABLE [dbo].[tbl_PublicationTerms]
ADD CONSTRAINT [PK_tbl_PublicationTerms]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [PK_tbl_PublicationType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ReadyToSell'
ALTER TABLE [dbo].[tbl_ReadyToSell]
ADD CONSTRAINT [PK_tbl_ReadyToSell]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Region'
ALTER TABLE [dbo].[tbl_Region]
ADD CONSTRAINT [PK_tbl_Region]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [PK_tbl_RelatedPublication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [PK_tbl_Reminder]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [PK_tbl_Request]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [PK_tbl_RequestComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [PK_tbl_RequestCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestFile'
ALTER TABLE [dbo].[tbl_RequestFile]
ADD CONSTRAINT [PK_tbl_RequestFile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [PK_tbl_RequestHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestSourceCategory'
ALTER TABLE [dbo].[tbl_RequestSourceCategory]
ADD CONSTRAINT [PK_tbl_RequestSourceCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [PK_tbl_RequestSourceType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequestStatus'
ALTER TABLE [dbo].[tbl_RequestStatus]
ADD CONSTRAINT [PK_tbl_RequestStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [PK_tbl_Requirement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [PK_tbl_RequirementComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [PK_tbl_RequirementCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementComplexity'
ALTER TABLE [dbo].[tbl_RequirementComplexity]
ADD CONSTRAINT [PK_tbl_RequirementComplexity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [PK_tbl_RequirementHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementImplementationComplete'
ALTER TABLE [dbo].[tbl_RequirementImplementationComplete]
ADD CONSTRAINT [PK_tbl_RequirementImplementationComplete]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementPriority'
ALTER TABLE [dbo].[tbl_RequirementPriority]
ADD CONSTRAINT [PK_tbl_RequirementPriority]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementSatisfaction'
ALTER TABLE [dbo].[tbl_RequirementSatisfaction]
ADD CONSTRAINT [PK_tbl_RequirementSatisfaction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [PK_tbl_RequirementSeverityOfExposure]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementSpeedTime'
ALTER TABLE [dbo].[tbl_RequirementSpeedTime]
ADD CONSTRAINT [PK_tbl_RequirementSpeedTime]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [PK_tbl_RequirementStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [PK_tbl_RequirementTransition]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [PK_tbl_RequirementType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Resolutions'
ALTER TABLE [dbo].[tbl_Resolutions]
ADD CONSTRAINT [PK_tbl_Resolutions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [PK_tbl_Responsible]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_RuleTypes'
ALTER TABLE [dbo].[tbl_RuleTypes]
ADD CONSTRAINT [PK_tbl_RuleTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevel'
ALTER TABLE [dbo].[tbl_ServiceLevel]
ADD CONSTRAINT [PK_tbl_ServiceLevel]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [PK_tbl_ServiceLevelClient]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [PK_tbl_ServiceLevelContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelIncludeToInform'
ALTER TABLE [dbo].[tbl_ServiceLevelIncludeToInform]
ADD CONSTRAINT [PK_tbl_ServiceLevelIncludeToInform]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelInform'
ALTER TABLE [dbo].[tbl_ServiceLevelInform]
ADD CONSTRAINT [PK_tbl_ServiceLevelInform]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelInformComment'
ALTER TABLE [dbo].[tbl_ServiceLevelInformComment]
ADD CONSTRAINT [PK_tbl_ServiceLevelInformComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelOutOfListServiceContacts'
ALTER TABLE [dbo].[tbl_ServiceLevelOutOfListServiceContacts]
ADD CONSTRAINT [PK_tbl_ServiceLevelOutOfListServiceContacts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ServiceLevelRole'
ALTER TABLE [dbo].[tbl_ServiceLevelRole]
ADD CONSTRAINT [PK_tbl_ServiceLevelRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SessionSourceRule'
ALTER TABLE [dbo].[tbl_SessionSourceRule]
ADD CONSTRAINT [PK_tbl_SessionSourceRule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [PK_tbl_Shipment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [PK_tbl_ShipmentComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [PK_tbl_ShipmentCommentMark]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [PK_tbl_ShipmentHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [PK_tbl_ShipmentProducts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentStatus'
ALTER TABLE [dbo].[tbl_ShipmentStatus]
ADD CONSTRAINT [PK_tbl_ShipmentStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ShipmentType'
ALTER TABLE [dbo].[tbl_ShipmentType]
ADD CONSTRAINT [PK_tbl_ShipmentType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [PK_tbl_SiteAction]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [PK_tbl_SiteActionAttachment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [PK_tbl_SiteActionLink]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionTagValue'
ALTER TABLE [dbo].[tbl_SiteActionTagValue]
ADD CONSTRAINT [PK_tbl_SiteActionTagValue]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [PK_tbl_SiteActionTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [PK_tbl_SiteActionTemplateRecipient]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [PK_tbl_SiteActionTemplateUserColumn]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleExternalFormFields'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleExternalFormFields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleExternalForms'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalForms]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleExternalForms]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleFormColumns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleLayout]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleOption]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [PK_tbl_SiteActivityRules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteActivityScoreType'
ALTER TABLE [dbo].[tbl_SiteActivityScoreType]
ADD CONSTRAINT [PK_tbl_SiteActivityScoreType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [PK_tbl_SiteColumns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteColumnValues'
ALTER TABLE [dbo].[tbl_SiteColumnValues]
ADD CONSTRAINT [PK_tbl_SiteColumnValues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteDomain'
ALTER TABLE [dbo].[tbl_SiteDomain]
ADD CONSTRAINT [PK_tbl_SiteDomain]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [PK_tbl_SiteEventActionTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [PK_tbl_SiteEventTemplateActivity]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [PK_tbl_SiteEventTemplates]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [PK_tbl_SiteEventTemplateScore]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [PK_tbl_Sites]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteTagObjects'
ALTER TABLE [dbo].[tbl_SiteTagObjects]
ADD CONSTRAINT [PK_tbl_SiteTagObjects]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [PK_tbl_SiteTags]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [PK_tbl_SocialAuthorizationToken]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [PK_tbl_SourceMonitoring]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [PK_tbl_SourceMonitoringFilter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_StartAfterTypes'
ALTER TABLE [dbo].[tbl_StartAfterTypes]
ADD CONSTRAINT [PK_tbl_StartAfterTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_StatisticData'
ALTER TABLE [dbo].[tbl_StatisticData]
ADD CONSTRAINT [PK_tbl_StatisticData]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Status'
ALTER TABLE [dbo].[tbl_Status]
ADD CONSTRAINT [PK_tbl_Status]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [PK_tbl_Task]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [PK_tbl_TaskDuration]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [PK_tbl_TaskHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [PK_tbl_TaskMember]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskMembersCount'
ALTER TABLE [dbo].[tbl_TaskMembersCount]
ADD CONSTRAINT [PK_tbl_TaskMembersCount]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [PK_tbl_TaskPersonalComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskResult'
ALTER TABLE [dbo].[tbl_TaskResult]
ADD CONSTRAINT [PK_tbl_TaskResult]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [PK_tbl_TaskType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskTypeAdjustDuration'
ALTER TABLE [dbo].[tbl_TaskTypeAdjustDuration]
ADD CONSTRAINT [PK_tbl_TaskTypeAdjustDuration]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskTypeCategory'
ALTER TABLE [dbo].[tbl_TaskTypeCategory]
ADD CONSTRAINT [PK_tbl_TaskTypeCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_TaskTypePaymentScheme'
ALTER TABLE [dbo].[tbl_TaskTypePaymentScheme]
ADD CONSTRAINT [PK_tbl_TaskTypePaymentScheme]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [PK_tbl_Term]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Unit'
ALTER TABLE [dbo].[tbl_Unit]
ADD CONSTRAINT [PK_tbl_Unit]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [PK_tbl_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_UserSettings'
ALTER TABLE [dbo].[tbl_UserSettings]
ADD CONSTRAINT [PK_tbl_UserSettings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ViewTypes'
ALTER TABLE [dbo].[tbl_ViewTypes]
ADD CONSTRAINT [PK_tbl_ViewTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [PK_tbl_WebSite]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WebSitePage'
ALTER TABLE [dbo].[tbl_WebSitePage]
ADD CONSTRAINT [PK_tbl_WebSitePage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Widget'
ALTER TABLE [dbo].[tbl_Widget]
ADD CONSTRAINT [PK_tbl_Widget]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WidgetCategory'
ALTER TABLE [dbo].[tbl_WidgetCategory]
ADD CONSTRAINT [PK_tbl_WidgetCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [PK_tbl_WidgetToAccessProfile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [PK_tbl_Workflow]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [PK_tbl_WorkflowElement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [PK_tbl_WorkflowParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplate'
ALTER TABLE [dbo].[tbl_WorkflowTemplate]
ADD CONSTRAINT [PK_tbl_WorkflowTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateConditionEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElement]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementEvent]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementEvent]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementExternalRequest'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementExternalRequest]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementParameter]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementPeriod'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementPeriod]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementPeriod]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementRelation'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementRelation]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementRelation]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementResult'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementResult]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementResult]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateElementTag'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementTag]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateElementTag]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateGoal'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoal]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateGoal]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateParameter]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateParameter]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_WorkflowTemplateRole'
ALTER TABLE [dbo].[tbl_WorkflowTemplateRole]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [tbl_AccessProfileModule_ID], [tbl_ModuleEditionOption_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [PK_tbl_AccessProfileModuleEditionOption]
    PRIMARY KEY CLUSTERED ([tbl_AccessProfileModule_ID], [tbl_ModuleEditionOption_ID] ASC);
GO

-- Creating primary key on [tbl_Invoice_ID], [tbl_Shipment_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [PK_tbl_InvoiceToShipment]
    PRIMARY KEY CLUSTERED ([tbl_Invoice_ID], [tbl_Shipment_ID] ASC);
GO

-- Creating primary key on [tbl_Request_ID], [tbl_Requirement_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [PK_tbl_RequestToRequirement]
    PRIMARY KEY CLUSTERED ([tbl_Request_ID], [tbl_Requirement_ID] ASC);
GO

-- Creating primary key on [tbl_ExternalResource_ID], [tbl_WebSitePage_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [PK_tbl_WebSitePageExternalResource]
    PRIMARY KEY CLUSTERED ([tbl_ExternalResource_ID], [tbl_WebSitePage_ID] ASC);
GO

-- Creating primary key on [tbl_WorkflowTemplateElement_ID], [tbl_WorkflowTemplateGoal_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [PK_tbl_WorkflowTemplateGoalElement]
    PRIMARY KEY CLUSTERED ([tbl_WorkflowTemplateElement_ID], [tbl_WorkflowTemplateGoal_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProductID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [FK_tbl_AccessProfile_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfile_tbl_Product'
CREATE INDEX [IX_FK_tbl_AccessProfile_tbl_Product]
ON [dbo].[tbl_AccessProfile]
    ([ProductID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_AccessProfile'
ALTER TABLE [dbo].[tbl_AccessProfile]
ADD CONSTRAINT [FK_tbl_AccessProfile_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfile_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AccessProfile_tbl_Sites]
ON [dbo].[tbl_AccessProfile]
    ([SiteID]);
GO

-- Creating foreign key on [AccessProfileID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_AccessProfile]
ON [dbo].[tbl_AccessProfileModule]
    ([AccessProfileID]);
GO

-- Creating foreign key on [AccessProfileID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileRecord_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_AccessProfileRecord_tbl_AccessProfile]
ON [dbo].[tbl_AccessProfileRecord]
    ([AccessProfileID]);
GO

-- Creating foreign key on [AccessProfileID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_Menu_tbl_AccessProfile]
ON [dbo].[tbl_Menu]
    ([AccessProfileID]);
GO

-- Creating foreign key on [AllowedAccessProfileID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_AccessProfile]
    FOREIGN KEY ([AllowedAccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_AccessProfile]
ON [dbo].[tbl_RequirementTransition]
    ([AllowedAccessProfileID]);
GO

-- Creating foreign key on [AccessProfileID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_Sites_tbl_AccessProfile]
ON [dbo].[tbl_Sites]
    ([AccessProfileID]);
GO

-- Creating foreign key on [AccessProfileID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_AccessProfile]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([AccessProfileID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_Module'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_Module]
ON [dbo].[tbl_AccessProfileModule]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleEditionID] in table 'tbl_AccessProfileModule'
ALTER TABLE [dbo].[tbl_AccessProfileModule]
ADD CONSTRAINT [FK_tbl_AccessProfileModule_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModule_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_AccessProfileModule_tbl_ModuleEdition]
ON [dbo].[tbl_AccessProfileModule]
    ([ModuleEditionID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileRecord_tbl_Module'
CREATE INDEX [IX_FK_tbl_AccessProfileRecord_tbl_Module]
ON [dbo].[tbl_AccessProfileRecord]
    ([ModuleID]);
GO

-- Creating foreign key on [ActionStatusID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_ActionStatus]
    FOREIGN KEY ([ActionStatusID])
    REFERENCES [dbo].[tbl_ActionStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_ActionStatus'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_ActionStatus]
ON [dbo].[tbl_SiteAction]
    ([ActionStatusID]);
GO

-- Creating foreign key on [ActionTypeID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_ActionTypes]
    FOREIGN KEY ([ActionTypeID])
    REFERENCES [dbo].[tbl_ActionTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplate_tbl_ActionTypes'
CREATE INDEX [IX_FK_tbl_SiteActionTemplate_tbl_ActionTypes]
ON [dbo].[tbl_SiteActionTemplate]
    ([ActionTypeID]);
GO

-- Creating foreign key on [ActivityTypeID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_ActivityTypes]
    FOREIGN KEY ([ActivityTypeID])
    REFERENCES [dbo].[tbl_ActivityTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_ActivityTypes'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_ActivityTypes]
ON [dbo].[tbl_ContactActivity]
    ([ActivityTypeID]);
GO

-- Creating foreign key on [ActivityTypeID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes]
    FOREIGN KEY ([ActivityTypeID])
    REFERENCES [dbo].[tbl_ActivityTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_ActivityTypes]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([ActivityTypeID]);
GO

-- Creating foreign key on [LocationAddressID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_LocationAddress]
    FOREIGN KEY ([LocationAddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_LocationAddress'
CREATE INDEX [IX_FK_tbl_Company_LocationAddress]
ON [dbo].[tbl_Company]
    ([LocationAddressID]);
GO

-- Creating foreign key on [PostalAddressID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_PostalAddress]
    FOREIGN KEY ([PostalAddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_PostalAddress'
CREATE INDEX [IX_FK_tbl_Company_PostalAddress]
ON [dbo].[tbl_Company]
    ([PostalAddressID]);
GO

-- Creating foreign key on [AddressID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Address]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tbl_Address]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Address'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Address]
ON [dbo].[tbl_Contact]
    ([AddressID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_AdvertisingCampaign'
ALTER TABLE [dbo].[tbl_AdvertisingCampaign]
ADD CONSTRAINT [FK_tbl_AdvertisingCampaign_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingCampaign_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AdvertisingCampaign_tbl_Sites]
ON [dbo].[tbl_AdvertisingCampaign]
    ([SiteID]);
GO

-- Creating foreign key on [AdvertisingCampaignID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingCampaign]
    FOREIGN KEY ([AdvertisingCampaignID])
    REFERENCES [dbo].[tbl_AdvertisingCampaign]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingCampaign'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingCampaign]
ON [dbo].[tbl_Contact]
    ([AdvertisingCampaignID]);
GO

-- Creating foreign key on [AdvertisingCampaignID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingCampaign]
    FOREIGN KEY ([AdvertisingCampaignID])
    REFERENCES [dbo].[tbl_AdvertisingCampaign]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingCampaign'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingCampaign]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingCampaignID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_AdvertisingPlatform'
ALTER TABLE [dbo].[tbl_AdvertisingPlatform]
ADD CONSTRAINT [FK_tbl_AdvertisingPlatform_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingPlatform_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AdvertisingPlatform_tbl_Sites]
ON [dbo].[tbl_AdvertisingPlatform]
    ([SiteID]);
GO

-- Creating foreign key on [AdvertisingPlatformID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingPlatform]
    FOREIGN KEY ([AdvertisingPlatformID])
    REFERENCES [dbo].[tbl_AdvertisingPlatform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingPlatform'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingPlatform]
ON [dbo].[tbl_Contact]
    ([AdvertisingPlatformID]);
GO

-- Creating foreign key on [AdvertisingPlatformID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingPlatform]
    FOREIGN KEY ([AdvertisingPlatformID])
    REFERENCES [dbo].[tbl_AdvertisingPlatform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingPlatform'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingPlatform]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingPlatformID]);
GO

-- Creating foreign key on [AdvertisingTypeCategoryID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory]
    FOREIGN KEY ([AdvertisingTypeCategoryID])
    REFERENCES [dbo].[tbl_AdvertisingTypeCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory'
CREATE INDEX [IX_FK_tbl_AdvertisingType_tbl_AdvertisingTypeCategory]
ON [dbo].[tbl_AdvertisingType]
    ([AdvertisingTypeCategoryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_AdvertisingType'
ALTER TABLE [dbo].[tbl_AdvertisingType]
ADD CONSTRAINT [FK_tbl_AdvertisingType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AdvertisingType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_AdvertisingType_tbl_Sites]
ON [dbo].[tbl_AdvertisingType]
    ([SiteID]);
GO

-- Creating foreign key on [AdvertisingTypeID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_AdvertisingType]
    FOREIGN KEY ([AdvertisingTypeID])
    REFERENCES [dbo].[tbl_AdvertisingType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_AdvertisingType'
CREATE INDEX [IX_FK_tbl_Contact_tbl_AdvertisingType]
ON [dbo].[tbl_Contact]
    ([AdvertisingTypeID]);
GO

-- Creating foreign key on [AdvertisingTypeID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_AdvertisingType]
    FOREIGN KEY ([AdvertisingTypeID])
    REFERENCES [dbo].[tbl_AdvertisingType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_AdvertisingType'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_AdvertisingType]
ON [dbo].[tbl_ContactSessions]
    ([AdvertisingTypeID]);
GO

-- Creating foreign key on [AnalyticID] in table 'tbl_AnalyticAxis'
ALTER TABLE [dbo].[tbl_AnalyticAxis]
ADD CONSTRAINT [FK_tbl_AnalyticAxis_tbl_Analytic]
    FOREIGN KEY ([AnalyticID])
    REFERENCES [dbo].[tbl_Analytic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticAxis_tbl_Analytic'
CREATE INDEX [IX_FK_tbl_AnalyticAxis_tbl_Analytic]
ON [dbo].[tbl_AnalyticAxis]
    ([AnalyticID]);
GO

-- Creating foreign key on [AnalyticID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [FK_tbl_AnalyticReport_tbl_Analytic]
    FOREIGN KEY ([AnalyticID])
    REFERENCES [dbo].[tbl_Analytic]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReport_tbl_Analytic'
CREATE INDEX [IX_FK_tbl_AnalyticReport_tbl_Analytic]
ON [dbo].[tbl_AnalyticReport]
    ([AnalyticID]);
GO

-- Creating foreign key on [AnalyticAxisID] in table 'tbl_AnalyticAxisFilterValues'
ALTER TABLE [dbo].[tbl_AnalyticAxisFilterValues]
ADD CONSTRAINT [FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis]
    FOREIGN KEY ([AnalyticAxisID])
    REFERENCES [dbo].[tbl_AnalyticAxis]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis'
CREATE INDEX [IX_FK_tbl_AnalyticAxisFilterValues_tbl_AnalyticAxis]
ON [dbo].[tbl_AnalyticAxisFilterValues]
    ([AnalyticAxisID]);
GO

-- Creating foreign key on [AnalyticAxisID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis]
    FOREIGN KEY ([AnalyticAxisID])
    REFERENCES [dbo].[tbl_AnalyticAxis]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis'
CREATE INDEX [IX_FK_tbl_AnalyticReportSystem_tbl_AnalyticAxis]
ON [dbo].[tbl_AnalyticReportSystem]
    ([AnalyticAxisID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_AnalyticReport'
ALTER TABLE [dbo].[tbl_AnalyticReport]
ADD CONSTRAINT [FK_tbl_AnalyticReport_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReport_tbl_Module'
CREATE INDEX [IX_FK_tbl_AnalyticReport_tbl_Module]
ON [dbo].[tbl_AnalyticReport]
    ([ModuleID]);
GO

-- Creating foreign key on [AnalyticReportID] in table 'tbl_AnalyticReportSystem'
ALTER TABLE [dbo].[tbl_AnalyticReportSystem]
ADD CONSTRAINT [FK_tbl_AnalyticReportSystem_tbl_AnalyticReport]
    FOREIGN KEY ([AnalyticReportID])
    REFERENCES [dbo].[tbl_AnalyticReport]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportSystem_tbl_AnalyticReport'
CREATE INDEX [IX_FK_tbl_AnalyticReportSystem_tbl_AnalyticReport]
ON [dbo].[tbl_AnalyticReportSystem]
    ([AnalyticReportID]);
GO

-- Creating foreign key on [AnalyticReportID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport]
    FOREIGN KEY ([AnalyticReportID])
    REFERENCES [dbo].[tbl_AnalyticReport]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport'
CREATE INDEX [IX_FK_tbl_AnalyticReportUserSetting_tbl_AnalyticReport]
ON [dbo].[tbl_AnalyticReportUserSettings]
    ([AnalyticReportID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_AnalyticReportUserSettings'
ALTER TABLE [dbo].[tbl_AnalyticReportUserSettings]
ADD CONSTRAINT [FK_tbl_AnalyticReportUserSetting_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AnalyticReportUserSetting_tbl_User'
CREATE INDEX [IX_FK_tbl_AnalyticReportUserSetting_tbl_User]
ON [dbo].[tbl_AnalyticReportUserSettings]
    ([UserID]);
GO

-- Creating foreign key on [CityID] in table 'tbl_Bank'
ALTER TABLE [dbo].[tbl_Bank]
ADD CONSTRAINT [FK_tbl_Bank_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Bank_tbl_City'
CREATE INDEX [IX_FK_tbl_Bank_tbl_City]
ON [dbo].[tbl_Bank]
    ([CityID]);
GO

-- Creating foreign key on [BankID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Bank]
    FOREIGN KEY ([BankID])
    REFERENCES [dbo].[tbl_Bank]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Bank'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Bank]
ON [dbo].[tbl_CompanyLegalAccount]
    ([BankID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Brand'
ALTER TABLE [dbo].[tbl_Brand]
ADD CONSTRAINT [FK_tbl_Brand_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Brand_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Brand_tbl_Sites]
ON [dbo].[tbl_Brand]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Browsers'
ALTER TABLE [dbo].[tbl_Browsers]
ADD CONSTRAINT [FK_tbl_Browsers_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Browsers_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Browsers_tbl_Sites]
ON [dbo].[tbl_Browsers]
    ([SiteID]);
GO

-- Creating foreign key on [BrowserID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Browsers]
    FOREIGN KEY ([BrowserID])
    REFERENCES [dbo].[tbl_Browsers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Browsers'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Browsers]
ON [dbo].[tbl_ContactSessions]
    ([BrowserID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_Country'
CREATE INDEX [IX_FK_tbl_City_tbl_Country]
ON [dbo].[tbl_City]
    ([CountryID]);
GO

-- Creating foreign key on [DistrictID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_District]
    FOREIGN KEY ([DistrictID])
    REFERENCES [dbo].[tbl_District]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_District'
CREATE INDEX [IX_FK_tbl_City_tbl_District]
ON [dbo].[tbl_City]
    ([DistrictID]);
GO

-- Creating foreign key on [RegionID] in table 'tbl_City'
ALTER TABLE [dbo].[tbl_City]
ADD CONSTRAINT [FK_tbl_City_tbl_Region]
    FOREIGN KEY ([RegionID])
    REFERENCES [dbo].[tbl_Region]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_City_tbl_Region'
CREATE INDEX [IX_FK_tbl_City_tbl_Region]
ON [dbo].[tbl_City]
    ([RegionID]);
GO

-- Creating foreign key on [CityID] in table 'tbl_CityIP'
ALTER TABLE [dbo].[tbl_CityIP]
ADD CONSTRAINT [FK_tbl_CityIP_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CityIP_tbl_City'
CREATE INDEX [IX_FK_tbl_CityIP_tbl_City]
ON [dbo].[tbl_CityIP]
    ([CityID]);
GO

-- Creating foreign key on [CityID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_City]
    FOREIGN KEY ([CityID])
    REFERENCES [dbo].[tbl_City]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_City'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_City]
ON [dbo].[tbl_ContactSessions]
    ([CityID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ColumnCategories'
ALTER TABLE [dbo].[tbl_ColumnCategories]
ADD CONSTRAINT [FK_tbl_ColumnCategories_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ColumnCategories_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ColumnCategories_tbl_Sites]
ON [dbo].[tbl_ColumnCategories]
    ([SiteID]);
GO

-- Creating foreign key on [CategoryID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnCategories]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[tbl_ColumnCategories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_ColumnCategories'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_ColumnCategories]
ON [dbo].[tbl_SiteColumns]
    ([CategoryID]);
GO

-- Creating foreign key on [ColumnTypesID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypes]
    FOREIGN KEY ([ColumnTypesID])
    REFERENCES [dbo].[tbl_ColumnTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ColumnTypesExpression_tbl_ColumnTypes'
CREATE INDEX [IX_FK_tbl_ColumnTypesExpression_tbl_ColumnTypes]
ON [dbo].[tbl_ColumnTypesExpression]
    ([ColumnTypesID]);
GO

-- Creating foreign key on [TypeID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_ColumnTypes]
    FOREIGN KEY ([TypeID])
    REFERENCES [dbo].[tbl_ColumnTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_ColumnTypes'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_ColumnTypes]
ON [dbo].[tbl_SiteColumns]
    ([TypeID]);
GO

-- Creating foreign key on [ID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypesExpression]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[tbl_ColumnTypesExpression]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CompanySectorID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanySector]
    FOREIGN KEY ([CompanySectorID])
    REFERENCES [dbo].[tbl_CompanySector]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanySector'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanySector]
ON [dbo].[tbl_Company]
    ([CompanySectorID]);
GO

-- Creating foreign key on [CompanySizeID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanySize]
    FOREIGN KEY ([CompanySizeID])
    REFERENCES [dbo].[tbl_CompanySize]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanySize'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanySize]
ON [dbo].[tbl_Company]
    ([CompanySizeID]);
GO

-- Creating foreign key on [CompanyTypeID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_CompanyType]
    FOREIGN KEY ([CompanyTypeID])
    REFERENCES [dbo].[tbl_CompanyType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_CompanyType'
CREATE INDEX [IX_FK_tbl_Company_tbl_CompanyType]
ON [dbo].[tbl_Company]
    ([CompanyTypeID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Company_tbl_Contact]
ON [dbo].[tbl_Company]
    ([OwnerID]);
GO

-- Creating foreign key on [PriorityID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Priorities]
    FOREIGN KEY ([PriorityID])
    REFERENCES [dbo].[tbl_Priorities]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Priorities'
CREATE INDEX [IX_FK_tbl_Company_tbl_Priorities]
ON [dbo].[tbl_Company]
    ([PriorityID]);
GO

-- Creating foreign key on [ReadyToSellID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_ReadyToSell]
    FOREIGN KEY ([ReadyToSellID])
    REFERENCES [dbo].[tbl_ReadyToSell]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_ReadyToSell'
CREATE INDEX [IX_FK_tbl_Company_tbl_ReadyToSell]
ON [dbo].[tbl_Company]
    ([ReadyToSellID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Company_tbl_Sites]
ON [dbo].[tbl_Company]
    ([SiteID]);
GO

-- Creating foreign key on [StatusID] in table 'tbl_Company'
ALTER TABLE [dbo].[tbl_Company]
ADD CONSTRAINT [FK_tbl_Company_tbl_Status]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_Status]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Company_tbl_Status'
CREATE INDEX [IX_FK_tbl_Company_tbl_Status]
ON [dbo].[tbl_Company]
    ([StatusID]);
GO

-- Creating foreign key on [CompanyID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Company'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Company]
ON [dbo].[tbl_CompanyLegalAccount]
    ([CompanyID]);
GO

-- Creating foreign key on [CompanyID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Company'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Company]
ON [dbo].[tbl_Contact]
    ([CompanyID]);
GO

-- Creating foreign key on [ClientID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [FK_tbl_Contract_tbl_Company]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contract_tbl_Company'
CREATE INDEX [IX_FK_tbl_Contract_tbl_Company]
ON [dbo].[tbl_Contract]
    ([ClientID]);
GO

-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Company_Buyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Company_Buyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Company_Buyer]
ON [dbo].[tbl_Invoice]
    ([BuyerCompanyID]);
GO

-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Company_Executor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Company_Executor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Company_Executor]
ON [dbo].[tbl_Invoice]
    ([ExecutorCompanyID]);
GO

-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_CompanyBuyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_CompanyBuyer'
CREATE INDEX [IX_FK_tbl_Order_tbl_CompanyBuyer]
ON [dbo].[tbl_Order]
    ([BuyerCompanyID]);
GO

-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_CompanyExecutor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_CompanyExecutor'
CREATE INDEX [IX_FK_tbl_Order_tbl_CompanyExecutor]
ON [dbo].[tbl_Order]
    ([ExecutorCompanyID]);
GO

-- Creating foreign key on [PayerID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Company]
    FOREIGN KEY ([PayerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Company'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Company]
ON [dbo].[tbl_Payment]
    ([PayerID]);
GO

-- Creating foreign key on [RecipientID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Company1]
    FOREIGN KEY ([RecipientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Company1'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Company1]
ON [dbo].[tbl_Payment]
    ([RecipientID]);
GO

-- Creating foreign key on [PayerID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company]
    FOREIGN KEY ([PayerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Company'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Company]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PayerID]);
GO

-- Creating foreign key on [RecipientID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Company1]
    FOREIGN KEY ([RecipientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Company1'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Company1]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([RecipientID]);
GO

-- Creating foreign key on [SupplierID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Company]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Company'
CREATE INDEX [IX_FK_tbl_Product_tbl_Company]
ON [dbo].[tbl_Product]
    ([SupplierID]);
GO

-- Creating foreign key on [ManufacturerID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Company1]
    FOREIGN KEY ([ManufacturerID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Company1'
CREATE INDEX [IX_FK_tbl_Product_tbl_Company1]
ON [dbo].[tbl_Product]
    ([ManufacturerID]);
GO

-- Creating foreign key on [SupplierID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_Company]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_Company'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_Company]
ON [dbo].[tbl_ProductPrice]
    ([SupplierID]);
GO

-- Creating foreign key on [SupplierID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_Product]
    FOREIGN KEY ([SupplierID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_Product'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_Product]
ON [dbo].[tbl_ProductPrice]
    ([SupplierID]);
GO

-- Creating foreign key on [AccessCompanyID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Company]
    FOREIGN KEY ([AccessCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Company'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Company]
ON [dbo].[tbl_Publication]
    ([AccessCompanyID]);
GO

-- Creating foreign key on [CompanyID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Company'
CREATE INDEX [IX_FK_tbl_Request_tbl_Company]
ON [dbo].[tbl_Request]
    ([CompanyID]);
GO

-- Creating foreign key on [CompanyID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Company'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Company]
ON [dbo].[tbl_Requirement]
    ([CompanyID]);
GO

-- Creating foreign key on [ClientID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_Company]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_Company'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_Company]
ON [dbo].[tbl_ServiceLevelClient]
    ([ClientID]);
GO

-- Creating foreign key on [BuyerCompanyID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Company_Buyer]
    FOREIGN KEY ([BuyerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Company_Buyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Company_Buyer]
ON [dbo].[tbl_Shipment]
    ([BuyerCompanyID]);
GO

-- Creating foreign key on [ExecutorCompanyID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Company_Executor]
    FOREIGN KEY ([ExecutorCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Company_Executor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Company_Executor]
ON [dbo].[tbl_Shipment]
    ([ExecutorCompanyID]);
GO

-- Creating foreign key on [PayerCompanyID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_Company]
    FOREIGN KEY ([PayerCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_Company'
CREATE INDEX [IX_FK_tbl_Sites_tbl_Company]
ON [dbo].[tbl_Sites]
    ([PayerCompanyID]);
GO

-- Creating foreign key on [MainMemberCompanyID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_CompanyMainMember]
    FOREIGN KEY ([MainMemberCompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_CompanyMainMember'
CREATE INDEX [IX_FK_tbl_Task_tbl_CompanyMainMember]
ON [dbo].[tbl_Task]
    ([MainMemberCompanyID]);
GO

-- Creating foreign key on [ContractorID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Company]
    FOREIGN KEY ([ContractorID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Company'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Company]
ON [dbo].[tbl_TaskMember]
    ([ContractorID]);
GO

-- Creating foreign key on [AccountantID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant]
    FOREIGN KEY ([AccountantID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Contact_Accountant]
ON [dbo].[tbl_CompanyLegalAccount]
    ([AccountantID]);
GO

-- Creating foreign key on [HeadID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Contact_Head]
    FOREIGN KEY ([HeadID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Contact_Head'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Contact_Head]
ON [dbo].[tbl_CompanyLegalAccount]
    ([HeadID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_CompanyLegalAccount'
ALTER TABLE [dbo].[tbl_CompanyLegalAccount]
ADD CONSTRAINT [FK_tbl_CompanyLegalAccount_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyLegalAccount_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanyLegalAccount_tbl_Sites]
ON [dbo].[tbl_CompanyLegalAccount]
    ([SiteID]);
GO

-- Creating foreign key on [BuyerCompanyLegalAccountID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer]
    FOREIGN KEY ([BuyerCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_CompanyLegalAccountBuyer]
ON [dbo].[tbl_Invoice]
    ([BuyerCompanyLegalAccountID]);
GO

-- Creating foreign key on [ExecutorCompanyLegalAccountID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor]
    FOREIGN KEY ([ExecutorCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_CompanyLegalAccountExecutor]
ON [dbo].[tbl_Invoice]
    ([ExecutorCompanyLegalAccountID]);
GO

-- Creating foreign key on [PayerLegalAccountID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount]
    FOREIGN KEY ([PayerLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_CompanyLegalAccount'
CREATE INDEX [IX_FK_tbl_Payment_tbl_CompanyLegalAccount]
ON [dbo].[tbl_Payment]
    ([PayerLegalAccountID]);
GO

-- Creating foreign key on [RecipientLegalAccountID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_CompanyLegalAccount1]
    FOREIGN KEY ([RecipientLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_CompanyLegalAccount1'
CREATE INDEX [IX_FK_tbl_Payment_tbl_CompanyLegalAccount1]
ON [dbo].[tbl_Payment]
    ([RecipientLegalAccountID]);
GO

-- Creating foreign key on [PayerLegalAccountID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount]
    FOREIGN KEY ([PayerLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PayerLegalAccountID]);
GO

-- Creating foreign key on [RecipientLegalAccountID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1]
    FOREIGN KEY ([RecipientLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_CompanyLegalAccount1]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([RecipientLegalAccountID]);
GO

-- Creating foreign key on [BuyerCompanyLegalAccountID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer]
    FOREIGN KEY ([BuyerCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_CompanyLegalAccountBuyer]
ON [dbo].[tbl_Shipment]
    ([BuyerCompanyLegalAccountID]);
GO

-- Creating foreign key on [ExecutorCompanyLegalAccountID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor]
    FOREIGN KEY ([ExecutorCompanyLegalAccountID])
    REFERENCES [dbo].[tbl_CompanyLegalAccount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_CompanyLegalAccountExecutor]
ON [dbo].[tbl_Shipment]
    ([ExecutorCompanyLegalAccountID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_CompanySector'
ALTER TABLE [dbo].[tbl_CompanySector]
ADD CONSTRAINT [FK_tbl_CompanySector_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanySector_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanySector_tbl_Sites]
ON [dbo].[tbl_CompanySector]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_CompanySize'
ALTER TABLE [dbo].[tbl_CompanySize]
ADD CONSTRAINT [FK_tbl_CompanySize_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanySize_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanySize_tbl_Sites]
ON [dbo].[tbl_CompanySize]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_CompanyType'
ALTER TABLE [dbo].[tbl_CompanyType]
ADD CONSTRAINT [FK_tbl_CompanyType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CompanyType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_CompanyType_tbl_Sites]
ON [dbo].[tbl_CompanyType]
    ([SiteID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Contact]
ON [dbo].[tbl_Contact]
    ([OwnerID]);
GO

-- Creating foreign key on [ContactFunctionInCompanyID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactFunctionInCompany]
    FOREIGN KEY ([ContactFunctionInCompanyID])
    REFERENCES [dbo].[tbl_ContactFunctionInCompany]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactFunctionInCompany'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactFunctionInCompany]
ON [dbo].[tbl_Contact]
    ([ContactFunctionInCompanyID]);
GO

-- Creating foreign key on [ContactJobLevelID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactJobLevel]
    FOREIGN KEY ([ContactJobLevelID])
    REFERENCES [dbo].[tbl_ContactJobLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactJobLevel'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactJobLevel]
ON [dbo].[tbl_Contact]
    ([ContactJobLevelID]);
GO

-- Creating foreign key on [ContactTypeID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ContactType]
    FOREIGN KEY ([ContactTypeID])
    REFERENCES [dbo].[tbl_ContactType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ContactType'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ContactType]
ON [dbo].[tbl_Contact]
    ([ContactTypeID]);
GO

-- Creating foreign key on [PriorityID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Priorities]
    FOREIGN KEY ([PriorityID])
    REFERENCES [dbo].[tbl_Priorities]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Priorities'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Priorities]
ON [dbo].[tbl_Contact]
    ([PriorityID]);
GO

-- Creating foreign key on [ReadyToSellID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_ReadyToSell]
    FOREIGN KEY ([ReadyToSellID])
    REFERENCES [dbo].[tbl_ReadyToSell]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_ReadyToSell'
CREATE INDEX [IX_FK_tbl_Contact_tbl_ReadyToSell]
ON [dbo].[tbl_Contact]
    ([ReadyToSellID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Sites]
ON [dbo].[tbl_Contact]
    ([SiteID]);
GO

-- Creating foreign key on [StatusID] in table 'tbl_Contact'
ALTER TABLE [dbo].[tbl_Contact]
ADD CONSTRAINT [FK_tbl_Contact_tbl_Status]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_Status]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contact_tbl_Status'
CREATE INDEX [IX_FK_tbl_Contact_tbl_Status]
ON [dbo].[tbl_Contact]
    ([StatusID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_Contact]
ON [dbo].[tbl_ContactActivity]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_Contact]
ON [dbo].[tbl_ContactColumnValues]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactCommunication'
ALTER TABLE [dbo].[tbl_ContactCommunication]
ADD CONSTRAINT [FK_tbl_ContactCommunication_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactCommunication_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactCommunication_tbl_Contact]
ON [dbo].[tbl_ContactCommunication]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactScore_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactScore_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactScore_tbl_Contact]
ON [dbo].[tbl_ContactActivityScore]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Contact]
ON [dbo].[tbl_ContactSessions]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_Contact]
ON [dbo].[tbl_ContactToContactRole]
    ([ContactID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact]
ON [dbo].[tbl_Invoice]
    ([OwnerID]);
GO

-- Creating foreign key on [BuyerContactID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Buyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact_Buyer'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact_Buyer]
ON [dbo].[tbl_Invoice]
    ([BuyerContactID]);
GO

-- Creating foreign key on [ExecutorContactID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Contact_Executor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Contact_Executor'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Contact_Executor]
ON [dbo].[tbl_Invoice]
    ([ExecutorContactID]);
GO

-- Creating foreign key on [AuthorID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_InvoiceHistory_tbl_Contact]
ON [dbo].[tbl_InvoiceHistory]
    ([AuthorID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_Contact]
ON [dbo].[tbl_MassMailContact]
    ([ContactID]);
GO

-- Creating foreign key on [BuyerContactID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_ContactBuyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_ContactBuyer'
CREATE INDEX [IX_FK_tbl_Order_tbl_ContactBuyer]
ON [dbo].[tbl_Order]
    ([BuyerContactID]);
GO

-- Creating foreign key on [ExecutorContactID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_ContactExecutor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_ContactExecutor'
CREATE INDEX [IX_FK_tbl_Order_tbl_ContactExecutor]
ON [dbo].[tbl_Order]
    ([ExecutorContactID]);
GO

-- Creating foreign key on [AuthorID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Contact]
ON [dbo].[tbl_Publication]
    ([AuthorID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [FK_tbl_PublicationComment_tbl_Contact]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationComment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_PublicationComment_tbl_Contact]
ON [dbo].[tbl_PublicationComment]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_Contact]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_Contact'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_Contact]
ON [dbo].[tbl_PublicationMark]
    ([UserID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [FK_tbl_Reminder_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Reminder_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Reminder_tbl_Contact]
ON [dbo].[tbl_Reminder]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact]
ON [dbo].[tbl_Request]
    ([ContactID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact_Owner]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact_Owner'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact_Owner]
ON [dbo].[tbl_Request]
    ([OwnerID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_Request_tbl_Contact_Responsible]
ON [dbo].[tbl_Request]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Contact]
ON [dbo].[tbl_RequestHistory]
    ([ContactID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Contact_Responsible]
ON [dbo].[tbl_RequestHistory]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact]
ON [dbo].[tbl_Requirement]
    ([ContactID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Owner]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact_Owner'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact_Owner]
ON [dbo].[tbl_Requirement]
    ([OwnerID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact_Responsible]
ON [dbo].[tbl_Requirement]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_Contact]
ON [dbo].[tbl_RequirementHistory]
    ([ContactID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_ResponsibleContact]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_ResponsibleContact'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_ResponsibleContact]
ON [dbo].[tbl_RequirementHistory]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Contact]
ON [dbo].[tbl_Responsible]
    ([ContactID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Contact1]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Contact1'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Contact1]
ON [dbo].[tbl_Responsible]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_Contact]
ON [dbo].[tbl_ServiceLevelContact]
    ([ContactID]);
GO

-- Creating foreign key on [OwnerID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact]
ON [dbo].[tbl_Shipment]
    ([OwnerID]);
GO

-- Creating foreign key on [BuyerContactID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Buyer]
    FOREIGN KEY ([BuyerContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact_Buyer'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact_Buyer]
ON [dbo].[tbl_Shipment]
    ([BuyerContactID]);
GO

-- Creating foreign key on [ExecutorContactID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Contact_Executor]
    FOREIGN KEY ([ExecutorContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Contact_Executor'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Contact_Executor]
ON [dbo].[tbl_Shipment]
    ([ExecutorContactID]);
GO

-- Creating foreign key on [AuthorID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Contact]
    FOREIGN KEY ([AuthorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_Contact]
ON [dbo].[tbl_ShipmentHistory]
    ([AuthorID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Contact]
ON [dbo].[tbl_SiteAction]
    ([ContactID]);
GO

-- Creating foreign key on [SenderID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Contact_Sender]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Contact_Sender'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Contact_Sender]
ON [dbo].[tbl_SiteAction]
    ([SenderID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_Contact]
ON [dbo].[tbl_SiteActionLink]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateRecipient_tbl_Contact'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateRecipient_tbl_Contact]
ON [dbo].[tbl_SiteActionTemplateRecipient]
    ([ContactID]);
GO

-- Creating foreign key on [ReceiverContactID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_ReceiverContact]
    FOREIGN KEY ([ReceiverContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_ReceiverContact'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_ReceiverContact]
ON [dbo].[tbl_SourceMonitoring]
    ([ReceiverContactID]);
GO

-- Creating foreign key on [CreatorID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactCreator]
    FOREIGN KEY ([CreatorID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactCreator'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactCreator]
ON [dbo].[tbl_Task]
    ([CreatorID]);
GO

-- Creating foreign key on [MainMemberContactID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactMainMember]
    FOREIGN KEY ([MainMemberContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactMainMember'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactMainMember]
ON [dbo].[tbl_Task]
    ([MainMemberContactID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_ContactResponsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_ContactResponsible'
CREATE INDEX [IX_FK_tbl_Task_tbl_ContactResponsible]
ON [dbo].[tbl_Task]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [FK_tbl_TaskDuration_tbl_ContactResponsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskDuration_tbl_ContactResponsible'
CREATE INDEX [IX_FK_tbl_TaskDuration_tbl_ContactResponsible]
ON [dbo].[tbl_TaskDuration]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ResponsibleID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_Contact]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_Contact]
ON [dbo].[tbl_TaskHistory]
    ([ResponsibleID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Contact]
ON [dbo].[tbl_TaskMember]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskPersonalComment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskPersonalComment_tbl_Contact]
ON [dbo].[tbl_TaskPersonalComment]
    ([ContactID]);
GO

-- Creating foreign key on [ContactID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [FK_tbl_User_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_User_tbl_Contact'
CREATE INDEX [IX_FK_tbl_User_tbl_Contact]
ON [dbo].[tbl_User]
    ([ContactID]);
GO

-- Creating foreign key on [ContactSessionID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_ContactSessions]
    FOREIGN KEY ([ContactSessionID])
    REFERENCES [dbo].[tbl_ContactSessions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_ContactSessions'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_ContactSessions]
ON [dbo].[tbl_ContactActivity]
    ([ContactSessionID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_Sites]
ON [dbo].[tbl_ContactActivity]
    ([SiteID]);
GO

-- Creating foreign key on [SourceMonitoringID] in table 'tbl_ContactActivity'
ALTER TABLE [dbo].[tbl_ContactActivity]
ADD CONSTRAINT [FK_tbl_ContactActivity_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivity_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_ContactActivity_tbl_SourceMonitoring]
ON [dbo].[tbl_ContactActivity]
    ([SourceMonitoringID]);
GO

-- Creating foreign key on [SiteActivityScoreTypeID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType]
    FOREIGN KEY ([SiteActivityScoreTypeID])
    REFERENCES [dbo].[tbl_SiteActivityScoreType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType'
CREATE INDEX [IX_FK_tbl_ContactActivityScore_tbl_SiteActivityScoreType]
ON [dbo].[tbl_ContactActivityScore]
    ([SiteActivityScoreTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactActivityScore'
ALTER TABLE [dbo].[tbl_ContactActivityScore]
ADD CONSTRAINT [FK_tbl_ContactActivityScore_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScore_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactActivityScore_tbl_Sites]
ON [dbo].[tbl_ContactActivityScore]
    ([SiteID]);
GO

-- Creating foreign key on [ContactActivityScoreID] in table 'tbl_ContactActivityScoreHistory'
ALTER TABLE [dbo].[tbl_ContactActivityScoreHistory]
ADD CONSTRAINT [FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore]
    FOREIGN KEY ([ContactActivityScoreID])
    REFERENCES [dbo].[tbl_ContactActivityScore]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore'
CREATE INDEX [IX_FK_tbl_ContactActivityScoreHistory_tbl_ContactActivityScore]
ON [dbo].[tbl_ContactActivityScoreHistory]
    ([ContactActivityScoreID]);
GO

-- Creating foreign key on [SiteColumnID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_SiteColumns]
ON [dbo].[tbl_ContactColumnValues]
    ([SiteColumnID]);
GO

-- Creating foreign key on [SiteColumnValueID] in table 'tbl_ContactColumnValues'
ALTER TABLE [dbo].[tbl_ContactColumnValues]
ADD CONSTRAINT [FK_tbl_ContactColumnValues_tbl_SiteColumnValues]
    FOREIGN KEY ([SiteColumnValueID])
    REFERENCES [dbo].[tbl_SiteColumnValues]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactColumnValues_tbl_SiteColumnValues'
CREATE INDEX [IX_FK_tbl_ContactColumnValues_tbl_SiteColumnValues]
ON [dbo].[tbl_ContactColumnValues]
    ([SiteColumnValueID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactFunctionInCompany'
ALTER TABLE [dbo].[tbl_ContactFunctionInCompany]
ADD CONSTRAINT [FK_tbl_ContactFunctionInCompany_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactFunctionInCompany_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactFunctionInCompany_tbl_Sites]
ON [dbo].[tbl_ContactFunctionInCompany]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactJobLevel'
ALTER TABLE [dbo].[tbl_ContactJobLevel]
ADD CONSTRAINT [FK_tbl_ContactJobLevel_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactJobLevel_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactJobLevel_tbl_Sites]
ON [dbo].[tbl_ContactJobLevel]
    ([SiteID]);
GO

-- Creating foreign key on [RoleTypeID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [FK_tbl_ContactRole_tbl_ContactRoleType]
    FOREIGN KEY ([RoleTypeID])
    REFERENCES [dbo].[tbl_ContactRoleType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactRole_tbl_ContactRoleType'
CREATE INDEX [IX_FK_tbl_ContactRole_tbl_ContactRoleType]
ON [dbo].[tbl_ContactRole]
    ([RoleTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [FK_tbl_ContactRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactRole_tbl_Sites]
ON [dbo].[tbl_ContactRole]
    ([SiteID]);
GO

-- Creating foreign key on [ContactRoleID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_ContactRole]
    FOREIGN KEY ([ContactRoleID])
    REFERENCES [dbo].[tbl_ContactRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_ContactRole'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_ContactRole]
ON [dbo].[tbl_ContactToContactRole]
    ([ContactRoleID]);
GO

-- Creating foreign key on [ContactRoleID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_ContactRole]
    FOREIGN KEY ([ContactRoleID])
    REFERENCES [dbo].[tbl_ContactRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_ContactRole'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_ContactRole]
ON [dbo].[tbl_Responsible]
    ([ContactRoleID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Country'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Country]
ON [dbo].[tbl_ContactSessions]
    ([CountryID]);
GO

-- Creating foreign key on [MobileDeviceID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_MobileDevices]
    FOREIGN KEY ([MobileDeviceID])
    REFERENCES [dbo].[tbl_MobileDevices]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_MobileDevices'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_MobileDevices]
ON [dbo].[tbl_ContactSessions]
    ([MobileDeviceID]);
GO

-- Creating foreign key on [OperatingSystemID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_OperatingSystems]
    FOREIGN KEY ([OperatingSystemID])
    REFERENCES [dbo].[tbl_OperatingSystems]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_OperatingSystems'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_OperatingSystems]
ON [dbo].[tbl_ContactSessions]
    ([OperatingSystemID]);
GO

-- Creating foreign key on [ResolutionID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Resolutions]
    FOREIGN KEY ([ResolutionID])
    REFERENCES [dbo].[tbl_Resolutions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Resolutions'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Resolutions]
ON [dbo].[tbl_ContactSessions]
    ([ResolutionID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactSessions'
ALTER TABLE [dbo].[tbl_ContactSessions]
ADD CONSTRAINT [FK_tbl_ContactSessions_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactSessions_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactSessions_tbl_Sites]
ON [dbo].[tbl_ContactSessions]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactToContactRole'
ALTER TABLE [dbo].[tbl_ContactToContactRole]
ADD CONSTRAINT [FK_tbl_ContactToContactRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactToContactRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactToContactRole_tbl_Sites]
ON [dbo].[tbl_ContactToContactRole]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ContactType'
ALTER TABLE [dbo].[tbl_ContactType]
ADD CONSTRAINT [FK_tbl_ContactType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactType_tbl_Sites]
ON [dbo].[tbl_ContactType]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [FK_tbl_Contract_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contract_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Contract_tbl_Sites]
ON [dbo].[tbl_Contract]
    ([SiteID]);
GO

-- Creating foreign key on [ContractID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contract]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[tbl_Contract]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contract'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contract]
ON [dbo].[tbl_Requirement]
    ([ContractID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_CountryIP'
ALTER TABLE [dbo].[tbl_CountryIP]
ADD CONSTRAINT [FK_tbl_CountryIP_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CountryIP_tbl_Country'
CREATE INDEX [IX_FK_tbl_CountryIP_tbl_Country]
ON [dbo].[tbl_CountryIP]
    ([CountryID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [FK_tbl_District_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_District_tbl_Country'
CREATE INDEX [IX_FK_tbl_District_tbl_Country]
ON [dbo].[tbl_District]
    ([CountryID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Country'
CREATE INDEX [IX_FK_tbl_Product_tbl_Country]
ON [dbo].[tbl_Product]
    ([CountryID]);
GO

-- Creating foreign key on [CountryID] in table 'tbl_Region'
ALTER TABLE [dbo].[tbl_Region]
ADD CONSTRAINT [FK_tbl_Region_tbl_Country]
    FOREIGN KEY ([CountryID])
    REFERENCES [dbo].[tbl_Country]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Region_tbl_Country'
CREATE INDEX [IX_FK_tbl_Region_tbl_Country]
ON [dbo].[tbl_Region]
    ([CountryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Currency'
ALTER TABLE [dbo].[tbl_Currency]
ADD CONSTRAINT [FK_tbl_Currency_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Currency_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Currency_tbl_Sites]
ON [dbo].[tbl_Currency]
    ([SiteID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_CurrencyCourse'
ALTER TABLE [dbo].[tbl_CurrencyCourse]
ADD CONSTRAINT [FK_tbl_CurrencyCourse_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_CurrencyCourse_tbl_Currency'
CREATE INDEX [IX_FK_tbl_CurrencyCourse_tbl_Currency]
ON [dbo].[tbl_CurrencyCourse]
    ([CurrencyID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Currency]
ON [dbo].[tbl_InvoiceProducts]
    ([CurrencyID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Currency]
ON [dbo].[tbl_OrderProducts]
    ([CurrencyID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Currency'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Currency]
ON [dbo].[tbl_Payment]
    ([CurrencyID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Currency'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Currency]
ON [dbo].[tbl_Requirement]
    ([CurrencyID]);
GO

-- Creating foreign key on [CurrencyID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Currency'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Currency]
ON [dbo].[tbl_ShipmentProducts]
    ([CurrencyID]);
GO

-- Creating foreign key on [DictionaryGroupID] in table 'tbl_Dictionary'
ALTER TABLE [dbo].[tbl_Dictionary]
ADD CONSTRAINT [FK_tbl_Dictionary_tbl_DictionaryGroup]
    FOREIGN KEY ([DictionaryGroupID])
    REFERENCES [dbo].[tbl_DictionaryGroup]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Dictionary_tbl_DictionaryGroup'
CREATE INDEX [IX_FK_tbl_Dictionary_tbl_DictionaryGroup]
ON [dbo].[tbl_Dictionary]
    ([DictionaryGroupID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_DictionaryGroup'
ALTER TABLE [dbo].[tbl_DictionaryGroup]
ADD CONSTRAINT [FK_tbl_DictionaryGroup_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_DictionaryGroup_tbl_Module'
CREATE INDEX [IX_FK_tbl_DictionaryGroup_tbl_Module]
ON [dbo].[tbl_DictionaryGroup]
    ([ModuleID]);
GO

-- Creating foreign key on [DirectionID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Direction]
    FOREIGN KEY ([DirectionID])
    REFERENCES [dbo].[tbl_Direction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Direction'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Direction]
ON [dbo].[tbl_InvoiceType]
    ([DirectionID]);
GO

-- Creating foreign key on [RegionID] in table 'tbl_District'
ALTER TABLE [dbo].[tbl_District]
ADD CONSTRAINT [FK_tbl_District_tbl_Region]
    FOREIGN KEY ([RegionID])
    REFERENCES [dbo].[tbl_Region]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_District_tbl_Region'
CREATE INDEX [IX_FK_tbl_District_tbl_Region]
ON [dbo].[tbl_District]
    ([RegionID]);
GO

-- Creating foreign key on [ServiceAdvertisingActionID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_EmailActions]
    FOREIGN KEY ([ServiceAdvertisingActionID])
    REFERENCES [dbo].[tbl_EmailActions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_EmailActions'
CREATE INDEX [IX_FK_tbl_Sites_tbl_EmailActions]
ON [dbo].[tbl_Sites]
    ([ServiceAdvertisingActionID]);
GO

-- Creating foreign key on [UnsubscribeActionID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_EmailActions1]
    FOREIGN KEY ([UnsubscribeActionID])
    REFERENCES [dbo].[tbl_EmailActions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_EmailActions1'
CREATE INDEX [IX_FK_tbl_Sites_tbl_EmailActions1]
ON [dbo].[tbl_Sites]
    ([UnsubscribeActionID]);
GO

-- Creating foreign key on [EmailStatsID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_EmailStats]
    FOREIGN KEY ([EmailStatsID])
    REFERENCES [dbo].[tbl_EmailStats]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SiteID] in table 'tbl_EmailStatsUnsubscribe'
ALTER TABLE [dbo].[tbl_EmailStatsUnsubscribe]
ADD CONSTRAINT [FK_tbl_EmailStatsUnsubscribe_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_EmailStatsUnsubscribe_tbl_Sites'
CREATE INDEX [IX_FK_tbl_EmailStatsUnsubscribe_tbl_Sites]
ON [dbo].[tbl_EmailStatsUnsubscribe]
    ([SiteID]);
GO

-- Creating foreign key on [SourceMonitoringID] in table 'tbl_EmailToAnalysis'
ALTER TABLE [dbo].[tbl_EmailToAnalysis]
ADD CONSTRAINT [FK_tbl_EmailToAnalysis_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_EmailToAnalysis_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_EmailToAnalysis_tbl_SourceMonitoring]
ON [dbo].[tbl_EmailToAnalysis]
    ([SourceMonitoringID]);
GO

-- Creating foreign key on [EventCategoryID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_EventCategories]
    FOREIGN KEY ([EventCategoryID])
    REFERENCES [dbo].[tbl_EventCategories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_EventCategories'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_EventCategories]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([EventCategoryID]);
GO

-- Creating foreign key on [ExpirationActionID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [FK_tbl_OrderType_tbl_ExpirationAction]
    FOREIGN KEY ([ExpirationActionID])
    REFERENCES [dbo].[tbl_ExpirationAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderType_tbl_ExpirationAction'
CREATE INDEX [IX_FK_tbl_OrderType_tbl_ExpirationAction]
ON [dbo].[tbl_OrderType]
    ([ExpirationActionID]);
GO

-- Creating foreign key on [FormulaID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Formula]
    FOREIGN KEY ([FormulaID])
    REFERENCES [dbo].[tbl_Formula]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_Formula'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_Formula]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([FormulaID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Import'
ALTER TABLE [dbo].[tbl_Import]
ADD CONSTRAINT [FK_tbl_Import_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Import_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Import_tbl_Sites]
ON [dbo].[tbl_Import]
    ([SiteID]);
GO

-- Creating foreign key on [ImportID] in table 'tbl_ImportTag'
ALTER TABLE [dbo].[tbl_ImportTag]
ADD CONSTRAINT [FK_tbl_ImportTag_tbl_Import]
    FOREIGN KEY ([ImportID])
    REFERENCES [dbo].[tbl_Import]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ImportTag_tbl_Import'
CREATE INDEX [IX_FK_tbl_ImportTag_tbl_Import]
ON [dbo].[tbl_ImportTag]
    ([ImportID]);
GO

-- Creating foreign key on [InvoiceStatusID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_InvoiceStatus]
    FOREIGN KEY ([InvoiceStatusID])
    REFERENCES [dbo].[tbl_InvoiceStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_InvoiceStatus'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_InvoiceStatus]
ON [dbo].[tbl_Invoice]
    ([InvoiceStatusID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Order'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Order]
ON [dbo].[tbl_Invoice]
    ([OrderID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_PriceList]
ON [dbo].[tbl_Invoice]
    ([PriceListID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Invoice'
ALTER TABLE [dbo].[tbl_Invoice]
ADD CONSTRAINT [FK_tbl_Invoice_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Invoice_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Invoice_tbl_Sites]
ON [dbo].[tbl_Invoice]
    ([SiteID]);
GO

-- Creating foreign key on [ContentID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [FK_tbl_InvoiceComment_tbl_Invoice]
    FOREIGN KEY ([ContentID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceComment_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_InvoiceComment_tbl_Invoice]
ON [dbo].[tbl_InvoiceComment]
    ([ContentID]);
GO

-- Creating foreign key on [InvoiceID] in table 'tbl_InvoiceHistory'
ALTER TABLE [dbo].[tbl_InvoiceHistory]
ADD CONSTRAINT [FK_tbl_InvoiceHistory_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceHistory_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_InvoiceHistory_tbl_Invoice]
ON [dbo].[tbl_InvoiceHistory]
    ([InvoiceID]);
GO

-- Creating foreign key on [InvoiceID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Invoice]
ON [dbo].[tbl_InvoiceProducts]
    ([InvoiceID]);
GO

-- Creating foreign key on [InvoiceID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Invoice]
ON [dbo].[tbl_Payment]
    ([InvoiceID]);
GO

-- Creating foreign key on [InvoiceID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Invoice]
ON [dbo].[tbl_Requirement]
    ([InvoiceID]);
GO

-- Creating foreign key on [ReplyToID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [FK_tbl_InvoiceComment_tbl_InvoiceComment]
    FOREIGN KEY ([ReplyToID])
    REFERENCES [dbo].[tbl_InvoiceComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceComment_tbl_InvoiceComment'
CREATE INDEX [IX_FK_tbl_InvoiceComment_tbl_InvoiceComment]
ON [dbo].[tbl_InvoiceComment]
    ([ReplyToID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [FK_tbl_InvoiceComment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceComment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_InvoiceComment_tbl_Sites]
ON [dbo].[tbl_InvoiceComment]
    ([SiteID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [FK_tbl_InvoiceComment_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceComment_tbl_User'
CREATE INDEX [IX_FK_tbl_InvoiceComment_tbl_User]
ON [dbo].[tbl_InvoiceComment]
    ([UserID]);
GO

-- Creating foreign key on [DestinationUserID] in table 'tbl_InvoiceComment'
ALTER TABLE [dbo].[tbl_InvoiceComment]
ADD CONSTRAINT [FK_tbl_InvoiceComment_tbl_User_Destination]
    FOREIGN KEY ([DestinationUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceComment_tbl_User_Destination'
CREATE INDEX [IX_FK_tbl_InvoiceComment_tbl_User_Destination]
ON [dbo].[tbl_InvoiceComment]
    ([DestinationUserID]);
GO

-- Creating foreign key on [ContentCommentID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_InvoiceComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_InvoiceComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceCommentMark_tbl_InvoiceComment'
CREATE INDEX [IX_FK_tbl_InvoiceCommentMark_tbl_InvoiceComment]
ON [dbo].[tbl_InvoiceCommentMark]
    ([ContentCommentID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_InvoiceCommentMark'
ALTER TABLE [dbo].[tbl_InvoiceCommentMark]
ADD CONSTRAINT [FK_tbl_InvoiceCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_InvoiceCommentMark_tbl_User]
ON [dbo].[tbl_InvoiceCommentMark]
    ([UserID]);
GO

-- Creating foreign key on [InvoiceInformCatalogID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog]
    FOREIGN KEY ([InvoiceInformCatalogID])
    REFERENCES [dbo].[tbl_InvoiceInformCatalog]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog]
ON [dbo].[tbl_ServiceLevelContact]
    ([InvoiceInformCatalogID]);
GO

-- Creating foreign key on [InvoiceInformFormID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm]
    FOREIGN KEY ([InvoiceInformFormID])
    REFERENCES [dbo].[tbl_InvoiceInformForm]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm]
ON [dbo].[tbl_ServiceLevelContact]
    ([InvoiceInformFormID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_PriceList]
ON [dbo].[tbl_InvoiceProducts]
    ([PriceListID]);
GO

-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_InvoiceProducts]
    ([SpecialOfferPriceListID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Product]
ON [dbo].[tbl_InvoiceProducts]
    ([ProductID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Task'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Task]
ON [dbo].[tbl_InvoiceProducts]
    ([TaskID]);
GO

-- Creating foreign key on [UnitID] in table 'tbl_InvoiceProducts'
ALTER TABLE [dbo].[tbl_InvoiceProducts]
ADD CONSTRAINT [FK_tbl_InvoiceProducts_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceProducts_tbl_Unit'
CREATE INDEX [IX_FK_tbl_InvoiceProducts_tbl_Unit]
ON [dbo].[tbl_InvoiceProducts]
    ([UnitID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Numerator]
ON [dbo].[tbl_InvoiceType]
    ([NumeratorID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_InvoiceType'
ALTER TABLE [dbo].[tbl_InvoiceType]
ADD CONSTRAINT [FK_tbl_InvoiceType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_InvoiceType_tbl_Sites]
ON [dbo].[tbl_InvoiceType]
    ([SiteID]);
GO

-- Creating foreign key on [RuleTypeID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [FK_tbl_Links_tbl_RuleTypes]
    FOREIGN KEY ([RuleTypeID])
    REFERENCES [dbo].[tbl_RuleTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Links_tbl_RuleTypes'
CREATE INDEX [IX_FK_tbl_Links_tbl_RuleTypes]
ON [dbo].[tbl_Links]
    ([RuleTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Links'
ALTER TABLE [dbo].[tbl_Links]
ADD CONSTRAINT [FK_tbl_Links_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Links_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Links_tbl_Sites]
ON [dbo].[tbl_Links]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_Links]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_Links]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_Links'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_Links]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActivityRuleID]);
GO

-- Creating foreign key on [LogicConditionID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_LogicConditions]
    FOREIGN KEY ([LogicConditionID])
    REFERENCES [dbo].[tbl_LogicConditions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplates_tbl_LogicConditions'
CREATE INDEX [IX_FK_tbl_SiteEventTemplates_tbl_LogicConditions]
ON [dbo].[tbl_SiteEventTemplates]
    ([LogicConditionID]);
GO

-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [FK_tbl_MassMail_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMail_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_MassMail_tbl_SiteActionTemplate]
ON [dbo].[tbl_MassMail]
    ([SiteActionTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_MassMail'
ALTER TABLE [dbo].[tbl_MassMail]
ADD CONSTRAINT [FK_tbl_MassMail_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMail_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassMail_tbl_Sites]
ON [dbo].[tbl_MassMail]
    ([SiteID]);
GO

-- Creating foreign key on [MassMailID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_MassMail]
    FOREIGN KEY ([MassMailID])
    REFERENCES [dbo].[tbl_MassMail]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_MassMail'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_MassMail]
ON [dbo].[tbl_MassMailContact]
    ([MassMailID]);
GO

-- Creating foreign key on [SiteActionID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_SiteAction]
ON [dbo].[tbl_MassMailContact]
    ([SiteActionID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_MassMailContact'
ALTER TABLE [dbo].[tbl_MassMailContact]
ADD CONSTRAINT [FK_tbl_MassMailContact_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassMailContact_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassMailContact_tbl_Sites]
ON [dbo].[tbl_MassMailContact]
    ([SiteID]);
GO

-- Creating foreign key on [MassWorkflowTypeID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [FK_tbl_MassWorkflow_tbl_MassWorkflowType]
    FOREIGN KEY ([MassWorkflowTypeID])
    REFERENCES [dbo].[tbl_MassWorkflowType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflow_tbl_MassWorkflowType'
CREATE INDEX [IX_FK_tbl_MassWorkflow_tbl_MassWorkflowType]
ON [dbo].[tbl_MassWorkflow]
    ([MassWorkflowTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_MassWorkflow'
ALTER TABLE [dbo].[tbl_MassWorkflow]
ADD CONSTRAINT [FK_tbl_MassWorkflow_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflow_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MassWorkflow_tbl_Sites]
ON [dbo].[tbl_MassWorkflow]
    ([SiteID]);
GO

-- Creating foreign key on [MassWorkflowID] in table 'tbl_MassWorkflowContact'
ALTER TABLE [dbo].[tbl_MassWorkflowContact]
ADD CONSTRAINT [FK_tbl_MassWorkflowContact_tbl_MassWorkflow]
    FOREIGN KEY ([MassWorkflowID])
    REFERENCES [dbo].[tbl_MassWorkflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MassWorkflowContact_tbl_MassWorkflow'
CREATE INDEX [IX_FK_tbl_MassWorkflowContact_tbl_MassWorkflow]
ON [dbo].[tbl_MassWorkflowContact]
    ([MassWorkflowID]);
GO

-- Creating foreign key on [MassWorkflowID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_MassWorkflow]
    FOREIGN KEY ([MassWorkflowID])
    REFERENCES [dbo].[tbl_MassWorkflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_MassWorkflow'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_MassWorkflow]
ON [dbo].[tbl_Workflow]
    ([MassWorkflowID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Material'
ALTER TABLE [dbo].[tbl_Material]
ADD CONSTRAINT [FK_tbl_Material_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Material_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Material_tbl_Sites]
ON [dbo].[tbl_Material]
    ([SiteID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_Module'
CREATE INDEX [IX_FK_tbl_Menu_tbl_Module]
ON [dbo].[tbl_Menu]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleEditionActionID] in table 'tbl_Menu'
ALTER TABLE [dbo].[tbl_Menu]
ADD CONSTRAINT [FK_tbl_Menu_tbl_ModuleEditionAction]
    FOREIGN KEY ([ModuleEditionActionID])
    REFERENCES [dbo].[tbl_ModuleEditionAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Menu_tbl_ModuleEditionAction'
CREATE INDEX [IX_FK_tbl_Menu_tbl_ModuleEditionAction]
ON [dbo].[tbl_Menu]
    ([ModuleEditionActionID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_MobileDevices'
ALTER TABLE [dbo].[tbl_MobileDevices]
ADD CONSTRAINT [FK_tbl_MobileDevices_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MobileDevices_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MobileDevices_tbl_Sites]
ON [dbo].[tbl_MobileDevices]
    ([SiteID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_ModuleEdition'
ALTER TABLE [dbo].[tbl_ModuleEdition]
ADD CONSTRAINT [FK_tbl_ModuleEdition_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEdition_tbl_Module'
CREATE INDEX [IX_FK_tbl_ModuleEdition_tbl_Module]
ON [dbo].[tbl_ModuleEdition]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [FK_tbl_RelatedPublication_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RelatedPublication_tbl_Module'
CREATE INDEX [IX_FK_tbl_RelatedPublication_tbl_Module]
ON [dbo].[tbl_RelatedPublication]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_Reminder'
ALTER TABLE [dbo].[tbl_Reminder]
ADD CONSTRAINT [FK_tbl_Reminder_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Reminder_tbl_Module'
CREATE INDEX [IX_FK_tbl_Reminder_tbl_Module]
ON [dbo].[tbl_Reminder]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_Module'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_Module]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([ModuleID]);
GO

-- Creating foreign key on [ModuleEditionID] in table 'tbl_ModuleEditionAction'
ALTER TABLE [dbo].[tbl_ModuleEditionAction]
ADD CONSTRAINT [FK_tbl_ModuleEditionAction_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEditionAction_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_ModuleEditionAction_tbl_ModuleEdition]
ON [dbo].[tbl_ModuleEditionAction]
    ([ModuleEditionID]);
GO

-- Creating foreign key on [ModuleEditionID] in table 'tbl_ModuleEditionOption'
ALTER TABLE [dbo].[tbl_ModuleEditionOption]
ADD CONSTRAINT [FK_tbl_ModuleEditionOption_tbl_ModuleEdition]
    FOREIGN KEY ([ModuleEditionID])
    REFERENCES [dbo].[tbl_ModuleEdition]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ModuleEditionOption_tbl_ModuleEdition'
CREATE INDEX [IX_FK_tbl_ModuleEditionOption_tbl_ModuleEdition]
ON [dbo].[tbl_ModuleEditionOption]
    ([ModuleEditionID]);
GO

-- Creating foreign key on [NumeratorPeriodID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [FK_tbl_Numerator_tbl_NumeratorPeriod]
    FOREIGN KEY ([NumeratorPeriodID])
    REFERENCES [dbo].[tbl_NumeratorPeriod]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Numerator_tbl_NumeratorPeriod'
CREATE INDEX [IX_FK_tbl_Numerator_tbl_NumeratorPeriod]
ON [dbo].[tbl_Numerator]
    ([NumeratorPeriodID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Numerator'
ALTER TABLE [dbo].[tbl_Numerator]
ADD CONSTRAINT [FK_tbl_Numerator_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Numerator_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Numerator_tbl_Sites]
ON [dbo].[tbl_Numerator]
    ([SiteID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_NumeratorUsage'
ALTER TABLE [dbo].[tbl_NumeratorUsage]
ADD CONSTRAINT [FK_tbl_NumeratorUsage_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_NumeratorUsage_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_NumeratorUsage_tbl_Numerator]
ON [dbo].[tbl_NumeratorUsage]
    ([NumeratorID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_OrderType'
ALTER TABLE [dbo].[tbl_OrderType]
ADD CONSTRAINT [FK_tbl_OrderType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_OrderType_tbl_Numerator]
ON [dbo].[tbl_OrderType]
    ([NumeratorID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_Numerator]
ON [dbo].[tbl_PublicationType]
    ([NumeratorID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_Numerator]
ON [dbo].[tbl_RequestSourceType]
    ([NumeratorID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [FK_tbl_RequirementType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_RequirementType_tbl_Numerator]
ON [dbo].[tbl_RequirementType]
    ([NumeratorID]);
GO

-- Creating foreign key on [NumeratorID] in table 'tbl_ShipmentType'
ALTER TABLE [dbo].[tbl_ShipmentType]
ADD CONSTRAINT [FK_tbl_ShipmentType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_ShipmentType_tbl_Numerator]
ON [dbo].[tbl_ShipmentType]
    ([NumeratorID]);
GO

-- Creating foreign key on [ObjectTypeID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [FK_tbl_SiteTags_tbl_ObjectTypes]
    FOREIGN KEY ([ObjectTypeID])
    REFERENCES [dbo].[tbl_ObjectTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTags_tbl_ObjectTypes'
CREATE INDEX [IX_FK_tbl_SiteTags_tbl_ObjectTypes]
ON [dbo].[tbl_SiteTags]
    ([ObjectTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_OperatingSystems'
ALTER TABLE [dbo].[tbl_OperatingSystems]
ADD CONSTRAINT [FK_tbl_OperatingSystems_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OperatingSystems_tbl_Sites'
CREATE INDEX [IX_FK_tbl_OperatingSystems_tbl_Sites]
ON [dbo].[tbl_OperatingSystems]
    ([SiteID]);
GO

-- Creating foreign key on [OperationID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Operations]
    FOREIGN KEY ([OperationID])
    REFERENCES [dbo].[tbl_Operations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_Operations'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_Operations]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([OperationID]);
GO

-- Creating foreign key on [OrderStatusID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_OrderStatus]
    FOREIGN KEY ([OrderStatusID])
    REFERENCES [dbo].[tbl_OrderStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_OrderStatus'
CREATE INDEX [IX_FK_tbl_Order_tbl_OrderStatus]
ON [dbo].[tbl_Order]
    ([OrderStatusID]);
GO

-- Creating foreign key on [OrderTypeID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_OrderType]
    FOREIGN KEY ([OrderTypeID])
    REFERENCES [dbo].[tbl_OrderType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_OrderType'
CREATE INDEX [IX_FK_tbl_Order_tbl_OrderType]
ON [dbo].[tbl_Order]
    ([OrderTypeID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Order_tbl_PriceList]
ON [dbo].[tbl_Order]
    ([PriceListID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Order'
ALTER TABLE [dbo].[tbl_Order]
ADD CONSTRAINT [FK_tbl_Order_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Order_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Order_tbl_Sites]
ON [dbo].[tbl_Order]
    ([SiteID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Order'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Order]
ON [dbo].[tbl_OrderProducts]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Order'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Order]
ON [dbo].[tbl_Payment]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Order'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Order]
ON [dbo].[tbl_Requirement]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Order'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Order]
ON [dbo].[tbl_Shipment]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_Order'
CREATE INDEX [IX_FK_tbl_Task_tbl_Order]
ON [dbo].[tbl_Task]
    ([OrderID]);
GO

-- Creating foreign key on [OrderID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Order'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Order]
ON [dbo].[tbl_TaskMember]
    ([OrderID]);
GO

-- Creating foreign key on [ParentOrderProductID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_OrderProductsParent]
    FOREIGN KEY ([ParentOrderProductID])
    REFERENCES [dbo].[tbl_OrderProducts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_OrderProductsParent'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_OrderProductsParent]
ON [dbo].[tbl_OrderProducts]
    ([ParentOrderProductID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_PriceList]
ON [dbo].[tbl_OrderProducts]
    ([PriceListID]);
GO

-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_OrderProducts]
    ([SpecialOfferPriceListID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_OrderProducts'
ALTER TABLE [dbo].[tbl_OrderProducts]
ADD CONSTRAINT [FK_tbl_OrderProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OrderProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_OrderProducts_tbl_Product]
ON [dbo].[tbl_OrderProducts]
    ([ProductID]);
GO

-- Creating foreign key on [OrderProductsID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_OrderProducts]
    FOREIGN KEY ([OrderProductsID])
    REFERENCES [dbo].[tbl_OrderProducts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_OrderProducts'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_OrderProducts]
ON [dbo].[tbl_TaskMember]
    ([OrderProductsID]);
GO

-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentPassRule]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentPassRule'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentPassRule]
ON [dbo].[tbl_Payment]
    ([PaymentPassRuleID]);
GO

-- Creating foreign key on [StatusID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentStatus]
    FOREIGN KEY ([StatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentStatus'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentStatus]
ON [dbo].[tbl_Payment]
    ([StatusID]);
GO

-- Creating foreign key on [PaymentTypeID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_PaymentType]
    FOREIGN KEY ([PaymentTypeID])
    REFERENCES [dbo].[tbl_PaymentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_PaymentType'
CREATE INDEX [IX_FK_tbl_Payment_tbl_PaymentType]
ON [dbo].[tbl_Payment]
    ([PaymentTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Payment'
ALTER TABLE [dbo].[tbl_Payment]
ADD CONSTRAINT [FK_tbl_Payment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Payment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Payment_tbl_Sites]
ON [dbo].[tbl_Payment]
    ([SiteID]);
GO

-- Creating foreign key on [PaymentID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_Payment]
    FOREIGN KEY ([PaymentID])
    REFERENCES [dbo].[tbl_Payment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_Payment'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_Payment]
ON [dbo].[tbl_PaymentPass]
    ([PaymentID]);
GO

-- Creating foreign key on [PaymentPassCategoryID] in table 'tbl_PaymentArticle'
ALTER TABLE [dbo].[tbl_PaymentArticle]
ADD CONSTRAINT [FK_tbl_PaymentArticle_tbl_PaymentPassCategory]
    FOREIGN KEY ([PaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentArticle_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentArticle_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentArticle]
    ([PaymentPassCategoryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentArticle'
ALTER TABLE [dbo].[tbl_PaymentArticle]
ADD CONSTRAINT [FK_tbl_PaymentArticle_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentArticle_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentArticle_tbl_Sites]
ON [dbo].[tbl_PaymentArticle]
    ([SiteID]);
GO

-- Creating foreign key on [PaymentArticleID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentArticle]
    FOREIGN KEY ([PaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentBalance]
    ([PaymentArticleID]);
GO

-- Creating foreign key on [OutgoPaymentArticleID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle]
    FOREIGN KEY ([OutgoPaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentPass]
    ([OutgoPaymentArticleID]);
GO

-- Creating foreign key on [IncomePaymentArticleID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentArticle1]
    FOREIGN KEY ([IncomePaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentArticle1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentArticle1]
ON [dbo].[tbl_PaymentPass]
    ([IncomePaymentArticleID]);
GO

-- Creating foreign key on [OutgoPaymentArticleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle]
    FOREIGN KEY ([OutgoPaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentArticle'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentArticle]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoPaymentArticleID]);
GO

-- Creating foreign key on [IncomePaymentArticleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1]
    FOREIGN KEY ([IncomePaymentArticleID])
    REFERENCES [dbo].[tbl_PaymentArticle]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentArticle1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomePaymentArticleID]);
GO

-- Creating foreign key on [PaymentPassCategoryID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentBalance]
    FOREIGN KEY ([PaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentBalance'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentBalance]
ON [dbo].[tbl_PaymentBalance]
    ([PaymentPassCategoryID]);
GO

-- Creating foreign key on [CFOID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_PaymentCFO]
    FOREIGN KEY ([CFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentBalance]
    ([CFOID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentBalance'
ALTER TABLE [dbo].[tbl_PaymentBalance]
ADD CONSTRAINT [FK_tbl_PaymentBalance_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentBalance_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentBalance_tbl_Sites]
ON [dbo].[tbl_PaymentBalance]
    ([SiteID]);
GO

-- Creating foreign key on [PaymentPassCategoryID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [FK_tbl_PaymentCFO_tbl_PaymentPassCategory]
    FOREIGN KEY ([PaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentCFO_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentCFO_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentCFO]
    ([PaymentPassCategoryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentCFO'
ALTER TABLE [dbo].[tbl_PaymentCFO]
ADD CONSTRAINT [FK_tbl_PaymentCFO_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentCFO_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentCFO_tbl_Sites]
ON [dbo].[tbl_PaymentCFO]
    ([SiteID]);
GO

-- Creating foreign key on [OutgoCFOID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO]
    FOREIGN KEY ([OutgoCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentPass]
    ([OutgoCFOID]);
GO

-- Creating foreign key on [IncomeCFOID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentCFO1]
    FOREIGN KEY ([IncomeCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentCFO1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentCFO1]
ON [dbo].[tbl_PaymentPass]
    ([IncomeCFOID]);
GO

-- Creating foreign key on [OutgoCFOID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO]
    FOREIGN KEY ([OutgoCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentCFO'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentCFO]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoCFOID]);
GO

-- Creating foreign key on [IncomeCFOID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1]
    FOREIGN KEY ([IncomeCFOID])
    REFERENCES [dbo].[tbl_PaymentCFO]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentCFO1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomeCFOID]);
GO

-- Creating foreign key on [OutgoPaymentPassCategoryID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory]
    FOREIGN KEY ([OutgoPaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentPass]
    ([OutgoPaymentPassCategoryID]);
GO

-- Creating foreign key on [IncomePaymentPassCategoryID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_PaymentPassCategory1]
    FOREIGN KEY ([IncomePaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_PaymentPassCategory1'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_PaymentPassCategory1]
ON [dbo].[tbl_PaymentPass]
    ([IncomePaymentPassCategoryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentPass'
ALTER TABLE [dbo].[tbl_PaymentPass]
ADD CONSTRAINT [FK_tbl_PaymentPass_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPass_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPass_tbl_Sites]
ON [dbo].[tbl_PaymentPass]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassCategory'
ALTER TABLE [dbo].[tbl_PaymentPassCategory]
ADD CONSTRAINT [FK_tbl_PaymentPassCategory_tbl_Sites1]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassCategory_tbl_Sites1'
CREATE INDEX [IX_FK_tbl_PaymentPassCategory_tbl_Sites1]
ON [dbo].[tbl_PaymentPassCategory]
    ([SiteID]);
GO

-- Creating foreign key on [IncomePaymentPassCategoryID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory]
    FOREIGN KEY ([IncomePaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory]
ON [dbo].[tbl_PaymentPassRulePass]
    ([IncomePaymentPassCategoryID]);
GO

-- Creating foreign key on [OutgoPaymentPassCategoryID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1]
    FOREIGN KEY ([OutgoPaymentPassCategoryID])
    REFERENCES [dbo].[tbl_PaymentPassCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassCategory1]
ON [dbo].[tbl_PaymentPassRulePass]
    ([OutgoPaymentPassCategoryID]);
GO

-- Creating foreign key on [PaymentTypeID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [FK_tbl_PaymentPassRule_tbl_PaymentType]
    FOREIGN KEY ([PaymentTypeID])
    REFERENCES [dbo].[tbl_PaymentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRule_tbl_PaymentType'
CREATE INDEX [IX_FK_tbl_PaymentPassRule_tbl_PaymentType]
ON [dbo].[tbl_PaymentPassRule]
    ([PaymentTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRule'
ALTER TABLE [dbo].[tbl_PaymentPassRule]
ADD CONSTRAINT [FK_tbl_PaymentPassRule_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRule_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRule_tbl_Sites]
ON [dbo].[tbl_PaymentPassRule]
    ([SiteID]);
GO

-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_PaymentPassRule]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([PaymentPassRuleID]);
GO

-- Creating foreign key on [PaymentPassRuleID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass]
    FOREIGN KEY ([PaymentPassRuleID])
    REFERENCES [dbo].[tbl_PaymentPassRule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_PaymentPassRulePass]
ON [dbo].[tbl_PaymentPassRulePass]
    ([PaymentPassRuleID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRuleCompany'
ALTER TABLE [dbo].[tbl_PaymentPassRuleCompany]
ADD CONSTRAINT [FK_tbl_PaymentPassRuleCompany_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRuleCompany_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRuleCompany_tbl_Sites]
ON [dbo].[tbl_PaymentPassRuleCompany]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentPassRulePass'
ALTER TABLE [dbo].[tbl_PaymentPassRulePass]
ADD CONSTRAINT [FK_tbl_PaymentPassRulePass_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentPassRulePass_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentPassRulePass_tbl_Sites]
ON [dbo].[tbl_PaymentPassRulePass]
    ([SiteID]);
GO

-- Creating foreign key on [ID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [FK_tbl_PaymentStatus_tbl_PaymentStatus]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentStatus'
ALTER TABLE [dbo].[tbl_PaymentStatus]
ADD CONSTRAINT [FK_tbl_PaymentStatus_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentStatus_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentStatus_tbl_Sites]
ON [dbo].[tbl_PaymentStatus]
    ([SiteID]);
GO

-- Creating foreign key on [InitialPaymentStatusID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus]
    FOREIGN KEY ([InitialPaymentStatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_PaymentStatus'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_PaymentStatus]
ON [dbo].[tbl_PaymentTransition]
    ([InitialPaymentStatusID]);
GO

-- Creating foreign key on [FinalPaymentStatusID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_PaymentStatus1]
    FOREIGN KEY ([FinalPaymentStatusID])
    REFERENCES [dbo].[tbl_PaymentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_PaymentStatus1'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_PaymentStatus1]
ON [dbo].[tbl_PaymentTransition]
    ([FinalPaymentStatusID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PaymentTransition'
ALTER TABLE [dbo].[tbl_PaymentTransition]
ADD CONSTRAINT [FK_tbl_PaymentTransition_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PaymentTransition_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PaymentTransition_tbl_Sites]
ON [dbo].[tbl_PaymentTransition]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PortalSettings'
ALTER TABLE [dbo].[tbl_PortalSettings]
ADD CONSTRAINT [FK_tbl_PortalSettings_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PortalSettings_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PortalSettings_tbl_Sites]
ON [dbo].[tbl_PortalSettings]
    ([SiteID]);
GO

-- Creating foreign key on [PortalSettingsID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_PortalSettings]
    FOREIGN KEY ([PortalSettingsID])
    REFERENCES [dbo].[tbl_PortalSettings]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SocialAuthorizationToken_tbl_PortalSettings'
CREATE INDEX [IX_FK_tbl_SocialAuthorizationToken_tbl_PortalSettings]
ON [dbo].[tbl_SocialAuthorizationToken]
    ([PortalSettingsID]);
GO

-- Creating foreign key on [PriceListStatusID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_PriceListStatus]
    FOREIGN KEY ([PriceListStatusID])
    REFERENCES [dbo].[tbl_PriceListStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_PriceListStatus'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_PriceListStatus]
ON [dbo].[tbl_PriceList]
    ([PriceListStatusID]);
GO

-- Creating foreign key on [PriceListTypeID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_PriceListType]
    FOREIGN KEY ([PriceListTypeID])
    REFERENCES [dbo].[tbl_PriceListType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_PriceListType'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_PriceListType]
ON [dbo].[tbl_PriceList]
    ([PriceListTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_Sites]
ON [dbo].[tbl_PriceList]
    ([SiteID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_ProductPrice'
ALTER TABLE [dbo].[tbl_ProductPrice]
ADD CONSTRAINT [FK_tbl_ProductPrice_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPrice_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_ProductPrice_tbl_PriceList]
ON [dbo].[tbl_ProductPrice]
    ([PriceListID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_PriceList]
ON [dbo].[tbl_Shipment]
    ([PriceListID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_PriceList]
ON [dbo].[tbl_ShipmentProducts]
    ([PriceListID]);
GO

-- Creating foreign key on [SpecialOfferPriceListID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer]
    FOREIGN KEY ([SpecialOfferPriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_PriceListSpecialOffer]
ON [dbo].[tbl_ShipmentProducts]
    ([SpecialOfferPriceListID]);
GO

-- Creating foreign key on [PriceListID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_PriceList]
    FOREIGN KEY ([PriceListID])
    REFERENCES [dbo].[tbl_PriceList]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_PriceList'
CREATE INDEX [IX_FK_tbl_Sites_tbl_PriceList]
ON [dbo].[tbl_Sites]
    ([PriceListID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Priorities'
ALTER TABLE [dbo].[tbl_Priorities]
ADD CONSTRAINT [FK_tbl_Priorities_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Priorities_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Priorities_tbl_Sites]
ON [dbo].[tbl_Priorities]
    ([SiteID]);
GO

-- Creating foreign key on [ProductStatusID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_ProductStatus]
    FOREIGN KEY ([ProductStatusID])
    REFERENCES [dbo].[tbl_ProductStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_ProductStatus'
CREATE INDEX [IX_FK_tbl_Product_tbl_ProductStatus]
ON [dbo].[tbl_Product]
    ([ProductStatusID]);
GO

-- Creating foreign key on [ProductTypeID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_ProductType]
    FOREIGN KEY ([ProductTypeID])
    REFERENCES [dbo].[tbl_ProductType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_ProductType'
CREATE INDEX [IX_FK_tbl_Product_tbl_ProductType]
ON [dbo].[tbl_Product]
    ([ProductTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Product_tbl_Sites]
ON [dbo].[tbl_Product]
    ([SiteID]);
GO

-- Creating foreign key on [UnitID] in table 'tbl_Product'
ALTER TABLE [dbo].[tbl_Product]
ADD CONSTRAINT [FK_tbl_Product_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Product_tbl_Unit'
CREATE INDEX [IX_FK_tbl_Product_tbl_Unit]
ON [dbo].[tbl_Product]
    ([UnitID]);
GO

-- Creating foreign key on [BaseProductID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product]
    FOREIGN KEY ([BaseProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductComplectation_tbl_Product'
CREATE INDEX [IX_FK_tbl_ProductComplectation_tbl_Product]
ON [dbo].[tbl_ProductComplectation]
    ([BaseProductID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_ProductComplectation'
ALTER TABLE [dbo].[tbl_ProductComplectation]
ADD CONSTRAINT [FK_tbl_ProductComplectation_tbl_Product1]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductComplectation_tbl_Product1'
CREATE INDEX [IX_FK_tbl_ProductComplectation_tbl_Product1]
ON [dbo].[tbl_ProductComplectation]
    ([ProductID]);
GO

-- Creating foreign key on [ProductId] in table 'tbl_ProductPhoto'
ALTER TABLE [dbo].[tbl_ProductPhoto]
ADD CONSTRAINT [FK_tbl_ProductPhoto_tbl_ProductPhoto]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPhoto_tbl_ProductPhoto'
CREATE INDEX [IX_FK_tbl_ProductPhoto_tbl_ProductPhoto]
ON [dbo].[tbl_ProductPhoto]
    ([ProductId]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Product'
CREATE INDEX [IX_FK_tbl_Request_tbl_Product]
ON [dbo].[tbl_Request]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Product'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Product]
ON [dbo].[tbl_Requirement]
    ([ProductID]);
GO

-- Creating foreign key on [EvaluationRequirementsProductID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Product_EvaluationRequirements]
    FOREIGN KEY ([EvaluationRequirementsProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Product_EvaluationRequirements'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Product_EvaluationRequirements]
ON [dbo].[tbl_Requirement]
    ([EvaluationRequirementsProductID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Product'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Product]
ON [dbo].[tbl_ShipmentProducts]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_Product'
CREATE INDEX [IX_FK_tbl_Task_tbl_Product]
ON [dbo].[tbl_Task]
    ([ProductID]);
GO

-- Creating foreign key on [ProductID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_Product'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_Product]
ON [dbo].[tbl_TaskType]
    ([ProductID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductCategory_tbl_ProductCategory'
CREATE INDEX [IX_FK_tbl_ProductCategory_tbl_ProductCategory]
ON [dbo].[tbl_ProductCategory]
    ([SiteID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_ProductCategory'
ALTER TABLE [dbo].[tbl_ProductCategory]
ADD CONSTRAINT [FK_tbl_ProductCategory_tbl_ProductCategory1]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_ProductCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductCategory_tbl_ProductCategory1'
CREATE INDEX [IX_FK_tbl_ProductCategory_tbl_ProductCategory1]
ON [dbo].[tbl_ProductCategory]
    ([ParentID]);
GO

-- Creating foreign key on [ProductWorkWithComplectationID] in table 'tbl_ProductType'
ALTER TABLE [dbo].[tbl_ProductType]
ADD CONSTRAINT [FK_tbl_ProductType_tbl_ProductWorkWithComplectation]
    FOREIGN KEY ([ProductWorkWithComplectationID])
    REFERENCES [dbo].[tbl_ProductWorkWithComplectation]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductType_tbl_ProductWorkWithComplectation'
CREATE INDEX [IX_FK_tbl_ProductType_tbl_ProductWorkWithComplectation]
ON [dbo].[tbl_ProductType]
    ([ProductWorkWithComplectationID]);
GO

-- Creating foreign key on [PublicationCategoryID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationCategory]
    FOREIGN KEY ([PublicationCategoryID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationCategory'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationCategory]
ON [dbo].[tbl_Publication]
    ([PublicationCategoryID]);
GO

-- Creating foreign key on [PublicationStatusID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationStatus]
    FOREIGN KEY ([PublicationStatusID])
    REFERENCES [dbo].[tbl_PublicationStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationStatus'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationStatus]
ON [dbo].[tbl_Publication]
    ([PublicationStatusID]);
GO

-- Creating foreign key on [PublicationTypeID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_PublicationType]
    FOREIGN KEY ([PublicationTypeID])
    REFERENCES [dbo].[tbl_PublicationType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_PublicationType'
CREATE INDEX [IX_FK_tbl_Publication_tbl_PublicationType]
ON [dbo].[tbl_Publication]
    ([PublicationTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Publication'
ALTER TABLE [dbo].[tbl_Publication]
ADD CONSTRAINT [FK_tbl_Publication_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Publication_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Publication_tbl_Sites]
ON [dbo].[tbl_Publication]
    ([SiteID]);
GO

-- Creating foreign key on [PublicationID] in table 'tbl_PublicationComment'
ALTER TABLE [dbo].[tbl_PublicationComment]
ADD CONSTRAINT [FK_tbl_PublicationComment_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationComment_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationComment_tbl_Publication]
ON [dbo].[tbl_PublicationComment]
    ([PublicationID]);
GO

-- Creating foreign key on [PublicationID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_Publication]
ON [dbo].[tbl_PublicationMark]
    ([PublicationID]);
GO

-- Creating foreign key on [PublicationID] in table 'tbl_PublicationTerms'
ALTER TABLE [dbo].[tbl_PublicationTerms]
ADD CONSTRAINT [FK_tbl_PublicationTerms_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationTerms_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationTerms_tbl_Publication]
ON [dbo].[tbl_PublicationTerms]
    ([PublicationID]);
GO

-- Creating foreign key on [PublicationID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [FK_tbl_RelatedPublication_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RelatedPublication_tbl_Publication'
CREATE INDEX [IX_FK_tbl_RelatedPublication_tbl_Publication]
ON [dbo].[tbl_RelatedPublication]
    ([PublicationID]);
GO

-- Creating foreign key on [PublicationID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [FK_tbl_Term_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Term_tbl_Publication'
CREATE INDEX [IX_FK_tbl_Term_tbl_Publication]
ON [dbo].[tbl_Term]
    ([PublicationID]);
GO

-- Creating foreign key on [PublicationAccessCommentID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessComment]
    FOREIGN KEY ([PublicationAccessCommentID])
    REFERENCES [dbo].[tbl_PublicationAccessComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationAccessComment'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationAccessComment]
ON [dbo].[tbl_PublicationType]
    ([PublicationAccessCommentID]);
GO

-- Creating foreign key on [PublicationAccessRecordID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationAccessRecord]
    FOREIGN KEY ([PublicationAccessRecordID])
    REFERENCES [dbo].[tbl_PublicationAccessRecord]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationAccessRecord'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationAccessRecord]
ON [dbo].[tbl_PublicationType]
    ([PublicationAccessRecordID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [FK_tbl_PublicationCategory_tbl_PublicationCategory1]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationCategory_tbl_PublicationCategory1'
CREATE INDEX [IX_FK_tbl_PublicationCategory_tbl_PublicationCategory1]
ON [dbo].[tbl_PublicationCategory]
    ([ParentID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PublicationCategory'
ALTER TABLE [dbo].[tbl_PublicationCategory]
ADD CONSTRAINT [FK_tbl_PublicationCategory_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationCategory_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationCategory_tbl_Sites]
ON [dbo].[tbl_PublicationCategory]
    ([SiteID]);
GO

-- Creating foreign key on [PublicationCategoryID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_PublicationCategory]
    FOREIGN KEY ([PublicationCategoryID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_PublicationCategory'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_PublicationCategory]
ON [dbo].[tbl_Requirement]
    ([PublicationCategoryID]);
GO

-- Creating foreign key on [PublicationCommentID] in table 'tbl_PublicationMark'
ALTER TABLE [dbo].[tbl_PublicationMark]
ADD CONSTRAINT [FK_tbl_PublicationMark_tbl_PublicationComment]
    FOREIGN KEY ([PublicationCommentID])
    REFERENCES [dbo].[tbl_PublicationComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationMark_tbl_PublicationComment'
CREATE INDEX [IX_FK_tbl_PublicationMark_tbl_PublicationComment]
ON [dbo].[tbl_PublicationMark]
    ([PublicationCommentID]);
GO

-- Creating foreign key on [PublicationKindID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_PublicationKind]
    FOREIGN KEY ([PublicationKindID])
    REFERENCES [dbo].[tbl_PublicationKind]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_PublicationKind'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_PublicationKind]
ON [dbo].[tbl_PublicationType]
    ([PublicationKindID]);
GO

-- Creating foreign key on [PublicationStatusID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus]
    FOREIGN KEY ([PublicationStatusID])
    REFERENCES [dbo].[tbl_PublicationStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_PublicationStatus]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([PublicationStatusID]);
GO

-- Creating foreign key on [PublicationTypeID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType]
    FOREIGN KEY ([PublicationTypeID])
    REFERENCES [dbo].[tbl_PublicationType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_PublicationType]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([PublicationTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PublicationStatusToPublicationType'
ALTER TABLE [dbo].[tbl_PublicationStatusToPublicationType]
ADD CONSTRAINT [FK_tbl_PublicationStatusToPublicationType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationStatusToPublicationType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationStatusToPublicationType_tbl_Sites]
ON [dbo].[tbl_PublicationStatusToPublicationType]
    ([SiteID]);
GO

-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_RequestSourceType]
ON [dbo].[tbl_PublicationType]
    ([RequestSourceTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_PublicationType'
ALTER TABLE [dbo].[tbl_PublicationType]
ADD CONSTRAINT [FK_tbl_PublicationType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PublicationType_tbl_Sites]
ON [dbo].[tbl_PublicationType]
    ([SiteID]);
GO

-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_Request_tbl_RequestSourceType]
ON [dbo].[tbl_Request]
    ([RequestSourceTypeID]);
GO

-- Creating foreign key on [RequestStatusID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_RequestStatus]
    FOREIGN KEY ([RequestStatusID])
    REFERENCES [dbo].[tbl_RequestStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_RequestStatus'
CREATE INDEX [IX_FK_tbl_Request_tbl_RequestStatus]
ON [dbo].[tbl_Request]
    ([RequestStatusID]);
GO

-- Creating foreign key on [ServiceLevelID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_Request_tbl_ServiceLevel]
ON [dbo].[tbl_Request]
    ([ServiceLevelID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Request'
ALTER TABLE [dbo].[tbl_Request]
ADD CONSTRAINT [FK_tbl_Request_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Request_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Request_tbl_Sites]
ON [dbo].[tbl_Request]
    ([SiteID]);
GO

-- Creating foreign key on [ContentID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [FK_tbl_RequestComment_tbl_Request]
    FOREIGN KEY ([ContentID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestComment_tbl_Request'
CREATE INDEX [IX_FK_tbl_RequestComment_tbl_Request]
ON [dbo].[tbl_RequestComment]
    ([ContentID]);
GO

-- Creating foreign key on [RequestID] in table 'tbl_RequestFile'
ALTER TABLE [dbo].[tbl_RequestFile]
ADD CONSTRAINT [FK_tbl_RequestFile_tbl_Request]
    FOREIGN KEY ([RequestID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestFile_tbl_Request'
CREATE INDEX [IX_FK_tbl_RequestFile_tbl_Request]
ON [dbo].[tbl_RequestFile]
    ([RequestID]);
GO

-- Creating foreign key on [RequestID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Request]
    FOREIGN KEY ([RequestID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Request'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Request]
ON [dbo].[tbl_RequestHistory]
    ([RequestID]);
GO

-- Creating foreign key on [ReplyToID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [FK_tbl_RequestComment_tbl_RequestComment]
    FOREIGN KEY ([ReplyToID])
    REFERENCES [dbo].[tbl_RequestComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestComment_tbl_RequestComment'
CREATE INDEX [IX_FK_tbl_RequestComment_tbl_RequestComment]
ON [dbo].[tbl_RequestComment]
    ([ReplyToID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [FK_tbl_RequestComment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestComment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequestComment_tbl_Sites]
ON [dbo].[tbl_RequestComment]
    ([SiteID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [FK_tbl_RequestComment_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestComment_tbl_User'
CREATE INDEX [IX_FK_tbl_RequestComment_tbl_User]
ON [dbo].[tbl_RequestComment]
    ([UserID]);
GO

-- Creating foreign key on [DestinationUserID] in table 'tbl_RequestComment'
ALTER TABLE [dbo].[tbl_RequestComment]
ADD CONSTRAINT [FK_tbl_RequestComment_tbl_User_Destination]
    FOREIGN KEY ([DestinationUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestComment_tbl_User_Destination'
CREATE INDEX [IX_FK_tbl_RequestComment_tbl_User_Destination]
ON [dbo].[tbl_RequestComment]
    ([DestinationUserID]);
GO

-- Creating foreign key on [ContentCommentID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [FK_tbl_RequestCommentMark_tbl_RequestComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_RequestComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestCommentMark_tbl_RequestComment'
CREATE INDEX [IX_FK_tbl_RequestCommentMark_tbl_RequestComment]
ON [dbo].[tbl_RequestCommentMark]
    ([ContentCommentID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_RequestCommentMark'
ALTER TABLE [dbo].[tbl_RequestCommentMark]
ADD CONSTRAINT [FK_tbl_RequestCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_RequestCommentMark_tbl_User]
ON [dbo].[tbl_RequestCommentMark]
    ([UserID]);
GO

-- Creating foreign key on [RequestSourceCategoryID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceCategoryID])
    REFERENCES [dbo].[tbl_RequestSourceCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_RequestSourceType]
ON [dbo].[tbl_RequestSourceType]
    ([RequestSourceCategoryID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_Sites]
ON [dbo].[tbl_RequestSourceType]
    ([SiteID]);
GO

-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequestSourceType]
ON [dbo].[tbl_Requirement]
    ([RequestSourceTypeID]);
GO

-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_RequestSourceType]
ON [dbo].[tbl_SourceMonitoring]
    ([RequestSourceTypeID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Requirement]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Requirement]
ON [dbo].[tbl_Requirement]
    ([ParentID]);
GO

-- Creating foreign key on [RequirementComplexityID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementComplexity]
    FOREIGN KEY ([RequirementComplexityID])
    REFERENCES [dbo].[tbl_RequirementComplexity]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementComplexity'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementComplexity]
ON [dbo].[tbl_Requirement]
    ([RequirementComplexityID]);
GO

-- Creating foreign key on [RequirementImplementationCompleteID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementImplementationComplete]
    FOREIGN KEY ([RequirementImplementationCompleteID])
    REFERENCES [dbo].[tbl_RequirementImplementationComplete]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementImplementationComplete'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementImplementationComplete]
ON [dbo].[tbl_Requirement]
    ([RequirementImplementationCompleteID]);
GO

-- Creating foreign key on [RequirementPriorityID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementPriority]
    FOREIGN KEY ([RequirementPriorityID])
    REFERENCES [dbo].[tbl_RequirementPriority]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementPriority'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementPriority]
ON [dbo].[tbl_Requirement]
    ([RequirementPriorityID]);
GO

-- Creating foreign key on [RequirementSatisfactionID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSatisfaction]
    FOREIGN KEY ([RequirementSatisfactionID])
    REFERENCES [dbo].[tbl_RequirementSatisfaction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSatisfaction'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSatisfaction]
ON [dbo].[tbl_Requirement]
    ([RequirementSatisfactionID]);
GO

-- Creating foreign key on [RequirementSeverityOfExposureID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSeverityOfExposure]
    FOREIGN KEY ([RequirementSeverityOfExposureID])
    REFERENCES [dbo].[tbl_RequirementSeverityOfExposure]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSeverityOfExposure'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSeverityOfExposure]
ON [dbo].[tbl_Requirement]
    ([RequirementSeverityOfExposureID]);
GO

-- Creating foreign key on [RequirementSpeedTimeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSpeedTime]
    FOREIGN KEY ([RequirementSpeedTimeID])
    REFERENCES [dbo].[tbl_RequirementSpeedTime]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSpeedTime'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSpeedTime]
ON [dbo].[tbl_Requirement]
    ([RequirementSpeedTimeID]);
GO

-- Creating foreign key on [RequirementStatusID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementStatus]
    FOREIGN KEY ([RequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementStatus'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementStatus]
ON [dbo].[tbl_Requirement]
    ([RequirementStatusID]);
GO

-- Creating foreign key on [RequirementTypeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementType]
ON [dbo].[tbl_Requirement]
    ([RequirementTypeID]);
GO

-- Creating foreign key on [ServiceLevelID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_ServiceLevel]
ON [dbo].[tbl_Requirement]
    ([ServiceLevelID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Sites]
ON [dbo].[tbl_Requirement]
    ([SiteID]);
GO

-- Creating foreign key on [UnitID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Unit'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Unit]
ON [dbo].[tbl_Requirement]
    ([UnitID]);
GO

-- Creating foreign key on [InternalUnitID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Unit_Internal]
    FOREIGN KEY ([InternalUnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Unit_Internal'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Unit_Internal]
ON [dbo].[tbl_Requirement]
    ([InternalUnitID]);
GO

-- Creating foreign key on [ContentID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [FK_tbl_RequirementComment_tbl_Requirement]
    FOREIGN KEY ([ContentID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComment_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_RequirementComment_tbl_Requirement]
ON [dbo].[tbl_RequirementComment]
    ([ContentID]);
GO

-- Creating foreign key on [RequirementID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_Requirement]
    FOREIGN KEY ([RequirementID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_Requirement]
ON [dbo].[tbl_RequirementHistory]
    ([RequirementID]);
GO

-- Creating foreign key on [ReplyToID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [FK_tbl_RequirementComment_tbl_RequirementComment]
    FOREIGN KEY ([ReplyToID])
    REFERENCES [dbo].[tbl_RequirementComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComment_tbl_RequirementComment'
CREATE INDEX [IX_FK_tbl_RequirementComment_tbl_RequirementComment]
ON [dbo].[tbl_RequirementComment]
    ([ReplyToID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [FK_tbl_RequirementComment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementComment_tbl_Sites]
ON [dbo].[tbl_RequirementComment]
    ([SiteID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [FK_tbl_RequirementComment_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComment_tbl_User'
CREATE INDEX [IX_FK_tbl_RequirementComment_tbl_User]
ON [dbo].[tbl_RequirementComment]
    ([UserID]);
GO

-- Creating foreign key on [DestinationUserID] in table 'tbl_RequirementComment'
ALTER TABLE [dbo].[tbl_RequirementComment]
ADD CONSTRAINT [FK_tbl_RequirementComment_tbl_User_Destination]
    FOREIGN KEY ([DestinationUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComment_tbl_User_Destination'
CREATE INDEX [IX_FK_tbl_RequirementComment_tbl_User_Destination]
ON [dbo].[tbl_RequirementComment]
    ([DestinationUserID]);
GO

-- Creating foreign key on [ContentCommentID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_RequirementComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_RequirementComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementCommentMark_tbl_RequirementComment'
CREATE INDEX [IX_FK_tbl_RequirementCommentMark_tbl_RequirementComment]
ON [dbo].[tbl_RequirementCommentMark]
    ([ContentCommentID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_RequirementCommentMark'
ALTER TABLE [dbo].[tbl_RequirementCommentMark]
ADD CONSTRAINT [FK_tbl_RequirementCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_RequirementCommentMark_tbl_User]
ON [dbo].[tbl_RequirementCommentMark]
    ([UserID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementComplexity'
ALTER TABLE [dbo].[tbl_RequirementComplexity]
ADD CONSTRAINT [FK_tbl_RequirementComplexity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementComplexity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementComplexity_tbl_Sites]
ON [dbo].[tbl_RequirementComplexity]
    ([SiteID]);
GO

-- Creating foreign key on [RequirementStatusID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_RequirementStatus]
    FOREIGN KEY ([RequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_RequirementStatus'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_RequirementStatus]
ON [dbo].[tbl_RequirementHistory]
    ([RequirementStatusID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementImplementationComplete'
ALTER TABLE [dbo].[tbl_RequirementImplementationComplete]
ADD CONSTRAINT [FK_tbl_RequirementImplementationComplete_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementImplementationComplete_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementImplementationComplete_tbl_Sites]
ON [dbo].[tbl_RequirementImplementationComplete]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementPriority'
ALTER TABLE [dbo].[tbl_RequirementPriority]
ADD CONSTRAINT [FK_tbl_RequirementPriority_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementPriority_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementPriority_tbl_Sites]
ON [dbo].[tbl_RequirementPriority]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementSatisfaction'
ALTER TABLE [dbo].[tbl_RequirementSatisfaction]
ADD CONSTRAINT [FK_tbl_RequirementSatisfaction_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSatisfaction_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementSatisfaction_tbl_Sites]
ON [dbo].[tbl_RequirementSatisfaction]
    ([SiteID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_RequirementSeverityOfExposure]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_RequirementSeverityOfExposure]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([ParentID]);
GO

-- Creating foreign key on [RequirementTypeID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_RequirementType]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([RequirementTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementSeverityOfExposure'
ALTER TABLE [dbo].[tbl_RequirementSeverityOfExposure]
ADD CONSTRAINT [FK_tbl_RequirementSeverityOfExposure_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSeverityOfExposure_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementSeverityOfExposure_tbl_Sites]
ON [dbo].[tbl_RequirementSeverityOfExposure]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementSpeedTime'
ALTER TABLE [dbo].[tbl_RequirementSpeedTime]
ADD CONSTRAINT [FK_tbl_RequirementSpeedTime_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementSpeedTime_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementSpeedTime_tbl_Sites]
ON [dbo].[tbl_RequirementSpeedTime]
    ([SiteID]);
GO

-- Creating foreign key on [ServiceLevelRoleID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [FK_tbl_RequirementStatus_tbl_ServiceLevelRole]
    FOREIGN KEY ([ServiceLevelRoleID])
    REFERENCES [dbo].[tbl_ServiceLevelRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementStatus_tbl_ServiceLevelRole'
CREATE INDEX [IX_FK_tbl_RequirementStatus_tbl_ServiceLevelRole]
ON [dbo].[tbl_RequirementStatus]
    ([ServiceLevelRoleID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementStatus'
ALTER TABLE [dbo].[tbl_RequirementStatus]
ADD CONSTRAINT [FK_tbl_RequirementStatus_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementStatus_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementStatus_tbl_Sites]
ON [dbo].[tbl_RequirementStatus]
    ([SiteID]);
GO

-- Creating foreign key on [FinalRequirementStatusID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Final]
    FOREIGN KEY ([FinalRequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementStatus_Final'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementStatus_Final]
ON [dbo].[tbl_RequirementTransition]
    ([FinalRequirementStatusID]);
GO

-- Creating foreign key on [InitialRequirementStatusID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial]
    FOREIGN KEY ([InitialRequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementStatus_Initial]
ON [dbo].[tbl_RequirementTransition]
    ([InitialRequirementStatusID]);
GO

-- Creating foreign key on [RequirementTypeID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_RequirementType]
ON [dbo].[tbl_RequirementTransition]
    ([RequirementTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementTransition'
ALTER TABLE [dbo].[tbl_RequirementTransition]
ADD CONSTRAINT [FK_tbl_RequirementTransition_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementTransition_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementTransition_tbl_Sites]
ON [dbo].[tbl_RequirementTransition]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [FK_tbl_RequirementType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementType_tbl_Sites]
ON [dbo].[tbl_RequirementType]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Resolutions'
ALTER TABLE [dbo].[tbl_Resolutions]
ADD CONSTRAINT [FK_tbl_Resolutions_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Resolutions_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Resolutions_tbl_Sites]
ON [dbo].[tbl_Resolutions]
    ([SiteID]);
GO

-- Creating foreign key on [WorkflowID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Workflow]
ON [dbo].[tbl_Responsible]
    ([WorkflowID]);
GO

-- Creating foreign key on [RuleTypeID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_RuleTypes]
    FOREIGN KEY ([RuleTypeID])
    REFERENCES [dbo].[tbl_RuleTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_RuleTypes'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_RuleTypes]
ON [dbo].[tbl_SiteActivityRules]
    ([RuleTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ServiceLevel'
ALTER TABLE [dbo].[tbl_ServiceLevel]
ADD CONSTRAINT [FK_tbl_ServiceLevel_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevel_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ServiceLevel_tbl_Sites]
ON [dbo].[tbl_ServiceLevel]
    ([SiteID]);
GO

-- Creating foreign key on [ServiceLevelID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_ServiceLevel]
ON [dbo].[tbl_ServiceLevelClient]
    ([ServiceLevelID]);
GO

-- Creating foreign key on [OutOfListServiceContactsID] in table 'tbl_ServiceLevelClient'
ALTER TABLE [dbo].[tbl_ServiceLevelClient]
ADD CONSTRAINT [FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts]
    FOREIGN KEY ([OutOfListServiceContactsID])
    REFERENCES [dbo].[tbl_ServiceLevelOutOfListServiceContacts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts'
CREATE INDEX [IX_FK_tbl_ServiceLevelClient_tbl_ServiceLevelOutOfListServiceContacts]
ON [dbo].[tbl_ServiceLevelClient]
    ([OutOfListServiceContactsID]);
GO

-- Creating foreign key on [ServiceLevelClientID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient]
    FOREIGN KEY ([ServiceLevelClientID])
    REFERENCES [dbo].[tbl_ServiceLevelClient]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient]
ON [dbo].[tbl_ServiceLevelContact]
    ([ServiceLevelClientID]);
GO

-- Creating foreign key on [IncludeToInformID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform]
    FOREIGN KEY ([IncludeToInformID])
    REFERENCES [dbo].[tbl_ServiceLevelIncludeToInform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform]
ON [dbo].[tbl_ServiceLevelContact]
    ([IncludeToInformID]);
GO

-- Creating foreign key on [InformRequestID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform]
    FOREIGN KEY ([InformRequestID])
    REFERENCES [dbo].[tbl_ServiceLevelInform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform]
ON [dbo].[tbl_ServiceLevelContact]
    ([InformRequestID]);
GO

-- Creating foreign key on [InformCommentID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment]
    FOREIGN KEY ([InformCommentID])
    REFERENCES [dbo].[tbl_ServiceLevelInformComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment]
ON [dbo].[tbl_ServiceLevelContact]
    ([InformCommentID]);
GO

-- Creating foreign key on [ServiceLevelRoleID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole]
    FOREIGN KEY ([ServiceLevelRoleID])
    REFERENCES [dbo].[tbl_ServiceLevelRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole]
ON [dbo].[tbl_ServiceLevelContact]
    ([ServiceLevelRoleID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ServiceLevelRole'
ALTER TABLE [dbo].[tbl_ServiceLevelRole]
ADD CONSTRAINT [FK_tbl_ServiceLevelRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ServiceLevelRole_tbl_Sites]
ON [dbo].[tbl_ServiceLevelRole]
    ([SiteID]);
GO

-- Creating foreign key on [ShipmentStatusID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentStatus]
    FOREIGN KEY ([ShipmentStatusID])
    REFERENCES [dbo].[tbl_ShipmentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_ShipmentStatus'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_ShipmentStatus]
ON [dbo].[tbl_Shipment]
    ([ShipmentStatusID]);
GO

-- Creating foreign key on [ShipmentTypeID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_ShipmentType]
    FOREIGN KEY ([ShipmentTypeID])
    REFERENCES [dbo].[tbl_ShipmentType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_ShipmentType'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_ShipmentType]
ON [dbo].[tbl_Shipment]
    ([ShipmentTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Shipment'
ALTER TABLE [dbo].[tbl_Shipment]
ADD CONSTRAINT [FK_tbl_Shipment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Shipment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Shipment_tbl_Sites]
ON [dbo].[tbl_Shipment]
    ([SiteID]);
GO

-- Creating foreign key on [ContentID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_Shipment]
    FOREIGN KEY ([ContentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_Shipment]
ON [dbo].[tbl_ShipmentComment]
    ([ContentID]);
GO

-- Creating foreign key on [ShipmentID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_Shipment]
    FOREIGN KEY ([ShipmentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_Shipment]
ON [dbo].[tbl_ShipmentHistory]
    ([ShipmentID]);
GO

-- Creating foreign key on [ShipmentID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Shipment]
    FOREIGN KEY ([ShipmentID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Shipment]
ON [dbo].[tbl_ShipmentProducts]
    ([ShipmentID]);
GO

-- Creating foreign key on [ReplyToID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_ShipmentComment]
    FOREIGN KEY ([ReplyToID])
    REFERENCES [dbo].[tbl_ShipmentComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_ShipmentComment'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_ShipmentComment]
ON [dbo].[tbl_ShipmentComment]
    ([ReplyToID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_Sites]
ON [dbo].[tbl_ShipmentComment]
    ([SiteID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_User'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_User]
ON [dbo].[tbl_ShipmentComment]
    ([UserID]);
GO

-- Creating foreign key on [DestinationUserID] in table 'tbl_ShipmentComment'
ALTER TABLE [dbo].[tbl_ShipmentComment]
ADD CONSTRAINT [FK_tbl_ShipmentComment_tbl_User_Destination]
    FOREIGN KEY ([DestinationUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentComment_tbl_User_Destination'
CREATE INDEX [IX_FK_tbl_ShipmentComment_tbl_User_Destination]
ON [dbo].[tbl_ShipmentComment]
    ([DestinationUserID]);
GO

-- Creating foreign key on [ContentCommentID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_ShipmentComment]
    FOREIGN KEY ([ContentCommentID])
    REFERENCES [dbo].[tbl_ShipmentComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentCommentMark_tbl_ShipmentComment'
CREATE INDEX [IX_FK_tbl_ShipmentCommentMark_tbl_ShipmentComment]
ON [dbo].[tbl_ShipmentCommentMark]
    ([ContentCommentID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_ShipmentCommentMark'
ALTER TABLE [dbo].[tbl_ShipmentCommentMark]
ADD CONSTRAINT [FK_tbl_ShipmentCommentMark_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentCommentMark_tbl_User'
CREATE INDEX [IX_FK_tbl_ShipmentCommentMark_tbl_User]
ON [dbo].[tbl_ShipmentCommentMark]
    ([UserID]);
GO

-- Creating foreign key on [ShipmentStatusID] in table 'tbl_ShipmentHistory'
ALTER TABLE [dbo].[tbl_ShipmentHistory]
ADD CONSTRAINT [FK_tbl_ShipmentHistory_tbl_ShipmentStatus]
    FOREIGN KEY ([ShipmentStatusID])
    REFERENCES [dbo].[tbl_ShipmentStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentHistory_tbl_ShipmentStatus'
CREATE INDEX [IX_FK_tbl_ShipmentHistory_tbl_ShipmentStatus]
ON [dbo].[tbl_ShipmentHistory]
    ([ShipmentStatusID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Task'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Task]
ON [dbo].[tbl_ShipmentProducts]
    ([TaskID]);
GO

-- Creating foreign key on [UnitID] in table 'tbl_ShipmentProducts'
ALTER TABLE [dbo].[tbl_ShipmentProducts]
ADD CONSTRAINT [FK_tbl_ShipmentProducts_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentProducts_tbl_Unit'
CREATE INDEX [IX_FK_tbl_ShipmentProducts_tbl_Unit]
ON [dbo].[tbl_ShipmentProducts]
    ([UnitID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_ShipmentType'
ALTER TABLE [dbo].[tbl_ShipmentType]
ADD CONSTRAINT [FK_tbl_ShipmentType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ShipmentType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ShipmentType_tbl_Sites]
ON [dbo].[tbl_ShipmentType]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteAction]
    ([SiteActionTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_Sites]
ON [dbo].[tbl_SiteAction]
    ([SiteID]);
GO

-- Creating foreign key on [SourceMonitoringID] in table 'tbl_SiteAction'
ALTER TABLE [dbo].[tbl_SiteAction]
ADD CONSTRAINT [FK_tbl_SiteAction_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteAction_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_SiteAction_tbl_SourceMonitoring]
ON [dbo].[tbl_SiteAction]
    ([SourceMonitoringID]);
GO

-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionAttachment_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionAttachment_tbl_SiteAction]
ON [dbo].[tbl_SiteActionAttachment]
    ([SiteActionID]);
GO

-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_SiteAction]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActionID]);
GO

-- Creating foreign key on [SiteActionID] in table 'tbl_SiteActionTagValue'
ALTER TABLE [dbo].[tbl_SiteActionTagValue]
ADD CONSTRAINT [FK_tbl_SiteActionTagValue_tbl_SiteAction]
    FOREIGN KEY ([SiteActionID])
    REFERENCES [dbo].[tbl_SiteAction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTagValue_tbl_SiteAction'
CREATE INDEX [IX_FK_tbl_SiteActionTagValue_tbl_SiteAction]
ON [dbo].[tbl_SiteActionTagValue]
    ([SiteActionID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActionAttachment'
ALTER TABLE [dbo].[tbl_SiteActionAttachment]
ADD CONSTRAINT [FK_tbl_SiteActionAttachment_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionAttachment_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionAttachment_tbl_Sites]
ON [dbo].[tbl_SiteActionAttachment]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteActionLink'
ALTER TABLE [dbo].[tbl_SiteActionLink]
ADD CONSTRAINT [FK_tbl_SiteActionLink_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionLink_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteActionLink_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteActionLink]
    ([SiteActionTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteActionTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionTemplate_tbl_Sites]
ON [dbo].[tbl_SiteActionTemplate]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteActionTemplateRecipient'
ALTER TABLE [dbo].[tbl_SiteActionTemplateRecipient]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateRecipient_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteActionTemplateRecipient]
    ([SiteActionTemplateID]);
GO

-- Creating foreign key on [SiteActionTemplateID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate]
    FOREIGN KEY ([SiteActionTemplateID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_SiteActionTemplate]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteActionTemplateID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_SiteActionTemplate'
ALTER TABLE [dbo].[tbl_SiteActionTemplate]
ADD CONSTRAINT [FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_SiteActionTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent'
CREATE INDEX [IX_FK_tbl_tbl_SiteActionTemplate_tbl_SiteActionTemplate_Parent]
ON [dbo].[tbl_SiteActionTemplate]
    ([ParentID]);
GO

-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteColumnID]);
GO

-- Creating foreign key on [SiteColumnValueID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues]
    FOREIGN KEY ([SiteColumnValueID])
    REFERENCES [dbo].[tbl_SiteColumnValues]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteColumnValueID]);
GO

-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteEventTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_Sites]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActivityRuleExternalFormID] in table 'tbl_SiteActivityRuleExternalFormFields'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms]
    FOREIGN KEY ([SiteActivityRuleExternalFormID])
    REFERENCES [dbo].[tbl_SiteActivityRuleExternalForms]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms]
ON [dbo].[tbl_SiteActivityRuleExternalFormFields]
    ([SiteActivityRuleExternalFormID]);
GO

-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleExternalForms'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalForms]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleExternalForms]
    ([SiteActivityRuleID]);
GO

-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleFormColumns]
    ([SiteActivityRuleID]);
GO

-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns]
ON [dbo].[tbl_SiteActivityRuleFormColumns]
    ([SiteColumnID]);
GO

-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteActivityRuleID]);
GO

-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteColumnID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_Sites]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([SiteActivityRuleID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_Sites]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([SiteID]);
GO

-- Creating foreign key on [ViewTypeID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_ViewTypes]
    FOREIGN KEY ([ViewTypeID])
    REFERENCES [dbo].[tbl_ViewTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_ViewTypes'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_ViewTypes]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([ViewTypeID]);
GO

-- Creating foreign key on [TemplateID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template]
    FOREIGN KEY ([TemplateID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template]
ON [dbo].[tbl_SiteActivityRules]
    ([TemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_Sites]
ON [dbo].[tbl_SiteActivityRules]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityScoreType'
ALTER TABLE [dbo].[tbl_SiteActivityScoreType]
ADD CONSTRAINT [FK_tbl_SiteActivityScoreType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityScoreType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityScoreType_tbl_Sites]
ON [dbo].[tbl_SiteActivityScoreType]
    ([SiteID]);
GO

-- Creating foreign key on [SiteActivityScoreTypeID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType]
    FOREIGN KEY ([SiteActivityScoreTypeID])
    REFERENCES [dbo].[tbl_SiteActivityScoreType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_SiteActivityScoreType]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteActivityScoreTypeID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteColumns'
ALTER TABLE [dbo].[tbl_SiteColumns]
ADD CONSTRAINT [FK_tbl_SiteColumns_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumns_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteColumns_tbl_Sites]
ON [dbo].[tbl_SiteColumns]
    ([SiteID]);
GO

-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteColumnValues'
ALTER TABLE [dbo].[tbl_SiteColumnValues]
ADD CONSTRAINT [FK_tbl_SiteColumnValues_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteColumnValues_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteColumnValues_tbl_SiteColumns]
ON [dbo].[tbl_SiteColumnValues]
    ([SiteColumnID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteDomain'
ALTER TABLE [dbo].[tbl_SiteDomain]
ADD CONSTRAINT [FK_tbl_SiteDomain_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteDomain_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteDomain_tbl_Sites]
ON [dbo].[tbl_SiteDomain]
    ([SiteID]);
GO

-- Creating foreign key on [SiteDomainID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [FK_tbl_WebSite_tbl_SiteDomain]
    FOREIGN KEY ([SiteDomainID])
    REFERENCES [dbo].[tbl_SiteDomain]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSite_tbl_SiteDomain'
CREATE INDEX [IX_FK_tbl_WebSite_tbl_SiteDomain]
ON [dbo].[tbl_WebSite]
    ([SiteDomainID]);
GO

-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteEventTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_Sites]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([SiteID]);
GO

-- Creating foreign key on [StartAfterTypeID] in table 'tbl_SiteEventActionTemplate'
ALTER TABLE [dbo].[tbl_SiteEventActionTemplate]
ADD CONSTRAINT [FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes]
    FOREIGN KEY ([StartAfterTypeID])
    REFERENCES [dbo].[tbl_StartAfterTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes'
CREATE INDEX [IX_FK_tbl_SiteEventActionTemplate_tbl_StartAfterTypes]
ON [dbo].[tbl_SiteEventActionTemplate]
    ([StartAfterTypeID]);
GO

-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([SiteEventTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplateActivity'
ALTER TABLE [dbo].[tbl_SiteEventTemplateActivity]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateActivity_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateActivity_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateActivity_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplateActivity]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplates'
ALTER TABLE [dbo].[tbl_SiteEventTemplates]
ADD CONSTRAINT [FK_tbl_SiteEventTemplates_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplates_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplates_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplates]
    ([SiteID]);
GO

-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteEventTemplateID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SiteEventTemplateScore'
ALTER TABLE [dbo].[tbl_SiteEventTemplateScore]
ADD CONSTRAINT [FK_tbl_SiteEventTemplateScore_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteEventTemplateScore_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteEventTemplateScore_tbl_Sites]
ON [dbo].[tbl_SiteEventTemplateScore]
    ([SiteID]);
GO

-- Creating foreign key on [MainUserID] in table 'tbl_Sites'
ALTER TABLE [dbo].[tbl_Sites]
ADD CONSTRAINT [FK_tbl_Sites_tbl_User]
    FOREIGN KEY ([MainUserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Sites_tbl_User'
CREATE INDEX [IX_FK_tbl_Sites_tbl_User]
ON [dbo].[tbl_Sites]
    ([MainUserID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SourceMonitoring'
ALTER TABLE [dbo].[tbl_SourceMonitoring]
ADD CONSTRAINT [FK_tbl_SourceMonitoring_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoring_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SourceMonitoring_tbl_Sites]
ON [dbo].[tbl_SourceMonitoring]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoringFilter_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SourceMonitoringFilter_tbl_Sites]
ON [dbo].[tbl_SourceMonitoringFilter]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_StatisticData'
ALTER TABLE [dbo].[tbl_StatisticData]
ADD CONSTRAINT [FK_tbl_StatisticData_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_StatisticData_tbl_Sites'
CREATE INDEX [IX_FK_tbl_StatisticData_tbl_Sites]
ON [dbo].[tbl_StatisticData]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_TaskResult'
ALTER TABLE [dbo].[tbl_TaskResult]
ADD CONSTRAINT [FK_tbl_TaskResult_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskResult_tbl_Sites'
CREATE INDEX [IX_FK_tbl_TaskResult_tbl_Sites]
ON [dbo].[tbl_TaskResult]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_Sites]
ON [dbo].[tbl_TaskType]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Term'
ALTER TABLE [dbo].[tbl_Term]
ADD CONSTRAINT [FK_tbl_Term_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Term_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Term_tbl_Sites]
ON [dbo].[tbl_Term]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_User'
ALTER TABLE [dbo].[tbl_User]
ADD CONSTRAINT [FK_tbl_User_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_User_tbl_Sites'
CREATE INDEX [IX_FK_tbl_User_tbl_Sites]
ON [dbo].[tbl_User]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_WebSite'
ALTER TABLE [dbo].[tbl_WebSite]
ADD CONSTRAINT [FK_tbl_WebSite_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSite_tbl_Sites'
CREATE INDEX [IX_FK_tbl_WebSite_tbl_Sites]
ON [dbo].[tbl_WebSite]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_Sites]
ON [dbo].[tbl_Workflow]
    ([SiteID]);
GO

-- Creating foreign key on [SiteID] in table 'tbl_WorkflowTemplate'
ALTER TABLE [dbo].[tbl_WorkflowTemplate]
ADD CONSTRAINT [FK_tbl_WorkflowTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_WorkflowTemplate_tbl_Sites]
ON [dbo].[tbl_WorkflowTemplate]
    ([SiteID]);
GO

-- Creating foreign key on [SiteTagID] in table 'tbl_SiteTagObjects'
ALTER TABLE [dbo].[tbl_SiteTagObjects]
ADD CONSTRAINT [FK_tbl_SiteTagObjects_tbl_SiteTags]
    FOREIGN KEY ([SiteTagID])
    REFERENCES [dbo].[tbl_SiteTags]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTagObjects_tbl_SiteTags'
CREATE INDEX [IX_FK_tbl_SiteTagObjects_tbl_SiteTags]
ON [dbo].[tbl_SiteTagObjects]
    ([SiteTagID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_SiteTags'
ALTER TABLE [dbo].[tbl_SiteTags]
ADD CONSTRAINT [FK_tbl_SiteTags_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteTags_tbl_User'
CREATE INDEX [IX_FK_tbl_SiteTags_tbl_User]
ON [dbo].[tbl_SiteTags]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'tbl_SocialAuthorizationToken'
ALTER TABLE [dbo].[tbl_SocialAuthorizationToken]
ADD CONSTRAINT [FK_tbl_SocialAuthorizationToken_tbl_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[tbl_User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SocialAuthorizationToken_tbl_User'
CREATE INDEX [IX_FK_tbl_SocialAuthorizationToken_tbl_User]
ON [dbo].[tbl_SocialAuthorizationToken]
    ([UserID]);
GO

-- Creating foreign key on [SourceMonitoringID] in table 'tbl_SourceMonitoringFilter'
ALTER TABLE [dbo].[tbl_SourceMonitoringFilter]
ADD CONSTRAINT [FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring]
    FOREIGN KEY ([SourceMonitoringID])
    REFERENCES [dbo].[tbl_SourceMonitoring]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring'
CREATE INDEX [IX_FK_tbl_SourceMonitoringFilter_tbl_SourceMonitoring]
ON [dbo].[tbl_SourceMonitoringFilter]
    ([SourceMonitoringID]);
GO

-- Creating foreign key on [TaskResultID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_TaskResult]
    FOREIGN KEY ([TaskResultID])
    REFERENCES [dbo].[tbl_TaskResult]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_TaskResult'
CREATE INDEX [IX_FK_tbl_Task_tbl_TaskResult]
ON [dbo].[tbl_Task]
    ([TaskResultID]);
GO

-- Creating foreign key on [TaskTypeID] in table 'tbl_Task'
ALTER TABLE [dbo].[tbl_Task]
ADD CONSTRAINT [FK_tbl_Task_tbl_TaskType]
    FOREIGN KEY ([TaskTypeID])
    REFERENCES [dbo].[tbl_TaskType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Task_tbl_TaskType'
CREATE INDEX [IX_FK_tbl_Task_tbl_TaskType]
ON [dbo].[tbl_Task]
    ([TaskTypeID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_TaskDuration'
ALTER TABLE [dbo].[tbl_TaskDuration]
ADD CONSTRAINT [FK_tbl_TaskDuration_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskDuration_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskDuration_tbl_Task]
ON [dbo].[tbl_TaskDuration]
    ([TaskID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_Task]
ON [dbo].[tbl_TaskHistory]
    ([TaskID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Task]
ON [dbo].[tbl_TaskMember]
    ([TaskID]);
GO

-- Creating foreign key on [TaskID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskPersonalComment_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskPersonalComment_tbl_Task]
ON [dbo].[tbl_TaskPersonalComment]
    ([TaskID]);
GO

-- Creating foreign key on [TaskResultID] in table 'tbl_TaskHistory'
ALTER TABLE [dbo].[tbl_TaskHistory]
ADD CONSTRAINT [FK_tbl_TaskHistory_tbl_TaskResult]
    FOREIGN KEY ([TaskResultID])
    REFERENCES [dbo].[tbl_TaskResult]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskHistory_tbl_TaskResult'
CREATE INDEX [IX_FK_tbl_TaskHistory_tbl_TaskResult]
ON [dbo].[tbl_TaskHistory]
    ([TaskResultID]);
GO

-- Creating foreign key on [TaskMembersCountID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskMembersCount]
    FOREIGN KEY ([TaskMembersCountID])
    REFERENCES [dbo].[tbl_TaskMembersCount]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskMembersCount'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskMembersCount]
ON [dbo].[tbl_TaskType]
    ([TaskMembersCountID]);
GO

-- Creating foreign key on [TaskTypeAdjustDurationID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeAdjustDuration]
    FOREIGN KEY ([TaskTypeAdjustDurationID])
    REFERENCES [dbo].[tbl_TaskTypeAdjustDuration]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypeAdjustDuration'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypeAdjustDuration]
ON [dbo].[tbl_TaskType]
    ([TaskTypeAdjustDurationID]);
GO

-- Creating foreign key on [TaskTypeCategoryID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypeCategory]
    FOREIGN KEY ([TaskTypeCategoryID])
    REFERENCES [dbo].[tbl_TaskTypeCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypeCategory'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypeCategory]
ON [dbo].[tbl_TaskType]
    ([TaskTypeCategoryID]);
GO

-- Creating foreign key on [TaskTypePaymentSchemeID] in table 'tbl_TaskType'
ALTER TABLE [dbo].[tbl_TaskType]
ADD CONSTRAINT [FK_tbl_TaskType_tbl_TaskTypePaymentScheme]
    FOREIGN KEY ([TaskTypePaymentSchemeID])
    REFERENCES [dbo].[tbl_TaskTypePaymentScheme]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskType_tbl_TaskTypePaymentScheme'
CREATE INDEX [IX_FK_tbl_TaskType_tbl_TaskTypePaymentScheme]
ON [dbo].[tbl_TaskType]
    ([TaskTypePaymentSchemeID]);
GO

-- Creating foreign key on [WebSiteID] in table 'tbl_WebSitePage'
ALTER TABLE [dbo].[tbl_WebSitePage]
ADD CONSTRAINT [FK_tbl_WebSitePage_tbl_WebSite]
    FOREIGN KEY ([WebSiteID])
    REFERENCES [dbo].[tbl_WebSite]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSitePage_tbl_WebSite'
CREATE INDEX [IX_FK_tbl_WebSitePage_tbl_WebSite]
ON [dbo].[tbl_WebSitePage]
    ([WebSiteID]);
GO

-- Creating foreign key on [WidgetCategoryID] in table 'tbl_Widget'
ALTER TABLE [dbo].[tbl_Widget]
ADD CONSTRAINT [FK_tbl_Widget_tbl_WidgetCategory]
    FOREIGN KEY ([WidgetCategoryID])
    REFERENCES [dbo].[tbl_WidgetCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Widget_tbl_WidgetCategory'
CREATE INDEX [IX_FK_tbl_Widget_tbl_WidgetCategory]
ON [dbo].[tbl_Widget]
    ([WidgetCategoryID]);
GO

-- Creating foreign key on [WidgetID] in table 'tbl_WidgetToAccessProfile'
ALTER TABLE [dbo].[tbl_WidgetToAccessProfile]
ADD CONSTRAINT [FK_tbl_WidgetToAccessProfile_tbl_Widget]
    FOREIGN KEY ([WidgetID])
    REFERENCES [dbo].[tbl_Widget]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetToAccessProfile_tbl_Widget'
CREATE INDEX [IX_FK_tbl_WidgetToAccessProfile_tbl_Widget]
ON [dbo].[tbl_WidgetToAccessProfile]
    ([WidgetID]);
GO

-- Creating foreign key on [ParentID] in table 'tbl_WidgetCategory'
ALTER TABLE [dbo].[tbl_WidgetCategory]
ADD CONSTRAINT [FK_tbl_WidgetCategory_tbl_WidgetCategory]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_WidgetCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WidgetCategory_tbl_WidgetCategory'
CREATE INDEX [IX_FK_tbl_WidgetCategory_tbl_WidgetCategory]
ON [dbo].[tbl_WidgetCategory]
    ([ParentID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_Workflow'
ALTER TABLE [dbo].[tbl_Workflow]
ADD CONSTRAINT [FK_tbl_Workflow_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Workflow_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_Workflow_tbl_WorkflowTemplate]
ON [dbo].[tbl_Workflow]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [FK_tbl_WorkflowElement_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowElement_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_WorkflowElement_tbl_Workflow]
ON [dbo].[tbl_WorkflowElement]
    ([WorkflowID]);
GO

-- Creating foreign key on [WorkflowID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [FK_tbl_WorkflowParameter_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowParameter_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_WorkflowParameter_tbl_Workflow]
ON [dbo].[tbl_WorkflowParameter]
    ([WorkflowID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowElement'
ALTER TABLE [dbo].[tbl_WorkflowElement]
ADD CONSTRAINT [FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowElement_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowElement]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateParameterID] in table 'tbl_WorkflowParameter'
ALTER TABLE [dbo].[tbl_WorkflowParameter]
ADD CONSTRAINT [FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter]
    FOREIGN KEY ([WorkflowTemplateParameterID])
    REFERENCES [dbo].[tbl_WorkflowTemplateParameter]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter'
CREATE INDEX [IX_FK_tbl_WorkflowParameter_tbl_WorkflowTemplateParameter]
ON [dbo].[tbl_WorkflowParameter]
    ([WorkflowTemplateParameterID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateConditionEvent]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElement_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateElement]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateElementRelation'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementRelation]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementRelation_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateElementRelation]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateGoal'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoal]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateGoal_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateGoal]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateParameter]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateParameter_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateParameter]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateID] in table 'tbl_WorkflowTemplateRole'
ALTER TABLE [dbo].[tbl_WorkflowTemplateRole]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate]
    FOREIGN KEY ([WorkflowTemplateID])
    REFERENCES [dbo].[tbl_WorkflowTemplate]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateRole_tbl_WorkflowTemplate]
ON [dbo].[tbl_WorkflowTemplateRole]
    ([WorkflowTemplateID]);
GO

-- Creating foreign key on [WorkflowTemplateElementEventID] in table 'tbl_WorkflowTemplateConditionEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateConditionEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent]
    FOREIGN KEY ([WorkflowTemplateElementEventID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElementEvent]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateConditionEvent_tbl_WorkflowTemplateElementEvent]
ON [dbo].[tbl_WorkflowTemplateConditionEvent]
    ([WorkflowTemplateElementEventID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementEvent'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementEvent]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementEvent_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementEvent]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementExternalRequest'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementExternalRequest]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementExternalRequest_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementExternalRequest]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementParameter'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementParameter]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementParameter_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementParameter]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementPeriod'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementPeriod]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementPeriod_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementPeriod]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementResult'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementResult]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementResult_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementResult]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [WorkflowTemplateElementID] in table 'tbl_WorkflowTemplateElementTag'
ALTER TABLE [dbo].[tbl_WorkflowTemplateElementTag]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([WorkflowTemplateElementID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateElementTag_tbl_WorkflowTemplateElement]
ON [dbo].[tbl_WorkflowTemplateElementTag]
    ([WorkflowTemplateElementID]);
GO

-- Creating foreign key on [tbl_AccessProfileModule_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_AccessProfileModule]
    FOREIGN KEY ([tbl_AccessProfileModule_ID])
    REFERENCES [dbo].[tbl_AccessProfileModule]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_ModuleEditionOption_ID] in table 'tbl_AccessProfileModuleEditionOption'
ALTER TABLE [dbo].[tbl_AccessProfileModuleEditionOption]
ADD CONSTRAINT [FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption]
    FOREIGN KEY ([tbl_ModuleEditionOption_ID])
    REFERENCES [dbo].[tbl_ModuleEditionOption]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption'
CREATE INDEX [IX_FK_tbl_AccessProfileModuleEditionOption_tbl_ModuleEditionOption]
ON [dbo].[tbl_AccessProfileModuleEditionOption]
    ([tbl_ModuleEditionOption_ID]);
GO

-- Creating foreign key on [tbl_Invoice_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Invoice]
    FOREIGN KEY ([tbl_Invoice_ID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_Shipment_ID] in table 'tbl_InvoiceToShipment'
ALTER TABLE [dbo].[tbl_InvoiceToShipment]
ADD CONSTRAINT [FK_tbl_InvoiceToShipment_tbl_Shipment]
    FOREIGN KEY ([tbl_Shipment_ID])
    REFERENCES [dbo].[tbl_Shipment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_InvoiceToShipment_tbl_Shipment'
CREATE INDEX [IX_FK_tbl_InvoiceToShipment_tbl_Shipment]
ON [dbo].[tbl_InvoiceToShipment]
    ([tbl_Shipment_ID]);
GO

-- Creating foreign key on [tbl_Request_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Request]
    FOREIGN KEY ([tbl_Request_ID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_Requirement_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Requirement]
    FOREIGN KEY ([tbl_Requirement_ID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestToRequirement_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_RequestToRequirement_tbl_Requirement]
ON [dbo].[tbl_RequestToRequirement]
    ([tbl_Requirement_ID]);
GO

-- Creating foreign key on [tbl_ExternalResource_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_ExternalResource]
    FOREIGN KEY ([tbl_ExternalResource_ID])
    REFERENCES [dbo].[tbl_ExternalResource]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_WebSitePage_ID] in table 'tbl_WebSitePageExternalResource'
ALTER TABLE [dbo].[tbl_WebSitePageExternalResource]
ADD CONSTRAINT [FK_tbl_WebSitePageExternalResource_tbl_WebSitePage]
    FOREIGN KEY ([tbl_WebSitePage_ID])
    REFERENCES [dbo].[tbl_WebSitePage]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WebSitePageExternalResource_tbl_WebSitePage'
CREATE INDEX [IX_FK_tbl_WebSitePageExternalResource_tbl_WebSitePage]
ON [dbo].[tbl_WebSitePageExternalResource]
    ([tbl_WebSitePage_ID]);
GO

-- Creating foreign key on [tbl_WorkflowTemplateElement_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateElement]
    FOREIGN KEY ([tbl_WorkflowTemplateElement_ID])
    REFERENCES [dbo].[tbl_WorkflowTemplateElement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [tbl_WorkflowTemplateGoal_ID] in table 'tbl_WorkflowTemplateGoalElement'
ALTER TABLE [dbo].[tbl_WorkflowTemplateGoalElement]
ADD CONSTRAINT [FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal]
    FOREIGN KEY ([tbl_WorkflowTemplateGoal_ID])
    REFERENCES [dbo].[tbl_WorkflowTemplateGoal]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal'
CREATE INDEX [IX_FK_tbl_WorkflowTemplateGoalElement_tbl_WorkflowTemplateGoal]
ON [dbo].[tbl_WorkflowTemplateGoalElement]
    ([tbl_WorkflowTemplateGoal_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------