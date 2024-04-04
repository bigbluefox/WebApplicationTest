

/****** Object:  Table [dbo].[ImageAttribute]    Script Date: 09/24/2017 21:02:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageAttribute]') AND type in (N'U'))
DROP TABLE [dbo].[ImageAttribute]
GO

/****** Object:  Table [dbo].[ImageAttribute]    Script Date: 09/24/2017 21:02:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ImageAttribute](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [NVARCHAR](255) NULL,
	[Desc] [NVARCHAR](255) NULL,
	[Width] [INT] NULL,
	[Height] [INT] NULL,
	[Size] [INT] NULL,
	[Extension] [VARCHAR](6) NULL,
	[ContentType] [VARCHAR](128) NULL,
	[FullName] [NVARCHAR](255) NULL,
	[DirectoryName] [NVARCHAR](255) NULL,
	[MD5] [VARCHAR](128) NULL,
	[SHA1] [VARCHAR](160) NULL,
 CONSTRAINT [PK_IMAGEATTRIBUTE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬ���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Desc'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Width'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�߶�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Height'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬ��С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Size'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��չ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'Extension'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'ContentType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬȫ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'FullName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ͼƬĿ¼��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'DirectoryName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MD5��ϣֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'MD5'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SHA1��ϣֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute', @level2type=N'COLUMN',@level2name=N'SHA1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' ͼƬ���Ա�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ImageAttribute'
GO



/****** Object:  Table [dbo].[Standard_Local]    Script Date: 09/26/2017 08:47:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Standard_Local]') AND type in (N'U'))
DROP TABLE [dbo].[Standard_Local]
GO

/****** Object:  Table [dbo].[Standard_Local]    Script Date: 09/26/2017 08:47:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Standard_Local](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NULL,
	[FilePath] [nvarchar](255) NULL,
	[FileExt] [varchar](6) NULL,
	[FileSize] [int] NULL,
	[ContentType] [varchar](128) NULL,
	[StandClass] [varchar](8) NULL,
	[StandType] [varchar](8) NULL,
	[A100] [varchar](64) NULL,
	[StandPreNo] [varchar](8) NULL,
	[A107] [varchar](64) NULL,
	[A225] [int] NULL,
	[A825] [varchar](8) NULL,
	[A301] [nvarchar](255) NULL,
	[MD5] [varchar](128) NULL,
	[SHA1] [varchar](160) NULL,
 CONSTRAINT [PK_STANDARD_LOCAL] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'FileId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼Ӣ������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'FilePath'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��չ��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'FileExt'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�ļ���С' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'FileSize'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'ContentType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼���ͣ����ұ�׼[CN]�����ʱ�׼[ISO]�������׼[GW]����ҵ��׼[QT]��������̹淶[JJ]
   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'StandClass'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼���ԣ�GB,GBE,ISO,IEC,CSA,AS,ANSI,BB,CB,FZ,CY,DA,GA...' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'StandType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼��' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'A100'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'StandPreNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼���(����)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'A107'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'A225'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'�б�����' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'A825'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'��׼��������' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'A301'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MD5��ϣֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'MD5'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SHA1��ϣֵ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local', @level2type=N'COLUMN',@level2name=N'SHA1'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'���ر�׼�ļ����Ա���鱾�ر�׼�ļ�' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Standard_Local'
GO


