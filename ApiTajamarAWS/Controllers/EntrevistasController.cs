using ApiTajamarAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NugetApiPracticasTajamarJRP.Models;

namespace ApiTajamarAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrevistasController : ControllerBase
    {
        private RepositoryEntrevista repo;

        public EntrevistasController(RepositoryEntrevista repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Obtiene todas las entrevistas de los alumnos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<EntrevistaAlumno>>> GetEntrevistas()
        {
            return await this.repo.GetEntrevistasAsync();
        }

        /// <summary>
        /// Busca una entrevista por su identificador único.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EntrevistaAlumno>> FindEntrevista(int id)
        {
            return await this.repo.FindEntrevistaAsync(id);
        }

        /// <summary>
        /// Inserta una nueva entrevista en la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostEntrevista(EntrevistaAlumno entrevistaAlumno)
        {
            await this.repo.InsertEntrevistaAsync(entrevistaAlumno.IdentEntrevista, entrevistaAlumno.IdAlumno, entrevistaAlumno.IdEmpresa, entrevistaAlumno.FechaEntrevista, entrevistaAlumno.Estado);
            return Ok();
        }

        /// <summary>
        /// Elimina una entrevista de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntrevista(int id)
        {
            if (await this.repo.FindEntrevistaAsync(id) == null)
            {
                //NO EXISTE LA ENTREVISTA PARA ELIMINARLA
                return NotFound();
            }
            else
            {
                await this.repo.DeleteEntrevistaAsync(id);
                return Ok();
            }
        }

        /// <summary>
        /// Actualiza los datos de una entrevista en la base de datos.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> PutEntrevista(EntrevistaAlumno entrevistaAlumno)
        {
            await this.repo.UpdateEntrevistaAsync(entrevistaAlumno.IdentEntrevista, entrevistaAlumno.IdAlumno, entrevistaAlumno.IdEmpresa, entrevistaAlumno.FechaEntrevista, entrevistaAlumno.Estado);
            return Ok();
        }

        /// <summary>
        /// Obtiene todas las entrevistas de un usuario.
        /// </summary>
        [HttpGet("entrevistasPorUsuario/{idUsuario}")]
        public async Task<ActionResult<List<EntrevistaAlumno>>> GetEntrevistasPorUsuario(int idUsuario)
        {
            try
            {
                // Llama al método GetEntrevistasPorUsuario del repositorio de entrevistas
                var entrevistas = await repo.GetEntrevistasPorUsuario(idUsuario);
                return Ok(entrevistas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
