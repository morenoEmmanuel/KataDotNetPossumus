USE [master]
GO
/****** Object:  Database [kataPossumus]    Script Date: 12/06/2024 05:12:41 p. m. ******/
CREATE DATABASE [kataPossumus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'kataPossumus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\kataPossumus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'kataPossumus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\kataPossumus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [kataPossumus] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [kataPossumus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [kataPossumus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [kataPossumus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [kataPossumus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [kataPossumus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [kataPossumus] SET ARITHABORT OFF 
GO
ALTER DATABASE [kataPossumus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [kataPossumus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [kataPossumus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [kataPossumus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [kataPossumus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [kataPossumus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [kataPossumus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [kataPossumus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [kataPossumus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [kataPossumus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [kataPossumus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [kataPossumus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [kataPossumus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [kataPossumus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [kataPossumus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [kataPossumus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [kataPossumus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [kataPossumus] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [kataPossumus] SET  MULTI_USER 
GO
ALTER DATABASE [kataPossumus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [kataPossumus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [kataPossumus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [kataPossumus] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [kataPossumus] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [kataPossumus] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [kataPossumus] SET QUERY_STORE = OFF
GO
USE [kataPossumus]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/06/2024 05:12:42 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[IdAccount] [int] IDENTITY(1,1) NOT NULL,
	[IdCurrency] [int] NOT NULL,
	[IdWallet] [int] NOT NULL,
	[Balance] [float] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[IdAccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountHistory]    Script Date: 12/06/2024 05:12:42 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountHistory](
	[IdAccountHistory] [int] IDENTITY(1,1) NOT NULL,
	[IdAccount] [int] NOT NULL,
	[EditedBy] [int] NOT NULL,
	[NewBalance] [float] NOT NULL,
	[OldBalance] [float] NOT NULL,
	[EditionDate] [date] NOT NULL,
 CONSTRAINT [PK_AccountHistory] PRIMARY KEY CLUSTERED 
(
	[IdAccountHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 12/06/2024 05:12:42 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[IdCurrency] [int] IDENTITY(1,1) NOT NULL,
	[ShortName] [nvarchar](4) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[IdCurrency] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/06/2024 05:12:42 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallet]    Script Date: 12/06/2024 05:12:42 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallet](
	[IdWallet] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[IdWallet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (1, 144, 1, 150, 1)
INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (2, 136, 1, 1000, 1)
INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (3, 144, 2, 0, 1)
INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (4, 136, 2, 90000, 1)
INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (5, 5, 1, 0, 1)
INSERT [dbo].[Account] ([IdAccount], [IdCurrency], [IdWallet], [Balance], [Status]) VALUES (6, 40, 1, 92.8936, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[AccountHistory] ON 

INSERT [dbo].[AccountHistory] ([IdAccountHistory], [IdAccount], [EditedBy], [NewBalance], [OldBalance], [EditionDate]) VALUES (1, 1, 1, 100, 0, CAST(N'2024-06-10' AS Date))
INSERT [dbo].[AccountHistory] ([IdAccountHistory], [IdAccount], [EditedBy], [NewBalance], [OldBalance], [EditionDate]) VALUES (2, 1, 1, 1000, 0, CAST(N'2024-06-10' AS Date))
INSERT [dbo].[AccountHistory] ([IdAccountHistory], [IdAccount], [EditedBy], [NewBalance], [OldBalance], [EditionDate]) VALUES (3, 3, 2, 50, 0, CAST(N'2024-06-10' AS Date))
SET IDENTITY_INSERT [dbo].[AccountHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Currency] ON 

INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (1, N'STN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (2, N'XAG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (3, N'XAU', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (4, N'USDC', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (5, N'USDT', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (6, N'PLN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (7, N'UGX', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (8, N'GGP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (9, N'MWK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (10, N'NAD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (11, N'ALL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (12, N'BHD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (13, N'JEP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (14, N'BWP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (15, N'MRU', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (16, N'BMD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (17, N'MNT', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (18, N'FKP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (19, N'PYG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (20, N'AUD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (21, N'KYD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (22, N'RWF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (23, N'WST', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (24, N'SHP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (25, N'SOS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (26, N'SSP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (27, N'BIF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (28, N'SEK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (29, N'CUC', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (30, N'BTN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (31, N'MOP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (32, N'XDR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (33, N'IMP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (34, N'INR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (35, N'BYN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (36, N'BOB', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (37, N'SRD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (38, N'GEL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (39, N'ZWL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (40, N'EUR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (41, N'BBD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (42, N'RSD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (43, N'SDG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (44, N'VND', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (45, N'VES', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (46, N'ZMW', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (47, N'KGS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (48, N'HUF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (49, N'BND', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (50, N'BAM', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (51, N'CVE', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (52, N'BGN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (53, N'NOK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (54, N'BRL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (55, N'JPY', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (56, N'HRK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (57, N'HKD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (58, N'ISK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (59, N'IDR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (60, N'KRW', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (61, N'KHR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (62, N'XAF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (63, N'CHF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (64, N'MXN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (65, N'PHP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (66, N'RON', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (67, N'RUB', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (68, N'SGD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (69, N'AED', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (70, N'KWD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (71, N'CAD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (72, N'PKR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (73, N'CLP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (74, N'CNY', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (75, N'COP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (76, N'AOA', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (77, N'KMF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (78, N'CRC', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (79, N'CUP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (80, N'GNF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (81, N'NZD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (82, N'EGP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (83, N'DJF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (84, N'ANG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (85, N'DOP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (86, N'JOD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (87, N'AZN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (88, N'SVC', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (89, N'NGN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (90, N'ERN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (91, N'SZL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (92, N'DKK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (93, N'ETB', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (94, N'FJD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (95, N'XPF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (96, N'GMD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (97, N'AFN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (98, N'GHS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (99, N'GIP', 1)
GO
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (100, N'GTQ', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (101, N'HNL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (102, N'GYD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (103, N'HTG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (104, N'XCD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (105, N'GBP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (106, N'AMD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (107, N'IRR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (108, N'JMD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (109, N'IQD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (110, N'KZT', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (111, N'KES', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (112, N'ILS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (113, N'LYD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (114, N'LSL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (115, N'LBP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (116, N'LRD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (117, N'AWG', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (118, N'MKD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (119, N'LAK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (120, N'MGA', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (121, N'ZAR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (122, N'MDL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (123, N'MVR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (124, N'MUR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (125, N'MMK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (126, N'MAD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (127, N'XOF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (128, N'MZN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (129, N'MYR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (130, N'OMR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (131, N'NIO', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (132, N'NPR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (133, N'PAB', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (134, N'PGK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (135, N'PEN', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (136, N'ARS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (137, N'SAR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (138, N'QAR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (139, N'SCR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (140, N'SLL', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (141, N'LKR', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (142, N'SBD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (143, N'VUV', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (144, N'USD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (145, N'DZD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (146, N'BDT', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (147, N'BSD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (148, N'BZD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (149, N'CDF', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (150, N'UAH', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (151, N'YER', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (152, N'TMT', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (153, N'UZS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (154, N'UYU', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (155, N'CZK', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (156, N'SYP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (157, N'TJS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (158, N'TWD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (159, N'TZS', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (160, N'TOP', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (161, N'TTD', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (162, N'THB', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (163, N'TRY', 1)
INSERT [dbo].[Currency] ([IdCurrency], [ShortName], [Status]) VALUES (164, N'TND', 1)
SET IDENTITY_INSERT [dbo].[Currency] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [Name], [Email], [Password], [Status]) VALUES (1, N'Emmanuel', N'emma@emma.com', N'123', 1)
INSERT [dbo].[User] ([IdUser], [Name], [Email], [Password], [Status]) VALUES (2, N'Hernan', N'hernan@hernan.com', N'123', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Wallet] ON 

INSERT [dbo].[Wallet] ([IdWallet], [IdUser], [Status]) VALUES (1, 1, 1)
INSERT [dbo].[Wallet] ([IdWallet], [IdUser], [Status]) VALUES (2, 2, 1)
INSERT [dbo].[Wallet] ([IdWallet], [IdUser], [Status]) VALUES (3, 3, 0)
SET IDENTITY_INSERT [dbo].[Wallet] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Currency] FOREIGN KEY([IdCurrency])
REFERENCES [dbo].[Currency] ([IdCurrency])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Currency]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Wallet] FOREIGN KEY([IdWallet])
REFERENCES [dbo].[Wallet] ([IdWallet])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Wallet]
GO
ALTER TABLE [dbo].[AccountHistory]  WITH CHECK ADD  CONSTRAINT [FK_AccountHistory_Account] FOREIGN KEY([IdAccount])
REFERENCES [dbo].[Account] ([IdAccount])
GO
ALTER TABLE [dbo].[AccountHistory] CHECK CONSTRAINT [FK_AccountHistory_Account]
GO
ALTER TABLE [dbo].[AccountHistory]  WITH CHECK ADD  CONSTRAINT [FK_AccountHistory_User] FOREIGN KEY([EditedBy])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[AccountHistory] CHECK CONSTRAINT [FK_AccountHistory_User]
GO
ALTER TABLE [dbo].[Wallet]  WITH CHECK ADD  CONSTRAINT [FK_Wallet_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Wallet] CHECK CONSTRAINT [FK_Wallet_User]
GO
USE [master]
GO
ALTER DATABASE [kataPossumus] SET  READ_WRITE 
GO
