-- Creating table 'tbl_TaskMember'
CREATE TABLE [dbo].[tbl_TaskMember] (
    [ID] uniqueidentifier  NOT NULL,
    [TaskID] uniqueidentifier  NOT NULL,
    [ContractorID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [TaskMemberRoleID] int  NULL,
    [TaskMemberStatusID] int  NULL,
    [Comment] nvarchar(1024)  NULL,
    [OrderID] uniqueidentifier  NULL,
    [UserComment] nvarchar(1024)  NULL,
    [OrderProductsID] uniqueidentifier  NULL,
    [IsInformed] bit  NOT NULL
);
GO
-- Creating foreign key on [ContractorID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Company]
    FOREIGN KEY ([ContractorID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderProductsID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_OrderProducts]
    FOREIGN KEY ([OrderProductsID])
    REFERENCES [dbo].[tbl_OrderProducts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [TaskID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [FK_tbl_TaskMember_tbl_Task]
    FOREIGN KEY ([TaskID])
    REFERENCES [dbo].[tbl_Task]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_TaskMember'
ALTER TABLE [dbo].[tbl_TaskMember]
ADD CONSTRAINT [PK_tbl_TaskMember]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Company'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Company]
ON [dbo].[tbl_TaskMember]
    ([ContractorID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Contact'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Contact]
ON [dbo].[tbl_TaskMember]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Order'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Order]
ON [dbo].[tbl_TaskMember]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_OrderProducts'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_OrderProducts]
ON [dbo].[tbl_TaskMember]
    ([OrderProductsID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_TaskMember_tbl_Task'
CREATE INDEX [IX_FK_tbl_TaskMember_tbl_Task]
ON [dbo].[tbl_TaskMember]
    ([TaskID]);