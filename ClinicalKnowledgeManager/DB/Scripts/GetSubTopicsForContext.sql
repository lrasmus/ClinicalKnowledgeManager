
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE spGetSubTopicsForContext
    @topic_id INT,
	@info_recipient NVARCHAR(255),
	@search_code NVARCHAR(255),
	@search_code_system NVARCHAR(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- If there are no context values set, 
    IF (@info_recipient IS NULL OR @info_recipient = '')
    AND (@search_code IS NULL OR @search_code = '')
    BEGIN
        -- Because of how EF works, we need to return an empty data set
        SELECT * FROM dbo.SubTopics WHERE Id = -1
        
        --WITH AllSubTopics (Id, Name, ParentId, ParentType, CreatedOn)
        --AS
        --(
        --    SELECT s.* FROM dbo.Topics t
        --        INNER JOIN dbo.SubTopics s ON s.ParentId = t.Id AND s.ParentType = 'Topic'
        --    WHERE t.Id = @topic_id
            
        --    UNION ALL
            
        --    SELECT s.* FROM dbo.SubTopics s
        --        INNER JOIN AllSubTopics a ON s.ParentId = a.Id AND s.ParentType = 'SubTopic'
        -- )
        -- SELECT * FROM AllSubTopics
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
    )
    AS MatchingSubTopics;
    
    SELECT s.* FROM @MatchingSubTopics m
        INNER JOIN dbo.SubTopics s ON s.Id = m.Id
END
