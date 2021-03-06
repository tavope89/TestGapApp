USE [GapTest]
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 6/23/2017 1:11:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paciente](
	[Id_Paciente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Edad] [smallint] NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Fecha_Ultima_Visita] [datetime] NULL,
	[Fecha_Proxima_Visita] [datetime] NULL,
 CONSTRAINT [PK_Paciente] PRIMARY KEY CLUSTERED 
(
	[Id_Paciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tratamientos]    Script Date: 6/23/2017 1:11:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tratamientos](
	[Id_Tratamiento] [int] IDENTITY(1,1) NOT NULL,
	[Id_Paciente] [int] NOT NULL,
	[Fecha_Inicio] [datetime] NOT NULL,
	[Fecha_Fin] [datetime] NOT NULL,
	[Costo] [int] NOT NULL,
	[Detalle] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Tratamientos] PRIMARY KEY CLUSTERED 
(
	[Id_Tratamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Tratamientos]  WITH CHECK ADD  CONSTRAINT [FK_Tratamientos_Paciente] FOREIGN KEY([Id_Paciente])
REFERENCES [dbo].[Paciente] ([Id_Paciente])
GO
ALTER TABLE [dbo].[Tratamientos] CHECK CONSTRAINT [FK_Tratamientos_Paciente]
GO
