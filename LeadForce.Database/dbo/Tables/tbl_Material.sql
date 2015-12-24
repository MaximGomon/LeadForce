-- Creating table 'tbl_Material'
CREATE TABLE [dbo].[tbl_Material] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Type] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Value] nvarchar(2000)  NULL,
    [WorkflowTemplateID] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Material'
ALTER TABLE [dbo].[tbl_Material]
ADD CONSTRAINT [FK_tbl_Material_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Material'
ALTER TABLE [dbo].[tbl_Material]
ADD CONSTRAINT [PK_tbl_Material]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Material_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Material_tbl_Sites]
ON [dbo].[tbl_Material]
    ([SiteID]);