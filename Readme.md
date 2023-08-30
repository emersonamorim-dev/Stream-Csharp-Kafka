# Stream CSharp Kafka - MongoDB  

Codificação em Csharp de aplicação ASP.NET Core completa para Endpoints com Swagger para testar e que utiliza o MongoDB como banco de dados e o Kafka para envio e recebimento de mensagens. A aplicação é conteinerizada usando Docker.

## Pré-requisitos

- .NET 5.0 ou superior
- Docker e Docker Compose
- Kafka
- MongoDB

## Configuração

1. **MongoDB**: A aplicação se conecta ao MongoDB usando a string de conexão definida em `appsettings.json` sob a chave `MongoDb:ConnectionString`.

2. **Kafka**: A aplicação se conecta ao Kafka usando as configurações definidas em `appsettings.json` sob a chave `Kafka`.

## Executando a aplicação com Docker

1. Construa a imagem Docker:

docker build -t stream-csharp-kafka .


2. Use o Docker Compose para iniciar os serviços (MongoDB, Kafka e a aplicação):

docker-compose up


### Endpoints

#### 1. Criar um novo registro:

### Endpoints

#### 1. Criar um novo registro:

**Endpoint**: `POST /stream`

**Descrição**: Cria um novo registro no MongoDB e envia uma mensagem para o Kafka.

```bash
curl -X POST http://localhost:5000/stream \
     -H "Content-Type: application/json" \
     -d '{
           "Name": "NomeExemplo",
           "Description": "DescriçãoExemplo"
         }'
2. Recuperar um registro:
Endpoint: GET /stream/{id}

Descrição: Recupera um registro do MongoDB e consome uma mensagem do Kafka.


# Substitua {id} pelo ID do registro que você deseja recuperar
curl -X GET http://localhost:5000/stream/{id}
3. Recuperar todos os registros:
Endpoint: GET /stream

Descrição: Recupera todos os registros do MongoDB e consome mensagens do Kafka para cada registro.


curl -X GET http://localhost:5000/stream
4. Atualizar um registro:
Endpoint: PUT /stream/{id}

Descrição: Atualiza um registro específico no MongoDB e publica uma mensagem no Kafka.


# Substitua {id} pelo ID do registro que você deseja atualizar
curl -X PUT http://localhost:5000/stream/{id} \
     -H "Content-Type: application/json" \
     -d '{
           "Name": "NovoNome",
           "Description": "NovaDescrição"
         }'
5. Excluir um registro:
Endpoint: DELETE /stream/{id}

Descrição: Exclui um registro específico no MongoDB e publica uma mensagem no Kafka.


# Substitua {id} pelo ID do registro que você deseja excluir
curl -X DELETE http://localhost:5000/stream/{id}
```


Autor:
Emerson Amorim





