/* First, let's go to the ROOT */ 
USE FEDERATION ROOT WITH RESET
GO 

/* Next, let's loot at the metadata using saome of the buil-in system SMVs */
SELECT db_name() [db_name]
SELECT * FROM sys.federations
SELECT * FROM sys.federation_distributions
SELECT * FROM sys.federation_member_distributions ORDER BY federation_id, range_low


/* Query the federation */ 
SELECT * FROM Products 

USE FEDERATION Goodies_Federation (FederationKey = '00000000-0000-0000-0000-000000000000') WITH RESET, FILTERING =OFF

/* Query the federation */ 
SELECT * FROM Products 


/* Let's split this thing */ 
USE FEDERATION ROOT WITH RESET 
GO 

ALTER FEDERATION Goodies_Federation SPLIT AT (FederationKey = '')
GO 

SELECT * FROM sys.federations
SELECT * FROM sys.federation_distributions
SELECT * FROM sys.federation_member_distributions ORDER BY federation_id, range_low


/* *WITH reset = this is required keyword males the connection reset explicit */

USE FEDERATION Goodies_Federation (FederationKey = '') WITH RESET, FILTERING =OFF

SELECT * FROM Products ; 


/* Filtering = ON = Get for specific FederatedKey */ 
USE FEDERATION Goodies_Federation (FederationKey = '') WITH RESET, FILTERING =ON

SELECT * FROM Products ; 