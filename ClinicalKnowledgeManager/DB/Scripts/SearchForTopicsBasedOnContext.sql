
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spSearchForTopicsBasedOnContext]
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255)
AS
BEGIN

    DECLARE @MatchingSubTopics TABLE (id INT, context NVARCHAR(255))
    INSERT INTO @MatchingSubTopics
    SELECT id, context FROM
    (
    SELECT s.id, 'informationRecipient' AS [Context]
    FROM dbo.SubTopics s
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'SubTopic' AND cmir.ParentId = s.Id)
    WHERE 
        (@info_recipient = '' OR
            (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient'))

    UNION ALL

    SELECT s.id, 'searchCode' AS [Context]
    FROM dbo.SubTopics s
        INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'SubTopic' AND cmsc.ParentId = s.Id)
    WHERE         
        ((@search_code = '' AND @search_code_system = '') 
            OR (@search_code_system = '' AND cmsc.Code = @search_code)
            OR (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system) )
    )
    AS MatchingSubTopics;

    DECLARE @MatchingTopics TABLE (id INT, context NVARCHAR(255));

    --WITH TopicsForMatchingSubTopics(Id, [Type], [Context], [Level])
    --AS
    --(
    --    SELECT s.ParentId, s.ParentType, m.Context, 0 AS [Level]
    --    FROM dbo.SubTopics s
    --        INNER JOIN @MatchingSubTopics m ON m.id = s.id

	   -- UNION ALL
    	
	   -- SELECT s.ParentId, s.ParentType, t.Context, [Level] + 1 AS [Level]
	   -- FROM dbo.SubTopics s
	   --     INNER JOIN TopicsForMatchingSubTopics t ON t.Id = s.Id
    --)
    --INSERT INTO @MatchingTopics
    --SELECT Id, Context
    --FROM TopicsForMatchingSubTopics
    --WHERE [Type] = 'Topic'
    
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

    UNION ALL

    SELECT t.id, 'informationRecipient' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmir ON (cmir.ParentType = 'Topic' AND cmir.ParentId = t.Id)
    WHERE 
        (@info_recipient = '' OR
            (cmir.Code = @info_recipient AND cmir.CodeSystem = 'informationRecipient'))

    UNION ALL

    SELECT t.id, 'searchCode' AS [Context]
    FROM dbo.Topics t
        INNER JOIN dbo.ConceptMaps cmsc ON (cmsc.ParentType = 'Topic' AND cmsc.ParentId = t.Id)
    WHERE         
        ((@search_code = '' AND @search_code_system = '') 
            OR (@search_code_system = '' AND cmsc.Code = @search_code)
            OR (cmsc.Code = @search_code AND cmsc.CodeSystem = @search_code_system) )

    SELECT t.*
    FROM dbo.Topics t
        INNER JOIN (
            SELECT Id
            FROM (SELECT DISTINCT Id, Context FROM @MatchingTopics) mt
            GROUP BY mt.Id
            HAVING COUNT(mt.Id) = 2) mt ON mt.Id = t.Id

    --WITH TopicsForMatchingSubTopics(Id, [Type], [Level])
    --AS
    --(
    --    SELECT s.ParentId, s.ParentType, 0 AS [Level]
    --    FROM dbo.SubTopics s
	   --     LEFT OUTER JOIN dbo.ConceptMaps m ON (m.ParentType = 'SubTopic' AND m.ParentId = s.Id)
	   -- WHERE 
	   --     (@info_recipient = '' OR
	   --         (m.Code = @info_recipient AND m.CodeSystem = 'informationRecipient'))
	   -- OR ( (@search_code = '' AND @search_code_system = '') 
	   --     OR (@search_code_system = '' AND m.Code = @search_code)
	   --     OR (m.Code = @search_code AND m.CodeSystem = @search_code_system) )
    	    
	   -- UNION ALL
    	
	   -- SELECT s.ParentId, s.ParentType, [Level] + 1 AS [Level]
	   -- FROM dbo.SubTopics s
	   --     INNER JOIN TopicsForMatchingSubTopics t ON t.Id = s.Id
    --)
    --SELECT t.*
    --FROM TopicsForMatchingSubTopics m
    --    INNER JOIN dbo.Topics t ON t.Id = m.Id
    --WHERE m.[Type] = 'Topic'

    --UNION ALL

    ---- Find all topics directly tagged with that information
    --SELECT t.*
    --FROM dbo.Topics t
    --    LEFT OUTER JOIN dbo.ConceptMaps m ON (m.ParentType = 'Topic' AND m.ParentId = t.Id)
    --WHERE 
    --    (@info_recipient = '' OR
    --        (m.Code = @info_recipient AND m.CodeSystem = 'informationRecipient'))
    --OR ( (@search_code = '' AND @search_code_system = '') 
    --    OR (@search_code_system = '' AND m.Code = @search_code)
    --    OR (m.Code = @search_code AND m.CodeSystem = @search_code_system) )
    
	---- SET NOCOUNT ON added to prevent extra result sets from
	---- interfering with SELECT statements.
	--SET NOCOUNT ON;

 --   -- Insert statements for procedure here
	--SELECT t.* FROM dbo.Topics t
	--LEFT OUTER JOIN dbo.ConceptMaps m ON (m.ParentType = 'Topic' AND m.ParentId = t.Id)
	--WHERE 
	--    (@info_recipient = '' OR
	--        (m.Code = @info_recipient AND m.CodeSystem = 'informationRecipient'))
	--AND ( (@search_code = '' AND @search_code_system = '') 
	--    OR (@search_code_system = '' AND m.Code = @search_code)
	--    OR (m.Code = @search_code AND m.CodeSystem = @search_code_system) )
	            
END
