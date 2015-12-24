-- Creating table 'tbl_ContactRole'
CREATE TABLE [dbo].[tbl_ContactRole] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [RoleTypeID] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Email] nvarchar(255)  NULL,
    [DisplayName] nvarchar(255)  NULL,
    [SiteTagID] uniqueidentifier  NULL,
    [MethodAssigningResponsible] int  NULL,
    [LastAssignmentResponsible] uniqueidentifier  NULL
);
GO
-- Creating foreign key on [RoleTypeID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [FK_tbl_ContactRole_tbl_ContactRoleType]
    FOREIGN KEY ([RoleTypeID])
    REFERENCES [dbo].[tbl_ContactRoleType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [FK_tbl_ContactRole_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactRole'
ALTER TABLE [dbo].[tbl_ContactRole]
ADD CONSTRAINT [PK_tbl_ContactRole]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactRole_tbl_ContactRoleType'
CREATE INDEX [IX_FK_tbl_ContactRole_tbl_ContactRoleType]
ON [dbo].[tbl_ContactRole]
    ([RoleTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactRole_tbl_Sites'
CREATE INDEX [IX_FK_tbl_ContactRole_tbl_Sites]
ON [dbo].[tbl_ContactRole]
    ([SiteID]);