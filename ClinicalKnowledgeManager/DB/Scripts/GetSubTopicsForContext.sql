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
