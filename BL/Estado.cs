using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var estados = context.Estados.FromSqlRaw("[EstadoGetAll]").ToList();
                   
                    result.Objects = new List<object>();
                    if (estados != null)
                    {
                        foreach (var obj in estados)
                        {
                            ML.Pais pais = new ML.Pais();
                           
                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;
                            estado.Pais = new ML.Pais();
                            estado.Pais.NombrePais = obj.Nombre;


                            result.Objects.Add(estado);

                        }
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

    }
}
