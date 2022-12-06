
ReadFile();
Console.ReadKey();  

static void ReadFile()
{

    string file = @"C:\Users\digis\Documents\Alejandro Atento Lopez\LayoutUsuario.txt";
    if (File.Exists(file))
    {
        StreamReader TextFile = new StreamReader(file);
        string line;
        line = TextFile.ReadLine();

        ML.Result resulterror = new ML.Result();
        resulterror.Objects = new List<object>();
        while ((line = TextFile.ReadLine()) != null)
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
            usuario.IdRol = int.Parse(lines[11]);


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
}
    










