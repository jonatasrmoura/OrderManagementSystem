README - Backend API (Desafio TMB)
📌 Visão Geral
API REST simples em C# (.NET 7) + Entity Framework + PostgreSQL para gerenciamento de pedidos.

⚠️ Observação:

Não implementei a integração com Azure Service Bus (faltou tempo/configuração).

Não configurei o Docker Compose (rodei localmente com PostgreSQL instalado).

🚀 Como Executar
Pré-requisitos
.NET 7 SDK

PostgreSQL (rodando localmente)

---

Passos
Configure o banco de dados:

Crie um banco chamado order_db no PostgreSQL.

A propria migration do Entity Framework irá criar a tabela no banco

Altere a connection string no appsettings.json:

json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=5432;Database=order_db;User Id=seu_usuario;Password=sua_senha;"
}
Aplique as migrações do Entity Framework:

bash
dotnet ef database update
Execute a API:

bash
dotnet run
A API estará disponível em:

text
http://localhost:5042

---

📡 Endpoints
Método	Endpoint	Descrição
POST	/orders	Cria um novo pedido
GET	/orders	Lista todos os pedidos
GET	/orders/{id}	Retorna detalhes de um pedido
PUT	/orders/{id}	Atualizar um pedido
DELETE	/orders/{id}	Deletar um pedido

---

📝 Notas Finais
Faltou:

Integração real com Azure Service Bus por falta de recurso (usei um worker local como substituto).

Docker Compose por falta de tempo (rodei tudo manualmente apenas com um container Docker com banco Postgres).

Funcionando:

CRUD básico com PostgreSQL + EF Core.

Fluxo de status (Pendente → Processando → Finalizado).

Se precisar do Azure Service Bus, seria necessário configurar:

Um namespace no Azure.

Uma fila (queue).

Credenciais no appsettings.json.
