using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SystemBox.API.Dtos;
using SystemBox.API.Errors;
using SystemBox.Domain.Consts;
using SystemBox.Domain.Models;
using SystemBox.Service.Services;

namespace SystemBox.API.Controllers
{
    public class UsuariosController : BaseApiController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly IDadosPLCService _dadosPLCService;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioService usuarioService, ITokenService tokenService, IDadosPLCService dadosPLCService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _dadosPLCService = dadosPLCService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("ListaUsuarios")]
        public async Task<ActionResult<List<UsuarioDto>>> ListaUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();

            var usuarioDto = _mapper.Map<List<UsuarioDto>>(usuarios);

            return usuarioDto;
        }

        [Authorize]
        [HttpGet("getUsuarioCorrente")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioCorrente()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usuario = await _usuarioService.GetByEmailAsync(email);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return usuarioDto;
        }

        [Authorize]
        [HttpGet("emailExistente")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _usuarioService.GetByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _usuarioService.GetByNomeUsuarioAsync(loginDto.NomeUsuario);

            if (usuario == null) return Unauthorized(new ApiResponse(401));

            var result = await _usuarioService.CheckPasswordSignInAsync(usuario, loginDto.Password);

            if (!result) return BadRequest(UsuariosConsts.MsgUsuarioSenhaInvalida);

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            usuarioDto.Token = _tokenService.CreateToken(usuario);

            return usuarioDto;
        }

        [Authorize]
        [HttpPost("cadastrar")]
        public async Task<ActionResult<UsuarioDto>> Cadastrar(CadastroUsuarioDto cadastroUsuarioDto)
        {
            var usuario = new Usuario
            {
                Nome = cadastroUsuarioDto.Nome,
                Endereco = cadastroUsuarioDto.Endereco,
                Numero = cadastroUsuarioDto.Numero,
                Complemento = cadastroUsuarioDto.Complemento,
                Bairro = cadastroUsuarioDto.Bairro,
                Cidade = cadastroUsuarioDto.Cidade,
                Estado = cadastroUsuarioDto.Estado,
                CEP = cadastroUsuarioDto.CEP,
                Telefone = cadastroUsuarioDto.Telefone,
                Celular = cadastroUsuarioDto.Celular,
                UserName = cadastroUsuarioDto.UserName,
                PasswordHash = cadastroUsuarioDto.Senha,
                Email = cadastroUsuarioDto.Email
            };

            var result = await _usuarioService.PostAsync(usuario);

            var usuarioDto = _mapper.Map<UsuarioDto>(result);

            return usuarioDto;
        }

        [HttpPost("conectar")]
        public string Conectar()
        {
            int caixasAltas = _dadosPLCService.LerRegistroDeEntrada(0);
            int caixasBaixas = _dadosPLCService.LerRegistroDeEntrada(1);

            return $"Total de Caixas Altas: {caixasAltas}\r\nTotal de Caixas Baixas: {caixasBaixas}.";
        }
    }
}
