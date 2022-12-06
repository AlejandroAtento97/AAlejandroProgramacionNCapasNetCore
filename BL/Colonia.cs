using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Colonia
    {
        public static ML.Result GetAll(int IdColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var colonias = context.Colonia.FromSqlRaw("[ColoniaGetAll]").ToList();

                    result.Objects = new List<object>();
                    if (colonias != null)
                    {
                        foreach (var obj in colonias)
                        {


                            ML.Colonia colonia = new ML.Colonia();
                            ML.Municipio municipio = new ML.Municipio();
                            colonia.IdColonia = int.Parse(obj.IdColonia.ToString());
                            colonia.Nombre = obj.Nombre;
                            colonia.CodigoPostal =obj.CodigoPostal;
                            //colonia.Municipio.IdMunicipio = int.Parse(obj.IdMunicipio.ToString());
                           



                            result.Objects.Add(colonia);

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
