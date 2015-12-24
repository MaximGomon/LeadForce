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
-- Creating foreign key on [RuleTypeID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_RuleTypes]
    FOREIGN KEY ([RuleTypeID])
    REFERENCES [dbo].[tbl_RuleTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TemplateID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template]
    FOREIGN KEY ([TemplateID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [FK_tbl_SiteActivityRules_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRules'
ALTER TABLE [dbo].[tbl_SiteActivityRules]
ADD CONSTRAINT [PK_tbl_SiteActivityRules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_RuleTypes'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_RuleTypes]
ON [dbo].[tbl_SiteActivityRules]
    ([RuleTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_SiteActivityRules_Template]
ON [dbo].[tbl_SiteActivityRules]
    ([TemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRules_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRules_tbl_Sites]
ON [dbo].[tbl_SiteActivityRules]
    ([SiteID]);