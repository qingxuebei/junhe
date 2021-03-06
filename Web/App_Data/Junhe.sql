USE [Junhe]
GO
/****** Object:  Table [dbo].[Agents]    Script Date: 2016/12/1 23:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agents](
	[Id] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Province] [nvarchar](200) NOT NULL,
	[City] [nvarchar](200) NOT NULL,
	[Village] [nvarchar](200) NOT NULL,
	[Birthday] [datetime] NULL,
	[CareerStatus] [nvarchar](200) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[Rank] [nvarchar](200) NOT NULL,
	[RefereeId] [nvarchar](36) NULL,
	[RefereeName] [nvarchar](200) NULL,
	[AgencyId] [nvarchar](36) NULL,
	[AgencyName] [nvarchar](200) NULL,
	[AccountBank] [nvarchar](200) NULL,
	[AccountBankBranch] [nvarchar](200) NULL,
	[Account] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[ZipCode] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_AGENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2016/12/1 23:53:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [nvarchar](36) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[Unit] [nvarchar](200) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ProvinceCascade' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'City'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CityCascade' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Village'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'CareerStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Rank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'RefereeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'AgencyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'AccountBank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agents', @level2type=N'COLUMN',@level2name=N'Phone'
GO
