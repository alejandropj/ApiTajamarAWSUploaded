using Microsoft.EntityFrameworkCore;
using NugetApiPracticasTajamarJRP.Models;

namespace ApiTajamarAWS.Data
{
	public class TajamarContext : DbContext
	{
		public TajamarContext(DbContextOptions<TajamarContext> options) : base(options)
		{
		}

		public DbSet<UsuarioEmpresa> Usuarios { get; set; }

		public DbSet<Empresa> Empresas { get; set; }

		public DbSet<EntrevistaAlumno> EntrevistasAlumnos { get; set; }


	}
}
