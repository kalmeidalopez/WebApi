USE [master]
GO
/****** Object:  Database [PruebaDB]    Script Date: 10/6/2022 10:28:06 ******/
CREATE DATABASE [PruebaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS01\MSSQL\DATA\PruebaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS01\MSSQL\DATA\PruebaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PruebaDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaDB] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PruebaDB] SET QUERY_STORE = OFF
GO
USE [PruebaDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PruebaDB]
GO
/****** Object:  Table [dbo].[tbCliente]    Script Date: 10/6/2022 10:28:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCliente](
	[Clienteid] [int] NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
	[Identificacion] [varchar](150) NULL,
	[Contrasenia] [varchar](150) NOT NULL,
	[Edad] [int] NULL,
	[Direccion] [varchar](150) NOT NULL,
	[Telefono] [varchar](150) NULL,
	[Genero] [varchar](150) NULL,
	[Estado] [bit] NOT NULL,
	[FechaIngreso] [datetime] NOT NULL,
 CONSTRAINT [PK_tbCliente] PRIMARY KEY CLUSTERED 
(
	[Clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbCuenta]    Script Date: 10/6/2022 10:28:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCuenta](
	[IdCuenta] [int] NOT NULL,
	[Clienteid] [int] NOT NULL,
	[NumeroCuenta] [varchar](20) NOT NULL,
	[TipoCuenta] [varchar](20) NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
	[FechaIngreso] [datetime] NOT NULL,
 CONSTRAINT [PK_tbCuenta] PRIMARY KEY CLUSTERED 
(
	[IdCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbMovimiento]    Script Date: 10/6/2022 10:28:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbMovimiento](
	[IdMovimiento] [int] NOT NULL,
	[IdCuenta] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[TipoMovimiento] [varchar](20) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tbMovimiento] PRIMARY KEY CLUSTERED 
(
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbCuenta]  WITH CHECK ADD  CONSTRAINT [fkCuentaCliente] FOREIGN KEY([Clienteid])
REFERENCES [dbo].[tbCliente] ([Clienteid])
GO
ALTER TABLE [dbo].[tbCuenta] CHECK CONSTRAINT [fkCuentaCliente]
GO
ALTER TABLE [dbo].[tbMovimiento]  WITH CHECK ADD  CONSTRAINT [fkMovimientoCuenta] FOREIGN KEY([IdCuenta])
REFERENCES [dbo].[tbCuenta] ([IdCuenta])
GO
ALTER TABLE [dbo].[tbMovimiento] CHECK CONSTRAINT [fkMovimientoCuenta]
GO
USE [master]
GO
ALTER DATABASE [PruebaDB] SET  READ_WRITE 
GO
