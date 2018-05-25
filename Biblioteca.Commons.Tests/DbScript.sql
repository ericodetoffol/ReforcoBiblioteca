CREATE DATABASE DBBiblioteca;

USE DBBiblioteca;

CREATE TABLE [dbo].[TBLivro]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Titulo] VARCHAR(50) NOT NULL,
	[Tema] VARCHAR(50) NOT NULL,
	[Autor] VARCHAR(50) NOT NULL,
	[Volume] INT NOT NULL,
	[DataPublicacao] DATE NOT NULL,
	[Disponibilidade] BIT NOT NULL
)



CREATE TABLE [dbo].[TBEmprestimo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[NomeCliente] VARCHAR(50) NOT NULL,
	[LivroId] INT NOT NULL,
	[DataDevolucao] DATE NOT NULL,
	CONSTRAINT [FK_TBEmprestimo_TBLivro] FOREIGN KEY ([LivroId]) REFERENCES [dbo].[TBLivro] ([Id]) ON DELETE CASCADE
)