
-- =============================================
-- Author:		Luke Rasmussen
-- Create date: February 2013
-- Description:	Performs a search for the best topic(s) based on context
-- =============================================
CREATE PROCEDURE [dbo].[spSearchForTopicsBasedOnContext]
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255)
AS
BEGIN
    -- If no search parameters are specified, we're going to skip more complicated
    -- logic and just return a list of topics
    IF (@info_recipient IS NULL OR @info_recipient = '')
    AND (@search_code IS NULL OR @search_code = '')
    BEGIN
        SELECT * FROM dbo.Topics
        ORDER BY Id
        RETURN
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
            --(@info_recipient = '' OR
                (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient')--)

        UNION ALL

        SELECT s.id, 'searchCode' AS [Context]
        FROM dbo.SubTopics s
            INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
        WHERE         
            --((@search_code = '' AND @search_code_system = '') OR
                (@search_code_system = '' AND cmsc.Code = @search_code) OR
                (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system) --)
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
            INNER JOIN @MatchingSubTopics m ON m.id = s.id

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

    -- Union the sub-topic topics with actual topics that have matching concepts
    UNION ALL

    SELECT t.id, 'informationRecipient' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        --(@info_recipient = '' OR
            (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient')--)

    UNION ALL

    SELECT t.id, 'searchCode' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'Topic' AND cmsc.ParentId = t.Id)
    WHERE         
        --((@search_code = '' AND @search_code_system = '') OR
            (@search_code_system = '' AND cmsc.Code = @search_code) OR
            (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system) --)
            
    
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
    --SELECT t.*
    --FROM dbo.Topics t
    --    INNER JOIN (
    --        SELECT Id
    --        FROM (SELECT DISTINCT Id, Context FROM @MatchingTopics) mt
    --        GROUP BY mt.Id
    --        HAVING COUNT(mt.Id) = 2) mt ON mt.Id = t.Id
END
