USE [master]
GO
/****** Object:  Database [BiddingDb2]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE DATABASE [BiddingDb2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BiddingDb2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\BiddingDb2.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BiddingDb2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\BiddingDb2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BiddingDb2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BiddingDb2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BiddingDb2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BiddingDb2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BiddingDb2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BiddingDb2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BiddingDb2] SET ARITHABORT OFF 
GO
ALTER DATABASE [BiddingDb2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BiddingDb2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BiddingDb2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BiddingDb2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BiddingDb2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BiddingDb2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BiddingDb2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BiddingDb2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BiddingDb2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BiddingDb2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BiddingDb2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BiddingDb2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BiddingDb2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BiddingDb2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BiddingDb2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BiddingDb2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BiddingDb2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BiddingDb2] SET RECOVERY FULL 
GO
ALTER DATABASE [BiddingDb2] SET  MULTI_USER 
GO
ALTER DATABASE [BiddingDb2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BiddingDb2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BiddingDb2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BiddingDb2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BiddingDb2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BiddingDb2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BiddingDb2', N'ON'
GO
ALTER DATABASE [BiddingDb2] SET QUERY_STORE = ON
GO
ALTER DATABASE [BiddingDb2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BiddingDb2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Biddings]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Biddings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCurrentBidding] [decimal](18, 2) NOT NULL,
	[IsWinner] [bit] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BiddingSessionId] [uniqueidentifier] NOT NULL,
	[BiddingAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Biddings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BiddingSessions]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BiddingSessions](
	[Id] [uniqueidentifier] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[TotalBiddingCount] [int] NOT NULL,
	[HighestBidding] [decimal](18, 2) NOT NULL,
	[IsClosed] [bit] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MinimumJumpingValue] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BiddingSessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [int] NOT NULL,
	[Budget] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Brands] [int] NOT NULL,
	[VIN] [nvarchar](450) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Color] [nvarchar](30) NOT NULL,
	[ImageUrl] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
	[Horsepower] [int] NOT NULL,
	[MaximumSpeed] [decimal](18, 2) NOT NULL,
	[NumberOfChairs] [int] NOT NULL,
	[TrunkCapacity] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227030011_initMigration', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227042137_updatedb', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241229092819_updateDb2', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241229111829_updateDb3', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250102014319_updateDbV3', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250107085327_updateBiddingDate', N'8.0.1')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250107093100_updateCreatedDateSession', N'8.0.1')
GO
SET IDENTITY_INSERT [dbo].[Biddings] ON 

INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (36, CAST(520.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'9a703da1-83d8-4881-93fd-fd066e79a9d9', CAST(N'2025-01-07T16:16:35.6466667' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (37, CAST(540.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'9a703da1-83d8-4881-93fd-fd066e79a9d9', CAST(N'2025-01-07T16:17:05.6133333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (38, CAST(560.00 AS Decimal(18, 2)), 1, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'9a703da1-83d8-4881-93fd-fd066e79a9d9', CAST(N'2025-01-07T16:17:20.8533333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (39, CAST(320.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:19:57.3833333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (40, CAST(340.00 AS Decimal(18, 2)), 0, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:20:31.8933333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (41, CAST(360.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:20:40.2466667' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (42, CAST(380.00 AS Decimal(18, 2)), 0, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:21:04.1900000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (43, CAST(400.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:21:34.8666667' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (44, CAST(570.00 AS Decimal(18, 2)), 1, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'a60c3e57-388a-437a-a07a-3a7c78d4f645', CAST(N'2025-01-07T16:22:27.6266667' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (45, CAST(420.00 AS Decimal(18, 2)), 1, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T16:25:10.5133333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (46, CAST(520.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-09T10:46:18.2800000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (47, CAST(540.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-09T10:46:36.4266667' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (48, CAST(560.00 AS Decimal(18, 2)), 0, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-09T10:46:51.2400000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (49, CAST(580.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-09T10:47:01.7400000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (50, CAST(600.00 AS Decimal(18, 2)), 1, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-09T10:47:14.6433333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (51, CAST(470.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-09T10:49:32.2600000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (52, CAST(500.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-09T10:49:42.9933333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (53, CAST(520.00 AS Decimal(18, 2)), 0, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-09T10:50:07.2033333' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (54, CAST(540.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-09T10:50:18.7200000' AS DateTime2))
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId], [BiddingAt]) VALUES (55, CAST(560.00 AS Decimal(18, 2)), 1, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-09T10:50:31.0533333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Biddings] OFF
GO
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'edad6182-2a84-444a-898e-1011a1e8c750', CAST(N'2025-01-08T03:20:58.2433333' AS DateTime2), CAST(N'2025-02-01T03:20:58.2433333' AS DateTime2), 0, CAST(550.00 AS Decimal(18, 2)), 0, 29, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-08T10:21:35.9233333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'f459a079-7e23-40fa-bf46-29a65a5c9b35', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-01-15T09:11:50.4833333' AS DateTime2), 6, CAST(420.00 AS Decimal(18, 2)), 0, 11, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'5eae9dec-97a4-44ed-b7b2-2d6e404a5822', CAST(N'2025-01-01T09:09:37.8333333' AS DateTime2), CAST(N'2025-01-05T09:09:37.8333333' AS DateTime2), 0, CAST(500.00 AS Decimal(18, 2)), 1, 7, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-01T09:09:37.8333333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'232db194-17c9-45f8-b8b3-319b6a59a618', CAST(N'2025-01-07T09:40:31.6500000' AS DateTime2), CAST(N'2025-02-08T09:40:31.6500000' AS DateTime2), 5, CAST(600.00 AS Decimal(18, 2)), 0, 27, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T16:43:49.3233333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'a60c3e57-388a-437a-a07a-3a7c78d4f645', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-02-01T09:11:50.4833333' AS DateTime2), 1, CAST(570.00 AS Decimal(18, 2)), 0, 9, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-06T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'd528b09d-f7d5-4876-a01a-4534aa450556', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-03-01T09:11:50.4833333' AS DateTime2), 0, CAST(600.00 AS Decimal(18, 2)), 0, 10, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'7365317b-e300-4d89-9983-5bd4554c3805', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-01-21T09:11:50.4833333' AS DateTime2), 0, CAST(350.00 AS Decimal(18, 2)), 0, 13, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:08:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'3306cdba-b6a8-44e3-999a-ad80fa4b7628', CAST(N'2025-01-06T22:11:50.4833333' AS DateTime2), CAST(N'2025-02-06T09:11:50.4833333' AS DateTime2), 0, CAST(550.00 AS Decimal(18, 2)), 0, 23, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'c7dff076-a278-4eef-a528-d91f9d5d5a64', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-01-22T09:11:50.4833333' AS DateTime2), 0, CAST(450.00 AS Decimal(18, 2)), 0, 14, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:07:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'ed96eac6-c6a2-4495-92b7-dc0f532c4c9b', CAST(N'2025-01-08T03:20:58.2433333' AS DateTime2), CAST(N'2025-02-08T03:20:58.2433333' AS DateTime2), 5, CAST(560.00 AS Decimal(18, 2)), 0, 30, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-08T10:21:20.8800000' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'36e4ee4d-7779-471d-bb8d-dd65d43d6ef3', CAST(N'2025-01-05T09:11:50.4833333' AS DateTime2), CAST(N'2025-01-06T09:12:50.4833333' AS DateTime2), 0, CAST(550.00 AS Decimal(18, 2)), 1, 23, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-05T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'9a703da1-83d8-4881-93fd-fd066e79a9d9', CAST(N'2025-01-06T09:11:50.4833333' AS DateTime2), CAST(N'2025-01-07T16:18:30.4833333' AS DateTime2), 3, CAST(560.00 AS Decimal(18, 2)), 1, 7, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue], [CreateDate]) VALUES (N'f8114fa1-f723-441f-9d69-fdc726ebf73c', CAST(N'2025-01-07T09:11:50.4833333' AS DateTime2), CAST(N'2025-04-22T09:11:50.4833333' AS DateTime2), 0, CAST(700.00 AS Decimal(18, 2)), 0, 15, 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2025-01-07T09:05:50.4833333' AS DateTime2))
GO
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'thanhne', N'thanhne@gmail.com', N'AQAAAAIAAYagAAAAEL1ymcm6vGu6RcuTBzMWubVCYGBPpo+8VbktxrjSPsVRMr0y5cejW8u7JUuAs4QRVQ==', 1, CAST(1500.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'f4371dca-ea1b-4d34-8551-2f8d09879279', N'admin', N'admin@admin.com', N'AQAAAAIAAYagAAAAEPt4vT9qay6Kf+ZBZ8ObUjiLe0aDJ3ETIv1gzLPj8gRcCm89YimPnubl7lVvno/WvA==', 0, CAST(99999.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'0b811cef-471b-470f-95a0-7b8201e18de5', N'mla', N'mla@gmail.com', N'AQAAAAIAAYagAAAAEGu711Cf5Apj9jTdHxjt2qqXORRvX6U7GtKqh0MTecgI7L9KhNvMZZ4xcJoxIqaeDQ==', 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'ecec4cdb-ff19-4bc6-927e-7d9cc4c0bd35', N'string123', N'user123@example.com', N'AQAAAAIAAYagAAAAEGsoF8kJQ7MWjfLFvKOZ5PPl/xXv+0TvEooCcRKy3fzuEGmdi3BtbmSQqutIyxdyTA==', 1, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'4d68e250-a68f-4fa7-b517-b8edf130906d', N'user example', N'user@example.com', N'AQAAAAIAAYagAAAAEO/ZcsecGhq6hTQMm2VRc7e2rVIArdVOFR9Z/GSqbEEokQGY4y/YyDvrJjJEgJ065Q==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'd52bf210-336f-4959-b03f-b9f450b526d2', N'test3', N'test3@gmail.com', N'AQAAAAIAAYagAAAAEGK/0h3pYL4Gj2j0xpdXmfNlgf5R2iTTkB/niphVzzENWzPE0KvB3q3TJJcd7l5EVw==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'tina', N'tina@gmail.com', N'AQAAAAIAAYagAAAAELLXQwvwnXcV0oFcohCA4qTinpzvvcHNGOOcbc77yvsaOwz1ShJc4f9pIv+Y0HHXfg==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'c86904c2-fdac-44fa-90da-e86a025165d2', N'test2@gmail.com', N'test2@gmail.com', N'AQAAAAIAAYagAAAAEN+dkPHG5ghVKG8j5x1QtDb5luIU17nAWJY7ud+MS33TM+WuPsopvJG+eC6mUCA4kQ==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'474226ab-a5fb-49ec-91cc-f800a15bb0e1', N'test', N'test@gmail.com', N'AQAAAAIAAYagAAAAEOe7fuHCGd2jZ20KgCG1myCcc3u+Qmaqy/4hw4RTWS1sbOfmvfq5ubKn5RxvdMKE2Q==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'cdf40bd9-a4ad-43a5-9c6d-fc3a8670894a', N'test4', N'test4@gmail.com', N'AQAAAAIAAYagAAAAEOW0tTfVS2dTwYJNWMD8zMMf6AFdYYAILzRIKecOzSmNgj7WtXJOX35r/TGh7UPR1A==', 1, CAST(1000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (7, N'BMW 3-Series 2020', N'BMW 3-Series 2020', 0, N'BMW1BN1H2312ADAW2', CAST(500.00 AS Decimal(18, 2)), N'white', N'https://autopro8.mediacdn.vn/2020/4/22/bmw-3-series-cac-phien-ban-16-1587532639946455821534.jpg', 2, 150, CAST(200.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (9, N'New 2024 BMW i7', N'New 2024 BMW i7 M70 4dr Car in San Francisco', 0, N'BMW123A12B31JK23B12', CAST(400.00 AS Decimal(18, 2)), N'white', N'https://vehicle-images.dealerinspire.com/ac01-18003201/WBY53EJ05RCR48672/f6c9f2f1c0d9f346d675850a0c1c5c74.jpg', 1, 160, CAST(210.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (10, N'Rolls-Royce Phantom', N'Luxury car', 1, N'RR12BZ1B21M12VK12B3', CAST(600.00 AS Decimal(18, 2)), N'red', N'https://images.dealer.com/ddc/vehicles/2024/Rolls-Royce/Phantom/Sedan/color/Wildberry-WR14-78,23,23-320-en_US.jpg', 1, 170, CAST(220.00 AS Decimal(18, 2)), 7, CAST(130.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (11, N'Nissan Patrol 2025', N'Nissan Patrol 2025', 2, N'NIDWUB123V2V312HVA', CAST(300.00 AS Decimal(18, 2)), N'white', N'https://dailymuabanxe.net/wp-content/uploads/2024/09/Nissan-Patrol-2.jpg', 1, 170, CAST(210.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (13, N'Nissan X-Terra 2024', N'Nissan X-Terra 2024', 2, N'NI12HJ122KHAWD2DQ1', CAST(350.00 AS Decimal(18, 2)), N'red', N'https://nissanclub.com.vn/uploads/libraries/kcfinder/upload/images/nissan-terra-2021-red3.jpg', 1, 180, CAST(230.00 AS Decimal(18, 2)), 4, CAST(140.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (14, N'Honda CRV 2023', N'Honda CRV 2023', 3, N'HD12KVBJ123VJ12V3JAZ', CAST(450.00 AS Decimal(18, 2)), N'white', N'https://hondaotobinhdinh.com.vn/wp-content/uploads/2023/10/576995-un-honda-cr-v-hybride-moins-cher-s-ajoute-a-la-gamme.jpg', 1, 140, CAST(200.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (15, N'Rolls Royce Ghost EWB 2021', N'Rolls Royce Ghost EWB 2021', 1, N'RR12BZ1B21M12VA21FA', CAST(700.00 AS Decimal(18, 2)), N'black', N'https://sontungauto.vn/wp-content/uploads/2022/11/Rolls-Royce-Ghost-EWB-2021-1.jpg', 1, 180, CAST(210.00 AS Decimal(18, 2)), 4, CAST(130.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (22, N'2021 BMW 2 SERIES 4D SEDAN 228I GC', N'2021 BMW 2 SERIES 4D SEDAN 228I GC', 0, N'BMW53AK02M7H63893', CAST(450.00 AS Decimal(18, 2)), N'black', N'https://rmsusimg1.blob.core.windows.net/bmw/img/7142DDB03FEE305EA7CF18E5BA7BD1BD_Beautyshot.jpg', 3, 180, CAST(280.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (23, N'2021 BMW 2 SERIES X', N'2021 BMW 2 SERIES 4D SEDAN 228I GC', 0, N'WBA73AK07M7G38686', CAST(550.00 AS Decimal(18, 2)), N'white', N'https://rmsusimg1.blob.core.windows.net/bmw/img/CFF3B19EAF77D534428340A65A046AA3_Beautyshot.jpg', 1, 180, CAST(270.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (24, N'2021 BMW 3 SERIES 4D SEDAN 330I ', N'2021 BMW 3 SERIES 4D SEDAN 330I XDRIVE MSPORT', 0, N'BMW5R7J04M8B78733', CAST(600.00 AS Decimal(18, 2)), N'blue', N'https://rmsusimg1.blob.core.windows.net/bmw/img/229E463A31403999A67FA4A0C663EAA0_Beautyshot.jpg', 0, 190, CAST(280.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (25, N'BMW 5 SERIES 4D SEDAN 540I MSPORT', N'2021 BMW 5 SERIES 4D SEDAN 540I MSPORT', 0, N'BMW53BJ04MCF92210', CAST(500.00 AS Decimal(18, 2)), N'white', N'https://rmsusimg1.blob.core.windows.net/bmw/img/18184CB1E589965F6AF305E30D9B3960_Beautyshot.jpg', 0, 190, CAST(280.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (26, N'2021 BMW 7 SERIES 4D SEDAN 740I', N'2021 BMW 7 SERIES 4D SEDAN 740I', 0, N'BMW7T2C08MCF73570', CAST(500.00 AS Decimal(18, 2)), N'silver', N'https://rmsusimg1.blob.core.windows.net/bmw/img/CD10358D90BE28A89DD32BF383C3A10A_Beautyshot.jpg', 0, 190, CAST(280.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (27, N'2021 BMW 7 SERIES 4DXDRIVE MSPORT', N'2021 BMW 7 SERIES 4D SEDAN 750I XDRIVE MSPORT', 0, N'BMW7U2C0XMCF54192', CAST(500.00 AS Decimal(18, 2)), N'black', N'https://rmsusimg1.blob.core.windows.net/bmw/img/EA50D1BD148480530022382AE4E37829_Beautyshot.jpg', 1, 190, CAST(280.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (29, N'2021 BMW 3 SERIES 4D SEDAN 330I', N'2021 BMW 3 SERIES 4D SEDAN 330I', 0, N'3MW5R1J0XM8B86767', CAST(550.00 AS Decimal(18, 2)), N'beige', N'https://rmsusimg1.blob.core.windows.net/bmw/img/4E5F3CA3F94D5D8C06F46B8233F8F3AD_Beautyshot.jpg', 1, 180, CAST(220.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[Vehicles] ([Id], [Name], [Description], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status], [Horsepower], [MaximumSpeed], [NumberOfChairs], [TrunkCapacity]) VALUES (30, N'2024 BMW 5 Series Sedan 530i', N'2024 BMW 5 Series Sedan 530i', 0, N'WBA43FJ02RC787570', CAST(450.00 AS Decimal(18, 2)), N'white', N'https://rmsusnpimg1.blob.core.windows.net/bmw/staging/images/CE3B5C706EA1BC60C321033EF74F3F98_Beautyshot.jpg', 1, 160, CAST(190.00 AS Decimal(18, 2)), 4, CAST(120.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
/****** Object:  Index [IX_Biddings_BiddingSessionId]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE NONCLUSTERED INDEX [IX_Biddings_BiddingSessionId] ON [dbo].[Biddings]
(
	[BiddingSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Biddings_UserId]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE NONCLUSTERED INDEX [IX_Biddings_UserId] ON [dbo].[Biddings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BiddingSessions_VehicleId]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE NONCLUSTERED INDEX [IX_BiddingSessions_VehicleId] ON [dbo].[BiddingSessions]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Username]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles_VIN]    Script Date: 9/1/2025 2:56:37 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Vehicles_VIN] ON [dbo].[Vehicles]
(
	[VIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Biddings] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [BiddingAt]
GO
ALTER TABLE [dbo].[BiddingSessions] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[BiddingSessions] ADD  DEFAULT ((0.0)) FOR [MinimumJumpingValue]
GO
ALTER TABLE [dbo].[BiddingSessions] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateDate]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT ((0)) FOR [Horsepower]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT ((0.0)) FOR [MaximumSpeed]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT ((0)) FOR [NumberOfChairs]
GO
ALTER TABLE [dbo].[Vehicles] ADD  DEFAULT ((0.0)) FOR [TrunkCapacity]
GO
ALTER TABLE [dbo].[Biddings]  WITH CHECK ADD  CONSTRAINT [FK_Biddings_BiddingSessions_BiddingSessionId] FOREIGN KEY([BiddingSessionId])
REFERENCES [dbo].[BiddingSessions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Biddings] CHECK CONSTRAINT [FK_Biddings_BiddingSessions_BiddingSessionId]
GO
ALTER TABLE [dbo].[Biddings]  WITH CHECK ADD  CONSTRAINT [FK_Biddings_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Biddings] CHECK CONSTRAINT [FK_Biddings_Users_UserId]
GO
ALTER TABLE [dbo].[BiddingSessions]  WITH CHECK ADD  CONSTRAINT [FK_BiddingSessions_Vehicles_VehicleId] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BiddingSessions] CHECK CONSTRAINT [FK_BiddingSessions_Vehicles_VehicleId]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AuthenticateUser]
    @Email NVARCHAR(256),
    @Password NVARCHAR(MAX),
    @UserId UNIQUEIDENTIFIER OUTPUT,
    @UserName NVARCHAR(256) OUTPUT,
    @EmailOut NVARCHAR(256) OUTPUT,
    @HashedPassword NVARCHAR(MAX) OUTPUT,
    @Role NVARCHAR(20) OUTPUT,
    @Budget DECIMAL(18, 2) OUTPUT,
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Retrieve user information
    SELECT 
        @HashedPassword = Password,
        @UserId = Id,
        @UserName = Username,
        @EmailOut = Email,
        @Role = Role,
        @Budget = Budget
    FROM Users
    WHERE Email = @Email;

    -- Check if user exists
    IF @HashedPassword IS NULL
    BEGIN
        SET @Result = -1; -- User not found
        RETURN;
    END

    -- Check if the provided password matches the stored hashed password
    IF @Password <> @HashedPassword
    BEGIN
        SET @Result = 0; -- Invalid password
        RETURN;
    END

    -- Authentication successful
    SET @Result = 1; -- Success
END

GO
/****** Object:  StoredProcedure [dbo].[AutoCloseExpiredSessions]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AutoCloseExpiredSessions]
AS
BEGIN
    SET NOCOUNT ON;

    -- Close sessions where EndTime has passed and IsClosed is still 0
    UPDATE BiddingSessions
    SET IsClosed = 1
    WHERE EndTime <= GETDATE() AND IsClosed = 0;

   
END;

GO
/****** Object:  StoredProcedure [dbo].[CheckUserBiddingStatus]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckUserBiddingStatus]
    @SessionId UNIQUEIDENTIFIER,
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP(1) * FROM Biddings
    WHERE BiddingSessionId = @SessionId AND UserId = @UserId
	ORDER BY BiddingAt desc
END;
GO
/****** Object:  StoredProcedure [dbo].[CloseBiddingSession]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CloseBiddingSession]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BiddingSessions
    SET IsClosed = 1
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[CreateBidding]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateBidding]
    @UserCurrentBidding DECIMAL(18, 2),
    @UserId UNIQUEIDENTIFIER,
    @BiddingSessionId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Biddings (UserCurrentBidding, IsWinner, UserId, BiddingSessionId, BiddingAt)
    VALUES (@UserCurrentBidding, 1, @UserId, @BiddingSessionId, GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[CreateBiddingSession]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateBiddingSession]
   
	@StartTime DATETIME2(7),
	@EndTime DATETIME2(7),
	@OpeningValue DECIMAL(18,2),
	@MinimumJumpingValue DECIMAL(18,2),
    @VehicleId INT
AS
BEGIN
    SET NOCOUNT ON;
 
    INSERT INTO BiddingSessions (Id, [StartTime], [EndTime],[TotalBiddingCount], [HighestBidding], [MinimumJumpingValue], [IsClosed], IsActive, VehicleId, CreateDate)
    VALUES (NEWID(), @StartTime, @EndTime, 0, @OpeningValue, @MinimumJumpingValue, 0, 1, @VehicleId,  GETDATE());
END;
GO
/****** Object:  StoredProcedure [dbo].[CreateVehicle]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateVehicle]
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Brand INT,
    @VIN NVARCHAR(50),
    @Price DECIMAL(18, 2),
    @Color NVARCHAR(50),
    @ImageUrl NVARCHAR(255),
	@NumberOfChairs INT,
	@Horsepower INT,
	@MaximumSpeed DECIMAL(18,2),
	@TrunkCapacity DECIMAL (10,2),
    @Result NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the VIN already exists
    IF EXISTS (SELECT 1 FROM Vehicles WHERE VIN = @VIN)
    BEGIN
        -- Return NULL if VIN already exists and raise error
        SET @Result = NULL;
        RETURN;
    END

    -- Insert the new vehicle record
    INSERT INTO Vehicles (Name, Description, Brands, VIN, Price, Color, ImageUrl, NumberOfChairs, Horsepower, MaximumSpeed, TrunkCapacity, Status)
    VALUES (@Name, @Description, @Brand, @VIN, @Price, @Color, @ImageUrl, @NumberOfChairs, @Horsepower, @MaximumSpeed, @TrunkCapacity, 0);

    -- Return the VIN of the newly created vehicle
    SET @Result = @VIN; 
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteVehicle]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteVehicle]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete the vehicle
    DELETE FROM Vehicles WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[DisableBiddingSession]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DisableBiddingSession]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BiddingSessions
    SET IsActive = 0
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[FetchBiddingValue]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FetchBiddingValue]
    @SessionId UNIQUEIDENTIFIER,
	@CurrentBiddingValue DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BiddingSessions
    SET HighestBidding = @CurrentBiddingValue,
	    TotalBiddingCount = TotalBiddingCount + 1 

    WHERE Id = @SessionId;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetBiddingListBySessionId]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiddingListBySessionId]
    @SessionId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
		Id,
        UserCurrentBidding,
        IsWinner,
        UserId,
        BiddingSessionId,
		BiddingAt
    FROM Biddings
    WHERE BiddingSessionId = @SessionId;
END

GO
/****** Object:  StoredProcedure [dbo].[GetBiddingSessionById]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiddingSessionById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM BiddingSessions bs
    WHERE bs.Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetBiddingSessionsByUserIdWithPaging]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiddingSessionsByUserIdWithPaging]
    @PageNumber INT,
    @PageSize INT,
    @StartTime DATETIME = NULL,
    @EndTime DATETIME = NULL,
    @VIN NVARCHAR(50) = NULL,
	@Name NVARCHAR(100) = NULL,
	@Brand INT = NULL,
    @TotalItems INT OUTPUT,
	@ItemCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;
    
    -- Get the paginated results for BiddingSessions
    SELECT 
        bs.Id,
        bs.StartTime,
        bs.EndTime,
        bs.TotalBiddingCount,
        bs.HighestBidding,
        bs.MinimumJumpingValue,
        bs.IsActive,
        bs.IsClosed,
        bs.VehicleId,
		bs.CreateDate
    FROM BiddingSessions bs
    JOIN Vehicles v ON bs.VehicleId = v.Id
    WHERE 
        (bs.IsActive = 1) AND
		(bs.IsClosed = 0) AND
        (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
        (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
        (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%') AND
		(@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
		(@Brand IS NULL OR v.Brands = @Brand)
    ORDER BY bs.CreateDate DESC
    OFFSET @Skip ROWS
    FETCH NEXT @PageSize ROWS ONLY;

   -- Get the count of items on the current page
    SELECT @ItemCount = COUNT(*)
    FROM (
        SELECT bs.Id
        FROM BiddingSessions bs
        JOIN Vehicles v ON bs.VehicleId = v.Id
        WHERE 
            (bs.IsActive = 1) AND
			(bs.IsClosed = 0) AND
            (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
            (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
            (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%') AND
			(@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
			(@Brand IS NULL OR v.Brands = @Brand)
        ORDER BY bs.CreateDate DESC
        OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY
    ) AS CurrentPage;

	-- Get total items
	SELECT @TotalItems = COUNT(*)
    FROM BiddingSessions
	WHERE IsActive = 1 AND IsClosed = 0
END;
GO
/****** Object:  StoredProcedure [dbo].[GetBiddingSessionsWithPaging]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBiddingSessionsWithPaging]
    @PageNumber INT,
    @PageSize INT,
    @IsActive BIT = NULL,
    @StartTime DATETIME = NULL,
    @EndTime DATETIME = NULL,
    @VIN NVARCHAR(50) = NULL,
	@Name NVARCHAR(100) = NULL,
	@Brand INT = NULL,
    @TotalItems INT OUTPUT,
	@ItemCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;
    
    -- Get the paginated results for BiddingSessions
    SELECT 
        bs.Id,
        bs.StartTime,
        bs.EndTime,
        bs.TotalBiddingCount,
        bs.HighestBidding,
        bs.MinimumJumpingValue,
        bs.IsActive,
        bs.IsClosed,
        bs.VehicleId,
		bs.CreateDate
       
    FROM BiddingSessions bs
    JOIN Vehicles v ON bs.VehicleId = v.Id
    WHERE 
        (@IsActive IS NULL OR bs.IsActive = @IsActive) AND
        (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
        (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
        (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%') AND
		(@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
		(@Brand IS NULL OR v.Brands = @Brand)
    ORDER BY bs.CreateDate DESC
    OFFSET @Skip ROWS
    FETCH NEXT @PageSize ROWS ONLY;

   -- Get the count of items on the current page
    SELECT @ItemCount = COUNT(*)
    FROM (
        SELECT bs.Id
        FROM BiddingSessions bs
        JOIN Vehicles v ON bs.VehicleId = v.Id
        WHERE 
            (@IsActive IS NULL OR bs.IsActive = @IsActive) AND
            (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
            (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
            (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%') AND
			(@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
			(@Brand IS NULL OR v.Brands = @Brand)
        ORDER BY bs.CreateDate DESC
        OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY
    ) AS CurrentPage;

	-- Get total items
	SELECT @TotalItems = COUNT(*)
    FROM BiddingSessions
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTop10Bidding]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTop10Bidding]
    @SessionId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP(10) b.Id,
	b.UserCurrentBidding,
	b.IsWinner,
	b.UserId,
	b.BiddingSessionId, 
	b.BiddingAt,
	u.Id AS UserIdFactory,
	u.Username,
	u.Email
	FROM Biddings b
	JOIN Users u on u.Id = b.UserId
    WHERE b.BiddingSessionId = @SessionId
	ORDER BY BiddingAt DESC
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Username AS UserName,
        Email,
		Password,
        Role,
        Budget
    FROM Users
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserReportByUserIdWithPaging]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserReportByUserIdWithPaging]
	@UserId uniqueidentifier,
    @PageNumber INT,
    @PageSize INT,
	@IsWinner DATETIME = NULL,
    @StartTime DATETIME = NULL,
    @EndTime DATETIME = NULL,
	@IsClosed BIT = NULL,
    @VIN NVARCHAR(50) = NULL,
	@VehicleName NVARCHAR(100) = NULL,
    @TotalItems INT OUTPUT,
	@ItemCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;
    
    -- Get the paginated results for BiddingSessions
    SELECT 
        b.Id AS BiddingId,
        b.UserCurrentBidding,
		b.IsWinner,
		b.BiddingAt,
		bs.Id AS SessionId,
		bs.StartTime,
        bs.EndTime,
        bs.IsClosed,
		v.Name AS VehicleName,
        v.VIN,
		v.ImageUrl
       
    FROM Biddings b
	JOIN BiddingSessions bs on bs.Id = b.BiddingSessionId
    JOIN Vehicles v ON bs.VehicleId = v.Id
    WHERE 
	    b.UserId = @UserId AND
		(@IsWinner IS NULL OR b.IsWinner = @IsWinner) AND
        (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
        (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
		(@IsClosed IS NULL OR bs.IsClosed = @IsClosed) AND
		(@VehicleName IS NULL OR v.Name LIKE '%' + @VehicleName + '%') AND
        (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%') 
    ORDER BY b.BiddingAt DESC
    OFFSET @Skip ROWS
    FETCH NEXT @PageSize ROWS ONLY;

      -- Get the count of items on the current page
    SELECT @ItemCount = COUNT(*)
    FROM (
        SELECT b.Id
        FROM Biddings b
        JOIN BiddingSessions bs ON bs.Id = b.BiddingSessionId
        JOIN Vehicles v ON bs.VehicleId = v.Id
        WHERE 
            b.UserId = @UserId AND
            (@IsWinner IS NULL OR b.IsWinner = @IsWinner) AND
            (@StartTime IS NULL OR bs.StartTime >= @StartTime) AND
            (@EndTime IS NULL OR bs.EndTime <= @EndTime) AND
            (@IsClosed IS NULL OR bs.IsClosed = @IsClosed) AND
            (@VehicleName IS NULL OR v.Name LIKE '%' + @VehicleName + '%') AND
            (@VIN IS NULL OR v.VIN LIKE '%' + @VIN + '%')
        ORDER BY b.BiddingAt DESC
        OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY
    ) AS CurrentPage;

	-- Get total items
	SELECT @TotalItems = COUNT(*)
    FROM Biddings
	WHERE UserId = @UserId
END;
GO
/****** Object:  StoredProcedure [dbo].[GetVehicleById]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVehicleById]
    @Id INT
AS
BEGIN
    SELECT * FROM Vehicles
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetVehicleByVIN]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVehicleByVIN]
    @VIN NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Vehicles
    WHERE VIN = @VIN;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetVehiclesWithPaging]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVehiclesWithPaging]
    @PageNumber INT,
    @PageSize INT,
    @Name NVARCHAR(100) = NULL,
    @Brand INT = NULL,
    @VIN NVARCHAR(50) = NULL,
    @Color NVARCHAR(50) = NULL,
    @Status INT = NULL,
    @TotalItems INT OUTPUT,
	@ItemCount INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Calculate the number of records to skip
    DECLARE @Skip INT = (@PageNumber - 1) * @PageSize;

    -- Get the filtered and paginated results
    SELECT 
        v.Id,
        v.Name,
        v.Description,
        v.Brands,
        v.VIN,
        v.Price,
        v.Color,
        v.ImageUrl,
		v.NumberOfChairs,
		v.Horsepower,
		v.MaximumSpeed,
		v.TrunkCapacity,
        v.Status
    FROM Vehicles v
    WHERE 
        (@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
        (@Brand IS NULL OR v.Brands = @Brand) AND
        (@VIN IS NULL OR v.VIN = @VIN) AND
        (@Color IS NULL OR v.Color = @Color) AND
        (@Status IS NULL OR v.Status = @Status)
    ORDER BY v.Id
    OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY;

    -- Get the total count of filtered results
      SELECT @ItemCount = COUNT(*)
		FROM (
				SELECT v.Id
				FROM Vehicles v
				WHERE 
					(@Name IS NULL OR v.Name LIKE '%' + @Name + '%') AND
					(@Brand IS NULL OR v.Brands = @Brand) AND
					(@VIN IS NULL OR v.VIN = @VIN) AND
					(@Color IS NULL OR v.Color = @Color) AND
					(@Status IS NULL OR v.Status = @Status)
				ORDER BY v.Id
				OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY
			) AS CurrentPage;
	-- count all items
	SELECT @TotalItems = COUNT(*)
    FROM Vehicles v
END;
GO
/****** Object:  StoredProcedure [dbo].[GetWinnerUser]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWinnerUser]
    @SessionId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT TOP(1) * FROM Biddings
    WHERE BiddingSessionId = @SessionId
	ORDER BY BiddingAt DESC
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegisterUser]
    @UserName NVARCHAR(256),
    @Email NVARCHAR(256),
    @Password NVARCHAR(MAX),
	@Role INT,
	@Budget DECIMAL(18, 2),
    @Result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
 
    -- Check if email already exists
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        SET @Result = -1; -- Email already exists
        RETURN;
    END
 
    -- Check if username already exists
    IF EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName)
    BEGIN
        SET @Result = -2; -- Username already exists
        RETURN;
    END
 
    -- Insert the new user with the hashed password (Id will be auto-generated)
   INSERT INTO Users (Id, UserName, Email, Password, Role, Budget)
    VALUES (NEWID(), @UserName, @Email, @Password, @Role, @Budget);
 
    SET @Result = 1; -- Success
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserState]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateUserState]
    @SessionId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Biddings
    SET IsWinner = 0
    WHERE BiddingSessionId = @SessionId;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateVehicle]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVehicle]
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(255),
    @Brand INT,
    @Price DECIMAL(18, 2),
    @Color NVARCHAR(50),
    @ImageUrl NVARCHAR(255),
	@NumberOfChairs INT,
	@Horsepower INT,
	@MaximumSpeed DECIMAL(18,2),
	@TrunkCapacity DECIMAL (10,2),
    @Status INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Vehicles WHERE Id = @Id)
    BEGIN
        RAISERROR('Vehicle with the specified ID does not exist.', 16, 1);
        RETURN;
    END

    UPDATE Vehicles
    SET 
        Name = @Name,
        Description = @Description,
        Brands = @Brand,
        Price = @Price,
        Color = @Color,
        ImageUrl = @ImageUrl,
		NumberOfChairs = @NumberOfChairs,
		Horsepower = @Horsepower,
		MaximumSpeed = @MaximumSpeed,
		TrunkCapacity = @TrunkCapacity,
        Status = @Status
    WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateVehicleStatus]    Script Date: 9/1/2025 2:56:37 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateVehicleStatus]
    @Id INT,
    @Status INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Vehicles
    SET Status = @Status
    WHERE Id = @Id;
END;
GO
USE [master]
GO
ALTER DATABASE [BiddingDb2] SET  READ_WRITE 
GO
