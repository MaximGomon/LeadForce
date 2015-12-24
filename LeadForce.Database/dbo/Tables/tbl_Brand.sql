-- Creating table 'tbl_Brand'
CREATE TABLE [dbo].[tbl_Brand] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Brand'
ALTER TABLE [dbo].[tbl_Brand]
ADD CONSTRAINT [FK_tbl_Brand_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Brand'
ALTER TABLE [dbo].[tbl_Brand]
ADD CONSTRAINT [PK_tbl_Brand]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Brand_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Brand_tbl_Sites]
ON [dbo].[tbl_Brand]
    ([SiteID]);