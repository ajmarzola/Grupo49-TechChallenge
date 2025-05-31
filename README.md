# FIAP Cloud Games (FCG) - Grupo 49
> Projeto desenvolvido como parte do Tech Challenge da FIAP, focado na construção de uma API robusta para gerenciamento de jogos, utilizando a arquitetura DDD, .NET 8, e práticas modernas de desenvolvimento.

## Descrição do Projeto
A FIAP Cloud Games (FCG) é uma plataforma digital para venda de jogos educativos voltados à tecnologia.  
Este MVP (Fase 1) implementa a gestão de usuários e a biblioteca de jogos adquiridos, servindo de base para futuras funcionalidades como matchmaking e gerenciamento de servidores.

## Escopo
- **Cadastro de usuários** com validação de e-mail e senha segura.  
- **Autenticação e autorização** via JWT, com perfis **Usuário** e **Administrador**.  
- **Biblioteca de jogos**: Usuários podem consultar e visualizar seus jogos adquiridos.  
- **Administração de jogos**: Administradores podem cadastrar, atualizar e remover jogos.  
- **Promoções**: Administradores podem criar, listar, atualizar e excluir promoções aplicáveis a jogos.  
- **Gerenciamento de usuários**: Administradores podem atualizar ou remover contas de usuários.

## Domain Storytelling
### Cenários Principais
1. **Cadastro e uso da plataforma por um usuário comum**  
   - Usuário acessa a plataforma.  
   - Preenche nome, e-mail e senha.  
   - O sistema valida o e-mail e a senha (mínimo 8 caracteres, letras, números e caractere especial).  
   - Novo usuário é salvo no banco de dados.  
   - Usuário faz login; o sistema valida credenciais e gera token JWT.  
   - Usuário acessa sua biblioteca de jogos adquiridos e visualiza detalhes.

2. **Ações administrativas**  
   - Administrador faz login; o sistema valida credenciais e emite token JWT com `role=admin`.  
   - Acessa painel administrativo e:
     - Cadastra novos jogos.
     - Cria promoções para jogos.
     - Atualiza ou remove usuários.

## Event Storming

- [Event Storming]([https://localhost:5001/swagger](https://miro.com/app/board/uXjVIG6u010=/?share_link_id=863606644000))

| Fluxo                         | Comando                  | Evento                         | Regras                                                        |
|-------------------------------|--------------------------|--------------------------------|---------------------------------------------------------------|
| **Cadastro de Usuário**       | Cadastrar novo usuário   | Usuário cadastrado com sucesso | - Validar formato do e-mail<br>- Verificar senha segura       |
| **Autenticação de Usuário**   | Efetuar login            | Token JWT gerado               | - Verificar credenciais<br>- Gerar token com perfil           |
| **Acesso à Biblioteca**       | Consultar jogos adquiridos| Lista de jogos carregada      | - Validar token JWT<br>- Carregar jogos do usuário           |
| **Cadastro de Jogo (Admin)**  | Cadastrar novo jogo      | Jogo cadastrado com sucesso    | - Validar dados do jogo<br>- Salvar no banco                  |
| **Criação de Promoção (Admin)**| Criar nova promoção      | Promoção cadastrada com sucesso| - Aplicar desconto sobre jogo<br>- Validar datas de vigência |
| **Administração de Usuários** | Atualizar ou remover usuário | Dados do usuário atualizados / usuário removido | - Verificar existência do usuário<br>- Aplicar atualização    |

## Endpoints de API

### Autenticação
- **POST** `/api/Auth/registro`  
  Registro de usuário.  
  **Body**:  
  ```json
  {
    "nome": "Seu Nome",
    "email": "seu.email@exemplo.com",
    "senha": "SenhaSegura1!",
    "role": "Aluno" // ou "Administrador"
  }
  ```

- **POST** `/api/Auth/login`  
  Geração de token JWT.  
  **Body**:
  ```json
  {
    "email": "seu.email@exemplo.com",
    "senha": "SenhaSegura1!"
  }
  ```

### Jogos
- **GET** `/api/Jogos`  
  Lista todos os jogos.

- **GET** `/api/Jogos/{id}`  
  Retorna detalhes de um jogo por ID.

- **POST** `/api/Jogos`  
  Cadastra um novo jogo.  
  **Body**:
  ```json
  {
    "nome": "Nome do Jogo",
    "descricao": "Descrição detalhada",
    "preco": 99.90,
    "categoria": "Ação"
  }
  ```

- **PUT** `/api/Jogos/{id}`  
  Atualiza um jogo existente.  
  **Body**:
  ```json
  {
    "nome": "Nome Atualizado",
    "descricao": "...",
    "preco": 119.90,
    "categoria": "RPG"
  }
  ```

- **DELETE** `/api/Jogos/{id}`  
  Remove um jogo por ID.

### Promoções
- **GET** `/api/Promocoes`  
  Lista todas as promoções.

- **GET** `/api/Promocoes/{id}`  
  Detalha uma promoção por ID.

- **POST** `/api/Promocoes`  
  Cria uma nova promoção.  
  **Body**:
  ```json
  {
    "nome": "Promo Incrível",
    "descontoPercentual": 20,
    "dataInicio": "2025-06-01T00:00:00Z",
    "dataFim": "2025-06-15T23:59:59Z",
    "jogoId": "GUID-do-jogo"
  }
  ```

- **PUT** `/api/Promocoes/{id}`  
  Atualiza uma promoção existente.  
  **Body**: similar ao POST.

- **DELETE** `/api/Promocoes/{id}`  
  Remove uma promoção por ID.

## Documentação
- **Event Storming** e **Domain Storytelling** disponíveis no Miro (ou arquivos anexados).
- Coleção Postman: `FCG.EndPoints.postman_collection.json`.

## 🚀 Tecnologias Utilizadas
- **.NET 8** — Plataforma principal de desenvolvimento
- **ASP.NET Core Web API** — Criação de endpoints RESTful
- **Entity Framework Core** — Mapeamento objeto-relacional
- **JWT** — Autenticação baseada em tokens
- **xUnit & Moq** — Testes unitários e mocks
- **Swagger** — Documentação interativa da API
- **Azure DevOps** — Repositório, pipelines e gerenciamento de tarefas

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
└── docs/                      # Diagramas de Event Storming, Domain Story Telling, Testes do Postman
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

## 📬 Contato
Para dúvidas ou sugestões, entre em contato com qualquer um dos integrantes do grupo ou abra uma issue no repositório.

## 📎 Recursos Adicionais

- [Diagramas e Documentos Técnicos](docs/)

---
> _Este README faz parte da entrega da Fase 1 do Tech Challenge FIAP Cloud Games. Serão cobrados os vídeos, documentação DDD, testes unitários e instruções de uso conforme especificado._  

## 👥 Integrantes do Grupo 49
- **Anderson** — RM005100
- **Rafael** — RM334455
- **Valber** — RM131450
