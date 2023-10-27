using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tarefas.Models
{
    [Table("tarefas")]
    public class Tarefa
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime data { get; set; }
        public string status { get; set; }
    }
}
