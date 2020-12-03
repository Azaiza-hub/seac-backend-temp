using Microsoft.Data.Sqlite;
using SEACBE.Models;
using SEACBE.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace SEACBE.Repositories
{
    public class SolicitudesRepository : ISolicitudesRepository
    {
        private NLPService nLPService = new NLPService();
        public Solicitud ActualizarSolicitud(Solicitud solicitud)
        {
            using (var connection = new SqliteConnection("Data Source=SEAC.db"))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "Update Solicitud set Location = $location, Descripcion = $descripcion, Imagen = $imagen, Fecha = $fecha, Estado = $estado, Clasificacion = $clasificacion, Sentimentalismo = $sentimentalismo where id=$id";
                    command.Parameters.AddWithValue("$id", solicitud.IdSolicitud);
                    command.Parameters.AddWithValue("$location", solicitud.Location);
                    command.Parameters.AddWithValue("$descripcion", solicitud.Descripcion);
                    var binario = new SqliteParameter("$imagen", System.Data.DbType.Binary);
                    binario.Value = solicitud.Imagen;
                    command.Parameters.Add(binario);
                    command.Parameters.AddWithValue("$fecha", solicitud.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    command.Parameters.AddWithValue("$estado", solicitud.Estado);
                    command.Parameters.AddWithValue("$clasifiacion", solicitud.Clasificacion);
                    command.Parameters.AddWithValue("$sentimentalismo", solicitud.Sentimentalismo);
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
                command.CommandText = "Insert into Solicitud(Location,Descripcion,Imagen,Fecha,Estado,Clasificacion,Sentimentalismo) values ($location,$descripcion,$imagen,$fecha,$estado,$clasificacion,$sentimentalismo)";
                command.Parameters.AddWithValue("$location", solicitud.Location);
                command.Parameters.AddWithValue("$descripcion", solicitud.Descripcion);
                var binario = new SqliteParameter("$imagen",System.Data.DbType.Binary);
                binario.Value = solicitud.Imagen;
                command.Parameters.Add(binario);
                command.Parameters.AddWithValue("$fecha", solicitud.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                command.Parameters.AddWithValue("$estado", solicitud.Estado);
                var clasificacion = ClasificarSolicitud(solicitud.Descripcion);
                command.Parameters.AddWithValue("$clasificacion", clasificacion);
                var sentimentalismo = nLPService.ObtenerSentimentalismo(solicitud.Descripcion);
                command.Parameters.AddWithValue("$sentimentalismo", solicitud.Sentimentalismo);
                command.ExecuteNonQuery();
            };
            return solicitud;
        }

        public string ClasificarSolicitud(string desc)
        {
            string clasificacion = "";
            if (desc.Contains("golp")|| desc.Contains("peg") || desc.Contains("agarr"))
            {
                if(desc.Contains("marido") || desc.Contains("mujer") || desc.Contains("espos") || desc.Contains("novi") || desc.Contains("pareja") || desc.Contains("padre") || desc.Contains("papa") || desc.Contains("abuel") || desc.Contains("ti") || desc.Contains("herman") || desc.Contains("madre") || desc.Contains("mama"))
                {
                    clasificacion = "Violencia de Genero";
                    return clasificacion;
                }
                if(desc.Contains("vecino") || desc.Contains("hombre") || desc.Contains("persona") || desc.Contains("compañero") || desc.Contains("tipo"))
                {
                    clasificacion = "Acoso";
                    return clasificacion;
                }
            }
            if (desc.Contains("rob") ||desc.Contains("ladr")|| desc.Contains("chor") || desc.Contains("afan") || desc.Contains("llevar") || desc.Contains("yebar") || desc.Contains("drogado"))
            {
                if (desc.Contains("moto") || desc.Contains("auto") || desc.Contains("pistola") || desc.Contains("calle") || desc.Contains("vereda") || desc.Contains("bereda") || desc.Contains("casa") || desc.Contains("plata"))
                {
                    clasificacion = "Acto Delictivo";
                    return clasificacion;
                }
            }
            if (desc.Contains("fot") || desc.Contains("hombre") || desc.Contains("persona") || desc.Contains("compañer") || desc.Contains("amenaza") || desc.Contains("tipo") || desc.Contains("mujer"))
            {
                if (desc.Contains("fot")||desc.Contains("celular") || desc.Contains("cel") || desc.Contains("mensaj") || desc.Contains("wh") || desc.Contains("wspp") || desc.Contains("wa") || desc.Contains("guasap") || desc.Contains("videos") || desc.Contains("asqueroso") || desc.Contains("asquerosa"))
                {
                    clasificacion = "Ciberacoso";
                    return clasificacion;
                }
            }
            if (desc.Contains("hij") || desc.Contains("persona") || desc.Contains("tipo") || desc.Contains("camin") || desc.Contains("anda") || desc.Contains("ij")|| desc.Contains("vecin")||desc.Contains("droga"))
            {
                if (desc.Contains("droga") || desc.Contains("falopa") || desc.Contains("ofre") || desc.Contains("plata"))
                {
                    clasificacion = "Drogas";
                    return clasificacion;
                }
            }
            if (desc.Contains("calle") || desc.Contains("pavimento") || desc.Contains("asfalto") || desc.Contains("inunda") || desc.Contains("poste") || desc.Contains("camin") || desc.Contains("caido")|| desc.Contains("rot"))
            {
                if (desc.Contains("rot") || desc.Contains("inunda") || desc.Contains("no permite") || desc.Contains("vereda") || desc.Contains("romp") || desc.Contains("caid") || desc.Contains("pavimento"))
                {
                    clasificacion = "Defensa Civil";
                    return clasificacion;
                }
            }

            return clasificacion;
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
                        var clasificacion = reader.GetString(6);
                        var sentimentalismo = reader.GetString(7);
                        return new Solicitud()
                        {
                            IdSolicitud = resultid,
                            Location = location,
                            Descripcion = description,
                            Imagen = imagen,
                            Fecha = fecha,
                            Estado = estado,
                            Clasificacion = clasificacion,
                            Sentimentalismo = sentimentalismo
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
                command.CommandText = "Select Id,Location,Descripcion,Fecha,Estado,Clasificacion,Sentimentalismo from Solicitud";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var resultid = reader.GetInt32(0);
                        var location = reader.GetString(1);
                        var description = reader.GetString(2);
                        var fecha = DateTime.Parse(reader.GetString(3));
                        var estado = reader.GetString(4);
                        var clasificacion = reader.GetString(5);
                        var sentimentalismo = reader.GetString(6);
                        yield return new Solicitud()
                        {
                            IdSolicitud = resultid,
                            Location = location,
                            Descripcion = description,
                            Fecha = fecha,
                            Estado = estado,
                            Clasificacion = clasificacion,
                            Sentimentalismo = sentimentalismo
                        };
                    }
                }
            }
        }
    }
}
