using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.gerir.api.Dominios;
using senai.gerir.api.Interfaces;
using senai.gerir.api.Repositorios;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai.gerir.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController()
        {
            _tarefaRepositorio = new TarefaRepositorio();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                c => c.Type == JwtRegisteredClaimNames.Jti
                            );
                tarefa.UsuarioId = new System.Guid(usuarioid.Value);

                _tarefaRepositorio.Cadastrar(tarefa);

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{IdTarefa}")]
        public IActionResult Editar (Guid IdTarefa, Tarefa tarefa)
        {
            try
            {
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var tarefaexiste = _tarefaRepositorio.BuscarPorId(IdTarefa);

                if (tarefaexiste == null)
                    return NotFound();

                if (tarefaexiste.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");
                tarefa.Id = IdTarefa;
                _tarefaRepositorio.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("{IdTarefa}")]
        public IActionResult Remover (Guid IdTarefa)
        {
            try
            {
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(
                                 c => c.Type == JwtRegisteredClaimNames.Jti
                             );

                var tarefaexiste = _tarefaRepositorio.BuscarPorId(IdTarefa);

                if (tarefaexiste == null)
                    return NotFound();

                if (tarefaexiste.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                _tarefaRepositorio.Remover(IdTarefa);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("status/{id}")]
        public IActionResult AlteraStatus (Guid IdTarefa)
        {
            try
            {
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var tarefa = _tarefaRepositorio.BuscarPorId(IdTarefa);

                if (tarefa == null)
                    return NotFound();

                if (tarefa.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                _tarefaRepositorio.AlteraStatus(IdTarefa);

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult ListarTodos(Guid IdUsuario)
        {
            try
            {
                var claimsUsuario = HttpContext.User.Claims;

                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var usuario = new System.Guid(usuarioid.Value);

                return Ok(_tarefaRepositorio.ListarTodos(usuario));
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{IdTarefa}")]
        public IActionResult BuscarPorId(Guid IdTarefa)
        {
            try
            {
                var usuarioid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var tarefa = _tarefaRepositorio.BuscarPorId(IdTarefa);

                if (tarefa == null)
                    return NotFound();

                if (tarefa.UsuarioId != new Guid(usuarioid.Value))
                    return Unauthorized("Usuário não tem permissão");

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
