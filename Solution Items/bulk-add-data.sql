DECLARE @OrgId INT, @DeptId INT, @DesigId INT, @AllowanceId INT

-- Add Organization
INSERT INTO Organisations (OrganisationName, TimeIn, TimeOut, DailyWorkingHours, IsActive, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
VALUES ('Example Organization', '08:00:00', '17:00:00', 8.00, 1, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

SET @OrgId = SCOPE_IDENTITY() -- Get the ID of the inserted organization

-- Add Department
INSERT INTO Departments (DepartmentName, OrganisationId, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
VALUES ('Example Department', @OrgId, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

SET @DeptId = SCOPE_IDENTITY() -- Get the ID of the inserted department

-- Add Designation
INSERT INTO Designations (DesignationName, DepartmentId, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
VALUES ('Example Designation', @DeptId, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

SET @DesigId = SCOPE_IDENTITY() -- Get the ID of the inserted designation

-- Add Allowance
INSERT INTO Allowances (Name, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
VALUES ('Example Allowance', GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

SET @AllowanceId = SCOPE_IDENTITY() -- Get the ID of the inserted allowance

-- Add 1000 Employees with Compensation, Attendance, and Allowance Assignment
DECLARE @Counter INT
SET @Counter = 1

WHILE @Counter <= 1000
BEGIN
    DECLARE @PhoneNumber NVARCHAR(MAX)
    SET @PhoneNumber = '1234567890' + RIGHT('0000' + CONVERT(NVARCHAR(MAX), @Counter), 4) -- Concatenate with counter for unique phone number

    DECLARE @EmailAddress NVARCHAR(MAX)
    SET @EmailAddress = 'employee' + RIGHT('0000' + CONVERT(NVARCHAR(MAX), @Counter), 4) + '@example.com' -- Concatenate with counter for unique email address

    INSERT INTO EmployeeProfiles (EmployeeName, EmployeeCode, EmployeeType, LineManager, Segment, Gender, MaritalStatus, Contact, EmailAddress, ActiveStatus, DesignationId, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
    VALUES ('Employee' + CONVERT(NVARCHAR(MAX), @Counter), 'EMP' + RIGHT('0000' + CONVERT(NVARCHAR(MAX), @Counter), 4), 1, 'Line Manager', 'Segment', 1, 1, @PhoneNumber, @EmailAddress, 1, @DesigId, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted
    
    DECLARE @EmployeeProfileId INT
    SET @EmployeeProfileId = SCOPE_IDENTITY() -- Get the ID of the inserted employee profile

    -- Add Employee Compensation
    INSERT INTO EmployeeCompensations (BasicSalary, ModeOfPayment, EmployeeProfileId, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
    VALUES (40000.00, 1, @EmployeeProfileId, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

    -- Mark Attendance for Whole Year (Excluding Sundays)
    DECLARE @StartDate DATE = '2024-01-01'
    DECLARE @EndDate DATE = '2024-12-31'
    
    WHILE @StartDate <= @EndDate
    BEGIN
        IF DATEPART(WEEKDAY, @StartDate) <> 1 -- Check if it's not Sunday
        BEGIN
            INSERT INTO EmployeeAttendances (AttendanceDate, TimeIn, TimeOut, IsApproved, ApprovedBy, EmployeeProfileId, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
            VALUES (@StartDate, '08:00:00', '17:00:00', 0, 0, @EmployeeProfileId, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted
        END
        
        SET @StartDate = DATEADD(DAY, 1, @StartDate) -- Move to the next day
    END

    -- Assign Allowance to Employee
    INSERT INTO EmployeeAllowances (EmployeeCompensationId, AllowanceId, Amount, CreatedAt, LastModifiedAt, IsDeleted, DeletedAt)
    VALUES (@EmployeeProfileId, @AllowanceId, 15000.00, GETDATE(), GETDATE(), 0, GETDATE()) -- Default value for DeletedAt when not deleted

    SET @Counter = @Counter + 1
END
