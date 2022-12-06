using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result1 = new ML.Result();
            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {
                    var roles = context.Rols.FromSqlRaw("[RolGetAll]").ToList();
                    result1.Objects = new List<object>();
                    if (roles != null)
                    {
                        foreach (var objRol in roles)
                        {

                            ML.Rol rol = new ML.Rol();
                            //rol.IdRol = int.Parse(objRol.IdRol.ToString());
                            rol.IdRol = objRol.IdRol;

                            rol.Nombre = objRol.Nombre;

                            result1.Objects.Add(rol);

                        }
                        result1.Correct = true;
                    }
                    else
                    {
                        result1.Correct = false;
                        result1.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result1.Correct = false;
                result1.ErrorMessage = ex.Message;
            }
            return result1;
        }
    }
}
