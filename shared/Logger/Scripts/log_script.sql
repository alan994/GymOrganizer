USE [GymOrganizer_logs]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 24.05.2016. 13:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Level] [int] NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NOT NULL,
	[ErrorCode] [int] null,
	[Tenant] [nvarchar](200) NULL,
	[UserId] [nvarchar](200) NULL
 CONSTRAINT [PK_LogID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

create proc GetLogs
@tenantId nvarchar(200)
as
begin
select top 200 ID, Message, TimeStamp, Level, Exception, ErrorCode, Tenant, UserId from Logs where @tenantId is null or @tenantId = Tenant order by ID desc
end