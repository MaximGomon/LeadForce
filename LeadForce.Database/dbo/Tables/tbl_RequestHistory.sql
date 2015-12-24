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
-- Creating foreign key on [ContactID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [FK_tbl_RequestHistory_tbl_Request]
    FOREIGN KEY ([RequestID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequestHistory'
ALTER TABLE [dbo].[tbl_RequestHistory]
ADD CONSTRAINT [PK_tbl_RequestHistory]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Contact'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Contact]
ON [dbo].[tbl_RequestHistory]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Contact_Responsible]
ON [dbo].[tbl_RequestHistory]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestHistory_tbl_Request'
CREATE INDEX [IX_FK_tbl_RequestHistory_tbl_Request]
ON [dbo].[tbl_RequestHistory]
    ([RequestID]);