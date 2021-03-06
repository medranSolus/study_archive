USE [master]
GO
/****** Object:  Database [Organiser]    Script Date: 23.04.2018 17:37:22 ******/
CREATE DATABASE [Organiser]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Processes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Processes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Processes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MEDRANSQL\MSSQL\DATA\Processes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Organiser] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Organiser].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Organiser] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Organiser] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Organiser] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Organiser] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Organiser] SET ARITHABORT OFF 
GO
ALTER DATABASE [Organiser] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Organiser] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Organiser] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Organiser] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Organiser] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Organiser] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Organiser] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Organiser] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Organiser] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Organiser] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Organiser] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Organiser] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Organiser] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Organiser] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Organiser] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Organiser] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Organiser] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Organiser] SET RECOVERY FULL 
GO
ALTER DATABASE [Organiser] SET  MULTI_USER 
GO
ALTER DATABASE [Organiser] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Organiser] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Organiser] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Organiser] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Organiser] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Organiser', N'ON'
GO
ALTER DATABASE [Organiser] SET QUERY_STORE = OFF
GO
USE [Organiser]
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
USE [Organiser]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[LastTimeActive] [bigint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UsersView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UsersView] AS
SELECT Name, LastTimeActive FROM Users
GO
/****** Object:  Table [dbo].[Works]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Works](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProcessName] [nvarchar](31) NOT NULL,
	[TaskToDoId] [int] NOT NULL,
 CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksToDo]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksToDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[Deadline] [date] NULL,
 CONSTRAINT [PK_TasksToDo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Processes]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Processes](
	[Name] [nvarchar](31) NOT NULL,
	[LastOpen] [datetime] NOT NULL,
	[LastClosed] [datetime] NULL,
	[AverageTimeUsed] [bigint] NULL,
 CONSTRAINT [PK_Processes_1] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[WorkView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[WorkView](ProcessName, Task) AS
SELECT Processes.Name, TasksToDo.Name FROM Works LEFT JOIN Processes ON Works.ProcessName=Processes.Name LEFT JOIN TasksToDo ON Works.TaskToDoId=TasksToDo.Id
GO
/****** Object:  Table [dbo].[CurrentWorks]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentWorks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActiveTaskId] [int] NOT NULL,
	[ProcessName] [nvarchar](31) NOT NULL,
 CONSTRAINT [PK_CurrentWorks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CurrentWorksView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CurrentWorksView] (Process, Task) As
SELECT ProcessName, TasksToDo.Name FROM CurrentWorks LEFT JOIN TasksToDo ON ActiveTaskId=TasksToDo.Id
GO
/****** Object:  Table [dbo].[LifeGoals]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LifeGoals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[Details] [nvarchar](255) NULL,
	[Completed] [bit] NOT NULL,
 CONSTRAINT [PK_LifeGoals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GoalsView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GoalsView] AS SELECT Name, Details FROM LifeGoals WHERE Completed=0
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](31) NOT NULL,
	[Born] [date] NULL,
	[PhoneNumber] [nchar](9) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ContactsView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ContactsView] AS
SELECT Name, Born, PhoneNumber FROM Contacts
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](63) NOT NULL,
	[Location] [nvarchar](63) NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MeetingsView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MeetingsView] AS
SELECT Name, Location, Date, Time FROM Meetings
GO
/****** Object:  Table [dbo].[Events]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeetingId] [int] NOT NULL,
	[ContactId] [int] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[EventsView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EventsView] (MeetingName, ContactName) AS 
SELECT Meetings.Name, Contacts.Name FROM Events LEFT JOIN Meetings ON Events.MeetingId=Meetings.Id LEFT JOIN Contacts ON Events.ContactId=Contacts.Id
GO
/****** Object:  View [dbo].[TasksView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TasksView] AS
SELECT Name, Deadline FROM TasksToDo
GO
/****** Object:  View [dbo].[ProcessesView]    Script Date: 23.04.2018 17:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProcessesView] AS
SELECT * FROM Processes
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (1, N'Antek', CAST(N'1982-06-12' AS Date), N'889239321')
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (2, N'Gandalf', CAST(N'0003-04-04' AS Date), NULL)
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (3, N'Geralt', CAST(N'1268-04-05' AS Date), NULL)
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (4, N'Gniewomir', CAST(N'1601-08-12' AS Date), NULL)
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (5, N'Kasia', CAST(N'1990-01-01' AS Date), N'998998998')
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (6, N'Vincent Damon Furnier', CAST(N'1948-02-04' AS Date), N'123123123')
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (7, N'Władek', CAST(N'1972-12-01' AS Date), N'881881999')
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (8, N'Sandy', CAST(N'2018-04-03' AS Date), N'231231232')
INSERT [dbo].[Contacts] ([Id], [Name], [Born], [PhoneNumber]) VALUES (9, N'Maja', CAST(N'1999-02-19' AS Date), N'993434334')
SET IDENTITY_INSERT [dbo].[Contacts] OFF
SET IDENTITY_INSERT [dbo].[Events] ON 

INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (1, 1, 1)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (2, 1, 2)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (3, 1, 3)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (4, 5, 7)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (5, 8, 5)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (6, 8, 6)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (8, 8, 7)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (9, 7, 4)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (10, 6, 7)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (11, 6, 4)
INSERT [dbo].[Events] ([Id], [MeetingId], [ContactId]) VALUES (12, 9, 6)
SET IDENTITY_INSERT [dbo].[Events] OFF
SET IDENTITY_INSERT [dbo].[LifeGoals] ON 

INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (1, N'Posadzić drzewo', NULL, 0)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (2, N'Zbudować dom', N'Wybudować dom w cichym zakątku świata', 0)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (3, N'Zniszczyć Pierścień', N'Zanieść Jedyny Pierścień do Orodruiny', 1)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (4, N'Zaliczyć AK1', N'Assembler nie będzie taki trudny... prawda?', 0)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (5, N'Podbić Temerię', N'Tym razem Menno Coehoorn', 1)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (6, N'Wyplenić chaos', N'ZA IMPERATORA!', 1)
INSERT [dbo].[LifeGoals] ([Id], [Name], [Details], [Completed]) VALUES (7, N'Zamienić się w kransoluda', N'Jak powyżej!', 0)
SET IDENTITY_INSERT [dbo].[LifeGoals] OFF
SET IDENTITY_INSERT [dbo].[Meetings] ON 

INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (1, N'To ważne i bez sensu (praca)', N'Biuro', CAST(N'2018-08-04' AS Date), NULL)
INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (5, N'To mniej ważne i jeszcze bardziej bez sensu', N'Biuro', CAST(N'2018-05-17' AS Date), CAST(N'13:20:00' AS Time))
INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (6, N'Koncert MANOWAR', N'Katowice', CAST(N'2014-05-17' AS Date), CAST(N'18:30:00' AS Time))
INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (7, N'Pyrkon', N'Poznań', CAST(N'2018-05-18' AS Date), NULL)
INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (8, N'Wyplenianie chaosu', N'Eye of Terror', CAST(N'2018-05-13' AS Date), NULL)
INSERT [dbo].[Meetings] ([Id], [Name], [Location], [Date], [Time]) VALUES (9, N'Premiera Solo', N'Wrocław', CAST(N'2018-05-25' AS Date), CAST(N'19:30:23' AS Time))
SET IDENTITY_INSERT [dbo].[Meetings] OFF
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'AIMP.exe', CAST(N'2018-04-23T15:00:40.650' AS DateTime), CAST(N'2018-04-23T15:01:10.713' AS DateTime), 178196796)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'audiodg.exe', CAST(N'2018-04-22T02:20:31.520' AS DateTime), CAST(N'2018-04-23T01:39:13.117' AS DateTime), 419607983335)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'backgroundTaskHost.exe', CAST(N'2018-04-23T15:00:44.583' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'CheckHyperVHost.exe', CAST(N'2018-04-22T18:43:07.150' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'chrome.exe', CAST(N'2018-04-22T19:35:00.073' AS DateTime), CAST(N'2018-04-22T02:20:53.063' AS DateTime), 344595613)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'cmd.exe', CAST(N'2018-04-23T01:27:30.860' AS DateTime), CAST(N'2018-04-23T01:27:31.863' AS DateTime), 5243326)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'CompatTelRunner.exe', CAST(N'2018-04-21T23:42:14.990' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'conhost.exe', CAST(N'2018-04-23T01:27:30.940' AS DateTime), CAST(N'2018-04-23T01:27:31.867' AS DateTime), 164553592343)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'consent.exe', CAST(N'2018-04-21T23:39:28.110' AS DateTime), CAST(N'2018-04-21T23:39:28.113' AS DateTime), 15614)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'devenv.exe', CAST(N'2018-04-22T18:43:49.863' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'dllhost.exe', CAST(N'2018-04-23T15:00:39.597' AS DateTime), CAST(N'2018-04-23T15:00:44.583' AS DateTime), 256745437600)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'git.exe', CAST(N'2018-04-22T01:38:09.870' AS DateTime), CAST(N'2018-04-22T01:38:09.857' AS DateTime), 5033138573)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'LaunchTM.exe', CAST(N'2018-04-21T23:39:28.010' AS DateTime), CAST(N'2018-04-21T23:39:28.113' AS DateTime), 518123)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'MarekMachlinskiLab3Zad1.exe', CAST(N'2018-04-22T19:40:56.853' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'NvTmMon.exe', CAST(N'2018-04-22T01:55:17.953' AS DateTime), CAST(N'2018-04-22T01:55:17.953' AS DateTime), 2499595)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'powercfg.exe', CAST(N'2018-04-23T01:27:30.973' AS DateTime), CAST(N'2018-04-23T01:27:30.860' AS DateTime), -562950)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'remsh.exe', CAST(N'2018-04-22T19:29:05.283' AS DateTime), CAST(N'2018-04-23T01:32:32.397' AS DateTime), 109037995555)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'ScriptedSandbox64.exe', CAST(N'2018-04-23T14:59:30.777' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'SearchFilterHost.exe', CAST(N'2018-04-22T01:52:46.110' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'SearchProtocolHost.exe', CAST(N'2018-04-22T01:52:46.107' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'ServiceHub.Host.CLR.x86.exe', CAST(N'2018-04-23T14:59:30.583' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'smartscreen.exe', CAST(N'2018-04-23T15:00:40.577' AS DateTime), NULL, 0)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'svchost.exe', CAST(N'2018-04-23T15:00:38.573' AS DateTime), CAST(N'2018-04-23T15:01:08.713' AS DateTime), 207894405799)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'Taskmgr.exe', CAST(N'2018-04-21T23:39:28.110' AS DateTime), CAST(N'2018-04-21T23:39:48.997' AS DateTime), 542868714)
INSERT [dbo].[Processes] ([Name], [LastOpen], [LastClosed], [AverageTimeUsed]) VALUES (N'WmiPrvSE.exe', CAST(N'2018-04-22T19:29:27.267' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[TasksToDo] ON 

INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (1, N'Oddać projekt na kredka', CAST(N'2018-04-25' AS Date))
INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (2, N'Zdać semestr', CAST(N'2018-06-29' AS Date))
INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (3, N'Zdać studia', NULL)
INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (4, N'Pójść na słodową', NULL)
INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (5, N'Posprzątać', NULL)
INSERT [dbo].[TasksToDo] ([Id], [Name], [Deadline]) VALUES (6, N'Wyprowadzić Zerglinga', CAST(N'2018-05-06' AS Date))
SET IDENTITY_INSERT [dbo].[TasksToDo] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [LastTimeActive]) VALUES (1, N'marek', 67914523)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Works] ON 

INSERT [dbo].[Works] ([Id], [ProcessName], [TaskToDoId]) VALUES (1, N'AIMP.exe', 5)
SET IDENTITY_INSERT [dbo].[Works] OFF
ALTER TABLE [dbo].[CurrentWorks]  WITH CHECK ADD  CONSTRAINT [FK_CurrentWorks_Processes] FOREIGN KEY([ProcessName])
REFERENCES [dbo].[Processes] ([Name])
GO
ALTER TABLE [dbo].[CurrentWorks] CHECK CONSTRAINT [FK_CurrentWorks_Processes]
GO
ALTER TABLE [dbo].[CurrentWorks]  WITH CHECK ADD  CONSTRAINT [FK_CurrentWorks_TasksToDo] FOREIGN KEY([Id])
REFERENCES [dbo].[TasksToDo] ([Id])
GO
ALTER TABLE [dbo].[CurrentWorks] CHECK CONSTRAINT [FK_CurrentWorks_TasksToDo]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Contacts] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Contacts]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Meetings] FOREIGN KEY([MeetingId])
REFERENCES [dbo].[Meetings] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Meetings]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_Processes] FOREIGN KEY([ProcessName])
REFERENCES [dbo].[Processes] ([Name])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_Processes]
GO
ALTER TABLE [dbo].[Works]  WITH CHECK ADD  CONSTRAINT [FK_Works_TasksToDo] FOREIGN KEY([TaskToDoId])
REFERENCES [dbo].[TasksToDo] ([Id])
GO
ALTER TABLE [dbo].[Works] CHECK CONSTRAINT [FK_Works_TasksToDo]
GO
USE [master]
GO
ALTER DATABASE [Organiser] SET  READ_WRITE 
GO
