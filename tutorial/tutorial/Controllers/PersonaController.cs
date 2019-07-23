using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tutorial.Models;

namespace tutorial.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {

            try
            {
                
                using (BDtutorial bd = new BDtutorial()) 
                {

                    // De esta forma se mapean datos de distintas tablas usando consultas SQL
                    string sql = @"
                            select p.id as id, p.nombres, p.apellidos, p.sexo, p.edad, p.fechaRegistro, c.nombreCiudad as nombreCiudad
                            from Persona p
                            inner join Ciudad c on c.id = p.codCiudad"; // Termina la consulta SQL
                    return View(bd.Database.SqlQuery<PersonaCE>(sql).ToList()); // Muestra los datos encontrados a traves de la consulta SQL 
                    
                    // De esta forma se mapean datos de distintas tablas, segun el modelo fijado en PersonaCE (ver clase PersonaCE)
                    //var data = from p in bd.Persona
                    //           join c in bd.Ciudad on p.codCiudad equals c.id
                    //           select new PersonaCE()
                    //           {

                    //               id = p.id,
                    //               nombres = p.nombres,
                    //               apellidos = p.apellidos,
                    //               sexo = p.sexo,
                    //               edad = p.edad,
                    //               nombreCiudad = c.nombreCiudad,
                    //               fechaRegistro = p.fechaRegistro                                   

                    //           };

                    //List<Persona> listaPersonas= bd.Persona.Where(p=> p.edad>25).ToList(); // Muestra aquella personas que sean mayores de 25 años
                    //return View(data.ToList()); // Se pasan los datos buscados anteriormente a la vista a traves de var data!


                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult crearPersona() // Metodo para crear una persona en la lista
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]                      // Valida que el formulario que se esta enviando sea el correcto
        public ActionResult crearPersona(Persona p)
        {

            try
            {

                if (!ModelState.IsValid)
                    return View();

                using (BDtutorial bd=new BDtutorial())
                {

                    p.fechaRegistro = DateTime.Now;
                    bd.Persona.Add(p);
                    bd.SaveChanges(); // Guardo los datos de la nueva persona creada
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "Error al crear la persona " + ex.Message);
                return View();

            }
                        
        }

        public ActionResult listarCiudades()
        {

            try
            {

                using (BDtutorial bd = new BDtutorial())
                {

                    return PartialView(bd.Ciudad.ToList());

                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult editarPersona(int id)
        {
            try
            {
                using (BDtutorial bd = new BDtutorial())
                {
                    //Persona pe = bd.Persona.Where(p => p.id == id).FirstOrDefault(); // Esta busqueda se puede utilizar siempre
                    Persona per = bd.Persona.Find(id); // Esta busqueda solo se puede usar cuando hay una sola clave primaria en la tabla
                    return View(per);

                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editarPersona(Persona p)
        {

            try
            {

                if (!ModelState.IsValid)
                    return View();

                using (BDtutorial bd = new BDtutorial())
                {
                    Persona pe = bd.Persona.Find(p.id);

                    pe.nombres = p.nombres;
                    pe.apellidos = p.apellidos;
                    pe.sexo = p.sexo;
                    pe.edad = p.edad;

                    bd.SaveChanges(); // Guardo los cambios realizados sobre los datos de la persona

                    return RedirectToAction("Index");

                }
                    
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public ActionResult detallesPersona(int id)
        {

            try
            {

                using (BDtutorial bd = new BDtutorial())
                {

                    //Persona pe = bd.Persona.Where(p => p.id == id).FirstOrDefault(); // Esta busqueda se puede utilizar siempre
                    Persona per = bd.Persona.Find(id); // Esta busqueda solo se puede usar cuando hay una sola clave primaria en la tabla
                    return View(per);

                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public ActionResult eliminarPersona(int id)
        {

            try
            {

                using (BDtutorial bd = new BDtutorial())
                {

                    //Persona pe = bd.Persona.Where(p => p.id == id).FirstOrDefault(); // Esta busqueda se puede utilizar siempre
                    Persona per = bd.Persona.Find(id); // Esta busqueda solo se puede usar cuando hay una sola clave primaria en la tabla
                    bd.Persona.Remove(per); // Elimino la persona que encontre en la linea anterior

                    bd.SaveChanges(); // Guardo los cambios

                    return RedirectToAction("Index");

                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string nombreCiudad(int codCiudad) // Metodo para obtener el nombre de la Ciudad partiendo desde Persona. 
        {

            try
            {

                using (BDtutorial bd = new BDtutorial())
                {

                    return bd.Ciudad.Find(codCiudad).nombreCiudad;

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}