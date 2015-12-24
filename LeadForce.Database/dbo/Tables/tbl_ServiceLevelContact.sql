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
-- Creating foreign key on [ContactID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceInformCatalogID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog]
    FOREIGN KEY ([InvoiceInformCatalogID])
    REFERENCES [dbo].[tbl_InvoiceInformCatalog]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceInformFormID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm]
    FOREIGN KEY ([InvoiceInformFormID])
    REFERENCES [dbo].[tbl_InvoiceInformForm]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceLevelClientID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient]
    FOREIGN KEY ([ServiceLevelClientID])
    REFERENCES [dbo].[tbl_ServiceLevelClient]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [IncludeToInformID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform]
    FOREIGN KEY ([IncludeToInformID])
    REFERENCES [dbo].[tbl_ServiceLevelIncludeToInform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InformRequestID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform]
    FOREIGN KEY ([InformRequestID])
    REFERENCES [dbo].[tbl_ServiceLevelInform]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InformCommentID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment]
    FOREIGN KEY ([InformCommentID])
    REFERENCES [dbo].[tbl_ServiceLevelInformComment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceLevelRoleID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole]
    FOREIGN KEY ([ServiceLevelRoleID])
    REFERENCES [dbo].[tbl_ServiceLevelRole]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevelContact'
ALTER TABLE [dbo].[tbl_ServiceLevelContact]
ADD CONSTRAINT [PK_tbl_ServiceLevelContact]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_Contact]
ON [dbo].[tbl_ServiceLevelContact]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_InvoiceInformCatalog]
ON [dbo].[tbl_ServiceLevelContact]
    ([InvoiceInformCatalogID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_InvoiceInformForm]
ON [dbo].[tbl_ServiceLevelContact]
    ([InvoiceInformFormID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelClient]
ON [dbo].[tbl_ServiceLevelContact]
    ([ServiceLevelClientID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelIncludeToInform]
ON [dbo].[tbl_ServiceLevelContact]
    ([IncludeToInformID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelInform]
ON [dbo].[tbl_ServiceLevelContact]
    ([InformRequestID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelInformComment]
ON [dbo].[tbl_ServiceLevelContact]
    ([InformCommentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole'
CREATE INDEX [IX_FK_tbl_ServiceLevelContact_tbl_ServiceLevelRole]
ON [dbo].[tbl_ServiceLevelContact]
    ([ServiceLevelRoleID]);