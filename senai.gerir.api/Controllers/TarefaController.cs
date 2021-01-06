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

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa)
        {
            try
            {
                _tarefaRepositorio.Cadastrar(tarefa);

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Editar (Tarefa tarefa)
        {
            try
            {
                _tarefaRepositorio.Editar(tarefa);

                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{IdTarefa}")]
        public IActionResult Remover (Guid IdTarefa)
        {
            try
            {
                _tarefaRepositorio.Remover(IdTarefa);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("status")("{IdTarefa}")]
        public IActionResult AlteraStatus (Guid IdTarefa)
        {
            try
            {
                _tarefaRepositorio.AlteraStatus(IdTarefa);
                return Ok();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Lista")]
        public IActionResult ListarTodos(Guid IdUsuario)
        {
            try
            {
                var claimsUsuario = HttpContext.User.Claims;

                var usuarioid = claimsUsuario.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);

                var usuario = new Guid(usuarioid.Value);

                return Ok(_tarefaRepositorio.ListarTodos(usuario));
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{IdTarefa}")]
        public IActionResult BuscarPorId(Guid IdTarefa)
        {
            try
            {
                var tarefa = _tarefaRepositorio.BuscarPorId(IdTarefa);
                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
