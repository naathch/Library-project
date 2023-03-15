using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Biblioteca_Final
{
    /*
     * Elaborado por:
     * @naathch
     */
    class Program
    {
        public struct Usuario //Se definen las variables a utilizar para el usuario.
        {
            public String nombre, apellido, direccion, email, telefono, código, contraseña;
            public StreamReader LeerUS;
            public StreamWriter EscribirUS;
        }

        public struct Libro //Se definen las variables a utilizar para los libros.
        {
            public String codigo, titulo, autor;
            public int copia, direccion;
            public StreamReader LeerLib;
            public StreamWriter EscLib;
        }

        /*public struct Prestamo //Se definen las variables a utilizar para el prestamo de libros.
        {
            public String codigUS, CodLib;
            public int copyn;
            public Double mora;
            public DateTime[] fechas;
            public StreamReader LeerPrestamos;
            public StreamWriter EscPrestamos;
        }*/

        public struct InfoSesion
        {
            public int nlinea;
            public bool chequeo;
        }

        const string APP_NOMBRE_FORMAL = "BIBLIOTECA";
        const string APP_ABREVIATURA = "BIB_U";
        const string APP_NOMBRE_CLAVE = "BUDB";

        static void Bienvenida()   // Muestra un mensaje de bienvenida 
        {
            Console.WriteLine("\n\n\n\n\n");
            Console.WriteLine("\t\t\t\t\t*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*");
            Console.WriteLine($"\t\t\t\t\tBienvenido a la {APP_NOMBRE_FORMAL}");
            Console.WriteLine($"\t\t\t\t\t(También conocido como {APP_ABREVIATURA} / {APP_NOMBRE_CLAVE})");
            Console.WriteLine("\t\t\t\t\t*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*");
            Console.Write("\n\n\tCargando");
            for (int i = 0; i <= 3; i++)
            {
                Console.Write(" .");
                Thread.Sleep(1000);
            }
        }

            static void Menu1()//Menu Principal
        {
            Console.Clear();
            Console.WriteLine("\t\t\t\t\tBIBLIOTECA UDB");
            Console.WriteLine("\n\t\tMENÚ PRINCIPAL: Identifiquese segun su tipo de usuario");
            Console.WriteLine("\n\t1.\tUsuario de la biblioteca");
            Console.WriteLine("\t2.\tBibliotecario o responsable");
            Console.WriteLine("\t3.\tSalir");
            Console.WriteLine("\t4.\tAcerca de");
            Console.Write("\n\n\tSeleccione su opción: ");
            int opc = int.Parse(Console.ReadLine());
            Opcion1(opc);
        }
        static void Menu2()//Menu para el usuario de la biblioteca
        {
            Console.Clear();
            Console.WriteLine("\tBIENVENIDO A LA BIBLIOTECA, LECTOR\n\n\tSelccione una opción");
            Console.WriteLine("\t1.\tRegistrarse\n\t2.\tIniciar sesión\n\t3.\tVer Libros disponibles\n\t4.\tVolver\n\t5.\tSalir");
            Console.Write("\n\n");
            Console.Write("\n\n\tSu opción es: ");
            int opc = int.Parse(Console.ReadLine());
            Opcion2(opc);
        }
        static void RegUsuarios()
        {
            int contador = 0, continuar = 1;
            Usuario user = new Usuario();
            while (continuar == 1)
            {
                Console.Clear();
                Console.Write("\t¡BIENVENIDO AL REGISTRO DE USUARIOS!\n\n");
                user.nombre = Nombre();
                user.apellido = Apellido();
                user.email = Correo();
                user.direccion = Direccion();
                user.telefono = Telefono();
                user.código = CodigoUS();
                user.contraseña = Contraseña();
                user.EscribirUS = new StreamWriter("usuarios.txt", true);
                user.EscribirUS.WriteLine("\n{0} {1} Correo: {2} Dirección: {3} Teléfono: {4}", user.nombre, user.apellido, user.email, user.direccion, user.telefono);
                user.EscribirUS.WriteLine(user.código);
                user.EscribirUS.WriteLine(user.contraseña);
                Console.WriteLine("\n\n\t¡Se ha registrado con exito!");
                Console.WriteLine("\n\t¿Desea registrar algun otro usuario adicional?");
                Console.Write("\n\tDigite 1 para registrar otro usuario o 2 para volver al menu principal: ");
                continuar = int.Parse(Console.ReadLine());
                if (continuar == 1)
                {
                    contador++;
                }
                else
                {
                    user.EscribirUS.Close();
                    Console.WriteLine("\n\tPresione cualquier tecla para regresar al Menú principal");
                    Console.ReadKey();
                    Menu1();
                }
            }
        }
        static void Menu3()//Menú para el personal de la biblioteca
        {
            Console.Write("\n\n\tBienvenido Bibliotecario");
            Console.Write("\n\tIntroduzca su usuario de acceso: ");
            String UsAcceso = Console.ReadLine();
            Console.Write("\n\tIntroduzca el código de acceso: ");
            String access = Asterisk('*');
            if (access == "admin" && UsAcceso == "Biblio 1")//la contraseña para ingresar es 123456
            {
                Console.Clear();
                Console.WriteLine("\n\t\tAcceso otorgado\n\tSeleccione una opción\n\n\t1.\tRegistrar un nuevo usuario");
                Console.WriteLine("\t2.\tRegistrar un nuevo libro\n\t3.\tVer usuarios registrados\n\t4.\tVer libros registrados\n\t5.\tVolver\n\t6.\tSalir");
                Console.Write("\n\n\tSu opción es: ");
                int opc = int.Parse(Console.ReadLine());
                Opcion3(opc);
            }
            else
            {
                Console.Write("\n\n\tEl código de acceso o la contraseña son incorrectos!");
            }
        }
        static void RegLibro()
        {
            int contador = 0; 
            int continuar = 1;
            Libro libro = new Libro();
            while (continuar == 1)
            {
                Console.Clear();
                Console.Write("\tREGISTRO DE LIBROS\n\n");
                libro.titulo = Titulo();
                libro.autor = Autor();
                libro.direccion = Ubicacion();
                libro.codigo = CodLibro();
                libro.copia = CopLibro(libro.codigo);
                libro.EscLib = new StreamWriter("librosregistrados.txt", true);
                libro.EscLib.WriteLine("Titulo: {0} Autor: {1} Localizacion en la estantería: {2}", libro.titulo, libro.autor, libro.direccion);
                libro.EscLib.WriteLine("Codigo: {0} Copia: {1}", libro.codigo, libro.copia);
                Console.WriteLine("\n\n\t¡Se ha registrado el libro satisfactoriamente!");
                Console.WriteLine("\n\t¿Desea registrar otro libro?");
                Console.Write("\n\tDigite 1 para registrar otro libro o 2 para volver al menu principal: ");
                continuar = int.Parse(Console.ReadLine());
                if (continuar == 1)
                {
                    contador++;
                }
                else
                {
                    libro.EscLib.Close();
                    Console.WriteLine("\n\tPresione cualquier tecla para regresar al Menú principal");
                    Console.ReadKey();
                    Menu1();
                }
            }
        }
 
        static void Opcion1(int op)//switch sobre el menu general
        {
            switch (op)
            {
                case 1:
                    Menu2();
                    break;
                case 2:
                    Menu3();
                    break;
                case 3:
                    Salir();
                    break;
                case 4:
                    AcercaDe();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tEsa opción no está disponible. Por favor seleccione un número del 1 al 4");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Thread.Sleep(3000);
                    Menu1();
                    break;
            }
        }
        static void Opcion2(int op)//switch sobre el menu del usuario de la biblioteca
        {
            switch (op)
            {
                case 1:
                    RegUsuarios();
                    break;
                case 2:
                    Sesion();
                    break;
                case 3:
                    MostrarLibros();
                    break;
                case 4:
                    Menu1();
                    break;
                case 5:
                    Salir();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tEsa opción no está disponible. Por favor seleccione un número del 1 al 5");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Thread.Sleep(3000);
                    Menu1();
                    break;
            }
        }
        static void Opcion3(int op)//switch del personal de la biblioteca
        {
            switch (op)
            {
                case 1:
                    RegUsuarios();
                    break;
                case 2:
                    RegLibro();
                    break;
                case 3:
                    MostrarUsuarios();
                    break;
                case 4:
                    MostrarLibros();
                    break;
                case 5:
                    Menu1();
                    break;
                case 6:
                    Salir();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tEsa opción no está disponible. Por favor seleccione un número del 1 al 6");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Thread.Sleep(3000);
                    Menu1();
                    break;
            }
        }
        static void Sesion()//pantalla inicio de sesión
        {
            Console.Clear();
            Usuario user = new Usuario();
            InfoSesion sesion = new InfoSesion();
            user.LeerUS = new StreamReader("usuarios.txt", true);
            FileInfo arch = new FileInfo("usuarios.txt");
            if (arch.Length == 0)
            {
                Console.WriteLine("\tAun no hay usuarios registrados");
                Console.WriteLine("\tPresione cualquier tecla para volver...");
                Console.ReadKey();
                Menu2();
            }
            else
            {
                Console.WriteLine("\tIniciar Sesion...");
                Console.Write("\n\tIngrese su usuario: ");
                string usuario = "Codigo: " + Console.ReadLine();
                Console.Write("\n\tIngrese su contraseña: ");
                string contra = "@Contra: " + Asterisk('*');
                sesion = VerificarSesion(usuario, contra);
                if (sesion.chequeo == true)
                {
                    user.LeerUS.Close();
                    PantallaPerfil(sesion.nlinea);
                }
                else
                {
                    Console.Write("\n\n\n\tLa contraseña o usuario ingresado son incorrectos\n\tPresione 1 para intentarlo de nuevo o 0 para salir: ");
                    int opc = int.Parse(Console.ReadLine());
                    if (opc == 1)
                    {
                        Console.WriteLine("\tPor favor espere...");
                        Thread.Sleep(2000);
                        Sesion();
                    }
                    else
                        Menu2();
                }
            }
        }
        static InfoSesion VerificarSesion(string usuario, string contra)
        {
            Usuario check = new Usuario();
            InfoSesion sesion = new InfoSesion();
            check.LeerUS = new StreamReader("usuarios.txt");
            string[] lineas = File.ReadAllLines("usuarios.txt");
            for (int i = 0; i < lineas.Length; i++)//verifica cada linea del archivo/vector
            {
                if (lineas[i] == usuario && VerificarContra(contra, i + 1) == true)
                {
                    sesion.nlinea = i;//guarda la linea donde encontro el usuario i+1 donde esta la contraseña
                    sesion.chequeo = true;
                    break;
                }
                else
                {
                    sesion.chequeo = false;
                }
            }
            return sesion;
        }
        static bool VerificarContra(String contra, int nlinea)
        {
            string[] lineas = File.ReadAllLines("usuarios.txt");
            if (lineas[nlinea] == contra)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void PantallaPerfil(int nlinea)
        {
            Console.Clear();
            string[] lineas = File.ReadAllLines("usuarios.txt");
            string nombre = lineas[nlinea - 2];
            Console.WriteLine("\n\t¡Sesión iniciada con éxito!");
            Console.WriteLine("\n\tCargando...");
            Thread.Sleep(5000);
            Console.Clear();
            Console.WriteLine("\n\t¡Bienvenido a su perfil " + nombre);
            Console.WriteLine("\tElija que desea hacer: ");
            Console.WriteLine("\n\t1.\tVer mis datos\n\t2.\tRegistrar un préstamo\n\t3.\tVer préstamos pendientes\n\t4.\tDevolver libro\n\t5.\tSalir");
            Console.Write("\n\t\tSu opción: ");
            int opc = int.Parse(Console.ReadLine());
            PerfilUS(opc, nlinea);
        }
        static void PerfilUS(int option, int nlinea)
        {
            switch (option)
            {
                case 1:
                    InfoUS(nlinea);
                    break;
               /* case 2:
                    prestamo();
                    break;
                case 3:
                    VerPres();
                    break;
                case 4:
                    LibrosReg();
                    break;*/
                case 5:
                    Menu1();
                    break;
            }
        }
        static void InfoUS(int nlinea)
        {
            String[] lineas = File.ReadAllLines("usuarios.txt");
            Console.WriteLine("\tSus datos son: ");
            Console.WriteLine("\n\t" + lineas[nlinea - 1]);
            Thread.Sleep(5000);
            PantallaPerfil(nlinea);
        }
        static String Nombre() 
        {
            Console.Write("\n\tNombre(s): ");
            String nomb = Console.ReadLine();
            return nomb;
        }
        static String Apellido()
        {
            Console.Write("\n\tApellido(s): ");
            String last = Console.ReadLine();
            return last;
        }
        static String Direccion()
        {
            Console.Write("\n\tDirección (municipio de residencia): ");
            String direccion = Console.ReadLine();
            return direccion;
        }
        static String Correo()
        {
            Console.Write("\n\tDirección de e-mail: ");
            String correo = Console.ReadLine();
            if (correo.Contains("@") == false || correo.Contains(".") == false || correo.Length < 5 || correo.EndsWith(".") == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\tLa direccion de email ingresada es invalida");
                Console.ForegroundColor = ConsoleColor.Black;
                Correo();
            }
            return correo;
        }
        static String Telefono()
        {
            Console.Write("\n\tNumero de teléfono (incluya el código de país): ");
            String telefono = Console.ReadLine();
            if (telefono.Length < 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tNúmero de teléfono inválido!");
                Console.ForegroundColor = ConsoleColor.Black;
                Telefono();
            }
            return telefono;
        }
        static String CodigoUS() //Determina el codigo del usuario y revisa si ya esta en uso
        {
            Console.Write("\n\tCódigo de usuario (Debe de tener 8 caracteres): ");
            String codigo = Console.ReadLine(), Linea;
            Usuario check = new Usuario();
            check.LeerUS = new StreamReader("usuarios.txt", true);
            if (codigo.Length != 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\tEl codigo ingresado no es valido");
                Console.ForegroundColor = ConsoleColor.Black;
                CodigoUS();
            }
            else
            {
                while ((Linea = check.LeerUS.ReadLine()) != null)
                {
                    if (Linea == codigo)
                    {
                        Console.WriteLine("\t¡Ese Código de usuario ya está en uso!");
                        CodigoUS();
                    }
                    else
                    {
                        continue;
                    }
                }
                check.LeerUS.Close();
                return codigo;
            }
            return codigo;
        }
        static String Contraseña()
        {
            Console.Write("\n\tContraseña: ");
            string contrasena = Asterisk('*');
            return contrasena;
        }
        static String Asterisk(char asterisco)
        {
            int num = 0;
            string contra = "";
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    return contra;
                }
                if (info.Key == ConsoleKey.Backspace)
                {
                    if (num > 0)
                    {
                        contra = contra.Substring(0, contra.Length - 1);
                        Console.Write("\b \b");
                        num--;
                    }
                }
                else if ((info.KeyChar > ' ') && (info.KeyChar < '\x007f'))
                {
                    Console.Write(asterisco);
                    contra = contra + info.KeyChar.ToString();
                    num++;
                }
            }
        }

        static String Titulo()
        {
            Console.Write("\n\tTitulo del libro: ");
            String nombTi = Console.ReadLine();
            return nombTi;
        }
        static String Autor()
        {
            Console.Write("\n\tNombre y apellido del autor: ");
            String nomb = Console.ReadLine();
            return nomb;
        }
        static int Ubicacion()
        {
            Console.Write("\n\tNumero de estanteria donde se ubica el libro: ");
            int nomb = int.Parse(Console.ReadLine());
            return nomb;
        }
        static String CodLibro()
        {
            Console.Write("\n\tCódigo de libro (Debe contener especificamente 8 caracteres) : ");
            String codigo = ("Codigo: " + Console.ReadLine()), Linea;
            Libro check = new Libro();
            check.LeerLib = new StreamReader("librosregistrados.txt", true);
            if (codigo.Length != 16)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\tEl codigo ingresado no es valido");
                Console.ForegroundColor = ConsoleColor.Black;
                CodLibro();
            }
            else
            {
                while ((Linea = check.LeerLib.ReadLine()) != null)
                {
                    if (Linea == codigo)
                    {
                        Console.WriteLine("\t¡Ese Código de libro ya está en uso!");
                        CodLibro();
                    }
                    else
                    {
                        continue;
                    }
                }
                check.LeerLib.Close();
                return codigo;
            }
            return codigo;
        }

        static int CopLibro(String codigo)
        {
            Libro libro = new Libro();
            libro.LeerLib = new StreamReader("librosregistrados.txt", true);
            Console.Write("\n\tNumero de copia (1-3): ");
            int ncopia = int.Parse((Console.ReadLine()));
            if (ncopia < 1 || ncopia > 3)//verifica numero de copia
            {
                Console.WriteLine("\tEl numero de copia ingresado no es valido\n\tPor favor ingrese un numero entre 1 y 3");
                CopLibro(codigo);
                return ncopia;
            }
            else
            {
                String check = ("Codigo: {0}" + codigo + "Copia: " + ncopia), Linea;
                while ((Linea = libro.LeerLib.ReadLine()) != null)//verifica que el numero de copia de el libro no este en uso
                {
                    if (Linea == check)
                    {
                        Console.WriteLine("\tEl numero de copia de ese libro ya esta registrada");
                        CopLibro(codigo);
                    }
                    else
                    {
                        continue;
                    }
                }
                libro.LeerLib.Close();
                return ncopia;
            }
        }

        static String UserCode()
        {
            return "codigo";
        }
        static String CodLib()
        {
            Console.Write("\n\tEscriba el código del libro: ");
            String bcode = Console.ReadLine();
            return bcode;
        }
        static int CopLib()
        {
            Console.Write("\n\tEscriba el número de copia del libro: ");
            int numb = int.Parse(Console.ReadLine());
            if (numb > 3 || numb <= 0)
            {
                Console.WriteLine("\tNúmero de copia inválido");
                Thread.Sleep(2000);
                CopLib();
                return numb;
            }
            else
            {
                return numb;
            }
        }
        /*static DateTime FechaPres()
        {
            Console.Write("\n\tEscriba la fecha del préstamo (DD/MM/AAAA): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            //DateTime date = DateTime.Now;
            return date;
        }
        static DateTime RegresarFecha()
        {
            Console.Write("\n\tEscriba la fecha que se devolverá (DD/MM/AAAA): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine()); //esta es la variable capturada año,dia mes
            return date;
        }
        static DateTime Entrega()
        {
            Console.Write("\n\tEscriba la fecha de entrega (DD/MM/AAAA): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            return date;
        }
        static Double CalcMora(DateTime date1, DateTime date2)//date1 es la de devolucion date2 es la entrega
        {
            Double mora = ((date2 - date1).TotalDays) * 0.35;
            return mora; 
        }

        static void prestamo() //Registro de prestamos
        {
            int contador = 1, continuar = 1;
            Prestamo reg = new Prestamo();
            while (contador == 1 && contador <= 3 && continuar == 1)
            {
                Console.Clear();
                Console.Write("\t¡BIENVENIDO AL REGISTRO DE PRÉSTAMOS!\n\n");
                Console.WriteLine("\tPrestamos registrados: {0}", contador);
                reg.usercode = UserCode();
                reg.bookcode = CodLib();
                reg.copyn = CopLib();
                reg.fechas[0] = DateTime.Now;
                reg.fechas[1] = RegresarFecha();
                if (reg.fechas[0] < reg.fechas[1])
                {
                    reg.EscPrestamos = new StreamWriter("prestamos.txt", true);
                    reg.EscPrestamos.WriteLine("Usuario:{0}", reg.usercode);
                    reg.EscPrestamos.WriteLine("Libro:{0} Copia:{1}", reg.bookcode, reg.copyn);
                    reg.EscPrestamos.WriteLine("Fecha de entrega: {0}", reg.fechas[1]);
                    Console.WriteLine("\n\n\t¡Escritura existosa!");
                    Console.WriteLine("\n\t¿Desea registrar otro prestamo?");
                    Console.Write("\n\tDigite 1 para registrar otro: ");
                    continuar = int.Parse(Console.ReadLine());
                    contador++;
                }
                else
                {
                    Console.WriteLine("\n\tLas fechas indicadas no son correctas\n\tPresione cualquier tecla para volver a registrar el préstamo");
                    Thread.Sleep(3000);
                    prestamo();
                }
            }
            reg.EscPrestamos.Close();
            Console.WriteLine("\n\tPresione cualquier tecla para regresar al Menú principal");
            Console.ReadKey();
            Menu1();
        }

        /*static void LibroRegresado() 
        {
            Console.Clear();
            int continuar = 1, contador = 1;
            Prrestamo reg = new Prrestamo();
            reg.EscPrestamos.Close();
            reg.LeerPrestamos = new StreamReader("prestamos.txt", true);
            while (continuar == 1 && continuar <= 3)
            {
                Console.Clear();
                Console.Write("\tIngrese los datos de su devolución\n\n");
                reg.usercode = UserCode();
                reg.bookcode = CodLib();
                reg.copyn = CopLib();
            }
        }*/

        static void MostrarUsuarios() //Muestra los usuarios registrados
        {
            String Line;
            Usuario user = new Usuario();
            user.EscribirUS = new StreamWriter("usuarios.txt", true);
            user.EscribirUS.Close();
            user.LeerUS = new StreamReader("usuarios.txt", true);
            int contador = 0;
            if ((Line = user.LeerUS.ReadLine()) == null && contador == 0)
            {
                Console.WriteLine("\tNo hay usuarios registrados");
            }
            else
            {
                while ((Line = user.LeerUS.ReadLine()) != null)
                {
                    contador++;
                    if (contador % 3 == 0)
                    {
                        continue;//evita que se muestre la contraseña del usuario
                    }
                    else
                    {
                        Console.WriteLine("\t" + Line);
                    }
                }
            }
            user.LeerUS.Close();
            Console.WriteLine("\n\tPresione cualquier tecla para volver al Menú Principal...\n");
            Console.ReadKey();
            Menu1();
        }
        static void MostrarLibros() //Muestra los libros registrados
        {
            String Line;
            Libro book = new Libro();
            book.EscLib = new StreamWriter("librosregistrados.txt", true);
            book.EscLib.Close();
            book.LeerLib = new StreamReader("librosregistrados.txt", true);
            int contador = 0;
            if ((Line = book.LeerLib.ReadLine()) == null && contador == 0)
            {
                Console.WriteLine("\tNo hay libros registrados");
            }
            else
            {
                while ((Line = book.LeerLib.ReadLine()) != null)
                {
                    contador++;
                    Console.WriteLine("\t" + Line);
                }
            }
            book.LeerLib.Close();
            Console.WriteLine("\n\tPresione cualquier tecla para volver al Menú Principal...\n");
            Console.ReadKey();
            Menu1();
        }
        /*static void MostrarPrestamo() //Muestra los prestamos realizados
        {
            String Line;
            Prrestamo prest = new Prrestamo();
            prest.EscPrestamos = new StreamWriter("prestamos.txt", true);
            prest.EscPrestamos.Close();
            prest.LeerPrestamos = new StreamReader("prestamos.txt", true);
            int contador = 0;
            while ((Line = prest.LeerPrestamos.ReadLine()) != null)
            {
                Console.WriteLine(Line);
                contador++;
            }
            prest.LeerPrestamos.Close();
        }*/

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Title = "Biblioteca";
            Bienvenida();
            Menu1();
        }
        static void Salir()
        {
            Console.Beep(440, 225);
            Console.Beep(392, 225);
            Console.WriteLine("\n\n-->> FIN DEL PROGRAMA");
            Console.WriteLine("\n\n\t¡ADIÓS!");
            Console.ReadKey();
        }
        static void AcercaDe()
        {
            Console.Clear();
            Console.WriteLine("\t\tACERCA DE: ");
            Console.WriteLine("\n\tDesarrollado por:");
            Console.WriteLine("\n\n\@naathch");
            Console.ReadKey();
            Menu1();
        }
    }
}
