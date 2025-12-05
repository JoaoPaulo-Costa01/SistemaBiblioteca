using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaBiblioteca.Models {
    public class Autor {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do autor é obrigatório")]
        public string Nome { get; set; }

        [JsonIgnore]
        public List<Livro> Livros { get; set; }
    }
}
