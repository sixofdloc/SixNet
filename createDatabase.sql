USE [SixNet_BBS]

/****** Object:  Table [dbo].[AccessGroups]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessGroups](
	[AccessGroupId] [int] IDENTITY(5,1) NOT NULL,
	[AccessGroupNumber] [int] NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[Description] [nvarchar](4000) NULL,
	[CallsPerDay] [int] NOT NULL,
	[MinutesPerCall] [int] NOT NULL,
	[Flag_Remote_Maintenance] [bit] NOT NULL,
	[Is_SysOp] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AccessGroups] PRIMARY KEY CLUSTERED 
(
	[AccessGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BBSConfigs]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BBSConfigs](
	[BBSConfigId] [int] IDENTITY(2,1) NOT NULL,
	[SysOpUserId] [int] NOT NULL,
	[BBS_Name] [nvarchar](4000) NULL,
	[BBS_URL] [nvarchar](4000) NULL,
	[BBS_Port] [int] NOT NULL,
	[SysOp_Handle] [nvarchar](4000) NULL,
	[SysOp_Email] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dbo.BBSConfigs] PRIMARY KEY CLUSTERED 
(
	[BBSConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallLogs]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallLogs](
	[CallLogId] [int] IDENTITY(60,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Connected] [datetime] NOT NULL,
	[Disconnected] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.CallLogs] PRIMARY KEY CLUSTERED 
(
	[CallLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[FeedbackId] [int] IDENTITY(3,1) NOT NULL,
	[Subject] [nvarchar](4000) NULL,
	[Body] [nvarchar](4000) NULL,
	[FromUser] [int] NOT NULL,
	[Sent] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Feedbacks] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileDetails]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileDetails](
	[FileDetailId] [int] IDENTITY(1,1) NOT NULL,
	[UDBaseID] [int] NOT NULL,
	[UploaderID] [int] NOT NULL,
	[Filename] [nvarchar](4000) NULL,
	[Description] [nvarchar](4000) NULL,
	[FileSizeInBytes] [int] NOT NULL,
	[Uploaded] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.FileDetails] PRIMARY KEY CLUSTERED 
(
	[FileDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GFileAreas]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GFileAreas](
	[GFileAreaId] [int] IDENTITY(8,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentAreaId] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
 CONSTRAINT [PK_dbo.GFileAreas] PRIMARY KEY CLUSTERED 
(
	[GFileAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GFileDetails]    Script Date: 11/13/2017 4:39:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GFileDetails](
	[GFileDetailId] [int] IDENTITY(343,1) NOT NULL,
	[GFileAreaId] [int] NOT NULL,
	[Filename] [nvarchar](4000) NULL,
	[DisplayFilename] [nvarchar](40) NULL,
	[Description] [nvarchar](80) NULL,
	[PETSCII] [bit] NOT NULL,
	[FileSizeInBytes] [int] NOT NULL,
	[Added] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.GFileDetails] PRIMARY KEY CLUSTERED 
(
	[GFileDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Graffiti]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Graffiti](
	[GraffitiId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](4000) NULL,
	[UserId] [int] NOT NULL,
	[Posted] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Graffiti] PRIMARY KEY CLUSTERED 
(
	[GraffitiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageBaseAreas]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageBaseAreas](
	[MessageBaseAreaId] [int] IDENTITY(3,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentAreaId] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MessageBaseAreas] PRIMARY KEY CLUSTERED 
(
	[MessageBaseAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageBases]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageBases](
	[MessageBaseId] [int] IDENTITY(14,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentArea] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
	[MessageBaseArea_MessageBaseAreaId] [int] NULL,
 CONSTRAINT [PK_dbo.MessageBases] PRIMARY KEY CLUSTERED 
(
	[MessageBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageBodies]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageBodies](
	[MessageBodyId] [int] IDENTITY(2,1) NOT NULL,
	[MessageHeaderId] [int] NOT NULL,
	[Body] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dbo.MessageBodies] PRIMARY KEY CLUSTERED 
(
	[MessageBodyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageHeaders]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageHeaders](
	[MessageHeaderId] [int] IDENTITY(2,1) NOT NULL,
	[MessageThreadId] [int] NOT NULL,
	[MessageBaseId] [int] NOT NULL,
	[Subject] [nvarchar](4000) NULL,
	[Anonymous] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[Posted] [datetime] NOT NULL,
	[MessageBody_MessageBodyId] [int] NULL,
	[MessageHeader_MessageHeaderId] [int] NULL,
 CONSTRAINT [PK_dbo.MessageHeaders] PRIMARY KEY CLUSTERED 
(
	[MessageHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageThreads]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageThreads](
	[MessageThreadId] [int] IDENTITY(3,1) NOT NULL,
	[MessageBaseId] [int] NOT NULL,
	[InitialMessageHeaderId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MessageThreads] PRIMARY KEY CLUSTERED 
(
	[MessageThreadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News_Item]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News_Item](
	[News_ItemId] [int] IDENTITY(1,1) NOT NULL,
	[Posted] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[Subject] [nvarchar](4000) NULL,
	[Body] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dbo.News_Item] PRIMARY KEY CLUSTERED 
(
	[News_ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PFileAreas]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PFileAreas](
	[PFileAreaId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentAreaId] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PFileAreas] PRIMARY KEY CLUSTERED 
(
	[PFileAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PFileDetails]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PFileDetails](
	[PFileDetailId] [int] IDENTITY(3,1) NOT NULL,
	[PFileNumber] [int] NOT NULL,
	[ParentAreaId] [int] NOT NULL,
	[Filename] [nvarchar](4000) NULL,
	[Description] [nvarchar](4000) NULL,
	[Added] [datetime] NOT NULL,
	[PFileArea_PFileAreaId] [int] NULL,
 CONSTRAINT [PK_dbo.PFileDetails] PRIMARY KEY CLUSTERED 
(
	[PFileDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysConfigs]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysConfigs](
	[SysConfigId] [int] IDENTITY(2,1) NOT NULL,
	[DatabaseVersion] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SysConfigs] PRIMARY KEY CLUSTERED 
(
	[SysConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDBaseAreas]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDBaseAreas](
	[UDBaseAreaId] [int] IDENTITY(3,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentAreaId] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UDBaseAreas] PRIMARY KEY CLUSTERED 
(
	[UDBaseAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDBases]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDBases](
	[UDBaseId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](4000) NULL,
	[LongDescription] [nvarchar](4000) NULL,
	[ParentArea] [int] NOT NULL,
	[AccessLevel] [int] NOT NULL,
	[FilePath] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.UDBases] PRIMARY KEY CLUSTERED 
(
	[UDBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDFiles]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDFiles](
	[UDFileId] [int] IDENTITY(1,1) NOT NULL,
	[Filename] [nvarchar](255) NOT NULL,
	[Uploaded] [datetime] NOT NULL,
	[Uploader] [int] NOT NULL,
	[Uploadername] [nvarchar](50) NOT NULL,
	[UDBaseId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Filesize] [int] NOT NULL,
	[FileType] [nvarchar](50) NULL,
 CONSTRAINT [PK_UDFiles] PRIMARY KEY CLUSTERED 
(
	[UDFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDefinedFields]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDefinedFields](
	[UserDefinedFieldId] [int] IDENTITY(4,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Key] [nvarchar](4000) NULL,
	[FieldValue] [nvarchar](4000) NULL,
 CONSTRAINT [PK_dbo.UserDefinedFields] PRIMARY KEY CLUSTERED 
(
	[UserDefinedFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMessageBases]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMessageBases](
	[UserMessageBaseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MessageBaseId] [int] NOT NULL,
	[HighestMessageRead] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserMessageBases] PRIMARY KEY CLUSTERED 
(
	[UserMessageBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserReads]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReads](
	[UserReadId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[MessageHeaderId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserReads] PRIMARY KEY CLUSTERED 
(
	[UserReadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/13/2017 4:39:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(4,1) NOT NULL,
	[Username] [nvarchar](255) NULL,
	[HashedPassword] [nvarchar](255) NULL,
	[LastConnection] [datetime] NOT NULL,
	[LastDisconnection] [datetime] NOT NULL,
	[LastConnectionIP] [nvarchar](255) NULL,
	[AccessLevel] [int] NOT NULL,
	[RealName] [nvarchar](255) NULL,
	[ComputerType] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserUDBases]    Script Date: 11/13/2017 4:39:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserUDBases](
	[UserUDBaseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UDBaseId] [int] NOT NULL,
	[LastVisit] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.UserUDBases] PRIMARY KEY CLUSTERED 
(
	[UserUDBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccessGroups] ON 
GO
INSERT [dbo].[AccessGroups] ([AccessGroupId], [AccessGroupNumber], [Title], [Description], [CallsPerDay], [MinutesPerCall], [Flag_Remote_Maintenance], [Is_SysOp]) VALUES (1, 0, N'New Users', N'New Users who have not yet been validated', 2, 30, 0, 0)
GO
INSERT [dbo].[AccessGroups] ([AccessGroupId], [AccessGroupNumber], [Title], [Description], [CallsPerDay], [MinutesPerCall], [Flag_Remote_Maintenance], [Is_SysOp]) VALUES (2, 255, N'SysOps', N'System Operators', 255, 512, 1, 1)
GO
INSERT [dbo].[AccessGroups] ([AccessGroupId], [AccessGroupNumber], [Title], [Description], [CallsPerDay], [MinutesPerCall], [Flag_Remote_Maintenance], [Is_SysOp]) VALUES (3, 254, N'CoOps', N'Assistant System Operators', 255, 512, 0, 0)
GO
INSERT [dbo].[AccessGroups] ([AccessGroupId], [AccessGroupNumber], [Title], [Description], [CallsPerDay], [MinutesPerCall], [Flag_Remote_Maintenance], [Is_SysOp]) VALUES (4, 100, N'Users', N'Normal Users', 5, 60, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[AccessGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[BBSConfigs] ON 
GO
INSERT [dbo].[BBSConfigs] ([BBSConfigId], [SysOpUserId], [BBS_Name], [BBS_URL], [BBS_Port], [SysOp_Handle], [SysOp_Email]) VALUES (1, 1, N'BBS_Name', NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[BBSConfigs] OFF
GO
SET IDENTITY_INSERT [dbo].[GFileAreas] ON 
GO
INSERT [dbo].[GFileAreas] ([GFileAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (1, N'Classics', N'Classic 80s BBS Text Files', -1, 200)
GO
INSERT [dbo].[GFileAreas] ([GFileAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (2, N'Humor', N'Jokes and Amusement Files', -1, 100)
GO
INSERT [dbo].[GFileAreas] ([GFileAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (3, N'Jokes', N'Lists of Jokes', 2, 100)
GO
SET IDENTITY_INSERT [dbo].[GFileAreas] OFF
GO
SET IDENTITY_INSERT [dbo].[Graffiti] ON 
GO
INSERT [dbo].[Graffiti] ([GraffitiId], [Content], [UserId], [Posted]) VALUES (1, N'Testing', 1, CAST(N'2015-01-04T00:28:01.657' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Graffiti] OFF
GO
SET IDENTITY_INSERT [dbo].[MessageBaseAreas] ON 
GO
INSERT [dbo].[MessageBaseAreas] ([MessageBaseAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (1, N'Elite', N'Elite Members Only', -1, 200)
GO
INSERT [dbo].[MessageBaseAreas] ([MessageBaseAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (2, N'Programming', N'Software Development', -1, 10)
GO
SET IDENTITY_INSERT [dbo].[MessageBaseAreas] OFF
GO
SET IDENTITY_INSERT [dbo].[MessageBases] ON 
GO
INSERT [dbo].[MessageBases] ([MessageBaseId], [Title], [LongDescription], [ParentArea], [AccessLevel], [MessageBaseArea_MessageBaseAreaId]) VALUES (3, N'General', N'General Discussions', -1, 10, NULL)
GO
INSERT [dbo].[MessageBases] ([MessageBaseId], [Title], [LongDescription], [ParentArea], [AccessLevel], [MessageBaseArea_MessageBaseAreaId]) VALUES (4, N'BBSing', N'Modern BBSing', -1, 10, NULL)
GO
INSERT [dbo].[MessageBases] ([MessageBaseId], [Title], [LongDescription], [ParentArea], [AccessLevel], [MessageBaseArea_MessageBaseAreaId]) VALUES (13, N'Suggestion Box', N'Suggestions for the BBS', -1, 10, NULL)
GO
SET IDENTITY_INSERT [dbo].[MessageBases] OFF
GO
SET IDENTITY_INSERT [dbo].[PFileDetails] ON 
GO
INSERT [dbo].[PFileDetails] ([PFileDetailId], [PFileNumber], [ParentAreaId], [Filename], [Description], [Added], [PFileArea_PFileAreaId]) VALUES (1, 1, -1, N'PFiles//PFile_Dopewars.dll', N'Dopewars', CAST(N'2014-12-10T18:10:25.767' AS DateTime), NULL)
GO
INSERT [dbo].[PFileDetails] ([PFileDetailId], [PFileNumber], [ParentAreaId], [Filename], [Description], [Added], [PFileArea_PFileAreaId]) VALUES (2, 2, -1, N'PFiles//PFile_Empire.dll', N'Empire', CAST(N'2014-12-30T00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[PFileDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[SysConfigs] ON 
GO
INSERT [dbo].[SysConfigs] ([SysConfigId], [DatabaseVersion]) VALUES (1, 0)
GO
SET IDENTITY_INSERT [dbo].[SysConfigs] OFF
GO
SET IDENTITY_INSERT [dbo].[UDBaseAreas] ON 
GO
INSERT [dbo].[UDBaseAreas] ([UDBaseAreaId], [Title], [LongDescription], [ParentAreaId], [AccessLevel]) VALUES (3, N'Demos', N'Misc. Demos', -1, 1)
GO
SET IDENTITY_INSERT [dbo].[UDBaseAreas] OFF
GO
SET IDENTITY_INSERT [dbo].[UDBases] ON 
GO
INSERT [dbo].[UDBases] ([UDBaseId], [Title], [LongDescription], [ParentArea], [AccessLevel], [FilePath]) VALUES (1, N'NTSC', N'NTSC Demos', 3, 1, N'C:\SixNet\UDBase\Demos')
GO
SET IDENTITY_INSERT [dbo].[UDBases] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDefinedFields] ON 
GO
INSERT [dbo].[UserDefinedFields] ([UserDefinedFieldId], [UserId], [Key], [FieldValue]) VALUES (1, 1, N'DOPEWARS', N'﻿<?xml version="1.0" encoding="utf-8"?><DopeUser xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><UserId>1</UserId><Nick>six</Nick><Cash>1000</Cash><Debt>2200</Debt><Drugs><Dopewars_Drug><Drug_Id>0</Drug_Id><Price>300</Price><Units>1</Units></Dopewars_Drug><Dopewars_Drug><Drug_Id>7</Drug_Id><Price>0</Price><Units>5</Units></Dopewars_Drug></Drugs><Health>100</Health><Firepower>1</Firepower><Posse>0</Posse><Turns>43</Turns><Carry>100</Carry><Location>7</Location><LastTurnUsed>2017-11-10T15:10:51.9520303-05:00</LastTurnUsed></DopeUser>')
GO
INSERT [dbo].[UserDefinedFields] ([UserDefinedFieldId], [UserId], [Key], [FieldValue]) VALUES (2, 1, N'EMPIRE', N'﻿<?xml version="1.0" encoding="utf-8"?><EmpireUser xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><UserId>1</UserId><Land>110</Land><Grain>992</Grain><Gold>1750</Gold><Tax>49</Tax><Mills>0</Mills><Markets>0</Markets><Serfs>1029</Serfs><Soldiers>40</Soldiers><Nobles>1</Nobles><Castle>0</Castle><Shipyards>0</Shipyards><Foundries>0</Foundries><Turns>37</Turns><ArmyMobile>true</ArmyMobile><LastPlay>2015-02-22T22:17:34.0062797-05:00</LastPlay></EmpireUser>')
GO
INSERT [dbo].[UserDefinedFields] ([UserDefinedFieldId], [UserId], [Key], [FieldValue]) VALUES (3, 0, N'EMPIRENEWS', N'|Six was too STINGY!')
GO
SET IDENTITY_INSERT [dbo].[UserDefinedFields] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Username], [HashedPassword], [LastConnection], [LastDisconnection], [LastConnectionIP], [AccessLevel], [RealName], [ComputerType], [Email]) VALUES (1, N'Sysop', N'sysopPassword', CAST(N'2014-12-10T18:10:24.507' AS DateTime), CAST(N'2014-12-10T18:10:24.507' AS DateTime), N'127.0.0.1', 255, N'Oliver Clothesoff', N'Commodore 64', N'six@darklordsofchaos.com')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_UDBaseID]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UDBaseID] ON [dbo].[FileDetails]
(
	[UDBaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UploaderID]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UploaderID] ON [dbo].[FileDetails]
(
	[UploaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_GFileAreaId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_GFileAreaId] ON [dbo].[GFileDetails]
(
	[GFileAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageBaseArea_MessageBaseAreaId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageBaseArea_MessageBaseAreaId] ON [dbo].[MessageBases]
(
	[MessageBaseArea_MessageBaseAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageBaseId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageBaseId] ON [dbo].[MessageHeaders]
(
	[MessageBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageBody_MessageBodyId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageBody_MessageBodyId] ON [dbo].[MessageHeaders]
(
	[MessageBody_MessageBodyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageHeader_MessageHeaderId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageHeader_MessageHeaderId] ON [dbo].[MessageHeaders]
(
	[MessageHeader_MessageHeaderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[MessageHeaders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PFileArea_PFileAreaId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_PFileArea_PFileAreaId] ON [dbo].[PFileDetails]
(
	[PFileArea_PFileAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageBaseId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_MessageBaseId] ON [dbo].[UserMessageBases]
(
	[MessageBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserMessageBases]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UDBaseId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UDBaseId] ON [dbo].[UserUDBases]
(
	[UDBaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/13/2017 4:39:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserUDBases]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UDBases] ADD  CONSTRAINT [DF_UDBases_ParentArea]  DEFAULT ((-1)) FOR [ParentArea]
GO
ALTER TABLE [dbo].[UDBases] ADD  CONSTRAINT [DF_UDBases_AccessLevel]  DEFAULT ((1)) FOR [AccessLevel]
GO
ALTER TABLE [dbo].[FileDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FileDetails_dbo.UDBases_UDBaseID] FOREIGN KEY([UDBaseID])
REFERENCES [dbo].[UDBases] ([UDBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileDetails] CHECK CONSTRAINT [FK_dbo.FileDetails_dbo.UDBases_UDBaseID]
GO
ALTER TABLE [dbo].[FileDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.FileDetails_dbo.Users_UploaderID] FOREIGN KEY([UploaderID])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileDetails] CHECK CONSTRAINT [FK_dbo.FileDetails_dbo.Users_UploaderID]
GO
ALTER TABLE [dbo].[GFileDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GFileDetails_dbo.GFileAreas_GFileAreaId] FOREIGN KEY([GFileAreaId])
REFERENCES [dbo].[GFileAreas] ([GFileAreaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GFileDetails] CHECK CONSTRAINT [FK_dbo.GFileDetails_dbo.GFileAreas_GFileAreaId]
GO
ALTER TABLE [dbo].[MessageBases]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MessageBases_dbo.MessageBaseAreas_MessageBaseArea_MessageBaseAreaId] FOREIGN KEY([MessageBaseArea_MessageBaseAreaId])
REFERENCES [dbo].[MessageBaseAreas] ([MessageBaseAreaId])
GO
ALTER TABLE [dbo].[MessageBases] CHECK CONSTRAINT [FK_dbo.MessageBases_dbo.MessageBaseAreas_MessageBaseArea_MessageBaseAreaId]
GO
ALTER TABLE [dbo].[MessageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageBases_MessageBaseId] FOREIGN KEY([MessageBaseId])
REFERENCES [dbo].[MessageBases] ([MessageBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageHeaders] CHECK CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageBases_MessageBaseId]
GO
ALTER TABLE [dbo].[MessageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageBodies_MessageBody_MessageBodyId] FOREIGN KEY([MessageBody_MessageBodyId])
REFERENCES [dbo].[MessageBodies] ([MessageBodyId])
GO
ALTER TABLE [dbo].[MessageHeaders] CHECK CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageBodies_MessageBody_MessageBodyId]
GO
ALTER TABLE [dbo].[MessageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageHeaders_MessageHeader_MessageHeaderId] FOREIGN KEY([MessageHeader_MessageHeaderId])
REFERENCES [dbo].[MessageHeaders] ([MessageHeaderId])
GO
ALTER TABLE [dbo].[MessageHeaders] CHECK CONSTRAINT [FK_dbo.MessageHeaders_dbo.MessageHeaders_MessageHeader_MessageHeaderId]
GO
ALTER TABLE [dbo].[MessageHeaders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MessageHeaders_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageHeaders] CHECK CONSTRAINT [FK_dbo.MessageHeaders_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[PFileDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PFileDetails_dbo.PFileAreas_PFileArea_PFileAreaId] FOREIGN KEY([PFileArea_PFileAreaId])
REFERENCES [dbo].[PFileAreas] ([PFileAreaId])
GO
ALTER TABLE [dbo].[PFileDetails] CHECK CONSTRAINT [FK_dbo.PFileDetails_dbo.PFileAreas_PFileArea_PFileAreaId]
GO
ALTER TABLE [dbo].[UserMessageBases]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserMessageBases_dbo.MessageBases_MessageBaseId] FOREIGN KEY([MessageBaseId])
REFERENCES [dbo].[MessageBases] ([MessageBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserMessageBases] CHECK CONSTRAINT [FK_dbo.UserMessageBases_dbo.MessageBases_MessageBaseId]
GO
ALTER TABLE [dbo].[UserMessageBases]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserMessageBases_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserMessageBases] CHECK CONSTRAINT [FK_dbo.UserMessageBases_dbo.Users_UserId]
GO
ALTER TABLE [dbo].[UserUDBases]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserUDBases_dbo.UDBases_UDBaseId] FOREIGN KEY([UDBaseId])
REFERENCES [dbo].[UDBases] ([UDBaseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserUDBases] CHECK CONSTRAINT [FK_dbo.UserUDBases_dbo.UDBases_UDBaseId]
GO
ALTER TABLE [dbo].[UserUDBases]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserUDBases_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserUDBases] CHECK CONSTRAINT [FK_dbo.UserUDBases_dbo.Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [SixNet_BBS] SET  READ_WRITE 
GO
