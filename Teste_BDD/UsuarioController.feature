# language: pt-BR
Funcionalidade: UsuarioController
  Como um usuário do sistema
  Eu quero poder listar e buscar usuários
  Para que eu possa visualizar os dados dos usuários cadastrados

  Cenario: Listar todos os usuários
    Dado que existem usuários cadastrados no sistema
    Quando eu faço uma requisição para listar os usuários
    Então a resposta deve ser 200 OK e conter a lista de usuários

  Cenario: Buscar usuário por ID
    Dado que existe um usuário com o ID "1234abcd-0000-1111-2222-abcdef123456"
    Quando eu faço uma requisição para buscar o usuário com o ID "1234abcd-0000-1111-2222-abcdef123456"
    Então a resposta deve ser 200 OK e conter o usuário com o ID "1234abcd-0000-1111-2222-abcdef123456"
