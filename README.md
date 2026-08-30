# SistemaDelivery

Aplicação console em C# para gestão de um sistema de delivery — restaurantes, clientes, pratos e pedidos — usando Entity Framework Core (Database First) sobre SQL Server.

## Sobre o projeto

Projeto de prática focado em modelagem relacional, integridade de dados e mapeamento ORM. O banco de dados foi desenhado primeiro (Database First), com regras de negócio garantidas via `CHECK` constraints e chaves estrangeiras, e as classes C# foram geradas a partir do schema com o EF Core.

## Tecnologias

- C# (.NET)
- Entity Framework Core (Database First)
- SQL Server
- Aplicação console

## Modelo de dados

O banco é composto por 5 tabelas principais:

| Tabela | Descrição |
|---|---|
| `Restaurante` | Restaurantes cadastrados, com categoria e estado (ativo/inativo) |
| `Cliente` | Clientes que fazem pedidos |
| `Prato` | Pratos oferecidos por cada restaurante |
| `Pedido` | Pedidos feitos por um cliente a um restaurante |
| `ItemPedido` | Itens (pratos + quantidade) que compõem um pedido |

### Decisões de design

- **Soft delete**: restaurantes e pratos nunca são apagados fisicamente — são marcados como `Ativo = 0` / `Disponivel = 0`, preservando o histórico de pedidos já realizados.
- **Integridade de valores**: `CHECK` constraints garantem que preços e totais nunca sejam negativos, e que a quantidade de um item de pedido seja sempre maior que zero.
- **Categorias controladas**: `Categoria` (Restaurante) e `StatusPedido` (Pedido) usam `CHECK` constraints para restringir os valores possíveis, evitando inconsistências como "Pendente" vs "pendente".

## Como executar

1. Clona o repositório:
   ```bash
   git clone https://github.com/FreeRafa/SistemaDelivery.git
   ```
2. Cria o banco de dados executando o script SQL disponível em `/Database` (ou a pasta correspondente).
3. Configura a tua connection string (via `appsettings.json` local ou `dotnet user-secrets`) — este ficheiro não é versionado por conter dados sensíveis.
4. Restaura os pacotes e executa:
   ```bash
   dotnet restore
   dotnet run
   ```

## Roadmap

- [ ] CRUD completo para todas as entidades
- [ ] Validações do lado da aplicação (Data Annotations / Fluent Validation)
- [ ] Camada de serviços separada da lógica de apresentação
- [ ] Testes unitários
- [ ] Relatórios básicos (ex: pedidos por restaurante, faturamento por período)

## Autor

**Rafael Velloso**
[GitHub](https://github.com/FreeRafa)
