-- Creating table 'tbl_WorkflowTemplate'
CREATE TABLE [dbo].[tbl_WorkflowTemplate] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [ModuleID] uniqueidentifier  NULL,
    [Status] int  NOT NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Description] nvarchar(max)  NULL,
    [ManualStart] bit  NOT NULL,
    [AutomaticMethod] int  NULL,
    [Event] int  NULL,
    [Frequency] int  NOT NULL,
    [Condition] int  NULL,
    [ActivityCount] int  NULL,
    [DenyMultipleRun] bit  NOT NULL,
    [WorkflowXml] nvarchar(max)  NULL,
    [DataBaseStatusID] int  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_WorkflowTemplate'
ALTER TABLE [dbo].[tbl_WorkflowTemplate]
ADD CONSTRAINT [FK_tbl_WorkflowTemplate_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_WorkflowTemplate'
ALTER TABLE [dbo].[tbl_WorkflowTemplate]
ADD CONSTRAINT [PK_tbl_WorkflowTemplate]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_WorkflowTemplate_tbl_Sites'
CREATE INDEX [IX_FK_tbl_WorkflowTemplate_tbl_Sites]
ON [dbo].[tbl_WorkflowTemplate]
    ([SiteID]);