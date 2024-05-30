using ApiTajamarAWS.Repositories;
using Microsoft.AspNetCore.Authorization;
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
    public class UsuariosController : ControllerBase
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>

        [HttpGet]
        public async Task<ActionResult<List<UsuarioEmpresa>>> GetUsuarios()
        {
            return await this.repo.GetUsuariosAsync();
        }

        /// <summary>
        /// Busca un usuario por su identificador único.
        /// </summary>
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEmpresa>> FindUsuario(int id)
        {
            return await this.repo.FindUsuarioAsync(id);
        }

        /// <summary>
        /// Busca un usuario por su email.
        /// </summary>
    
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UsuarioEmpresa>> FindUsuarioByEmail(string email)
        {
            return await this.repo.FindUsuarioByEmailAsync(email);
        }
        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        
        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioEmpresa usuario)
        {
            await this.repo.InsertUsuarioAsync(usuario.IdUsuario, usuario.IdClase, usuario.Nombre, usuario.Role, usuario.Linkedin, usuario.Email, usuario.Emp_1Id, usuario.Emp_2Id, usuario.Emp_3Id, usuario.Emp_4Id, usuario.Emp_5Id, usuario.Emp_6Id);
            return Ok();
        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            if (await this.repo.FindUsuarioAsync(id) == null)
            {
                //NO EXISTE EL USUARIO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeleteUsuarioAsync(id);
                return Ok();
            }
        }

        /// <summary>
        /// Actualiza los datos de un usuario en la base de datos.
        /// </summary>
        
        [HttpPut]
        public async Task<ActionResult> PutUsuario(UsuarioEmpresa usuario)
        {
            await this.repo.UpdateUsuarioAsync(usuario.IdUsuario, usuario.IdClase, usuario.Nombre, usuario.Role, usuario.Linkedin, usuario.Email, usuario.Emp_1Id, usuario.Emp_2Id, usuario.Emp_3Id, usuario.Emp_4Id, usuario.Emp_5Id, usuario.Emp_6Id);
            return Ok();
        }

        /// <summary>
        /// Obtiene el perfil de un usuario, incluyendo los nombres de las empresas asociadas.
        /// </summary>
       
        [HttpGet("perfil/{idUsuario}")]
        public async Task<ActionResult<object>> Perfil(int idUsuario)
        {
            try
            {
                var perfil = await repo.Perfil(idUsuario);
                return Ok(perfil);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios que pertenecen a una clase específica.
        /// </summary>
        [Authorize]
        [HttpGet("usuariosPorIdClase/{idClase}")]
        public async Task<ActionResult<List<UsuarioEmpresa>>> GetUsuariosPorIdClase(int idClase)
        {
            try
            {
                var usuarios = await repo.GetUsuariosPorIdClase(idClase);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios que tienen asignada una empresa específica en la primera columna.
        /// </summary>
        
        [HttpGet("usuariosPorEmpresa/{idEmpresa}")]
        public async Task<ActionResult<List<UsuarioEmpresa>>> GetUsuariosPorEmpresa(int idEmpresa)
        {
            try
            {
                var usuarios = await repo.GetUsuariosPorEmpresa(idEmpresa);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

