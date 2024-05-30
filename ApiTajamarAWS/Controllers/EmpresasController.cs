using ApiTajamarAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using NugetApiPracticasTajamarJRP.Models;
namespace ApiTajamarAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private RepositoryEmpresa repo;

        public EmpresasController(RepositoryEmpresa repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Obtiene todas las empresas.
        /// </summary>
        
        [HttpGet]
        public async Task<ActionResult<List<Empresa>>> GetEmpresas()
        {
            return await this.repo.GetEmpresasAsync();
        }

        /// <summary>
        /// Busca una empresa por su identificador único.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> FindEmpresa(int id)
        {
            return await this.repo.FindEmpresaAsync(id);
        }

        /// <summary>
        /// Inserta una nueva empresa en la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostEmpresa(Empresa empresa)
        {
            await this.repo.InsertEmpresaAsync(empresa.IdEmpresa, empresa.Nombre, empresa.Linkedin, empresa.Imagen, empresa.Plazas, empresa.PlazasDisponibles);
            return Ok();
        }

        /// <summary>
        /// Elimina una empresa de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmpresa(int id)
        {
            if (await this.repo.FindEmpresaAsync(id) == null)
            {
                //NO EXISTE LA EMPRESA PARA ELIMINARLA
                return NotFound();
            }
            else
            {
                await this.repo.DeleteEmpresaAsync(id);
                return Ok();
            }
        }

        /// <summary>
        /// Actualiza los datos de una empresa en la base de datos.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> PutEmpresa(Empresa empresa)
        {
            await this.repo.UpdateEmpresaAsync(empresa.IdEmpresa, empresa.Nombre, empresa.Linkedin, empresa.Imagen, empresa.Plazas, empresa.PlazasDisponibles);
            return Ok();
        }

        /// <summary>
        /// devuelve las empresas
        /// </summary>
        [HttpGet("EmpresasSeleccionadas")]
        public async Task<ActionResult<List<Empresa>>> EmpresasSeleccionadas(
            [FromQuery] int? idempresa1,
            [FromQuery] int? idempresa2,
            [FromQuery] int? idempresa3,
            [FromQuery] int? idempresa4,
            [FromQuery] int? idempresa5,
            [FromQuery] int? idempresa6)
        { 
            List<Empresa> empresas = new List<Empresa>();

            if (idempresa1.HasValue)
            {
                var empresa1 = this.repo.GetEmpresaById(idempresa1.Value);
                if (empresa1 != null)
                {
                    empresas.Add(empresa1);

                }
            }

            if (idempresa2.HasValue)
            {
                var empresa2 = this.repo.GetEmpresaById(idempresa2.Value);
                if (empresa2 != null)
                {
                    empresas.Add(empresa2);

                }
            }

            if (idempresa3.HasValue)
            {
                var empresa3 = this.repo.GetEmpresaById(idempresa3.Value);
                if (empresa3 != null)
                {
                    empresas.Add(empresa3);

                }
            }

            if (idempresa4.HasValue)
            {
                var empresa4 = this.repo.GetEmpresaById(idempresa4.Value);
                if (empresa4 != null)
                {
                    empresas.Add(empresa4);

                }
            }

            if (idempresa5.HasValue)
            {
                var empresa5 = this.repo.GetEmpresaById(idempresa5.Value);
                if (empresa5 != null)
                {
                    empresas.Add(empresa5);

                }
            }

            if (idempresa6.HasValue)
            {
                var empresa6 = this.repo.GetEmpresaById(idempresa6.Value);
                if (empresa6 != null)
                {
                    empresas.Add(empresa6);

                }
            }

            return empresas;
        }






    }
}
