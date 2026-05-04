# Programação de Funcionalidades (INCLUIR A PROGRAMAÇAÕ DE FUNCIONALIDADE EM PROFUNDIDADE)

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="4-Metodologia.md"> Metodologia</a>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>, <a href="5-Arquitetura da Solução.md"> Arquitetura da Solução</a>

Nesta seção, a implementação do sistema descrita por meio dos requisitos funcionais e/ou não funcionais. Nesta seção, é essencial relacionar os requisitos atendidos com os artefatos criados (código fonte) e com o(s) responsável(is) pelo desenvolvimento de cada artefato a cada etapa. Nesta seção também deverão ser apresentadas, se necessário, as instruções para acesso e verificação da **implementação que deve estar funcional no ambiente de hospedagem, OBRIGATORIAMENTE, a partir da Etapa 03**.

**O que DEVE ser utilizado para o desenvolvimento da aplicação:**
- Microsoft Visual Studio (IDE de Codificação)
- HTML e CSS (frontend)
- Javascript (frontend)
- C# (backend)
- MySQL ou SQLServer(Base de Dados)
- Bootstrap (template responsivo para frontend)
- Github (documentação e controle de versão)

**O que NÃO PODE ser utilizado:**
- Template React (e qualquer outro template - exceto o Bootstrap)
- Qualquer outra liguagem de programação diferente de C#

A tabela a seguir é um exemplo de como ela deverá ser preenchida considerando os artefatos desenvolvidos.

|ID    | Descrição do Requisito  | Artefatos produzidos | Aluno(a) responsável |
|------|-----------------------------------------|----|----|
|RF-001| Criar conta | CriarConta.cshtml, UsuariosController.cs | Arthur Eduardo Andrade Lobo  |
|RF-002| Gerenciar conta | Login.cshtml, UsuariosController.cs  | Arthur Eduardo Andrade Lobo |
|RF-003| Avaliar usuário | Avaliacao.cshtml, AvaliacoesController.cs | Arthur Eduardo Andrade Lobo |
|RF-004| Acessar chat de apoio | Chat.cshtml, ChatHub.cs | Arthur Eduardo Andrade Lobo |
|RF-005| Visualizar pedido | DetalhesSolicitacao.cshtml, PedidosController.cs | Arthur Eduardo Andrade Lobo |
|RF-006| Realizar doação | ComoAjudar.cshtml, DoacoesController.cs | Arthur Eduardo Andrade Lobo |
|RF-007| Gerenciar pedidos | ListaSolicitacoes.cshtml, NovaSolicitacao.cshtml, PedidosController.cs | Arthur Eduardo Andrade Lobo |
|RF-008| Gerenciar doações | ConfirmacaoEntrega.cshtml, DoacoesController.cs | Arthur Eduardo Andrade Lobo |
|RF-009| Gerenciar usuários | DashboardAdmin.cshtml, AdminController.cs | Arthur Eduardo Andrade Lobo |
|RF-010| Registrar doações | AjudaConfirmada.cshtml, DoacoesController.cs | Arthur Eduardo Andrade Lobo |
|RF-011| Recuperar a senha do usuário | RecuperarSenha.cshtml, AuthController.cs | Arthur Eduardo Andrade Lobo |
|RF-012| Editar perfil | MeuPerfil.cshtml, UsuariosController.cs | Arthur Eduardo Andrade Lobo |


# Instruções de acesso

A aplicação estará disponível para acesso no ambiente de produção hospedado na nuvem (Microsoft Azure) a partir da Etapa 03.

URL da Aplicação:
https://conexaosolidaria.azurewebsites.net (Nota: Substitua por sua URL real após o deploy no Azure)

Para facilitar a verificação da implementação e testes de diferentes perfis de acesso, utilize as credenciais abaixo:

Usuário Administrador (Acesso total às métricas e gestão):

Login: admin@conexaosolidaria.com

Senha: Admin@2026

Usuário Voluntário/Doador (Visualização de pedidos e chat):

Login: voluntario@conexaosolidaria.com

Senha: Teste@123

Usuário Beneficiário (Registro de necessidades):

Login: beneficiario@conexaosolidaria.com

Senha: Teste@123

> **Links Úteis**:
>
> - [Trabalhando com HTML5 Local Storage e JSON](https://www.devmedia.com.br/trabalhando-com-html5-local-storage-e-json/29045)
> - [JSON Tutorial](https://www.w3resource.com/JSON)
> - [JSON Data Set Sample](https://opensource.adobe.com/Spry/samples/data_region/JSONDataSetSample.html)
> - [JSON - Introduction (W3Schools)](https://www.w3schools.com/js/js_json_intro.asp)
> - [JSON Tutorial (TutorialsPoint)](https://www.tutorialspoint.com/json/index.htm)
