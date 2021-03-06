USE [master]
GO
/****** Object:  Database [prisonerManagementSystem]    Script Date: 04-Jan-21 4:44:28 PM ******/
CREATE DATABASE [prisonerManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'prisonerManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SAQIBKAMRAN\MSSQL\DATA\prisonerManagementSystem.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'prisonerManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SAQIBKAMRAN\MSSQL\DATA\prisonerManagementSystem_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [prisonerManagementSystem] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [prisonerManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [prisonerManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [prisonerManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [prisonerManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [prisonerManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [prisonerManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [prisonerManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [prisonerManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [prisonerManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [prisonerManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [prisonerManagementSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [prisonerManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
USE [prisonerManagementSystem]
GO
/****** Object:  Table [dbo].[asset]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[worth] [int] NOT NULL,
	[prisonerid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[beneficiary]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[beneficiary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[contact] [varchar](50) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[prisonerid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[community_task]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[community_task](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task] [varchar](50) NULL,
	[date] [varchar](50) NULL,
	[amount] [int] NULL,
	[prisonerid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[complaint]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[complaint](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](100) NULL,
	[description] [varchar](100) NOT NULL,
	[prisonerid] [int] NULL,
	[reg_date] [varchar](20) NOT NULL,
	[resolved_date] [varchar](20) NULL,
	[status] [varchar](30) NULL,
	[court_officer_id] [int] NULL,
	[jail_officer_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[court_officer]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[court_officer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[contact] [varchar](50) NOT NULL,
	[address] [varchar](40) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[appointment_date] [varchar](100) NOT NULL,
	[retirement_date] [varchar](100) NULL,
	[in_service] [int] NOT NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[courts_visited]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[courts_visited](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[court_name] [varchar](100) NULL,
	[address] [varchar](100) NOT NULL,
	[prisonerid] [int] NULL,
	[visit_date] [varchar](20) NOT NULL,
	[description] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[crime_record]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[crime_record](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[crime_date] [varchar](20) NOT NULL,
	[description] [varchar](100) NOT NULL,
	[prisonerid] [int] NULL,
	[punishment] [varchar](100) NOT NULL,
	[imprisonment_date] [varchar](50) NOT NULL,
	[release_date] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[imprisonment_record]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[imprisonment_record](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[jail_name] [varchar](100) NULL,
	[imprisonment_duration] [varchar](50) NOT NULL,
	[prisonerid] [int] NULL,
	[reason_of_transfer] [varchar](100) NOT NULL,
	[entry_date] [varchar](20) NULL,
	[exit_date] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jail_officer]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jail_officer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[contact] [varchar](50) NOT NULL,
	[address] [varchar](40) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[appointment_date] [varchar](100) NOT NULL,
	[retirement_date] [varchar](100) NULL,
	[in_service] [int] NOT NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jailer]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jailer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[contact] [varchar](50) NOT NULL,
	[address] [varchar](40) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[appointment_date] [varchar](100) NOT NULL,
	[retirement_date] [varchar](100) NULL,
	[in_service] [int] NOT NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prisoner]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prisoner](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[father_name] [varchar](50) NOT NULL,
	[punishment] [varchar](100) NOT NULL,
	[category] [int] NOT NULL,
	[imprisonment_duration] [varchar](50) NOT NULL,
	[cell_type] [int] NOT NULL,
	[allocated_meeting_time] [varchar](50) NOT NULL,
	[available_meeting_time] [varchar](50) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[stipend] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[prisoner_transfer_request]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[prisoner_transfer_request](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[from_jail] [varchar](100) NULL,
	[to_jail] [varchar](50) NOT NULL,
	[reason_of_transfer] [varchar](50) NOT NULL,
	[description] [varchar](400) NULL,
	[request_date] [varchar](30) NULL,
	[accept_date] [varchar](30) NULL,
	[status] [varchar](30) NULL,
	[prisonerid] [int] NULL,
	[court_officer_id] [int] NULL,
	[jailer_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[visitor]    Script Date: 04-Jan-21 4:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[visitor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[cnic] [varchar](50) NOT NULL,
	[contact] [varchar](50) NOT NULL,
	[address] [varchar](100) NOT NULL,
	[dob] [varchar](50) NOT NULL,
	[date_of_visit] [varchar](50) NOT NULL,
	[duration] [varchar](50) NOT NULL,
	[image] [varchar](400) NOT NULL,
	[prisonerid] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[asset]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[beneficiary]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[community_task]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[complaint]  WITH CHECK ADD FOREIGN KEY([court_officer_id])
REFERENCES [dbo].[court_officer] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[complaint]  WITH CHECK ADD FOREIGN KEY([jail_officer_id])
REFERENCES [dbo].[jail_officer] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[complaint]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[courts_visited]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[crime_record]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[imprisonment_record]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[prisoner_transfer_request]  WITH CHECK ADD FOREIGN KEY([court_officer_id])
REFERENCES [dbo].[court_officer] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[prisoner_transfer_request]  WITH CHECK ADD FOREIGN KEY([jailer_id])
REFERENCES [dbo].[jailer] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[prisoner_transfer_request]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[visitor]  WITH CHECK ADD FOREIGN KEY([prisonerid])
REFERENCES [dbo].[prisoner] ([id])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
USE [master]
GO
ALTER DATABASE [prisonerManagementSystem] SET  READ_WRITE 
GO
