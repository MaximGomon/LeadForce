-- Creating table 'tbl_Contract'
CREATE TABLE [dbo].[tbl_Contract] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [Number] nvarchar(256)  NULL,
    [SerialNumber] int  NULL,
    [ClientID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ClientID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [FK_tbl_Contract_tbl_Company]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [FK_tbl_Contract_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Contract'
ALTER TABLE [dbo].[tbl_Contract]
ADD CONSTRAINT [PK_tbl_Contract]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contract_tbl_Company'
CREATE INDEX [IX_FK_tbl_Contract_tbl_Company]
ON [dbo].[tbl_Contract]
    ([ClientID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Contract_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Contract_tbl_Sites]
ON [dbo].[tbl_Contract]
    ([SiteID]);