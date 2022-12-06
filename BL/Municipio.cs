using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Municipio
    {
        public static ML.Result GetAll(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var municipios = context.Municipios.FromSqlRaw("[MunicipioGetByIdEstado]").ToList();

                    result.Objects = new List<object>();
                    if (municipios != null)
                    {
                        foreach (var obj in municipios)
                        {
                           

                            ML.Estado estado = new ML.Estado();
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.NombreMunicipio = obj.Nombre;
                            //municipio.Estado = new ML.Estado();
                            //municipio.Estado.Nombre = obj.Nombre;



                            result.Objects.Add(municipio);

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
