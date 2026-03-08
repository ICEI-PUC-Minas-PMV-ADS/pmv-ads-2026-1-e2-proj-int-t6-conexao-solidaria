# Especificações do Projeto

A **Especificação do Projeto** do aplicativo *Conexão Solidária* tem como objetivo definir o problema e apresentar uma proposta de solução sob a perspectiva dos usuários da plataforma. O projeto busca facilitar a conexão entre pessoas afetadas por desastres naturais, voluntários e doadores, permitindo o registro de necessidades, a oferta de ajuda e a troca de informações de forma simples e acessível. Nessa etapa, procura-se compreender as necessidades dos usuários e estruturar funcionalidades que apoiem a organização da ajuda e o fortalecimento da rede de solidariedade.

Para isso, serão utilizadas técnicas de levantamento e organização de requisitos comuns na engenharia de software, como a elaboração de **personas**, e a partir delas a criação de **histórias de usuários**, assim como a definição de **requisitos funcionais e não funcionais**. Também serão consideradas as **restrições do projeto**, relacionadas às tecnologias utilizadas e às limitações de desenvolvimento, garantindo uma documentação clara para orientar a implementação do sistema, além do **diagrama de casos de uso** para representar visualmente os principais atores do sistema e as interações que eles podem realizar na aplicação.

<!--
<span style="color:red">Pré-requisitos: <a href="1-Documentação de Contexto.md"> Documentação de Contexto</a></span>

Definição do problema e ideia de solução a partir da perspectiva do usuário. É composta pela definição do  diagrama de personas, histórias de usuários, requisitos funcionais e não funcionais além das restrições do projeto.

Apresente uma visão geral do que será abordado nesta parte do documento, enumerando as técnicas e/ou ferramentas utilizadas para realizar a especificações do projeto
-->

## Personas

## Persona 1

| <div align="center"><img src="img/CarlosSilva.png" width="120"></div> | **Carlos Silva** | **Idade:** 42 anos <br> **Profissão:** Motorista de aplicativo <br> **Localização:** Canoas, Rio Grande do Sul, Brasil <br> **Formação:** Ensino Médio Completo <br> **Objetivo:** Encontrar ajuda para reconstruir sua casa após uma enchente |
|---|---|---|
| **Descrição** | **Dores** | **Expectativas** |
| Carlos perdeu grande parte de seus bens após uma enchente que atingiu seu bairro. Ele utiliza o celular como principal meio de acesso à internet e redes sociais, mas possui conhecimento limitado em tecnologia. Carlos precisa de uma forma simples de informar suas necessidades e encontrar pessoas dispostas a ajudar sua família a se reerguer. | Carlos sente dificuldade em encontrar informações centralizadas sobre onde pedir ajuda. Muitas vezes precisa recorrer a diferentes redes sociais ou grupos de mensagens, o que gera confusão e atraso no recebimento de apoio. Além disso, ele teme que seus pedidos não sejam vistos ou priorizados. | Carlos espera encontrar uma plataforma simples e confiável onde possa registrar suas necessidades, receber apoio rapidamente e acompanhar as doações ou ajuda oferecida por voluntários e organizações. |

---

## Persona 2

| <div align="center"><img src="img/MarinaOliveira.png" width="120"></div> | **Mariana Oliveira** | **Idade:** 34 anos <br> **Profissão:** Vendedora <br> **Localização:** Petrópolis, Rio de Janeiro, Brasil <br> **Formação:** Ensino Médio Completo <br> **Objetivo:** Conseguir apoio para sua família após um deslizamento de terra |
|---|---|---|
| **Descrição** | **Dores** | **Expectativas** |
| Mariana vive em uma região que frequentemente sofre com deslizamentos durante períodos de chuva intensa. Após um desastre recente, ela precisou deixar sua casa e está temporariamente abrigada com familiares. Mariana utiliza aplicativos no celular diariamente, principalmente redes sociais e aplicativos de mensagens. | Mariana sente dificuldade em saber quais organizações estão ajudando e quais recursos estão disponíveis para sua comunidade. Muitas informações são desencontradas e nem sempre chegam rapidamente às pessoas afetadas. | Mariana espera utilizar uma plataforma que facilite a comunicação entre pessoas afetadas, voluntários e doadores, permitindo encontrar ajuda mais rapidamente e acompanhar informações importantes sobre assistência disponível. |

---

## Persona 3

| <div align="center"><img src="img/RobertoAlmeida.png" width="120"></div> | **Roberto Almeida** | **Idade:** 51 anos <br> **Profissão:** Coordenador da Defesa Civil Municipal <br> **Localização:** Belo Horizonte, Minas Gerais, Brasil <br> **Formação:** Graduação em Administração Pública <br> **Objetivo:** Melhorar a organização e distribuição de ajuda em situações de emergência |
|---|---|---|
| **Descrição** | **Dores** | **Expectativas** |
| Roberto trabalha na coordenação de ações emergenciais após desastres naturais e precisa lidar com grande volume de informações em pouco tempo. Ele utiliza sistemas digitais no trabalho, planilhas e aplicativos para acompanhar ocorrências e necessidades da população. | A falta de uma plataforma centralizada dificulta o acompanhamento das demandas da população e a organização da ajuda recebida. Muitas solicitações chegam por diferentes canais, como telefone, redes sociais e mensagens, o que torna o processo mais lento e desorganizado. | Roberto espera que uma plataforma digital possa centralizar pedidos de ajuda, facilitar o contato entre cidadãos e voluntários e fornecer dados organizados que auxiliem na tomada de decisões durante situações de emergência. |

---

## Persona 4

| <div align="center"><img src="img/JulianaFerreira.png" width="120"></div> | **Juliana Ferreira** | **Idade:** 29 anos <br> **Profissão:** Professora de Ensino Fundamental <br> **Localização:** Blumenau, Santa Catarina, Brasil <br> **Formação:** Licenciatura em Pedagogia <br> **Objetivo:** Ajudar outras pessoas que estão passando por desastres naturais |
|---|---|---|
| **Descrição** | **Dores** | **Expectativas** |
| Juliana já foi vítima de uma enchente quando era adolescente e recebeu apoio de voluntários durante aquele período. Desde então, sente o desejo de ajudar outras pessoas em situações semelhantes. Ela tem boa familiaridade com tecnologia e utiliza aplicativos com frequência. | Juliana encontra dificuldade em identificar onde sua ajuda é realmente necessária e como se conectar diretamente com pessoas que precisam de apoio imediato. Muitas campanhas são desorganizadas ou não possuem informações claras. | Juliana espera encontrar uma plataforma que facilite a identificação de necessidades reais e permita que voluntários possam oferecer ajuda de forma rápida, organizada e transparente. |

---

## Persona 5

| <div align="center"><img src="img/LucasMartins.png" width="120"></div> | **Lucas Martins** | **Idade:** 38 anos <br> **Profissão:** Empresário <br> **Localização:** São Paulo, Brasil <br> **Formação:** Graduação em Administração de Empresas <br> **Objetivo:** Contribuir com doações para apoiar comunidades afetadas por desastres |
|---|---|---|
| **Descrição** | **Dores** | **Expectativas** |
| Lucas costuma realizar doações para campanhas humanitárias sempre que ocorre algum desastre natural no país. Ele acompanha notícias e utiliza bastante aplicativos e plataformas digitais para fazer transferências e contribuir com projetos sociais. | Lucas muitas vezes não tem certeza se suas doações estão chegando diretamente a quem precisa. A falta de transparência e acompanhamento nas campanhas de ajuda faz com que ele se sinta inseguro em alguns casos. | Lucas espera utilizar uma plataforma confiável que conecte diretamente doadores com pessoas que precisam de ajuda, permitindo acompanhar o impacto de sua contribuição e garantindo maior transparência no processo de doação. |

<!--
Identifique, em torno de, 5 personas. Para cada persona, lembre-se de descrever suas angústicas, frustrações e expectativas de vida relacionadas ao problema. Além disso, defina uma "aparência" para a persona. Para isso, você poderá utilizar sites como [https://this-person-does-not-exist.com/pt#google_vignette](https://this-person-does-not-exist.com/pt) ou https://thispersondoesnotexist.com/ 

Utilize também como referência o exemplo abaixo:

<img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/IntApplicationProject-Template/blob/main/docs/img/AnaClara1.png" alt="Persona1"/>

Enumere e detalhe as personas da sua solução. Para tanto, baseie-se tanto nos documentos disponibilizados na disciplina e/ou nos seguintes links:

> **Links Úteis**:
> 
> - [Rock Content](https://rockcontent.com/blog/personas/)
> - [Hotmart](https://blog.hotmart.com/pt-br/como-criar-persona-negocio/)
> - [O que é persona?](https://resultadosdigitais.com.br/blog/persona-o-que-e/)
> - [Persona x Público-alvo](https://flammo.com.br/blog/persona-e-publico-alvo-qual-a-diferenca/)
> - [Mapa de Empatia](https://resultadosdigitais.com.br/blog/mapa-da-empatia/)
> - [Mapa de Stalkeholders](https://www.racecomunicacao.com.br/blog/como-fazer-o-mapeamento-de-stakeholders/)
>
Lembre-se que você deve ser enumerar e descrever precisamente e personalizada todos os clientes ideais que sua solução almeja.
-->

## Histórias de Usuários

Com base na análise das personas forma identificadas as seguintes histórias de usuários:

## Histórias de Usuários

|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
| Carlos (Pessoa afetada por desastre) | Registrar um pedido de ajuda na plataforma | Informar minhas necessidades após perder bens em um desastre natural |
| Carlos (Pessoa afetada por desastre) | Acompanhar o status das doações ou ajuda recebida | Saber se alguém já está atendendo meu pedido |
| Mariana (Pessoa afetada por desastre) | Encontrar informações sobre apoio disponível na minha região | Conseguir ajuda de forma mais rápida e organizada |
| Mariana (Pessoa afetada por desastre) | Participar de grupos de apoio ou comunicação | Compartilhar experiências e receber orientação de outras pessoas |
| Roberto (Representante da Defesa Civil) | Visualizar os pedidos de ajuda registrados no sistema | Ter uma visão organizada das necessidades da população |
| Roberto (Representante da Defesa Civil) | Acompanhar estatísticas e informações sobre os pedidos | Melhorar a tomada de decisão durante situações de emergência |
| Juliana (Voluntária) | Encontrar pedidos de ajuda próximos da minha localização | Oferecer apoio de forma rápida para quem precisa |
| Juliana (Voluntária) | Entrar em contato com pessoas que solicitaram ajuda | Organizar melhor a entrega de doações ou apoio |
| Lucas (Doador) | Encontrar campanhas ou pedidos de ajuda confiáveis | Garantir que minha doação chegue a quem realmente precisa |
| Lucas (Doador) | Acompanhar o impacto das minhas doações | Ter transparência sobre como minha ajuda está sendo utilizada |
<!--
|EU COMO... `PERSONA`| QUERO/PRECISO ... `FUNCIONALIDADE` |PARA ... `MOTIVO/VALOR`                 |
|--------------------|------------------------------------|----------------------------------------|
|Ana Clara  | Uma forma de identificar se uma agência é realmente confiável           | Me sentir mais segura ao contratar seus serviços               |
|Ana Clara       | Ter um mecanismo eficiente e rápido de comunicação                 | Que eu possa sanar todas as minhas dúvidas rapidamente |

Apresente aqui as histórias de usuário que são relevantes para o projeto de sua solução. As Histórias de Usuário consistem em uma ferramenta poderosa para a compreensão e elicitação dos requisitos funcionais e não funcionais da sua aplicação. Se possível, agrupe as histórias de usuário por contexto, para facilitar consultas recorrentes à essa parte do documento.

> **Links Úteis**:
> - [Histórias de usuários com exemplos e template](https://www.atlassian.com/br/agile/project-management/user-stories)
> - [Como escrever boas histórias de usuário (User Stories)](https://medium.com/vertice/como-escrever-boas-users-stories-hist%C3%B3rias-de-usu%C3%A1rios-b29c75043fac)
> - [User Stories: requisitos que humanos entendem](https://www.luiztools.com.br/post/user-stories-descricao-de-requisitos-que-humanos-entendem/)
> - [Histórias de Usuários: mais exemplos](https://www.reqview.com/doc/user-stories-example.html)
> - [9 Common User Story Mistakes](https://airfocus.com/blog/user-story-mistakes/)
-->

## Requisitos

As tabelas que se seguem apresentam os requisitos funcionais e não funcionais que detalham o escopo do projeto.

### Requisitos Funcionais

|ID    | Descrição do Requisito  | Prioridade |
|------|-----------------------------------------|----|
|RF-001| visualização de solicitações | MEDIA | 
|RF-002| cadastrar itens    | ALTA |
|RF-003| Permitir que o usuário faça login  | ALTA |
|RF-004| Permitir que o usuário crie uma conta | ALTA|
|RF-005| Confirmação de Entrega/Recebimento | ALTA |
|RF-006| Classificação de Urgência | ALTA |
|RF-007| Avaliação de Usuários | BAIXA |
|RF-008| Notificações de Atualização | MEDIA | 
|RF-009| Sistema de Classificação por Urgência ( BAIXA, MEDIA OU ALTA )  | MEDIA |
|RF-010| Envio de Feedback sobre a Plataforma | BAIXA |

### Requisitos não Funcionais

|ID     | Descrição do Requisito  |Prioridade |
|-------|-------------------------|----|
|RNF-001| O sistema deve ser responsivo, funcionando em computadores e celulares. | ALTA | 
|RNF-002| A aplicação deve processar requisições do usuário em no máximo 3s. |  MÉDIA | 
|RNF-003| O sistema deve ter interface simples e objetiva. | ALTA |  
|RNF-004| O sistema deve funcionar nas versões recentes do: (Google Chrome, Mozilla Firefox, Microsoft Edge).| MÉDIA |
|RNF-005| O sistema deve estar disponível inicialmente em Português(BR). | BAIXA | 
|RNF-006| O sistema deve exigir autenticação por e-mail e senha para acesso às funcionalidades de cadastro e registro de pedidos.| MÉDIA |
Com base nas Histórias de Usuário, enumere os requisitos da sua solução. Classifique esses requisitos em dois grupos:

- [Requisitos Funcionais
 (RF)](https://pt.wikipedia.org/wiki/Requisito_funcional):
 correspondem a uma funcionalidade que deve estar presente na
  plataforma (ex: cadastro de usuário).
- [Requisitos Não Funcionais
  (RNF)](https://pt.wikipedia.org/wiki/Requisito_n%C3%A3o_funcional):
  correspondem a uma característica técnica, seja de usabilidade,
  desempenho, confiabilidade, segurança ou outro (ex: suporte a
  dispositivos iOS e Android).
Lembre-se que cada requisito deve corresponder à uma e somente uma
característica alvo da sua solução. Além disso, certifique-se de que
todos os aspectos capturados nas Histórias de Usuário foram cobertos.

## Restrições

O projeto está restrito pelos itens apresentados na tabela a seguir.

|ID| Restrição                                             |
|--|-------------------------------------------------------|
|01| O projeto deverá ser entregue até o final do semestre |
|02| Não pode ser desenvolvido um módulo de backend        |
|03| A aplicação deverá permitir apenas doações e solicitações relacionadas a necessidades básicas em situações de desastres naturais |
|04| O sistema não será responsável pelo transporte ou entrega física das doações entre usuários |
|05| As informações cadastradas pelos usuários devem ser utilizadas apenas para fins de conexão entre doadores, voluntários e pessoas afetadas |
|06| O desenvolvimento do projeto deverá ser realizado utilizando as tecnologias definidas: ASP.NET MVC, Entity Framework, MSSQL e Azure |
|07| O projeto deverá ser desenvolvido e documentado utilizando ferramentas de versionamento de código no GitHub |
|08| O desenvolvimento será realizado por uma equipe com conhecimento técnico inicial, compatível com alunos em formação na área de tecnologia |

<!--
Enumere as restrições à sua solução. Lembre-se de que as restrições geralmente limitam a solução candidata.

> **Links Úteis**:
> - [O que são Requisitos Funcionais e Requisitos Não Funcionais?](https://codificar.com.br/requisitos-funcionais-nao-funcionais/)
> - [O que são requisitos funcionais e requisitos não funcionais?](https://analisederequisitos.com.br/requisitos-funcionais-e-requisitos-nao-funcionais-o-que-sao/)
-->

## Diagrama de Casos de Uso
 <img src="https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria/blob/main/docs/img/casosDeUso.png?raw=true">

<!-- 
O diagrama de casos de uso é o próximo passo após a elicitação de requisitos, que utiliza um modelo gráfico e uma tabela com as descrições sucintas dos casos de uso e dos atores. Ele contempla a fronteira do sistema e o detalhamento dos requisitos funcionais com a indicação dos atores, casos de uso e seus relacionamentos. 

As referências abaixo irão auxiliá-lo na geração do artefato “Diagrama de Casos de Uso”.

> **Links Úteis**:
> - [Criando Casos de Uso](https://www.ibm.com/docs/pt-br/elm/6.0?topic=requirements-creating-use-cases)
> - [Como Criar Diagrama de Caso de Uso: Tutorial Passo a Passo](https://gitmind.com/pt/fazer-diagrama-de-caso-uso.html/)
> - [Lucidchart](https://www.lucidchart.com/)
> - [Astah](https://astah.net/)
> - [Diagrams](https://app.diagrams.net/)
-->
## Imagens do Projeto

Algumas imagens utilizadas no projeto, como as fotos das personas, foram geradas por Inteligência Artificial apenas para fins ilustrativos no contexto acadêmico deste trabalho.
