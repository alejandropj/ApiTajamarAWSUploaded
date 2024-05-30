using ApiTajamarAWS.Data;
using Microsoft.EntityFrameworkCore;
using NugetApiPracticasTajamarJRP.Models;
using System.Text;

namespace ApiTajamarAWS.Repositories
{
    public class RepositoryUsuarios
    {
        private TajamarContext context;

        public RepositoryUsuarios(TajamarContext context)
        {
            this.context = context;
        }


        //SEGURIDAD
        //public async Task<Usuario> LogInUsuarioAsync(string mail, int usuario)
        //{
        //    return await this.context.Usuarios.Where(x => x.IdUsuario == usuario && x.Email == mail).FirstOrDefaultAsync();
        //}

        public async Task<UsuarioEmpresa> LogInUsuarioAsync(string mail, int password)
        {
           

            // Buscar al usuario por correo electrónico y comparar las contraseñas como arreglos de bytes
            return await this.context.Usuarios
                .Where(x => x.Email == mail && x.IdUsuario == password)
                .FirstOrDefaultAsync();

        }

        //METODOS

        public async Task<List<UsuarioEmpresa>> GetUsuariosAsync()
        {
            return await this.context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioEmpresa> FindUsuarioAsync(int idUsuario)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
        }

        public async Task<UsuarioEmpresa> FindUsuarioByEmailAsync(string email)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(z => z.Email == email);
        }

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await
                    this.context.Usuarios.MaxAsync(z => z.IdUsuario) + 1;
            }
        }


        public async Task InsertUsuarioAsync(int idUsuario, int? idClase, string nombre, string role, string linkedin, string email, int? e1, int? e2, int? e3, int? e4, int? e5, int? e6)
        {
            UsuarioEmpresa usuario = new UsuarioEmpresa();
            usuario.IdUsuario = await GetMaxIdUsuarioAsync();
            usuario.IdClase = idClase;
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Role = "Alumno";
            usuario.Linkedin = linkedin;
            usuario.Emp_1Id = e1;
            usuario.Emp_2Id = e2;
            usuario.Emp_3Id = e3;
            usuario.Emp_4Id = e4;
            usuario.Emp_5Id = e5;
            usuario.Emp_6Id = e6;
            this.context.Usuarios.Add(usuario);
            await this.context.SaveChangesAsync();
        }


        public async Task UpdateUsuarioAsync(int idUsuario, int? idClase, string nombre, string role, string linkedin, string email, int? e1, int? e2, int? e3, int? e4, int? e5, int? e6)
        {
            UsuarioEmpresa usuario = await this.FindUsuarioAsync(idUsuario);
            usuario.IdUsuario = idUsuario;
            usuario.IdClase = idClase;
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Role = role;
            usuario.Linkedin = linkedin;
            usuario.Emp_1Id = e1;
            usuario.Emp_2Id = e2;
            usuario.Emp_3Id = e3;
            usuario.Emp_4Id = e4;
            usuario.Emp_5Id = e5;
            usuario.Emp_6Id = e6;
            await this.context.SaveChangesAsync();
        }


        public async Task DeleteUsuarioAsync(int id)
        {
            UsuarioEmpresa usuario = await this.FindUsuarioAsync(id);
            this.context.Usuarios.Remove(usuario);
            await this.context.SaveChangesAsync();
        }



        public async Task<object> Perfil(int idUsuario)
        {
            // Busca el usuario por su ID
            UsuarioEmpresa usuario = await this.FindUsuarioAsync(idUsuario);

            // Crea un objeto para almacenar los datos del usuario y los nombres de las empresas asociadas
            var perfil = new
            {
                Usuario = usuario,
                Empresas = new List<string>()
            };

            // Obtén el nombre de cada empresa asociada al usuario y agrégalo a la lista
            if (usuario.Emp_1Id != null)
            {
                Empresa empresa1 = await context.Empresas.FindAsync(usuario.Emp_1Id);
                perfil.Empresas.Add(empresa1.Nombre);
            }
            if (usuario.Emp_2Id != null)
            {
                Empresa empresa2 = await context.Empresas.FindAsync(usuario.Emp_2Id);
                perfil.Empresas.Add(empresa2.Nombre);
            }
            if (usuario.Emp_3Id != null)
            {
                Empresa empresa3 = await context.Empresas.FindAsync(usuario.Emp_3Id);
                perfil.Empresas.Add(empresa3.Nombre);
            }
            if (usuario.Emp_4Id != null)
            {
                Empresa empresa4 = await context.Empresas.FindAsync(usuario.Emp_4Id);
                perfil.Empresas.Add(empresa4.Nombre);
            }
            if (usuario.Emp_5Id != null)
            {
                Empresa empresa5 = await context.Empresas.FindAsync(usuario.Emp_5Id);
                perfil.Empresas.Add(empresa5.Nombre);
            }
            if (usuario.Emp_6Id != null)
            {
                Empresa empresa6 = await context.Empresas.FindAsync(usuario.Emp_6Id);
                perfil.Empresas.Add(empresa6.Nombre);
            }
            // Repite el proceso para las otras empresas asociadas
            // (Puedes repetir este bloque de código para cada empresa asociada)

            // Devuelve el objeto con los datos del usuario y los nombres de las empresas asociadas
            return perfil;
        }


        public async Task<List<UsuarioEmpresa>> GetUsuariosPorIdClase(int idClase)
        {
            // Utiliza LINQ para filtrar los usuarios por el ID de la clase
            var usuarios = await this.context.Usuarios
                .Where(u => u.IdClase == idClase)
                .ToListAsync();

            return usuarios;
        }


        public async Task<List<UsuarioEmpresa>> GetUsuariosPorEmpresa(int idEmpresa)
        {
            // Utiliza LINQ para filtrar los usuarios por el ID de la empresa en emp_1Id
            var usuarios = await this.context.Usuarios
                .Where(u => u.Emp_1Id == idEmpresa || u.Emp_2Id ==idEmpresa || u.Emp_3Id == idEmpresa || u.Emp_4Id == idEmpresa || u.Emp_5Id == idEmpresa || u.Emp_6Id == idEmpresa) 
                .ToListAsync();
            return usuarios;
        }

    }
}