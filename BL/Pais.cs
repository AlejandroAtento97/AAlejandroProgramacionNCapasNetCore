using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
   public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var paises1 = context.Pais.FromSqlRaw("[PaisGetAll]").ToList();

                    result.Objects = new List<object>();

                    if (paises1 != null)
                    {
                        foreach (var obj in paises1)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            //ML.Pais pais = new ML.Pais();



                            //usuario.Pais = new ML.Pais();
                            //usuario.Pais.IdPais = int.Parse(obj.IdPais.ToString());
                            //usuario.Pais.NombrePais = obj.Nombre;


                            result.Objects.Add(usuario);
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

    }
}

