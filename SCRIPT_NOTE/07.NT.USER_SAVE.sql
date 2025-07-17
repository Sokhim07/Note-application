ALTER PROCEDURE NT.USER_SAVE
(
    @US_ID			NVARCHAR(25),
    @USER_NAME		NVARCHAR(500),
	@PASS			NVARCHAR(MAX),
    @IS_LOCK		BIT,
    @LLOG_IN		DATETIME,
    @IS_ONLINE		BIT = 0,
    @LLOG_OUT		DATETIME
)
WITH ENCRYPTION AS
BEGIN
    BEGIN TRANSACTION TRAN1;
    DECLARE @CREAT_DATE DATETIME = GETDATE();
	DECLARE @US_STS NVARCHAR(225) ='CREATE'
	DECLARE @PASSWORD VARBINARY(MAX) = CONVERT(VARBINARY(MAX), @PASS);

    BEGIN TRY
	IF NOT EXISTS(SELECT US_ID FROM NT.[USER] WHERE US_ID = @US_ID)
        BEGIN
            EXEC NT.GEN_NEW_ID '[NT].[USER]', '[US_ID]', @US_ID OUTPUT;
			
            INSERT INTO NT.[USER]
            (
               [US_ID],
            [USER_NAME],
            [PASSWORD],
            [IS_LOCK],
            [LLOG_IN],
            [IS_ONLINE],
            [LLOG_OUT],
            [CREAT_DATE]
            )
            VALUES
            (
             @US_ID,
            @USER_NAME,
            @PASSWORD,
            @IS_LOCK,
            @LLOG_IN,
            @IS_ONLINE,
            @LLOG_OUT,
            @CREAT_DATE
            );
        END
        ELSE
        BEGIN
			UPDATE NT.[USER] SET
            [USER_NAME] = @USER_NAME,
            [PASSWORD] = @PASSWORD,
            [IS_LOCK] = @IS_LOCK,
            [LLOG_IN] = @LLOG_IN,
            [IS_ONLINE] = @IS_ONLINE,
            [LLOG_OUT] = @LLOG_OUT,
            [CREAT_DATE] = @CREAT_DATE
        WHERE [US_ID] = @US_ID;
        END;

        COMMIT TRANSACTION TRAN1;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION TRAN1;
        DECLARE @MSG NVARCHAR(1000) = ERROR_MESSAGE();
        RAISERROR(@MSG, 16, 1);
    END CATCH;
END;
