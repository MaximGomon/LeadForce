-- Creating table 'tbl_Requirement'
CREATE TABLE [dbo].[tbl_Requirement] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Number] nvarchar(256)  NOT NULL,
    [SerialNumber] int  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ShortDescription] nvarchar(2048)  NOT NULL,
    [RequestID] uniqueidentifier  NULL,
    [CompanyID] uniqueidentifier  NULL,
    [ContactID] uniqueidentifier  NULL,
    [ProductID] uniqueidentifier  NULL,
    [ProductSeriesNumber] nvarchar(256)  NULL,
    [RequirementTypeID] uniqueidentifier  NOT NULL,
    [ServiceLevelID] uniqueidentifier  NOT NULL,
    [RequirementSeverityOfExposureID] uniqueidentifier  NULL,
    [ParentID] uniqueidentifier  NULL,
    [RequirementPriorityID] uniqueidentifier  NULL,
    [RequirementComplexityID] uniqueidentifier  NULL,
    [PublicationCategoryID] uniqueidentifier  NULL,
    [RequirementStatusID] uniqueidentifier  NOT NULL,
    [ResponsibleID] uniqueidentifier  NULL,
    [RealizationDatePlanned] datetime  NULL,
    [RealizationDateActual] datetime  NULL,
    [ContractID] uniqueidentifier  NULL,
    [OrderID] uniqueidentifier  NULL,
    [InvoiceID] uniqueidentifier  NULL,
    [EvaluationRequirementsProductID] uniqueidentifier  NULL,
    [AnyProductName] nvarchar(255)  NULL,
    [Quantity] decimal(18,4)  NOT NULL,
    [UnitID] uniqueidentifier  NULL,
    [CurrencyID] uniqueidentifier  NULL,
    [Rate] decimal(19,4)  NOT NULL,
    [CurrencyPrice] decimal(19,4)  NOT NULL,
    [CurrencyAmount] decimal(19,4)  NOT NULL,
    [Price] decimal(19,4)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [RequirementImplementationCompleteID] uniqueidentifier  NULL,
    [RequirementSpeedTimeID] uniqueidentifier  NULL,
    [RequirementSatisfactionID] uniqueidentifier  NULL,
    [EstimationComment] nvarchar(2048)  NULL,
    [OwnerID] uniqueidentifier  NULL,
    [LongDescription] nvarchar(max)  NULL,
    [RequestSourceTypeID] uniqueidentifier  NULL,
    [InternalQuantity] decimal(18,4)  NOT NULL,
    [InternalUnitID] uniqueidentifier  NULL,
    [EstimateCommentInternal] nvarchar(2048)  NULL,
    [EstimateCommentForClient] nvarchar(2048)  NULL
);
GO
-- Creating foreign key on [CompanyID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Company]
    FOREIGN KEY ([CompanyID])
    REFERENCES [dbo].[tbl_Company]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContactID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OwnerID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Owner]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ResponsibleID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contact_Responsible]
    FOREIGN KEY ([ResponsibleID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ContractID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Contract]
    FOREIGN KEY ([ContractID])
    REFERENCES [dbo].[tbl_Contract]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [CurrencyID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Currency]
    FOREIGN KEY ([CurrencyID])
    REFERENCES [dbo].[tbl_Currency]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InvoiceID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Invoice]
    FOREIGN KEY ([InvoiceID])
    REFERENCES [dbo].[tbl_Invoice]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [OrderID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Order]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[tbl_Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ProductID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [EvaluationRequirementsProductID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Product_EvaluationRequirements]
    FOREIGN KEY ([EvaluationRequirementsProductID])
    REFERENCES [dbo].[tbl_Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [PublicationCategoryID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_PublicationCategory]
    FOREIGN KEY ([PublicationCategoryID])
    REFERENCES [dbo].[tbl_PublicationCategory]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequestSourceTypeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequestSourceType]
    FOREIGN KEY ([RequestSourceTypeID])
    REFERENCES [dbo].[tbl_RequestSourceType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ParentID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Requirement]
    FOREIGN KEY ([ParentID])
    REFERENCES [dbo].[tbl_Requirement]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementComplexityID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementComplexity]
    FOREIGN KEY ([RequirementComplexityID])
    REFERENCES [dbo].[tbl_RequirementComplexity]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementImplementationCompleteID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementImplementationComplete]
    FOREIGN KEY ([RequirementImplementationCompleteID])
    REFERENCES [dbo].[tbl_RequirementImplementationComplete]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementPriorityID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementPriority]
    FOREIGN KEY ([RequirementPriorityID])
    REFERENCES [dbo].[tbl_RequirementPriority]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementSatisfactionID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSatisfaction]
    FOREIGN KEY ([RequirementSatisfactionID])
    REFERENCES [dbo].[tbl_RequirementSatisfaction]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementSeverityOfExposureID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSeverityOfExposure]
    FOREIGN KEY ([RequirementSeverityOfExposureID])
    REFERENCES [dbo].[tbl_RequirementSeverityOfExposure]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementSpeedTimeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementSpeedTime]
    FOREIGN KEY ([RequirementSpeedTimeID])
    REFERENCES [dbo].[tbl_RequirementSpeedTime]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementStatusID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementStatus]
    FOREIGN KEY ([RequirementStatusID])
    REFERENCES [dbo].[tbl_RequirementStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [RequirementTypeID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_RequirementType]
    FOREIGN KEY ([RequirementTypeID])
    REFERENCES [dbo].[tbl_RequirementType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [ServiceLevelID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_ServiceLevel]
    FOREIGN KEY ([ServiceLevelID])
    REFERENCES [dbo].[tbl_ServiceLevel]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [SiteID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [UnitID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Unit]
    FOREIGN KEY ([UnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating foreign key on [InternalUnitID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [FK_tbl_Requirement_tbl_Unit_Internal]
    FOREIGN KEY ([InternalUnitID])
    REFERENCES [dbo].[tbl_Unit]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Requirement'
ALTER TABLE [dbo].[tbl_Requirement]
ADD CONSTRAINT [PK_tbl_Requirement]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Company'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Company]
ON [dbo].[tbl_Requirement]
    ([CompanyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact]
ON [dbo].[tbl_Requirement]
    ([ContactID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact_Owner'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact_Owner]
ON [dbo].[tbl_Requirement]
    ([OwnerID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contact_Responsible'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contact_Responsible]
ON [dbo].[tbl_Requirement]
    ([ResponsibleID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Contract'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Contract]
ON [dbo].[tbl_Requirement]
    ([ContractID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Currency'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Currency]
ON [dbo].[tbl_Requirement]
    ([CurrencyID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Invoice'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Invoice]
ON [dbo].[tbl_Requirement]
    ([InvoiceID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Order'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Order]
ON [dbo].[tbl_Requirement]
    ([OrderID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Product'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Product]
ON [dbo].[tbl_Requirement]
    ([ProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Product_EvaluationRequirements'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Product_EvaluationRequirements]
ON [dbo].[tbl_Requirement]
    ([EvaluationRequirementsProductID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_PublicationCategory'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_PublicationCategory]
ON [dbo].[tbl_Requirement]
    ([PublicationCategoryID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequestSourceType'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequestSourceType]
ON [dbo].[tbl_Requirement]
    ([RequestSourceTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Requirement'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Requirement]
ON [dbo].[tbl_Requirement]
    ([ParentID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementComplexity'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementComplexity]
ON [dbo].[tbl_Requirement]
    ([RequirementComplexityID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementImplementationComplete'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementImplementationComplete]
ON [dbo].[tbl_Requirement]
    ([RequirementImplementationCompleteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementPriority'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementPriority]
ON [dbo].[tbl_Requirement]
    ([RequirementPriorityID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSatisfaction'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSatisfaction]
ON [dbo].[tbl_Requirement]
    ([RequirementSatisfactionID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSeverityOfExposure'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSeverityOfExposure]
ON [dbo].[tbl_Requirement]
    ([RequirementSeverityOfExposureID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementSpeedTime'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementSpeedTime]
ON [dbo].[tbl_Requirement]
    ([RequirementSpeedTimeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementStatus'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementStatus]
ON [dbo].[tbl_Requirement]
    ([RequirementStatusID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_RequirementType'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_RequirementType]
ON [dbo].[tbl_Requirement]
    ([RequirementTypeID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_ServiceLevel'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_ServiceLevel]
ON [dbo].[tbl_Requirement]
    ([ServiceLevelID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Sites]
ON [dbo].[tbl_Requirement]
    ([SiteID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Unit'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Unit]
ON [dbo].[tbl_Requirement]
    ([UnitID]);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Requirement_tbl_Unit_Internal'
CREATE INDEX [IX_FK_tbl_Requirement_tbl_Unit_Internal]
ON [dbo].[tbl_Requirement]
    ([InternalUnitID]);