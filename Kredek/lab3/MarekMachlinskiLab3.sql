USE [master]
GO
/****** Object:  Database [Pizzeria]    Script Date: 19.04.2018 21:13:40 ******/
CREATE DATABASE [Pizzeria]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Pizzeria', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Pizzeria.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Pizzeria_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Pizzeria_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Pizzeria] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pizzeria].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pizzeria] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pizzeria] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pizzeria] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pizzeria] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pizzeria] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pizzeria] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pizzeria] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pizzeria] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pizzeria] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pizzeria] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pizzeria] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pizzeria] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pizzeria] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pizzeria] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pizzeria] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Pizzeria] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pizzeria] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pizzeria] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pizzeria] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pizzeria] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pizzeria] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pizzeria] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pizzeria] SET RECOVERY FULL 
GO
ALTER DATABASE [Pizzeria] SET  MULTI_USER 
GO
ALTER DATABASE [Pizzeria] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pizzeria] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pizzeria] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pizzeria] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Pizzeria] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Pizzeria', N'ON'
GO
ALTER DATABASE [Pizzeria] SET QUERY_STORE = OFF
GO
USE [Pizzeria]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Pizzeria]
GO
/****** Object:  Table [dbo].[Indegriends]    Script Date: 19.04.2018 21:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Indegriends](
	[Id] [int] IDENTITY(10,10) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[PricePerUnit] [decimal](3, 2) NULL,
 CONSTRAINT [PK_Indegriends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pizzas]    Script Date: 19.04.2018 21:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pizzas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[Diameter] [tinyint] NOT NULL,
	[Price] [decimal](6, 2) NOT NULL,
	[Seasonal] [bit] NOT NULL,
 CONSTRAINT [PK_Pizzas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topics]    Script Date: 19.04.2018 21:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PizzaId] [int] NOT NULL,
	[IndegriendId] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Indegriends] ON 

INSERT [dbo].[Indegriends] ([Id], [Name], [PricePerUnit]) VALUES (10, N'Salami', CAST(1.50 AS Decimal(3, 2)))
INSERT [dbo].[Indegriends] ([Id], [Name], [PricePerUnit]) VALUES (20, N'Czosnek', CAST(1.00 AS Decimal(3, 2)))
INSERT [dbo].[Indegriends] ([Id], [Name], [PricePerUnit]) VALUES (30, N'Ananas', CAST(1.00 AS Decimal(3, 2)))
INSERT [dbo].[Indegriends] ([Id], [Name], [PricePerUnit]) VALUES (40, N'Ser', CAST(0.50 AS Decimal(3, 2)))
SET IDENTITY_INSERT [dbo].[Indegriends] OFF
SET IDENTITY_INSERT [dbo].[Pizzas] ON 

INSERT [dbo].[Pizzas] ([Id], [Name], [Diameter], [Price], [Seasonal]) VALUES (9, N'Hawajska', 35, CAST(18.50 AS Decimal(6, 2)), 0)
INSERT [dbo].[Pizzas] ([Id], [Name], [Diameter], [Price], [Seasonal]) VALUES (10, N'Pepperoni', 35, CAST(19.50 AS Decimal(6, 2)), 0)
INSERT [dbo].[Pizzas] ([Id], [Name], [Diameter], [Price], [Seasonal]) VALUES (11, N'Diablo', 66, CAST(66.60 AS Decimal(6, 2)), 1)
INSERT [dbo].[Pizzas] ([Id], [Name], [Diameter], [Price], [Seasonal]) VALUES (12, N'Studencka', 32, CAST(12.00 AS Decimal(6, 2)), 0)
INSERT [dbo].[Pizzas] ([Id], [Name], [Diameter], [Price], [Seasonal]) VALUES (15, N'Państwowa', 34, CAST(15.00 AS Decimal(6, 2)), 0)
SET IDENTITY_INSERT [dbo].[Pizzas] OFF
SET IDENTITY_INSERT [dbo].[Topics] ON 

INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (2, 9, 10)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (3, 9, 20)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (4, 9, 30)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (5, 9, 40)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (6, 10, 10)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (7, 10, 20)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (8, 10, 30)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (9, 10, 40)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (10, 11, 10)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (11, 11, 20)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (12, 11, 30)
INSERT [dbo].[Topics] ([Id], [PizzaId], [IndegriendId]) VALUES (13, 11, 40)
SET IDENTITY_INSERT [dbo].[Topics] OFF
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Indegriends] FOREIGN KEY([IndegriendId])
REFERENCES [dbo].[Indegriends] ([Id])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Indegriends]
GO
ALTER TABLE [dbo].[Topics]  WITH CHECK ADD  CONSTRAINT [FK_Topics_Pizzas] FOREIGN KEY([PizzaId])
REFERENCES [dbo].[Pizzas] ([Id])
GO
ALTER TABLE [dbo].[Topics] CHECK CONSTRAINT [FK_Topics_Pizzas]
GO
USE [master]
GO
ALTER DATABASE [Pizzeria] SET  READ_WRITE 
GO
