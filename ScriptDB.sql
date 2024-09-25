USE [master]
GO
/****** Object:  Database [facturacion4]    Script Date: 25/09/2024 20:30:52 ******/
CREATE DATABASE [facturacion4]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'facturacion4', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLNASHE\MSSQL\DATA\facturacion4.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'facturacion4_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLNASHE\MSSQL\DATA\facturacion4_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [facturacion4] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [facturacion4].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [facturacion4] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [facturacion4] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [facturacion4] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [facturacion4] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [facturacion4] SET ARITHABORT OFF 
GO
ALTER DATABASE [facturacion4] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [facturacion4] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [facturacion4] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [facturacion4] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [facturacion4] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [facturacion4] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [facturacion4] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [facturacion4] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [facturacion4] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [facturacion4] SET  ENABLE_BROKER 
GO
ALTER DATABASE [facturacion4] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [facturacion4] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [facturacion4] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [facturacion4] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [facturacion4] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [facturacion4] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [facturacion4] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [facturacion4] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [facturacion4] SET  MULTI_USER 
GO
ALTER DATABASE [facturacion4] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [facturacion4] SET DB_CHAINING OFF 
GO
ALTER DATABASE [facturacion4] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [facturacion4] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [facturacion4] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [facturacion4] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [facturacion4] SET QUERY_STORE = ON
GO
ALTER DATABASE [facturacion4] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [facturacion4]
GO
/****** Object:  Table [dbo].[articulos]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[articulos](
	[cod_articulo] [int] NOT NULL,
	[articulo] [varchar](50) NULL,
	[pre_unitario] [float] NULL,
 CONSTRAINT [pk_cod_articulos] PRIMARY KEY CLUSTERED 
(
	[cod_articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalles_facturas]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalles_facturas](
	[cod_detalle_factura] [int] IDENTITY(1,1) NOT NULL,
	[cod_factura] [int] NULL,
	[cod_articulo] [int] NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [pk_detalle_factura] PRIMARY KEY CLUSTERED 
(
	[cod_detalle_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[facturas]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[facturas](
	[cod_factura] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NULL,
	[cod_forma_pago] [int] NULL,
	[cliente] [varchar](50) NULL,
 CONSTRAINT [pk_cod_factura] PRIMARY KEY CLUSTERED 
(
	[cod_factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[formas_pagos]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[formas_pagos](
	[cod_forma_pago] [int] IDENTITY(1,1) NOT NULL,
	[forma_pago] [varchar](20) NULL,
 CONSTRAINT [pk_cod_forma_pago] PRIMARY KEY CLUSTERED 
(
	[cod_forma_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[detalles_facturas]  WITH CHECK ADD  CONSTRAINT [fk_cod_articulo] FOREIGN KEY([cod_articulo])
REFERENCES [dbo].[articulos] ([cod_articulo])
GO
ALTER TABLE [dbo].[detalles_facturas] CHECK CONSTRAINT [fk_cod_articulo]
GO
ALTER TABLE [dbo].[detalles_facturas]  WITH CHECK ADD  CONSTRAINT [fk_cod_factura] FOREIGN KEY([cod_factura])
REFERENCES [dbo].[facturas] ([cod_factura])
GO
ALTER TABLE [dbo].[detalles_facturas] CHECK CONSTRAINT [fk_cod_factura]
GO
ALTER TABLE [dbo].[facturas]  WITH CHECK ADD  CONSTRAINT [fk_cod_forma_pago] FOREIGN KEY([cod_forma_pago])
REFERENCES [dbo].[formas_pagos] ([cod_forma_pago])
GO
ALTER TABLE [dbo].[facturas] CHECK CONSTRAINT [fk_cod_forma_pago]
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_ARTICULO]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_ARTICULO]
    @cod_articulo INT,
    @articulo VARCHAR(50),
    @pre_unitario FLOAT
AS
BEGIN
    UPDATE Articulos
    SET articulo = @articulo,
        pre_unitario = @pre_unitario
    WHERE cod_articulo = @cod_articulo;
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_FACTURA]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_FACTURA]
    @cod_factura INT,
    @cliente NVARCHAR(100),
    @cod_forma_pago INT,
    @fecha DATE
AS
BEGIN
    UPDATE facturas
    SET cliente = @cliente, cod_forma_pago = @cod_forma_pago, fecha = @fecha
    WHERE cod_factura = @cod_factura;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_DETALLES_POR_FACTURA]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ELIMINAR_DETALLES_POR_FACTURA]
    @cod_factura INT
AS
BEGIN
    DELETE FROM detalles_factura
    WHERE cod_factura = @cod_factura;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_FACTURA]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ELIMINAR_FACTURA]
    @cod_factura INT
AS
BEGIN
    DELETE FROM facturas
    WHERE cod_factura = @cod_factura;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_ARTICULO]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_INSERTAR_ARTICULO]
@cod_articulo int,
@articulo varchar(50),
@pre_unitario float
as
Insert Into articulos (cod_articulo, articulo, pre_unitario) Values (@cod_articulo, @articulo, @pre_unitario)

GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_DETALLE]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_INSERTAR_DETALLE]
	@cod_factura int,
	@cod_articulo int,
	@cantidad int

AS
	INSERT INTO detalles_facturas(cod_factura, cod_articulo, cantidad) 
	VALUES ( @cod_factura, @cod_articulo, @cantidad)

GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_MAESTRO]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_INSERTAR_MAESTRO]
@fecha datetime,
@cod_forma_pago int,
@cliente varchar(50),
@cod_factura int output
as
Insert into facturas(fecha, cod_forma_pago, cliente) Values (@fecha, @cod_forma_pago, @cliente)
set @cod_factura = SCOPE_IDENTITY();

GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_ARTICULO]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_RECUPERAR_ARTICULO]
as
select * from articulos

GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_ARTICULO_POR_ID]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_RECUPERAR_ARTICULO_POR_ID]
@cod_articulo int
as
select *
from articulos
Where cod_articulo = @cod_articulo

GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_FACTURAS]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_RECUPERAR_FACTURAS]
as
SELECT f.*, df.cantidad, a.*, fp.forma_pago
	  FROM facturas f
	  JOIN formas_pagos fp on fp.cod_forma_pago = f.cod_forma_pago
	  JOIN detalles_facturas df ON df.cod_factura = f.cod_factura
	  JOIN articulos a ON a.cod_articulo = df.cod_articulo
	  ORDER BY f.cod_factura;

GO
/****** Object:  StoredProcedure [dbo].[SP_RECUPERAR_FACTURAS_POR_ID]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_RECUPERAR_FACTURAS_POR_ID]
@cod_factura int
as
SELECT f.*, df.cantidad, a.*, fp.forma_pago
	  FROM facturas f
	  JOIN formas_pagos fp on fp.cod_forma_pago = f.cod_forma_pago
	  JOIN detalles_facturas df ON df.cod_factura = f.cod_factura
	  JOIN articulos a ON a.cod_articulo = df.cod_articulo
	  Where f.cod_factura = @cod_factura;

GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRAR_BAJA_ARTICULO]    Script Date: 25/09/2024 20:30:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_REGISTRAR_BAJA_ARTICULO]
@cod_articulo int
as
delete from articulos where cod_articulo = @cod_articulo
GO
USE [master]
GO
ALTER DATABASE [facturacion4] SET  READ_WRITE 
GO
