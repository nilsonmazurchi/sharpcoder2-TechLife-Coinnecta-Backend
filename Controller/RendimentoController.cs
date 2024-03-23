using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Globalization; 


[ApiController]
[Route("rendimento")]
public class RendimentoController : ControllerBase
{
    private readonly RendimentoServico _rendimentoServico;

    private readonly IMapper _mapper;

    public RendimentoController(RendimentoServico rendimentoServico, IMapper mapper)
    {
        _rendimentoServico = rendimentoServico;
        _mapper = mapper;
    }

    [HttpGet("obter")]
    public async Task<ActionResult<double>> ObterRendimento([FromQuery]string dataFinal)
    {
        try
        {
            double rendimento = await _rendimentoServico.ObterRendimento(dataFinal);
            
            return Ok(rendimento/10000);
            
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro ao obter o rendimento: {ex.Message}");
        }
    }
}
