# Cliente em .NET para a [API REST do Cobre Grátis](https://github.com/BielSystems/cobregratis-api)

## Instalação

Baixe o código ou utilize [o pacote nuget](https://nuget.org/packages/CobreGratisDotNet) da seguinte forma:
```
PM> Install-Package CobreGratisDotNet
```

## Uso

Simples assim:

```c#
using System;
using BielSystems;
using BielSystems.Log;

namespace ConsoleSandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            // Você pode passar um receptor de mensagens de log:
            var myLogger = new MyConsoleLogger();

            // Inicialização do cliente:
            var appIdentification = "Client app name (e-mail)";
            var token = "Enter your token here!";
            var CobreGratis = new CobreGratis(appIdentification, token, myLogger);

            // Você pode habilitar ou desabilitar o cache HTTP de requisições GET (habilitado por padrão):
            CobreGratis.EnableCache = true;

            // Você pode criar novos boletos com poucos ou muitos dados:
            CobreGratis.CreateBankBillet(123.45M, DateTime.Now.AddDays(10), "Sacado da Silva");
            CobreGratis.CreateBankBillet(123.45M, DateTime.Now.AddDays(10), "Sacado da Silva", "description", "instructions", "cnpjCpf", "address", "zipcode", "neighborhood", "city", "state", "docNumber", 10.0M, 10.0M, 10.0M, 10.0M, "comments");

            // Você pode atualizar boletos *!* EM RESCUNHO *!*:
            CobreGratis.UpdateBankBillet(1, name: "Nome do Sacado", instructions: "Boleto de testes");

            // Você pode deletar boletos:
            CobreGratis.DeleteBillet(2);

            // Você pode buscar um boleto específico:
            var billet = CobreGratis.GetBankBillet(3);

            // Você pode buscar uma lista com todos os boletos:
            var allBillets = CobreGratis.GetBankBillets();

            // Você pode buscar uma lista paginada de boletos:
            var pageBillets = CobreGratis.GetBankBillets(page: 1);
        }
    }

    public class MyConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
```

## Log

Se injetado via construtor da classe CobreGratis, o implementador da interface **BielSystems.Log.ILogger** receberá mensagens que poderão ajudar no debug das aplicações em runtime.

```c#
public interface ILogger
{
    void Log(string message);
}
```

## Cache

Quando o recurso de cache estiver habilitado (e estará, por padrão), a classe CobreGratis irá utilizar uma instância de **BielSystems.Cache.StaticCache** para persistir as informações de cache de forma estática.

Para habilitar ou desabilitar o cache, utilize a propriedade **CobreGratis.EnableCache**.

Caso deseje utilizar um sistema de persistência diferente do padrão ao utilizar a classe CobreGratis, basta injetar via contrutor a sua própria implementação da interface **BielSystems.Cache.ICache**:

```c#
public interface ICache
{
    void StoreData(string key, object data);
    object LoadData(string key);
    void ClearAll();
    void ClearKey(string key);
    bool ContainsKey(string key);
}
```

## Licença

Esse código é livre para ser usado dentro dos termos da licença [MIT license](http://www.opensource.org/licenses/mit-license.php).

## Bugs, Issues, Agradecimentos, etc

Comentários são bem-vindos. Envie seu feedback através do [issue tracker do GitHub](http://github.com/FredZvt/cobregratis-dotnet/issues)

## Autor

[**Frederico Zveiter**](https://github.com/FredZvt)  
Blog: [http://fredzvt.wordpress.com/](http://fredzvt.wordpress.com/)  
LinkedIn: [http://www.linkedin.com/in/fredericozveiter](http://www.linkedin.com/in/fredericozveiter)