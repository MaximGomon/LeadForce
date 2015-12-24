-- Creating table 'tbl_RequirementType'
CREATE TABLE [dbo].[tbl_RequirementType] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [NumeratorID] uniqueidentifier  NOT NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [NumeratorID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [FK_tbl_RequirementType_tbl_Numerator]
    FOREIGN KEY ([NumeratorID])
    REFERENCES [dbo].[tbl_Numerator]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [FK_tbl_RequirementType_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequirementType'
ALTER TABLE [dbo].[tbl_RequirementType]
ADD CONSTRAINT [PK_tbl_RequirementType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementType_tbl_Numerator'
CREATE INDEX [IX_FK_tbl_RequirementType_tbl_Numerator]
ON [dbo].[tbl_RequirementType]
    ([NumeratorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequirementType_tbl_Sites'
CREATE INDEX [IX_FK_tbl_RequirementType_tbl_Sites]
ON [dbo].[tbl_RequirementType]
    ([SiteID]);