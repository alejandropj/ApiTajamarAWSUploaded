using ApiTajamarAWS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NugetApiPracticasTajamarJRP.Models;
using System.ComponentModel;

namespace ApiTajamarAWS.Repositories
{
    public class RepositoryEntrevista
    {
        private TajamarContext context;

        public RepositoryEntrevista(TajamarContext context)
        {
            this.context = context;
        }

        public async Task<List<EntrevistaAlumno>> GetEntrevistasAsync()
        {
            return await this.context.EntrevistasAlumnos.ToListAsync();
        }

        public async Task<EntrevistaAlumno> FindEntrevistaAsync(int id)
        {
            return await this.context.EntrevistasAlumnos.FirstOrDefaultAsync(x => x.IdentEntrevista == id);
        }

        private async Task<int> GetMaxIdEntrevistaAsync()
        {
            if (this.context.EntrevistasAlumnos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.EntrevistasAlumnos.MaxAsync(z => z.IdentEntrevista) + 1;
            }
        }

        public async Task InsertEntrevistaAsync(int id, int idalumno, int idempresa, DateTime? fecha, string estado)
        {
            EntrevistaAlumno entrevistaAlumno = new EntrevistaAlumno();
            int idmax = await GetMaxIdEntrevistaAsync();
            entrevistaAlumno.IdentEntrevista = idmax;
            entrevistaAlumno.IdAlumno = idalumno;
            entrevistaAlumno.IdEmpresa = idempresa;
            entrevistaAlumno.FechaEntrevista = fecha;
            entrevistaAlumno.Estado = estado;
            this.context.EntrevistasAlumnos.Add(entrevistaAlumno);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEntrevistaAsync(int id, int idalumno, int idempresa, DateTime? fecha, string estado)
        {
            EntrevistaAlumno entrevistaAlumno = await this.FindEntrevistaAsync(id);
            entrevistaAlumno.IdentEntrevista = id;
            entrevistaAlumno.IdAlumno = idalumno;
            entrevistaAlumno.IdEmpresa = idempresa;
            entrevistaAlumno.FechaEntrevista = fecha;
            entrevistaAlumno.Estado = estado;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteEntrevistaAsync(int id)
        {
            EntrevistaAlumno entrevistaAlumno = await this.FindEntrevistaAsync(id);
            this.context.EntrevistasAlumnos.Remove(entrevistaAlumno);
            await this.context.SaveChangesAsync();
        }
        public async Task<List<EntrevistaAlumno>> GetEntrevistasPorUsuario(int idUsuario)
        {
            // Utiliza LINQ para filtrar las entrevistas por el ID del usuario
            var entrevistas = await this.context.EntrevistasAlumnos
                .Where(e => e.IdAlumno == idUsuario)
                .ToListAsync();

            return entrevistas;
        }


    }
}