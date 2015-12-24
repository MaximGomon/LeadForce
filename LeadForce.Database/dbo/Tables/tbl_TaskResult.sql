-- Creating table 'tbl_TaskResult'
CREATE TABLE [dbo].[tbl_TaskResult] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NULL,
    [IsDefault] bit  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_TaskResult'
ALTER TABLE [dbo].[tbl_TaskResult]
ADD CONSTRAINT [FK_tbl_TaskResult_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskResult'
ALTER TABLE [dbo].[tbl_TaskResult]
ADD CONSTRAINT [PK_tbl_TaskResult]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskResult_tbl_Sites'
CREATE INDEX [IX_FK_tbl_TaskResult_tbl_Sites]
ON [dbo].[tbl_TaskResult]
    ([SiteID]);