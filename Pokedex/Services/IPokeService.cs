using Pokedex.Models;

namespace Pokedex.Services;

// Dentro da interface definimos os métodos que queremos possuir em nosso serviço
public interface IPokeService
{
    // Para buscar todos os Pokemons
    List<Pokemon> GetPokemons();
    
    // Para buscar todos os Tipo
    List<Tipo> GetTipos();

    // Para buscar um Pokemon especifico por seu Número
    Pokemon GetPokemon(int Numero);

    // Para retornar um Objeto do tipo PokedexDto, com os tipos e pokemons
    PokedexDto GetPokedexDto();

    // Para retornar um Objeto do tipo DetailDto, com um pokemon especificao
    // O anterior e o próximo, como está no DetailDto
    DetailsDto GetDetailedPokemon(int Numero);

    // Para retornar um Tipo especifico por seu Nome
    Tipo GetTipo(string Nome);
}
