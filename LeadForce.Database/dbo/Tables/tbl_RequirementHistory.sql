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
-- Creating foreign key on [ContactID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_ResponsibleContact]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_Requirement]
    FOREIGN KEY ([RequirementID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementStatusID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [FK_tbl_RequirementHistory_tbl_RequirementStatus]
    FOREIGN KEY ([RequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementHistory'
ALTER TABLE [dbo].[tbl_RequirementHistory]
ADD CONSTRAINT [PK_tbl_RequirementHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_Contact]
ON [dbo].[tbl_RequirementHistory]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_ResponsibleContact'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_ResponsibleContact]
ON [dbo].[tbl_RequirementHistory]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_Requirement]
ON [dbo].[tbl_RequirementHistory]
    ([RequirementID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementHistory_tbl_RequirementStatus'
CREATE INDEX [IX_FK_tbl_RequirementHistory_tbl_RequirementStatus]
ON [dbo].[tbl_RequirementHistory]
    ([RequirementStatusID]);