# Sistema de Agendamento ASP.NET Core MVC com PostgreSQL

Este é um projeto de exemplo simples desenvolvido em ASP.NET Core MVC 8 com Entity Framework Core e PostgreSQL, criado para fins de estudo.

## Funcionalidades Implementadas

### Cliente:
- Ver Lista de Serviços
- Ver Detalhes de um Serviço
- Ver Horários Disponíveis para um Serviço
- Selecionar Horário e Profissional
- Confirmar Agendamento (com usuário fixo ID=1 para demonstração)
- Ver Confirmação do Agendamento

### Administrador/Profissional:
- Gerenciar Serviços (Listar, Criar, Editar, Ver Detalhes, Excluir - com validação de agendamentos existentes)
- Gerenciar Horários Disponíveis (Listar, Criar, Editar, Excluir - com validação de sobreposição e agendamentos existentes)
- Visualizar Agenda Completa (Listar todos os agendamentos confirmados)

**Observação:** A funcionalidade de Login/Autenticação real não foi implementada. Para o fluxo de agendamento do cliente, um usuário com ID=1 é assumido (e criado se não existir). Para as funcionalidades administrativas, não há controle de acesso implementado.

## Estrutura do Projeto

- **/Controllers**: Contém os controladores MVC (ServicosController, HorariosController, AgendamentoController, HomeController).
- **/Data**: Contém o ApplicationDbContext para interação com o banco de dados via Entity Framework Core.
- **/Migrations**: Contém as migrações do Entity Framework Core para criar/atualizar o schema do banco.
- **/Models**: Contém as classes de modelo (Usuario, Servico, Profissional, Horario, Agendamento).
- **/Views**: Contém as views Razor (.cshtml) para cada controlador.
- **/wwwroot**: Contém arquivos estáticos (CSS, JS, etc.).
- **appsettings.json**: Arquivo de configuração (inclui a string de conexão).
- **Program.cs**: Configuração inicial da aplicação e pipeline de requisições.
- **AgendamentoSistema.csproj**: Arquivo do projeto .NET.

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

## Configuração

1.  **Clone ou baixe o projeto.**
2.  **Configure o Banco de Dados PostgreSQL:**
    - Certifique-se de que o PostgreSQL está instalado e rodando.
    - Crie um banco de dados chamado `AgendamentoDb`.
    - Crie um usuário `postgres` com a senha `postgres` (ou ajuste a string de conexão em `appsettings.json`).
    - Alternativamente, ajuste a string de conexão em `appsettings.json` para corresponder à sua configuração do PostgreSQL:
      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Host=SEU_HOST;Port=SUA_PORTA;Database=AgendamentoDb;Username=SEU_USUARIO;Password=SUA_SENHA"
      }
      ```
3.  **Aplique as Migrações:**
    - Abra um terminal na pasta raiz do projeto (`/home/ubuntu/AgendamentoSistema`).
    - Execute o comando para aplicar as migrações e criar as tabelas no banco:
      ```bash
      dotnet ef database update
      ```

## Executando a Aplicação

1.  **Abra um terminal na pasta raiz do projeto.**
2.  **Execute o comando:**
    ```bash
    dotnet run
    ```
3.  **Acesse a aplicação** no seu navegador através do endereço fornecido (geralmente `https://localhost:PORTA` ou `http://localhost:PORTA`).

## Próximos Passos (Sugestões para Estudo)

- Implementar autenticação e autorização (ASP.NET Core Identity).
- Criar CRUD completo para Profissionais e Usuários.
- Melhorar a lógica de busca de horários disponíveis (filtrar por profissional que oferece o serviço).
- Adicionar validações mais robustas.
- Implementar testes unitários e de integração.
- Adicionar um frontend mais elaborado (JavaScript, etc.).
- Implementar cancelamento de agendamentos.
- Adicionar notificações (email, etc.).

