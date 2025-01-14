USE [QLNS3]
GO
/****** Object:  Table [dbo].[account]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[status] [bit] NOT NULL,
	[id_role] [int] NOT NULL,
 CONSTRAINT [PK_account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[boardnew]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[boardnew](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[content] [nvarchar](4000) NOT NULL,
	[image_link] [nvarchar](4000) NOT NULL,
	[author] [nvarchar](50) NOT NULL,
	[created] [date] NOT NULL,
 CONSTRAINT [PK__boardnew__3213E83FB87261F6] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_cart_1] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[catalog]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catalog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[parent_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameuser] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NULL,
	[phone] [nvarchar](20) NULL,
	[email] [nvarchar](50) NOT NULL,
	[created] [date] NOT NULL,
	[id_account] [int] NULL,
 CONSTRAINT [PK_users_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[employee]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[email] [nchar](50) NOT NULL,
	[phone] [nvarchar](20) NOT NULL,
	[id_account] [int] NULL,
 CONSTRAINT [PK__admin__3213E83F4640B4FB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[import_detail]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[import_detail](
	[invoice_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[import_price] [int] NOT NULL,
	[quantity_import] [int] NOT NULL,
	[stock] [int] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_import_detail] PRIMARY KEY CLUSTERED 
(
	[invoice_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [int] IDENTITY(1,100) NOT NULL,
	[sentdate] [date] NOT NULL,
	[address] [nvarchar](100) NOT NULL,
	[receiveddate] [date] NULL,
	[user_name] [nvarchar](50) NULL,
	[user_mail] [nvarchar](30) NULL,
	[user_phone] [nvarchar](20) NULL,
	[payment] [nvarchar](30) NULL,
	[message] [text] NULL,
	[amount] [int] NULL,
	[status] [int] NULL,
	[admin_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_bill] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_status]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_status](
	[id] [int] IDENTITY(0,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_order_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ordered]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ordered](
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[price] [int] NOT NULL,
	[qty] [int] NOT NULL,
 CONSTRAINT [PK_ordered] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producer]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[numphone] [nchar](10) NOT NULL,
	[email] [nvarchar](50) NULL,
 CONSTRAINT [PK_producer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[catalog_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[quantity] [int] NULL,
	[price] [int] NOT NULL,
	[status] [int] NOT NULL,
	[description] [nvarchar](4000) NOT NULL,
	[content] [nvarchar](4000) NOT NULL,
	[discount] [int] NULL,
	[image_link] [nvarchar](4000) NOT NULL,
	[created] [date] NOT NULL,
	[expiry_date] [int] NULL,
	[unit] [nchar](10) NULL,
 CONSTRAINT [PK__product__3213E83F6B8B1FD3] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[review]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[contentReview] [nvarchar](4000) NULL,
	[created] [date] NULL,
	[score] [int] NULL,
	[id_user] [int] NULL,
 CONSTRAINT [PK__review__3213E83FCDAFC71B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [text] NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slides]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slides](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[discount_content] [nvarchar](100) NULL,
	[content_slide] [nvarchar](200) NOT NULL,
	[image_link] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_slides] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[supply_Invoice]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supply_Invoice](
	[id] [int] IDENTITY(100,1) NOT NULL,
	[supply_time] [date] NOT NULL,
	[ad_id] [int] NOT NULL,
	[producer_id] [int] NOT NULL,
 CONSTRAINT [PK_supply_Invoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[supply_list]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[supply_list](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[quantity] [int] NULL,
	[product_id] [int] NOT NULL,
	[import_price] [int] NULL,
 CONSTRAINT [PK_supply_list] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[used]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[used](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_used] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[used_product]    Script Date: 12/14/2024 12:50:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[used_product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[used_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
 CONSTRAINT [PK_used_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[account] ON 

INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (2, N'manager', N'1234', 1, 1)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (3, N'user1', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (4, N'user2', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (5, N'user3', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (6, N'user4', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (7, N'user5', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (9, N'staff2', N'1234', 1, 3)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (10, N'user6', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (11, N'user7', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (12, N'user8', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (13, N'user9', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (14, N'user10', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (15, N'user11', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (16, N'user12', N'122960', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (17, N'user13', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (18, N'user14', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (19, N'user15', N'1234', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (1011, N'user33', N'928388', 1, 2)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (1012, N'adminthe', N'1234', 1, 3)
INSERT [dbo].[account] ([id], [username], [password], [status], [id_role]) VALUES (1013, N'usertest', N'1234', 1, 2)
SET IDENTITY_INSERT [dbo].[account] OFF
GO
SET IDENTITY_INSERT [dbo].[boardnew] ON 

INSERT [dbo].[boardnew] ([id], [title], [content], [image_link], [author], [created]) VALUES (1, N'Thực phẩm sạch tăng mạnh sau dịch Covid-19', N'(TN&MT) - Trong giai đoạn dịch Covid-19 bùng phát, ý thức về việc nâng cao sức khỏe của người dân ngày càng cao, thực phẩm sạch, an toàn đã được kiểm chứng đang là lựa chọn hàng đầu được nhiều gia đình hướng đến. 
 \n Trước tình trạng thực phẩm kém chất lượng, hàng giả tràn lan trên thị trường, người tiêu dùng đang dần khắt khe hơn trong sự lựa chọn của mình. Trong vài năm trở lại đây, xu hướng thực phẩm an toàn, có nguồn gốc tự nhiên, thân thiện với môi 
 trường trở nên phổ biến và trở thành một xu hướng mới trong ngành thực phẩm tại Việt Nam. Các cơ sở kinh doanh, chuỗi cửa hàng thực phẩm an toàn ngày càng nhiều với đủ mọi mặt hàng để phục vụ nhu cầu của người tiêu dùng. 
\n Báo cáo xu hướng tiêu dùng thực phẩm hữu cơ của AC Nielsen cho thấy có đến 86% người tiêu dùng Việt Nam ưu tiên lựa chọn sản phẩm organic cho những bữa ăn hàng ngày bởi tính an toàn, giàu dinh dưỡng và hương vị thơm ngon. Các chuyên gia cũng cho rằng, 
khi thu nhập của người dân tăng lên, nhu cầu về đời sống cao hơn, tỷ lệ dân số trẻ cao và tầng lớp trung lưu phát triển, người tiêu dùng sẽ dần trở thành những người tiêu dùng thông minh và hướng đến một lối sống xanh và lành mạnh thông qua việc sử dụng 
các thực phẩm có nguồn gốc hữu cơ và nguyên liệu sạch. Kinh doanh thực phẩm sạch đã 3 năm nay, chị Vũ Thị Hà Anh (Cầu Giấy, Hà Nội) cho biết nhiều khi “cung không đủ cầu”, bởi nhu cầu của khách hàng rất cao mà nguồn thực phẩm chất lượng lại có hạn. Thực phẩm
ở cửa hàng của của chị đều có xuất xứ rõ ràng, từ các vùng trên khắp cả nước và có cả thực phẩm nhập khẩu, chủ yếu là các mặt hàng như thịt, rau củ quả, hải sản… Theo chị Hà Anh, tuy giá của các loại thực phẩm hữu cơ, có nguồn gốc tự nhiên có giá thành cao hơn 
những thực phẩm thông thường nhưng khách hàng vẫn sẵn sàng chi trả để đảm bảo sức khỏe cho gia đình.', N'blog_1.jpg', N'Kim Vy', CAST(N'2023-06-05' AS Date))
INSERT [dbo].[boardnew] ([id], [title], [content], [image_link], [author], [created]) VALUES (2, N'Phát hiện loại rau củ chứa chất chữa được bệnh nan y', N'Căn bệnh chưa có thuốc chữa, gây chết người hàng đầu có thể đẩy lùi bởi axit retinoic, chất tạo ra trong quá trình phân hủy vitamin A khi ăn một số rau củ như cải brussel, cà rốt… <br><br>
Hai trường đại học Anh là Aberdeen và Durham vừa công bố nghiên cứu hứa hẹn tạo ra loại thuốc đầu tiên chữa bệnh Alzheimer, căn bệnh phổ biến nhất trong nhóm bệnh mất trí nhớ và là nguyên nhân gây chết sớm hàng đầu ở Anh, Úc và thứ 2 tại Mỹ. <br> </br> Điều đáng ngạc nhiên là chất cơ bản để họ tạo nên "thần dược" này lại là axit retinoic, một chất rất dễ tìm kiếm trong tự nhiên. Axit retinoic là hóa chất được tạo ra trong quá trình cơ thể chúng ta phân hủy vitamin A "siêu nạp" – được tìm thấy trong các loại rau củ được biết đến rất giàu vitamin A như cà rốt hay các loại rau mầm như cải brussel.<br><br>

Theo các tác giả, axit retinoic là một hóa chất cực kỳ tốt cho hệ thần kinh. Khi được ứng dụng vào thuốc, nó có thể đem lại tác động mạnh mẽ hơn nhiều so với cách ăn trực tiếp và sẽ có tác dụng chữa bệnh.<br><br>

Giáo sư Peter McCaffery, đến từ Đại học Aberdeen, thành viên nhóm nghiên cứu mô tả thuốc như một phiên bản khuyếch đại những gì mà quá trình phân hủy vitamin A đã tạo ra cho cơ thể. <br><br>', N'blog_2.jpg', N'Lan Ngọc', CAST(N'2023-05-22' AS Date))
INSERT [dbo].[boardnew] ([id], [title], [content], [image_link], [author], [created]) VALUES (3, N'Phù phép rau chợ đầu mối thành rau sạch tuồn vào trường học', N'Thông tin từ Phòng cảnh sát phòng chống tội phạm về môi trường (PC49 - Công an Hà Nội), 2h30 sáng 14/1, tại chợ đầu mối rau Vân Nội (Đông Anh, Hà Nội), các nhân viên của Cty Rau củ quả Trung Thành (địa chỉ: Xóm Đầm, Vân Nội, huyện Đông Anh) đi thu mua rau tại chợ. Chỉ trong vòng 30 phút, họ đã thu gom được hàng trăm kg rau, củ quả.<br><br>

3h30 sáng, ngay tại sân của Cty này, số hàng trên đã được sơ chế và phù phép thành các loại rau an toàn, sau đó được chất lên những chiếc xe máy chuyển đến các trường tiểu học và mầm non trên địa bàn quận Tây Hồ.<br><br>

Sau quá trình trinh sát và thu thập chứng cứ, 6h30 cùng ngày, tổ công tác gồm Đội 4, PC49 và Thanh tra Sở NN-PTNT Hà Nội kiểm tra, phát hiện nhân viên của Cty CP Rau củ quả Trung Thành là Trần Văn Đỗ đang vận chuyển gần 2 tạ rau, củ, quả cho 2 trường mầm non trên địa bàn quận Tây Hồ là trường Mầm non Nhật Tân và trường mầm non Tứ Liên.<br><br>

Tại thời điểm kiểm tra, chủ hàng không xuất trình được bất kỳ giấy tờ nào chứng minh nguồn gốc của sản phẩm. Điều đáng nói là hợp đồng ký kết giữa Cty Trung Thành và nhà trường cam kết với hàng ngàn phụ huynh học sinh của trường này cung cấp rau và thực phẩm an toàn có nguồn gốc xuất xứ.<br><br>

Tuy nhiên, theo khẳng định của PC49, ''số thực phẩm này không thể đảm bảo để nhà trường chế biến thành các suất ăn. Đáng chú ý, nguồn cung cấp rau của Cty Trung Thành trước đây là HTX rau Đạo Đức, đã bị cơ quan công an phát hiện hành vi trà trộn rau bẩn vào rau an toàn hồi tháng 6 vừa qua.<br><br>', N'blog_3.jpg', N'Lê Thạch', CAST(N'2022-05-22' AS Date))
INSERT [dbo].[boardnew] ([id], [title], [content], [image_link], [author], [created]) VALUES (4, N'Việt Nam đứng thứ 6 thế giới về xuất khẩu mật ong', N'Giá trị nhập khẩu mật ong toàn thế giới năm 2017 đạt 2.300 triệu USD. Mặc dù sản lượng mật ong xuất khẩu của Việt Nam đứng ở vị trí cao, nhưng giá mật ong của Việt Nam chỉ đạt 1,22 Euro/kg - mức giá thấp nhất về sản phẩm mật ong xuất khẩu. Sản phẩm mật ong có mức giá cao nhất thuộc về New Zealand với 23,25 Euro/kg.<br><br>

Đây là thông tin được TS. Đinh Quyết Tâm, Hội Nuôi ong Việt Nam chia sẻ tại hội thảo "Giải pháp nâng cao chất lượng và thúc đẩy xuất khẩu mật ong Việt Nam", do Viện Chăn nuôi, Hội Nuôi ong Việt Nam và Công ty Syngenta tổ chức ngày 17/8 tại Hà Nội.<br><br>

TS. Nguyễn Thanh Sơn, Viện trưởng Viện Chăn nuôi cho biết, hiện cả nước có 1,2 triệu đàn ong gồm các giống ong ngoại và ong nội. Trong đó, số đàn ong nội khoảng 200 nghìn đàn, ong ngoại khoảng 1 triệu đàn. Số người nuôi ong khoảng 30 nghìn người, trong đó có 6 nghìn người nuôi chuyên nghiệp.<br><br>

Ở Việt Nam nghề nuôi ong đã và đang góp phần đáng kể vào việc tạo sinh kế và cải thiện đời sống người dân và góp phần nâng cao kim ngạch xuất khẩu các sản phẩm nông nghiệp.

Ngoài việc tạo ra các sản phẩm có giá trị cao, ong mật đóng vai trò chủ yếu trong thụ phấn cho cây trồng, giúp tăng năng suất và chất lượng rau quả. Hàng năm, Việt Nam sản xuất được hơn 55.000 tấn mật ong và hơn 1.000 tấn sáp ong; trong đó có khoảng 85–90% sản lượng mật và sáp ong được xuất khẩu.<br><br>

Thị trường tiêu thụ sản phẩm ong chủ yếu là Mỹ và các nước châu Âu, Nhật Bản. Đây là những thị trường khó tính, yêu cầu cao về chất lượng mật ong, nhưng với gần 5.000 tấn mật ong được xuất khẩu năm 2014 đã đưa kim ngạch xuất khẩu sản phẩm ong đạt 150 triệu USD thu được từ 12 quốc gia và vùng lãnh thổ. Việt Nam đứng hàng thứ 6 thế giới và thứ 2 châu Á về xuất khẩu mật ong.<br><br>

Tuy nhiên, trong 3 năm gần đây do nhiều nguyên nhân cả khách quan và chủ quan, xuất khẩu mật ong của Việt Nam đã giảm đáng kể cả về sản lượng và kim ngạch, gây khó khăn không nhỏ tới đời sống người nuôi ong và các doanh nghiệp xuất khẩu. Năm 2017 Việt Nam chỉ xuất khẩu được 39.000 tấn thu về gần 70 triệu USD.<br><br>', N'blog_4.png', N'Lan Ngọc', CAST(N'2023-05-22' AS Date))
SET IDENTITY_INSERT [dbo].[boardnew] OFF
GO
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (1, 3, 1)
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (5, 3, 1)
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (5, 15, 1)
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (5, 22, 1)
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (20, 6, 3)
INSERT [dbo].[cart] ([user_id], [product_id], [quantity]) VALUES (21, 7, 1)
GO
SET IDENTITY_INSERT [dbo].[catalog] ON 

INSERT [dbo].[catalog] ([id], [name], [parent_id]) VALUES (1, N'Rau củ quả', NULL)
INSERT [dbo].[catalog] ([id], [name], [parent_id]) VALUES (2, N'Các Loại Hạt', NULL)
INSERT [dbo].[catalog] ([id], [name], [parent_id]) VALUES (3, N'Trái Cây', NULL)
INSERT [dbo].[catalog] ([id], [name], [parent_id]) VALUES (4, N'Mật Ong & Tinh Dầu', NULL)
SET IDENTITY_INSERT [dbo].[catalog] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (1, N'Nhật Porn', N'', N'091293232', N'NhatPorn@gmail.com', CAST(N'2023-05-05' AS Date), 3)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (2, N'Nam Nam', NULL, N'032312312', N'Nam@gmail', CAST(N'2023-05-06' AS Date), 10)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (3, N'Nghĩa Khánh Hòa', NULL, N'0905015017', N'nghianguyen@gmail.com', CAST(N'2024-08-01' AS Date), 4)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (4, N'Toản Mõm', NULL, N'12345677', N'toanmom@gmail.com', CAST(N'2024-08-01' AS Date), 5)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (5, N'Toản ', NULL, N'123456772', N'test2@gail.com', CAST(N'2024-08-14' AS Date), 6)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (7, N'user7', N'97 Man Thiện', N'098765432', N'test7@gmail', CAST(N'2024-06-06' AS Date), 11)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (10, N'user8', N'97 Man Thiện', N'098765433', N'test8@gmail', CAST(N'2024-05-06' AS Date), 12)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (11, N'user9', N'nnaan', N'092938283', N'test9@gmail', CAST(N'2022-03-03' AS Date), 13)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (15, N'Thế Ok', N'Quảng trị', N'20391203912', N'test10@gmail.com', CAST(N'2024-09-10' AS Date), 14)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (16, N'user11', N'asd', N'0982727212', N'test11@gmail', CAST(N'2022-10-10' AS Date), 15)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (17, N'user12', N'11 Nguyễn Bỉnh Khiêm', N'097823123', N'test122@gmail.com', CAST(N'2023-02-09' AS Date), 16)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (18, N'user13', N'104 Man Thiên', N'0987262822', N'test13@gmail.com', CAST(N'2024-10-10' AS Date), 17)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (19, N'user14', N'92 Lê Văn Viêt', N'0787224334', N'test14@gmail', CAST(N'2024-09-09' AS Date), 18)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (20, N'user15', N'100 Lê Văn Lương', N'0909009009', N'test15@gmail.com', CAST(N'2024-10-05' AS Date), 19)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (21, N'Lê VL', N'97 Man Thien Street, Thu Duc City', N'0972968122', N'phanthulinhs@gmail.com', CAST(N'2023-01-01' AS Date), 1011)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (22, N'Tiến Bịp', N'Nghệ An', N'0923823232', N'bip@gmail.com', CAST(N'2024-01-01' AS Date), 7)
INSERT [dbo].[customer] ([id], [nameuser], [address], [phone], [email], [created], [id_account]) VALUES (23, N'LVL update', N'99 Man Thien Street, Thu Duc City', N'0905015216', N'tt@gmail.com', CAST(N'2024-12-03' AS Date), 1013)
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[employee] ON 

INSERT [dbo].[employee] ([id], [name], [email], [phone], [id_account]) VALUES (1, N'AkaCua', N'eze@gmail.com                                     ', N'0972962222', 2)
INSERT [dbo].[employee] ([id], [name], [email], [phone], [id_account]) VALUES (3, N'Toàn đù staff', N'n20dccn038@student.ptithcm.edu.vn                 ', N'3232422111', 9)
INSERT [dbo].[employee] ([id], [name], [email], [phone], [id_account]) VALUES (4, N'theok', N'theok@gmail.com                                   ', N'0333223423', 1012)
SET IDENTITY_INSERT [dbo].[employee] OFF
GO
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (100, 1, 15000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (100, 2, 14000, 100, 95, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (100, 4, 45000, 100, 97, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (100, 6, 120000, 90, 87, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 1, 7500, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 2, 7000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 3, 25000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 4, 22500, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 5, 27500, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 6, 60000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 7, 40000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 8, 50000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 41, 75000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 43, 25000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (101, 45, 42500, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 1, 750000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 2, 700000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 5, 2750000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 7, 4000000, 100, 96, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 8, 5000000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 10, 2250000, 100, 94, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 11, 12500000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 12, 15000000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 14, 22500000, 100, 88, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 18, 2500000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 19, 1250000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 21, 4500000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 23, 1500000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 25, 2500000, 100, 90, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 30, 5000000, 100, 94, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 31, 500000, 100, 100, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 35, 7500000, 100, 97, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 40, 600000, 100, 85, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (102, 44, 17500000, 100, 99, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (119, 9, 3750000, 100, 91, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (119, 13, 10000000, 100, 97, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (120, 2, 700000, 97, 97, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (120, 6, 6000000, 100, 99, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (121, 2, 700000, 100, 98, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (121, 41, 7500000, 98, 98, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (122, 12, 15000000, 98, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (123, 2, 700000, 110, 110, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 2, 400000, 100, 91, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 3, 500000, 100, 73, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 4, 1000000, 100, 19, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 5, 2750000, 100, 89, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 6, 6000000, 100, 43, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 7, 4000000, 100, 92, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 8, 5000000, 100, 94, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 9, 3750000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 10, 2250000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 11, 12500000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 15, 6250000, 100, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 16, 7500000, 100, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 17, 1750000, 100, 98, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 18, 2500000, 100, 97, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 19, 1250000, 100, 93, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 21, 4500000, 100, 91, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 22, 1250000, 100, 88, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 23, 1500000, 100, 95, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 24, 1750000, 100, 95, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 26, 2500000, 100, 96, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 27, 2500000, 100, 96, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 28, 2500000, 100, 97, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 29, 5000000, 100, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 31, 500000, 100, 30, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 32, 10000000, 100, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 33, 4500000, 100, 98, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 34, 2500000, 100, 91, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 36, 2500000, 100, 97, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 37, 25000000, 100, 93, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 38, 12500000, 100, 98, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 39, 2500000, 100, 91, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 41, 7500000, 100, 95, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 42, 4000000, 100, 97, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 43, 2500000, 100, 87, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 45, 4250000, 100, 93, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (124, 47, 61565650, 100, 94, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1124, 1, 750000, 103, 103, 0)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1126, 20, 2000000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 1, 75000, 50, 50, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 2, 70000, 61, 60, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 3, 250000, 30, 29, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 4, 22500, 10, 10, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 5, 275000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 6, 60000, 10, 10, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 7, 400000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 8, 50000, 10, 10, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1127, 17, 1750000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1128, 21, 1500000, 50, 50, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1128, 22, 1250000, 100, 100, 1)
INSERT [dbo].[import_detail] ([invoice_id], [product_id], [import_price], [quantity_import], [stock], [status]) VALUES (1128, 23, 1500000, 100, 100, 1)
GO
SET IDENTITY_INSERT [dbo].[order] ON 

INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (1, CAST(N'2024-07-31' AS Date), N'PtitHCM', CAST(N'2024-07-31' AS Date), N'user1', N'transporter@gmail.com', N'123456789', N'Direct Trading', N'm?i lúc m?i noi', 1, 2, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3301, CAST(N'2024-07-31' AS Date), N'Av. dos Andradas, 3000
Andar 2, Apartamento 1', CAST(N'2024-07-31' AS Date), N'user1', N'user1@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 1, 0, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3401, CAST(N'2024-08-02' AS Date), N'Khánh Hòa', CAST(N'2024-08-02' AS Date), N'user3', N'nghia@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 1, 3, 1, 3)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3501, CAST(N'2024-08-02' AS Date), N'ưeqwe', CAST(N'2024-08-02' AS Date), N'user3', N'nghia@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 1, 2, 1, 3)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3601, CAST(N'2024-08-02' AS Date), N'Hải Phòng', CAST(N'2024-08-02' AS Date), N'user4', N'toan@gmail', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 247500, 2, 1, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3701, CAST(N'2024-08-09' AS Date), N'12312312', CAST(N'2024-08-09' AS Date), N'user3', N'nghia@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 299400, 2, 1, 3)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3801, CAST(N'2024-08-11' AS Date), N'12312312', CAST(N'2024-08-11' AS Date), N'user3', N'nghia@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', N'ok', 90000, 2, 1, 3)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (3901, CAST(N'2024-08-13' AS Date), N'123141212', CAST(N'2024-08-13' AS Date), N'quang brave', N'user1@gmail.com', N'123456789', N'Thanh toán khi nhận hàng', NULL, 28500, 2, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (4001, CAST(N'2024-08-14' AS Date), N'123141212', CAST(N'2024-08-14' AS Date), N'Linhhh', N'toan@gmail', N'123456789', N'NULLThanh toán khi nhận hàng', NULL, 468000, 2, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (4101, CAST(N'2024-08-14' AS Date), N'HCM', CAST(N'2024-08-14' AS Date), N'MC', N'toan@gmail', N'123456789', N'Thanh toán khi nhận hàng', NULL, 120600, 2, 1, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (8401, CAST(N'2024-08-14' AS Date), N'2323', CAST(N'2024-08-14' AS Date), N'ez', N'toan@gmail', N'123456789', N'Thanh toán khi nhận hàng', NULL, 69600, 2, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (8501, CAST(N'2024-10-04' AS Date), N'fsdfsd', NULL, N'user1', N'NhatPorn@gmail.com', N'091293232', N'Thanh toán khi nhận hàng', N'fsdf', 37800, 0, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (8601, CAST(N'2024-10-05' AS Date), N'sđs', NULL, N'user1', N'NhatPorn@gmail.com', N'091293232', N'Thanh toán khi nhận hàng', NULL, 58200, 3, 1, 1)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (108601, CAST(N'2024-10-12' AS Date), N'ptit hcm', NULL, N'user3', N'toanmom@gmail.com', N'12345677', N'Thanh toán khi nhận hàng', N'abc xyz', 1092600, 3, 1, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109401, CAST(N'2024-10-17' AS Date), N'assss', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán khi nhận hàng', N'ok', 700500, 2, 3, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109501, CAST(N'2024-10-17' AS Date), N'nam nam', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán bằng VNPay', NULL, 1464663, 2, 1, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109601, CAST(N'2024-10-17' AS Date), N'assdd', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán khi nhận hàng', NULL, 260000, 2, 3, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109701, CAST(N'2024-10-18' AS Date), N'Thủ Đức', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'abc', 555000, 2, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109801, CAST(N'2024-10-18' AS Date), N'Thủ Đức', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'aass', 322500, 2, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (109901, CAST(N'2024-10-18' AS Date), N'asssa', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'sdsdd', 187500, 2, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110001, CAST(N'2024-10-18' AS Date), N'dabet', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'2222', 223250, 2, 1, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110101, CAST(N'2024-10-18' AS Date), N'sđss', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'aa', 1255063, 2, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110201, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'aaa', 95000, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110301, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'aa', 95000, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110401, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'aq', 296400, 2, 1, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110501, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'qwe', 76000, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110601, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'e', 179250, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110701, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'ffd', 100000, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110801, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', N'c', 237500, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (110901, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'ddd', 102600, 2, 1, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111001, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'q', 185000, 2, 3, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111101, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'd', 324600, 2, 3, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111201, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'v', 257000, 2, 3, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111301, CAST(N'2024-10-18' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'a', 165000, 2, 3, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111401, CAST(N'2024-10-18' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'c', 295000, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111501, CAST(N'2024-10-18' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'a', 531250, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111601, CAST(N'2024-10-18' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'aa', 135000, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111701, CAST(N'2024-10-18' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N's', 28500, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111801, CAST(N'2024-10-18' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'c', 116250, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (111901, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', N'l', 360750, 2, 1, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112001, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', N'k', 83850, 2, 3, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112101, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', N'l', 24000, 2, 3, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112201, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', N',', 119500, 2, 3, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112301, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', NULL, 50400, 2, 1, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112401, CAST(N'2024-10-18' AS Date), N'Quảng trị', NULL, N'user10', N'test10@gmail.com', N'20391203912', N'Thanh toán khi nhận hàng', NULL, 45000, 2, 3, 15)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112501, CAST(N'2024-10-18' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 557000, 2, 3, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112601, CAST(N'2024-10-18' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 1371313, 3, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112701, CAST(N'2024-10-18' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 85000, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112801, CAST(N'2024-10-18' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 81500, 2, 3, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (112901, CAST(N'2024-10-18' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 57000, 2, 3, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113001, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 62100, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113101, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 65000, 2, 1, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113201, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 212500, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113301, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 91000, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113401, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 432000, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113501, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 295500, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113601, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 45000, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113701, CAST(N'2024-10-18' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', NULL, 242500, 2, 3, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113801, CAST(N'2024-10-18' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', NULL, 1340063, 2, 3, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (113901, CAST(N'2024-10-18' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', NULL, 126000, 3, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114001, CAST(N'2024-10-18' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', NULL, 500000, 3, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114101, CAST(N'2024-10-18' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', NULL, 92000, 3, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114201, CAST(N'2024-10-18' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', NULL, 90000, 3, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114301, CAST(N'2024-10-18' AS Date), N'92 Lê Văn Viêt', NULL, N'user14', N'test14@gmail', N'0787224334', N'Thanh toán khi nhận hàng', NULL, 271250, 1, 3, 19)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114401, CAST(N'2024-10-18' AS Date), N'92 Lê Văn Viêt', NULL, N'user14', N'test14@gmail', N'0787224334', N'Thanh toán khi nhận hàng', NULL, 160000, 1, 3, 19)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114501, CAST(N'2024-10-18' AS Date), N'92 Lê Văn Viêt', NULL, N'user14', N'test14@gmail', N'0787224334', N'Thanh toán khi nhận hàng', NULL, 315000, 1, 3, 19)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114601, CAST(N'2024-10-18' AS Date), N'92 Lê Văn Viêt', NULL, N'user14', N'test14@gmail', N'0787224334', N'Thanh toán khi nhận hàng', NULL, 146400, 1, 3, 19)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114701, CAST(N'2024-10-18' AS Date), N'92 Lê Văn Viêt', NULL, N'user14', N'test14@gmail', N'0787224334', N'Thanh toán khi nhận hàng', NULL, 175000, 1, 3, 19)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114801, CAST(N'2024-10-18' AS Date), N'100 Lê Văn Lương', NULL, N'user15', N'test15@gmail.com', N'0909009009', N'Thanh toán khi nhận hàng', NULL, 137500, 2, 3, 20)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (114901, CAST(N'2024-10-18' AS Date), N'100 Lê Văn Lương', NULL, N'user15', N'test15@gmail.com', N'0909009009', N'Thanh toán khi nhận hàng', NULL, 485000, 2, 1, 20)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115001, CAST(N'2024-10-18' AS Date), N'100 Lê Văn Lương', NULL, N'user15', N'test15@gmail.com', N'0909009009', N'Thanh toán khi nhận hàng', NULL, 450000, 2, 3, 20)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115101, CAST(N'2024-10-18' AS Date), N'100 Lê Văn Lương', NULL, N'user15', N'test15@gmail.com', N'0909009009', N'Thanh toán khi nhận hàng', NULL, 58900, 2, 3, 20)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115201, CAST(N'2024-10-18' AS Date), N'100 Lê Văn Lương', NULL, N'user15', N'test15@gmail.com', N'0909009009', N'Thanh toán bằng VNPay', NULL, 47500, 2, 1, 20)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115301, CAST(N'2024-11-06' AS Date), N'ádasda', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán bằng VNPay', N'abc', 108000, 1, 3, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115401, CAST(N'2024-11-06' AS Date), N'ádsd', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán bằng VNPay', N'ok', 50000, 2, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115501, CAST(N'2024-11-06' AS Date), N'Hải Phòng hoa cải đỏ', NULL, N'user3', N'toanmom@gmail.com', N'12345677', N'Thanh toán bằng VNPay', N'ok', 72000, 2, 3, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115601, CAST(N'2024-11-06' AS Date), N'Hải Phòng hoa cải đỏ', NULL, N'user3', N'toanmom@gmail.com', N'12345677', N'Thanh toán bằng VNPay', N'ok', 72000, 3, 1, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115701, CAST(N'2024-11-06' AS Date), N'asds', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', N'aaa', 45000, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115801, CAST(N'2024-11-06' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán bằng VNPay', N'dd', 50000, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (115901, CAST(N'2024-11-06' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán bằng VNPay', NULL, 50000, 3, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116001, CAST(N'2024-11-07' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán bằng VNPay', N'222', 108000, 2, 1, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116101, CAST(N'2024-11-07' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán bằng VNPay', N'y', 40500, 2, 1, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116201, CAST(N'2024-11-07' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán bằng VNPay', N'u', 90000, 2, 1, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116301, CAST(N'2024-11-07' AS Date), N'o2', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán bằng VNPay', N'xyz', 94500, 2, 1, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116401, CAST(N'2024-11-07' AS Date), N'fdfd', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán bằng VNPay', N'ddd', 72000, 2, 1, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116501, CAST(N'2024-11-07' AS Date), N'asds', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán bằng VNPay', N'a', 125000, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116601, CAST(N'2024-11-07' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán bằng VNPay', N's', 42500, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116701, CAST(N'2024-11-07' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán bằng VNPay', N'x', 42500, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116801, CAST(N'2024-11-07' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', N'd', 45000, 0, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (116901, CAST(N'2024-11-07' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán bằng VNPay', N'2', 72000, 2, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (117001, CAST(N'2024-11-10' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', N'j', 15000, 2, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (117101, CAST(N'2024-11-10' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán bằng VNPay', N'a', 90000, 2, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (217001, CAST(N'2024-11-22' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', N'ii', 1242713, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317001, CAST(N'2024-11-24' AS Date), N'97 Man Thiện', NULL, N'user8', N'test8@gmail', N'098765433', N'Thanh toán khi nhận hàng', N'sa', 613750, 2, 1, 10)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317101, CAST(N'2024-11-24' AS Date), N'97 Man Thiện', NULL, N'user7', N'test7@gmail', N'098765432', N'Thanh toán khi nhận hàng', NULL, 1080000, 2, 3, 7)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317201, CAST(N'2024-11-29' AS Date), N'97 Man Thien Street, Thu Duc City', NULL, N'user4', N'test2@gail.com', N'123456772', N'Thanh toán khi nhận hàng', N'accs', 304500, 2, 1, 5)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317301, CAST(N'2024-12-03' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', N'L?n ki?m tra 1', 285500, 2, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317401, CAST(N'2024-12-03' AS Date), N'97 Man Thien Street, Thu Duc City', NULL, N'user3', N'toanmom@gmail.com', N'12345677', N'Thanh toán khi nhận hàng', N'sdasd', 5724000, 2, 3, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317501, CAST(N'2024-12-03' AS Date), N'asd', NULL, N'user3', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 6530000, 3, 1, 4)
GO
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317601, CAST(N'2024-12-07' AS Date), N'Nghệ An', NULL, N'user5', N'bip@gmail.com', N'0923823232', N'Thanh toán khi nhận hàng', N'mua d?ng th?i', 700000, 2, 3, 22)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (317901, CAST(N'2024-12-07' AS Date), N'Nghệ An', NULL, N'user5', N'bip@gmail.com', N'0923823232', N'Thanh toán khi nhận hàng', N'xxxx', 12600, 2, 1, 22)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318001, CAST(N'2024-12-07' AS Date), N'Nghệ An', NULL, N'user5', N'bip@gmail.com', N'0923823232', N'Thanh toán khi nhận hàng', N'th? nghi?m mua hàng', 55000, 2, 3, 22)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318101, CAST(N'2024-12-07' AS Date), N'11 Nguyễn Bỉnh Khiêm', NULL, N'user12', N'test122@gmail.com', N'097823123', N'Thanh toán khi nhận hàng', N'mua hàng', 45000, 2, 1, 17)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318201, CAST(N'2024-12-07' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'mua', 160000, 2, 3, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318301, CAST(N'2024-12-07' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'hjj', 22500, 3, 1, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318401, CAST(N'2024-12-07' AS Date), N'nnaan', NULL, N'user9', N'test9@gmail', N'092938283', N'Thanh toán khi nhận hàng', N'ghi nh?n xóa', 72000, 3, 1, 11)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318501, CAST(N'2024-12-13' AS Date), N'104 Man Thiên', NULL, N'user13', N'test13@gmail.com', N'0987262822', N'Thanh toán khi nhận hàng', N'test', 407500, 3, 1, 18)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318601, CAST(N'2024-12-14' AS Date), N'97 Man Thien Street, Thu Duc City', NULL, N'user3', N'toanmom@gmail.com', N'12345677', N'Thanh toán khi nhận hàng', N'xx', 1702800, 2, 3, 4)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318701, CAST(N'2024-12-14' AS Date), N'Nghệ An', NULL, N'user5', N'bip@gmail.com', N'0923823232', N'Thanh toán khi nhận hàng', NULL, 547500, 2, 3, 22)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318801, CAST(N'2024-12-14' AS Date), N'Nghệ An', NULL, N'user5', N'bip@gmail.com', N'0923823232', N'Thanh toán khi nhận hàng', NULL, 1134000, 3, 1, 22)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (318901, CAST(N'2024-12-14' AS Date), N'97 Man Thien Street, Thu Duc City', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', N'l', 294000, 3, 1, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (319001, CAST(N'2024-12-14' AS Date), N'93 Man Thien Street, Thu Duc City', NULL, N'user6', N'Nam@gmail', N'032312312', N'Thanh toán khi nhận hàng', NULL, 356250, 1, 3, 2)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (319101, CAST(N'2024-12-14' AS Date), N'thu duc', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 62600, 2, 3, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (319201, CAST(N'2024-12-14' AS Date), N'asd', NULL, N'user11', N'test11@gmail', N'0982727212', N'Thanh toán khi nhận hàng', NULL, 75000, 3, 1, 16)
INSERT [dbo].[order] ([id], [sentdate], [address], [receiveddate], [user_name], [user_mail], [user_phone], [payment], [message], [amount], [status], [admin_id], [user_id]) VALUES (319301, CAST(N'2024-12-14' AS Date), N'97 Man Thien Street, Thu Duc City', NULL, N'user33', N'phanthulinhs@gmail.com', N'0972968122', N'Thanh toán khi nhận hàng', N'kj', 2868813, 2, 3, 21)
SET IDENTITY_INSERT [dbo].[order] OFF
GO
SET IDENTITY_INSERT [dbo].[order_status] ON 

INSERT [dbo].[order_status] ([id], [name]) VALUES (0, N'Đang chờ duyệt')
INSERT [dbo].[order_status] ([id], [name]) VALUES (1, N'Đang giao')
INSERT [dbo].[order_status] ([id], [name]) VALUES (2, N'Đã hoàn thành')
INSERT [dbo].[order_status] ([id], [name]) VALUES (3, N'Đã hủy')
SET IDENTITY_INSERT [dbo].[order_status] OFF
GO
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3301, 2, 12600, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3301, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3301, 4, 40500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3301, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3301, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3401, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3501, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3601, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3601, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3601, 8, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3701, 7, 72000, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3701, 40, 11400, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3801, 8, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (3901, 23, 28500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (4001, 30, 90000, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (4101, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (4101, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (8401, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (8401, 40, 11400, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (8501, 2, 12600, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (8601, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (8601, 40, 11400, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (108601, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (108601, 14, 360000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109401, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109401, 8, 90000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109401, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109401, 39, 50000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109401, 43, 42500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 4, 40500, 2)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 24, 33250, 2)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109501, 47, 1231313, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109601, 1, 15000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109601, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109601, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109601, 41, 150000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109701, 17, 31500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109701, 18, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109701, 21, 90000, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109701, 23, 28500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109701, 45, 85000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109801, 19, 22500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109801, 45, 85000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109901, 22, 23750, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109901, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (109901, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110001, 21, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110001, 24, 33250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110001, 29, 100000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110101, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110101, 47, 1231313, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110201, 26, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110201, 27, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110301, 26, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110301, 28, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110401, 27, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110401, 38, 237500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110401, 40, 11400, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110501, 42, 76000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110601, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110601, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110601, 28, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110701, 29, 100000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110801, 38, 237500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110901, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110901, 4, 40500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (110901, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111001, 8, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111001, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111001, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111101, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111101, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111101, 33, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111101, 41, 150000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111201, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111201, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111201, 35, 135000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111301, 1, 15000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111301, 41, 150000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111401, 13, 160000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111401, 16, 135000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111501, 9, 71250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111501, 32, 180000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111501, 44, 280000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111601, 35, 135000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111701, 23, 28500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111801, 9, 71250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111801, 10, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111901, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111901, 8, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111901, 9, 71250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (111901, 41, 150000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112001, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112001, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112001, 36, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112101, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112101, 40, 11400, 1)
GO
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112201, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112201, 36, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112301, 2, 12600, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112401, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112501, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112501, 24, 33250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112501, 37, 500000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112601, 18, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112601, 21, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112601, 47, 1231313, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112701, 45, 85000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112801, 17, 31500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112801, 18, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112901, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (112901, 24, 33250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113001, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113001, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113101, 1, 15000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113101, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113201, 43, 42500, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113301, 1, 15000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113301, 42, 76000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113401, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113401, 14, 360000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113501, 4, 40500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113501, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113501, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113501, 36, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113501, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113601, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113701, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113701, 41, 150000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113701, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113801, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113801, 45, 85000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113801, 47, 1231313, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113901, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (113901, 42, 76000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114001, 37, 500000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114101, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114101, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114201, 21, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114301, 9, 71250, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114301, 11, 200000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114401, 13, 160000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114501, 32, 180000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114501, 35, 135000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114601, 16, 135000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114601, 40, 11400, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114701, 21, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114701, 45, 85000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114801, 27, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114801, 30, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114901, 14, 360000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (114901, 15, 125000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115001, 25, 45000, 10)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115101, 27, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115101, 40, 11400, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115201, 28, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115301, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115401, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115501, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115701, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115801, 39, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (115901, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116001, 6, 108000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116101, 4, 40500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116201, 33, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116301, 5, 49500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116301, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116401, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116501, 15, 125000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116601, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116701, 43, 42500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116801, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (116901, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (117001, 1, 15000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (117101, 8, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (217001, 40, 11400, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (217001, 47, 1231313, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317001, 21, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317001, 22, 23750, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317001, 37, 500000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317101, 14, 360000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317201, 5, 55000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317201, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317201, 19, 22500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317301, 3, 50000, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317301, 23, 28500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317401, 6, 108000, 53)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317601, 31, 10000, 70)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (317901, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318001, 5, 55000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318101, 34, 45000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318201, 13, 160000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318301, 19, 22500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318401, 7, 72000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318501, 19, 22500, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318501, 20, 34000, 10)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318601, 2, 12600, 3)
GO
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318601, 10, 45000, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318601, 14, 360000, 4)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318701, 26, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318701, 37, 500000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318801, 7, 72000, 7)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318801, 33, 90000, 7)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318901, 3, 50000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (318901, 7, 72000, 2)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319001, 9, 71250, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319101, 2, 12600, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319101, 3, 50000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319201, 1, 15000, 5)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319301, 26, 47500, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319301, 30, 90000, 1)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319301, 37, 500000, 3)
INSERT [dbo].[ordered] ([order_id], [product_id], [price], [qty]) VALUES (319301, 47, 1231313, 1)
GO
SET IDENTITY_INSERT [dbo].[producer] ON 

INSERT [dbo].[producer] ([id], [name], [address], [numphone], [email]) VALUES (1, N'Trại rau sạch Biên Hòa', N'Đồng Nai', N'11111111  ', N'BHFarm@gmail.com')
INSERT [dbo].[producer] ([id], [name], [address], [numphone], [email]) VALUES (2, N'Tây Nguyên Farm', N'Đak lak', N'123123123 ', N'TN@gmail.com')
INSERT [dbo].[producer] ([id], [name], [address], [numphone], [email]) VALUES (3, N'Xí nghiệp chế biến nông sản Phước Hưng', N'Bà Rịa-Vũng Tàu', N'3443423   ', N'PH@gmaim.com')
SET IDENTITY_INSERT [dbo].[producer] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (1, 1, N'Rau Cải', 50, 15000, 1, N'Đây là rau sạch', N'Rau cải là loại rau không mấy xa lạ với người Việt ta. Bởi nó được dùng để chế biến thành các món ăn rất ngon và hợp khẩu vị. Nhưng nó ít được biết đến là vị thuốc chữa được rất nhiều loại bệnh. Ngoài cái tên cải bó xôi, nó còn có nhiều tên khác như: rau chân vịt, rau bắp xôi, rau nhà chùa, cải bina… Đây là loài cây thuộc họ nhà Dền và có xuất xứ từ miền Trung và Tây Nam Á.', 0, N'rau_cai.jpg', CAST(N'2020-05-22' AS Date), 3, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (2, 1, N'Rau Muống', 60, 14000, 1, N'Đây là rau sạch', N'Rau muống là loại rau có giá rất rẻ so với các loại rau khác nhưng lại đem lại lượng khoáng chất và vitamin dồi dào như protein, sắt, canxin, chất xơ, vitamin A... Những chất này là những dưỡng chất cần thiết cho cơ thể.', 10, N'rau_muong.jpg', CAST(N'2020-05-22' AS Date), 3, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (3, 1, N'Đậu Nhật', 29, 50000, 1, N'Đậu Nhật', N'Ðậu nành Nhật hay còn gọi là Đậu nành rau hay Đậu nành lông có nguồn gốc từ Nhật Bản, rất ngon và có giá trị dinh dưỡng cao, là loại thực phẩm rất tốt cho sức khỏe và bổ dưỡng vì hàm lượng của nó giàu chất khoáng, vitamin, protein, chất béo, chất xơ, rất tốt cho đường ruột, làm mịn da mặt và còn có tác dụng ngăn ngừa một số bệnh về ung thư', 0, N'dau_nhat.jpg', CAST(N'2020-05-22' AS Date), 7, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (4, 1, N'Hành baro', 10, 45000, 1, N'Hành Baro', N'Hành Baro có tên gọi khác là tỏi tây. Đây là một trong những nguyên liệu không thể thiếu trong rất nhiều món ăn, tạo mùi vị hấp dẫn, khó quên. Không những thế, tỏi tây còn là nguồn dinh dưỡng dồi dào đối với cơ thể. Để có thêm nhiều kiến thức về Hành Baro đừng bỏ lỡ bài viết này nhé.', 10, N'hanh_baro.jpg', CAST(N'2020-05-22' AS Date), 3, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (5, 1, N'Rau Cần Tây', 100, 55000, 1, N'Rau Cần Tây tươi', N'Rau cần tây là một trong những loại rau được đánh giá là “ siêu thực phẩm” chứa hàm lượng dinh dưỡng cao, và ăn cực kỳ ngon nhất là khi kết hợp mùi thơm đặc biệt của cần tây xào với thịt bò…Ngoài sử dụng ở dạng tươi sống khi chế biến món ăn thì bạn cũng có thể làm nước ép cần tây tác dụng giảm cân, thanh nhiệt giải độc cơ thể rất tốt.', 0, N'rau_can_tay.jpg', CAST(N'2020-05-22' AS Date), 3, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (6, 1, N'Rau mùi - Ngò', 10, 120000, 1, N'Rau mùi - Ngò', N'Rau mùi hay còn được gọi bằng các tên gọi khác như: rau ngò, ngò rí, mùi ta, ngổ, mùi tui, nguyên tuy, hồ tuy, hương tuy, ngổ thơm,....Rau mùi là một loại cây thân thảo, loại rau này có nguồn gốc từ các nước Tây Nam Á và Châu Phi. ', 10, N'rau_mui.jpg', CAST(N'2020-05-22' AS Date), 4, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (7, 1, N'Xà lách Mỹ - Cuộn Xanh', 100, 80000, 1, N'Xà lách', N'Xà lách Mỹ có nguồn gốc từ vùng nhiệt đới và đến ngày nay nó đã  trở thành cây của toàn thế giới. Ở nước ta, Rau xà lách ưa khí hậu mát, đặc biệt là khí hậu ở Đà Lạt ', 10, N'xa_lach.jpg', CAST(N'2020-05-22' AS Date), 3, N'cây 150g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (8, 1, N'Súp Lơ Tím', 10, 100000, 1, N'Súp Lơ', N'Súp lơ tím có tên gọi khác là bông cải tím. Đây là một trong những loại rau trồng được các bà nội trợ ưa chuộng với giá trị dinh dưỡng cao. Đó cũng là lý do giá súp lơ tím cao hơn so với các loại rau thông thường.', 10, N'sup_lo.jpg', CAST(N'2020-05-22' AS Date), 5, N'cây 150g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (9, 2, N'Hạt Sen', 100, 75000, 1, N'Hạt Sen', N'Hạt sen sấy là một loại hạt dinh dưỡng, mang lại rất nhiều lợi ích đối với sức khỏe người sử dụng. Từ xưa, cây sen đã là một loài cây trồng quen thuộc và là biểu tượng của người dân Việt Nam. Hạt sen sấy là sản phẩm của hạt sen tươi khi đã chín được người nông dân thu về và chế biến, bảo quản. Hạt sen sấy giòn (hạt sen sấy khô) sẽ bảo quản được thời gian lâu hơn so với hạt sen tươi. Hạt sen sấy có rất nhiều công dụng khác nhau. Ngoài sử dụng như một vị thuốc không thể thiếu trong Đông Y, sen sấy khô còn là nguyên liệu để chế biến rất nhiều món ăn thơm ngon, bổ dưỡng chẳng hạn như: chè hạt sen long nhãn, chè hạt sen nấm tuyết, cháo hạt sen, hạt sen sấy giòn…', 5, N'hat_sen.jpg', CAST(N'2017-01-01' AS Date), 120, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (10, 2, N'Hạt Ươi', 100, 45000, 1, N'Hạt Ươi', N'Hạt Ươi (Sterculia lychonophora Hnce) còn có tên gọi khác là hạt ươi bay, hạt đười ươi, an nam tứ, đại đồng quả, pang da hai,...Đây là một trong những loại hạt được pha làm nước uống bổ dưỡng vào mùa hè, xua tan đi cái nóng và mệt mỏi. Vòng tuần hoàn ra quả của cây ươi đó là 4 năm một lần. Cây ươi sinh trường, phát triển ở trong rừng thường có mặt ở các nước thuộc khu vực Đông Nam Á như: Thái Lan, Việt Nam, Lào hay Malaysia. Tại Việt Nam, cây ươi mọc ở Tây Nguyên và vùng Trung Trung Bộ. Hạt ươi rừng sau khi thu hoạch sẽ được sàng lọc thêm một lần nữa để chọn ra những hạt chất lượng nhất. Sau đó sơ chế để bán lẻ và xuất khẩu đi một số nước. ', 0, N'hat_uoi.jpg', CAST(N'2020-05-22' AS Date), 120, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (11, 2, N'Hạt Phỉ', 100, 250000, 1, N'Hạt Phỉ', N'Hạt phỉ còn có tên gọi khác là Hazelnut, tên thực vật của nó là Corylus avellana. Hạt phỉ được trồng nhiều ở nhiều quốc gia như: Hy Lạp, Georgia, Thổ Nhĩ Kì, Italia, Anh, phía nam Tây Ban Nha và 2 bang Oregon  và Washington  của Mỹ. Thổ Nhĩ Kì là nơi cung cấp nguồn hạt phỉ lớn nhất trên thế giới, chiếm tới 75% sản lượng toàn thế giới. Hạt phỉ ngon, nhiều chất dinh dưỡng rất tốt cho sức khỏe. Không chỉ thế, mùi vị của hạt phỉ thơm ngon, bùi béo nên nó thường được dùng để nấu ăn, trong các món bánh.', 20, N'hat_phi.jpg', CAST(N'2020-05-22' AS Date), 120, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (12, 2, N'Quả Hạch', 198, 300000, 1, N'Quả Hạch', N'Quả hạch khô còn có tên gọi khác là hạt bào ngư, hạt móng ngựa, hình dáng của quả hạch bên ngoài xù xì, có lớp vỏ cứng màu nâu sẫm. Quả hạch có phần nhân bên trong màu trắng, nhận được bao bọc 1 lớp màn mỏng nâu. Phần nhân béo gùi, ăn rất thơm. Quả hạch được chọn to đèu, không bị dập nát, bị thối, sau đó mang đi sấy khô với công nghệ hiện đại. Quả hạch sẽ được tách lớp vỏ cứng bên ngoài rồi  mới đóng gói trong các bao bì an toàn, hợp vệ sinh. Hạt quả hạch đóng gói tiện lợi, dễ sử dụng, bạn chỉ cần bảo quản ở nơi thoáng mát, khô ráo và dùng mỗi ngày, không nhất thiết phải để trong tủ lạnh.', 0, N'hach_kho.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (13, 2, N'Ô Mai Khô ', 97, 200000, 1, N'Ô Mai', N'Ô Mai còn có tên gọi khác là mơ đen, từ xưa ô mai là tên một vị thuốc làm từ quả mơ phơi khô cho đên khi đen và quắt lại. Trong y học cổ truyền, ô mai là vị thuộc rất phổ biến ở các nước châu á như Việt Nam, Trung Quốc. Ngày nay thì ô mai thường được dùng như một loại đồ ăn vặt được làm từ các loại quả khác nhau như: mận, me, sấu mơ, sử dụng ăn vặt nhiều hơn là làm thuốc. Những loại quả để làm mở cần phải được chọn lựa kỹ càng và chế biến với nhiều hương liệu, nguyên liệu khác nhau thì mới làm ra được món ô mai ngon nhất.', 20, N'o_mai.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (14, 2, N'Sa Nhân', 88, 450000, 1, N'Sa Nhân', N'Sa Nhân còn có tên gọi khác trong tiếng Tày là mác nẻng, trong tiếng là co nénh, sa nhân là loại cây mọc hoang rất nhiều ở các vùng rừng núi, dưới tán cây sa nhân râm mát. Bọ phận thường dùng làm thuốc của sa nhân là hạt quả. Quả thường được hát vào mùa hè, bóc vỏ rồi lấy hạt ở phía trong, sây khô sử dụng dần.', 20, N'sa_nhan.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (15, 2, N'Thảo Quả', 98, 125000, 1, N'Thảo Quả', N'Tên tiếng Việt khác: Thảo quả đỏ / Thảo quả đen Tên danh pháp hai phần: Amomum tsao-ko Thuộc họ: Gừng(Zingiberaceae) Hoa thảo quả thường nở vào mùa hè ( khoảng từ tháng 5 đến tháng 7) và ra quả vào mùa đông Ở Việt Nam, thảo quả mọc chủ yếu ở vùng Tây Bắc và dãy núi Hoàng Liên Sơn, nó mọc nhiều nhất ở các tỉnh Lào Cai, Yên Bái, Hà Giang, Lai Châu Thảo quả là một loại cây thảo mộc có vị cay nóng, mùi thơm đặc trưng, chúng thường được sử dụng trong ẩm thực và làm thuốc chữa bệnh. Nó còn được mệnh danh là “nữ hoàng gia vị” trong chế biến các món ăn ngon và nổi tiếng của Việt Nam.', 0, N'thao_qua.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (16, 2, N'Hạt Điều', 98, 150000, 1, N'Hạt Điều', N'Điều hay còn gọi là đào lộn hột, là một cây công nghiệp thuộc họ xoài. Nó được trồng ở khí hậu nhiệt đới để lấy nhân hạt chế biến làm thực phẩm.Hạt điều ăn rất giòn và có hương thơm đặc trưng. Thường mọi người hay tìm mua hạt điều rang muối. Đây là đặc sản của Việt Nam nên thị trường hạt điều xuất khẩu luôn dẫn đầu cao.', 10, N'hat_dieu.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (17, 3, N'Xoài Tượng Bình Định', 100, 35000, 1, N'Xoài Tượng Bình Định ', N'Ở Bình Định nổi tiếng với nhiều loại xoài như: Xoài tượng, xoài thanh ca, xoài mật, xoài tro, xoài xẻ...vv. Mỗi loại xoài đều có những hương vị độc đáo riêng. Trong đó ngon nhất và ấn tượng nhất thì phải kể đến xoài tượng. Ngoài vị thơm, ngọt thì xoài tượng có cho giá trị kinh tế cao nhất và được thị trường rất ưa chuộng. Xoài Tượng được trồng ở rất nhiều nơi nhưng nhắc đến xoài tượng là người ta nghĩ ngay đến Đại An (Bình Định) vì nơi đây trồng xoài tượng nhiều nhất và ngon nhất hiện nay.', 10, N'xoai_tuong.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (18, 3, N'Vú Sữa Lò Rèn', 0, 50000, 1, N'Vũ Sữa Lò Rèn', N'Vú sữa Lò rèn Vĩnh Kim là một loại trái cây ngon mọi người dân Nam Bộ và quốc gia đều biết đến và hiện nay được trồng ở quy mô công nghiệp, nhưng giống vú sữa Lò rèn Vĩnh Kim gắn với vùng đất đặc biệt này vẫn được người dân gìn giữ, đặc biệt cây vú sữa Lò rèn Vĩnh Kim được trồng từ hạt của cây vú sữa đầu tiên. Thịt quả có màu trắng đục, mềm, nước dạng sữa, dày thịt, tỷ lệ thịt quả cao, ít hạt, có mùi vị rất ngọt, béo, mùi thơm dịu đặc trưng. ngon. Vú sữa lò rèn vĩnh kim có vị ngọt, được ăn tươi hoặc làm lạnh. Đây là loại quả được người Việt Nam ưa chuộng và sử dụng từ hàng trăm năm trước.', 0, N'vu_sua.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (19, 3, N'Hồng Xiêm', 0, 25000, 1, N'Hồng Xiêm', N'Quả Hồng Xiêm là tên gọi ở miền Bắc, bà con miền Nam gọi là trái sapoche, đây là loại quả quí, lành tính dùng được cho người khỏe mạnh và cả người ốm. Để chất lượng quả thơm, ngọt lúc chín như ý cần lấy được quả già và biết cách dấm vừa độ chín.', 10, N'hong_xiem.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (20, 3, N'Nhãn Lồng Hưng Yên ', 100, 40000, 1, N'Nhãn Lồng Hưng Yên', N'Nhãn có tên khoa học là Dimocarpus longan, tiếng hán việt gọi là “long nhãn”. Đây là một loại trái cây điển hình của vùng nhiệt đới thuộc thân gỗ, sống lâu năm. Theo tổng hợp từ các địa phương, hiện nay Hưng Yên là nơi có diện tích trồng lớn nhất và cho chất lượng nhãn ngon nhất, hiện nay Hưng Yên có gần 4 nghìn ha nhãn, trong đó, diện tích cho thu hoạch khoảng 3 nghìn ha.', 15, N'nhan_long.jpg', CAST(N'2020-05-22' AS Date), 5, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (21, 3, N'Quả Thanh Mai', 50, 90000, 1, N'Quả Thanh Mai', N'Quả thanh mai có cân bằng axit - đường tốt và là nguồn cung cấp thiamine, riboflavin, carotene, khoáng chất, chất xơ và hàm lượng vitamin rất cao. Chúng cũng là một nguồn tốt cung cấp chất chống oxy hóa tương tự (ví dụ anthocyanin) có màu đỏ rượu vang lợi ích sức khỏe. Ngoài anthocyanin, bayberry còn chứa flavonol, ellagitannin và các hợp chất phenolic như axit gallic, quercetin hexoside,...', 0, N'thanh_mai.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (22, 3, N'Cam Sành', 100, 25000, 1, N'Cam Sành', N'Cam sành là một giống cây ăn quả thuộc chi Cam chanh phân bố rộng khắp Việt Nam từ Tuyên Quang, Hà Giang, Yên Bái tới Vĩnh Long, Tiền Giang, Cần Thơ. Nhưng nhìn chung cam sành thích hợp với vùng đất phù sa cổ màu mỡ,khí hậu mát ẩm. Cam sành vietgap đang là tiêu chuẩn trồng và chăm sóc chính của bà con nơi đây', 5, N'cam_sanh.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (23, 3, N'Na Lạng Sơn ', 100, 30000, 1, N'Na Lạng Sơn', N'Na, hay còn gọi là mãng cầu ta, mãng cầu dai/giai, sa lê, phan lệ chi, là một loài thuộc chi Na (Annona) có nguồn gốc ở vùng châu Mỹ nhiệt đới. Quả na là một trong những trái cây nhiệt đới ngon nhất có nguồn gốc từ các thung lũng Andean của Peru và Ecuador. Ngày nay, loại quả vừa ngon vừa tốt cho sức khỏe này đã được trồng phổ biến ở khắp thể giới.', 5, N'na.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (24, 3, N'Thanh Long', 0, 35000, 1, N'Thanh Long', N'Thanh long ruột trắng thuộc chi Hylocereus tên khoa học là Hylocereus undatus. Vỏ quả có màu hồng hoặc đỏ. Người ta gọi là thanh long ruột trắng vì ở Phan Thiết- Bình Thuận có còn một loại thanh long nữa là thanh long ruột đỏ', 5, N'thanh_long.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (25, 4, N'Tinh Dầu Nghệ', 90, 50000, 1, N'Tinh Dầu Nghệ', N'Là một sản phẩm được chiết xuất từ củ nghệ, sản xuất bằng phương pháp chưng cất hơi nước, tinh dầu nghệ có dạng nước, màu vàng và mùi nghệ, hăng cay đặc trưng. Trong tinh dầu nghệ có chứa hơn 300 hợp chất phenol, trong đó nổi bật là hoạt chất Curcumin bao gồm các phân tử có khả năng tăng cường lượng oxy cung cấp cho não. Ngoài ra, tinh dầu nghệ cũng chứa các chất kháng khuẩn, chống nấm, kháng virut, chống giun, chống dị ứng và chống ký sinh trùng đem đến những công dụng tuyệt vời cho người dùng không chỉ về sức khỏe mà còn trong việc làm đẹp của chị em phụ nữ.', 10, N'tinh_dau_nghe.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (26, 4, N'Tinh Dầu Hoa Hồng', 96, 50000, 1, N'Tinh Dầu Hoa Hồng', N'Tinh dầu hoa hồng là sản phẩm được chiết xuất từ cánh hoa hồng bằng phương pháp trưng cất hơi nước. Với mùi hương quyến rũ cùng nhiều dưỡng chất đặc biệt, tinh dầu hoa hồng được coi là nguồn cảm hứng bất tận của các nhà nghiên cứu và luôn có một chỗ đứng vững chắc trong nền công nghiệp mỹ phẩm. Không chỉ vậy, trong tinh dầu hoa hồng còn chứa rất nhiều những dưỡng chất có lợi cho sức khỏe như:  Ethanol, Stearpoten, Citronellyl Acetate, Eugenol, Nonanol, Nonanal, Citronellol, Citral, Carvone, Nerol, Phenyl Acetaldehyd, Farnesol, Phenylmenthyl Acetate, Methyl Eugenol…', 5, N'tinh_dau_hoa_hong.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (27, 4, N'Tinh Dầu Dừa', 96, 50000, 1, N'Tinh Dầu Dừa', N'Đây là thành phẩm được làm, chiết xuất từ lượng dưỡng chất có trong quả dừa già. Không giống như những sản phẩm dầu dừa ép nóng bằng phương pháp thủ công thông thường, dầu dừa ép lạnh được sản xuất trên dây truyền sản xuất hiện đại nhờ công nghệ sấy lạnh, tách thành nhiều khâu khác nhau, đòi hỏi sự tỉ mỉ và kiên nhẫn. Dầu dừa ép lạnh luôn đảm bảo giữ được những thành phần dưỡng chất tốt, quan trọng, cần thiết nhất, đem lại hiệu quả cao cho người sử dụng. Bên cạnh đó, dầu dừa ép lạnh 100% tinh chất, không chứa nước hay bất kỳ tạp chất giúp bạn có thể bảo quản và sử dụng trong thời gian dài hơn so với các sản phẩm được làm từ phương pháp truyền thống.', 5, N'tinh_dau_dua.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (28, 4, N'Tinh Dầu Gừng', 97, 50000, 1, N'Tinh Dầu Gừng', N'Tinh dầu gừng là thành phẩm được làm từ củ gừng, do vậy nó chứa các thành phần dưỡng chất có trong củ gừng và các đặc tính có lợi chẳng hạn như: nhuận tràng, khử trùng, cung cấp các loại vitamin,...Tinh dầu gừng được sử dụng trong việc bảo vệ duy trì tốt sức khỏe như: trị viêm họng, buồn nôn, đạu bụng, điều trị một số bệnh ảnh hưởng đến đường hô hấp, rong kinh, rối loạn kinh nguyệt,...Không những thế, tinh dầu gừng được sử dụng như một loại nước hoa để tạo mùi thơm, khiến chúng ta tự tin hơn.', 5, N'tinh_dau_gung.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (29, 4, N'Mật Ong Rừng', 98, 100000, 1, N'Mật ong rừng', N'Mật ong rừng ở Hà Giang không hề có sự tác động của con người và hoàn toàn tự nhiên, mật ong rừng thường được thu hoạch duy nhất trong 1 mùa tầmk hoảng từ tháng 3 tới tháng 6 mỗi năm, mật ong rừng mang đậm vị đặc trưng của các bông hoa rừng, có vị ngọt và thanh. Mật ong rừng nguyên chất có màu rất đa dạng, từ nâu sẫm tới vàng sẫm, độ kết dính tương đối cao.', 0, N'mat_ong.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (30, 4, N'Mật Ong Bạc Hà', 94, 100000, 1, N'Mật ong bạc hà', N'Mật ong hoa bạc hà có nguồn gốc từ khu vực Đồng Văn - Hà Giang, là một loài hoa mọc dại. Hoa bạc hà nở nhiều nhất vào tầm tháng 10 tới tháng 1. Dó đó mà mật ong bạc hà trên thị trường tương đối khan hiếm bởi tác dụng đặc biệt cho sức khỏe con người.', 10, N'mat_ong_bac_ha.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (31, 3, N'Kiwi xanh', 0, 10000, 1, N'Kiwi xanh', N'Kiwi xanh chứa nhiều vitaminC, kali, acid folic và chất xơ. Giúp bồi dưỡng sức khỏe cho trẻ em và phụ nữ sau khi sinh. VitaminC trong kiwi xanh nhiều gấp đôi so với trái cam (cam sành, cam canh, cam cao phong, ... ). Chúng giúp cải thiện chức năng của hệ miễn dịch, phòng ngừa những tác động của chứng viêm gan và sự tấn công của virus và vi khuẩn, nâng cao sự miễn dịch, chống lại bệnh liệt dương. Ăn Kiwi xanh sẽ ngăn chặn việc tạo thành chất gelatin (chất keo) hoặc đông cục khi tiêu thụ các loại thực phẩm được chế biến từ sữa nhờ vào thành phần actindin.', 0, N'kiwi.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (32, 2, N'Hạnh nhân', 98, 200000, 1, N'Hạnh nhân', N'Hạt hạnh nhân thuộc phân chi Amygdalus, tên gọi khác của nó là hạnh đào. Lớp vỏ của hạnh nhân rất cứng (vỏ phía trong), nếp nhăn quả hạnh nhân lượn sóng, bao quanh hạt giống. Hạnh nhân không phải là loại quả kiên, đây là 1 loại quả hạch. Bạn có thể rang hoặc ăn sống, hạnh nhân được dùng trong khá nhiều món ăn và cũng là loại hạt nằm trong bộ hạt dinh dưỡng đối với bà bầu.Có 2 loại hạnh nhân hiện nay đó là: hạnh nhân ngọt và hạnh nhân đắng. Hạnh nhân đắng có vị đắng, tính ấm, hơi độc.', 10, N'hanh_nhan.jpg', CAST(N'2020-05-22' AS Date), 120, N'túi 200g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (33, 1, N'Súp lơ xanh', 0, 90000, 1, N'Súp lơ xanh', N'Súp lơ xanh còn gọi là bông cải xanh, nó thuộc loài cải bắp dại, có hoa lớn ở đầu và được sử dụng làm rau. Nó rất tốt cho sức khỏe, chúng chứa nhiều vitamin A, C, và E hơn các rau củ xanh khác, giúp bổ sung dinh dưỡng và tăng cường đề kháng cho cơ thể.Các nhà nghiên cứu khoa học đã chỉ ra rằng, súp lơ xanh giúp làm giảm nguy cơ mắc bệnh ung thư và nhiều loại bệnh tật khác.', 0, N'sup_lo_xanh.jpg', CAST(N'2020-05-22' AS Date), 5, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (34, 1, N'Bí Ngô', 0, 50000, 1, N'Bí Ngô', N'Bí Ngô (Cucurbita pepo) còn có tên gọi khác là bí rợ, bí đỏ thuộc họ nhà bầu bí. Đây là một thực phẩm rất tốt, có công dụng cực kỳ hữu ích. Hầu hết tất cả các bộ phận của cây bí đỏ đều có thể sử dụng được từ hoa bí đỏ, ngọn bí đỏ cho tới quả bí đỏ và hạt bí đỏ.', 10, N'bi_do.jpg', CAST(N'2020-05-22' AS Date), 7, N'1         ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (35, 2, N'Hạt Điều Rang Muối', 97, 150000, 1, N'Hạt Điều Rang Muối', N'Điều hay còn gọi là đào lộn hột, là một cây công nghiệp thuộc họ xoài. Nó được trồng ở khí hậu nhiệt đới để lấy nhân hạt chế biến làm thực phẩm.Hạt điều ăn rất giòn và có hương thơm đặc trưng. Thường mọi người hay tìm mua hạt điều rang muối. Đây là đặc sản của Việt Nam nên thị trường hạt điều xuất khẩu luôn dẫn đầu cao.', 10, N'hatdieu.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 300g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (36, 1, N'Xà lách đăm', 0, 50000, 1, N'Xà Lách', N'Xà lách đăm còn gọi là cải bèo, rau diếp. Tên khoa học là Lactuca sativa, là loại thực vật có hoa thuộc họ Cúc, có nguồn gốc khá lâu đời từ các nước Châu Âu, được mô tả khoa học lần đầu vào năm 1753.Tên của rau xà lách đăm bắt nguồn từ việc từ thân cây rau sau khi bị cắt sẽ chảy ra một loại nước trắng đục như cao su. Xà lách đăm là một loại rau ăn lá, có tính giải khát vì sự tinh khiết và tính an thần của nó.', 5, N'salach.jpg', CAST(N'2020-05-22' AS Date), 6, N'bó 200g   ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (37, 4, N'Mật ong ', 93, 500000, 1, N'Mật ong', N'Mật ong rừng ở Hà Giang không hề có sự tác động của con người và hoàn toàn tự nhiên, mật ong rừng thường được thu hoạch duy nhất trong 1 mùa tầmk hoảng từ tháng 3 tới tháng 6 mỗi năm, mật ong rừng mang đậm vị đặc trưng của các bông hoa rừng, có vị ngọt và thanh. Mật ong rừng nguyên chất có màu rất đa dạng, từ nâu sẫm tới vàng sẫm, độ kết dính tương đối cao.', 0, N'matong.jpg', CAST(N'2020-05-22' AS Date), 730, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (38, 4, N'Mật ong gừng', 98, 250000, 1, N'Mật ong gừng', N'Mật ong gừng là thực phẩm rất quen thuốc với tất cả mọi người, mật ong gừng được dùng rất nhiều trong cuộc sống hàng ngàu. Khi kết hợp mật ong và gừng sẽ đem lại tác dụng gì tới sức khỏe con người. Rất nhiều nghiên cứu để chỉ ra rằng mật ong gừng có tác dụng rất tốt trong việc điều trị bệnh, tăng cường sức khỏe.', 5, N'mat_ong_gung.jpg', CAST(N'2020-05-22' AS Date), 365, N'hũ 150ml  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (39, 1, N'Cà chua bi', 0, 50000, 1, N'Cà chua bi', N'Cà chua bi, ngoài ra loại quả này còn được gọi với cái tên khá mỹ miều đó là Cherry Tomato . Đây là loại cà chua trái nhỏ quả hình tròn hoặc dài, màu đỏ, quả đều nhìn rất đẹp. Cà chua bi tuy quả nhỏ, ngọt hơn cà chua thông thường. Cà chua bi rât dễ trồng, quả rất sai, có thể trồng được quanh năm, sai quả, giá thành gấp 2 – 3 lần so với loại cà chua thông thường.', 0, N'ca_chua.jpg', CAST(N'2020-05-22' AS Date), 4, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (40, 1, N'Bông Atiso tươi', 0, 12000, 1, N'Bông Atiso', N'Bông Atiso là một trong những đặc sản nổi tiếng và đặc trưng nhất của Đà Lạt. Trong hoa atiso chứa hàm lượng dinh dưỡng cao có nhiều công dụng cho sức khỏe người sử dụng. ', 5, N'hoa_atiso.jpg', CAST(N'2020-05-22' AS Date), 120, N'1         ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (41, 1, N'Măng tây', 0, 150000, 1, N'Măng tây', N'Măng tây không còn xa lạ gì với người Việt Nam, hành tía là loại thực phẩm không thể thiếu trong bữa cơm gia đình Việt. Hành tía giúp cho hương vị của món ăn trở nên hấp dẫn hơn nhiều. Không chỉ là một loại thực phẩm làm cho món ăn trở nên hấp dẫn hơn, hành tía còn mang đến cho chúng ta nhiều chất dinh dưỡng tốt cho sức khỏe.', 0, N'mangtay.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (42, 1, N'Dưa lưới', 0, 80000, 0, N'Dưa lưới', N'Dưa lưới là một loại hoa quả sạch có hình tròn, khá to, có các đường gân trắng khá độc đáo. Ngoài ra, hương vị của nó cũng rất đặc biệt, ăn giòn và vị ngọt mát. Đây là loại trái cây giúp bổ sung nhiều dinh dưỡng cho cơ thể và thường được sử dụng để giải nhiệt . Nó gồm có 2 loại là dưa lưới vàng và dưa lưới xanh. ', 5, N'dua_luoi.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (43, 1, N'Dưa leo', 0, 50000, 1, N'Dưa leo', N'Dưa leo (dưa chuột) là thực phẩm rất quen thuộc hàng ngày, đặc biệt là những công dụng của dưa chuột đối với sức khỏe. Dưa chuột chứa hàm lượng nước cao (trong quả dưa chuột chứa tới 95% lượng nước), giàu vitamin C và chất xơ giúp làn da trắng sáng, khỏe khoắn. Hơn nữa, dưa chuột còn có tác dụng làm giảm nhiệt và kháng viêm. Dưa chuột là món ăn nhẹ rất tốt trong mùa hè. ', 15, N'dua_chuot.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (44, 2, N'Hạt mắc ca', 99, 350000, 1, N'Hạt mắc ca', N'Hạt mắc ca khi so với nhiều loại hạt dinh dưỡng khác như hạnh nhân, óc chó, hạt thông.. thì trong mắc ca chứa nhiều chất béo nhưng ít hàm lượng protein. Lượng chất béo không bão hòa đơn trong hạt mắc ca là cao nhất so với các loại hạt dinh dưỡng khác. Axit béo omega 7 có khoảng 22% trong hạt mắc ca hoạt động sinh học giống như 1 chất béo chưa bão hòa đơn. Ngoài ra, trong hạt mắc ca có chứa 2% sơ dinh dưỡng, 9% cacbohydrat, 9% protein cũng như các chất selen, photpho, natri, kali, riboflavin, thiamin, sắt và niacin.', 20, N'hat_mac_ca.jpg', CAST(N'2020-05-22' AS Date), 365, N'túi 400g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (45, 3, N'Táo xanh Pháp', 0, 85000, 1, N'Táo xanh Pháp', N'Từ lâu, táo xanh được công nhận là loại trái cây lành mạnh, đem lại nhiều lợi ích cho sức khỏe.Với thương hiệu Pháp, Táo xanh Pháp được nhiều người dân Việt ưa chuộng và lựa chọn tin cậy. Mùa vụ táo xanh chỉ khoảng 1 đợt trong năm, nhưng giá thành lại không cao.Chất xơ phức tạp của táo giúp no lâu hơn mà không bị tiêu thụ nhiều calo (một quả táo bình thường chỉ chứa khoảng 95 calo). Một loại axit có trong vỏ táo là Axit Ursolic làm giảm nguy cơ béo phì, Axit Ursolic thúc đẩy cơ thể đốt cháy calo, tăng việc hình thành cơ và giảm chất béo lâu năm trong cơ thể.', 0, N'tao_xanh.jpg', CAST(N'2020-05-22' AS Date), 7, N'1 kg      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (47, 3, N'Me chua', 94, 1231313, 1, N'me chua lên men', N'abc', 0, N'mechua.jpg', CAST(N'2024-08-14' AS Date), 120, N'hộp 200g  ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (49, 1, N'rau thử', 0, 23200, 1, N'thử sp', N'thử sp', 0, N'ft.png', CAST(N'2024-11-30' AS Date), 5, N'bó        ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (50, 2, N'test 2', 0, 21200, 1, N'thử 2', N'thử 2', 0, N'raucau.jpg', CAST(N'2024-11-30' AS Date), 7, N'trái      ')
INSERT [dbo].[product] ([id], [catalog_id], [name], [quantity], [price], [status], [description], [content], [discount], [image_link], [created], [expiry_date], [unit]) VALUES (52, 1, N'rau thử2', 0, 21200, 0, N'thử nghiệm', N'2qweqwe', 20, N'ft.png', CAST(N'2024-12-03' AS Date), 21, N'trái      ')
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[review] ON 

INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (1, 3, N'An toàn, sạch sẽ!', CAST(N'2020-10-10' AS Date), 2, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (2, 4, N'Sản phẩm chất lượng', CAST(N'2020-10-11' AS Date), 4, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (3, 5, N'Ngon, lần sau tôi sẽ mua tiếp', CAST(N'2020-10-12' AS Date), 4, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (8, 2, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 4, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (9, 3, N'Đã sử dụng sản phẩm.', NULL, 5, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (10, 4, N'Đã sử dụng sản phẩm.', NULL, 5, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (11, 6, N'Đã sử dụng sản phẩm.', NULL, 4, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (12, 23, N'Đã sử dụng sản phẩm.', NULL, 3, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (13, 30, N'Đã sử dụng sản phẩm.', NULL, 1, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (14, 40, N'Đã sử dụng sản phẩm.', NULL, 1, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (15, 43, N'Đã sử dụng sản phẩm.', NULL, 2, 1)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (16, 17, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (17, 18, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 1, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (18, 19, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (19, 21, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 4, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (20, 22, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (21, 23, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 4, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (22, 24, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 3, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (23, 29, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 1, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (24, 39, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 2, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (25, 43, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 1, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (26, 45, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (27, 47, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 4, 2)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (28, 6, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (29, 7, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 5, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (30, 8, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 4, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (31, 40, N'Đã sử dụng sản phẩm.', CAST(N'2024-10-26' AS Date), 2, 3)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (32, 2, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 4)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (33, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 4)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (35, 6, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 4)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (36, 8, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 4)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (37, 14, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 4)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (38, 1, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (39, 2, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (40, 4, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (41, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (42, 6, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (43, 8, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (44, 22, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (45, 24, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (46, 34, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (47, 39, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (48, 41, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (49, 43, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (50, 47, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 5)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (51, 6, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (52, 22, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (53, 26, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (54, 27, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (55, 28, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (57, 29, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (58, 38, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (59, 40, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (60, 42, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 7)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (61, 1, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (62, 2, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (63, 3, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (64, 4, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (65, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (66, 7, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (67, 8, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (68, 33, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (69, 34, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (70, 35, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (71, 39, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (72, 41, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 10)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (73, 9, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (74, 10, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (75, 13, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (76, 16, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (77, 23, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (78, 32, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (79, 35, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (80, 44, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 11)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (81, 2, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (82, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (83, 7, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (84, 8, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (85, 9, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (86, 22, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (87, 34, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (88, 36, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (89, 40, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (90, 41, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 15)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (91, 17, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (92, 18, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (93, 21, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (94, 22, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (95, 24, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (96, 37, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (97, 45, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (98, 47, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 16)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (99, 1, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (100, 2, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (101, 4, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (102, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (103, 7, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (104, 6, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (105, 14, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 17)
GO
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (106, 34, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (107, 36, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (108, 39, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (109, 41, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (110, 42, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (111, 43, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 17)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (112, 5, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (113, 21, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (114, 22, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (115, 37, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (116, 39, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (117, 42, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (118, 43, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (119, 45, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (120, 47, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 18)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (121, 9, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (122, 11, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (123, 13, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (124, 16, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 3, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (125, 21, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (126, 32, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (127, 35, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (128, 40, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (129, 45, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 19)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (130, 14, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (131, 15, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (132, 25, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 4, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (133, 27, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 1, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (134, 28, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (135, 30, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 2, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (136, 40, N'Đã đánh giá', CAST(N'2024-10-26' AS Date), 5, 20)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (137, 37, N'Đã sử dụng sản phẩm.', CAST(N'2024-12-14' AS Date), 5, 21)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (138, 47, N'Đã sử dụng sản phẩm.', CAST(N'2024-12-14' AS Date), 2, 21)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (139, 26, N'Đã sử dụng sản phẩm.', CAST(N'2024-12-14' AS Date), 5, 21)
INSERT [dbo].[review] ([id], [product_id], [contentReview], [created], [score], [id_user]) VALUES (140, 30, N'Đã sử dụng sản phẩm.', CAST(N'2024-12-14' AS Date), 5, 21)
SET IDENTITY_INSERT [dbo].[review] OFF
GO
SET IDENTITY_INSERT [dbo].[roles] ON 

INSERT [dbo].[roles] ([id], [name], [description]) VALUES (1, N'admin', N'toàn quyên')
INSERT [dbo].[roles] ([id], [name], [description]) VALUES (2, N'customer', N'khách hàng')
INSERT [dbo].[roles] ([id], [name], [description]) VALUES (3, N'staff', N'nhân viên ')
SET IDENTITY_INSERT [dbo].[roles] OFF
GO
SET IDENTITY_INSERT [dbo].[slides] ON 

INSERT [dbo].[slides] ([id], [title], [discount_content], [content_slide], [image_link]) VALUES (1, N'Các Loại Hạt Bổ Dưỡng', N'Giảm giá lên đến 50%', N'Các loại hạt tại của hàng luôn luôn được tuyển chọn,và các mặt hàng luôn đảm bảo chất lượng.', N'slide_1.jpg')
INSERT [dbo].[slides] ([id], [title], [discount_content], [content_slide], [image_link]) VALUES (2, N'Rau Củ Quả Xanh Sạch', N'Giảm giá lên đến 20%', N'Rau củ quả được trồng đảm bảo không thuốc hóa học , đảm bảo chất lương xanh sạch và an toàn.', N'slide_2.jpg')
INSERT [dbo].[slides] ([id], [title], [discount_content], [content_slide], [image_link]) VALUES (3, N'Trái Cây Ngon Ngọt', N'Giảm giá lên đến 33%', N'Trái cây tại cửa hàng đa dạng về loại hàng, luôn đảm bảo tính an toàn và chất lượng lên hàng đầu.', N'slide_3.jpg')
INSERT [dbo].[slides] ([id], [title], [discount_content], [content_slide], [image_link]) VALUES (4, N'Mật Ong Và Tinh Dầu', N'Giảm giá lên đến 25%', N'Là 2 sản phẩm mới bên cửa hàng của chúng tôi,sản phẩm đã được kiểm định
                và cấp phép về an toàn.', N'slide_4.jpg')
SET IDENTITY_INSERT [dbo].[slides] OFF
GO
SET IDENTITY_INSERT [dbo].[supply_Invoice] ON 

INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (100, CAST(N'2024-07-29' AS Date), 1, 1)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (101, CAST(N'2024-07-31' AS Date), 1, 3)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (102, CAST(N'2024-08-08' AS Date), 1, 2)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (119, CAST(N'2024-08-09' AS Date), 1, 3)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (120, CAST(N'2024-08-11' AS Date), 1, 2)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (121, CAST(N'2024-08-14' AS Date), 1, 1)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (122, CAST(N'2024-08-14' AS Date), 1, 2)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (123, CAST(N'2024-08-14' AS Date), 1, 1)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (124, CAST(N'2024-10-17' AS Date), 3, 2)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (1124, CAST(N'2024-11-29' AS Date), 3, 2)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (1125, CAST(N'2024-12-13' AS Date), 3, 3)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (1126, CAST(N'2024-12-13' AS Date), 3, 3)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (1127, CAST(N'2024-12-14' AS Date), 3, 1)
INSERT [dbo].[supply_Invoice] ([id], [supply_time], [ad_id], [producer_id]) VALUES (1128, CAST(N'2024-12-14' AS Date), 3, 2)
SET IDENTITY_INSERT [dbo].[supply_Invoice] OFF
GO
SET IDENTITY_INSERT [dbo].[used] ON 

INSERT [dbo].[used] ([id], [name]) VALUES (1, N'Làm đẹp')
INSERT [dbo].[used] ([id], [name]) VALUES (2, N'Giải nhiệt')
INSERT [dbo].[used] ([id], [name]) VALUES (3, N'Gia vị')
INSERT [dbo].[used] ([id], [name]) VALUES (4, N'Nguyên liệu nấu ăn')
INSERT [dbo].[used] ([id], [name]) VALUES (5, N'Ăn vặt')
INSERT [dbo].[used] ([id], [name]) VALUES (6, N'Hỗ trợ sức khỏe')
INSERT [dbo].[used] ([id], [name]) VALUES (7, N'Hương thơm')
INSERT [dbo].[used] ([id], [name]) VALUES (8, N'Giải độc')
INSERT [dbo].[used] ([id], [name]) VALUES (9, N'Hỗ trợ tiêu hóa')
INSERT [dbo].[used] ([id], [name]) VALUES (10, N'Giảm cân')
INSERT [dbo].[used] ([id], [name]) VALUES (11, N'Giàu năng lượng')
INSERT [dbo].[used] ([id], [name]) VALUES (12, N'trang trí')
INSERT [dbo].[used] ([id], [name]) VALUES (1012, N'Nên hạn chế')
SET IDENTITY_INSERT [dbo].[used] OFF
GO
SET IDENTITY_INSERT [dbo].[used_product] ON 

INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (1, 1, 25)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (2, 1, 26)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (3, 1, 27)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (4, 1, 29)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (5, 1, 30)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (6, 1, 42)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (7, 1, 43)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (8, 2, 42)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (9, 2, 43)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (10, 2, 1)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (11, 2, 2)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (12, 2, 22)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (13, 2, 24)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (14, 2, 36)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (15, 2, 39)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (16, 3, 6)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (17, 3, 29)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (18, 3, 37)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (19, 3, 47)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (20, 3, 4)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (21, 4, 1)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (22, 4, 2)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (23, 4, 3)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (24, 4, 5)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (25, 4, 7)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (27, 4, 8)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (28, 4, 9)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (29, 4, 34)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (30, 4, 40)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (31, 4, 41)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (32, 4, 43)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (33, 5, 10)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (34, 5, 11)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (35, 5, 12)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (36, 5, 13)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (37, 5, 14)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (38, 6, 22)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (39, 5, 16)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (40, 5, 17)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (41, 5, 18)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (42, 5, 19)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (43, 5, 20)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (44, 5, 21)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (45, 5, 22)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (46, 5, 23)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (47, 5, 24)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (48, 5, 31)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (49, 5, 32)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (50, 5, 35)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (51, 5, 43)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (52, 5, 44)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (53, 5, 45)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (54, 5, 42)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (55, 6, 9)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (56, 6, 11)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (57, 6, 15)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (58, 6, 29)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (59, 6, 30)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (60, 6, 37)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (61, 6, 45)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (62, 7, 26)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (63, 7, 28)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (64, 7, 15)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (65, 8, 5)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (66, 8, 9)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (67, 8, 14)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (68, 8, 27)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (69, 8, 38)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (70, 8, 40)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (71, 8, 45)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (72, 9, 1)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (73, 9, 7)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (74, 9, 38)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (75, 9, 33)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (76, 9, 45)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (77, 10, 2)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (78, 10, 13)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (79, 10, 22)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (80, 10, 39)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (81, 10, 45)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (82, 11, 10)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (83, 11, 11)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (84, 11, 32)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (85, 11, 44)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (87, 5, 5)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (1086, 4, 49)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (1087, 7, 4)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (1089, 10, 8)
INSERT [dbo].[used_product] ([id], [used_id], [product_id]) VALUES (1091, 7, 27)
SET IDENTITY_INSERT [dbo].[used_product] OFF
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF_account_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[import_detail] ADD  CONSTRAINT [DF_import_detail_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[order] ADD  CONSTRAINT [DF_bill_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[review] ADD  CONSTRAINT [DF_review_contentReview]  DEFAULT (N'Đã đánh giá') FOR [contentReview]
GO
ALTER TABLE [dbo].[review] ADD  CONSTRAINT [DF_review_created]  DEFAULT (getdate()) FOR [created]
GO
ALTER TABLE [dbo].[review] ADD  CONSTRAINT [DF_review_score]  DEFAULT ((0)) FOR [score]
GO
ALTER TABLE [dbo].[supply_list] ADD  CONSTRAINT [DF_supply_list_quantity]  DEFAULT ((100)) FOR [quantity]
GO
ALTER TABLE [dbo].[supply_list] ADD  CONSTRAINT [DF_supply_list_import_price]  DEFAULT ((0)) FOR [import_price]
GO
ALTER TABLE [dbo].[account]  WITH CHECK ADD  CONSTRAINT [FK_account_roles] FOREIGN KEY([id_role])
REFERENCES [dbo].[roles] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[account] CHECK CONSTRAINT [FK_account_roles]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK_cart_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK_cart_product]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK_cart_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[customer] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK_cart_users]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [FK_users_account] FOREIGN KEY([id_account])
REFERENCES [dbo].[account] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [FK_users_account]
GO
ALTER TABLE [dbo].[employee]  WITH CHECK ADD  CONSTRAINT [FK_admin_account] FOREIGN KEY([id_account])
REFERENCES [dbo].[account] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[employee] CHECK CONSTRAINT [FK_admin_account]
GO
ALTER TABLE [dbo].[import_detail]  WITH CHECK ADD  CONSTRAINT [FK_import_detail_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[import_detail] CHECK CONSTRAINT [FK_import_detail_product]
GO
ALTER TABLE [dbo].[import_detail]  WITH CHECK ADD  CONSTRAINT [FK_import_detail_supply_Invoice] FOREIGN KEY([invoice_id])
REFERENCES [dbo].[supply_Invoice] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[import_detail] CHECK CONSTRAINT [FK_import_detail_supply_Invoice]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_admin] FOREIGN KEY([admin_id])
REFERENCES [dbo].[employee] ([id])
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_admin]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_order_status] FOREIGN KEY([status])
REFERENCES [dbo].[order_status] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_order_status]
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD  CONSTRAINT [FK_order_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[customer] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[order] CHECK CONSTRAINT [FK_order_users]
GO
ALTER TABLE [dbo].[ordered]  WITH CHECK ADD  CONSTRAINT [FK_ordered_bill] FOREIGN KEY([order_id])
REFERENCES [dbo].[order] ([id])
GO
ALTER TABLE [dbo].[ordered] CHECK CONSTRAINT [FK_ordered_bill]
GO
ALTER TABLE [dbo].[ordered]  WITH CHECK ADD  CONSTRAINT [FK_ordered_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[ordered] CHECK CONSTRAINT [FK_ordered_product]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_catalog] FOREIGN KEY([catalog_id])
REFERENCES [dbo].[catalog] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_catalog]
GO
ALTER TABLE [dbo].[review]  WITH CHECK ADD  CONSTRAINT [FK_review_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[review] CHECK CONSTRAINT [FK_review_product]
GO
ALTER TABLE [dbo].[review]  WITH CHECK ADD  CONSTRAINT [FK_review_users] FOREIGN KEY([id_user])
REFERENCES [dbo].[customer] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[review] CHECK CONSTRAINT [FK_review_users]
GO
ALTER TABLE [dbo].[supply_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_supply_Invoice_admin] FOREIGN KEY([ad_id])
REFERENCES [dbo].[employee] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[supply_Invoice] CHECK CONSTRAINT [FK_supply_Invoice_admin]
GO
ALTER TABLE [dbo].[supply_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_supply_Invoice_producer] FOREIGN KEY([producer_id])
REFERENCES [dbo].[producer] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[supply_Invoice] CHECK CONSTRAINT [FK_supply_Invoice_producer]
GO
ALTER TABLE [dbo].[supply_list]  WITH CHECK ADD  CONSTRAINT [FK_supply_list_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[supply_list] CHECK CONSTRAINT [FK_supply_list_product]
GO
ALTER TABLE [dbo].[used_product]  WITH CHECK ADD  CONSTRAINT [FK_used_product_product] FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[used_product] CHECK CONSTRAINT [FK_used_product_product]
GO
ALTER TABLE [dbo].[used_product]  WITH CHECK ADD  CONSTRAINT [FK_used_product_used] FOREIGN KEY([used_id])
REFERENCES [dbo].[used] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[used_product] CHECK CONSTRAINT [FK_used_product_used]
GO
ALTER TABLE [dbo].[review]  WITH CHECK ADD  CONSTRAINT [CK_review_score] CHECK  (([score]>=(0) AND [score]<=(5)))
GO
ALTER TABLE [dbo].[review] CHECK CONSTRAINT [CK_review_score]
GO
/****** Object:  StoredProcedure [dbo].[CHECK_ORDER]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CHECK_ORDER]
AS
DECLARE @currentDate DATE = GETDATE();
UPDATE dbo.[order]
SET status = 2
WHERE status = 1 
  AND sentdate < DATEADD(day, -3, @currentDate);
GO
/****** Object:  StoredProcedure [dbo].[SP_BEST_SELLING]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_BEST_SELLING]
as
begin
SELECT TOP(10) p.id
FROM dbo.product AS p, dbo.ordered AS o 
WHERE p.id = o.product_id GROUP BY p.id
ORDER BY SUM(o.qty) DESC; 
end;
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECKDATE]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CHECKDATE]
AS
BEGIN
	 DECLARE @currentDate DATE = GETDATE();
	 DECLARE @ExpiredImportDetails TABLE (
        siId INT,
        productId INT,
        expiredStock INT
    );
	INSERT INTO @ExpiredImportDetails (siId, productId, expiredStock)
    SELECT id.invoice_id, id.product_id, id.stock
    FROM import_detail AS id
    JOIN product p ON id.product_id = p.id
    JOIN supply_Invoice si ON id.invoice_id = si.id
    WHERE DATEADD(DAY, p.expiry_date, si.supply_time) < @currentDate
      AND id.status = 1;
	--SELECT * FROM @ExpiredImportDetails
	-- Cập nhật import Detail
	UPDATE id
    SET --id.stock = 0,
        id.status = 0
    FROM import_detail id
    JOIN @ExpiredImportDetails e ON id.invoice_id = e.siId AND id.product_id = e.productId;
	UPDATE p
	SET p.quantity = p.quantity - e.expiredStock
    FROM product p
    JOIN @ExpiredImportDetails e ON p.id = e.productId;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_DATA1]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DATA1]
AS
BEGIN
SELECT p.id, p.name as product, c.id as catalog, up.used_id as used FROM dbo.product as p JOIN dbo.catalog as c 
ON p.catalog_id = c.id LEFT JOIN dbo.used_product as up ON p.id = up.product_id;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_DATA2]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DATA2]
AS
BEGIN
SELECT r.id_user, r.product_id, r.score as rating FROM dbo.review as r
END;
GO
/****** Object:  StoredProcedure [dbo].[V_BCDT]    Script Date: 12/14/2024 12:50:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[V_BCDT]
@bd DATE,
@kt DATE
AS
BEGIN
SELECT o.id,[date] = o.sentdate,LoaiGG = N'Bán hàng',[name] = a.[name], patner = u.nameuser, 
	[status] = CASE WHEN o.[status] = 0 THEN N'Đang xét' WHEN o.[status] = 1 THEN N'Đã duyệt' 
	WHEN o.[status] = 3 THEN N'Đã Hủy' WHEN o.[status] = 2 THEN N'Đã giao' END,
	amount = CASE WHEN o.[status] = 0 THEN 0 WHEN o.[status] = 1 AND o.payment = 'Thanh toán bằng VNPay' THEN o.amount 
	WHEN o.[status] = 3 THEN 0 WHEN o.[status] = 2 THEN o.amount ELSE 0 END   
FROM dbo.[order] AS o, dbo.employee AS a,dbo.customer AS u  
WHERE o.sentdate >= @bd AND o.sentdate <= @kt AND o.admin_id = a.id AND o.[user_id] = u.id 
--ORDER BY [date] ASC;
UNION
SELECT s.id, [date] = s.supply_time, LoaiGG = N'Nhập hàng',
	[name] = a.[name],patner = p.[name], [status] = N'Nhập hàng',amount = -kt.amount
FROM supply_Invoice AS s,  dbo.employee AS a,dbo.producer AS p,
	(SELECT invoice_id,  SUM(CAST(import_price AS float)) AS amount
FROM import_detail
GROUP BY invoice_id)  AS kt
WHERE s.supply_time >= @bd AND s.supply_time <= @kt AND s.ad_id = a.id AND s.producer_id = p.id AND s.id = kt.invoice_id
--ORDER BY [date] ASC;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1-Đang hoạt đông; 0-Đã bị khóa' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'account', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 : chưa hoàn thành, 1 hoàn thành' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'order', @level2type=N'COLUMN',@level2name=N'status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'day' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'expiry_date'
GO
