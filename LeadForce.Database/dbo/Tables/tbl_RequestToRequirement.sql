-- Creating table 'tbl_RequestToRequirement'
CREATE TABLE [dbo].[tbl_RequestToRequirement] (
    [tbl_Request_ID] uniqueidentifier  NOT NULL,
    [tbl_Requirement_ID] uniqueidentifier  NOT NULL
);
GO
-- Creating foreign key on [tbl_Request_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Request]
    FOREIGN KEY ([tbl_Request_ID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [tbl_Requirement_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [FK_tbl_RequestToRequirement_tbl_Requirement]
    FOREIGN KEY ([tbl_Requirement_ID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [tbl_Request_ID], [tbl_Requirement_ID] in table 'tbl_RequestToRequirement'
ALTER TABLE [dbo].[tbl_RequestToRequirement]
ADD CONSTRAINT [PK_tbl_RequestToRequirement]
    PRIMARY KEY CLUSTERED ([tbl_Request_ID], [tbl_Requirement_ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestToRequirement_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_RequestToRequirement_tbl_Requirement]
ON [dbo].[tbl_RequestToRequirement]
    ([tbl_Requirement_ID]);