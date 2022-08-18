CREATE DATABASE APPFOTO;

USE APPFOTO;

CREATE TABLE FOTOGRAFOS

(
    idFotografo int identity(1,1) primary key not null,
    nome varchar(50) not null,
    especialidade varchar (15) not null,
    endereco varchar (50) not null,
	login varchar (50) not null,
	senha varchar (50) not null
);

UPDATE Fotografos
Set id =  id int identity(1,1) primary key

DROP TABLE Fotografos;

SELECT * FROM Fotografos;