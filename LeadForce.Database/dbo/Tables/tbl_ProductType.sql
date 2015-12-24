-- Creating table 'tbl_ProductType'
CREATE TABLE [dbo].[tbl_ProductType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(250)  NOT NULL,
    [Order] int  NULL,
    [IsDefault] bit  NOT NULL,
    [ProductWorkWithComplectationID] int  NULL
);
GO
-- Creating foreign key on [ProductWorkWithComplectationID] in table 'tbl_ProductType'
ALTER TABLE [dbo].[tbl_ProductType]
ADD CONSTRAINT [FK_tbl_ProductType_tbl_ProductWorkWithComplectation]
    FOREIGN KEY ([ProductWorkWithComplectationID])
    REFERENCES [dbo].[tbl_ProductWorkWithComplectation]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ProductType'
ALTER TABLE [dbo].[tbl_ProductType]
ADD CONSTRAINT [PK_tbl_ProductType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductType_tbl_ProductWorkWithComplectation'
CREATE INDEX [IX_FK_tbl_ProductType_tbl_ProductWorkWithComplectation]
ON [dbo].[tbl_ProductType]
    ([ProductWorkWithComplectationID]);