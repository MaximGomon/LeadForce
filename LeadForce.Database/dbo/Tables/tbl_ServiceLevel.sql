-- Creating table 'tbl_ServiceLevel'
CREATE TABLE [dbo].[tbl_ServiceLevel] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ReactionTime] int  NOT NULL,
    [ResponseTime] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_ServiceLevel'
ALTER TABLE [dbo].[tbl_ServiceLevel]
ADD CONSTRAINT [FK_tbl_ServiceLevel_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ServiceLevel'
ALTER TABLE [dbo].[tbl_ServiceLevel]
ADD CONSTRAINT [PK_tbl_ServiceLevel]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ServiceLevel_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ServiceLevel_tbl_Sites]
ON [dbo].[tbl_ServiceLevel]
    ([SiteID]);