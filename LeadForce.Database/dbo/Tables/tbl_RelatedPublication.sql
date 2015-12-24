-- Creating table 'tbl_RelatedPublication'
CREATE TABLE [dbo].[tbl_RelatedPublication] (
    [ID] uniqueidentifier  NOT NULL,
    [PublicationID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NOT NULL,
    [RecordID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [ModuleID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [FK_tbl_RelatedPublication_tbl_Module]
    FOREIGN KEY ([ModuleID])
    REFERENCES [dbo].[tbl_Module]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [FK_tbl_RelatedPublication_tbl_Publication]
    FOREIGN KEY ([PublicationID])
    REFERENCES [dbo].[tbl_Publication]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RelatedPublication'
ALTER TABLE [dbo].[tbl_RelatedPublication]
ADD CONSTRAINT [PK_tbl_RelatedPublication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RelatedPublication_tbl_Module'
CREATE INDEX [IX_FK_tbl_RelatedPublication_tbl_Module]
ON [dbo].[tbl_RelatedPublication]
    ([ModuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RelatedPublication_tbl_Publication'
CREATE INDEX [IX_FK_tbl_RelatedPublication_tbl_Publication]
ON [dbo].[tbl_RelatedPublication]
    ([PublicationID]);