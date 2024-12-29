USE [master]
GO
/****** Object:  Database [BiddingDb2]    Script Date: 29/12/2024 7:02:50 pm ******/
CREATE DATABASE [BiddingDb2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BiddingDb2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\BiddingDb2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29/12/2024 7:02:50 pm ******/
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
/****** Object:  Table [dbo].[Biddings]    Script Date: 29/12/2024 7:02:50 pm ******/
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
 CONSTRAINT [PK_Biddings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BiddingSessions]    Script Date: 29/12/2024 7:02:50 pm ******/
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
 CONSTRAINT [PK_BiddingSessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 29/12/2024 7:02:50 pm ******/
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
/****** Object:  Table [dbo].[Vehicles]    Script Date: 29/12/2024 7:02:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Desciption] [nvarchar](max) NOT NULL,
	[Brands] [int] NOT NULL,
	[VIN] [nvarchar](450) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Color] [nvarchar](30) NOT NULL,
	[ImageUrl] [nvarchar](255) NOT NULL,
	[Status] [int] NOT NULL,
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
GO
SET IDENTITY_INSERT [dbo].[Biddings] ON 

INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId]) VALUES (11, CAST(520.00 AS Decimal(18, 2)), 0, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'15e5f8bc-a256-4c59-b36f-a3a261843099')
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId]) VALUES (12, CAST(540.00 AS Decimal(18, 2)), 0, N'0b811cef-471b-470f-95a0-7b8201e18de5', N'15e5f8bc-a256-4c59-b36f-a3a261843099')
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId]) VALUES (13, CAST(560.00 AS Decimal(18, 2)), 0, N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'15e5f8bc-a256-4c59-b36f-a3a261843099')
INSERT [dbo].[Biddings] ([Id], [UserCurrentBidding], [IsWinner], [UserId], [BiddingSessionId]) VALUES (14, CAST(580.00 AS Decimal(18, 2)), 1, N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'15e5f8bc-a256-4c59-b36f-a3a261843099')
SET IDENTITY_INSERT [dbo].[Biddings] OFF
GO
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue]) VALUES (N'15e5f8bc-a256-4c59-b36f-a3a261843099', CAST(N'2024-12-29T09:45:35.9500000' AS DateTime2), CAST(N'2024-12-30T09:45:35.9500000' AS DateTime2), 4, CAST(580.00 AS Decimal(18, 2)), 0, 7, 1, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[BiddingSessions] ([Id], [StartTime], [EndTime], [TotalBiddingCount], [HighestBidding], [IsClosed], [VehicleId], [IsActive], [MinimumJumpingValue]) VALUES (N'44229b01-97bf-4025-aa3b-aaf9831201a7', CAST(N'2024-12-29T11:24:31.5033333' AS DateTime2), CAST(N'2024-12-30T11:24:31.5033333' AS DateTime2), 0, CAST(400.00 AS Decimal(18, 2)), 0, 9, 1, CAST(20.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'08bbd2a9-eeeb-477e-9203-0e2f3617dfaa', N'thanhne', N'thanhne@gmail.com', N'AQAAAAIAAYagAAAAEL1ymcm6vGu6RcuTBzMWubVCYGBPpo+8VbktxrjSPsVRMr0y5cejW8u7JUuAs4QRVQ==', 1, CAST(1500.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'f4371dca-ea1b-4d34-8551-2f8d09879279', N'admin', N'admin@admin.com', N'AQAAAAIAAYagAAAAEPt4vT9qay6Kf+ZBZ8ObUjiLe0aDJ3ETIv1gzLPj8gRcCm89YimPnubl7lVvno/WvA==', 0, CAST(99999.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'0b811cef-471b-470f-95a0-7b8201e18de5', N'mla', N'mla@gmail.com', N'AQAAAAIAAYagAAAAEGu711Cf5Apj9jTdHxjt2qqXORRvX6U7GtKqh0MTecgI7L9KhNvMZZ4xcJoxIqaeDQ==', 2, CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'4d68e250-a68f-4fa7-b517-b8edf130906d', N'user example', N'user@example.com', N'AQAAAAIAAYagAAAAEO/ZcsecGhq6hTQMm2VRc7e2rVIArdVOFR9Z/GSqbEEokQGY4y/YyDvrJjJEgJ065Q==', 1, CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Role], [Budget]) VALUES (N'c72160b6-f2d7-422e-93e5-d31280262d2c', N'tina', N'tina@gmail.com', N'AQAAAAIAAYagAAAAELLXQwvwnXcV0oFcohCA4qTinpzvvcHNGOOcbc77yvsaOwz1ShJc4f9pIv+Y0HHXfg==', 0, CAST(1000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (7, N'BMW 3-Series 2020', N'BMW 3-Series 2020', 0, N'BMW1BN1H2312ADAW2', CAST(500.00 AS Decimal(18, 2)), N'White', N'https://autopro8.mediacdn.vn/2020/4/22/bmw-3-series-cac-phien-ban-16-1587532639946455821534.jpg', 0)
INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (9, N'New 2024 BMW i7', N'New 2024 BMW i7 M70 4dr Car in San Francisco', 0, N'BMW123A12B31JK23B12', CAST(400.00 AS Decimal(18, 2)), N'White', N'https://imagehere.png', 0)
INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (10, N'Rolls-Royce Phantom', N'Luxury car', 1, N'RR12BZ1B21M12VK12B3', CAST(600.00 AS Decimal(18, 2)), N'White', N'https://imagehere.png', 0)
INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (11, N'Nissan Patrol 2025', N'Nissan Patrol 2025', 2, N'NIDWUB123V2V312HVA', CAST(300.00 AS Decimal(18, 2)), N'White', N'https://dailymuabanxe.net/wp-content/uploads/2024/09/Nissan-Patrol-2.jpg', 0)
INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (13, N'Nissan X-Terra 2024', N'Nissan X-Terra 2024', 2, N'NI12HJ122KHAWD2DQ1', CAST(350.00 AS Decimal(18, 2)), N'Red', N'https://nissanclub.com.vn/uploads/libraries/kcfinder/upload/images/nissan-terra-2021-red3.jpg', 0)
INSERT [dbo].[Vehicles] ([Id], [Name], [Desciption], [Brands], [VIN], [Price], [Color], [ImageUrl], [Status]) VALUES (14, N'Honda CRV 2023', N'Honda CRV 2023', 3, N'HD12KVBJ123VJ12V3JAZ', CAST(450.00 AS Decimal(18, 2)), N'White', N'https://hondaotobinhdinh.com.vn/wp-content/uploads/2023/10/576995-un-honda-cr-v-hybride-moins-cher-s-ajoute-a-la-gamme.jpg', 0)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
/****** Object:  Index [IX_Biddings_BiddingSessionId]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE NONCLUSTERED INDEX [IX_Biddings_BiddingSessionId] ON [dbo].[Biddings]
(
	[BiddingSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Biddings_UserId]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE NONCLUSTERED INDEX [IX_Biddings_UserId] ON [dbo].[Biddings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BiddingSessions_VehicleId]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE NONCLUSTERED INDEX [IX_BiddingSessions_VehicleId] ON [dbo].[BiddingSessions]
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Username]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles_VIN]    Script Date: 29/12/2024 7:02:51 pm ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Vehicles_VIN] ON [dbo].[Vehicles]
(
	[VIN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BiddingSessions] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[BiddingSessions] ADD  DEFAULT ((0.0)) FOR [MinimumJumpingValue]
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
/****** Object:  StoredProcedure [dbo].[AuthenticateUser]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[CloseBiddingSession]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[CreateBidding]    Script Date: 29/12/2024 7:02:51 pm ******/
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

    INSERT INTO Biddings (UserCurrentBidding, IsWinner, UserId, BiddingSessionId)
    VALUES (@UserCurrentBidding, 1, @UserId, @BiddingSessionId);
END;
GO
/****** Object:  StoredProcedure [dbo].[CreateBiddingSession]    Script Date: 29/12/2024 7:02:51 pm ******/
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
 
    INSERT INTO BiddingSessions (Id, [StartTime], [EndTime],[TotalBiddingCount], [HighestBidding], [MinimumJumpingValue], [IsClosed], IsActive, VehicleId)
    VALUES (NEWID(), @StartTime, @EndTime, 0, @OpeningValue, @MinimumJumpingValue, 0, 1, @VehicleId);
END;
GO
/****** Object:  StoredProcedure [dbo].[DisableBiddingSession]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[FetchBiddingValue]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[GetBiddingListBySessionId]    Script Date: 29/12/2024 7:02:51 pm ******/
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
        BiddingSessionId
    FROM Biddings
    WHERE BiddingSessionId = @SessionId;
END

GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 29/12/2024 7:02:51 pm ******/
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
/****** Object:  StoredProcedure [dbo].[UpdateUserState]    Script Date: 29/12/2024 7:02:51 pm ******/
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
USE [master]
GO
ALTER DATABASE [BiddingDb2] SET  READ_WRITE 
GO
