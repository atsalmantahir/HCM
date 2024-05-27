CREATE PROCEDURE CreateWeekendHolidays
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATE;
    DECLARE @EndDate DATE;

    -- Set the range of dates for which you want to create weekend holidays
    SET @StartDate = '2024-01-01'; -- Start date
    SET @EndDate = '2024-12-31';   -- End date

    -- Temporary table to hold weekend holidays
    CREATE TABLE #WeekendHolidays (
        HolidayDate DATE
    );

    -- Insert weekends into temporary table
    WHILE @StartDate <= @EndDate
    BEGIN
        --IF DATEPART(WEEKDAY, @StartDate) IN (1, 7) -- 1 is Sunday, 7 is Saturday
		IF DATEPART(WEEKDAY, @StartDate) IN (1) -- 1 is Sunday, 7 is Saturday
        BEGIN
            INSERT INTO #WeekendHolidays (HolidayDate)
            VALUES (@StartDate);
        END

        SET @StartDate = DATEADD(DAY, 1, @StartDate); -- Move to the next day
    END

    -- Insert weekend holidays into main holiday table
    INSERT INTO Holidays(HolidayName, HolidayDate, IsOfficial, IsActive, CreatedAt, LastModifiedAt)
    SELECT 'Weekend Holiday', HolidayDate, 0, 1, GETDATE(),GETDATE() -- Assuming IsOfficial is false for weekends
    FROM #WeekendHolidays;

    -- Drop temporary table
    DROP TABLE #WeekendHolidays;
END;
