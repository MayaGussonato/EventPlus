CREATE DATABASE EventPlus;
GO

USE EventPlus;
GO

CREATE TABLE TipoUsuario
(
    IdTipoUsuario UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
    Titulo        VARCHAR(100)     NOT NULL
);

CREATE TABLE TipoEvento
(
    IdTipoEvento UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
    Titulo       VARCHAR(100)     NOT NULL
);

CREATE TABLE Usuario
(
    IdUsuario     UNIQUEIDENTIFIER PRIMARY KEY DEFAULT    ((NEWID())),
    Nome          VARCHAR(100)     NOT NULL,
    Email         VARCHAR(256)     UNIQUE      NOT NULL,
    Senha         VARCHAR(60)      NOT NULL,
    IdTipoUsuario UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TipoUsuario(IdTipoUsuario)

);
 GO

 CREATE TABLE Instituicao
 (
   IdInstituicao UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
   NomeFantasia  VARCHAR (100)     NOT NULL,
   CNPJ          VARCHAR (14)      UNIQUE      NOT NULL,
   Endereco      VARCHAR (100)     NOT NULL
 );
 GO

 CREATE TABLE Evento
 (
   IdEvento      UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
   Nome          VARCHAR  (100)     NOT NULL,
   DataEvento    DATETIME           NOT NULL,
   Descricao     TEXT               NOT NULL,
   IdtipoEvento  UNIQUEIDENTIFIER   FOREIGN KEY REFERENCES TipoEvento  (IdTipoEvento),
   IdInstituicao UNIQUEIDENTIFIER   FOREIGN KEY REFERENCES Instituicao (IdInstituicao),
 );

 CREATE TABLE Presenca
 (
  IdPresenca UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
  Situacao   BIT              NOT  NULL,
  IdUsuario UNIQUEIDENTIFIER  FOREIGN KEY REFERENCES  Usuario (IdUsuario),
  IdEvento  UNIQUEIDENTIFIER  FOREIGN KEY REFERENCES Evento  (IdEvento)
 );
 GO

 CREATE TABLE ComentarioEventos
 (
  IdComentarioEventos   UNIQUEIDENTIFIER PRIMARY KEY DEFAULT ((NEWID())),
  Descricao             VARCHAR          (200)   NOT NULL,
  Exibe                 BIT                      NOT NULL,
  DataComentarioevento  DATETIME                 NOT NULL,
  IdUsuario             UNIQUEIDENTIFIER FOREIGN KEY REFERENCES  Usuario (IdUsuario),
  IdEvento              UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Evento  (IdEvento)

 );
