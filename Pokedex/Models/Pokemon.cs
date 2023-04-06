namespace Pokedex.Models;

public class Pokemon
{
    // Atributos da Classe Pokemon - Caracteristicas dos Pokemons
    // baseado no arquivo pokemons.json
    public int Numero { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Especie { get; set; }
    public List<string> Tipo { get; set; }
    public double Altura { get; set; }
    public double Peso { get; set; }
    public string Imagem { get; set; }

    // Método Construtor, implementado para criar a lista de Tipos
    public Pokemon()
    {
        Tipo = new List<string>();
    }
}
