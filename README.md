# Neo.DataBase alpha

<img align="right" alt="GIF" src="https://media.giphy.com/media/MC6eSuC3yypCU/giphy.gif" />

Neo DataBase foi criado com a ideia de facilitar o desenvolvimento de utilitários que necessitem de uma base local, muito simples de usar o Neo oferece classes que facilitam o acesso aos dados de forma simples e prática.

Atualmente o Neo gera apenas bases locais no formato arquivo Json, estamos trabalhando para trazer novos formatos.

</br></br></br></br>

## Como Utilizar?

Muito simples e prático, Neo procura oferecer uma experiência simples em sua implementação, para utilizá-lo basta seguir os passos abaixo:

1- Criar uma entidade que Herde de NeoEntity conforme abaixo:

  ![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/exemploStudent.png?raw=true)
  
 
 2 - Na classe de inicialização do program (program.cs ou Setup.cs) adicione a seguinte linha:
 
 para rotinas síncronas:
 
![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/initializeApplication.png?raw=true)  

para rotinas assíncronas:

![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/initializeApplicationAsync.png?raw=true)

 
Esse método será responsável por criar o diretório no qual estarão os arquivos que serão os repositórios das classes que herdam de NeoEntity, esse diretório será criado na pasta raiz da aplicação com o nome NeoDataBase, caso o desenvolvedor queira configurar um caminho específico para o diretório basta utilizar a seguinte linha de código antes de gerar a base. 

![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/directory.png?raw=true)


## Como acessar os dados?

Existem alguns métodos disponibilizados na classe NeoRepository, que podem auxiliar nessa tarefa, abaixo alguns exemplos:

![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/exemploRepository.png?raw=true)

## Interfaces disponíveis

![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/INeoRepository.png?raw=true)

![Folder example](https://github.com/Gabriel1011/Neo.DataBase/blob/master/Screenshots/INeoRepositoryAsync.png?raw=true)


### Como instalar?

Basta seguir as instruções que estão [aqui](https://www.nuget.org/packages/NeoDataBase). 

## O que tem no Neo e o que falta?

- [x] Gera a base de dados automaticamente
- [x] Classe para acesso ao dados
- [ ] Classes de testes interna
- [ ] Índices nos métodos disponíveis
