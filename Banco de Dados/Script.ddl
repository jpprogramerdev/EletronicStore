-- Gerado por Oracle SQL Developer Data Modeler 24.3.1.351.0831
--   em:        2025-02-22 17:35:27 BRT
--   site:      SQL Server 2012
--   tipo:      SQL Server 2012



CREATE TABLE BANDEIRAS_CARTAO 
    (
     BDC_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     BDC_NOME VARCHAR (100) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que representa o registro das bandeira do cartão' , 'USER' , 'dbo' , 'TABLE' , 'BANDEIRAS_CARTAO' , 'COLUMN' , 'BDC_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o nome de uma bandeira' , 'USER' , 'dbo' , 'TABLE' , 'BANDEIRAS_CARTAO' , 'COLUMN' , 'BDC_NOME' 
GO

ALTER TABLE BANDEIRAS_CARTAO ADD CONSTRAINT PK_BDC PRIMARY KEY CLUSTERED (BDC_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE CARTOES 
    (
     CRT_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     CRT_NUMERO VARCHAR (50) NOT NULL , 
     CRT_CVV CHAR (3) NOT NULL , 
     CRT_APELIDO VARCHAR (100) NOT NULL , 
     CRT_BDC_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que representa o registro dos cartões' , 'USER' , 'dbo' , 'TABLE' , 'CARTOES' , 'COLUMN' , 'CRT_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o numero do cartão' , 'USER' , 'dbo' , 'TABLE' , 'CARTOES' , 'COLUMN' , 'CRT_NUMERO' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o apelido  do cartão' , 'USER' , 'dbo' , 'TABLE' , 'CARTOES' , 'COLUMN' , 'CRT_APELIDO' 
GO

ALTER TABLE CARTOES ADD CONSTRAINT PK_CRT PRIMARY KEY CLUSTERED (CRT_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE ENDERECOS 
    (
     END_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     END_LOGRADOURO VARCHAR (300) NOT NULL , 
     END_NUMERO VARCHAR (10) NOT NULL , 
     END_CEP CHAR (8) NOT NULL , 
     END_OBSERVACAO VARCHAR (200) , 
     END_APELIDO VARCHAR (150) NOT NULL , 
     END_TPR_ID INTEGER NOT NULL , 
     END_TPL_ID INTEGER NOT NULL , 
     END_MUC_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que representa o registro do endereço' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o logradouro do endereço' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_LOGRADOURO' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o numero do endereço' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_NUMERO' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o CEP do endereço' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_CEP' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica uma observação/complemento do endereço (não obrigatório)' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_OBSERVACAO' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o apelido do endereço' , 'USER' , 'dbo' , 'TABLE' , 'ENDERECOS' , 'COLUMN' , 'END_APELIDO' 
GO

ALTER TABLE ENDERECOS ADD CONSTRAINT PK_END PRIMARY KEY CLUSTERED (END_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE ESTADOS 
    (
     EST_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     EST_NOME VARCHAR (200) NOT NULL , 
     EST_PAS_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que indentifica o registro do estado' , 'USER' , 'dbo' , 'TABLE' , 'ESTADOS' , 'COLUMN' , 'EST_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o nome do estado' , 'USER' , 'dbo' , 'TABLE' , 'ESTADOS' , 'COLUMN' , 'EST_NOME' 
GO

ALTER TABLE ESTADOS ADD CONSTRAINT PK_EST PRIMARY KEY CLUSTERED (EST_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE GENEROS 
    (
     GNR_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     GRN_GENERO VARCHAR (100) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária que representa o registro dos generos dos usuarios' , 'USER' , 'dbo' , 'TABLE' , 'GENEROS' , 'COLUMN' , 'GNR_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o os generos para o usuário' , 'USER' , 'dbo' , 'TABLE' , 'GENEROS' , 'COLUMN' , 'GRN_GENERO' 
GO

ALTER TABLE GENEROS ADD CONSTRAINT PK_GNR PRIMARY KEY CLUSTERED (GNR_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE MUNICIPIOS 
    (
     MUC_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     MUC_NOME VARCHAR (200) NOT NULL , 
     MUC_EST_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária que representa o registro do municipio' , 'USER' , 'dbo' , 'TABLE' , 'MUNICIPIOS' , 'COLUMN' , 'MUC_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o nome do municipio' , 'USER' , 'dbo' , 'TABLE' , 'MUNICIPIOS' , 'COLUMN' , 'MUC_NOME' 
GO

ALTER TABLE MUNICIPIOS ADD CONSTRAINT PK_MUC PRIMARY KEY CLUSTERED (MUC_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE PAISES 
    (
     PAS_Id INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     PAS_NOME VARCHAR (200) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que identifica o registro do pais' , 'USER' , 'dbo' , 'TABLE' , 'PAISES' , 'COLUMN' , 'PAS_Id' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Nome do pai' , 'USER' , 'dbo' , 'TABLE' , 'PAISES' , 'COLUMN' , 'PAS_NOME' 
GO

ALTER TABLE PAISES ADD CONSTRAINT PK_PAS PRIMARY KEY CLUSTERED (PAS_Id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TELEFONES 
    (
     TLF_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     TLF_DDD CHAR (2) NOT NULL , 
     TLF_NUMERO VARCHAR (12) NOT NULL , 
     TLF_TPT_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária que rerpesenta o registro dos telefones' , 'USER' , 'dbo' , 'TABLE' , 'TELEFONES' , 'COLUMN' , 'TLF_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o DDD do telefone' , 'USER' , 'dbo' , 'TABLE' , 'TELEFONES' , 'COLUMN' , 'TLF_DDD' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o numero do telefone' , 'USER' , 'dbo' , 'TABLE' , 'TELEFONES' , 'COLUMN' , 'TLF_NUMERO' 
GO

ALTER TABLE TELEFONES ADD CONSTRAINT PK_TLF PRIMARY KEY CLUSTERED (TLF_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPOS_LOGRADOURO 
    (
     TPL_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     TPL_TIPO VARCHAR (200) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que representa o registro do tipo de logradouro' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_LOGRADOURO' , 'COLUMN' , 'TPL_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o tipo de logradouro do endereço' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_LOGRADOURO' , 'COLUMN' , 'TPL_TIPO' 
GO

ALTER TABLE TIPOS_LOGRADOURO ADD CONSTRAINT PK_TPL PRIMARY KEY CLUSTERED (TPL_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPOS_RESIDENCIA 
    (
     TPR_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     TPR_TIPO VARCHAR (200) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primaria que representa o registro do Tipo de Residencia' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_RESIDENCIA' , 'COLUMN' , 'TPR_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o tipo de residencia' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_RESIDENCIA' , 'COLUMN' , 'TPR_TIPO' 
GO

ALTER TABLE TIPOS_RESIDENCIA ADD CONSTRAINT PK_TPR PRIMARY KEY CLUSTERED (TPR_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPOS_TELEFONE 
    (
     TPT_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     TPT_TIPO VARCHAR (100) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária para representar o registro do tipo de telefone' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_TELEFONE' , 'COLUMN' , 'TPT_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o tipo de telefone' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_TELEFONE' , 'COLUMN' , 'TPT_TIPO' 
GO

ALTER TABLE TIPOS_TELEFONE ADD CONSTRAINT PK_TPT PRIMARY KEY CLUSTERED (TPT_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE TIPOS_USUARIO 
    (
     TPU_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     TPU_TIPO VARCHAR (100) NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária que representa o registro dos tipos  dos usuarios' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_USUARIO' , 'COLUMN' , 'TPU_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o tipo de usuario' , 'USER' , 'dbo' , 'TABLE' , 'TIPOS_USUARIO' , 'COLUMN' , 'TPU_TIPO' 
GO

ALTER TABLE TIPOS_USUARIO ADD CONSTRAINT PK_TPU PRIMARY KEY CLUSTERED (TPU_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIOS 
    (
     USU_ID INTEGER NOT NULL IDENTITY NOT FOR REPLICATION , 
     USU_NOME VARCHAR (200) NOT NULL , 
     USU_EMAIL VARCHAR (200) NOT NULL , 
     USU_SENHA VARCHAR (300) NOT NULL , 
     USU_CPF CHAR (14) NOT NULL , 
     USU_DATA_NASCIMENTO DATE NOT NULL , 
     USU_TLF_ID INTEGER NOT NULL , 
     USU_GNR_ID INTEGER NOT NULL , 
     USU_TPU_ID INTEGER NOT NULL 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , 'Chave primária que representa o registro do usuario' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_ID' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o nome do usuario' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_NOME' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o email do usuario' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_EMAIL' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o senha  do usuario(deverá ser criptografada)' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_SENHA' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o CPF  do usuario' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_CPF' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Coluna que identifica o data de nascimento do usuario' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_DATA_NASCIMENTO' 
GO



EXEC sp_addextendedproperty 'MS_Description' , 'Chave estrangeira que faz referencia aos telefones' , 'USER' , 'dbo' , 'TABLE' , 'USUARIOS' , 'COLUMN' , 'USU_TLF_ID' 
GO

ALTER TABLE USUARIOS ADD CONSTRAINT PK_USU PRIMARY KEY CLUSTERED (USU_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIOS_CARTOES 
    (
     USU_CRT_ID INTEGER NOT NULL , 
     CRT_USU_ID INTEGER NOT NULL 
    )
GO

ALTER TABLE USUARIOS_CARTOES ADD CONSTRAINT PK_USUARIOS_CARTOES PRIMARY KEY CLUSTERED (USU_CRT_ID, CRT_USU_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIOS_ENDERECOS 
    (
     END_USU_ID INTEGER NOT NULL , 
     USU_END_ID INTEGER NOT NULL 
    )
GO

ALTER TABLE USUARIOS_ENDERECOS ADD CONSTRAINT PK_USUARIO_ENDERECO PRIMARY KEY CLUSTERED (END_USU_ID, USU_END_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE USUARIOS_TELEFONES 
    (
     TLF_USU_ID INTEGER NOT NULL , 
     USU_TLF_ID INTEGER NOT NULL 
    )
GO

ALTER TABLE USUARIOS_TELEFONES ADD CONSTRAINT PK_USUARIOS_TELEFONES PRIMARY KEY CLUSTERED (TLF_USU_ID, USU_TLF_ID)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE MUNICIPIOS 
    ADD CONSTRAINT F_MUC_EST FOREIGN KEY 
    ( 
     MUC_EST_ID
    ) 
    REFERENCES ESTADOS 
    ( 
     EST_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE CARTOES 
    ADD CONSTRAINT FK_CRT_BDC FOREIGN KEY 
    ( 
     CRT_BDC_ID
    ) 
    REFERENCES BANDEIRAS_CARTAO 
    ( 
     BDC_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_CARTOES 
    ADD CONSTRAINT FK_CRT_USU FOREIGN KEY 
    ( 
     CRT_USU_ID
    ) 
    REFERENCES CARTOES 
    ( 
     CRT_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE ENDERECOS 
    ADD CONSTRAINT FK_END_MUC FOREIGN KEY 
    ( 
     END_MUC_ID
    ) 
    REFERENCES MUNICIPIOS 
    ( 
     MUC_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE ENDERECOS 
    ADD CONSTRAINT FK_END_TPL FOREIGN KEY 
    ( 
     END_TPL_ID
    ) 
    REFERENCES TIPOS_LOGRADOURO 
    ( 
     TPL_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE ENDERECOS 
    ADD CONSTRAINT FK_END_TPR FOREIGN KEY 
    ( 
     END_TPR_ID
    ) 
    REFERENCES TIPOS_RESIDENCIA 
    ( 
     TPR_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_ENDERECOS 
    ADD CONSTRAINT FK_END_USU FOREIGN KEY 
    ( 
     END_USU_ID
    ) 
    REFERENCES ENDERECOS 
    ( 
     END_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE ESTADOS 
    ADD CONSTRAINT FK_EST_PAS FOREIGN KEY 
    ( 
     EST_PAS_ID
    ) 
    REFERENCES PAISES 
    ( 
     PAS_Id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE TELEFONES 
    ADD CONSTRAINT " FK_TLF_TPT" FOREIGN KEY 
    ( 
     TLF_TPT_ID
    ) 
    REFERENCES TIPOS_TELEFONE 
    ( 
     TPT_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_TELEFONES 
    ADD CONSTRAINT FK_TLF_USU FOREIGN KEY 
    ( 
     TLF_USU_ID
    ) 
    REFERENCES TELEFONES 
    ( 
     TLF_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_CARTOES 
    ADD CONSTRAINT FK_USU_CRT FOREIGN KEY 
    ( 
     USU_CRT_ID
    ) 
    REFERENCES USUARIOS 
    ( 
     USU_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_ENDERECOS 
    ADD CONSTRAINT FK_USU_END FOREIGN KEY 
    ( 
     USU_END_ID
    ) 
    REFERENCES USUARIOS 
    ( 
     USU_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS 
    ADD CONSTRAINT FK_USU_GNR FOREIGN KEY 
    ( 
     USU_GNR_ID
    ) 
    REFERENCES GENEROS 
    ( 
     GNR_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS_TELEFONES 
    ADD CONSTRAINT FK_USU_TLF FOREIGN KEY 
    ( 
     USU_TLF_ID
    ) 
    REFERENCES USUARIOS 
    ( 
     USU_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE USUARIOS 
    ADD CONSTRAINT FK_USU_TPU FOREIGN KEY 
    ( 
     USU_TPU_ID
    ) 
    REFERENCES TIPOS_USUARIO 
    ( 
     TPU_ID 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO



-- Relatório do Resumo do Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                            16
-- CREATE INDEX                             0
-- ALTER TABLE                             31
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
