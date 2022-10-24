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
	[Id] [int] NOT NULL,
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
	[Identificador] [varchar](30) NULL,
	[UltimaActualizacion] datetime2 NULL,
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
	[UltimaActualizacion] [datetime2] NULL,
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

use pictogramasdblocal
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('1','Infraestructura','infrastructure','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('2','Transporte terrestre','land transport','479',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('3','Seguridad vial','road safety','350',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('4','Bebida','beverage','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('5','Alimento de origen mineral','mineral rich food','111',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('6','Ajuar','trousseau','541','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('7','Vocabulario nuclear','core vocabulary-object','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('8','COVID-19','covid-19','377',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('9','Joya','jewelry','522',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('10','Complemento','accessories','522',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('11','Aparato de comunicación','mass media device','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('12','Arácnido','arachnid','513',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('13','Ovíparo','oviparous','533',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('14','Animal terrestre','terrestrial animal','629',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('15','Carnívoro','carnivorous','554',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('16','Halloween','halloween','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('17','Animal salvaje','wild animal','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('18','Vocabulario nuclear','core vocabulary-living being','486',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('19','Vocabulario nuclear','core vocabulary-time','575',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('20','Colectivo','group','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('21','Árbol','tree','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('22','Parque infantil','playground','311',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('23','Herbívoro','herbivorous','554',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('24','Mamífero','mammal','453',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('25','Vivíparo','viviparous','533',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('26','Mobiliario','furniture','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('27','Gastronomía','gastronomy','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('28','Equipamiento del edificio','building facility','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('29','Construcción','construction','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('30','Edificio residencial','residential building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('31','Vocabulario nuclear','core vocabulary-place','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('32','Vocabulario central','core vocabulary-work','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('33','Adjetivo calificativo','qualifying adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('34','Sentimiento','feeling','625',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('35','Insecto','insect','513',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('36','Animal aéreo','flying animal','629',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('37','Apicultura','apiculture','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('38','Animal doméstico','domestic animal','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('39','Ropa','clothes','522',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('40','Anciano','elderly','467',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('41','Familia','family','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('42','Condimento','condiment','86',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('43','Cocina','cookery','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('44','Vocabulario nuclear','core vocabulary-feeding','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('45','Vocabulario nuclear','core vocabulary-movement','328',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('46','Transporte aéreo','aerial transport','479',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('47','Utensilio de cocina','utensil','541',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('48','Transporte acuático','water transport','479',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('49','Electrodoméstico','electrical appliance','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('50','Bebé','baby','467',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('51','Verbo','verb','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('52','Necesidades básicas','basic needs','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('53','Omnívoro','omnivorous','554',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('54','Animal marino','marine animal','143',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('55','Material deportivo','sport material','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('56','Fútbol','football','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('57','Alimento procesado','processed food','111',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('58','Material educativo','educational material','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('59','Aparato de iluminación','light fixture','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('60','Juguete','toy','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('61','Juego popular','traditional game','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('62','Calzado','footwear','522',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('63','Vajilla','crockery','541',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('64','Recipiente','container','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('65','Tarea educativa','educational task','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('66','Natación','swimming','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('67','Ropa deportiva','sportswear','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('68','Piscina','swimming pool','290',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('69','Playa','beach','290',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('70','Vocabulario nuclear','core vocabulary-leisure','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('71','Higiene corporal','corporal hygiene','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('72','Telecomunicación','telecommunication','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('73','Medio de comunicación','mass media','561',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('74','Espacio urbano','urban area','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('75','Sensación corporal','body sensation','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('76','Navidad','christmas','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('77','Síntoma','symptom','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('78','Arte musical','musical art','617',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('79','Carne','meat','191',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('80','Mobiliario urbano','street furniture','74',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('81','Crustáceo','crustacean','513',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('82','Instrumento de percusión','percussion instrument','466',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('83','Fiestas del Pilar','fiestas del pilar','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('84','Producto de higiene','hygiene product','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('85','Rutina','routine','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('86','Alimento ultraprocesado','ultra-processed food','111',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('87','Ganadería','cattle farming','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('88','Fruta','fruit','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('89','Verdura','vegetable','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('90','Elemento arquitectónico','architectural element','161',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('91','Ave','bird','453',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('92','Animal de río','river animal','143',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('93','Embutido','cold meat','57',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('94','Reptil','reptile','453',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('95','Hardware','hardware','234',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('96','Vocabulario nuclear','core vocabulary-knowledge','616','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('97','Verbos usuales','usual verbs','51',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('98','Vocabulario nuclear','core vocabulary-communication','561',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('99','Cubertería','cutlery','541',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('100','Profesional','professional','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('101','Servicios personales','personal services','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('102','Caza','hunting','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('103','Reciclaje','recycling','501',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('104','Videojuego','video game','506',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('105','Repostería','baking','122',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('106','Pintura','painting','503',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('107','Objeto de decoración','decorative item','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('108','Ropa de trabajo','workwear','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('109','Género literario','literacy genre','126',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('110','Biblioteconomía','library science','619',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('111','Alimento','food','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('112','Vocabulario nuclear','core vocabulary-education','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('113','Sentidos','senses','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('114','Equipo de música','music device','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('115','Máquina de trabajo','work machine','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('116','Personal sanitario','health personnel','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('117','Profesionales de la Sanidad','sanitary professional','100',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('118','Hongo','fungus','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('119','Enfermedad','disease','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('120','Instrumento de viento','wind instrument','466',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('121','Instrumento de cuerda','string instrument','466',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('122','Postre','dessert','86',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('123','Fisioterapia','physiotherapy','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('124','Mascota','pet','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('125','Producto lácteo','dairy product','191',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('126','Literatura','literature','619',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('127','Objeto','object',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('128','Logopedia','speech therapy','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('129','Personal educativo','educational staff','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('130','Profesionales de la Educación','education professionals','100',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('131','Ovoproducto','egg product','191',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('132','Niño','child','467',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('133','Atención a la diversidad','special education','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('134','Juego de mesa','board game','506',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('135','Patinaje','skating','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('136','Pescado','fish','191',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('137','Plato típico','traditional dish','27',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('138','Peluquería','hairdresser','101',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('139','Industria textil','clothing industry','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('140','Instrumento de teclado','keyboard instrument','466',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('141','Pez','fish-animal','453','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('142','Ovovivíparo','ovoviviparous','533',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('143','Animal acuático','aquatic animal','629',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('144','Anfibio','amphibian','453',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('145','Dispositivo cronológico','chronological device','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('146','Instrumento cronológico','chronological instrument','604',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('147','Alimentación','feeding',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('148','Señal de tráfico','traffic signal','3',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('149','Herramienta de trabajo','work tool','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('150','Cumpleaños','birthday','526',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('151','Agricultura','agriculture','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('152','Pronombre personal','personal pronoun','577',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('153','Tauromaquia','bullfighting','225',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('154','Alcoholismo','alcoholism','443',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('155','Número','number','246',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('156','Justicia','law and justice','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('157','Derecho','law','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('158','Jardinería','gardening','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('159','Fruto seco','dried fruit','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('160','Danza','dance','225',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('161','Arquitectura','architecture','622',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('162','Producto de limpieza','cleaning product','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('163','Molusco','mollusc','556',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('164','Pesca','fishing','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('165','Anatomía humana','human anatomy','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('166','Rasgos físicos','physical features','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('167','Color','color','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('168','Comercio','trade','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('169','Hábitat','natural habitat','406',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('170','Montaña','mountain','290',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('171','Aparato digestivo','digestive system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('172','Aparato respiratorio','respiratory system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('173','Vocabulario nuclear','core vocabulary-miscellaneous','377',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('174','Seguridad y defensa','security and defense','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('175','Legumbre','legume','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('176','Hostelería','hospitality industry','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('177','Espacio rural','rural area','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('178','Aparato','appliance','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('179','Fotografía','photography','503',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('180','Materia derivada','derived material','550',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('181','Odontología','odontology','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('182','Sistema nervioso','nervous system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('183','Carpintería','carpentry','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('184','Edificio comercial','commercial building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('185','Dulce y caramelo','sweets','86',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('186','Sistema articular','joint system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('187','Astronomía','astronomy','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('188','Sistema circulatorio','circulatory system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('189','Disfraz','costume','39',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('190','Ejercicio físico','physical exercise','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('191','Alimento de origen animal','animal-based food','111',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('192','Accidente geográfico','landform','406',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('193','Prehistoria','prehistory','528',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('194','Dinosaurio','dinosaur','521',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('195','Cosmético','cosmetic','522',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('196','Tabaquismo','smoking','443',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('197','Fenómeno atmosférico','atmospheric phenomena','293',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('198','Veterinaria','veterinary medicine','622',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('199','Material de enfermería','nursing equipment','389',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('200','Guerra','war','314',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('201','Verano','summer','630',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('202','Río','river','192',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('203','Aparato excretor','excretory system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('204','Publicación','publication','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('205','Turismo','tourism','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('206','Artista visual','visual artist','539',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('207','Artista profesional','professional artist','617',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('208','Planeta','planet','187',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('209','Arma','weapon','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('210','Sistema tegumentario','integumentary system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('211','Sistema visual','visual system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('212','Aparato locomotor','locomotor system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('213','Vendedor','seller','100',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('214','Circo','circus','225',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('215','Espacio recreativo','entertainment facility','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('216','Medicamento','medicament','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('217','Equipamiento educativo','educational equipment','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('218','cuidado de los niños','childcare','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('219','Oftalmología','ophthalmology','624',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('220','Sistema auditivo','auditory system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('221','Deportista','athlete','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('222','Sistema muscular','muscular system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('223','Artista musical','musical artist','539',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('224','Mar','sea and oceans','192',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('225','Arte escénico','scenic art','617',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('226','Celentéreo','coelenterata','556',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('227','Anélido','annelid','556',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('228','Silvicultura','forestry','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('229','Materia de origen animal','animal material','511',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('230','Ingeniería','engineering','622',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('231','Servicios profesionales','professional services','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('232','Industria','industry','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('233','TIC','information technology','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('234','Informática','computing','622',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('235','Sitio de trabajo','worksite','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('236','Lugar de trabajo','workplace','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('237','Sistema óseo','osseous system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('238','Herbácea','herbaceous plant','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('239','Primavera','spring','630',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('240','Servicios de transporte','transport services','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('241','Servicios de limpieza','waste disposal','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('242','Buceo','diving','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('243','Higiene íntima','intimate hygiene','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('244','Letra','letter','246',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('245','Arbusto','bush','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('246','Alfabeto','alphabet','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('247','Ajedrez','chess','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('248','Edificio público','public building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('249','Administración pública','public administration','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('250','Representación política','political representation','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('251','Instalación','facility','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('252','Personaje religioso','religious character','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('253','Cristianismo','christianity','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('254','Servicios financieros','financial services','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('255','Cactus','cactus','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('256','Edificio cultural','cultural building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('257','Instalación deportiva','sports facility','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('258','Acto religioso','religious act','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('259','Evento religioso','religious event','314',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('260','Calendario','calendar','604',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('261','Flor','flower','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('262','Carnaval','carnival','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('263','Centro educativo','educational building','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('264','Edificio educativo','educational_building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('265','Institución educativa','educational institution','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('266','Ciclismo','cycling','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('267','Edificio de servicios','service building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('268','Modalidad deportiva','sport modality','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('269','Evento popular','popular event','314',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('270','Juego de azar','gambling','506',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('271','Anatomía vegetal','plant anatomy','401',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('272','Gimnasia','gymnastics','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('273','Gimnasia rítmica','rhythmic gymnastics','272',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('274','Centro médico','medical center','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('275','Edificio sanitario','medical centre','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('276','Edificio religioso','religious building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('277','Lugar religioso','religious place','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('278','Vocabulario nuclear','core vocabulary-religion','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('279','Personaje','character','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('280','Invierno','winter','630',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('281','Semana Santa','easter week','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('282','Geología','geology','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('283','Educación','education',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('284','Actividad lectiva','teaching activity','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('285','Persona de estado','leader','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('286','Momento del día','day time','260',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('287','Juego de cartas','card game','506',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('288','Signo ortográfico','orthographic sign','246',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('289','Matemáticas','mathematics','623',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('290','Actividad al aire libre','outdoor activity','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('291','Hípica','horse riding','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('292','Defunción','death','526',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('293','Meteorología','meteorology','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('294','Respuesta humana','human response','625',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('295','Baloncesto','basketball','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('296','Artesanía','craftsmanship','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('297','Procedimiento médico','medical procedure','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('298','Paciente','patient','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('299','Equinodermo','echinoderm','556',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('300','Vivienda animal','animal housing','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('301','Aparato reproductor','reproductive system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('302','Psicología','psychology','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('303','Alimento de origen vegetal','plant-based food','111',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('304','Accidente de tráfico','traffic accident','350',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('305','Artista escénico','performing artist','539',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('306','Teatro','theater','225',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('307','Sexualidad','sexuality','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('308','Sabor','taste','147',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('309','Textura','texture','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('310','Reglamento deportivo','sport rules','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('311','Instalación recreativa','recreational facility','251',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('312','Establecimiento de restauración','catering establishment','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('313','Conceptos básicos','basic concepts','289',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('314','Evento','event','604',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('315','Accidente laboral','work accident','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('316','Forma','shape','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('317','Geometría','geometry','289',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('318','Cine','cinema','225',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('319','Categorización','categorization','377',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('320','Dinero','money','333',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('321','Organización educativa','educational organization','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('322','Conducta disruptiva','disruptive behavior','625',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('323','Tamaño','size','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('324','Adulto','adult','467',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('325','Desastre natural','natural disaster','293',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('326','Adolescente','teenager','467',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('327','Delito','crime','157',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('328','Desplazamiento','movement',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('329','Documento comercial','trade document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('330','Violencia de género','gender violence','327',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('331','Química','chemistry','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('332','Unidad de medida','measurement unit','289',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('333','Economía','economy','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('334','Adjetivo ordinal','ordinal adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('335','Fórmula de cortesía','polite set expression','394',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('336','Espectáculo','show','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('337','Moneda','coin','320',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('338','Billete','bill','320',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('339','Material de psicomotricidad','psychomotor equipment','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('340','Otoño','autumn','630',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('342','Señalética','signaling system','561',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('343','Año nuevo','new year','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('345','Atletismo','athletics','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('346','Divisa','foreign currency','320',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('347','Adverbio de lugar','adverb of place','592',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('348','Continente','continent','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('349','Anatomía animal','animal anatomy','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('350','Tráfico','traffic','328',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('351','Adjetivo indefinido','indefinite adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('352','Pronombre indefinido','indefinite pronoun','577',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('355','Baile regional','regional dance','160',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('356','Monumento','monument','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('357','Personaje de cuento','tale character','360',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('358','Adverbio de modo','adverb of manner','592',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('359','Personaje histórico','historical character','279',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('360','Personaje literario','book character','279',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('361','Edad Media','middle ages','528',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('363','Personaje de cine','movie character','279',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('364','Fiesta popular','popular festival','269',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('365','Estación','season','260',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('368','Geografía política','political geography','510',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('370','Adverbio de negación','adverb of denial','592',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('371','Pueblos indígenas','indigenouos people','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('372','Adverbio de tiempo','adverb of time','592',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('373','Evento deportivo','sport event','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('374','Componente de vehículo','vehicle component','479',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('375','Personaje mitológico','mythological character','279',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('376','Adverbio de afirmación','adverb of affirmation','592',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('377','Miscelánea','miscellaneous',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('378','Provincia','province','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('379','Bandera','flag','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('380','Afición','hobby','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('381','Alumnado','students','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('382','Prueba médica','medical test','297',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('383','Producto ortopédico','orthopedic product','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('384','Dependencia educativo','educational space','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('385','Materia de origen mineral','mineral origin material','511',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('386','Habitación','room','30','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('387','Materia de origen fósil','fossil origin material','511',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('388','Minería','mining industry','563',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('389','Material médico','medical equipment','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('390','Documento acreditativo','supporting document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('391','Producto de apoyo para la comunicación','communication aid','423',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('392','Sala de edificio','building room','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('393','Edificio','building','473',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('394','Expresión','expression','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('395','Sala de hospital','hospital room','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('396','Sistema de comunicación','communication system','423',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('397','Organización laboral','work organization','559',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('398','Mes','month','260',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('399','Posición corporal','body position','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('400','Hábito saludable','healthy habit','578',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('401','Planta','plant','486',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('402','Oceanografía','oceanography','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('403','Materia de origen vegetal','vegetal origin material','511',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('404','Notación musical','musical notation','78',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('405','Programa de TV','tv show','73',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('406','Geografía física','physical geography','510',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('407','Movimiento social','social movement','600',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('408','Fisiología animal','animal physiology','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('409','Día de la semana','day','260',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('410','Obstetricia','obstetrics','525',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('411','Equinoterapia','equine-assisted therapy','532',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('412','Contaminación','pollution','501',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('414','Animal','animal','486',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('415','Unidad de tiempo','unit of time','604',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('416','Adjetivo demostrativo','demonstrative adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('417','Dietética','dietetics','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('418','Onomatopeya','onomatopoeia','529',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('419','Comportamiento animal','animal behaviour','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('420','Cereal','cereal','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('421','Documento','document',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('422','Boda','wedding','526',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('423','Comunicación aumentativa','augmentative communication','561',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('424','Deporte','sport','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('425','Fisiología','physiology','568',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('426','Preposición','preposition','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('427','Patrón','pattern','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('428','Hábito insano','unhealthy habit','578',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('429','Fisiología vegetal','plant physiology','401',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('430','Material de estimulación sensorial','sensory stimulation material','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('431','Documento informativo','information document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('432','Documento oficial','official document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('433','Tratamiento','medical treatment','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('435','Pronombre interrogativo','interrogative pronoun','577',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('436','País','country','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('437','Valor humano','human value','625',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('438','Personaje de cómic','comic character','279',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('439','Energía','energy','560',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('440','Frase hecha','set phrase','394',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('441','Dibujo','drawing','503',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('442','Biología','biology','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('443','Drogadicción','drug addiction','544',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('444','Estado civil','marital status','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('445','Región','region','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('446','Física','physics','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('447','Egipto','egypt','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('448','Arqueología','archaeology','546',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('450','Aparato médico','medical device','389',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('451','Roma','rome','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('452','Grecia','greece','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('453','Vertebrado','vertebrate','627',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('454','Artículo','article','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('455','Deporte adaptado','adapted sport','424',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('456','Ciudad','city','368',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('457','Islamismo','islamism','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('458','Judaísmo','judaism','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('459','Escultura','sculpture','503',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('460','Vocabulario nuclear','core vocabulary-document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('461','Edificio industrial','industrial building','393',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('462','Planta aromática','aromatic plant','601',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('463','Hierba aromática','aromatic herb','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('464','Identidad de género','gender identity','307',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('465','Marisco','seafood','191',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('466','Instrumento musical','musical instrument','78',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('467','Personas según su edad','person according to their age','558','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('468','Canoterapia','dog-assisted therapy','532',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('469','Organismo internacional','international organziation','377',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('470','Ciencias de la salud','health sciences','618',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('471','Hinduismo','hinduism','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('472','Equipo de protección','protective equipment','108',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('473','Lugar','place',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('474','Léxico','lexicon','516',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('475','Ruta','route','350',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('476','Grupo de animales','animal group','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('477','Asignatura','subject','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('478','Orientación sexual','sexual orientation','307',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('479','Medio de transporte','mode of transport','350',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('480','Conjunción concesiva','concessive conjunction','611',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('481','Reproducción humana','human reproduction','568','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('482','Conjunción copulativa','copulative conjunction','611',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('483','Persona religiosa','religious person','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('484','Conjunción casual','causal conjunction','611',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('485','Conjunción adversativa','adversative conjunction','611',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('486','Ser vivo','living being',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('487','Ocio','leisure',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('488','Cultura','culture','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('489','Terapia ocupacional','occupational therapy','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('490','Adjetivo posesivo','possessive adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('491','Pronombre posesivo','possessive pronoun','577',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('492','Práctica sexual','sexual practice','307',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('493','Método anticonceptivo','contraceptive method','307',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('494','Documento judicial','court document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('495','Praxia orofacial','orofacial praxis','377',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('496','Acoso escolar','bullying','327',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('497','Terapia','therapy','302',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('498','Medicina','medicine','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('499','Documentación médica','medical documentation','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('500','Documento médico','medical document','498',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('501','Ciencias ambientales','environmental science','618',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('502','Especias','spices','303',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('503','Arte visual','visual art','617',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('504','Karate','karate','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('505','Material de fisioterapia','physiotherapy equipment','389',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('506','Juego','game','487',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('507','Religión','religion',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('508','Objeto religioso','religious object','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('509','Budismo','buddhism','507',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('510','Geografía','geography','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('511','Materia prima','raw material','550',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('512','Metodología educativa','educational methodology','283',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('513','Artrópodo','arthropod','556',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('514','Traje regional','regional costume','39',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('515','Fósil','fossil','521',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('516','Lenguaje','language','561',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('517','Ópera','opera','78',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('518','Conjunción condicional','conditional conjunction','611',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('519','Clasificación Decimal Universal','universal decimal classification','110',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('520','Horas del día','day hours','415',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('521','Ser extinto','extinct being','486',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('522','Moda','fashion','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('523','Delito sexual','sexual crime','625',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('524','Sistema inmunitario','immune system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('525','Ginecología','gynecology','624',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('526','Evento social','social event','314',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('527','Adjetivo','adjective','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('528','Períodos','periods','546',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('529','Interjección','interjection','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('530','Edad Antigua','ancient history','528',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('531','Surf','surf','626',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('532','Terapia asistida con animales','animal-assisted therapy','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('533','Reproducción animal','animal reproduction','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('534','Punto limpio','recycling center','251',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('535','Consultoría','consultancy','570',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('536','Paleontología','paleontology','620',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('537','Institución jurídica','legal institution','156',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('538','Sistema endócrino','endocrine system','425',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('539','Artista','artist','100',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('540','Documento educativo','educational document','421',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('541','Menaje','household','127',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('542','Anatomía del dinosaurio','dinosaur anatomy','194',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('543','Medida de prevención','prevention measure','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('544','Adicción','addiction','428',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('545','Edad Moderna','early modern period','528',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('546','Historia','history','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('547','ibéricos','iberians','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('548','Instrumento atmosférico','atmospheric device','178',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('549','Adjetivo comparativo','comparative adjective','527',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('550','Materia','material','632',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('551','Gimnasia acrobática','acrobatic gymnastics','272',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('552','Edad Contemporánea','late modern period','528',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('553','Implementación SAAC','aac implementation','423',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('554','Nutrición animal','animal nutrition','414',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('556','Invertebrado','invertebrate','627','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('558','Persona','person','486',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('559','Trabajo','work',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('560','Sector secundario','secondary sector','631',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('561','Comunicación','communication',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('563','Sector primario','primary sector','631',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('568','Cuerpo humano','human body','558',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('570','Sector terciario','tertiary sector','631',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('575','Tiempo','time',NULL,NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('577','Pronombre','pronoun','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('578','Estilo de vida','lifestyle','470',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('585','Puericultura','babycare','624','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('592','Adverbio','adverb','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('600','Sociología','sociology','621',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('601','Clasificación vegetal','plant clasification','401','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('604','Cronológico','chronological','575','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('606','Civilización','civilization','546',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('607','Egipto','Egypt','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('608','Roma','Rome','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('609','Grecia','Greece','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('611','Conjunción','conjunction','474',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('614','Íberos','Iberians','606',NULL);
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('616','Conocimiento','knowledge',NULL,'');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('617','Arte','art','616','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('618','Ciencia','science','616','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('619','Humanidades','humanities','616','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('620','Ciencias naturales','natural sciences','618','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('621','Ciencias sociales','social sciences','618','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('622','Ciencias aplicadas','applied sciences','618','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('623','Ciencias formales','formal sciences','618','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('624','Especialidad médica','medical specialty','498','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('625','Relación humana','human relationship','302','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('626','Grupo de deportes','sports','424','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('627','Esqueleto animal','animal skeleton','414','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('629','Animal según su entorno','animals by their environment','414','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('630','Estaciones del año','seasons','377','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('631','Trabajo por sectores','work by sectors','559','');
INSERT INTO dbo.Categorias (Id, Nombre, NombreOriginal, CategoriaPadre, Nivel) VALUES ('632','Propiedad del objeto','object property','127','');