/****** Object:  StoredProcedure [dbo].[spGetSubTopicsForContext]    Script Date: 10/02/2013 09:34:13 ******/
DROP PROCEDURE [dbo].[spGetSubTopicsForContext]
GO
/****** Object:  StoredProcedure [dbo].[spSearchForTopicsBasedOnContext]    Script Date: 10/02/2013 09:34:13 ******/
DROP PROCEDURE [dbo].[spSearchForTopicsBasedOnContext]
GO
/****** Object:  Table [dbo].[SubTopics]    Script Date: 10/02/2013 09:34:11 ******/
DROP TABLE [dbo].[SubTopics]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 10/02/2013 09:34:11 ******/
DROP TABLE [dbo].[Topics]
GO
/****** Object:  Table [dbo].[ConceptMaps]    Script Date: 10/02/2013 09:34:11 ******/
DROP TABLE [dbo].[ConceptMaps]
GO
/****** Object:  Table [dbo].[Contents]    Script Date: 10/02/2013 09:34:11 ******/
DROP TABLE [dbo].[Contents]
GO
/****** Object:  Table [dbo].[Contents]    Script Date: 10/02/2013 09:34:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[ParentType] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Contents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Contents] ON
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (13, N'This result predicts that your patient will be a <b><u>NORMAL METABOLIZER</u></b> of clopidogrel.<br/><br/>This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.', 33, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (14, N'This result predicts that your patient will be a NORMAL METABOLIZER of clopidogrel.  This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.', 34, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (15, N'<ul><li>Please note that this genetic test only tests for the 8 most common variants and does not test for all variants in the CYP2C19 gene</li><li>Other factors such as weight, certain health conditions, and other medications may influence a person’s response to clopidogrel</li><li>The interpretation of these results is based on current understanding and may change as more information is learned about the genetic variants associated with clopidogrel</li></ul>', 35, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (16, N'CPIC guidelines: <a href="http://www.pharmgkb.org/drug/PA449053">http://www.pharmgkb.org/drug/PA449053</a><br/><br/>SA Scott, K Sangkuhl, EE Gardern, CM Stein, J-S Hulot, JA Johnson, DM Roden, TE Klein, and AR Shuldiner  “Clinical Pharmacogenetics Implementation Consortium Guidelines for Cytochrome P450-2C19 (CYP2C19) Genotype and Clopidogrel Therapy” Clin Pharmacol Ther 90(2): 328-332 (2011).  <a href="http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf ">http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf</a><br/><br/>FDA Drug Safety Communication on Clopidogrel: <a href="http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm">http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm</a>', 36, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (17, N'This result predicts that your patient will be an <b><u>INTERMEDIATE METABOLIZER</u></b> of clopidogrel. This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.<br/><br/>This means that <b><u>clopidogrel may have a reduced effect</u></b> because of reduced conversion to the active metabolite. There are some studies suggesting that there might be an increased risk for adverse cardiovascular events. ', 38, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (18, N'If your patient is taking clopidogrel or if you are considering prescribing it, consider prescribing an alternative medication such as prasugrel. (Prasugrel might be associated with an increased bleeding risk compared to clopidogrel.)', 39, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (19, N'<ul><li>Please note that this genetic test only tests for the 8 most common variants and does not test for all variants in the CYP2C19 gene</li><li>Other factors such as weight, certain health conditions, and other medications may influence a person’s response to clopidogrel</li><li>The interpretation of these results is based on current understanding and may change as more information is learned about the genetic variants associated with clopidogrel</li></ul>', 40, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (20, N'CPIC guidelines: <a href="http://www.pharmgkb.org/drug/PA449053">http://www.pharmgkb.org/drug/PA449053</a><br/><br/>SA Scott, K Sangkuhl, EE Gardern, CM Stein, J-S Hulot, JA Johnson, DM Roden, TE Klein, and AR Shuldiner  “Clinical Pharmacogenetics Implementation Consortium Guidelines for Cytochrome P450-2C19 (CYP2C19) Genotype and Clopidogrel Therapy” Clin Pharmacol Ther 90(2): 328-332 (2011).  <a href="http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf ">http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf</a><br/><br/>FDA Drug Safety Communication on Clopidogrel: <a href="http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm">http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm</a>', 41, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (21, N'This result predicts that your patient will be a <b><u>RAPID METABOLIZER</u></b> of clopidogrel. This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.<br/><br/>This means that a patient taking clopidogrel could experience increased platelet inhibition.  ', 43, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (22, N'If your patient is taking clopidogrel or if you are considering prescribing it, no changes are recommended, but some studies suggest altering the dosage of the medication.    ', 44, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (23, N'<ul><li>Please note that this genetic test only tests for the 8 most common variants and does not test for all variants in the CYP2C19 gene</li><li>Other factors such as weight, certain health conditions, and other medications may influence a person’s response to clopidogrel</li><li>The interpretation of these results is based on current understanding and may change as more information is learned about the genetic variants associated with clopidogrel</li></ul>', 45, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (24, N'CPIC guidelines: <a href="http://www.pharmgkb.org/drug/PA449053">http://www.pharmgkb.org/drug/PA449053</a><br/><br/>SA Scott, K Sangkuhl, EE Gardern, CM Stein, J-S Hulot, JA Johnson, DM Roden, TE Klein, and AR Shuldiner  “Clinical Pharmacogenetics Implementation Consortium Guidelines for Cytochrome P450-2C19 (CYP2C19) Genotype and Clopidogrel Therapy” Clin Pharmacol Ther 90(2): 328-332 (2011).  <a href="http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf ">http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf</a><br/><br/>FDA Drug Safety Communication on Clopidogrel: <a href="http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm">http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm</a>', 46, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (25, N'This result predicts that your patient will be a <b><u>POOR METABOLIZER</u></b> of clopidogrel. This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.<br/><br/>This means that <b><u>clopidogrel may not be effective</u></b> at preventing  adverse cardiovascular events because it is not metabolized to its active form.', 48, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (26, N'This result predicts that your patient will be a <b><u>POOR METABOLIZER</u></b> of clopidogrel. This interpretation is based on current Clinical Pharmacogenetics Implementation Consortium (CPIC) guidelines.<br/><br/>This means that <b><u>clopidogrel may not be effective</u></b> at preventing  adverse cardiovascular events because it is not metabolized to its active form.', 49, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (27, N'<ul><li>Please note that this genetic test only tests for the 8 most common variants and does not test for all variants in the CYP2C19 gene</li><li>Other factors such as weight, certain health conditions, and other medications may influence a person’s response to clopidogrel</li><li>The interpretation of these results is based on current understanding and may change as more information is learned about the genetic variants associated with clopidogrel</li></ul>', 50, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[Contents] ([Id], [Value], [ParentId], [ParentType], [CreatedOn]) VALUES (28, N'CPIC guidelines: <a href="http://www.pharmgkb.org/drug/PA449053">http://www.pharmgkb.org/drug/PA449053</a><br/><br/>SA Scott, K Sangkuhl, EE Gardern, CM Stein, J-S Hulot, JA Johnson, DM Roden, TE Klein, and AR Shuldiner  “Clinical Pharmacogenetics Implementation Consortium Guidelines for Cytochrome P450-2C19 (CYP2C19) Genotype and Clopidogrel Therapy” Clin Pharmacol Ther 90(2): 328-332 (2011).  <a href="http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf ">http://www.ncbi.nlm.nih.gov/pmc/articles/PMC3234301/pdf/clpt2011132a.pdf</a><br/><br/>FDA Drug Safety Communication on Clopidogrel: <a href="http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm">http://www.fda.gov/Drugs/DrugSafety/PostmarketDrugSafetyInformationforPatientsandProviders/ucm203888.htm</a>', 51, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Contents] OFF
/****** Object:  Table [dbo].[ConceptMaps]    Script Date: 10/02/2013 09:34:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptMaps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[CodeSystem] [nvarchar](255) NULL,
	[ParentId] [int] NOT NULL,
	[ParentType] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ConceptMaps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ConceptMaps] ON
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (12, N'10071614', N'2.16.840.1.113883.6.163', 28, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (13, N'10071614', N'2.16.840.1.113883.6.163', 32, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (14, N'10071614', N'2.16.840.1.113883.6.163', 33, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (15, N'10071614', N'2.16.840.1.113883.6.163', 34, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (16, N'10071614', N'2.16.840.1.113883.6.163', 35, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (17, N'10071614', N'2.16.840.1.113883.6.163', 36, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (18, N'10071622', N'2.16.840.1.113883.6.163', 28, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (19, N'10071622', N'2.16.840.1.113883.6.163', 32, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (20, N'10071622', N'2.16.840.1.113883.6.163', 33, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (21, N'10071622', N'2.16.840.1.113883.6.163', 34, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (22, N'10071622', N'2.16.840.1.113883.6.163', 35, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (23, N'10071622', N'2.16.840.1.113883.6.163', 36, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (24, N'10071616', N'2.16.840.1.113883.6.163', 29, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (25, N'10071616', N'2.16.840.1.113883.6.163', 37, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (26, N'10071616', N'2.16.840.1.113883.6.163', 38, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (27, N'10071616', N'2.16.840.1.113883.6.163', 39, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (28, N'10071616', N'2.16.840.1.113883.6.163', 40, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (29, N'10071616', N'2.16.840.1.113883.6.163', 41, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (30, N'10071624', N'2.16.840.1.113883.6.163', 29, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (31, N'10071624', N'2.16.840.1.113883.6.163', 37, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (32, N'10071624', N'2.16.840.1.113883.6.163', 38, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (33, N'10071624', N'2.16.840.1.113883.6.163', 39, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (34, N'10071624', N'2.16.840.1.113883.6.163', 40, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (35, N'10071624', N'2.16.840.1.113883.6.163', 41, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (36, N'10071617', N'2.16.840.1.113883.6.163', 30, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (37, N'10071617', N'2.16.840.1.113883.6.163', 42, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (38, N'10071617', N'2.16.840.1.113883.6.163', 43, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (39, N'10071617', N'2.16.840.1.113883.6.163', 44, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (40, N'10071617', N'2.16.840.1.113883.6.163', 45, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (41, N'10071617', N'2.16.840.1.113883.6.163', 46, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (42, N'10071625', N'2.16.840.1.113883.6.163', 30, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (43, N'10071625', N'2.16.840.1.113883.6.163', 42, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (44, N'10071625', N'2.16.840.1.113883.6.163', 43, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (45, N'10071625', N'2.16.840.1.113883.6.163', 44, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (46, N'10071625', N'2.16.840.1.113883.6.163', 45, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (47, N'10071625', N'2.16.840.1.113883.6.163', 46, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (48, N'10071615', N'2.16.840.1.113883.6.163', 31, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (49, N'10071615', N'2.16.840.1.113883.6.163', 47, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (50, N'10071615', N'2.16.840.1.113883.6.163', 48, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (51, N'10071615', N'2.16.840.1.113883.6.163', 49, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (52, N'10071615', N'2.16.840.1.113883.6.163', 50, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (53, N'10071615', N'2.16.840.1.113883.6.163', 51, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (54, N'10071623', N'2.16.840.1.113883.6.163', 31, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (55, N'10071623', N'2.16.840.1.113883.6.163', 47, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (56, N'10071623', N'2.16.840.1.113883.6.163', 48, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (57, N'10071623', N'2.16.840.1.113883.6.163', 49, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (58, N'10071623', N'2.16.840.1.113883.6.163', 50, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[ConceptMaps] ([Id], [Code], [CodeSystem], [ParentId], [ParentType], [CreatedOn]) VALUES (59, N'10071623', N'2.16.840.1.113883.6.163', 51, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[ConceptMaps] OFF
/****** Object:  Table [dbo].[Topics]    Script Date: 10/02/2013 09:34:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Audience] [nvarchar](50) NULL,
	[InternalComments] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Topics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Topics] ON
INSERT [dbo].[Topics] ([Id], [Name], [Audience], [InternalComments], [CreatedOn]) VALUES (6, N'Clopidogrel Metabolism', N'Physician', NULL, CAST(0x0000A24A00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Topics] OFF
/****** Object:  Table [dbo].[SubTopics]    Script Date: 10/02/2013 09:34:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubTopics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ParentId] [int] NOT NULL,
	[ParentType] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.SubTopics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SubTopics] ON
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (28, N'Normal Metabolizer', 6, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (29, N'Intermediate Metabolizer', 6, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (30, N'Rapid Metabolizer', 6, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (31, N'Poor Metabolizer', 6, N'Topic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (32, N'Overview', 28, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (33, N'Interpretation', 28, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (34, N'Recommendation', 28, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (35, N'Limitations', 28, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (36, N'References', 28, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (37, N'Overview', 29, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (38, N'Interpretation', 29, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (39, N'Recommendation', 29, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (40, N'Limitations', 29, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (41, N'References', 29, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (42, N'Overview', 30, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (43, N'Interpretation', 30, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (44, N'Recommendation', 30, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (45, N'Limitations', 30, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (46, N'References', 30, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (47, N'Overview', 31, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (48, N'Interpretation', 31, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (49, N'Recommendation', 31, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (50, N'Limitations', 31, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
INSERT [dbo].[SubTopics] ([Id], [Name], [ParentId], [ParentType], [CreatedOn]) VALUES (51, N'References', 31, N'SubTopic', CAST(0x0000A24A00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[SubTopics] OFF
/****** Object:  StoredProcedure [dbo].[spSearchForTopicsBasedOnContext]    Script Date: 10/02/2013 09:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luke Rasmussen
-- Create date: February 2013
-- Description:	Performs a search for the best topic(s) based on context
-- =============================================
CREATE PROCEDURE [dbo].[spSearchForTopicsBasedOnContext]
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255),
	@task NVARCHAR(255),
	@sub_topic_code NVARCHAR(255),
	@sub_topic_code_system NVARCHAR(255),
	@gender NVARCHAR(10),
	@age_group NVARCHAR(255),
    @performer_language_code NVARCHAR(255),
    @recipient_language_code NVARCHAR(255),
    @performer_provider_code NVARCHAR(255),
    @recipient_provider_code NVARCHAR(255),
	@encounter_code NVARCHAR(255)
AS
BEGIN
    -- If no search parameters are specified, we're going to skip more complicated
    -- logic and just return a list of topics
    IF (@info_recipient IS NULL OR @info_recipient = '')
    AND (@search_code IS NULL OR @search_code = '')
	AND (@task IS NULL OR @task = '')
	AND (@sub_topic_code IS NULL OR @sub_topic_code = '')
	AND (@gender IS NULL OR @gender = '')
	AND (@age_group IS NULL OR @age_group = '')
    AND (@performer_language_code IS NULL OR @performer_language_code = '')
    AND (@recipient_language_code IS NULL OR @recipient_language_code = '')
    AND (@performer_provider_code IS NULL OR @performer_provider_code = '')
	AND (@recipient_provider_code IS NULL OR @recipient_provider_code = '')
	AND (@encounter_code IS NULL OR @encounter_code = '')
    BEGIN
        SELECT * FROM dbo.Topics
        ORDER BY Id
        RETURN
    END

	-- If both performer and recipient languages are specified, recipient wins out
	IF (@performer_language_code IS NOT NULL AND @performer_language_code != '')
		AND (@recipient_language_code IS NOT NULL AND @recipient_language_code != '')
	BEGIN
		SET @performer_language_code = ''
	END

	-- If both performer and recipient healthcare provider codes are specified, recipient wins out
	IF (@performer_provider_code IS NOT NULL AND @performer_provider_code != '')
		AND (@recipient_provider_code IS NOT NULL AND @recipient_provider_code != '')
	BEGIN
		SET @performer_provider_code = ''
	END

    -- Find all sub-topics that match any of the concept parameters
    DECLARE @MatchingSubTopics TABLE (id INT, context NVARCHAR(255))
    INSERT INTO @MatchingSubTopics
    SELECT id, context FROM
    (
        SELECT s.id, 'informationRecipient' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient')

        UNION ALL

        SELECT s.id, 'searchCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
        WHERE
            (@search_code_system = '' AND cmsc.Code = @search_code) OR
            (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)

        UNION ALL

        SELECT s.id, 'task' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @task AND cmir.CodeSystem = '2.16.840.1.113883.5.4')

		UNION ALL

        SELECT s.id, 'subTopic' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
        WHERE
            (@sub_topic_code_system = '' AND cmsc.Code = @sub_topic_code) OR
            (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)

        UNION ALL

        SELECT s.id, 'gender' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @gender AND cmir.CodeSystem = '2.16.840.1.113883.5.1')

        UNION ALL

        SELECT s.id, 'ageGroup' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @age_group AND cmir.CodeSystem = '2.16.840.1.113883.6.177')

        UNION ALL

        SELECT s.id, 'performerLanguage' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @performer_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

        UNION ALL

        SELECT s.id, 'recipientLanguage' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @recipient_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

        UNION ALL

        SELECT s.id, 'performerProviderCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @performer_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

        UNION ALL

        SELECT s.id, 'recipientProviderCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @recipient_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

		UNION ALL

        SELECT s.id, 'encounter' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @encounter_code AND cmir.CodeSystem = '2.16.840.1.113883.5.4')
    )
    AS MatchingSubTopics;
    
    -- Roll all of these sub-topics up into their appropriate topics (keep in mind that sub-topics
    -- can be nested, so this recursively goes up the hierarchy)
    DECLARE @MatchingTopics TABLE (id INT, context NVARCHAR(255));
    WITH TopLevelMatchingSubTopics(Id, [Type], [Context], [Level])
    AS
    (
        SELECT s.ParentId, s.ParentType, m.Context, 0 AS [Level]
        FROM dbo.SubTopics s
            INNER JOIN @MatchingSubTopics m ON m.id = s.id AND s.ParentType = 'SubTopic'

	    UNION ALL
    	
	    SELECT s.ParentId, s.ParentType, t.Context, [Level] + 1 AS [Level]
	    FROM dbo.SubTopics s
	        INNER JOIN TopLevelMatchingSubTopics t ON t.Id = s.Id AND (s.ParentType = 'SubTopic')
    )
    INSERT INTO @MatchingTopics
    SELECT t.Id, m.Context
    FROM TopLevelMatchingSubTopics m
    INNER JOIN dbo.SubTopics s ON s.Id = m.Id
    INNER JOIN dbo.Topics t ON t.ID = s.ParentId AND s.ParentType = 'Topic'
    
    UNION ALL
    
    SELECT t.Id, m.Context
    FROM @MatchingSubTopics m
    INNER JOIN dbo.SubTopics s ON s.Id = m.Id
    INNER JOIN dbo.Topics t ON t.ID = s.ParentId AND s.ParentType = 'Topic'

    -- Union the sub-topic topics with actual topics that have matching concepts
    UNION ALL

    SELECT t.id, 'informationRecipient' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient')

    UNION ALL

    SELECT t.id, 'searchCode' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'Topic' AND cmsc.ParentId = t.Id)
    WHERE         
        (@search_code_system = '' AND cmsc.Code = @search_code) OR
        (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)

    UNION ALL

    SELECT t.id, 'task' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @task AND cmir.CodeSystem = '2.16.840.1.113883.5.4')

	UNION ALL

    SELECT t.id, 'subTopic' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'Topic' AND cmsc.ParentId = t.Id)
    WHERE
        (@sub_topic_code_system = '' AND cmsc.Code = @sub_topic_code) OR
        (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)

    UNION ALL

    SELECT t.id, 'gender' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @gender AND cmir.CodeSystem = '2.16.840.1.113883.5.1')

    UNION ALL

    SELECT t.id, 'ageGroup' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @age_group AND cmir.CodeSystem = '2.16.840.1.113883.6.177')

    UNION ALL

    SELECT t.id, 'performerLanguage' AS [Context]
    FROM dbo.Topics t
		INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @performer_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

    UNION ALL

    SELECT t.id, 'recipientLanguage' AS [Context]
    FROM dbo.Topics t
		INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @recipient_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

    UNION ALL

    SELECT t.id, 'performerProviderCode' AS [Context]
    FROM dbo.Topics t
		INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @performer_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

    UNION ALL

    SELECT t.id, 'recipientProviderCode' AS [Context]
    FROM dbo.Topics t
		INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @recipient_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

	UNION ALL

    SELECT t.id, 'encounter' AS [Context]
    FROM dbo.Topics t
		INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (cmir.Code = @encounter_code AND cmir.CodeSystem = '2.16.840.1.113883.5.4')
            
    
    DECLARE @max_matching INT
    SELECT @max_matching = MAX(cnt) FROM
    (
        SELECT Id, COUNT(Id) AS cnt
        FROM (SELECT DISTINCT * from @MatchingTopics) Topics
        GROUP BY Id
    ) sub            

    -- If search code is specified, we require the search code match
    IF (@search_code != '')
    BEGIN
        SELECT t.*
        FROM dbo.Topics t
            INNER JOIN (
                SELECT mt.Id
                FROM (SELECT DISTINCT mt.*
                        FROM @MatchingTopics mt
                        INNER JOIN @MatchingTopics mtc ON mtc.Id = mt.Id AND mtc.context = 'searchCode') mt
                GROUP BY mt.Id
                HAVING COUNT(mt.Id) = @max_matching) mt ON mt.Id = t.Id
        ORDER BY t.Id
    END
    -- Otherwise, we look for the top matching results
    ELSE
    BEGIN
        SELECT t.*
        FROM dbo.Topics t
            INNER JOIN (
                SELECT Id
                FROM (SELECT DISTINCT * from @MatchingTopics) mt
                GROUP BY Id
                HAVING COUNT(Id) = @max_matching) mt ON mt.Id = t.Id
        ORDER BY t.Id
    END
END
GO
/****** Object:  StoredProcedure [dbo].[spGetSubTopicsForContext]    Script Date: 10/02/2013 09:34:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luke Rasmussen
-- Create date: February 2013
-- Description:	Used after identifying relevant topics.  This will return the specific
--		sub-topics that match the context parameters.
-- =============================================
CREATE PROCEDURE [dbo].[spGetSubTopicsForContext]
    @topic_id INT,
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255),
	@task NVARCHAR(255),
	@sub_topic_code NVARCHAR(255),
	@sub_topic_code_system NVARCHAR(255),
	@gender NVARCHAR(10),
	@age_group NVARCHAR(255),
    @performer_language_code NVARCHAR(255),
    @recipient_language_code NVARCHAR(255),
    @performer_provider_code NVARCHAR(255),
    @recipient_provider_code NVARCHAR(255),
	@encounter_code NVARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- If there are no context values set, 
    IF (@info_recipient IS NULL OR @info_recipient = '')
    AND (@search_code IS NULL OR @search_code = '')
	AND (@task IS NULL OR @task = '')
	AND (@sub_topic_code IS NULL OR @sub_topic_code = '')
	AND (@gender IS NULL OR @gender = '')
	AND (@age_group IS NULL OR @age_group = '')
    BEGIN
        -- Because of how EF works, we need to return an empty data set
        SELECT * FROM dbo.SubTopics WHERE Id = -1
        RETURN
    END
    
    -- Find all sub-topics that match any of the concept parameters
    DECLARE @MatchingSubTopics TABLE (id INT, context NVARCHAR(255))
    INSERT INTO @MatchingSubTopics
    SELECT DISTINCT id, context FROM
    (
        SELECT s.id, 'informationRecipient' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient')

        UNION ALL

        SELECT s.id, 'searchCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
        WHERE         
            (@search_code_system = '' AND cmsc.Code = @search_code) OR
            (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)

        UNION ALL

        SELECT s.id, 'task' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @task AND cmir.CodeSystem = '2.16.840.1.113883.5.4')

		UNION ALL

        SELECT s.id, 'subTopic' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
        WHERE
            (@sub_topic_code_system = '' AND cmsc.Code = @sub_topic_code) OR
            (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)

        UNION ALL

        SELECT s.id, 'gender' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @gender AND cmir.CodeSystem = '2.16.840.1.113883.5.1')

        UNION ALL

        SELECT s.id, 'ageGroup' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @age_group AND cmir.CodeSystem = '2.16.840.1.113883.6.177')

        UNION ALL

        SELECT s.id, 'performerLanguage' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @performer_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

        UNION ALL

        SELECT s.id, 'recipientLanguage' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @recipient_language_code AND cmir.CodeSystem = '2.16.840.1.113883.6.121')

        UNION ALL

        SELECT s.id, 'performerProviderCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @performer_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

        UNION ALL

        SELECT s.id, 'recipientProviderCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @recipient_provider_code AND cmir.CodeSystem = '2.16.840.1.113883.6.101')

		UNION ALL

        SELECT s.id, 'encounter' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
        WHERE 
            (cmir.Code = @encounter_code AND cmir.CodeSystem = '2.16.840.1.113883.5.4')
    )
    AS MatchingSubTopics;
    
    SELECT s.* FROM @MatchingSubTopics m
        INNER JOIN dbo.SubTopics s ON s.Id = m.Id
END
GO
