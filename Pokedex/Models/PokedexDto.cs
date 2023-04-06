namespace Pokedex.Models;

// Este DTO será utilizado pela página Index.cshtml
public class PokedexDto
{
    // Propriedade com uma Lista de Tipos para gerar botões de todos os tipos
    public List<Tipo> Tipos { get; set; }
    // Propriedade com uma Lista de Pokemons para exibição na página
    public List<Pokemon> Pokemons { get; set; }
}
