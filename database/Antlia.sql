USE [Antlia]
GO
/****** Object:  Table [dbo].[MovimentoManual]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovimentoManual](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProdutoId] [int] NOT NULL,
	[ProdutoCosifId] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[Ano] [int] NOT NULL,
	[NumeroLancamento] [int] NOT NULL,
	[valor] [numeric](18, 2) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_MovimentoManual] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produto]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](30) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_Produto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdutoCosif]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdutoCosif](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProdutoId] [int] NOT NULL,
	[Codigo] [varchar](11) NOT NULL,
	[Classificacao] [varchar](6) NULL,
	[Status] [varchar](1) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_ProdutoCosif] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MovimentoManual]  WITH CHECK ADD  CONSTRAINT [FK_MovimentoManual_Produto] FOREIGN KEY([ProdutoId])
REFERENCES [dbo].[Produto] ([Id])
GO
ALTER TABLE [dbo].[MovimentoManual] CHECK CONSTRAINT [FK_MovimentoManual_Produto]
GO
ALTER TABLE [dbo].[MovimentoManual]  WITH CHECK ADD  CONSTRAINT [FK_MovimentoManual_ProdutoCosif] FOREIGN KEY([ProdutoCosifId])
REFERENCES [dbo].[ProdutoCosif] ([Id])
GO
ALTER TABLE [dbo].[MovimentoManual] CHECK CONSTRAINT [FK_MovimentoManual_ProdutoCosif]
GO
ALTER TABLE [dbo].[ProdutoCosif]  WITH CHECK ADD  CONSTRAINT [FK_ProdutoCosif_Produto] FOREIGN KEY([ProdutoId])
REFERENCES [dbo].[Produto] ([Id])
GO
ALTER TABLE [dbo].[ProdutoCosif] CHECK CONSTRAINT [FK_ProdutoCosif_Produto]
GO
/****** Object:  StoredProcedure [dbo].[spMovimentoManualCreate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spMovimentoManualCreate]
(
	  @ProdutoId		int
	, @ProdutoCosifId	int
	, @Mes				int
	, @Ano				int
	, @NumeroLancamento	int
	, @valor			numeric(18,2)
	, @Descricao		varchar(50)
)
as
begin
	insert into MovimentoManual with(rowlock)
	(
		  ProdutoId
		, ProdutoCosifId
		, Mes
		, Ano
		, NumeroLancamento
		, valor
		, Descricao
		, DataCriacao
	)
	values
	(
		  @ProdutoId
		, @ProdutoCosifId
		, @Mes
		, @Ano
		, @NumeroLancamento
		, @valor
		, @Descricao
		, GETDATE()
	)
	select @@IDENTITY as Id
end
GO
/****** Object:  StoredProcedure [dbo].[spMovimentoManualRemove]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spMovimentoManualRemove]
(
	  @Id			int
)
as
begin
	delete from MovimentoManual with(rowlock) where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[spMovimentoManualSelect]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMovimentoManualSelect]
(
	  @Id				int			= null
	, @ProdutoId		int			= null
	, @ProdutoCosifId	int			= null
	, @Mes				int			= null
	, @Ano				int			= null
	, @NumeroLancamento	int			= null
	, @Descricao		varchar(50)	= null
)
as
begin
	select 
		  Id
		, ProdutoId
		, ProdutoCosifId
		, Mes
		, Ano
		, NumeroLancamento
		, valor
		, Descricao
		, DataCriacao
		, DataAlteracao
	from
		MovimentoManual with(nolock)
	where
		(@Id is null or Id = @Id)
	and
		(@ProdutoId is null or ProdutoId = @ProdutoId)
	and
		(@ProdutoCosifId is null or ProdutoCosifId = @ProdutoCosifId) 
	and
		(@Mes is null or Mes = @Mes)
	and
		(@Ano is null or Ano = @Ano)
	and
		(@NumeroLancamento is null or NumeroLancamento = @NumeroLancamento)
	and
		(@Descricao is null or Descricao like '%'+ @Descricao + '%')
	order by
		Id, Mes, Ano, NumeroLancamento asc
end
GO
/****** Object:  StoredProcedure [dbo].[spMovimentoManualUpdate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spMovimentoManualUpdate]
(
	  @Id				int
	, @ProdutoId		int
	, @ProdutoCosifId	int
	, @Mes				int
	, @Ano				int
	, @NumeroLancamento	int
	, @valor			numeric(18,2)
	, @Descricao		varchar(50)
)
as
begin
	update MovimentoManual with(rowlock) set
		  ProdutoId = @ProdutoId
		, ProdutoCosifId = @ProdutoCosifId
		, Mes = @Mes
		, Ano = @Ano
		, NumeroLancamento = @NumeroLancamento
		, valor = @valor
		, Descricao = @Descricao
		, DataAlteracao = GETDATE()
	where
		Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoCosifCreate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoCosifCreate]
(
	  @ProdutoId		int
	, @Codigo			varchar(11)
	, @Classificacao	varchar(6) = null
	, @Status			varchar(1)
)
as
begin
	insert into ProdutoCosif with(rowlock)
	(
		  ProdutoId
		, Codigo
		, Classificacao
		, Status
		, DataCriacao
	)
	values
	(
		  @ProdutoId
		, @Codigo
		, @Classificacao
		, @Status
		, GETDATE()
	)
	select @@IDENTITY as Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoCosifRemove]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spProdutoCosifRemove]
(
	  @Id			int
)
as
begin
	delete from ProdutoCosif with(rowlock) where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoCosifSelect]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoCosifSelect]
(
	  @Id				int			= null
	, @ProdutoId		int			= null
	, @Codigo			varchar(11)	= null
	, @Classificacao	varchar(6)	= null
	, @Status			varchar(1)	= null
)
as
begin
	select 
		  Id
		, ProdutoId
		, Codigo
		, Classificacao
		, Status
		, DataCriacao
		, DataAlteracao
	from
		ProdutoCosif with(nolock)
	where
		(@Id is null or Id = @Id)
	and
		(@ProdutoId is null or ProdutoId = @ProdutoId)
	and
		(@Codigo is null or Codigo like '%' + @Codigo + '%') 
	and
		(@Classificacao is null or Classificacao = @Classificacao)
	and
		(@Status is null or Status = @Status)
	order by
		Id, Codigo, Classificacao, Status asc
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoCosifUpdate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoCosifUpdate]
(
	  @Id				int
	, @ProdutoId		int
	, @Codigo			varchar(11)
	, @Classificacao	varchar(6) =  null
	, @Status			varchar(1)
)
as
begin
	update ProdutoCosif with(rowlock) set
		  ProdutoId = @ProdutoId
		, Codigo = @Codigo
		, Classificacao = @Classificacao
		, Status = @Status
		, DataAlteracao = GETDATE()
	where
		Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoCreate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoCreate]
(
	  @Descricao	varchar(30)
	, @Status		varchar(1)
)
as
begin
	insert into Produto with(rowlock)
	(
		  Descricao
		, Status
		, DataCriacao
	)
	values
	(
		  @Descricao
		, @Status
		, GETDATE()
	)
	select @@IDENTITY as Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoRemove]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spProdutoRemove]
(
	  @Id			int
)
as
begin
	delete from Produto with(rowlock) where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoSelect]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoSelect]
(
	  @Id			int			= null
	, @Descricao	varchar(30) = null
	, @Status		varchar(1)	= null
)
as
begin
	select 
		  Id
		, Descricao
		, Status
		, DataCriacao
		, DataAlteracao
	from
		Produto with(nolock)
	where
		(@Id is null or Id = @Id)
	and
		(@Descricao is null or Descricao like'%' + @Descricao + '%')
	and
		(@Status is null or Status = @Status) 
	order by
		Id, Status asc
end
GO
/****** Object:  StoredProcedure [dbo].[spProdutoUpdate]    Script Date: 12/06/2025 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[spProdutoUpdate]
(
	  @Id			int
	, @Descricao	varchar(30)
	, @Status		varchar(1)
)
as
begin
	update Produto with(rowlock) set
		  Descricao = @Descricao
		, Status = @Status
		, DataAlteracao = GETDATE()
	where
		Id = @Id
end
GO
