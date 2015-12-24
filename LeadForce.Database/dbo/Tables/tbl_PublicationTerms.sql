-- Creating table 'tbl_PublicationTerms'
CREATE TABLE [dbo].[tbl_PublicationTerms] (
    [ID] uniqueidentifier  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [Term] nvarchar(250)  NOT NULL,
    [PublicationCode] nvarchar(250)  NOT NULL,
    [ElementCode] nvarchar(250)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [PublicationID] in table 'tbl_PublicationTerms'
ALTER TABLE [dbo].[tbl_PublicationTerms]
ADD CONSTRAINT [FK_tbl_PublicationTerms_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_PublicationTerms'
ALTER TABLE [dbo].[tbl_PublicationTerms]
ADD CONSTRAINT [PK_tbl_PublicationTerms]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_PublicationTerms_tbl_Publication'
CREATE INDEX [IX_FK_tbl_PublicationTerms_tbl_Publication]
ON [dbo].[tbl_PublicationTerms]
    ([PublicationID]);