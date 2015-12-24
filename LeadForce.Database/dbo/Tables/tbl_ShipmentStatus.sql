-- Creating table 'tbl_ShipmentStatus'
CREATE TABLE [dbo].[tbl_ShipmentStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL
);
GO
-- Creating primary key on [ID] in table 'tbl_ShipmentStatus'
ALTER TABLE [dbo].[tbl_ShipmentStatus]
ADD CONSTRAINT [PK_tbl_ShipmentStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);