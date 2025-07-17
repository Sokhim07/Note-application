ALTER PROCEDURE NT.SV_NOTE_SAVE
(
    @NT_ID			DECIMAL(18,0),
    @TITLE			NVARCHAR(500),
    @CONTENT		NVARCHAR(MAX),
	@USER_NAME		NVARCHAR(25)
)
WITH ENCRYPTION AS
BEGIN
    BEGIN TRANSACTION TRAN1;
	DECLARE @NT_CODE NVARCHAR(25)
    DECLARE @CREATE_AT DATETIME = GETDATE();
    DECLARE @UPDATE_AT DATETIME = GETDATE();

    BEGIN TRY
	IF NOT EXISTS(SELECT NT_ID FROM NT.NOTED WHERE NT_ID = @NT_ID)
        BEGIN
            EXEC NT.GEN_NEW_ID '[NT].[NOTED]', '[NT_ID]', @NT_ID OUTPUT;
			SET @NT_CODE = CONCAT(N'000',@NT_ID)
			
            INSERT INTO NT.NOTED
            (
                 NT_ID
                ,NT_CODE
                ,TITLE
                ,CONTENT
				,NT_STS
                ,CREATE_AT
				,[USER_NAME]
            )
            VALUES
            (
                @NT_ID
                ,@NT_CODE
                ,@TITLE
                ,@CONTENT
				,N'CREATE'
                ,@CREATE_AT
				,@USER_NAME
            );
        END
        ELSE
        BEGIN
            UPDATE NT.NOTED
            SET
                
                TITLE = @TITLE
                ,CONTENT = @CONTENT
				,NT_STS = N'MODIFY'
                ,UPDATE_AT = @UPDATE_AT
				,[USER_NAME] = @USER_NAME
            WHERE NT_ID = @NT_ID;
        END;

        COMMIT TRANSACTION TRAN1;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION TRAN1;
        DECLARE @MSG NVARCHAR(1000) = ERROR_MESSAGE();
        RAISERROR(@MSG, 16, 1);
    END CATCH;
END;