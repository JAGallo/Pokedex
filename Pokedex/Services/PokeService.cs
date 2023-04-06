using System.Text.Json;
using Pokedex.Models;

namespace Pokedex.Services;

// Agora podemos clicar sobre IPokeService as tecla 'Ctrl + .' e selecionar
// a opção Implementar Interface para facilitar a codificação
public class PokeService : IPokeService
{
    // O Objeto _session é necessário para acessar a sessão 
    private readonly IHttpContextAccessor _session;
    // Estes arquivos textos, serão utilizados para informar o caminho dos
    // arquivos json
    private readonly string pokemonFile = @"Data\pokemons.json";
    private readonly string tiposFile = @"Data\tipos.json";

    // Método Construtor, para injetar (Injeção de Dependencia) a sessão na classe
    public PokeService(IHttpContextAccessor session)
    {
        _session = session;
        PopularSessao();
    }

    public List<Pokemon> GetPokemons()
    {
        // Aqui agora, vamos ler os dados da sessão e transforma em uma lista
        PopularSessao();
        // Ctrl + .  no erro JsonSerializer para adicionar o using automaticamente
        var pokemons = JsonSerializer.Deserialize<List<Pokemon>>(
            _session.HttpContext.Session.GetString("Pokemons")
        );
        return pokemons;
    }

    public List<Tipo> GetTipos()
    {
        // Aqui temos o mesmo processo que o GetPokemons
        PopularSessao();
        var tipos = JsonSerializer.Deserialize<List<Tipo>>(
            _session.HttpContext.Session.GetString("Tipos")
        );
        return tipos;
    }

    public Pokemon GetPokemon(int Numero)
    {
        // primeiro criar uma lista com os pokemons usando o método criado acima
        var pokemons = GetPokemons();
        // agora pesquisamos na lista o pokemon que tem o número igual ao parametro
        // (int Numero)
        return pokemons.Where(p => p.Numero == Numero).FirstOrDefault();
    }

    public PokedexDto GetPokedexDto()
    {
        // Aqui basta criar um PokedexDto, e preencher suas propriedades com 
        // os outros métodos da classe
        var pokes = new PokedexDto()
        {
            Pokemons = GetPokemons(),
            Tipos = GetTipos()
        };
        return pokes;
    }

    public DetailsDto GetDetailedPokemon(int Numero)
    {
        var pokemons = GetPokemons();
        // A ideia aqui é pesquisar o pokemon que tem o número do parametro
        // Ordenando por número com o OrderBy, buscar o que vem antes e depois do número
        var poke = new DetailsDto()
        {
            Current = pokemons.Where(p => p.Numero == Numero).FirstOrDefault(),
            Prior = pokemons.OrderByDescending(p => p.Numero)
                    .FirstOrDefault(p => p.Numero < Numero),
            Next = pokemons.OrderBy(p => p.Numero)
                    .FirstOrDefault(p => p.Numero > Numero)
        };
        return poke;
    }

    public Tipo GetTipo(string Nome)
    {
        var tipos = GetTipos();
        return tipos.Where(t => t.Nome == Nome).FirstOrDefault();
    }

    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(_session.HttpContext.Session.GetString("Tipos")))
        {
            // Caso não exista a sessão Tipos, então são criados dois elementos na
            // sessão, um com o nome Pokemons e os dados do arquivo pokemons.json
            // outro com o nome Tipos e os dados do arquivo tipos.json
            _session.HttpContext.Session.SetString("Pokemons", LerArquivo(pokemonFile));
            _session.HttpContext.Session.SetString("Tipos", LerArquivo(tiposFile));
        }
    }

    private string LerArquivo(string fileName)
    {
        // O Objeto leitor, irá realizar através do método ReadToEnd,
        // ler os dados dos arquivo informado e devolver no formato texto
        using (StreamReader leitor = new StreamReader(fileName))
        {
            string dados = leitor.ReadToEnd();
            return dados;
        }
    }

}
