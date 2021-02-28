USE [master]
GO
/****** Object:  Database [MagicPortal]    Script Date: 13.05.2018 14:53:57 ******/
CREATE DATABASE [MagicPortal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Players', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Players.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Players_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Players_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MagicPortal] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MagicPortal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MagicPortal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MagicPortal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MagicPortal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MagicPortal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MagicPortal] SET ARITHABORT OFF 
GO
ALTER DATABASE [MagicPortal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MagicPortal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MagicPortal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MagicPortal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MagicPortal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MagicPortal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MagicPortal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MagicPortal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MagicPortal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MagicPortal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MagicPortal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MagicPortal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MagicPortal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MagicPortal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MagicPortal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MagicPortal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MagicPortal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MagicPortal] SET RECOVERY FULL 
GO
ALTER DATABASE [MagicPortal] SET  MULTI_USER 
GO
ALTER DATABASE [MagicPortal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MagicPortal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MagicPortal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MagicPortal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MagicPortal] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MagicPortal', N'ON'
GO
ALTER DATABASE [MagicPortal] SET QUERY_STORE = OFF
GO
USE [MagicPortal]
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
USE [MagicPortal]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 13.05.2018 14:53:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[Name] [nvarchar](31) NOT NULL,
	[EnlistDate] [date] NOT NULL,
	[Title] [nchar](31) NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Admin', CAST(N'2018-05-12' AS Date), NULL)
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'amino', CAST(N'2018-05-12' AS Date), N'grill                          ')
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Bestia', CAST(N'2018-05-01' AS Date), N'Adam                           ')
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Joakim', CAST(N'2017-12-01' AS Date), N'War Machine                    ')
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'JSOS', CAST(N'2014-01-19' AS Date), N'Unbreakable                    ')
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Kerrigan', CAST(N'2018-05-12' AS Date), NULL)
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Lemmy', CAST(N'2018-05-12' AS Date), NULL)
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Optimus', CAST(N'2018-05-12' AS Date), N'Prime                          ')
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Sigmar', CAST(N'2018-05-12' AS Date), NULL)
INSERT [dbo].[Players] ([Name], [EnlistDate], [Title]) VALUES (N'Tapczan', CAST(N'2018-05-12' AS Date), N'Tekman                         ')
USE [master]
GO
ALTER DATABASE [MagicPortal] SET  READ_WRITE 
GO
