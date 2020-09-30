using System.Security.AccessControl;
using System;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Context;
using BackEnd.Services;
using System.Threading.Tasks;


namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class EstacionamentoController : ControllerBase
    {
        private readonly IRepository _repositorio;
        private readonly ICalculoEstacionamento _calculoServico;

        public EstacionamentoController(IRepository repositorio, ICalculoEstacionamento calculoServico)
        {
            _repositorio = repositorio;
            _calculoServico = calculoServico;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _repositorio.GetAllEstacionamentosAsync(true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet("{estacionamentoId}")]
        public async Task<IActionResult> GetById(int estacionamentoId)
        {
            try
            {
                var result = await _repositorio.GetEstacionamentoAsyncById(estacionamentoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
        
        [HttpGet("placa/{veiculoPlaca}")]
        public async Task<IActionResult> GetByVeiculoPlaca(string veiculoPlaca)
        {
            try
            {
                var result = await _repositorio.GetEstacionamentoAsyncByVeiculoPlaca(veiculoPlaca, true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPut("{estacionamentoId}")]
        public async Task<IActionResult> Put(int estacionamentoId, Estacionamento estacionamento)
        {
            try
            {                   
                var estacionamentoCadastrado = await _repositorio.GetEstacionamentoAsyncById(estacionamentoId);
                
                if (estacionamentoCadastrado == null)
                {
                    return NotFound();
                }

                estacionamento.DataSaida = DateTime.Now;

                estacionamento.ValorTotal = this._calculoServico.CalcularValor(estacionamentoCadastrado);
                            
                _repositorio.Update(estacionamento);

                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(estacionamento);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Estacionamento estacionamento)
        {
            try
            {
                var vigenciaCadastrada = await _repositorio.GetPrecoByVigencia(estacionamento.DataEntrada);
                if (vigenciaCadastrada == null)
                {
                    return BadRequest("Uma Vigencia deve ser cadastrada!");
                }
                
                estacionamento.PrecoId = vigenciaCadastrada.Id;

                _repositorio.Add(estacionamento);
                
                if (await _repositorio.SaveChangesAsync())
                {
                    return Ok(estacionamento);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
            return BadRequest();
        }

        [HttpDelete("{estacionamentoId}")]
        public async Task<IActionResult> Delete(int estacionamentoId)
        {
            try
            {
                var estacionamento = await _repositorio.GetEstacionamentoAsyncById(estacionamentoId);
                if (estacionamento == null)
                {
                    return NotFound();
                }
                _repositorio.Delete(estacionamento);

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