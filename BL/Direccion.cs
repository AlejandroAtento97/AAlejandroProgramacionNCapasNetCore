using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Direccion
    {
        public static ML.Result GetAll(int IdDireccion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var direccions = context.Direccions.FromSqlRaw("[DireccionGetAll]").ToList();

                    result.Objects = new List<object>();
                    if (direccions != null)
                    {
                        foreach (var obj in direccions)
                        {


                            ML.Direccion direccion = new ML.Direccion();
                            ML.Colonia colonia = new ML.Colonia();
                            direccion.IdDireccion = obj.IdDireccion;
                            direccion.Calle = obj.Calle;
                            direccion.NumeroExterior = obj.NumeroExterior;
                            direccion.NumeroInterior = obj.NumeroInterior;
                          



                            result.Objects.Add(direccion);

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
