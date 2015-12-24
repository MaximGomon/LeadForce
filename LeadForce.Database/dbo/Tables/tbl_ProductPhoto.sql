-- Creating table 'tbl_ProductPhoto'
CREATE TABLE [dbo].[tbl_ProductPhoto] (
    [Id] uniqueidentifier  NOT NULL,
    [ProductId] uniqueidentifier  NOT NULL,
    [Photo] nvarchar(250)  NOT NULL,
    [Preview] nvarchar(250)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [IsMain] bit  NOT NULL
);
GO
-- Creating foreign key on [ProductId] in table 'tbl_ProductPhoto'
ALTER TABLE [dbo].[tbl_ProductPhoto]
ADD CONSTRAINT [FK_tbl_ProductPhoto_tbl_ProductPhoto]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [Id] in table 'tbl_ProductPhoto'
ALTER TABLE [dbo].[tbl_ProductPhoto]
ADD CONSTRAINT [PK_tbl_ProductPhoto]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ProductPhoto_tbl_ProductPhoto'
CREATE INDEX [IX_FK_tbl_ProductPhoto_tbl_ProductPhoto]
ON [dbo].[tbl_ProductPhoto]
    ([ProductId]);