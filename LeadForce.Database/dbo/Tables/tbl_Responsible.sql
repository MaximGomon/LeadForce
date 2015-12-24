-- Creating table 'tbl_Responsible'
CREATE TABLE [dbo].[tbl_Responsible] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactRoleID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [WorkflowID] uniqueidentifier  NULL,
    [ResponsibleID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Contact1]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactRoleID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_ContactRole]
    FOREIGN KEY ([ContactRoleID])
    REFERENCES [dbo].[tbl_ContactRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [WorkflowID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [FK_tbl_Responsible_tbl_Workflow]
    FOREIGN KEY ([WorkflowID])
    REFERENCES [dbo].[tbl_Workflow]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Responsible'
ALTER TABLE [dbo].[tbl_Responsible]
ADD CONSTRAINT [PK_tbl_Responsible]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Contact]
ON [dbo].[tbl_Responsible]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Contact1'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Contact1]
ON [dbo].[tbl_Responsible]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_ContactRole'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_ContactRole]
ON [dbo].[tbl_Responsible]
    ([ContactRoleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Responsible_tbl_Workflow'
CREATE INDEX [IX_FK_tbl_Responsible_tbl_Workflow]
ON [dbo].[tbl_Responsible]
    ([WorkflowID]);