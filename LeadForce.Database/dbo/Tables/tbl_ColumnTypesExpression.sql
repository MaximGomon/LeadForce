-- Creating table 'tbl_ColumnTypesExpression'
CREATE TABLE [dbo].[tbl_ColumnTypesExpression] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [ColumnTypesID] int  NOT NULL,
    [Expression] nvarchar(512)  NOT NULL
);
GO
-- Creating foreign key on [ColumnTypesID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypes]
    FOREIGN KEY ([ColumnTypesID])
    REFERENCES [dbo].[tbl_ColumnTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [FK_tbl_ColumnTypesExpression_tbl_ColumnTypesExpression]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[tbl_ColumnTypesExpression]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ColumnTypesExpression'
ALTER TABLE [dbo].[tbl_ColumnTypesExpression]
ADD CONSTRAINT [PK_tbl_ColumnTypesExpression]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ColumnTypesExpression_tbl_ColumnTypes'
CREATE INDEX [IX_FK_tbl_ColumnTypesExpression_tbl_ColumnTypes]
ON [dbo].[tbl_ColumnTypesExpression]
    ([ColumnTypesID]);