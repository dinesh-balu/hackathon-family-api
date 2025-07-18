CREATE PROCEDURE dbo.GetChildSessionStatistics
    @ChildId int
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.Id as ChildId,
        c.Name as ChildName,
        dbo.GetChildAge(c.Id) as Age,
        COUNT(ts.Id) as TotalSessions,
        AVG(CAST(ts.Duration as float)) as AverageDuration,
        dbo.GetSessionCompletionRate(c.Id) as AverageCompletionRate,
        MAX(ts.Date) as LastSessionDate,
        COUNT(CASE WHEN ts.Status = 'Completed' THEN 1 END) as CompletedSessions,
        COUNT(CASE WHEN ts.Status = 'Cancelled' THEN 1 END) as CancelledSessions
    FROM Children c
    LEFT JOIN TherapySessions ts ON c.Id = ts.ChildId
    WHERE c.Id = @ChildId
    GROUP BY c.Id, c.Name, c.DateOfBirth;
END;
GO

CREATE PROCEDURE dbo.GetTodaysSessionSchedule
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Today date = CAST(GETDATE() as date);
    
    SELECT 
        ts.Id as SessionId,
        ts.Date,
        ts.Duration,
        ts.Status,
        ts.Notes,
        c.Id as ChildId,
        c.Name as ChildName,
        u.Name as ParentName,
        u.Email as ParentEmail,
        ISNULL(sp.CompletionPercentage, 0) as LatestProgress
    FROM TherapySessions ts
    INNER JOIN Children c ON ts.ChildId = c.Id
    INNER JOIN Users u ON c.UserId = u.Id
    LEFT JOIN SessionProgresses sp ON ts.Id = sp.SessionId 
        AND sp.Id = (SELECT TOP 1 Id FROM SessionProgresses WHERE SessionId = ts.Id ORDER BY UpdatedAt DESC)
    WHERE CAST(ts.Date as date) = @Today
    ORDER BY ts.Date;
END;
GO

CREATE PROCEDURE dbo.UpdateSessionProgress
    @SessionId int,
    @ProgressNotes nvarchar(1000) = NULL,
    @CompletionPercentage decimal(5,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF NOT EXISTS (SELECT 1 FROM TherapySessions WHERE Id = @SessionId)
        BEGIN
            RAISERROR('Session not found', 16, 1);
            RETURN;
        END
        
        INSERT INTO SessionProgresses (SessionId, ProgressNotes, CompletionPercentage, UpdatedAt)
        VALUES (@SessionId, @ProgressNotes, @CompletionPercentage, GETUTCDATE());
        
        UPDATE TherapySessions 
        SET Status = CASE 
            WHEN @CompletionPercentage >= 100 THEN 'Completed'
            WHEN @CompletionPercentage > 0 THEN 'In Progress'
            ELSE Status
        END
        WHERE Id = @SessionId;
        
        COMMIT TRANSACTION;
        
        SELECT 'Success' as Result, 'Session progress updated successfully' as Message;
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        
        SELECT 'Error' as Result, ERROR_MESSAGE() as Message;
    END CATCH
END;
GO

CREATE PROCEDURE dbo.GetCareTeamByRole
    @Role nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Name,
        Role,
        Email,
        Phone,
        CreatedAt
    FROM CareTeamMembers
    WHERE (@Role IS NULL OR Role = @Role)
    ORDER BY Role, Name;
END;
GO
