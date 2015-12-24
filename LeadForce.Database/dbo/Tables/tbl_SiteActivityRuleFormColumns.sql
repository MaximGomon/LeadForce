-- Creating table 'tbl_SiteActivityRuleFormColumns'
CREATE TABLE [dbo].[tbl_SiteActivityRuleFormColumns] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleFormColumns'
ALTER TABLE [dbo].[tbl_SiteActivityRuleFormColumns]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleFormColumns]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleFormColumns_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleFormColumns]
    ([SiteActivityRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleFormColumns_tbl_SiteColumns]
ON [dbo].[tbl_SiteActivityRuleFormColumns]
    ([SiteColumnID]);