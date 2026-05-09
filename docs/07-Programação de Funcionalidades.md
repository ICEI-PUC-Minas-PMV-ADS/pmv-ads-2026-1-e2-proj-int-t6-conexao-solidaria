# Programação de Funcionalidades

Nesta seção, detalhamos a implementação técnica do sistema, relacionando os requisitos funcionais aos artefatos de código produzidos em ASP.NET Core. A arquitetura utiliza uma abordagem híbrida com Razor Pages para fluxos de dados e MVC (Controllers/Views) para funcionalidades dinâmicas como o Chat e Doações, garantindo uma aplicação robusta e escalável.

## 🛠️ Tecnologias Utilizadas

* **Framework Principal:** .NET 8.0/9.0 (ASP.NET Core).
* **Comunicação em Tempo Real:** SignalR (WebSockets) para o chat instantâneo.
* **Segurança e Autenticação:** ASP.NET Core Identity com suporte a **Roles** (Perfis de Acesso).
* **Persistência de Dados:** Entity Framework Core com SQL Server.
* **Front-end:** Razor Pages, Bootstrap 5 e JavaScript (Client-side SignalR).
* **Serviços Complementares:** Mock Email Service para recuperação de senha e Azure Blob Storage para futura expansão de mídia.

## 📋 Mapeamento de Funcionalidades

| ID | Descrição do Requisito | Artefatos Produzidos (Views/Controllers) | Responsável |
|:---|:---|:---|:---|
| **RF-001** | Criar conta de usuário | `Pages/CriarConta.cshtml`, `Models/Usuario.cs` | Arthur Eduardo A. Lobo |
| **RF-002** | Gestão de Acesso (Login/Logout) | `Pages/Login.cshtml`, `Pages/Logout.cshtml` | Arthur Eduardo A. Lobo |
| **RF-003** | Sistema de Avaliação (Reputação) | `Pages/Avaliacao/Index.cshtml`, `Models/Avaliacao.cs` | Arthur Eduardo A. Lobo |
| **RF-004** | Chat de Apoio em Tempo Real | `ChatController.cs`, `ChatHub.cs`, `Views/Chat/Index.cshtml` | Arthur Eduardo A. Lobo |
| **RF-005** | Visualizar Pedidos de Ajuda | `Pages/Solicitacoes/Index.cshtml`, `Pages/Dashboard.cshtml` | Arthur Eduardo A. Lobo |
| **RF-006** | Realizar Oferta de Doação | `DoacoesController.cs`, `Models/Doacao.cs` | Arthur Eduardo A. Lobo |
| **RF-007** | Gestão de Solicitações | `Pages/Solicitacoes/Criar.cshtml`, `Pages/Solicitacoes/Minhas.cshtml` | Arthur Eduardo A. Lobo |
| **RF-008** | Histórico de Mensagens | `ChatController.cs (Ação Lista)`, `Views/Chat/Lista.cshtml` | Arthur Eduardo A. Lobo |
| **RF-009** | Painel da Defesa Civil (Admin) | `Pages/Admin/Dashboard.cshtml`, `Pages/Admin/Dashboard.cshtml.cs` | Arthur Eduardo A. Lobo |
| **RF-010** | Ciclo de Vida da Doação (Entrega) | `DoacoesController.cs (ConfirmarEntrega)`, `Views/Doacoes/Index.cshtml` | Arthur Eduardo A. Lobo |
| **RF-011** | Recuperação de Senha | `Pages/EsqueciSenha.cshtml`, `Pages/RedefinirSenha.cshtml`, `SmtpEmailService.cs` | Arthur Eduardo A. Lobo |
| **RF-012** | Gerenciar Perfil do Usuário | `Pages/Perfil/Index.cshtml`, `Models/Usuario.cs` | Arthur Eduardo A. Lobo |
| **RF-013** | Relatórios e Focos de Crise | `Pages/Admin/Dashboard.cshtml` | Arthur Eduardo A. Lobo |

---

## 🚀 Instruções de Acesso

A aplicação está preparada para execução local e publicação em ambiente Microsoft Azure.

* **Repositório GitHub:** [Link do Projeto](https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria.git)
* **Link da Aplicação (Azure):** [https://conexaosolidaria.azurewebsites.net](https://conexaosolidaria.azurewebsites.net)

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
