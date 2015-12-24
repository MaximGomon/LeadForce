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
-- Creating foreign key on [SiteActivityRuleExternalFormID] in table 'tbl_SiteActivityRuleExternalFormFields'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms]
    FOREIGN KEY ([SiteActivityRuleExternalFormID])
    REFERENCES [dbo].[tbl_SiteActivityRuleExternalForms]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleExternalFormFields'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalFormFields]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleExternalFormFields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleExternalFormFields_tbl_SiteActivityRuleExternalForms]
ON [dbo].[tbl_SiteActivityRuleExternalFormFields]
    ([SiteActivityRuleExternalFormID]);