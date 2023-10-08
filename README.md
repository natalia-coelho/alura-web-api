# alura-web-api
# Minhas Impressões do Curso

- O curso passou superficialmente por conceitos importantes, por isso é crucial aprofundar mais.

- O código utilizado é bem fraquinho para facilitar a didática, mas já existem conceitos mais atualizados e padrões mais elegantes para resolver os mesmos problemas.

- Fiz algumas alterações por conta própria para otimizar alguns conceitos

Abaixo estão as anotações dos conceitos que achei bacana de registrar para consultar mais tarde.
## API Definition

- **RESTFul API**

    - REST é um padrão arquitetural que visa padronizar os meios de tráfego de dados.

    - Padroniza os dados do servidor para que qualquer cliente consiga utilizar.

    - Abstrai detalhes de implementação.

    - Controla o que deve ser acessado.

    - Serve como uma interface/ponte entre a requisição e...

  

    ![Pasted image 20230905113348.png](Pasted%20image%2020230905113348.png)

## Movies API

  

Os controladores servem para lidar com as requisições recebidas e devolver uma resposta. Para validar dados de entrada do usuário, são usadas as `DataAnnotations`.

  

- Utilizou anotações como `[FromQuery]`.


### Paginação
  
A paginação envolve retornar trechos reduzidos e não a totalidade dos dados, usando `Skip()` e `Take()`.

### Padronização a Nível REST

Padronizar APIs a nível REST torna os resultados e o código mais compreensíveis para outras pessoas que venham a trabalhar com eles. Algumas dicas incluem usar métodos como `NotFound()`, `Ok()`, `CreatedAtAction()` do namespace `Microsoft.AspNetCore.Mvc`.

  

Também é importante retornar códigos de status como:

- 201 (recurso criado)

- 200 (requisição bem-sucedida)

- 404 (recurso não encontrado)

  

Além disso, na aba "Headers" do Postman, você pode encontrar o campo "Location" com a URL referente ao recurso criado quando o código de status é 201.

  

## Entity FrameworkCore

  

### Database Connection

  

Para instalar o EntityFrameworkCore, você pode seguir os passos abaixo:

  

1. [Instale o EntityFrameworkCore](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install).

2. Use o comando CLI:

   ```shell

   Install-Package Microsoft.EntityFrameworkCore.SqlServer

   ```

  

Para utilizar o Entity Framework, você pode usar o comando `dotnet-ef` e o comando desejado.

  

#### DBContext

  

*O `DbContext` serve como uma ponte para fazer operações no banco de dados. Adicionando propriedades ao contexto:**

  

> [!tip]

> Como injetar o `DbContext` em nosso controlador a fim de acessar o banco de dados:

  
  

```csharp

public class MovieContext : DbContext

{

    public MovieContext(DbContextOptions<MovieContext> options) : base(options){}

    public DbSet<Movie> Movies { get; set; }

}

```

  

O `DbContext` é uma das principais maneiras de acessar o banco de dados e abstrair a lógica de acesso. Ele também lida com a Connection String e permite realizar várias operações no banco, incluindo escrita.

  

#### Migrations

  

As migrações fazem o mapeamento da aplicação para o banco de dados e migram os dados da aplicação para o banco. Alguns comandos úteis são:

  

Como gerar migrations e mapear o objeto no banco de dados:

  

- Adicionar uma migração:

  ```shell

  dotnet ef migrations add NomeDaMigração

  ```

- Atualizar o banco de dados:

  ```shell

  dotnet ef database update

  ```

- Remover uma migração:

  ```shell

  dotnet ef migrations remove

  ```

### DTOS (Data Transfer Object)

  

DTOs trazem vantagens relacionadas à organização do código e permitem um maior controle nas requisições e respostas. Com DTOs, você pode definir os parâmetros enviados de maneira isolada do modelo do banco de dados.

  
  

> [!NOTE]

>DTOs nos ajudam a não deixar nosso modelo de banco de dados exposto.

  

Ferramentas utilizadas:

  

- **AutoMapper** por Jimmy Bogard, que mapeia as propriedades facilmente:

  ```shell

  dotnet add package AutoMapper

  ```

  

- **AutoMapper.Extensions.Microsoft.DependencyInjection:**

  ```shell

  dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1

  ```

  

Para adicionar o AutoMapper ao seu programa, você pode fazê-lo no `Program.cs`, por exemplo:

```csharp

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

```

  

Crie um perfil e adicione o mapeamento do `MovieDTO` para `Movie.cs`:

```csharp

namespace MoviesAPI.Profiles

{
    public class MovieProfile : Profile

    {
        public MovieProfile()

        {
            CreateMap<CreateMovieDTO, Movie>();
        }
    }
}

```

  

##### Update

**PUT**: 
é possível atualizar um recurso no banco de dados utilizando o verbo PUT e fazendo a interface com o banco através do Entity. 
para manter o ****padrão REST***, deve-se retornar o código: `204 (No Content)`
Status Code: `return NoContent();`
- Exige que seja passado um objeto inteiro ao atualizar 
```c#
    [HttpPut()]
    public IActionResult UpdateMovie(Guid id, [FromBody]UpdateMovieDTO movieDTO)
    {
        var movie = _movieContext.Movies.FirstOrDefault(
            movie => movie.Id == id);
            if(movie == null) return NotFound();
            _mapper.Map(movieDTO, movie);
            _movieContext.SaveChanges();

        return NoContent();
    }
```

**PATCH:**
Verbo para atualizações parciais
- Permite alterar apenas uma informação sem precisar passar o objeto inteiro;
- Exige uma configuração mais complexa.

	Used Tool: 
	- **Microsoft.AspNetCore.Mvc.NewtonsoftJson**
	  `dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 6.0.10

No `Program.cs` adicionar
```c#
builder.Services.AddControllers().AddNewtonsoftJson();
```

Na hora de passar os valores para atualizar por patch, é necessário seguir o seguinte padrão: 
- `[]`: passar uma lista de objetos ;
- `op`: operação que está sendo realizada. ex: replace;
- `path`: o valor do objeto que se deseja alterar;
- `value`: o valor a ser alterado.
Exemplo: 
```
[
    {
        "op" : "replace",
        "path": "/title",
        "value": "The Lord of the Rings"
    }
]
```

##### Delete
- Utiliza o verbo `Delete`
	`HttpDelete({"id"})`
- método para remover: Remove();
	`_movieContext.Remove(existingMovie)`
e.g: 
```c#
    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(Guid id)
    {
        var existingMovie = _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if (existingMovie == null) return NotFound();
        _movieContext.Remove(existingMovie);
        _movieContext.SaveChanges();
        return NoContent();
    }
```

status: `204 (No Content)`

---
### **Troubleshootings**

- Se ocorrerem erros ao adicionar as migrações, verifique se a classe no `DbContext` está definida como `public` em vez de `protected`.

> [!FAIL] 
>   
A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - A cadeia de certificação foi emitida por uma autoridade que não é de confiança.)

**Solução:** Caso a base seja somente de desenvolvimento, adicionar o seguitnte parâmetro nas conncetion strings `TrustServerCertificate=true;`
Exemplo de Connection Strings (appsettings.json): 
```json
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MovieConnection": "Server=NomeDoServer;Database=NomeDatabase;User Id=NomeUsuario;password=senha;TrustServerCertificate=true;"
  }
```
OBS: quando as connection strings forem atualizadas, talvez seja necessário regerar as migrations:
1. `dotnet ef migrations add NomeMigration`
2. `dotnet ef database update`

