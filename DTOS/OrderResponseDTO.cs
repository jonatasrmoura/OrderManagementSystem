using OrderManagementSystem.Models;

namespace OrderManagementSystem.DTOS
{
    public class OrderResponseDTO
    {
        public Guid Id { get; set; }

        public string Client { get; set; }

        public string Product { get; set; }

        public decimal Value { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        // Campo calculado (opcional)
        public string FormattedValue => Value.ToString("C2");

        // Status simplificado (opcional)
        public string StatusDescription => Status switch
        {
            "Pendente" => "Aguardando processamento",
            "Processando" => "Em preparação",
            "Finalizado" => "Concluído",
            _ => Status
        };
    }
}