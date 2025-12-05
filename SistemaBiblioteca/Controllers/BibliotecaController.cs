using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Data;
using SistemaBiblioteca.Dtos;
using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase {
        private readonly AppDbContext _appDbContext;

        public LivrosController(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddLivro([FromBody] LivroDTO livroDto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var autor = await _appDbContext.Autores.FirstOrDefaultAsync(a => a.Nome.ToLower() == livroDto.NomeAutor.ToLower());
            if (autor == null) {
                autor = new Autor { Nome = livroDto.NomeAutor };
                _appDbContext.Autores.Add(autor);
                await _appDbContext.SaveChangesAsync();
            }
            var novoLivro = new Livro {
                Titulo = livroDto.Titulo,
                AnoPublicacao = livroDto.AnoPublicacao,
                AutorId = autor.Id
            };
            _appDbContext.Livros.Add(novoLivro);
            await _appDbContext.SaveChangesAsync();

            var retorno = new LivroRetornoDto {
                Id = novoLivro.Id,
                Titulo = novoLivro.Titulo,
                AnoPublicacao = novoLivro.AnoPublicacao,
                NomeAutor = autor.Nome
            };

            return CreatedAtAction(nameof(GetIdLivro), new { id = novoLivro.Id }, retorno);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroRetornoDto>>> GetLivros() {
            var livros = await _appDbContext.Livros
                .Include(l => l.Autor)
                .Select(l => new LivroRetornoDto {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    AnoPublicacao = l.AnoPublicacao,
                    NomeAutor = l.Autor.Nome
                })
                .ToListAsync();

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroRetornoDto>> GetIdLivro(int id) {
            var livro = await _appDbContext.Livros
                .Include(l => l.Autor)
                .Where(l => l.Id == id)
                .Select(l => new LivroRetornoDto {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    AnoPublicacao = l.AnoPublicacao,
                    NomeAutor = l.Autor.Nome
                })
                .FirstOrDefaultAsync();

            if (livro == null) {
                return NotFound("Livro não encontrado!");
            }

            return Ok(livro);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<ActionResult<LivroRetornoDto>> GetTituloLivro(string titulo) {
            if (string.IsNullOrEmpty(titulo)) return BadRequest("Informe o título.");

            var livro = await _appDbContext.Livros
                .Include(l => l.Autor)
                .Where(l => l.Titulo.ToLower() == titulo.ToLower())
                .Select(l => new LivroRetornoDto {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    AnoPublicacao = l.AnoPublicacao,
                    NomeAutor = l.Autor.Nome
                })
                .FirstOrDefaultAsync();

            if (livro == null) return NotFound("Livro não encontrado!");

            return Ok(livro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, [FromBody] LivroDTO livroDto) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var livroExistente = await _appDbContext.Livros.FindAsync(id);
            if (livroExistente == null) return NotFound("Livro não encontrado!");

            var autor = await _appDbContext.Autores
                .FirstOrDefaultAsync(a => a.Nome.ToLower() == livroDto.NomeAutor.ToLower());

            if (autor == null) {
                autor = new Autor { Nome = livroDto.NomeAutor };
                _appDbContext.Autores.Add(autor);
                await _appDbContext.SaveChangesAsync();
            }

            livroExistente.Titulo = livroDto.Titulo;
            livroExistente.AnoPublicacao = livroDto.AnoPublicacao;
            livroExistente.AutorId = autor.Id;

            await _appDbContext.SaveChangesAsync();

            var retorno = new LivroRetornoDto {
                Id = livroExistente.Id,
                Titulo = livroExistente.Titulo,
                AnoPublicacao = livroExistente.AnoPublicacao,
                NomeAutor = autor.Nome
            };

            return Ok(retorno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id) {
            var livro = await _appDbContext.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (livro == null) return NotFound("Livro não encontrado!");

            _appDbContext.Livros.Remove(livro);
            await _appDbContext.SaveChangesAsync();

            var retorno = new {
                Mensagem = "Livro deletado com sucesso!",
                LivroApagado = livro.Titulo,
                AutorApagado = livro.Autor.Nome
            };

            return Ok(retorno);
        }
    }
}