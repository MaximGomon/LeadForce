-- Creating table 'tbl_SiteActivityRuleExternalForms'
CREATE TABLE [dbo].[tbl_SiteActivityRuleExternalForms] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleExternalForms'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalForms]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleExternalForms'
ALTER TABLE [dbo].[tbl_SiteActivityRuleExternalForms]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleExternalForms]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleExternalForms_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleExternalForms]
    ([SiteActivityRuleID]);