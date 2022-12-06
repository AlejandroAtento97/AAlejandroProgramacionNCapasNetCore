using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AalejandroProgramacionNcapasContext context = new DL.AalejandroProgramacionNcapasContext())
                {

                    var productos = context.Productos.FromSqlRaw("[ProductoGetAll]").ToList();

                    result.Objects = new List<object>();

                    if (productos != null)
                    {
                        foreach (var obj in productos)
                        {
                            

                            ML.Producto producto1 = new ML.Producto();
                            ML.Proveedor proveedor = new ML.Proveedor();
                            ML.Departamento departamento = new ML.Departamento();
                            

                            producto1.IdProducto = int.Parse(obj.IdProducto.ToString());
                            producto1.Nombre = obj.Nombre;
                            producto1.PrecioUnitario = obj.PrecioUnitario;
                            producto1.Stock = obj.Stock;
                            producto1.Proveedor = new ML.Proveedor();                           
                            producto1.Proveedor.IdProveedor = int.Parse(obj.IdProveedor.ToString());
                            producto1.Proveedor.Nombre = obj.NombreProveedor;
                            producto1.Departamento=new ML.Departamento();
                            producto1.Departamento.IdDepartamento = int.Parse(obj.IdDepartamento.ToString());
                            producto1.Descripcion = obj.Descripcion;
                            //producto1.Imagen = obj.Imagen;
                           
                            producto1.Departamento.Nombre = obj.NombreDepartamento;
                            result.Objects.Add(producto1);
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
