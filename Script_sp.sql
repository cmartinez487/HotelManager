--consulta de Usuario segun su id
CREATE procedure dbo.spGetUser
@UserId int
    as begin
  select UserId,
       nombre,
       apellidos,
       mail,
       direccion
from Usuario
where UserId = @UserId;
end
go

--Consulta lista de Hoteles
CREATE procedure dbo.spGetHotels
    as begin
  select Idhotel,
       nombre,
       pais,
       latitud,
       longitud,
       descripcion,
       activo,
       Capacidad
from Hotel
end
go

-- Consulta Hotel segun su id
CREATE procedure dbo.spGetHotel
@Idhotel int
    as begin
  select Idhotel,
       nombre,
       pais,
       latitud,
       longitud,
       descripcion,
       activo,
       Capacidad
from Hotel h
where Idhotel = @Idhotel;
end
go

-- Crea reserva
CREATE procedure dbo.spCreateReservation
    @UserId int,
    @Idhotel int,
    @checkin datetime,
    @checkout datetime,
    @hab int
    as begin
        declare @reserva datetime = getdate()
        insert into Reserva(UserId,Idhotel,checkin,checkout,fechaReserva,Estado,IdHab) values (@UserId,@Idhotel,@checkin,@checkout,@reserva,1,@hab)
        update hotel set Capacidad = Capacidad-1 where Idhotel = @Idhotel
end
go

-- Cancela reserva
CREATE procedure dbo.spCancelReservation
    @IdReserva int,
    @Idhotel int
    as begin
        update Reserva set Estado = 0 where IdReserva = @IdReserva
        update hotel set Capacidad = Capacidad+1 where Idhotel = @Idhotel
end
go

-- Cancela reservax2
CREATE procedure dbo.spCancelReservation
    @IdReserva int,
    @Idhotel int
    as begin
        update Reserva set Estado = 0 where IdReserva = @IdReserva
        update hotel set Capacidad = Capacidad+1 where Idhotel = @Idhotel
end
go