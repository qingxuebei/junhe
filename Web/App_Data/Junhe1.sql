USE [Junhe]
GO
/****** Object:  Table [dbo].[Agencys]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agencys](
	[Id] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Province] [nvarchar](200) NOT NULL,
	[City] [nvarchar](200) NOT NULL,
	[Village] [nvarchar](200) NOT NULL,
	[Birthday] [datetime] NULL,
	[CareerStatus] [nvarchar](200) NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[PromotionDate] [datetime] NOT NULL,
	[Rank] [nvarchar](200) NOT NULL,
	[RefereeId] [nvarchar](36) NOT NULL,
	[RefereeName] [nvarchar](200) NULL,
	[AgencyId] [nvarchar](36) NULL,
	[AgencyName] [nvarchar](200) NULL,
	[AccountBank] [nvarchar](200) NULL,
	[AccountBankBranch] [nvarchar](200) NULL,
	[Account] [nvarchar](200) NULL,
	[RegisterAddress] [nvarchar](200) NULL,
	[Address] [nvarchar](200) NULL,
	[ZipCode] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[AgentId] [nvarchar](36) NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_AGENCYS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Agents]    Script Date: 2017/1/17 23:52:37 ******/
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
	[AgentsStatus] [int] NOT NULL CONSTRAINT [DF_Agents_AgentsStatus]  DEFAULT ((0)),
 CONSTRAINT [PK_AGENTS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DictBank]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictBank](
	[Id] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_DICTBANK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DictRegion]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictRegion](
	[RegionCode] [nvarchar](36) NOT NULL,
	[RegionName] [nvarchar](200) NOT NULL,
	[ParentCode] [nvarchar](36) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_DICTREGION] PRIMARY KEY CLUSTERED 
(
	[RegionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Income]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Income](
	[YearMonth] [int] NOT NULL,
	[AgentId] [nvarchar](36) NOT NULL,
	[AgentName] [nvarchar](200) NOT NULL,
	[CareerStatus] [nvarchar](200) NOT NULL,
	[Rank] [nvarchar](200) NOT NULL,
	[RefereeId] [nvarchar](36) NULL,
	[RefereeName] [nvarchar](200) NULL,
	[AgencyId] [nvarchar](36) NULL,
	[AgencyName] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
	[AllMonthMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_AllMonthMoney]  DEFAULT ((0)),
	[LastMonthMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_LastMonthMoney]  DEFAULT ((0)),
	[NearlyTwoMonthsMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_NearlyTwoMonthsMoney]  DEFAULT ((0)),
	[NearlyThreeMonthsMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_NearlyThreeMonthsMoney]  DEFAULT ((0)),
	[NearlyFiveMonthsMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_NearlyFiveMonthsMoney]  DEFAULT ((0)),
	[NearlySixMonthsMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_NearlySixMonthsMoney]  DEFAULT ((0)),
	[AllSalesMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_AllSalesMoney]  DEFAULT ((0)),
	[SalesMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_SalesMoney]  DEFAULT ((0)),
	[SalesServiceMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_SalesServiceMoney]  DEFAULT ((0)),
	[PersonalMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_PersonalMoney]  DEFAULT ((0)),
	[PersonalServiceMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_PersonalServiceMoney]  DEFAULT ((0)),
	[MarketMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_MarketMoney]  DEFAULT ((0)),
	[MarketServiceMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_MarketServiceMoney]  DEFAULT ((0)),
	[OneMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_OneMoney]  DEFAULT ((0)),
	[TwoMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_TwoMoney]  DEFAULT ((0)),
	[ThreeMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_ThreeMoney]  DEFAULT ((0)),
	[RegionServiceMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_RegionServiceMoney]  DEFAULT ((0)),
	[RegionYum] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_RegionYum]  DEFAULT ((0)),
	[RegionServiceYum] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_RegionServiceYum]  DEFAULT ((0)),
	[IncomeMoney] [decimal](18, 2) NOT NULL CONSTRAINT [DF_Income_IncomeMoney]  DEFAULT ((0)),
 CONSTRAINT [PK_Income] PRIMARY KEY CLUSTERED 
(
	[YearMonth] ASC,
	[AgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogMonthCreate]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogMonthCreate](
	[Id] [nvarchar](36) NOT NULL,
	[YearMonth] [int] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_LOGMONTHCREATE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [nvarchar](36) NOT NULL,
	[AgentId] [nvarchar](36) NOT NULL,
	[AgentName] [nvarchar](200) NOT NULL,
	[YearMonth] [int] NOT NULL,
	[YearMonthDate] [datetime] NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_ORDERS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrdersDetail]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersDetail](
	[Id] [nvarchar](36) NOT NULL,
	[OrdersId] [nvarchar](36) NOT NULL,
	[ProductId] [nvarchar](36) NOT NULL,
	[ProductName] [nvarchar](200) NOT NULL,
	[UnitPrice] [decimal](18, 0) NOT NULL,
	[Num] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL CONSTRAINT [DF_OrdersDetail_State]  DEFAULT ((0)),
 CONSTRAINT [PK_ORDERSDETAIL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2017/1/17 23:52:37 ******/
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
/****** Object:  Table [dbo].[SysUser]    Script Date: 2017/1/17 23:52:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Username] [nvarchar](36) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_SYSUSER] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown,Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Province'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ProvinceCascade' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'City'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CityCascade' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Village'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'CareerStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Rank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'RefereeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'AccountBank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Agencys', @level2type=N'COLUMN',@level2name=N'Phone'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'YearMonth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AgentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代理人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AgentName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事业状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'CareerStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'Rank'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推荐代理人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'RefereeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推荐代理人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'RefereeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资深代理商编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AgencyId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资深代理商姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AgencyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累计订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AllMonthMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上月订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'LastMonthMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'近两个月的个人订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'NearlyTwoMonthsMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'近三个月订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'NearlyThreeMonthsMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'近五个月的订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'NearlyFiveMonthsMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'近六个月订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'NearlySixMonthsMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'AllSalesMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'VIP顾客销售金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'SalesMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'VIP顾客销售奖金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'SalesServiceMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个人订单金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'PersonalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个人订单返利' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'PersonalServiceMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场总费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'MarketMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场推广服务费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'MarketServiceMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级区域费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'OneMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'二级区域费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'TwoMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'三级区域费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'ThreeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域管理服务费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'RegionServiceMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域业绩' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'RegionYum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业绩分红' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'RegionServiceYum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Income', @level2type=N'COLUMN',@level2name=N'IncomeMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DropDown,Research' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'AgentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'RadioButton' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Password'
GO
