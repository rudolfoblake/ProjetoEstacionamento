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
    
    public class VeiculoController : ControllerBase
    {
        private readonly IRepository _repositorio;

        public VeiculoController(IRepository repositorio)
        {
            _repositorio = repositorio;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repositorio.GetAllVeiculosAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{veiculoId}")]
        public async Task<IActionResult> Get(int veiculoId)
        {
            try
            {
                var result = await _repositorio.GetCarbyId(veiculoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPut("{veiculoId}")]
        public async Task<IActionResult> Put(int veiculoId, Veiculo veiculo)
        {
            try
            {
                var veiculoCadastrado = await _repositorio.GetCarbyId(veiculoId);
                if (veiculoCadastrado == null)
                {
                    return NotFound();
                }
                _repositorio.Update(veiculo);

                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(veiculo);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Veiculo veiculo)
        {
            try
            {
                _repositorio.Add(veiculo);
                
                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(veiculo);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("{veiculoId}")]
        public async Task<IActionResult> Delete(int veiculoId)
        {
            try
            {
                var veiculo = await _repositorio.GetCarbyId(veiculoId);
                if (veiculo == null)
                {
                    return NotFound();
                }
                _repositorio.Delete(veiculo);

                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(
                        new {
                            message = "Deletado!!"
                        }
                    );
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