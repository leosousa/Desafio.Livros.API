# Desafio.Livros.API
Criação de uma API em .Net 8. Desafio proposto como parte de uma avaliação técnica


## Tecnologias utilizadas
- .Net Core 8
- Entity Framework Core 8
- Sql Server
- Containeirzação de aplicação com Docker
- Fluent validation

# Padrões utilizados
- DDD
- Repository Pattern
- CQRS Pattern


## Arquitetura
A arquitetura utilizada é a de desenvolvimento em camadas, com foco no domínio da aplicação, utilizando DDD, com as seguintes camadas abaixo:

### API
- Camada de exposição das rotas de API disponíveis.

### Aplicacao
- Camada de aplicação, contendo os casos de uso da aplicação e ligação com os serviços do domínio.

### Dominio
- Camada de domínio e coração do sistema, contendo os casos de uso do sistemaas, entidades e interfaces de comunicação com outras camadas.

### Infraestrutura
- Camada de infra que provê recursos utilizados pelo sistema, tais como a comunicação com o banco de dados.

### Teste Unitario
- Camada de de testes unitários das camadas de aplicação e domínio.


## Primeiros passos

### Requisitos para rodar a aplicação
Para rodar a aplicação precisa dos seguintes requisitos instalados na máquina
- SDK do .Net Core 8
- Docker

#### Rodando a aplicação
Siga os passos abaixo para rodar a aplicação com o Docker

##### Rodando a aplicação via Docker
1. Abra um terminal, acesse a pasta raiz onde está a  solution e rode o comando abaixo:
```
docker-compose up --build
```

> :exclamation:
> Observação: Aguarde até que o banco de dados fique de pé. Ele demora um pouco utilizando a imagem SqlServer.
>

2. Acesse a url localmente
[https://localhost:8081/swagger/index.html](https://localhost:8081/swagger/index.html)

##### Rodando a aplicação localmente
Para rodar localmente, você vai precisar ter instalado o Sql Server Express instalado
1. Crie um banco de dados com o Sql Server Express localmente

2. Ajuste a string de conexão no arquivo appSetings.json e appSettings.Development.json com o banco criado. 
Ajuste as variáveis SERVER, DATABASE, USER e PASSWORD conforme sua configuração. Use o exemplo abaixo para 
ajustar e colar no seu arquivo de configuração
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=SERVER;Database=DATABASE;User ID=USER;Password=PASSWORD;TrustServerCertificate=True"
  },
```

3. Inicie a aplicação com o Visual Studio ou digite o comando abaixo na raiz do projeto
```
dotnet run --project "Livros.API/Livros.API.csproj"
```

4. Acesse a url
http://localhost:7143/swagger/index.html
[http://localhost:7143/swagger/index.html](http://localhost:7143/swagger/index.html)