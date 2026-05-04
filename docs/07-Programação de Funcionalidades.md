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
|RF-001| A aplicação deve permitir que o usuário avalie uma agência de intercâmbio com base na sua experiência| |  |
|RF-002| A aplicação deve permitir que o usuário inclua comentários ao fazer uma avaliação de uma agência de intercâmbio     |  |  |
|RF-003| A aplicação deve permitir que o usuário consulte todas as agências de intercâmbio cadastradas ordenando-as com base em suas notas |  |  |

ID,Descrição do Requisito,Artefatos produzidos (Views e Controllers),Aluno(a) responsável
RF-001,Criar conta,"CriarConta.cshtml, UsuariosController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-002,Gerenciar conta,"Login.cshtml, UsuariosController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-003,Avaliar usuário,"Avaliacao.cshtml, AvaliacoesController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-004,Acessar chat de apoio,"Chat.cshtml, ChatHub.cs (SignalR)",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-005,Visualizar pedido,"DetalhesSolicitacao.cshtml, PedidosController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-006,Realizar doação,"ComoAjudar.cshtml, DoacoesController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-007,Gerenciar pedidos,"ListaSolicitacoes.cshtml, NovaSolicitacao.cshtml, PedidosController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-008,Gerenciar doações,"ConfirmacaoEntrega.cshtml, DoacoesController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-009,Gerenciar usuários,"DashboardAdmin.cshtml, AdminController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-010,Registrar doações,"AjudaConfirmada.cshtml, DoacoesController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-011,Recuperar a senha do usuário,"RecuperarSenha.cshtml, AuthController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
RF-012,Editar perfil,"MeuPerfil.cshtml, UsuariosController.cs",Arthur Eduardo Andrade Lobo (Back-end) / [Nome da Dupla] (Front-end)
Fontes
https://github.com/ICEI-PUC-Minas-PMV-ADS/icei-puc-minas-pmv-ads-2025-1-e2-proj-int-t4-odontofacil
# Instruções de acesso

Não deixe de informar o link onde a aplicação estiver disponível para acesso (por exemplo: https://adota-pet.herokuapp.com/src/index.html).

Se houver usuário de teste, o login e a senha também deverão ser informados aqui (por exemplo: usuário - admin / senha - admin).

O link e o usuário/senha descritos acima são apenas exemplos de como tais informações deverão ser apresentadas.

> **Links Úteis**:
>
> - [Trabalhando com HTML5 Local Storage e JSON](https://www.devmedia.com.br/trabalhando-com-html5-local-storage-e-json/29045)
> - [JSON Tutorial](https://www.w3resource.com/JSON)
> - [JSON Data Set Sample](https://opensource.adobe.com/Spry/samples/data_region/JSONDataSetSample.html)
> - [JSON - Introduction (W3Schools)](https://www.w3schools.com/js/js_json_intro.asp)
> - [JSON Tutorial (TutorialsPoint)](https://www.tutorialspoint.com/json/index.htm)
