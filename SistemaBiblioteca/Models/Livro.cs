using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SistemaBiblioteca.Models {
    public class Livro {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }

        public int AnoPublicacao { get; set; }

        public int AutorId { get; set; }

        [JsonIgnore] 
        public Autor Autor { get; set; }
    }
}
