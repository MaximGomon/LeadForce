-- Creating table 'tbl_AdvertisingTypeCategory'
CREATE TABLE [dbo].[tbl_AdvertisingTypeCategory] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_AdvertisingTypeCategory'
ALTER TABLE [dbo].[tbl_AdvertisingTypeCategory]
ADD CONSTRAINT [PK_tbl_AdvertisingTypeCategory]
    PRIMARY KEY CLUSTERED ([ID] ASC);