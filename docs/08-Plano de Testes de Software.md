# Plano de Testes de Software

<span style="color:red">Pré-requisitos: <a href="2-Especificação do Projeto.md"> Especificação do Projeto</a></span>, <a href="3-Projeto de Interface.md"> Projeto de Interface</a>

O objetivo deste plano é definir as estratégias, critérios e casos de teste que serão utilizados para validar o correto funcionamento do sistema, garantindo qualidade, confiabilidade e aderência aos requisitos definidos.

 
| **Caso de Teste** 	| **CT01 –Cadastro de Usuário* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-001 – Criar conta. |
|**Entrada** |Nome válido (ex: João Silva),E-mail válido (ex: joao@email.com), Telefone válido (ex: 31999999999), Senha válida (mínimo 6 caracteres). |
|**Objetivo do Teste** | Cadastro: Verificar se o sistema permite o cadastro de um novo usuário.  |
|**Passos** | 1) Acessar tela inicial  2) Clicar em “Criar Conta”  3) Preencher dados  4) Confirmar cadastro. |
|**Critérios de Êxito** | "Cadastro realizado com sucesso". |

| **Caso de Teste** 	| **CT02 –Gerenciar Conta (Login)* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-002 – Gerenciar conta. |
|**Entrada** |Nome, e-mail, telefone e senha válidos. |
|**Objetivo do Teste** | Validar o acesso do usuário ao sistema.  |
|**Passos** | 1) Inserir e-mail e senha  2) Clicar em login. |
|**Critérios de Êxito** | "Usuário autenticado." |

| **Caso de Teste** 	| **CT03 –Avaliar Usuário* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-003 – Avaliar usuário. |
|**Entrada** |Seleção de usuário, Nota (ex: 1 a 5 estrelas), Comentário opcional. |
|**Objetivo do Teste** | Verificar se é possível avaliar outro usuário após interação.  |
|**Passos** | 1) Acessar perfil de outro usuário  2) Selecionar opção de avaliação 3) Inserir nota/comentário 4)Confirmar. |
|**Critérios de Êxito** | "Usuário autenticado." |

| **Caso de Teste** 	| **CT04 –Acessar Chat de Apoio* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-004 – Acessar chat de apoio. |
|**Entrada** |Seleção de usuário, Nota (ex: 1 a 5 estrelas), Comentário opcional. |
|**Objetivo do Teste** | Validar o acesso ao chat entre usuários. |
|**Passos** | 1) Acessar chat  2) Selecionar conversa 3) Enviar mensagem. |
|**Critérios de Êxito** | "Mensagem enviada com sucesso." |

| **Caso de Teste** 	| **CT05 –Visualizar Pedido* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-005 – Visualizar pedido. |
|**Entrada** |Acesso à página de pedidos. |
|**Objetivo do Teste** | Verificar a listagem de pedidos disponíveis. |
|**Passos** | 1) Acessar lista de pedidos. |
|**Critérios de Êxito** | "Pedidos exibidos corretamente." |

| **Caso de Teste** 	| **CT06 –Realizar Doação* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-006 – Realizar doação. |
|**Entrada** |Seleção de pedido, Tipo de ajuda (ex: alimento, roupa, dinheiro). |
|**Objetivo do Teste** | Validar o processo de doação. |
|**Passos** | 1) Selecionar pedido 2) Clicar em “Doar” 3) Confirmar. |
|**Critérios de Êxito** | "Doação registrada." |

| **Caso de Teste** 	| **CT07 –Gerenciar Pedidos* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-007 – Gerenciar Pedidos. |
|**Entrada** |Descrição do pedido, Tipo de necessidade, Nível de urgência. |
|**Objetivo do Teste** | Verificar criação e gerenciamento de pedidos. |
|**Passos** | 1) Criar pedido 2) Editar pedido 3) Excluir pedido. |
|**Critérios de Êxito** | "Operações realizadas com sucesso." |


| **Caso de Teste** 	| **CT08 –Gerenciar Doações* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-008 – Gerenciar doações. |
|**Entrada** |Histórico de doações do usuário. |
|**Objetivo do Teste** | Verificar controle de doações realizadas. |
|**Passos** | 1) Acessar área de doações 2) Visualizar histórico. |
|**Critérios de Êxito** | "Doações exibidas corretamente." |


| **Caso de Teste** 	| **CT09 –Gerenciar Usuários* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-009 – Gerenciar usuários. |
|**Entrada** |Lista de usuários, Dados para edição (nome, status, etc.). |
|**Objetivo do Teste** | Validar funcionalidades administrativas de usuários. |
|**Passos** | 1) Acessar lista de usuários 2) Editar ou excluir usuário. |
|**Critérios de Êxito** | "Alterações realizadas corretamente." |


| **Caso de Teste** 	| **CT10 –Registrar Doações* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-010 – Registrar doações. |
|**Entrada** |Dados da doação (tipo, descrição, usuário). |
|**Objetivo do Teste** | Verificar o registro de uma doação no sistema. |
|**Passos** | 1) Realizar doação 2) Confirmar registro. |
|**Critérios de Êxito** | "Doação salva no sistema." |


| **Caso de Teste** 	| **CT11 –Recuperar Senha* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-011 – Recuperar senha do usuárioo. |
|**Entrada** |E-mail válido cadastrado. |
|**Objetivo do Teste** | Validar recuperação de acesso. |
|**Passos** | 1) Clicar em “Esqueci minha senha” 2) Informar e-mail 3) Confirmar. |
|**Critérios de Êxito** | "Sistema inicia processo de recuperação." |


| **Caso de Teste** 	| **CT12 –Editar Perfil* 	|
|:---:	|:---:	|
|--------------------|----------------------------------------------------------------------|
|**Requisitos Associados** |RF-012 – Editar perfil. |
|**Entrada** |Novos dados do usuário (nome, telefone, etc.). |
|**Objetivo do Teste** | Verificar atualização de dados do usuário. |
|**Passos** | 1) Acessar perfil 2) Editar dados 3) Salvar. |
|**Critérios de Êxito** | "Dados atualizados." |
