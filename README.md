# Cliente em .NET para a [API REST do Cobre Gr�tis](https://github.com/BielSystems/cobregratis-api)

## Instala��o

Baixe o c�digo ou utilize [o pacote nuget](https://nuget.org/packages/CobreGratisDotNet) da seguinte forma:
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
            // Voc� pode passar um receptor de mensagens de log:
            var myLogger = new MyConsoleLogger();

            // Inicializa��o do cliente:
            var appIdentification = "Client app name (e-mail)";
            var token = "Enter your token here!";
            var CobreGratis = new CobreGratis(appIdentification, token, myLogger);

            // Voc� pode habilitar ou desabilitar o cache HTTP de requisi��es GET (habilitado por padr�o):
            CobreGratis.EnableCache = true;

            // Voc� pode criar novos boletos com poucos ou muitos dados:
            CobreGratis.CreateBankBillet(123.45M, DateTime.Now.AddDays(10), "Sacado da Silva");
            CobreGratis.CreateBankBillet(123.45M, DateTime.Now.AddDays(10), "Sacado da Silva", "description", "instructions", "cnpjCpf", "address", "zipcode", "neighborhood", "city", "state", "docNumber", 10.0M, 10.0M, 10.0M, 10.0M, "comments");

            // Voc� pode atualizar boletos *!* EM RESCUNHO *!*:
            CobreGratis.UpdateBankBillet(1, name: "Nome do Sacado", instructions: "Boleto de testes");

            // Voc� pode deletar boletos:
            CobreGratis.DeleteBillet(2);

            // Voc� pode buscar um boleto espec�fico:
            var billet = CobreGratis.GetBankBillet(3);

            // Voc� pode buscar uma lista com todos os boletos:
            var allBillets = CobreGratis.GetBankBillets();

            // Voc� pode buscar uma lista paginada de boletos:
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

Se injetado via construtor da classe CobreGratis, o implementador da interface **BielSystems.Log.ILogger** receber� mensagens que poder�o ajudar no debug das aplica��es em runtime.

```c#
public interface ILogger
{
    void Log(string message);
}
```

## Cache

Quando o recurso de cache estiver habilitado (e estar�, por padr�o), a classe CobreGratis ir� utilizar uma inst�ncia de **BielSystems.Cache.StaticCache** para persistir as informa��es de cache de forma est�tica.

Para habilitar ou desabilitar o cache, utilize a propriedade **CobreGratis.EnableCache**.

Caso deseje utilizar um sistema de persist�ncia diferente do padr�o ao utilizar a classe CobreGratis, basta injetar via contrutor a sua pr�pria implementa��o da interface **BielSystems.Cache.ICache**:

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

## Licen�a

Esse c�digo � livre para ser usado dentro dos termos da licen�a [MIT license](http://www.opensource.org/licenses/mit-license.php).

## Bugs, Issues, Agradecimentos, etc

Coment�rios s�o bem-vindos. Envie seu feedback atrav�s do [issue tracker do GitHub](http://github.com/FredZvt/cobregratis-dotnet/issues)

## Autor

[**Frederico Zveiter**](https://github.com/FredZvt)  
Blog: [http://fredzvt.wordpress.com/](http://fredzvt.wordpress.com/)  
LinkedIn: [http://www.linkedin.com/in/fredericozveiter](http://www.linkedin.com/in/fredericozveiter)