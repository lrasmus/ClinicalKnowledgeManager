USE [ClinicalKnowledgeManager]
GO
TRUNCATE TABLE dbo.ConceptMaps
TRUNCATE TABLE dbo.Contents
TRUNCATE TABLE dbo.SubTopics
TRUNCATE TABLE dbo.Topics


SET IDENTITY_INSERT dbo.Topics ON
INSERT INTO dbo.Topics (Id, Name, Audience, InternalComments, CreatedOn)
SELECT * FROM [ClinicalKnowledgeManager.DB.ModelContext].dbo.Topics
SET IDENTITY_INSERT dbo.Topics OFF

SET IDENTITY_INSERT dbo.SubTopics ON
INSERT INTO dbo.SubTopics (Id, Name, ParentId, ParentType, CreatedOn)
SELECT * FROM [ClinicalKnowledgeManager.DB.ModelContext].dbo.SubTopics
SET IDENTITY_INSERT dbo.SubTopics OFF

SET IDENTITY_INSERT dbo.Contents ON
INSERT INTO dbo.Contents (Id, Value, ParentId, ParentType, CreatedOn)
SELECT * FROM [ClinicalKnowledgeManager.DB.ModelContext].dbo.Contents
SET IDENTITY_INSERT dbo.Contents OFF

SET IDENTITY_INSERT dbo.ConceptMaps ON
INSERT INTO dbo.ConceptMaps (Id, Code, CodeSystem, ParentId, ParentType, CreatedOn)
SELECT Id, Code, CodeSystem, ParentId, ParentType, CreatedOn FROM [ClinicalKnowledgeManager.DB.ModelContext].dbo.ConceptMaps
SET IDENTITY_INSERT dbo.ConceptMaps OFF