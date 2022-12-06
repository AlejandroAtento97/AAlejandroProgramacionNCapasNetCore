using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
      //GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario =new ML.Usuario();
             usuario.Rol=new ML.Rol();
            usuario.Pais=new ML.Pais();
            usuario.Estado=new ML.Estado();
            usuario.Municipio=new ML.Municipio();
            usuario.Colonia=new ML.Colonia();
            usuario.Direccion=new ML.Direccion();   

            ML.Result result =  BL.Usuario.GetAll(usuario);
           
            if (result.Correct)
            {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll(string? nombre, string? ap, string? am)
        {

            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (nombre == null) ? "" : nombre;
            usuario.ApellidoPaterno = (ap == null) ? "" : ap;
            usuario.ApellidoMaterno = (am == null) ? "" : am;

            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

            // GET api/<UsuarioController>/5
            [HttpGet("GetById/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
        {
            ML.Usuario usuario= new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Pais = new ML.Pais();
            usuario.Estado = new ML.Estado();
            usuario.Municipio = new ML.Municipio();
            usuario.Colonia = new ML.Colonia();
            usuario.Direccion = new ML.Direccion();
            ML.Result result= BL.Usuario.GetById(idUsuario);

            if (result.Correct) {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int IdUsuario)
        {
        ML.Usuario usuario = new ML.Usuario();
        ML.Result result = new ML.Result();
        result = BL.Usuario.Delete(usuario);
            if (result.Correct){
            
                return Ok(result);
            }else

                return NotFound();

        }
       
    }
}
