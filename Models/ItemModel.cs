namespace valmet_cadastro_item.Models
{
    public class ItemModel
    {


        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now;
        public string Usuario { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Grupo { get; set; } = string.Empty;

        public DateTime Data_De_Baixa { get; set; } = DateTime.Now;
    }
}
