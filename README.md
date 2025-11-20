# FIAP Cloud Games (FCG)
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

## Monitoramento
- Instalação do stack de monitoramento via Helm — ver [values-monitoring.yaml](https://github.com/ajmarzola/Grupo49-TechChallenge/blob/main/infra/monitoring/values-monitoring.yaml)

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

# FCG.TechChallenge.Jogos

> Microsserviço de **Jogos** da plataforma **FIAP Cloud Games (FCG)** — evolução do MVP do repositório **Grupo49-TechChallenge**, agora separado em **microsserviços** e com **busca avançada via Elasticsearch**, **processos assíncronos** e **observabilidade**. Este serviço cuida do **catálogo**, **busca**, **biblioteca do usuário** e **compra** de jogos, integrando-se a **Usuários** (autenticação) e **Pagamentos** (intents/status). 

- **Usuarios** (auth/identidade): https://github.com/ajmarzola/FCG.TechChallenge.Usuarios  
- **Pagamentos** (intents/status): https://github.com/ajmarzola/FCG.TechChallenge.Pagamentos  
- **Jogos** (este repositório): https://github.com/ajmarzola/FCG.TechChallenge.Jogos

🔎 **Projeto anterior (base conceitual):**  
https://github.com/ajmarzola/Grupo49-TechChallenge

🧭 **Miro – Visão de Arquitetura:**  
<https://miro.com/welcomeonboard/VXBnOHN6d0hWOWFHZmxhbzlMenp2cEV3N0FPQm9lUEZwUFVnWC9qWnUxc2ZGVW9FZnZ4SjNHRW5YYVBRTUJEWkFaTjZPNmZMcXFyWUNONEg3eVl4dEdOZWozd0J3RzZld08xM3E1cGl2dTR6QUlJSUVFSkpQcFVSRko1Z0hFSXphWWluRVAxeXRuUUgwWDl3Mk1qRGVRPT0hdjE=?share_link_id=964446466388>

---

## Sumário

- [Visão Geral](#visão-geral)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Como Rodar (Rápido)](#como-rodar-rápido)
- [Configuração por Ambiente](#configuração-por-ambiente)
- [Executando com .NET CLI](#executando-com-net-cli)
- [Executando com Docker](#executando-com-docker)
- [Elasticsearch: Índice e Ping](#elasticsearch-índice-e-ping)
- [Fluxo de Teste End-to-End](#fluxo-de-teste-end-to-end)
- [Coleções/API Docs](#coleçõesapi-docs)
- [Estrutura do Repositório](#estrutura-do-repositório)
- [CI/CD](#cicd)
- [Roadmap](#roadmap)
- [Licença](#licença)

---

## Visão Geral

O **FCG.TechChallenge.Jogos** provê APIs REST para **CRUD de jogos**, **busca** (com **Elasticsearch**), **compra** e **consulta de biblioteca**. Ele publica e consome **eventos** para manter o índice de busca atualizado e coordenar a jornada de compra com o serviço de **Pagamentos** por meio de **filas/tópicos**. 

Os requisitos da fase incluem: separar em três microsserviços (**Usuários, Jogos, Pagamentos**), indexar dados no **Elasticsearch** com consultas/agragações avançadas, usar **funcões serverless** para tarefas assíncronas e melhorar **observabilidade** (logs/traces). 

---

## Arquitetura

- **API Jogos** (ASP.NET Core) — catálogo, compra, biblioteca.
- **Read Model + Índice** — **Elasticsearch** para busca rápida; indexer assíncrono atualiza o índice a partir de eventos.
- **Write Model** — banco relacional (PostgreSQL/SQL Server) para persistência transacional.
- **Mensageria** — barramento/filas para propagar eventos e processar compra/pagamentos de forma **assíncrona**; DLQ para falhas.
- **Serverless** — **Azure Functions** para indexação e orquestrações (ex.: atualização do índice, handlers de eventos).

> O **API Gateway** (com **JWT**) orquestra o tráfego e a autenticação, roteando o front-end para as APIs de Usuários, Jogos e Pagamentos.

---

## Tecnologias

- **.NET 8** (API e processos)
- **EF Core** (PostgreSQL/SQL Server)
- **Elasticsearch** (busca/agragações)
- **Azure Service Bus** (eventos/filas/tópicos)
- **Azure Functions** (indexação/consumidores assíncronos)
- **Docker** (containers para dev e CI)

---

## Como Rodar (Rápido)

Duas opções:

1) **.NET CLI (sem Docker)** – ciclo de dev mais ágil.  
2) **Docker** – isolamento total e paridade com produção.

> Antes de iniciar, configure variáveis e *connection strings* conforme a seção abaixo.

### Pré-requisitos

- .NET SDK 8.x  
- Docker + Docker Compose (para a opção 2)  
- Banco (PostgreSQL **ou** SQL Server) acessível/local  
- **Elasticsearch** acessível (preferencialmente **Elastic Cloud**, ou um nó local para testes)  
- (Opcional) Azure Functions Core Tools (para indexers/handlers locais)

---

## Configuração por Ambiente

Use `appsettings.Development.json` **ou** variáveis de ambiente (recomendado).

| Chave (Environment) | Exemplo / Descrição |
|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Development` |
| `ConnectionStrings__Default` | `Host=localhost;Port=5432;Database=fcg_games;Username=dev;Password=dev` |
| `Elastic__CloudId` | `elastic-<nome>:<hash>` (**Elastic Cloud**) |
| `Elastic__ApiKey` | `base64id:base64secret` **ou** `Elastic__User`/`Elastic__Password` |
| `Elastic__Index__Games` | `fcg-games` |
| `ServiceBus__ConnectionString` | `Endpoint=sb://...;SharedAccessKeyName=...;SharedAccessKey=...` |
| `ServiceBus__Topics__Games` | `games-events` |
| `ServiceBus__Subscriptions__Indexer` | `games-indexer` |
| `Jwt__Authority` | URL do emissor (B2C/IdP) |
| `Jwt__Audience` | `fcg-api` |
| `Observability__EnableTracing` | `true` |

> Ajuste os nomes reais conforme o seu `appsettings`. Caso use **Elastic Cloud**, prefira `CloudId` + `ApiKey`. Se usar um **Elasticsearch local**, use `Elastic__Uri` (`http://localhost:9200`) + `Elastic__User/Password`.

---

## Executando com .NET CLI

> Estrutura típica da solução: **Application**, **Domain**, **Infrastructure**, **Presentation**, **Test**.

1. Restaurar & compilar
   ```bash
   dotnet restore
   dotnet build -c Debug
   ```

2. (Opcional) Aplicar **migrations** (Write/Read Model)
   ```bash
   dotnet ef database update \
     -s FCG.TechChallenge.Jogos.Api \
     -p FCG.TechChallenge.Jogos.Infrastructure
   ```

3. Executar a **API**
   ```bash
   dotnet run -c Debug --project src/FCG.TechChallenge.Jogos.Api
   ```
   - Por padrão, `http://localhost:5085` (ajuste conforme `launchSettings.json`).

4. (Opcional) Executar **Azure Functions** (indexers/handlers)
   ```bash
   func start
   ```

---

## Executando com Docker

> Este repo pode conter `docker-compose.yml` para levantar a API, banco e dependências (ajuste conforme necessidade).

1. Buildar imagens
   ```bash
   docker compose build
   ```

2. Subir serviços
   ```bash
   docker compose up -d
   ```

3. Ver logs
   ```bash
   docker compose logs -f jogos-api
   ```

> **Elasticsearch local** (opcional): você pode subir um nó *single* para desenvolvimento e apontar `Elastic__Uri` para `http://localhost:9200`. Para produção, recomenda-se **Elastic Cloud** e `CloudId+ApiKey`.

---

## Elasticsearch: Índice e Ping

### 1) Verificar conectividade (**ping**)
```bash
curl -u "<usuario>:<senha>" https://<seu-endpoint-elastic>/
# ou, em local:
curl http://localhost:9200/
```

### 2) Criar índice básico (dev/local)
```bash
curl -X PUT http://localhost:9200/fcg-games \
  -H "Content-Type: application/json" \
  -d '{
        "settings": { "number_of_shards": 1, "number_of_replicas": 0 },
        "mappings": {
          "properties": {
            "id":        { "type": "keyword" },
            "nome":      { "type": "text" },
            "descricao": { "type": "text" },
            "preco":     { "type": "double" },
            "tags":      { "type": "keyword" }
          }
        }
      }'
```

> Em **produção**, crie o índice via *bootstrap* do próprio serviço ou *pipeline* IaC. As buscas avançadas/agragações são requisitos desta fase.

---

## Fluxo de Teste End-to-End

> **Cenário típico**: Usuário autenticado lista/busca jogos, adiciona ao carrinho e inicia compra; **Jogos** publica intent de pagamento; **Pagamentos** processa e notifica; **Jogos** confirma aquisição e atualiza biblioteca.

1) **Subir microsserviços** (CLI ou Docker):  
   - **Usuarios** (gera **JWT** para chamadas autenticadas)  
   - **Jogos** (este repo)  
   - **Pagamentos** (consumirá intents publicadas por **Jogos**)

2) **Obter token** (Usuarios)  
   - Login e capture o `access_token` (JWT).

3) **CRUD & Busca de Jogos**
```bash
# Criar jogo
curl -X POST http://localhost:5085/api/jogos \
  -H "Authorization: Bearer <JWT>" \
  -H "Content-Type: application/json" \
  -d '{ "nome":"Celeste", "descricao":"Plataforma", "preco":49.90, "tags":["indie","plataforma"] }'

# Buscar (Elasticsearch)
curl "http://localhost:5085/api/jogos/busca?q=plataforma&tags=indie"
```

4) **Compra / Intent de Pagamento**
```bash
curl -X POST http://localhost:5085/api/jogos/compra \
  -H "Authorization: Bearer <JWT>" \
  -H "Content-Type: application/json" \
  -d '{ "jogoId":"<id>", "usuarioId":"<userId>", "metodo":"credit_card" }'
# → publica evento / intent; Pagamentos processa assíncrono
```

5) **Biblioteca do Usuário**
```bash
curl http://localhost:5085/api/jogos/biblioteca \
  -H "Authorization: Bearer <JWT>"
```

---

## Coleções/API Docs

- **Swagger/OpenAPI**: `http://localhost:<porta>/swagger`
- **Postman**: recomenda-se criar uma Collection com as rotas acima.
- **Autorização**: inclua o **JWT** do serviço de **Usuarios** nas requisições.

---

## Estrutura do Repositório

```
FCG.TechChallenge.Jogos/
├─ src/
│  ├─ FCG.TechChallenge.Jogos.Api/
│  ├─ FCG.TechChallenge.Jogos.Application/
│  ├─ FCG.TechChallenge.Jogos.Domain/
│  └─ FCG.TechChallenge.Jogos.Infrastructure/
├─ tests/
├─ docker-compose.yml
└─ FCG.TechChallenge.Jogos.sln
```

> Alguns nomes/pastas podem variar no seu repo — ajuste os comandos conforme a organização atual.

---

## CI/CD

- **GitHub Actions** para *build*, *test*, *container publish* e *deploy* (App Service / Container Apps / Functions).  
- **Environments** (Dev/Homolog/Prod) com **aprovação manual** para Prod.  
- **OIDC + azure/login** (se publicar no Azure).  
- **Secrets** por ambiente (ex.: `ELASTIC__APIKEY`, `CONNECTIONSTRINGS__DEFAULT`, `SERVICEBUS__CONNECTIONSTRING`).  
- *Infra as Code* opcional para Elasticsearch e Service Bus.

> Os entregáveis da fase pedem **README completo**, desenho/fluxo de arquitetura e **pipelines**; o deploy serverless é recomendado.

---

## Roadmap

- [ ] Projeções e *reindex* guiados por eventos (*event sourcing* + indexer)  
- [ ] Recomendações por histórico/agragações (popularidade, tags, preço)  
- [ ] Cache de consulta (elástico + memória)  
- [ ] Tracing distribuído (W3C) e métricas customizadas  
- [ ] **Rate limiting** e *circuit breakers* em integrações

---

## Licença

Projeto acadêmico, parte do **Tech Challenge FIAP**. Verifique os termos aplicáveis a cada repositório.

## 👥 Integrantes do Grupo
• Anderson Marzola — RM360850 — Discord: aj.marzola

• Rafael Nicoletti — RM361308 — Discord: rafaelnicoletti_

• Valber Martins — RM360859 — Discord: valberdev

