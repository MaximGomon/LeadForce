-- Creating table 'tbl_OperatingSystems'
CREATE TABLE [dbo].[tbl_OperatingSystems] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Version] nvarchar(255)  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_OperatingSystems'
ALTER TABLE [dbo].[tbl_OperatingSystems]
ADD CONSTRAINT [FK_tbl_OperatingSystems_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_OperatingSystems'
ALTER TABLE [dbo].[tbl_OperatingSystems]
ADD CONSTRAINT [PK_tbl_OperatingSystems]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_OperatingSystems_tbl_Sites'
CREATE INDEX [IX_FK_tbl_OperatingSystems_tbl_Sites]
ON [dbo].[tbl_OperatingSystems]
    ([SiteID]);