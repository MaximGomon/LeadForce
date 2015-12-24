-- Creating table 'tbl_Currency'
CREATE TABLE [dbo].[tbl_Currency] (
    [ID] uniqueidentifier  NOT NULL,
    [SiteID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [NumCode] nvarchar(3)  NOT NULL,
    [CharCode] nvarchar(3)  NOT NULL,
    [Symbol] nvarchar(10)  NOT NULL,
    [IsBaseCurrency] bit  NOT NULL,
    [IsUpdateInternalCourse] bit  NOT NULL,
    [InternalCoursePercent] decimal(19,4)  NOT NULL
);
GO
-- Creating foreign key on [SiteID] in table 'tbl_Currency'
ALTER TABLE [dbo].[tbl_Currency]
ADD CONSTRAINT [FK_tbl_Currency_tbl_Sites]
    FOREIGN KEY ([SiteID])
    REFERENCES [dbo].[tbl_Sites]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_Currency'
ALTER TABLE [dbo].[tbl_Currency]
ADD CONSTRAINT [PK_tbl_Currency]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_Currency_tbl_Sites'
CREATE INDEX [IX_FK_tbl_Currency_tbl_Sites]
ON [dbo].[tbl_Currency]
    ([SiteID]);