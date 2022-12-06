using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            ML.Result result1 = BL.Producto.GetAll();
            ML.Result result2 = BL.Proveedor.GetAll();
            producto.Departamento = new ML.Departamento();
            ML.Result result3 = BL.Departamento.GetAll();
            ML.Result result4 = new ML.Result();

            result1 = BL.Producto.GetAll();

            if (result1.Correct)
            {

                producto.Productos = result1.Objects;
                producto.Proveedor.Proveedors = result2.Objects;
                producto.Departamento.Departamentos = result3.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los productos";

            }
            return View("Modal");

        }


        [HttpGet]//muestra las vistas
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();
            ML.Proveedor proveedor = new ML.Proveedor();
            producto.Proveedor = new ML.Proveedor();
            ML.Departamento departamento = new ML.Departamento();
            producto.Departamento = new ML.Departamento();

            ML.Result resultprov = BL.Proveedor.GetAll();
            ML.Result resultdep = BL.Departamento.GetAll();


            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();



            if (IdProducto == null)
            {
                producto.Proveedor.Proveedors = resultprov.Objects;
                producto.Departamento.Departamentos = resultdep.Objects;
                return View(producto);


            }
            else
            {


                //GetbyId
                ML.Result result1 = BL.Usuario.GetById(IdProducto.Value);

                if (result1.Correct)
                {
                    producto = (ML.Producto)result1.Object;
                    producto.Proveedor.Proveedors = resultprov.Objects;
                    producto.Departamento.Departamentos = resultdep.Objects;
                    //usuario.Estado.Estados= resutEstados.Objects;

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al consultar el producto seleccionado";
                }
                return View(producto);
            }
        }
    }
}

    //    [HttpPost]
    //public ActionResult Form(ML.Producto producto)
    //{
    //    IFormFile image = Request.Form.Files["IFImage"];


    //    //valido si traigo imagen
    //    if (image != null)
    //    {
    //        //llamar al metodo que convierte a bytes la imagen
    //        byte[] ImagenBytes = ConvertToBytes(image);
    //        //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
    //        producto.Imagen = Convert.ToBase64String(ImagenBytes);
    //    }
    //    if (!ModelState.IsValid)
    //    {
    //        producto.Semestre = new ML.Semestre();
    //        ML.Result resultSemestre = BL.Semestre.GetAll();

    //        alumno.Semestre.Semestres = resultSemestre.Objects;
    //        alumno.Horario = new ML.Horario();
    //        alumno.Horario.Grupo = new ML.Grupo();
    //        alumno.Horario.Grupo.Plantel = new ML.Plantel();

    //        ML.Result resultPlanteles = BL.Plantel.GetAll();
    //        alumno.Horario.Grupo.Plantel.Planteles = resultPlanteles.Objects;
    //        return View(alumno);
    //    }
    //    else
    //    {


    //        ML.Result result = new ML.Result();

    //        if (alumno.IdAlumno == 0)
    //        {
    //            result = BL.Alumno.Add(alumno);

    //            if (result.Correct)
    //            {
    //                ViewBag.Message = "Alumno agregado correctamente";
    //            }
    //            else
    //            {
    //                ViewBag.Message = "Ocurrio un error al agregar al alumno" + result.ErrorMessage;
    //            }

    //        }
    //        else
    //        {
                //result = BL.Alumno.Update(alumno);

                //if (result.Correct)
                //{
                //    ViewBag.Message = "Alumno actualizado correctamente";
                //}
                //else
                //{
                //    ViewBag.Message = "Ocurrio un error al actualizar al alumno" + result.ErrorMessage;
                //}

//            }
//            return View("Modal");
//        }
//    }
//    public static byte[] ConvertToBytes(IFormFile imagen)
//    {

//        using var fileStream = imagen.OpenReadStream();

//        byte[] bytes = new byte[fileStream.Length];
//        fileStream.Read(bytes, 0, (int)fileStream.Length);

//        return bytes;
//    }
//}
