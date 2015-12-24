-- Creating table 'tbl_RequestFile'
CREATE TABLE [dbo].[tbl_RequestFile] (
    [ID] uniqueidentifier  NOT NULL,
    [RequestID] uniqueidentifier  NOT NULL,
    [FileName] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [RequestID] in table 'tbl_RequestFile'
ALTER TABLE [dbo].[tbl_RequestFile]
ADD CONSTRAINT [FK_tbl_RequestFile_tbl_Request]
    FOREIGN KEY ([RequestID])
    REFERENCES [dbo].[tbl_Request]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_RequestFile'
ALTER TABLE [dbo].[tbl_RequestFile]
ADD CONSTRAINT [PK_tbl_RequestFile]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_RequestFile_tbl_Request'
CREATE INDEX [IX_FK_tbl_RequestFile_tbl_Request]
ON [dbo].[tbl_RequestFile]
    ([RequestID]);