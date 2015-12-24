-- Creating table 'tbl_Resolutions'
CREATE TABLE [dbo].[tbl_Resolutions] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Value] varchar(15)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Resolutions'
ALTER TABLE [dbo].[tbl_Resolutions]
ADD CONSTRAINT [FK_tbl_Resolutions_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Resolutions'
ALTER TABLE [dbo].[tbl_Resolutions]
ADD CONSTRAINT [PK_tbl_Resolutions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Resolutions_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Resolutions_tbl_Sites]
ON [dbo].[tbl_Resolutions]
    ([SiteID]);