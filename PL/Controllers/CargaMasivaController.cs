using Microsoft.AspNetCore.Mvc;
using ML;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new Result();
            return View(result);
        }
        [HttpPost]
        public ActionResult CargaTXT()
        {


            IFormFile fileTXT = Request.Form.Files["archivoTXT"];


            if (fileTXT != null)
            {

                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                string line;
                line = Textfile.ReadLine();
                ML.Result resulterror = new ML.Result();
                resulterror.Objects = new List<object>();

                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.FechaNacimiento = lines[3];
                    usuario.Sexo = lines[4];
                    usuario.Curp = lines[5];
                    usuario.UserName = lines[6];
                    usuario.Email = lines[7];
                    usuario.Password = lines[8];
                    usuario.Telefono = lines[9];
                    usuario.Celular = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = Int32.Parse(lines[11]);
                    int.Parse(lines[11]);
                    //int.Parse(obj.IdRol.ToString());

                    ML.Result result = BL.Usuario.Add(usuario);

                    if (result.Correct == true)
                    {

                        Console.WriteLine("El usuario se a registrado");
                        Console.ReadKey();
                    }
                    else
                    {

                        if (!result.Correct)
                        {
                            Console.WriteLine("No se insertaron registros");
                            resulterror.Objects.Add("No se insertaron registros porque: " + result.ErrorMessage);

                        }
                    }
                    if (resulterror.Objects != null)
                    {

                    }
                    TextWriter texto = new StreamWriter(@"C:\Users\digis\Documents\Alejandro Atento Lopez\ErroresdeUsuario.txt");

                    foreach (string error in resulterror.Objects)
                    {
                        texto.WriteLine(error);
                    }
                    texto.Close();
                }

            }
            return PartialView("Modal");
        }
    

        [HttpPost]
        public ActionResult CargaMasiva(ML.Usuario usuario)
        {

            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result result = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                            //Se lee el archivo
                            ML.Result resultConvertExcel = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                            if (resultConvertExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {


                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);

                                }
                                return View("CargaMasiva", resultValidacion);
                            }

                            else
                            {
                                //error al leer el archivo
                                ViewBag.Message = "Ocurrio un error al leer el arhivo";
                                return View("Modal");
                            }
                        }
                    }
                }

                //crea la sesion 
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = _hostingEnvironment.WebRootPath + @"\Files\logErrores.txt";
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = " No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Se han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");

        }

    }
}
            
        
    



