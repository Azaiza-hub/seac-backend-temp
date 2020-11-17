using Microsoft.Data.Sqlite;
using SEACBE.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SEACBE.Repositories
{
    public class SolicitudesRepository : ISolicitudesRepository
    {
        public Solicitud ActualizarSolicitud(Solicitud solicitud)
        {
            using (var connection = new SqliteConnection("Data Source=SEAC.db"))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "Update Solicitud set Location = $location, Descripcion = $descripcion, Imagen = $imagen, Fecha = $fecha, Estado = $estado where id=$id";
                    command.Parameters.AddWithValue("$id", solicitud.IdSolicitud);
                    command.Parameters.AddWithValue("$location", solicitud.Location);
                    command.Parameters.AddWithValue("$descripcion", solicitud.Descripcion);
                    var binario = new SqliteParameter("$imagen", System.Data.DbType.Binary);
                    binario.Value = solicitud.Imagen;
                    command.Parameters.Add(binario);
                    command.Parameters.AddWithValue("$fecha", solicitud.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    command.Parameters.AddWithValue("$estado", solicitud.Estado);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Explotó mamila cósmica");
                } 
            };
            return solicitud;
        }

        public Solicitud CrearSolicitud(Solicitud solicitud)
        {
            using (var connection = new SqliteConnection("Data Source=SEAC.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Insert into Solicitud(Location,Descripcion,Imagen,Fecha,Estado) values ($location,$descripcion,$imagen,$fecha,$estado)";
                command.Parameters.AddWithValue("$location", solicitud.Location);
                command.Parameters.AddWithValue("$descripcion", solicitud.Descripcion);
                var binario = new SqliteParameter("$imagen",System.Data.DbType.Binary);
                binario.Value = solicitud.Imagen;
                command.Parameters.Add(binario);
                command.Parameters.AddWithValue("$fecha", solicitud.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("$estado", solicitud.Estado);
                command.ExecuteNonQuery();
            };
            return solicitud;
        }

        public Solicitud GetSolicitudById(int id)
        {
            using (var connection = new SqliteConnection("Data Source=SEAC.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Select * from Solicitud where Id = $id";
                command.Parameters.AddWithValue("$id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resultid = reader.GetInt32(0);
                        var location = reader.GetString(1);
                        var description = reader.GetString(2);
                        var imagen = (Byte[])reader["Imagen"];
                        var fecha = DateTime.Parse(reader.GetString(4));
                        var estado = "Abierta";
                        return new Solicitud()
                        {
                            IdSolicitud = resultid,
                            Location = location,
                            Descripcion = description,
                            Imagen = imagen,
                            Fecha = fecha,
                            Estado = estado
                            };
                    }
                }
            };
            return null;
        }
        
        public IEnumerable<Solicitud> GetSolicitudes()
        {
            using (var connection = new SqliteConnection("Data Source=SEAC.db"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "Select Id,Location,Descripcion,Fecha,Estado from Solicitud";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resultid = reader.GetInt32(0);
                        var location = reader.GetString(1);
                        var description = reader.GetString(2);
                        var fecha = DateTime.Parse(reader.GetString(3));
                        var estado = reader.GetString(4);
                        yield return new Solicitud()
                        {
                            IdSolicitud = resultid,
                            Location = location,
                            Descripcion = description,
                            Fecha = fecha,
                            Estado = estado
                        };
                    }
                }
            }
        }
    }
}
