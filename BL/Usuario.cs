using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {
                    usuario.Rol.IdRol = (usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol; //operador ternario
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuariosGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}',{usuario.Rol.IdRol}").ToList();

                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario usuario1 = new ML.Usuario();
                            ML.Pais pais = new ML.Pais();
                            ML.Estado estado = new ML.Estado();
                            ML.Municipio municipio = new ML.Municipio();
                            ML.Colonia colonia = new ML.Colonia();
                            ML.Direccion direccion = new ML.Direccion();
                            usuario1.IdUsuario = int.Parse(obj.IdUsuario.ToString());
                            usuario1.Nombre = obj.Nombre;
                            usuario1.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario1.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario1.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario1.Sexo = obj.Sexo;
                            usuario1.Curp = obj.Curp;
                            usuario1.UserName = obj.Username;
                            usuario1.Email = obj.Email;
                            usuario1.Password = obj.Password;
                            usuario1.Telefono = obj.Telefono;
                            usuario1.Celular = obj.Celular;
                            usuario1.Rol = new ML.Rol();
                            usuario1.Rol.Nombre = obj.NombreRol;
                            usuario1.Rol.IdRol = int.Parse(obj.IdRol.ToString());
                            usuario1.Status = bool.Parse(obj.Status.ToString());

                            usuario1.Pais = new ML.Pais();
                            usuario1.Pais.NombrePais = obj.NombrePais;
                            // usuario1.Pais.IdPais = int.Parse(obj.IdPais.ToString());

                            usuario1.Estado = new ML.Estado();
                            usuario1.Estado.Nombre = obj.NombreEstado;

                            usuario1.Municipio = new ML.Municipio();
                            usuario1.Municipio.NombreMunicipio = obj.NombreMunicipio;

                            usuario1.Colonia = new ML.Colonia();
                            usuario1.Colonia.Nombre = obj.NombreColonia;
                            usuario1.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario1.Direccion = new ML.Direccion();
                            usuario1.Direccion.Calle = obj.Calle;
                            usuario1.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario1.Direccion.NumeroInterior = obj.NumeroInterior;
                            result.Objects.Add(usuario1);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            usuario.Rol = new ML.Rol();
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {
                    //var usuarios = context.AlumnoUpdate(alumno.IdAlumno, alumno.Nombre, alumno.ApellidoPaterno, alumno.ApellidoMaterno, alumno.FechaNacimiento, alumno.Sexo, alumno.Semestre.IdSemestre, alumno.Horario.Nombre, alumno.Horario.Grupo.IdGrupo);
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Curp}', '{usuario.Password}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Telefono}', '{usuario.Celular}',{usuario.Rol.IdRol}");

                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }



        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (obj != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.Sexo = obj.Sexo;
                        usuario.Curp = obj.Curp;
                        usuario.UserName = obj.Username;
                        usuario.Email = obj.Email;
                        usuario.Password = obj.Password;
                        usuario.Telefono = obj.Telefono;
                        usuario.Celular = obj.Celular;
                        usuario.Status =(bool) obj.Status;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = obj.IdRol.Value;
                        usuario.Rol.IdRol = int.Parse(obj.IdRol.ToString());
                        usuario.Rol.Nombre = obj.NombreRol;
                        //result.Object = usuario;
                        usuario.Status = obj.Status.Value;

                        usuario.Pais = new ML.Pais();
                        usuario.Pais.NombrePais = obj.NombrePais;
                        // usuario1.Pais.IdPais = int.Parse(obj.IdPais.ToString());

                        usuario.Estado = new ML.Estado();
                        usuario.Estado.Nombre = obj.NombreEstado;

                        usuario.Municipio = new ML.Municipio();
                        usuario.Municipio.NombreMunicipio = obj.NombreMunicipio;

                        usuario.Colonia = new ML.Colonia();
                        usuario.Colonia.Nombre = obj.NombreColonia;
                        usuario.Colonia.CodigoPostal = obj.CodigoPostal;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Calle = obj.Calle;
                        usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                        usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                        //result.Object = usuario;
                        result.Objects.Add(usuario);

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }

            return result;
        }

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableusuario = new DataTable();

                        da.Fill(tableusuario);

                        if (tableusuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableusuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.FechaNacimiento = row[3].ToString();
                                usuario.Sexo = row[4].ToString();

                                usuario.Curp = row[5].ToString();
                                usuario.UserName = row[6].ToString();
                                usuario.Email = row[7].ToString();
                                usuario.Password = row[8].ToString();
                                usuario.Telefono = row[9].ToString();
                                usuario.Celular = row[10].ToString();
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());


                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableusuario;

                        if (tableusuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;

        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;


                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : usuario.Nombre; //operador ternario

                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno ";

                        if (usuario.FechaNacimiento.ToString() == "")
                        {
                            error.Mensaje += "Ingresar la fecha de nacimiento";
                        }

                        if (usuario.Sexo.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el sexo ";
                        }

                        if (usuario.Curp.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el curp";
                        }

                        if (usuario.Password.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el password ";
                        }

                        if (usuario.UserName.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el username ";
                        }

                        if (usuario.Email.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el email ";
                        }

                        if (usuario.Telefono.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el telefono ";
                        }


                        if (usuario.Celular.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el celular ";
                        }

                        if (usuario.Rol.IdRol.ToString() == "")
                        {
                            error.Mensaje += "Ingresar el rol ";
                        }
                        if (error.Mensaje != null)
                        {
                            result.Objects.Add(error);
                        }


                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result ChangeStatus(int idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {idUsuario}, {status}");

                    if (usuarios > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(ML.Usuario usuario)
        {
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                    {
                        var usuarios = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                        if (usuarios > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }

                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                }
                return result;
            }
        }

    }

}



