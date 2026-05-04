# Programação de Funcionalidades

Nesta seção, detalhamos a implementação das funcionalidades do sistema **Conexão Solidária**, relacionando os requisitos funcionais aos artefatos de código produzidos em **ASP.NET MVC**. A implementação está focada em garantir que a lógica de negócio suporte o ciclo completo de ajuda humanitária.

## 🛠️ Tecnologias Utilizadas

* **Back-end:** C# com framework .NET.
* **Front-end:** HTML, CSS, JavaScript e Bootstrap.
* **Base de Dados:** SQL Server / Azure SQL Database.
* **Cloud:** Microsoft Azure (Hospedagem e Base de Dados).

## 📋 Mapeamento de Funcionalidades

| ID | Descrição do Requisito | Artefatos Produzidos (Views/Controllers) | Responsável |
|:---|:---|:---|:---|
| **RF-001** | Criar conta | `Usuarios/CriarConta.cshtml`, `UsuariosController.cs` | Arthur Eduardo A. Lobo |
| **RF-002** | Gerenciar conta (Login/Logout) | `Usuarios/Login.cshtml`, `UsuariosController.cs` | Arthur Eduardo A. Lobo |
| **RF-003** | Avaliar usuário | `Avaliacoes/Index.cshtml`, `AvaliacoesController.cs` | Arthur Eduardo A. Lobo |
| **RF-004** | Acessar chat de apoio | `Chat/Conversa.cshtml`, `ChatController.cs` | Arthur Eduardo A. Lobo |
| **RF-005** | Visualizar pedido de ajuda | `Pedidos/Detalhes.cshtml`, `PedidosController.cs` | Arthur Eduardo A. Lobo |
| **RF-006** | Realizar doação | `Doacoes/Criar.cshtml`, `DoacoesController.cs` | Arthur Eduardo A. Lobo |
| **RF-007** | Gerenciar pedidos (Criar/Editar) | `Pedidos/Index.cshtml`, `PedidosController.cs` | Arthur Eduardo A. Lobo |
| **RF-008** | Gerenciar doações (Confirmação) | `Doacoes/Index.cshtml`, `DoacoesController.cs` | Arthur Eduardo A. Lobo |
| **RF-009** | Gerenciar usuários (Painel Admin) | `Admin/Usuarios.cshtml`, `AdminController.cs` | Arthur Eduardo A. Lobo |
| **RF-010** | Registrar doações concluídas | `Doacoes/Confirmar.cshtml`, `DoacoesController.cs` | Arthur Eduardo A. Lobo |
| **RF-011** | Recuperar a senha do usuário | `Usuarios/RecuperarSenha.cshtml`, `UsuariosController.cs` | Arthur Eduardo A. Lobo |
| **RF-012** | Editar perfil | `Usuarios/Perfil.cshtml`, `UsuariosController.cs` | Arthur Eduardo A. Lobo |

---

## 🚀 Instruções de Acesso

A aplicação está funcional e hospedada no ambiente Microsoft Azure.

* **Link da Aplicação:** [https://conexaosolidaria.azurewebsites.net](https://conexaosolidaria.azurewebsites.net)
* **Repositório GitHub:** [Link do Projeto](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria.git)

### Contas de Teste

| Perfil | Usuário | Senha |
|:---|:---|:---|
| **Administrador** | `admin@conexaosolidaria.com` | `Admin@2026` |
| **Doador/Voluntário** | `doador@teste.com` | `Teste@123` |
| **Beneficiário** | `beneficiario@teste.com` | `Teste@123` |

---

> **Links Úteis**:
>
> - [Trabalhando com HTML5 Local Storage e JSON](https://www.devmedia.com.br/trabalhando-com-html5-local-storage-e-json/29045)
> - [JSON Tutorial](https://www.w3resource.com/JSON)
> - [JSON Data Set Sample](https://opensource.adobe.com/Spry/samples/data_region/JSONDataSetSample.html)
> - [JSON - Introduction (W3Schools)](https://www.w3schools.com/js/js_json_intro.asp)
> - [JSON Tutorial (TutorialsPoint)](https://www.tutorialspoint.com/json/index.htm)
