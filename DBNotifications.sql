
-- Base de datos para pruebas de service bus, notificaciones y seguridad
create database [NotificationsDemo];
go

use [NotificationsDemo];
go


-------------------------------------------------------------------------------------------
-- Tablas de Seguridad
-------------------------------------------------------------------------------------------

-- Roles de la empresa o los departamentos
create table Roles(
	RolId int not null identity(1,1),
	Nombre nvarchar(50) not null,
	Descripcion nvarchar(150) not null,
	CreadoEn datetime not null,
	Constraint PrimaryKey_Roles_RolId primary key (RolId)
);
go

-- Tabla que especifica qué accesos existen
create table Permisos(
	PermisoId int not null identity(1,1),
	Modulo nvarchar(50) not null,
	Nombre nvarchar(50) not null,
	Descripcion nvarchar(150) not null,
	CreadoEn datetime not null,
	Constraint PrimaryKey_Permisos_PermisoId primary key (PermisoId)
);
go

-- Tabla donde se almacena el usuario
create table Usuarios(
	UsuarioId int not null identity(1,1),
	RolId int not null,
	Nombre nvarchar(50) not null,
	Correo nvarchar(50) not null,
	CreadoEn datetime not null,
	Constraint PrimaryKey_Usuarios_UsuarioId primary key (UsuarioId),
	Constraint ForeignKey_Usuarios_Roles_RolId foreign key (RolId) references Roles(RolId)
);
go

--Tabla que prohibe los accesos
create table PermisosDenegadosPorRol(
	PermisoDenegadoPorRolId int not null identity(1,1),
	RolId int not null,
	PermisoId int not null,
	CreadoEn datetime not null,
	Constraint PrimaryKey_PermisosDenegadosPorRol_PermisoDenegadoPorRolId primary key (PermisoDenegadoPorRolId),
	Constraint ForeignKey_PermisosDenegadosPorRol_Roles_RolId foreign key (RolId) references Roles(RolId),
	Constraint ForeignKey_PermisosDenegadosPorRol_Permisos_PermisoId foreign key (PermisoId) references Permisos(PermisoId)
);
go

--------------------------------------------------------------------------------------
-- delete Roles DBCC CHECKIDENT (Roles, RESEED,0)
Insert into Roles(Nombre, Descripcion, CreadoEn)
Values('Administrador', 'El administrador tiene la factultad de acceder a todos los procesos de la página.', GETDATE()),
('Usuario', 'El usuario puede tener acceso a cierta información.', GETDATE()),
('Invitado', 'El invitado no puede realizar procesos, sólo puede consultar procesos e información pública.', GETDATE());

-- delete Usuarios DBCC CHECKIDENT (Usuarios, RESEED,0)
Insert into Usuarios(RolId, Nombre, Correo, CreadoEn)
VALUES(1, 'Josué Mercadillo', 'jmercadillo@cgclatam.com', GETDATE()),
(2, 'Josué Flores', 'josuemercally@gmail.com', GETDATE()),
(3, 'Ulises Flores', 'metal_uli@hotmail.com', GETDATE());


-- delete Permisos DBCC CHECKIDENT (Permisos, RESEED,0)

Insert into Permisos(Modulo, Nombre, Descripcion, CreadoEn)
VALUES('Usuario','Crear_Usuario','Facultad de crear un usuario nuevo.', GETDATE()),
('Usuario','Eliminar_Usuario','Facultad de eliminar un usuario existente en el sistema.', GETDATE()),
('Usuario','Modificar_Usuario','Facultad de modificar un usuario existente en el sistema.', GETDATE()),
('Usuario','Consultar_Usuario','Facultad de ver los usuarios existentes en el sistema.', GETDATE());

Insert into PermisosDenegadosPorRol(RolId, PermisoId,CreadoEn)
VALUES(2, 2, GETDATE()),(3, 1, GETDATE()),(3, 2, GETDATE()), (3, 3, GETDATE());


-------------------------------------------------------------------------------------------
-- Tablas de notificaciones
-------------------------------------------------------------------------------------------

-- Tabla que almacenará las notificaciones
create table Notificaciones(
NotificacionId int not null identity(1,1),
UsuarioId int not null,
Titulo nvarchar(50) not null,
Cuerpo nvarchar(200) null,
AgregadoEn datetime null,
Leido bit not null,
Constraint PrimaryKey_Notificaciones_NotificacionId primary key (NotificacionId),
Constraint ForeignKey_Notificaciones_Usuarios_UsuarioId foreign key (UsuarioId) references Usuarios(UsuarioId)
);
go

-- Tabla que guardará qué suscription de notificaciones tiene cada usuario
create table RegistroNotificaciones(
RegistroNotificacionId int not null identity(1,1),
UsuarioId int not null,
SuscripcionId int not null,
Constraint PrimaryKey_Notificaciones_RegistroNotificacionId primary key (RegistroNotificacionId),
Constraint ForeignKey_RegistroNotificaciones_Usuarios_UsuarioId foreign key (UsuarioId) references Usuarios(UsuarioId)
);
go


-------------------------------------------------------------------------------------------
-- Tabla de Bitacora de acceso al sistema
-------------------------------------------------------------------------------------------

CREATE TABLE [dbo].[BitacoraIngresos](
	[BitacoraIngresosId] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [nvarchar](36) NULL,
	[NombreUsuario] [nvarchar](50) NOT NULL,
	[Accion] [char](1) NOT NULL,
	[Departamento] [nvarchar](50) NULL,
	[NombreCompleto] [nvarchar](100) NOT NULL,
	[Terminal] [nvarchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [BitacoraIngresos_BitacoraIngresosId] PRIMARY KEY CLUSTERED 
(
	[BitacoraIngresosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

GO

