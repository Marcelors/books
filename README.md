## Pre-requisitos

- Visual Studio 2019
- Node
- SqlServer
- .NET Core 3.1

---
## Detalhes

 - Existe dois projetos dentro da pasta src
 - books-api: é o projeto backend onde foi construido toda logica da aplicação
 - books-web: é o projeto com a interface web

---
## Configuração e Execução do Projeto Books-API

 - Abra a solução no visual studio
 - Set o projeto Books.Api como projeto que será executado 
 - Abra o arquivo "appsettings.json" e verifique a string de conexão com SQL Server, caso endereço esteja diferente alterer para deixa ela de acordo com seu SqlServer
 - Abra o Nuget Package Manager Console, e coloque default project Books.Infra.Data e rode o camando "Update-Database" ele irá criar as tabelas do sistema e usuário default
 - Usuário default será utilizado para se authenticar a primeira vez no portal e web e caso queira criar outros usuário, dados do usuário padrão. Email: admin@mail.com e senha: admin123
 - Apos isso execute o projeto Api é verifique o endereço ao qual ele abrir, por padrão executando como serviço irá abrir no endereço: localhost:5000, porem sendo executado pelo ISS express do visual studio pode ser outro, caso seja outra copie esse endereço pois será necessario atulizar a configuração do front-end.
---

## Configuração e Execução do Projeto Books-Web
 - Abra o projeto com Visual Studio Code
 - Abra o terminal dentro do visual studio code, e rode comando "npm install" para instalar os pacotes necessários no projeto
 - Com projeto aberto, verifique a pasta services, nela tem arquivo chamado "base.service.js" nesse arquivo está a url padrão utilizada, por padrão o endereço padrão e "localhost:5000" contudo quando estiver executando Books-Api e for diferente o endereço que ele abriu atualize esse endereço para que frontend consiga se comunicar com backend
  - para executar o projeto rode comando "npm run serve" no terminal do visual studio code


