## Sobre o projeto

Esta é uma **API** de um Gerenciador de Tarefas desenvolvido seguindo os princípios do **Domain-Driven-Design** como parte de um estudo de backend em **C#** utilizando o **.NET 8**. O projeto tem como objetivo oferecer uma aplicação simples e eficiente para o gerenciamento de tarefas, permitindo que os usuários realizem operações **CRUD(Create, Read, Update e Delete)** em suas tarefas. A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation** é utilizado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um **ORM(Object-Relational Mapper)** que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas **SQL**.

### Features

- **Domain Driven Design(DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por parte dos desenvolvedores.
- **Limitação de Tarefas Pendentes**: Para garantir uma gestão eficiente das tarefas, cada usuário pode ter, no máximo, 10 tarefas pendentes ao mesmo tempo. Ao tentar criar uma nova tarefa quando o limite é atingido, a API retorna uma mensagem de erro informando que o número máximo de tarefas pendentes foi alcançado. Isso ajuda a evitar sobrecarga e incentiva a conclusão das tarefas antes de adicionar novas.

## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

### Requisitos

- Visual Studio versão 2022+ ou Visual Studio Code
- Windows 10+ ou Linux/MacOS com .NET SDK instalado
- MySql Server

### Instalação

1. Clone o repositório:

    ```sh
    git clone git@github.com:benicio227/taskmanagement.git
    ```

2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a API