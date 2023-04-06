namespace Pokedex.Models;

// Este DTO, será utilizado pela página Details, possuindo o Pokemon clicado
// pelo usuário, além do Pokemon anterior e posterior ao clicado
public class DetailsDto
{
    // Pokemon Anterior
    public Pokemon Prior { get; set; }
    // Pokemon Atual
    public Pokemon Current { get; set; }
    // Pokemon Posterior
    public Pokemon Next { get; set; }
}