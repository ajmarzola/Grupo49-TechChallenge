# 🎮 FCG.TechChallenge — Grupo 49

> Projeto desenvolvido como parte do Tech Challenge da FIAP, focado na construção de uma API robusta para gerenciamento de jogos, utilizando a arquitetura DDD, .NET 8, e práticas modernas de desenvolvimento.

---

## 📌 Visão Geral

A **FCG.TechChallenge** é uma aplicação backend que oferece funcionalidades para cadastro, autenticação e gerenciamento de jogos, usuários e promoções. O projeto adota princípios de **Domain-Driven Design (DDD)**, **Event Storming** e **Clean Architecture**, visando escalabilidade, manutenibilidade e testes automatizados.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8** — Plataforma principal de desenvolvimento
- **ASP.NET Core Web API** — Criação de endpoints RESTful
- **Entity Framework Core** — Mapeamento objeto-relacional
- **JWT** — Autenticação baseada em tokens
- **xUnit & Moq** — Testes unitários e mocks
- **Swagger** — Documentação interativa da API
- **Azure DevOps** — Repositório, pipelines e gerenciamento de tarefas

---

## 🧱 Estrutura do Projeto

```
├── src/
│   ├── FCG.API/               # Camada de apresentação (Controllers)
│   ├── FCG.Application/       # Casos de uso e serviços de aplicação
│   ├── FCG.Domain/            # Entidades, agregados e interfaces de repositórios
│   ├── FCG.Infrastructure/    # Implementações de repositórios e contexto do EF Core
│   └── FCG.CrossCutting/      # Configurações, middlewares e utilitários
├── tests/
│   ├── FCG.Tests.Unit/        # Testes unitários
│   └── FCG.Tests.Integration/ # Testes de integração
└── docs/
    ├── event-storming.png     # Diagrama de Event Storming
    ├── ddd-model.png          # Diagrama de Domínio DDD
    └── arquitetura.pdf        # Documento de arquitetura do sistema
```

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou [Docker](https://www.docker.com/) com uma instância do SQL Server
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) ou [Visual Studio Code](https://code.visualstudio.com/)

### Passos para execução

1. Clone o repositório:

   ```bash
   git clone https://dev.azure.com/RM360850/Grupo49-TechChallenge/_git/FCG.TechChallenge
   ```

2. Navegue até a pasta do projeto:

   ```bash
   cd FCG.TechChallenge/src/FCG.API
   ```

3. Restaure as dependências:

   ```bash
   dotnet restore
   ```

4. Aplique as migrações e atualize o banco de dados:

   ```bash
   dotnet ef database update
   ```

5. Execute a aplicação:

   ```bash
   dotnet run
   ```

6. Acesse a documentação Swagger em:

   ```
   https://localhost:5001/swagger
   ```

---

## ✅ Funcionalidades Implementadas

- [x] Cadastro e autenticação de usuários com JWT
- [x] Gerenciamento de jogos (CRUD)
- [x] Aplicação de promoções e descontos
- [x] Controle de acesso baseado em papéis (Admin, Usuário)
- [x] Documentação interativa com Swagger
- [x] Testes unitários e de integração

---

## 👥 Integrantes do Grupo 49

- **Anderson** — RM005100
- **Rafael** — RM334455
- **Valber** — RM131450

---

## 📄 Licença

Este projeto está licenciado sob os termos da [Licença MIT](LICENSE).

---

## 📬 Contato

Para dúvidas ou sugestões, entre em contato com qualquer um dos integrantes do grupo ou abra uma issue no repositório.

---

## 📎 Recursos Adicionais

- [Documentação Swagger](https://localhost:5001/swagger)
- [Diagramas e Documentos Técnicos](docs/)
