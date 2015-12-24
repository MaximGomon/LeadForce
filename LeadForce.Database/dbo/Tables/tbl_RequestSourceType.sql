-- Creating table 'tbl_RequestSourceType'
CREATE TABLE [dbo].[tbl_RequestSourceType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [RequestSourceCategoryID] int  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestSourceCategoryID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceCategoryID])
    REFERENCES [dbo].[tbl_RequestSourceCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [FK_tbl_RequestSourceType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequestSourceType'
ALTER TABLE [dbo].[tbl_RequestSourceType]
ADD CONSTRAINT [PK_tbl_RequestSourceType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_Numerator]
ON [dbo].[tbl_RequestSourceType]
    ([NumeratorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_RequestSourceType]
ON [dbo].[tbl_RequestSourceType]
    ([RequestSourceCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestSourceType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequestSourceType_tbl_Sites]
ON [dbo].[tbl_RequestSourceType]
    ([SiteID]);