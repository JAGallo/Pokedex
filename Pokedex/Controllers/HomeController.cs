using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Services;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    // Vamos adicionar um novo objeto, do serviço PokeService
    private readonly IPokeService _pokeService;
    private readonly ILogger<HomeController> _logger;

    // Agora vamos usar a injeção de dependencia para incluir o serviço no controller
    public HomeController(ILogger<HomeController> logger, IPokeService pokeService)
    {
        _logger = logger;
        _pokeService = pokeService;
    }

    // Agora precisamos alterar o método Index, para enviar os dados a View
    // Vamos incluir tb um valor de filtro, que será utilizado posteriormente
    // Para filtrar os pokemons por tipo
    public IActionResult Index(string tipo)
    {
        // usamos o método GetPokedexDto do serviço, que já carrega duas listas
        // de pokemons e tipos
        var pokes = _pokeService.GetPokedexDto();
        // Caso o tipo tenha valor nulo ou vazio, a ViewData["filter"] recebe o 
        // valor "all" para exibir todos os pokemons, caso contrario
        // recebe o valor do parametro tipo, que será um dos tipos do arquivo json
        ViewData["filter"] = string.IsNullOrEmpty(tipo) ? "all" : tipo;
        return View(pokes);
    }

    // Vamos adicionar uma nova Action, que será responsavel por abrir a página
    // de detalhes com o pokemon clicado pelo usuário
    // Vamos incluir no método um valor de rota, que será o número do pokemon
    public IActionResult Details(int Numero)
    {
        // Usando o Numero, trazemos o Pokemon daquele número com o evento
        // GetDetailedPokemon do serviço
        var pokemon = _pokeService.GetDetailedPokemon(Numero);
        return View(pokemon);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
