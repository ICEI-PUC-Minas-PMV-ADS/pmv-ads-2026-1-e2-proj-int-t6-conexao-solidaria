# Arquitetura da Solução

<span style="color:red">Pré-requisitos: <a href="3-Projeto de Interface.md"> Projeto de Interface</a></span>

Definição de como o software é estruturado em termos dos componentes que fazem parte da solução e do ambiente de hospedagem da aplicação.

## Diagrama de Classes
<figure>
    <img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria/blob/main/docs/img/DiagramaDeClasses.jpeg?raw=true" alt="Diagrama de Classes - Conexão Solidária"/>
    <figcaption>Figura - Diagrama de Classes - Conexão Solidária</figcaption>
</figure>

## Modelo ER (Projeto Conceitual)
 <figure>
     <img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria/blob/main/docs/img/ModeloEntidadeRelacionamento.png?raw=true" alt="Modelo Entidade Relacionamento – Conexão Solidária"/>
  <figcaption>Figura – Tela - Modelo Entidade Relacionamento - Conexão Solidária</figcaption>
</figure>

<!-- O Modelo ER representa através de um diagrama como as entidades (coisas, objetos) se relacionam entre si na aplicação interativa.
-->
<!--Sugestão de ferramentas para geração deste artefato: LucidChart e Draw.io.
-->
<!--A referência abaixo irá auxiliá-lo na geração do artefato “Modelo ER”.
-->
<!-- - [Como fazer um diagrama entidade relacionamento | Lucidchart](https://www.lucidchart.com/pages/pt/como-fazer-um-diagrama-entidade-relacionamento)
-->
## Projeto da Base de Dados

## Projeto da Base de Dados

O projeto da base de dados materializa as entidades e os relacionamentos definidos no Modelo ER. O sistema está fundamentado em quatro pilares principais:

1. **Atores (Usuários)**: Representa todos os indivíduos que interagem com a plataforma. Baseado no modelo de herança estruturado no diagrama, este pilar centraliza o acesso ao sistema, mas respeita as especificidades de cada perfil: Beneficiários (que registram sua localização e necessidades atuais), Doadores (que mantêm um histórico de ações solidárias), Voluntários e Administradores.
2. **Demandas (Pedidos de Ajuda)**: Substitui o conceito genérico de "Necessidades" e é o registro formal de uma urgência feito exclusivamente por um Beneficiário. Cada pedido funciona como um chamado no sistema, contendo a categoria da demanda, descrição detalhada, status de atendimento e a localização exata de onde a ajuda é requerida.
3. **Ofertas (Doações)**: Refere-se à ação concreta de auxílio registrada por um Doador. A grande evolução estrutural neste pilar é que uma Doação não fica "solta" esperando um cruzamento; ela nasce para atender diretamente a um Pedido de Ajuda específico, simplificando a logística, garantindo rastreabilidade e fechando o ciclo de solidariedade.
4. **Interação (Chats de Apoio e Mensagens)**: É o ecossistema de comunicação segura e acolhimento da plataforma. Evoluiu para suportar não apenas mensagens diretas (1-para-1) entre quem ajuda e quem é ajudado, mas também a criação de Grupos Solidários (salas coletivas com vários participantes), essenciais para a organização de ações conjuntas e logística de reconstrução.

### Estrutura das Tabelas

**1. Tabela: Usuários**
Centraliza todos os perfis (Administrador, Doador, Beneficiário, Voluntário) usando o conceito de herança (Single Table Inheritance), incorporando os atributos específicos da classe Beneficiário.

| Campo | Tipo | Descrição |
|---|---|---|
| `id` | PK (INT) | Identificador único. |
| `nome` | VARCHAR | Nome completo do usuário. |
| `email` | VARCHAR | E-mail para login. |
| `senha` | VARCHAR | Senha de acesso. |
| `telefone` | VARCHAR | Telefone de contato. |
| `tipoUsuario` | VARCHAR | Ex: 'Beneficiário', 'Doador', 'Voluntário', 'Administrador'. |
| `localizacao` | VARCHAR | Endereço ou coordenadas (Específico de Beneficiário). |
| `necessidadeAtual` | TEXT | Resumo da situação (Específico de Beneficiário). |

**2. Tabela: Pedidos Ajuda**
Substituiu a antiga tabela "Necessidades", espelhando exatamente a classe PedidoAjuda e a relação "solicita" do diagrama.

| Campo | Tipo | Descrição |
|---|---|---|
| `id` | PK (INT) | Identificador único. |
| `id_beneficiario` | FK (INT) | Referência ao Usuário que solicitou. |
| `descricao` | TEXT | Detalhes sobre o pedido. |
| `categoria` | VARCHAR | Categoria do pedido. |
| `status` | VARCHAR | Ex: 'Aberto', 'Atendido', '
## ATENÇÃO!!!

Os três artefatos — **Diagrama de Classes, Modelo ER e Projeto da Base de Dados** — devem ser desenvolvidos de forma sequencial e integrada, garantindo total coerência e compatibilidade entre eles. O diagrama de classes orienta a estrutura e o comportamento do software; o modelo ER traduz essa estrutura para o nível conceitual dos dados; e o projeto da base de dados materializa essas definições no formato físico (tabelas, colunas, chaves e restrições). A construção isolada ou desconexa desses elementos pode gerar inconsistências, dificultar a implementação e comprometer a qualidade do sistema.

## Tecnologias Utilizadas

Descreva aqui qual(is) tecnologias você vai usar para resolver o seu problema, ou seja, implementar a sua solução. Liste todas as tecnologias envolvidas, linguagens a serem utilizadas, serviços web, frameworks, bibliotecas, IDEs de desenvolvimento, e ferramentas.

Apresente também uma figura explicando como as tecnologias estão relacionadas ou como uma interação do usuário com o sistema vai ser conduzida, por onde ela passa até retornar uma resposta ao usuário.

## Hospedagem

Explique como a hospedagem e o lançamento da plataforma foi feita.

> **Links Úteis**:
>
> - [Website com GitHub Pages](https://pages.github.com/)
> - [Programação colaborativa com Repl.it](https://repl.it/)
> - [Getting Started with Heroku](https://devcenter.heroku.com/start)
> - [Publicando Seu Site No Heroku](http://pythonclub.com.br/publicando-seu-hello-world-no-heroku.html)
