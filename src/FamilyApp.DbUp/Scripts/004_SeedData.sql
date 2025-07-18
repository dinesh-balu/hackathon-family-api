INSERT INTO Users (Name, Email, Role) VALUES
('John Smith', 'john.smith@example.com', 'Parent'),
('Sarah Johnson', 'sarah.johnson@example.com', 'Parent'),
('Mike Wilson', 'mike.wilson@example.com', 'Guardian');

INSERT INTO Children (Name, DateOfBirth, UserId) VALUES
('Emma Smith', '2018-03-15', 1),
('Liam Johnson', '2016-07-22', 2),
('Olivia Wilson', '2019-11-08', 3);

INSERT INTO CareTeamMembers (Name, Role, Email, Phone) VALUES
('Dr. Emily Rodriguez', 'Therapist', 'emily.rodriguez@clinic.com', '555-0101'),
('James Thompson', 'Speech Therapist', 'james.thompson@clinic.com', '555-0102'),
('Lisa Chen', 'Occupational Therapist', 'lisa.chen@clinic.com', '555-0103'),
('Dr. Michael Brown', 'Pediatrician', 'michael.brown@clinic.com', '555-0104');

INSERT INTO TherapySessions (ChildId, Date, Duration, Status, Notes) VALUES
(1, DATEADD(day, -7, GETDATE()), 60, 'Completed', 'Great progress with communication exercises'),
(1, DATEADD(day, -3, GETDATE()), 45, 'Completed', 'Worked on social interaction skills'),
(2, DATEADD(day, -5, GETDATE()), 60, 'Completed', 'Speech therapy session - good improvement'),
(2, GETDATE(), 60, 'Scheduled', 'Today''s session - speech therapy'),
(3, DATEADD(day, -2, GETDATE()), 30, 'Completed', 'Occupational therapy - fine motor skills'),
(3, DATEADD(day, 1, GETDATE()), 45, 'Scheduled', 'Tomorrow''s session - sensory integration');

INSERT INTO SessionProgresses (SessionId, ProgressNotes, CompletionPercentage) VALUES
(1, 'Child showed excellent engagement and completed all activities', 100.00),
(2, 'Good participation, some challenges with group activities', 85.00),
(3, 'Significant improvement in speech clarity', 90.00),
(5, 'Made good progress with fine motor coordination', 80.00);
