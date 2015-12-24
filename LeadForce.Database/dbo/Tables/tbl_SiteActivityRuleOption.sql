-- Creating table 'tbl_SiteActivityRuleOption'
CREATE TABLE [dbo].[tbl_SiteActivityRuleOption] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [SiteActivityRuleID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [ViewTypeID] int  NOT NULL,
    [LastViewDate] datetime  NOT NULL,
    [Views] int  NULL,
    [Result] int  NULL,
    [Conversion] float  NULL,
    [HtmlTemplate] nvarchar(max)  NULL
);
GO
-- Creating foreign key on [SiteActivityRuleID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules]
    FOREIGN KEY ([SiteActivityRuleID])
    REFERENCES [dbo].[tbl_SiteActivityRules]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ViewTypeID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [FK_tbl_SiteActivityRuleOption_tbl_ViewTypes]
    FOREIGN KEY ([ViewTypeID])
    REFERENCES [dbo].[tbl_ViewTypes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_SiteActivityRuleOption'
ALTER TABLE [dbo].[tbl_SiteActivityRuleOption]
ADD CONSTRAINT [PK_tbl_SiteActivityRuleOption]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_SiteActivityRules]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([SiteActivityRuleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_Sites'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_Sites]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SiteActivityRuleOption_tbl_ViewTypes'
CREATE INDEX [IX_FK_tbl_SiteActivityRuleOption_tbl_ViewTypes]
ON [dbo].[tbl_SiteActivityRuleOption]
    ([ViewTypeID]);