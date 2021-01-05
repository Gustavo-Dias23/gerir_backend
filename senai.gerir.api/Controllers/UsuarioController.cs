using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.gerir.api.Dominios;
using senai.gerir.api.Interfaces;
using senai.gerir.api.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace senai.gerir.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _usuarioRepositorio.Cadastrar(usuario);

                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Logar(Usuario usuario)
        {
            try
            {
                var usuarioexiste = _usuarioRepositorio.Logar(usuario.Email, usuario.Senha);

                if (usuarioexiste == null)
                    return NotFound();

                var token = GerarJsonWebToken(usuarioexiste);
                return Ok(token); 
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message) ;
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult MeusDados()
        {
            try
            {
                var claimsUsuario = HttpContext.User.Claims;

                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var usuario = _usuarioRepositorio.BuscarPorId(new Guid(usuarioid.Value));

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult Editar(Usuario usuario)
        {
            try
            {
                var claimsUsuario = HttpContext.User.Claims;

                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                usuario.Id = new Guid(usuarioid.Value);

                _usuarioRepositorio.Editar(usuario);

                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Excluir()
        {
            try
            {
                var claimsUsuario = HttpContext.User.Claims;

                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                _usuarioRepositorio.Remover(new Guid(usuarioid.Value));

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        private string GerarJsonWebToken(Usuario usuario)
        {
            var chaveSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GerirChaveSeguranca"));
            var credenciais = new SigningCredentials(chaveSeguranca, SecurityAlgorithms.HmacSha256);

            var data = new[]
            {
                new Claim(JwtRegisteredClaimNames.FamilyName, usuario.Nome),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Tipo),
                new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                "gerir.com.br",
                "gerir.com.br",
                data,
                expires : DateTime.Now.AddDays(1),
                signingCredentials: credenciais
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
