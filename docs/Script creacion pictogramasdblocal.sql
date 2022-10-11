USE [master]
GO
/****** Object:  Database [pictogramasdblocal]    Script Date: 22/8/2022 20:57:16 ******/
CREATE DATABASE [pictogramasdblocal]
 CONTAINMENT = NONE
 --ON  PRIMARY 
--( NAME = N'pictogramasdblocal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\pictogramasdblocal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
-- LOG ON 
--( NAME = N'pictogramasdblocal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\pictogramasdblocal_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [pictogramasdblocal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pictogramasdblocal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pictogramasdblocal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET ARITHABORT OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pictogramasdblocal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pictogramasdblocal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [pictogramasdblocal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pictogramasdblocal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET RECOVERY FULL 
GO
ALTER DATABASE [pictogramasdblocal] SET  MULTI_USER 
GO
ALTER DATABASE [pictogramasdblocal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pictogramasdblocal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pictogramasdblocal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pictogramasdblocal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pictogramasdblocal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pictogramasdblocal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'pictogramasdblocal', N'ON'
GO
ALTER DATABASE [pictogramasdblocal] SET QUERY_STORE = OFF
GO
USE [pictogramasdblocal]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[CategoriaPadre] [int] NULL,
	[Nivel] [int] NULL,
	[NombreOriginal] [varchar](50) NULL,
CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriasPorUsuarios]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasPorUsuarios](
	[Id] varchar(30) NOT NULL,
	[UsuarioId] [int] NULL,
	[CategoriaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Keywords]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Keywords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Keyword] [varchar](65) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Meaning] [varchar](1200) NOT NULL,
	[Plural] [varchar](70) NOT NULL,
	[HasLocution] [bit] NOT NULL,
	[IdPictograma] [int] NOT NULL,
 CONSTRAINT [PK_Keywords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pictogramas]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictogramas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Schematic] [bit] NOT NULL,
	[Sex] [bit] NOT NULL,
	[Violence] [bit] NOT NULL,
	[Aac] [bit] NOT NULL,
	[AacColor] [bit] NOT NULL,
	[Skin] [bit] NOT NULL,
	[Hair] [bit] NOT NULL,
	[IdArasaac] [int] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Pictogramas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PictogramasPorCategorias]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictogramasPorCategorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPictograma] [int] NOT NULL,
	[IdCategoria] [int] NOT NULL,
 CONSTRAINT [PK_PictogramaPorCategoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 22/8/2022 20:57:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
	[Schematic] [bit] NOT NULL,
	[Sex] [bit] NOT NULL,
	[Violence] [bit] NOT NULL,
	[Aac] [bit] NOT NULL,
	[AacColor] [bit] NOT NULL,
	[Skin] [bit] NOT NULL,
	[Hair] [bit] NOT NULL,
	[Nivel] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[FavoritosPorUsuarios]    Script Date: 30/9/2022 13:00:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FavoritosPorUsuarios](
	[Id] [varchar](30) NOT NULL,
	[UsuarioId] [int] NULL,
	[PictogramaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FavoritosPorUsuarios]  WITH CHECK ADD FOREIGN KEY([PictogramaId])
REFERENCES [dbo].[Pictogramas] ([Id])
GO

ALTER TABLE [dbo].[FavoritosPorUsuarios]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO

CREATE TABLE [dbo].[Pizarras](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Filas] [int] NOT NULL,
	[Columnas] [int] NOT NULL,
	[Nombre] [varchar](MAX) NOT NULL,
	[UltimaActualizacion] datetime2 NOT NULL,

 CONSTRAINT [PK_Pizarras] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pizarras]  WITH CHECK ADD  CONSTRAINT [FK_Pizarras_Usuarios] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Pizarras] CHECK CONSTRAINT [FK_Pizarras_Usuarios]
GO
/** Object:  Table [dbo].[CeldaPizarra]    Script Date: 6/9/2022 19:52:07 **/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CeldaPizarra](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [PizarraId] [int] NOT NULL,
    [Fila] [int] NOT NULL,
    [Columna] [int] NOT NULL,
    [Contenido] [nvarchar](100) NULL,
    [TipoContenido] [nvarchar](50) NULL,
    [Color] [nvarchar](10) NULL,
    [Identificacion] [nvarchar](50) NULL,
 CONSTRAINT [PK_CeldaPizarra] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CeldaPizarra]  WITH CHECK ADD  CONSTRAINT [FK_CeldaPizarra_Pizarras] FOREIGN KEY([PizarraId])
REFERENCES [dbo].[Pizarras] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CeldaPizarra] CHECK CONSTRAINT [FK_CeldaPizarra_Pizarras]
GO


ALTER TABLE [dbo].[Categorias] ADD  DEFAULT ((0)) FOR [Nivel]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Nivel]
GO
ALTER TABLE [dbo].[CategoriasPorUsuarios]  WITH CHECK ADD FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[CategoriasPorUsuarios]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Keywords]  WITH CHECK ADD  CONSTRAINT [FK_Keywords_Pictogramas] FOREIGN KEY([IdPictograma])
REFERENCES [dbo].[Pictogramas] ([Id])
GO
ALTER TABLE [dbo].[Keywords] CHECK CONSTRAINT [FK_Keywords_Pictogramas]
GO
ALTER TABLE [dbo].[PictogramasPorCategorias]  WITH CHECK ADD  CONSTRAINT [FK_PictogramaPorCategoria_Categorias] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[PictogramasPorCategorias] CHECK CONSTRAINT [FK_PictogramaPorCategoria_Categorias]
GO
ALTER TABLE [dbo].[PictogramasPorCategorias]  WITH CHECK ADD  CONSTRAINT [FK_PictogramaPorCategoria_Pictogramas] FOREIGN KEY([IdPictograma])
REFERENCES [dbo].[Pictogramas] ([Id])
GO
ALTER TABLE [dbo].[PictogramasPorCategorias] CHECK CONSTRAINT [FK_PictogramaPorCategoria_Pictogramas]
GO
USE [master]
GO
ALTER DATABASE [pictogramasdblocal] SET  READ_WRITE 
GO
