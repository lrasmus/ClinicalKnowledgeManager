/****** Object:  Index [IX_TopicAliases_Name]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP INDEX [IX_TopicAliases_Name] ON [dbo].[TopicAliases]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[Topics]
GO
/****** Object:  Table [dbo].[TopicAliases]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[TopicAliases]
GO
/****** Object:  Table [dbo].[SubTopics]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[SubTopics]
GO
/****** Object:  Table [dbo].[Contents]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[Contents]
GO
/****** Object:  Table [dbo].[ConceptMaps]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[ConceptMaps]
GO
/****** Object:  Table [dbo].[CKMLogs]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP TABLE [dbo].[CKMLogs]
GO
/****** Object:  StoredProcedure [dbo].[spSearchForTopicsBasedOnContext]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP PROCEDURE [dbo].[spSearchForTopicsBasedOnContext]
GO
/****** Object:  StoredProcedure [dbo].[spGetSubTopicsForContext]    Script Date: 6/24/2014 7:43:15 AM ******/
DROP PROCEDURE [dbo].[spGetSubTopicsForContext]
GO
/****** Object:  StoredProcedure [dbo].[spGetSubTopicsForContext]    Script Date: 6/24/2014 7:43:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luke Rasmussen
-- Create date: February 2013
-- Description:	Used after identifying relevant topics.  This will return the specific
--		sub-topics that match the context parameters.
--
-- Updates:
------------------------------------------------
-- 10/6/13 - Return all sub-topics if no matching context found
-- =============================================
CREATE PROCEDURE [dbo].[spGetSubTopicsForContext]
    @topic_id INT,
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255),
	@search_term NVARCHAR(255),
	@task NVARCHAR(255),
	@sub_topic_code NVARCHAR(255),
	@sub_topic_code_system NVARCHAR(255),
	@sub_topic_term NVARCHAR(255),
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
	AND (@search_term IS NULL OR @search_term = '')
	AND (@task IS NULL OR @task = '')
	AND (@sub_topic_code IS NULL OR @sub_topic_code = '')
	AND (@sub_topic_term IS NULL OR @sub_topic_term = '')
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
            ((@search_code_system = '' OR @search_code_system IS NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND cmsc.Code = @search_code) OR
			((@search_code_system != '' AND @search_code_system IS NOT NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)) OR
			(@search_term != '' AND @search_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @search_term)

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
            ((@sub_topic_code_system = '' OR @sub_topic_code_system IS NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND cmsc.Code = @sub_topic_code) OR
			((@sub_topic_code_system != '' AND @sub_topic_code_system IS NOT NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)) OR
			(@sub_topic_term != '' AND @sub_topic_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @sub_topic_term)

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
    
	DECLARE @max_matching INT
    SELECT @max_matching = MAX(cnt) FROM
    (
        SELECT Id, COUNT(Id) AS cnt
        FROM (SELECT DISTINCT * from @MatchingSubTopics) SubTopics
        GROUP BY Id
    ) sub  


    IF (SELECT COUNT(*) FROM @MatchingSubTopics) = 0
        BEGIN
            WITH TopicSubtopics(Id, [Name], [ParentId], [ParentType], [CreatedOn])
            AS
            (
                SELECT s.Id, s.[Name], s.[ParentId], s.[ParentType], s.[CreatedOn]
                FROM dbo.SubTopics s
                    INNER JOIN dbo.Topics t ON t.id = s.ParentId AND s.ParentType = 'Topic'
                WHERE t.id = @topic_id

                UNION ALL
            	
                SELECT s.Id, s.[Name], s.[ParentId], s.[ParentType], s.[CreatedOn]
                FROM dbo.SubTopics s
                    INNER JOIN TopicSubtopics t ON t.Id = s.ParentId AND (s.ParentType = 'SubTopic')
            )

            SELECT DISTINCT * FROM TopicSubtopics
        END
    ELSE
        BEGIN
			
			DECLARE @RelevantSubTopics TABLE (id INT)
			INSERT INTO @RelevantSubTopics
			SELECT Id
			FROM @MatchingSubTopics
			GROUP BY Id
			HAVING COUNT(Id) = @max_matching;

            WITH TopicSubtopics(Id, [Name], [ParentId], [ParentType], [CreatedOn])
            AS
            (
                SELECT s.Id, s.[Name], s.[ParentId], s.[ParentType], s.[CreatedOn]
                FROM dbo.SubTopics s
                    INNER JOIN @RelevantSubTopics m ON m.id = s.ParentId AND s.ParentType = 'SubTopic'

                UNION ALL
            	
                SELECT s.Id, s.[Name], s.[ParentId], s.[ParentType], s.[CreatedOn]
                FROM dbo.SubTopics s
                    INNER JOIN TopicSubtopics t ON t.Id = s.ParentId AND (s.ParentType = 'SubTopic')
            )
            SELECT * FROM TopicSubtopics
            UNION ALL
            SELECT s.* FROM @RelevantSubTopics m
                INNER JOIN dbo.SubTopics s ON s.Id = m.Id
        END
END


GO
/****** Object:  StoredProcedure [dbo].[spSearchForTopicsBasedOnContext]    Script Date: 6/24/2014 7:43:15 AM ******/
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
	@search_term NVARCHAR(255),
	@task NVARCHAR(255),
	@sub_topic_code NVARCHAR(255),
	@sub_topic_code_system NVARCHAR(255),
	@sub_topic_term NVARCHAR(255),
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
	AND (@search_term IS NULL OR @search_term = '')
	AND (@task IS NULL OR @task = '')
	AND (@sub_topic_code IS NULL OR @sub_topic_code = '')
	AND (@sub_topic_term IS NULL OR @sub_topic_term = '')
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
            ((@search_code_system = '' OR @search_code_system IS NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND cmsc.Code = @search_code) OR
			((@search_code_system != '' AND @search_code_system IS NOT NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)) OR
			(@search_term != '' AND @search_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @search_term)

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
            ((@sub_topic_code_system = '' OR @sub_topic_code_system IS NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND cmsc.Code = @sub_topic_code) OR
			((@sub_topic_code_system != '' AND @sub_topic_code_system IS NOT NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)) OR
			(@sub_topic_term != '' AND @sub_topic_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @sub_topic_term)

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
        ((@search_code_system = '' OR @search_code_system IS NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND cmsc.Code = @search_code) OR
		((@search_code_system != '' AND @search_code_system IS NOT NULL) AND (@search_code != '' AND @search_code IS NOT NULL) AND (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system)) OR
		(@search_term != '' AND @search_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @search_term)

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
		((@sub_topic_code_system = '' OR @sub_topic_code_system IS NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND cmsc.Code = @sub_topic_code) OR
		((@sub_topic_code_system != '' AND @sub_topic_code_system IS NOT NULL) AND (@sub_topic_code != '' AND @sub_topic_code IS NOT NULL) AND (cmsc.Code = @sub_topic_code AND cmsc.CodeSystem = @sub_topic_code_system)) OR
		(@sub_topic_term != '' AND @sub_topic_term IS NOT NULL AND cmsc.Term IS NOT NULL AND cmsc.Term = @sub_topic_term)

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
    IF ((@search_code IS NOT NULL AND @search_code != '') OR (@search_term IS NOT NULL AND @search_term != ''))
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
/****** Object:  Table [dbo].[CKMLogs]    Script Date: 6/24/2014 7:43:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CKMLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[ClientDetails] [text] NULL,
	[Message] [text] NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_CKMLogs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConceptMaps]    Script Date: 6/24/2014 7:43:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptMaps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](255) NULL,
	[CodeSystem] [nvarchar](255) NULL,
	[Term] [nvarchar](255) NULL,
	[ParentId] [int] NOT NULL,
	[ParentType] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ConceptMaps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contents]    Script Date: 6/24/2014 7:43:15 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubTopics]    Script Date: 6/24/2014 7:43:15 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TopicAliases]    Script Date: 6/24/2014 7:43:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopicAliases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TopicId] [int] NULL,
	[Context] [nvarchar](1024) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_TopicAliases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Topics]    Script Date: 6/24/2014 7:43:15 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TopicAliases_Name]    Script Date: 6/24/2014 7:43:15 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TopicAliases_Name] ON [dbo].[TopicAliases]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


