# SalesPageAPI

## Descrição do Projeto

SalesPageAPI é um projeto de API desenvolvido em C# utilizando o framework ASP.NET Core. A API tem como objetivo facilitar a integração de sistemas de vendas com a plataforma SalesPage. Ela fornece recursos para autenticação de usuários, gerenciamento de clientes, produtos, pedidos de venda e emissão de notas fiscais.

## Estrutura de Pastas da aplicação SalesPageAPI

A estrutura de pastas do projeto SalesPageAPI é a seguinte:

- `Authorization`: Contém a lógica para autorizar o acesso de algum usuário autenticado.
- `Controllers/`: Contém os controladores da API, responsáveis por definir os endpoints e as ações a serem executadas.
- `Data/`: Contém as classes de acesso a dados, como repositórios e contexto do banco de dados.
- `Interfaces/`: Contém as interfaces que aumentam a coesão das classes da aplicação.
- `Profiles`: Contém o mapeamento da camada de DTO para os usuários.
- `Models/`: Contém as classes de modelos de dados da aplicação.
- `Services/`: Contém os serviços da aplicação, que encapsulam a lógica de negócios.
- `Migrations/`: Contém as migrações do banco de dados.
- `Outras Pastas`: Contém as configurações do .NET para rodar a aplicação conforme o especificado.

## Estrutura de Pastas da aplicação de Testes do projeto SalesPageAPI

A estrutura de pastas do projeto de testes da SalesPageAPI é a seguinte:

Metodologia de teste: AAA e Given-When-Then.

- `SalesPageControllerTests`: Contém os testes da cammada responsável pelo envio e recebimento de dados.
- `UserControllerTests/`: Contém os testes da camada de usuários. 
- `Outras Pastas`: Contém as configurações do .NET para rodar os testes por meio do framework XUnit.


## Tecnologias Utilizadas

O projeto SalesPageAPI utiliza as seguintes tecnologias:

- Linguagem de programação: C#
- Framework: ASP.NET Core
- Banco de dados: MySQL
- Autenticação: JWT (JSON Web Tokens)
- Gerenciamento de identidade: ASP.NET Core Identity

## Pré-requisitos

Antes de executar a SalesPageAPI, certifique-se de ter os seguintes pré-requisitos instalados:

- .NET Core SDK 3.1 ou versão superior
- MySQL Server
- Visual Studio ou Visual Studio Code (opcional)

## Instalação e Execução

Siga as instruções abaixo para instalar e executar o projeto:

1. Clone o repositório do GitHub:

```bash
git clone https://github.com/yagoar45/SalesPageAPI.git
```

2. Acesse o diretório do projeto 

```bash
cd SalesPageAPI
```

3. Compile e execute o projeto.

```code
dotnet Watch run 
```

A API estará disponível em https://localhost:7215/swagger/index.html.

## Documentação da API

A documentação da API está disponível no link acima. Você pode utilizar a interface do swagger para interagir melhor e entender cada endpoint da aplicação

