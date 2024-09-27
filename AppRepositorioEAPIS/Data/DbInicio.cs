using AppRepositorioEAPIS.Models;
using AppRepositorioEAPIS.Data;
using Microsoft.EntityFrameworkCore;

namespace AppRepositorioEAPIS.Data
{
    public class DbInicio
    {
        public static void agregar(BDcontexto contexto)
        {
            // Verifica si ya existen registros en la base de datos
            if (contexto.Estudiantes.Any())
            {
                return;
            }

            List<Estudiante> estudiantes = new List<Estudiante>()
            {
                new Estudiante { Nombres = "Carlos", Apellidos = "Ramirez", Email = "carlosr@correo.com", Contraseña = "password123" },
                new Estudiante { Nombres = "Ana", Apellidos = "Gonzalez", Email = "anag@correo.com", Contraseña = "password456" },
                new Estudiante { Nombres = "Luis", Apellidos = "Martinez", Email = "luism@correo.com", Contraseña = "password789" },
                new Estudiante { Nombres = "Sofia", Apellidos = "Perez", Email = "sofiap@correo.com", Contraseña = "password012" },
                new Estudiante { Nombres = "Maria", Apellidos = "Garcia", Email = "mariag@correo.com", Contraseña = "password345" },
                new Estudiante { Nombres = "David", Apellidos = "Torres", Email = "davidt@correo.com", Contraseña = "password678" },
                new Estudiante { Nombres = "Pedro", Apellidos = "Hernandez", Email = "pedroh@correo.com", Contraseña = "password901" },
                new Estudiante { Nombres = "Lucia", Apellidos = "Lopez", Email = "lucial@correo.com", Contraseña = "password234" },
                new Estudiante { Nombres = "Jorge", Apellidos = "Diaz", Email = "jorge@correo.com", Contraseña = "password567" },
                new Estudiante { Nombres = "Elena", Apellidos = "Morales", Email = "elenam@correo.com", Contraseña = "password890" }
            };
            contexto.Estudiantes.AddRange(estudiantes);
            contexto.SaveChanges();

            List<Docente> docentes = new List<Docente>()
            {
                new Docente { Nombres = "Miguel", Apellidos = "Sanchez", Administrador = true, Email = "miguels@correo.com", Contraseña = "docente123" },
                new Docente { Nombres = "Gabriela", Apellidos = "Fernandez", Administrador = false, Email = "gabrielaf@correo.com", Contraseña = "docente456" },
                new Docente { Nombres = "Julio", Apellidos = "Rodriguez", Administrador = false, Email = "julior@correo.com", Contraseña = "docente789" },
                new Docente { Nombres = "Rosa", Apellidos = "Suarez", Administrador = false, Email = "rosas@correo.com", Contraseña = "docente012" },
                new Docente { Nombres = "Fernando", Apellidos = "Paredes", Administrador = false, Email = "fernandop@correo.com", Contraseña = "docente345" },
                new Docente { Nombres = "Raul", Apellidos = "Montes", Administrador = false, Email = "raulm@correo.com", Contraseña = "docente678" },
                new Docente { Nombres = "Patricia", Apellidos = "Vega", Administrador = false, Email = "patriciav@correo.com", Contraseña = "docente901" },
                new Docente { Nombres = "Carlos", Apellidos = "Mendoza", Administrador = false, Email = "carlosm@correo.com", Contraseña = "docente234" },
                new Docente { Nombres = "Sara", Apellidos = "Ramos", Administrador = false, Email = "sarar@correo.com", Contraseña = "docente567" },
                new Docente { Nombres = "Andres", Apellidos = "Gomez", Administrador = false, Email = "andresg@correo.com", Contraseña = "docente890" }
            };
            contexto.Docentes.AddRange(docentes);
            contexto.SaveChanges();

            List<Empresa> empresas = new List<Empresa>()
            {
                new Empresa { Nombre = "TechCorp", Direccion = "Av. Tecnologica 123", Telefono = "123456789", Email = "info@techcorp.com" },
                new Empresa { Nombre = "SoftSolutions", Direccion = "Calle Soluciones 456", Telefono = "987654321", Email = "contact@softsolutions.com" },
                new Empresa { Nombre = "InnovaTech", Direccion = "Av. Innovacion 789", Telefono = "123123123", Email = "support@innovatech.com" },
                new Empresa { Nombre = "GlobalCorp", Direccion = "Calle Global 012", Telefono = "321321321", Email = "global@globalcorp.com" },
                new Empresa { Nombre = "NetSolutions", Direccion = "Av. Redes 345", Telefono = "654654654", Email = "net@netsolutions.com" },
                new Empresa { Nombre = "FutureTech", Direccion = "Av. Futuro 678", Telefono = "456456456", Email = "future@futuretech.com" },
                new Empresa { Nombre = "SkySolutions", Direccion = "Calle Cielo 987", Telefono = "789789789", Email = "sky@skysolutions.com" },
                new Empresa { Nombre = "DataCorp", Direccion = "Av. Datos 741", Telefono = "852852852", Email = "data@datacorp.com" },
                new Empresa { Nombre = "SmartTech", Direccion = "Av. Inteligencia 963", Telefono = "963963963", Email = "smart@smarttech.com" },
                new Empresa { Nombre = "EcoSolutions", Direccion = "Calle Verde 852", Telefono = "741741741", Email = "eco@ecosolutions.com" }
            };
            contexto.Empresas.AddRange(empresas);
            contexto.SaveChanges();

            List<Practica> practicas = new List<Practica>()
            {
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Ramirez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Sanchez").DocenteID,
                    Especialidad="Programación",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "TechCorp").EmpresaID,
                    Nombre="PRACTICA N°1",
                    NroResolucion = "12345",
                    Descripcion = "Desarrollo de una aplicación web",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(1),
                    Url = "http://techcorp.com/app",
                    Progreso = 50,
                    Sugerencias = "Revisar estructura del código."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Gonzalez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Fernandez").DocenteID,
                    Especialidad="Gestión de procesos",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "SoftSolutions").EmpresaID,
                    Nombre="PRACTICA N°2",
                    NroResolucion = "67890",
                    Descripcion = "Análisis de redes y telecomunicaciones",
                    Estado = "Aprobado",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(2),
                    Url = "http://softsolutions.com/redes",
                    Progreso = 100,
                    Sugerencias = "Muy buen trabajo en el análisis de redes."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Martinez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Rodriguez").DocenteID,
                    Especialidad="Ingeniería Sofware",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "InnovaTech").EmpresaID,
                    Nombre="PRACTICA N°3",
                    NroResolucion = "11223",
                    Descripcion = "Diseño de interfaz UX/UI",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(1),
                    Url = "http://innovatech.com/ux",
                    Progreso = 35,
                    Sugerencias = "Mejorar la coherencia visual del diseño."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Ramirez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Sanchez").DocenteID,
                    Especialidad="Ingeniería Sofware",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "GlobalCorp").EmpresaID,
                    Nombre="PRACTICA N°3",
                    NroResolucion = "44556",
                    Descripcion = "Optimización de procesos de negocio",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(3),
                    Url = "http://globalcorp.com/procesos",
                    Progreso = 60,
                    Sugerencias = "Continuar con la optimización de flujo de trabajo."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Gonzalez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Fernandez").DocenteID,
                    Especialidad="Redes",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "NetSolutions").EmpresaID,
                    Nombre="PRACTICA N°4",
                    NroResolucion = "88900",
                    Descripcion = "Implementación de soluciones de red",
                    Estado = "Aprobado",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(2),
                    Url = "http://netsolutions.com/implementacion",
                    Progreso = 100,
                    Sugerencias = "Excelente implementación y documentación."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Lopez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Paredes").DocenteID,
                    Especialidad="Ingeniería Sofware",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "FutureTech").EmpresaID,
                    Nombre="PRACTICA N°5",
                    NroResolucion = "33445",
                    Descripcion = "Desarrollo de inteligencia artificial",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(1),
                    Url = "http://futuretech.com/ai",
                    Progreso = 45,
                    Sugerencias = "Seguir investigando técnicas de aprendizaje profundo."
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Diaz").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Montes").DocenteID,
                    Especialidad="Redes",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "SkySolutions").EmpresaID,
                    Nombre="PRACTICA N°6",
                    NroResolucion = "55678",
                    Descripcion = "Soluciones de almacenamiento en la nube",
                    Estado = "Aprobado",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(2),
                    Url = "http://skysolutions.com/cloud",
                    Progreso = 100,
                    Sugerencias = ""
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Hernandez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Vega").DocenteID,
                    Especialidad="Base de datos",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "DataCorp").EmpresaID,
                    Nombre="PRACTICA N°7",
                    NroResolucion = "99012",
                    Descripcion = "Análisis de big data",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(1),
                    Url = "http://datacorp.com/bigdata",
                    Progreso = 55,
                    Sugerencias = ""
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Perez").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Mendoza").DocenteID,
                    Especialidad="Inteligencia Artificial",
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "SmartTech").EmpresaID,
                    Nombre="PRACTICA N°8",
                    NroResolucion = "11224",
                    Descripcion = "Desarrollo de dispositivos IoT",
                    Estado = "En progreso",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(3),
                    Url = "http://smarttech.com/iot",
                    Progreso = 65,
                    Sugerencias = ""
                },
                new Practica
                {
                    EstudianteID = contexto.Estudiantes.First(e => e.Apellidos == "Torres").EstudianteID,
                    DocenteID = contexto.Docentes.First(d => d.Apellidos == "Ramos").DocenteID,
                    EmpresaID = contexto.Empresas.First(emp => emp.Nombre == "EcoSolutions").EmpresaID,
                    Especialidad="Gestión de proyectos",
                    Nombre="PRACTICA N°9",
                    NroResolucion = "77889",
                    Descripcion = "Energía renovable y tecnologías sostenibles",
                    Estado = "Aprobado",
                    FechaCreacion = DateTime.Now,
                    FechaAprobacion = DateTime.Now.AddMonths(1),
                    Url = "http://ecosolutions.com/energia",
                    Progreso = 100,
                    Sugerencias = ""
                }
            };

            contexto.Practicas.AddRange(practicas);
            contexto.SaveChanges();

            CrearProcedimientosAlmacenados(contexto);
        }

        private static void CrearProcedimientosAlmacenados(BDcontexto contexto)
        {
            //procedimiento almacenado para validar estudiante
            string spValidarEstudiante = @"
            CREATE OR ALTER PROCEDURE sp_ValidarEstudiante
            @Correo VARCHAR(100),
            @Contraseña VARCHAR(500)
            AS
            BEGIN
            IF EXISTS (SELECT * FROM Estudiante WHERE Email = @Correo AND Contraseña = @Contraseña)
                SELECT EstudianteID FROM Estudiante WHERE Email = @Correo AND Contraseña = @Contraseña;
            ELSE
                SELECT 0;
            END";

            contexto.Database.ExecuteSqlRaw(spValidarEstudiante); //crear el procedimiento
                                                                 
            //procedimiento almacenado para validar docente
            string spValidarDocente = @"
            CREATE OR ALTER PROCEDURE sp_ValidarDocente
            @Correo VARCHAR(100),
            @Contraseña VARCHAR(500)
            AS
            BEGIN
                IF EXISTS (SELECT * FROM Docente WHERE Email = @Correo AND Contraseña = @Contraseña)
                BEGIN
                    SELECT DocenteID, Administrador 
                    FROM Docente 
                    WHERE Email = @Correo AND Contraseña = @Contraseña;
                END
                ELSE
                BEGIN
                    SELECT 0 AS DocenteID, 0 AS Administrador;
                END
            END;";

            contexto.Database.ExecuteSqlRaw(spValidarDocente); //crear el procedimiento
        }
    }
}
