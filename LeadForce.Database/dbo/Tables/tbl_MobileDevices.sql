-- Creating table 'tbl_MobileDevices'
CREATE TABLE [dbo].[tbl_MobileDevices] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_MobileDevices'
ALTER TABLE [dbo].[tbl_MobileDevices]
ADD CONSTRAINT [FK_tbl_MobileDevices_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_MobileDevices'
ALTER TABLE [dbo].[tbl_MobileDevices]
ADD CONSTRAINT [PK_tbl_MobileDevices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_MobileDevices_tbl_Sites'
CREATE INDEX [IX_FK_tbl_MobileDevices_tbl_Sites]
ON [dbo].[tbl_MobileDevices]
    ([SiteID]);