# Desafio técnico de desenvolvedor senior .NET Full Stack para empresa Antlia
"Este código é um desafio técnico realizado para a [Antlia](https://Antlia.com.br/), referente à vaga de Desenvolvedor .NET Sênior.

## Descrição técnica
- C#
- Arquitetura limpa (Clean Architecture)
- Dot Net 9
- Web Api
- DataAccessLayer.SqlServerAdapter
- SQL Server
- AutoMapper
- Angular 19
## Observações sobre projeto
A aplicação tem como objetivo area finaceira.

O framework [DataAccessLayer.SqlServerAdapter](https://www.nuget.org/packages/DataAccessLayer.SqlServerAdapter) foi uma criação pessoal para facilitar a contrução de chamadas de banco de dados, é um framework de codigo aberto esta no [GIT](https://github.com/danielgmarconi/DataAccessLayer)

## Detalhamaneto das camadas

### Antlia.API
- Interface responsável por expor as chamadas dos endpoints.

### Antlia.Application
- Camada é responsável por orquestrar os casos de uso da aplicação, contendo serviços, DTOs, validadores e regras de negócio que dependem de fluxos específicos.

### Antlia.Domain
- Camada é responsável por representar o núcleo da aplicação, contendo as entidades, os contratos (interfaces) e as regras de negócio mais essenciais.

### Antlia.Infra.Data
- Camada é responsável pelo acesso a dados, incluindo a implementação de repositórios e a configuração do Entity Framework.

### Antlia.Infra.IoC
- A camada é usada para registrar e gerenciar as dependências entre os componentes da aplicação, como repositórios, serviços, validadores, handlers, entre outros. 
- Ela centraliza a configuração da injeção de dependência, facilitando a manutenção e promovendo o baixo acoplamento entre as camadas.




[Daniel Marconi](https://www.linkedin.com/in/daniel-marconi-2b058215/)
