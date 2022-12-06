using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var departamentos = context.Departamentos.FromSqlRaw("[DepartamentoGetAll]").ToList();

                    result.Objects = new List<object>();

                    if (departamentos != null)
                    {
                        foreach (var obj in departamentos)
                        {


                          
                            ML.Departamento departamento = new ML.Departamento();


                            departamento.IdDepartamento = int.Parse(obj.IdDepartamento.ToString());
                            departamento.Nombre = obj.Nombre;
                            //departamento.Area.IdArea= int.Parse(obj.IdArea.ToString()); ;
                            result.Objects.Add(departamentos);
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
