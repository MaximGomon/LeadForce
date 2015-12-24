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
-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleLayout_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleLayout'
ALTER TABLE [dbo].[tbl_SiteActivityRuleLayout]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleLayout]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteActivityRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_SiteColumns]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteColumnID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleLayout_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleLayout_tbl_Sites]
ON [dbo].[tbl_SiteActivityRuleLayout]
    ([SiteID]);