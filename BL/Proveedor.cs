using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var proveedores = context.Proveedors.FromSqlRaw("[ProveedorGetAll]").ToList();

                    result.Objects = new List<object>();

                    if (proveedores != null)
                    {
                        foreach (var obj in proveedores)
                        {


                            ML.Producto producto = new ML.Producto();
                            ML.Proveedor proveedor = new ML.Proveedor();
                            ML.Departamento departamento = new ML.Departamento();


                            proveedor.IdProveedor = int.Parse(obj.IdProveedor.ToString());
                            proveedor.Telefono = obj.Telefono;
                           
                            result.Objects.Add(proveedores);
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
