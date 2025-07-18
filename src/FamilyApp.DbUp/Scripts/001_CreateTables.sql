CREATE TABLE Users (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Email nvarchar(255) NOT NULL UNIQUE,
    Role nvarchar(50) NOT NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE Children (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    DateOfBirth datetime2 NOT NULL,
    UserId int NOT NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE TherapySessions (
    Id int IDENTITY(1,1) PRIMARY KEY,
    ChildId int NOT NULL,
    Date datetime2 NOT NULL,
    Duration int NOT NULL,
    Status nvarchar(50) NOT NULL,
    Notes nvarchar(1000) NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (ChildId) REFERENCES Children(Id) ON DELETE CASCADE
);

CREATE TABLE CareTeamMembers (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name nvarchar(100) NOT NULL,
    Role nvarchar(50) NOT NULL,
    Email nvarchar(255) NOT NULL UNIQUE,
    Phone nvarchar(20) NULL,
    CreatedAt datetime2 NOT NULL DEFAULT GETUTCDATE()
);

CREATE TABLE SessionProgresses (
    Id int IDENTITY(1,1) PRIMARY KEY,
    SessionId int NOT NULL,
    ProgressNotes nvarchar(1000) NULL,
    CompletionPercentage decimal(5,2) NOT NULL,
    UpdatedAt datetime2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (SessionId) REFERENCES TherapySessions(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Children_UserId ON Children(UserId);
CREATE INDEX IX_TherapySessions_ChildId ON TherapySessions(ChildId);
CREATE INDEX IX_TherapySessions_Date ON TherapySessions(Date);
CREATE INDEX IX_SessionProgresses_SessionId ON SessionProgresses(SessionId);
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_CareTeamMembers_Email ON CareTeamMembers(Email);
CREATE INDEX IX_CareTeamMembers_Role ON CareTeamMembers(Role);
