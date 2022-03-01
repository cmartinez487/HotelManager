use TestHotel
go

create table Hotel
(
    Idhotel     int identity,
    nombre      varchar(50),
    pais        varchar(2),
    latitud     int,
    longitud    int,
    descripcion varchar(50),
    activo      bit,
    Capacidad   int
)
go

use TestHotel
go

create table Reserva
(
    IdReserva    int identity,
    UserId       int,
    Idhotel      int,
    checkin      datetime,
    checkout     datetime,
    fechaReserva datetime,
    Estado       bit,
    IdHab        int
)
go

use TestHotel
go

create table Usuario
(
    UserId    int identity,
    nombre    varchar(25),
    apellidos varchar(25),
    mail      varchar(50),
    direccion varchar(100)
)
go
