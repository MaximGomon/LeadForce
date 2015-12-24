-- Creating table 'tbl_ContactCommunication'
CREATE TABLE [dbo].[tbl_ContactCommunication] (
    [ID] uniqueidentifier  NOT NULL,
    [ContactID] uniqueidentifier  NOT NULL,
    [CommunicationType] int  NOT NULL,
    [CommunicationNumber] nvarchar(256)  NOT NULL
);
GO
-- Creating foreign key on [ContactID] in table 'tbl_ContactCommunication'
ALTER TABLE [dbo].[tbl_ContactCommunication]
ADD CONSTRAINT [FK_tbl_ContactCommunication_tbl_Contact]
    FOREIGN KEY ([ContactID])
    REFERENCES [dbo].[tbl_Contact]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO
-- Creating primary key on [ID] in table 'tbl_ContactCommunication'
ALTER TABLE [dbo].[tbl_ContactCommunication]
ADD CONSTRAINT [PK_tbl_ContactCommunication]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO
-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_ContactCommunication_tbl_Contact'
CREATE INDEX [IX_FK_tbl_ContactCommunication_tbl_Contact]
ON [dbo].[tbl_ContactCommunication]
    ([ContactID]);