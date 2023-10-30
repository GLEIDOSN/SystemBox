using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemBox.API.Dtos;
using SystemBox.Domain.Consts;
using SystemBox.Domain.Models;
using SystemBox.Service.Services;
using static System.CustomExceptions.UsuariosCustomExceptions;

namespace SystemBox.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        // api/usuarios GET
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();

            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);

            return Ok(usuariosDto);
        }

        // api/usuarios/1 GET
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UsuarioDto>> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                return Ok(usuarioDto);
            }
            catch (UsuarioNaoExiste ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MsgErrorsConst.ErroInternoServidor(ex.Message));
            }
        }

        // api/usuarios POST
        [HttpPost("Post")]
        public async Task<ActionResult<UsuarioDto>> Post(Usuario usuario)
        {
            try
            {
                await _usuarioService.PostAsync(usuario);
                var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuarioDto);
            }
            catch (UsuarioJaCadastradoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MsgErrorsConst.ErroInternoServidor(ex.Message));
            }            
        }

        // api/usuarios/1 PUT
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, Usuario usuarioInput)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                await _usuarioService.UpdateAsync(id, usuarioInput);

                return NoContent();
            }
            catch (UsuarioNaoExiste ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MsgErrorsConst.ErroInternoServidor(ex.Message));
            }
        }

        // api/usuarios/1 DELETE
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);

                return NoContent();
            }
            catch (UsuarioNaoExiste ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, MsgErrorsConst.ErroInternoServidor(ex.Message));
            }
        }
    }
}
