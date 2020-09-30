using System.Security.AccessControl;
using System;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Context;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PrecoController : ControllerBase
    {
        private readonly IRepository _repositorio;

        public PrecoController(IRepository repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repositorio.GetAllPrecosAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{precoId}")]
        public async Task<IActionResult> Get(int precoId)
        {
            try
            {
                var result = await _repositorio.GetPrecoAsyncById(precoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }       
        
        [HttpPost]
        public async Task<IActionResult> Post(Preco preco)
        {
            try
            {
                var vigenciaPreco = await _repositorio.GetPrecoByVigencia(preco.VigenciaInicial);
                
                if (vigenciaPreco != null)
                {
                    return BadRequest("Tabela de preços ja cadastrada com essa vigência");
                }
                _repositorio.Add(preco);
                
                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(preco);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }       

    }
}