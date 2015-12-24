-- Creating table 'tbl_SiteActionTemplateUserColumn'
CREATE TABLE [dbo].[tbl_SiteActionTemplateUserColumn] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteEventTemplateID] uniqueidentifier  NOT NULL,
    [SiteColumnID] uniqueidentifier  NOT NULL,
    [StringValue] nvarchar(255)  NULL,
    [DateValue] datetime  NULL,
    [SiteColumnValueID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [SiteColumnID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns]
    FOREIGN KEY ([SiteColumnID])
    REFERENCES [dbo].[tbl_SiteColumns]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteColumnValueID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues]
    FOREIGN KEY ([SiteColumnValueID])
    REFERENCES [dbo].[tbl_SiteColumnValues]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteEventTemplateID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates]
    FOREIGN KEY ([SiteEventTemplateID])
    REFERENCES [dbo].[tbl_SiteEventTemplates]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [FK_tbl_SiteActionTemplateUserColumn_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActionTemplateUserColumn'
ALTER TABLE [dbo].[tbl_SiteActionTemplateUserColumn]
ADD CONSTRAINT [PK_tbl_SiteActionTemplateUserColumn]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumns]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteColumnID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteColumnValues]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteColumnValueID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_SiteEventTemplates]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteEventTemplateID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActionTemplateUserColumn_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActionTemplateUserColumn_tbl_Sites]
ON [dbo].[tbl_SiteActionTemplateUserColumn]
    ([SiteID]);