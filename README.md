# Sistema de Biblioteca
![C#](https://img.shields.io/badge/C%23-13-blue.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet.svg)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blueviolet.svg)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-orange.svg)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red.svg)
# 📚 API de Gestão de Biblioteca

Este projeto é uma API RESTful para um sistema de gerenciamento de biblioteca (livros e autores), desenvolvida como forma de aplicar e consolidar conhecimentos no ecossistema .NET, especificamente sobre relacionamentos em banco de dados.

A API permite realizar todas as operações CRUD (Create, Read, Update, Delete) para gerenciar o acervo, garantindo a conexão correta entre as obras e seus escritores. O projeto foi construído usando o template ASP.NET Core Web API e utiliza o Swagger/OpenAPI para documentação e testes dos endpoints.

## 🚀 Tecnologias Utilizadas

* **C#**
* **.NET**
* **ASP.NET Core Web API**
* **Entity Framework Core** (ORM)
* **SQL Server** (Banco de Dados)
* **Swagger** (Documentação da API)

## ✨ Destaques do Projeto
Este projeto foca na organização correta dos dados e na facilidade de uso:
* **Banco de Dados Conectado (Relacional):** O sistema não mistura tudo em uma tabela só. Separamos `Autores` e `Livros` em tabelas diferentes que conversam entre si. Isso permite que um único autor tenha vários livros cadastrados, sem precisar repetir o nome dele várias vezes no banco de dados (evitando duplicidade).
* **Segurança dos Dados:** O sistema garante que todo livro tenha um dono. Não é possível cadastrar um livro "órfão" (sem autor), nem cadastrar um livro para um autor que não existe. O banco de dados trava essas operações para manter as informações sempre corretas.
* **Facilidade para o Usuário (DTOs):** Quem usa a API não precisa saber códigos ou IDs complicados. Basta enviar o **Nome do Autor** e o sistema faz todo o trabalho difícil: ele busca se o autor já existe, se não existir ele cria, e depois vincula o livro automaticamente.

## ✨ Funcionalidades (Endpoints da API)

A API expõe os seguintes métodos para o gerenciamento dos livros e seus respectivos autores:

### `POST` /api/Livros
* **Descrição:** Adiciona um novo livro ao banco de dados. O sistema verifica automaticamente se o Autor informado já existe: se sim, vincula o livro a ele; se não, cria um novo autor.
* **Corpo da Requisição:** Um objeto JSON com os dados do livro (`Titulo`, `AnoPublicacao`, `NomeAutor`).
* **Validação:** Utiliza *Data Annotations* para validar os dados de entrada (ex: campos obrigatórios).

### `GET` /api/Livros
* **Descrição:** Retorna a lista completa de todos os livros cadastrados, incluindo o nome do autor de cada obra (sem expor IDs de relacionamento).

### `GET` /api/Livros/{id}
* **Descrição:** Busca e retorna um livro específico pelo seu Id (chave primária), trazendo os dados do autor vinculado.
* **Parâmetro:** `id` (int) - O ID do livro a ser buscado.

### `GET` /api/Livros/ObterPorTitulo
* **Descrição:** Busca e retorna um livro específico pelo seu Título.
* **Parâmetro (Query):** `titulo` (string) - O título do livro a ser buscado (a busca não diferencia maiúsculas de minúsculas).

### `PUT` /api/Livros/{id}
* **Descrição:** Atualiza os dados de um livro existente. Permite alterar o título, ano ou até mesmo o autor (apenas informando o novo nome).
* **Parâmetro:** `id` (int) - O ID do livro a ser atualizado.
* **Corpo da Requisição:** Um objeto JSON com os dados atualizados (`Titulo`, `AnoPublicacao`, `NomeAutor`).

### `DELETE` /api/Livros/{id}
* **Descrição:** Remove um livro do banco de dados.
* **Parâmetro:** `id` (int) - O ID do livro a ser deletado.

## 🗂 Estrutura do Banco de Dados

O sistema utiliza duas tabelas principais com um relacionamento **Um-para-Muitos (1:N)**:

| Tabela | Descrição | Relacionamento |
| :--- | :--- | :--- |
| **Autores** | Guarda os dados dos escritores. | Um Autor possui muitos Livros. |
| **Livros** | Guarda os dados das obras. | Um Livro pertence a um Autor. |

## 🖼️ Imagens do Swagger e do SQL Server
<img width="1919" height="904" alt="image" src="https://github.com/user-attachments/assets/7c5bfd8c-f6eb-4bda-a5a7-423ae91f7914" />
<img width="1565" height="679" alt="image" src="https://github.com/user-attachments/assets/d2b5b36b-d3e2-4856-bc28-8633d7988ec8" />
<img width="1558" height="668" alt="image" src="https://github.com/user-attachments/assets/57c37fc0-69ac-4e36-9e8d-f41b26f7d412" />




## 🛠️ Como Executar o Projeto

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/JoaoPaulo-Costa01/SistemaBiblioteca
    ```

2.  **Configure o Banco de Dados:**
    No arquivo `appsettings.json`, verifique a `ConnectionStrings` e aponte para seu servidor SQL local.

3.  **Aplique as Migrations:**
    Abra o terminal na pasta do projeto e execute:
    ```powershell
    dotnet ef database update
    ```
    *(Isso criará o banco de dados e as tabelas Livros e Autores automaticamente)*.

4.  **Execute a API:**
    ```powershell
    dotnet run
    ```
