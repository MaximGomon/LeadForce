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
-- Creating foreign key on [AccessProfileID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_AccessProfile]
    FOREIGN KEY ([AccessProfileID])
    REFERENCES [dbo].[tbl_AccessProfile]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ModuleID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [FK_tbl_AccessProfileRecord_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_AccessProfileRecord'
ALTER TABLE [dbo].[tbl_AccessProfileRecord]
ADD CONSTRAINT [PK_tbl_AccessProfileRecord]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileRecord_tbl_AccessProfile'
CREATE INDEX [IX_FK_tbl_AccessProfileRecord_tbl_AccessProfile]
ON [dbo].[tbl_AccessProfileRecord]
    ([AccessProfileID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_AccessProfileRecord_tbl_Module'
CREATE INDEX [IX_FK_tbl_AccessProfileRecord_tbl_Module]
ON [dbo].[tbl_AccessProfileRecord]
    ([ModuleID]);