 USE EletronicStore;

CREATE PROCEDURE Proc_InserirPaisCSV
AS BEGIN

DECLARE @V_Nome NVARCHAR(100);

DECLARE CURSOR_PAIS CURSOR FOR
	SELECT
	["NO_PAIS"]
	FROM 
	TABELA_PAIS;

OPEN CURSOR_PAIS;

FETCH NEXT FROM
	CURSOR_PAIS
INTO
	@V_Nome;

WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO PAISES (PAS_NOME) VALUES (UPPER(REPLACE(@V_Nome,'"','')));

		FETCH NEXT FROM
			CURSOR_PAIS
		INTO
			@V_Nome;
	END;

DEALLOCATE CURSOR_PAIS;

END;
GO