SELECT is_broker_enabled FROM sys.databases WHERE name = 'NotificationsDemo'


	insert into Notificaciones(Usuario, Titulo, Subtitulo, Cuerpo, AgregadoEn, Leido) 
	VALUES (150, 'Titulo 13', 'Subtitulo1', 'Cuerpo prueba', GETDATE(), 0)
	

insert into Notificaciones(UsuarioId, Titulo, Cuerpo, AgregadoEn, Leido) 
VALUES (150, 'Titulo1', 'Cuerpo prueba', GETDATE(), 0)

KILL QUERY NOTIFICATION SUBSCRIPTION ALL;

Insert into Notificaciones(Usuario, Titulo, Subtitulo, Cuerpo, AgregadoEn, Leido) 
VALUES (150, 'Titulo 13', 'Subtitulo1', 'Cuerpo prueba', GETDATE(), 0)
go
Select * from sys.dm_qn_subscriptions
select * from RegistroNotificaciones
select * from Notificaciones order by NotificacionId desc

SELECT TOP(1) id FROM sys.dm_qn_subscriptions ORDER BY id DESC

select top(10) * from Notificaciones order by NotificacionId desc

select * from RegistroNotificaciones 

delete RegistroNotificaciones

delete Notificaciones

CREATE TABLE [dbo].[Notificaciones]
(
	[NotificacionId] int not null identity(1,1),
	[Usuario] int not null,
	[Subtitulo] nvarchar(100) null,
	[Titulo] nvarchar(100) null,
	[Cuerpo] nvarchar(500) null,
	[AgregadoEn] Datetime not null,
	[Leido] bit not null,
	Constraint NotificacionId primary key (NotificacionId)
)

CREATE TABLE [dbo].[RegistroNotificaciones]
(
	[RegistroNotificacionId] int not null identity(1,1),
	[Usuario] int not null,
	[SubscripcionId] int not null,
	Constraint RegistroNotificacionId primary key ([RegistroNotificacionId])
)

SELECT TOP(1) id FROM sys.dm_qn_subscriptions ORDER BY id DESC;

insert into Notificaciones(UsuarioId, Usuario, Titulo, Cuerpo, AgregadoEn, Leido)
Values(0, '87939a03-f7dc-43da-90c0-7945a6678c24', 'Notificación de prueba', 'Esta es una notificación de prueba.', GETDATE(), 0)

Select * from sys.dm_qn_subscriptions
select * from RegistroNotificaciones
select * from Notificaciones order by NotificacionId desc

KILL QUERY NOTIFICATION SUBSCRIPTION all;
delete RegistroNotificaciones
Delete Notificaciones