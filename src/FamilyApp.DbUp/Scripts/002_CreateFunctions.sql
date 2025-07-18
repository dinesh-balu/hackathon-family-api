CREATE FUNCTION dbo.GetChildAge(@ChildId int)
RETURNS int
AS
BEGIN
    DECLARE @Age int;
    
    SELECT @Age = DATEDIFF(YEAR, DateOfBirth, GETDATE())
    FROM Children
    WHERE Id = @ChildId;
    
    RETURN ISNULL(@Age, 0);
END;
GO

CREATE FUNCTION dbo.GetSessionCompletionRate(@ChildId int)
RETURNS decimal(5,2)
AS
BEGIN
    DECLARE @CompletionRate decimal(5,2);
    
    SELECT @CompletionRate = AVG(sp.CompletionPercentage)
    FROM TherapySessions ts
    INNER JOIN SessionProgresses sp ON ts.Id = sp.SessionId
    WHERE ts.ChildId = @ChildId;
    
    RETURN ISNULL(@CompletionRate, 0.00);
END;
GO

CREATE FUNCTION dbo.CountSessionsInDateRange(@ChildId int, @StartDate datetime2, @EndDate datetime2)
RETURNS int
AS
BEGIN
    DECLARE @SessionCount int;
    
    SELECT @SessionCount = COUNT(*)
    FROM TherapySessions
    WHERE ChildId = @ChildId 
      AND Date >= @StartDate 
      AND Date <= @EndDate;
    
    RETURN ISNULL(@SessionCount, 0);
END;
GO
