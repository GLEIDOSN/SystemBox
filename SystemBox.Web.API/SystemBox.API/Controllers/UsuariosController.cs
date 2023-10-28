using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemBox.API.Dtos;
using SystemBox.Domain.Models;
using SystemBox.Service.Services;

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
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        // api/usuarios POST
        [HttpPost("Post")]
        public async Task<ActionResult<UsuarioDto>> Post(Usuario usuario)
        {
            await _usuarioService.PostAsync(usuario);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuarioDto);
        }

        // api/usuarios/1 PUT
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(int id, Usuario usuarioInput)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.UpdateAsync(id, usuarioInput);

            return NoContent();
        }

        // api/usuarios/1 DELETE
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _usuarioService.DeleteAsync(id);

            return NoContent();
        }
    }
}
