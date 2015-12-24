-- Creating table 'tbl_PriceList'
CREATE TABLE [dbo].[tbl_PriceList] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [PriceListTypeID] int  NOT NULL,
    [PriceListStatusID] int  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [SiteID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [PriceListStatusID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_PriceListStatus]
    FOREIGN KEY ([PriceListStatusID])
    REFERENCES [dbo].[tbl_PriceListStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PriceListTypeID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_PriceListType]
    FOREIGN KEY ([PriceListTypeID])
    REFERENCES [dbo].[tbl_PriceListType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [FK_tbl_PriceList_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PriceList'
ALTER TABLE [dbo].[tbl_PriceList]
ADD CONSTRAINT [PK_tbl_PriceList]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_PriceListStatus'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_PriceListStatus]
ON [dbo].[tbl_PriceList]
    ([PriceListStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_PriceListType'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_PriceListType]
ON [dbo].[tbl_PriceList]
    ([PriceListTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PriceList_tbl_Sites'
CREATE INDEX [IX_FK_tbl_PriceList_tbl_Sites]
ON [dbo].[tbl_PriceList]
    ([SiteID]);