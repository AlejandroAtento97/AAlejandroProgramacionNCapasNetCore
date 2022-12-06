using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultrol = BL.Rol.GetAll();
            ML.Result result = new ML.Result();
            //result = BL.Usuario.GetAll(usuario);
            //"UrlAPI":"http://localhost:5136/api/",

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Usuario/GetAll/");


                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItem);
                        }

                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            if (result.Correct)
            {
                usuario.Rol.Rols = resultrol.Objects;

                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los alumnos";

            }
            return View("Modal");
        }


        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();

            ML.Result resultrol = BL.Rol.GetAll();
            result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Rols = resultrol.Objects;

                return View(usuario);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al consultar los usuarios";
                return PartialView("Modal");
            }
        }

        //---------------VISTA DEL FORMULARIO--------------//

        [HttpGet]//muestra las vistas
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Pais pais = new ML.Pais();
            usuario.Rol = new ML.Rol();


            ML.Result resultPaises = BL.Pais.GetAll();
            ML.Result resultrol = BL.Rol.GetAll();


            usuario.Rol = new ML.Rol();
            usuario.Pais = new ML.Pais();

            usuario.Rol = new ML.Rol();
            usuario.Pais = new ML.Pais();


            if (IdUsuario == null)
            {
                usuario.Rol.Rols = resultrol.Objects;
                usuario.Pais.Paises = resultPaises.Objects;
                return View(usuario);


            }
            else
            {


                //GetbyId
                ML.Result result1 = BL.Usuario.GetById(IdUsuario.Value);

                if (result1.Correct)
                {
                    usuario = (ML.Usuario)result1.Object;
                    usuario.Rol.Rols = resultrol.Objects;
                    usuario.Pais.Paises = resultPaises.Objects;
                     //usuario.Estado.Estados= resutEstados.Objects;

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al consultar el alummno seleccionado";
                }
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //ADD
            ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);
                usuario.Usuarios = result.Objects;

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Se ha registrado al usuario";
                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "No ha registrado al usuario" + result.ErrorMessage;

                }if (!ModelState.IsValid)
                {

                }

                return View(usuario);
              

            }
            return View(usuario);
        }
        public JsonResult CambiarStatus(int idUsuario, bool status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(idUsuario, status);

            return Json(result);
        }
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Usuario usuario1 = new ML.Usuario();
            ML.Result result = new ML.Result();
            result = BL.Usuario.Delete(usuario1);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha elimnado el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "No see ha elimnado el registro" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }

}  
    




    


           

    


