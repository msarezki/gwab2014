-- // Create Federation 
CREATE FEDERATION Goodies_Federation (FederationKey uniqueidentifier range)
GO

--// Connect to the f  ederation member
--// FILTERING=OFF means that we are working with all the atomic units in
--// this federation member
USE FEDERATION Goodies_Federation (FederationKey = '00000000-0000-0000-0000-000000000000') WITH FILTERING=OFF, RESET
GO

-- 00000000-0000-0000-0000-000000000000

CREATE TABLE [dbo].[Products] (
    [Id] [uniqueidentifier] NOT NULL default newid(),
    [TenantId] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [Category] [nvarchar](max) NOT NULL,
    [Description] [nvarchar](max),
    [Price] [decimal](18, 2) NOT NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY ([Id],[TenantId])
)FEDERATED ON (FederationKey = TenantId)