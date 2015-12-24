-- Creating table 'tbl_TaskPersonalComment'
CREATE TABLE [dbo].[tbl_TaskPersonalComment] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [Comment] nvarchar(4000)  NULL,
    [RefusedComment] nvarchar(4000)  NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [FK_tbl_TaskPersonalComment_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskPersonalComment'
ALTER TABLE [dbo].[tbl_TaskPersonalComment]
ADD CONSTRAINT [PK_tbl_TaskPersonalComment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskPersonalComment_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskPersonalComment_tbl_Contact]
ON [dbo].[tbl_TaskPersonalComment]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskPersonalComment_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskPersonalComment_tbl_Task]
ON [dbo].[tbl_TaskPersonalComment]
    ([TaskID]);