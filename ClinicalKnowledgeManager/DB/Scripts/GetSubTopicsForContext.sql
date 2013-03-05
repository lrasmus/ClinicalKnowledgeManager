
-- =============================================
-- Author:		Luke Rasmussen
-- Create date: February 2013
-- Description:	Used after identifying relevant topics.  This will return the specific
--		sub-topics that match the context parameters.
-- =============================================
CREATE PROCEDURE spGetSubTopicsForContext
    @topic_id INT,
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255),
	@task NVARCHAR(255),
	@sub_topic_code NVARCHAR(255),
	@sub_topic_code_system NVARCHAR(255),
	@gender NVARCHAR(10),
	@age_group NVARCHAR(255)
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
    )
    AS MatchingSubTopics;
    
    SELECT s.* FROM @MatchingSubTopics m
        INNER JOIN dbo.SubTopics s ON s.Id = m.Id
END
