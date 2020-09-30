<h3>Projeto Estacionamento</h3>

<b>Solução do Desafio:</b><br>

Para elaboração do desafio, foram utilizados:

-Banco de dados: SQL Server<br>
-BackEnd: Asp/Net, C#<br>
-FrontEnd: Angular com bootstrap<br>

Visao Geral: Os registros sao cadastrados e buscados no banco de dados SQL Server, feito através de Migrations via api.
Api é responsavel por buscar e cadastrar as informaçoes no banco de dados, também é responsável por efetuar os calculos e o tratamento das informações recebidas.
No Front End o usario pode navegar entre as tabelas e listas, cadastrar e finalizar os estacionamentos, preços e veiculos.

As requisiçoes da API foram testadas via Postman, verificando o retorno das mesmas.

<h2>Solução das instruções do desafio</h2>

1. Será cobrado metade do valor da hora inicial quando o tempo total de permanência no
estacionamento for igual ou inferior a 30 minutos:

 public double CalcularValor(Estacionamento estacionamento) 
        {
            var valorDaHora = estacionamento.Preco.ValorHora;

            TimeSpan totalMinutosHoras = DateTime.Now.Subtract(estacionamento.DataEntrada);

            double valorMinuto = 0;

            if ((int)totalMinutosHoras.Minutes > 10)
            {
               valorMinuto = valorDaHora / 2;
            }
          
            return ((int)totalMinutosHoras.TotalHours * valorDaHora) + valorMinuto;
            
        }
        
2. Utilizar a placa do veículo como chave de busca.

Função do Controller/Estacionamento:

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
        
        
Função do Context/Repository:


public async Task<Estacionamento[]> GetEstacionamentoAsyncByVeiculoPlaca(string veiculoPlaca, bool includeVeiculo)
        {
            IQueryable<Estacionamento> query = _context.Estacionamento;            

            if (includeVeiculo)
            {
                query = query.Include(e => e.Veiculo);
            }

            query = query.AsNoTracking()
                        .OrderBy(e => e.Veiculo.Placa)
                        .Where(e => e.Veiculo.Placa == veiculoPlaca);
            return await query.ToArrayAsync();
        }


<h2>Considerações Finais</h2>

Para executar a aplicação devem-se utilizar os seguintes comandos:

ProjetoEstacionamento/backend : dotnet watch run 
<br>
ProjetoEstacionamento/frontend : npm start
<br>
Navegação: Via Chrome ou outro navegador, caminho: http://localhost:4200/






