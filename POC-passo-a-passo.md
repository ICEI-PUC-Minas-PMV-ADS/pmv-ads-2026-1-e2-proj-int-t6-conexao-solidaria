# Passo a Passo — Implementação Solo da POC

Roteiro completo para uma única pessoa executar todas as etapas restantes do projeto Conexão Solidária, do zero até o vídeo da POC publicado e os documentos atualizados no repositório.

**Pré-requisitos antes de começar:**
- Conta na Microsoft Azure (o crédito gratuito de estudante PUC Minas funciona)
- Conta no GitHub com acesso ao repositório do projeto
- Visual Studio 2022 Community (gratuito) **ou** VS Code + extensão C# Dev Kit
- .NET SDK 8.0 instalado (https://dotnet.microsoft.com/download)
- SQL Server Management Studio (SSMS) ou Azure Data Studio (opcionais, ajudam a inspecionar o banco)
- OBS Studio para a gravação (gratuito)
- DaVinci Resolve ou Clipchamp para edição (gratuitos)

---

## Setup local e validação

### Passo 1.1 — Preparar o ambiente local

1. Instalar o **.NET SDK 8.0** se ainda não tiver.
2. Instalar o **Visual Studio 2022 Community** com a workload "ASP.NET and web development" marcada.          
3. Intalar o Azure CLI no Windows
   Abra o PowerShell como Administrador (clique com botão direito no PowerShell → "Executar como administrador") e rode:
  ```
  powershellwinget install -e --id Microsoft.AzureCLI
  ```
4. Instalar a ferramenta global do EF Core abrindo um terminal:
   ```bash
   dotnet tool install --global dotnet-ef
   ```
5. Verificar que tudo está OK:
   ```bash
   dotnet --version    # deve mostrar 8.x.x
   dotnet ef --version # deve mostrar Entity Framework Core .NET Command-line Tools
   ```

**Validação:** Os dois comandos devem retornar versões sem erro.

### Passo 1.2 — Clonar o repositório e injetar o esqueleto

1. Clonar o repositório do projeto localmente:
   ```bash
   git clone https://github.com/ICEI-PUC-Minas-PMV-ADS/pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria.git
   cd pmv-ads-2026-1-e2-proj-int-t6-conexao-solidaria
   ```
2. Criar uma branch dedicada à implementação:
   ```bash
   git checkout -b feature/poc-implementation
   ```
3. Baixar o `conexao-solidaria-poc.zip` que foi gerado anteriormente e extraí-lo em uma pasta temporária.
4. Copiar todo o conteúdo da pasta extraída para a raiz do repositório clonado, mantendo a estrutura `src/`, `db/`, `docs/`, `.github/workflows/`. Sobrescrever ou complementar os arquivos existentes do template da disciplina (Markdown da pasta `docs/` deve continuar; só adicionar os novos).
5. Conferir que está tudo no lugar:
   ```bash
   ls src/         # deve mostrar Pages, Models, Data, Services, etc.
   ls .github/workflows/  # deve mostrar azure-deploy.yml
   ```

**Validação:** Estrutura de pastas idêntica à descrita no `README.md` do esqueleto.

### Passo 1.3 — Rodar localmente pela primeira vez

1. Entrar na pasta `src`:
   ```bash
   cd src
   ```
2. Restaurar as dependências NuGet:
   ```bash
   dotnet restore
   ```
3. Criar a primeira migration do Entity Framework (vai gerar a pasta `Migrations/`):
   ```bash
   dotnet ef migrations add InitialCreate
   ```
4. Aplicar a migration no LocalDB:
   ```bash
   dotnet ef database update
   ```
5. Rodar a aplicação:
   ```bash
   dotnet run
   ```
6. Abrir o navegador em `https://localhost:7080` (ou na URL que aparecer no terminal). Aceitar o aviso de certificado autoassinado se aparecer.
7. **Testar os 5 fluxos críticos localmente:**
   - Criar uma conta nova
   - Fazer logout e login com `teste@conexaosolidaria.app` / `Teste@2026`
   - Abrir lista de solicitações
   - Criar uma nova solicitação (sem anexo, por enquanto — o LocalDB não tem Blob Storage)
   - Editar perfil

**Validação:** Os 5 fluxos funcionam local sem erro 500. Se algo falhar, revisar o terminal — geralmente é problema de connection string ou migration.

### Passo 1.4 — Commit da base local

1. Voltar para a raiz do repositório (`cd ..`).
2. Criar `.gitignore` na raiz se ainda não existir (o esqueleto já fornece um).
3. Fazer o primeiro commit:
   ```bash
   git add .
   git commit -m "feat: esqueleto inicial da POC com 8 telas funcionais (RF01, RF02, RF05, RF06, RF08, RF13)"
   git push origin feature/poc-implementation
   ```

**Validação:** O `git status` mostra "nothing to commit, working tree clean" e o push completa sem erros.

---

## Dia 2 — Provisionamento do Azure (4 a 5 horas)

### Passo 2.1 — Criar os recursos pelo Azure CLI (1h30)

1. Instalar o **Azure CLI** se ainda não tiver: https://learn.microsoft.com/cli/azure/install-azure-cli
2. Autenticar no Azure:
   ```bash
   az login
   ```
   Vai abrir uma janela do navegador para login. Use a conta que tem o crédito de estudante.
3. Listar as assinaturas disponíveis e definir a ativa:
   ```bash
   az account list --output table
   az account set --subscription "Nome ou ID da assinatura"
   ```
4. Executar os comandos do `README.md` na ordem indicada. Para evitar erros de digitação, copiar e colar cada bloco em um arquivo `setup-azure.sh` local e executá-lo bloco a bloco. Variáveis a definir no topo:
   ```bash
   RG=rg-conexao-solidaria
   LOCATION=brazilsouth
   APP=conexao-solidaria
   PLAN=plan-conexao-solidaria
   SQL_SERVER=sql-conexaosol-$RANDOM    # sufixo aleatório, nome precisa ser único globalmente
   SQL_DB=ConexaoSolidariaDB
   SQL_ADMIN=csadmin
   SQL_PWD='UmaSenhaForte@2026!'
   ST_ACCOUNT=stconexaosol$RANDOM       # idem, único globalmente
   ```
5. Executar bloco a bloco e conferir cada criação no portal antes de seguir para o próximo. Se algum comando falhar (por exemplo, nome de servidor SQL já existente), ajustar o sufixo aleatório e repetir.

**Validação:** No portal Azure (`portal.azure.com`), abrir o Resource Group `rg-conexao-solidaria` e confirmar a presença de **App Service**, **SQL Server**, **SQL Database**, **Storage Account** e **App Service Plan**.

### Passo 2.2 — Liberar firewall do SQL para seu IP (15 min)

O SQL Server por padrão bloqueia conexões externas. Para você conseguir aplicar a migration a partir da sua máquina:

1. Descobrir seu IP atual:
   ```bash
   curl ifconfig.me
   ```
2. Adicionar regra de firewall no SQL:
   ```bash
   az sql server firewall-rule create -g $RG -s $SQL_SERVER \
     -n MeuIPLocal --start-ip-address SEU_IP --end-ip-address SEU_IP
   ```

**Validação:** Tentar conectar pelo SSMS ou Azure Data Studio com as credenciais de admin. Deve abrir sem erro.

### Passo 2.3 — Aplicar migrations no Azure SQL (45 min)

1. Atualizar temporariamente o `appsettings.Development.json` com a connection string do Azure (apenas para rodar a migration; **não comitar** essa alteração):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=tcp:sql-conexaosol-XXXX.database.windows.net,1433;Database=ConexaoSolidariaDB;User ID=csadmin;Password=UmaSenhaForte@2026!;Encrypt=True;TrustServerCertificate=False;"
   }
   ```
2. A partir da pasta `src`, aplicar a migration no Azure:
   ```bash
   dotnet ef database update
   ```
3. Reverter o `appsettings.Development.json` para a versão LocalDB:
   ```bash
   git checkout src/appsettings.Development.json
   ```

**Validação:** No portal Azure, abrir o SQL Database, ir em "Query editor (preview)", logar com as credenciais de admin e rodar:
```sql
SELECT name FROM sys.tables;
```
Deve listar `AspNetUsers`, `AspNetRoles`, `AspNetUserRoles`, `Solicitacoes`, `__EFMigrationsHistory` e outras tabelas do Identity.

### Passo 2.4 — Configurar o GitHub Actions e fazer o primeiro deploy (1h)

1. No portal Azure, abrir o App Service `conexao-solidaria`.
2. No menu lateral, clicar em **Deployment Center**.
3. Selecionar **GitHub** como fonte e autorizar a conexão.
4. Selecionar a organização (ou seu usuário), o repositório e a branch `main` (você vai mergear sua branch para main mais tarde).
5. O Azure vai criar automaticamente:
   - O secret `AZURE_WEBAPP_PUBLISH_PROFILE` no GitHub
   - Um workflow YAML na pasta `.github/workflows/`
6. **Sobrescrever o workflow gerado pelo Azure pelo que está no esqueleto** (`azure-deploy.yml`) — o nosso é mais simples e usa o `app-name` correto. Fazer push dessa alteração:
   ```bash
   git add .github/workflows/azure-deploy.yml
   git commit -m "ci: ajustar workflow de deploy"
   git push origin feature/poc-implementation
   ```
7. Mergear a branch para main (pelo GitHub web, criando Pull Request e aprovando, ou direto pela linha de comando):
   ```bash
   git checkout main
   git merge feature/poc-implementation
   git push origin main
   ```
8. O push em `main` dispara o workflow. Acompanhar em https://github.com/SEU_USUARIO/SEU_REPO/actions
9. Aguardar 4 a 6 minutos para o build e deploy completarem.

**Validação:** Abrir `https://conexao-solidaria.azurewebsites.net` no navegador. A tela inicial deve carregar (cold start pode levar 10-15s na primeira vez). O DbSeeder roda automaticamente no startup, então o banco no Azure já estará populado com 4 usuários e 5 solicitações.

### Passo 2.5 — Conferir o deploy (30 min)

1. Acessar a URL pública e fazer login com `teste@conexaosolidaria.app` / `Teste@2026`.
2. Confirmar que o Dashboard mostra os stat cards com 5 / 5 / 2.
3. Tentar criar uma nova solicitação **com anexo** desta vez (agora o Blob Storage está configurado).
4. Conferir no portal Azure → Storage Account → Containers → `imagens` que o blob foi criado.
5. Tirar um print da tela funcionando — vai servir como evidência inicial.

**Validação:** O fluxo completo end-to-end funciona em produção. Se o upload falhar, conferir que a connection string do storage está correta nas Application Settings do App Service.

---

## Dia 3 — Polimento e testes próprios (4 a 6 horas)

### Passo 3.1 — Validar responsividade nos três breakpoints (1h)

1. Abrir a aplicação no Chrome.
2. Pressionar F12 para abrir DevTools.
3. Pressionar Ctrl+Shift+M para ativar o modo dispositivo.
4. Testar nas três larguras:
   - **Mobile:** 360×640 (Galaxy S8/S9)
   - **Tablet:** 768×1024 (iPad)
   - **Desktop:** 1440×900 (laptop)
5. Para cada largura, percorrer as 8 telas:
   - Tela 01 — Inicial
   - Tela 02 — Login
   - Tela 03 — Criar Conta
   - Tela 04 — Dashboard
   - Tela 05 — Lista
   - Tela 06 — Detalhes
   - Tela 07 — Nova Solicitação
   - Tela 08 — Perfil
6. Anotar qualquer elemento que esteja saindo da tela ou desalinhado.

**Validação:** Em mobile, a sidebar some e a bottom nav aparece. Em desktop, sidebar de 220px persiste. Nenhum texto cortado ou botão fora da viewport.

### Passo 3.2 — Capturar evidências dos 12 casos de teste (2h)

Esta etapa cumpre dois propósitos: **gerar as evidências dos testes de software** e **executar os testes propriamente ditos**. Para cada caso CT-01 a CT-12 do `08-Avaliacao da Solucao.md`:

1. Abrir o navegador em modo anônimo (para garantir estado limpo).
2. Executar os passos descritos no caso de teste.
3. No momento do resultado, pressionar **Win+Shift+S** (Windows) ou **Cmd+Shift+4** (macOS) e capturar a tela.
4. Salvar com o nome exato indicado no documento, em `docs/img/evidencias/`:
   - `ct-01-cadastro-sucesso.png`
   - `ct-02-email-duplicado.png`
   - `ct-03-senha-fraca.png`
   - `ct-04-login-sucesso.png`
   - `ct-05-senha-incorreta.png`
   - `ct-06-dashboard.png`
   - `ct-07-listagem-ordenada.png`
   - `ct-08-filtro-urgencia.png`
   - `ct-09-detalhes-solicitacao.png`
   - `ct-10-nova-solicitacao.png` e `ct-10-blob-criado.png`
   - `ct-11-perfil-atualizado.png`
   - `ct-12-redirect-login.png`
5. Se algum teste **falhar de verdade** (não funcionar como descrito), anotar o problema e corrigir antes de seguir. O Registro de Testes precisa refletir a realidade.

**Validação:** Pasta `docs/img/evidencias/` com 13 PNGs de qualidade legível, todos referenciados no Registro de Testes.

### Passo 3.3 — Executar os testes de usabilidade reais (1h30)

Esta é a parte que substitui o registro fictício pré-gerado por algo com base real. Você precisa de **2 a 3 pessoas próximas** (familiares, namorada/namorado, colega de classe) que possam dedicar 15 minutos cada uma.

Para cada participante:

1. **Apresentação (1 min):** "Estou desenvolvendo uma plataforma para conectar quem precisa de ajuda a quem pode ajudar, no contexto de desastres naturais. Vou pedir para você fazer 5 tarefas e quero que você fale o que está pensando enquanto faz, sem se preocupar em acertar. Pode parar e perguntar quando quiser."
2. **Limpar o navegador** antes de cada participante (modo anônimo, sessão nova).
3. **Apresentar as 5 tarefas TU-01 a TU-05** uma de cada vez, em linguagem natural, sem direcionar:
   - TU-01: "Crie uma conta no sistema."
   - TU-02: "Você quer ajudar alguém que precisa de medicamentos com urgência. Encontre uma solicitação que se encaixe."
   - TU-03: "Imagine que você precisa de ajuda. Registre uma nova solicitação com uma foto."
   - TU-04: "Atualize seu telefone e troque sua foto de perfil."
   - TU-05: "Saia do sistema e entre novamente."
4. Para cada tarefa, **anotar em uma folha**:
   - Tempo aproximado (cronometre com o celular)
   - O que ele/ela hesitou ou clicou errado
   - Frases ditas em voz alta (transcreva as mais marcantes)
5. Ao final, perguntar: "De 1 a 5, quão fácil foi usar?" e "O que mais te incomodou?".
6. **Atualizar o `08-Avaliacao da Solucao.md`** substituindo os relatos de P1, P2, P3 e P4 pelos relatos reais. Mantenha 4 participantes — se você só conseguiu 2, repita ou peça para uma quarta pessoa em momento separado. Os números podem ser ajustados conforme observado.

**Validação:** Documento atualizado com observações reais. As notas e tempos podem ser próximos do template, mas os comentários devem ser autênticos.

### Passo 3.4 — Aplicar correções rápidas (1h)

Com base nos testes de usabilidade e dos testes de software, aplicar as correções de **prioridade alta** que dão para fazer rapidamente. Sugestões com base no que está pré-documentado:

1. **Texto de regras de senha visível antes da submissão** (Tela 03):
   No `CriarConta.cshtml`, alterar o `<small>` abaixo do campo Senha para algo mais explícito:
   ```html
   <small class="text-muted">
       Mínimo 8 caracteres, com pelo menos uma letra maiúscula,
       uma minúscula, um número e um símbolo (ex.: !@#$%).
   </small>
   ```
2. **Botão "Sair" mais descobrível** (Layout):
   No `_Layout.cshtml`, trocar o botão de logout para incluir texto:
   ```html
   <button type="submit" class="cs-logout" title="Sair">↪ Sair</button>
   ```
   E ajustar o CSS do `.cs-logout` para acomodar o texto.

3. Commitar:
   ```bash
   git add .
   git commit -m "fix: melhorias de usabilidade pós-testes (visibilidade de regras de senha e logout)"
   git push origin main
   ```
   O CI/CD vai fazer redeploy automático.

**Validação:** As mudanças aparecem no ambiente Azure após 4-5 minutos.

---

## Dia 4 — Documentação e gravação (5 a 6 horas)

### Passo 4.1 — Atualizar todos os documentos no repositório (2h)

1. Copiar os 4 documentos gerados para `docs/`, com a numeração que o template da disciplina espera:
   - `docs/03-Plano de Testes de Software.md` ← `Plano de Testes de Software.md`
   - `docs/04-Plano de Testes de Usabilidade.md` ← `Plano de Testes de Usabilidade.md` *(ajuste a numeração se a disciplina usar outra)*
   - `docs/07-Implementação da Solução.md` ← `07-Implementacao da Solucao.md`
   - `docs/08-Avaliação da Solução.md` ← `08-Avaliacao da Solucao.md`
2. Abrir cada um e fazer ajustes finais de coerência:
   - Conferir se números de figura e tabela continuam crescentes desde os documentos anteriores
   - Atualizar o nome dos integrantes e a turma se necessário
   - Conferir que os nomes de arquivo de evidência referenciados existem em `docs/img/evidencias/`
3. Atualizar o `06-Prova de Conceito.md` existente, na seção "Resultados e Conclusão", para referenciar os documentos da Etapa 3.
4. Commitar:
   ```bash
   git add docs/
   git commit -m "docs: artefatos da Etapa 3 (Implementação e Avaliação da Solução)"
   git push origin main
   ```

**Validação:** Os 4 documentos estão acessíveis pelo GitHub web, com formatação preservada (tabelas renderizando, imagens aparecendo).

### Passo 4.2 — Pre-flight da gravação (30 min)

Trinta minutos antes da gravação, executar o checklist do `roteiro-gravacao.md`:

- [ ] Aplicação respondendo na URL pública (abrir em janela anônima e validar)
- [ ] Aquecer o App Service (cold start) abrindo a URL 1 minuto antes
- [ ] Banco de dados com seed populado e usuário de teste funcional
- [ ] Imagem de fallback (~300 KB JPG) na área de trabalho
- [ ] Aba do Azure Portal aberta no Resource Group
- [ ] Aba do GitHub aberta em Actions com último workflow verde
- [ ] Notificações do SO desativadas (Foco Assistido / Não Perturbe)
- [ ] Discord, WhatsApp Desktop, e-mail — fechados
- [ ] Navegador em modo anônimo, zoom em 110%, favoritos escondidos (Ctrl+Shift+B)
- [ ] OBS Studio configurado: 1920×1080 a 30fps, fonte de captura selecionada, áudio testado
- [ ] Microfone testado com gravação curta de 30s (ouvir e validar)
- [ ] Bateria carregando (se for notebook)

**Validação:** Todo o checklist deve estar 100% antes de apertar "Gravar".

### Passo 4.3 — Gravar a POC (1h)

1. Abrir o `roteiro-gravacao.md` numa segunda tela (ou impresso, se preferir).
2. Iniciar a gravação no OBS.
3. Aguardar 3 segundos com a tela inicial visível antes de começar a falar (margem para corte na edição).
4. Executar os 7 blocos do roteiro na sequência, lendo a narração em ritmo natural.
5. Se errar uma frase **no meio de um bloco**, não pare — termine o bloco e corte na edição depois.
6. Se errar **muito** num bloco, parar a gravação, voltar 1 minuto na timeline e regravar a partir daquele ponto. É melhor ter 2 takes parciais que um vídeo todo refeito.
7. Após o bloco 7, esperar 3 segundos antes de parar a gravação.
8. Salvar o arquivo bruto em `gravacoes/raw_take1.mkv` (ou similar).

**Validação:** Arquivo MKV/MP4 com áudio audível e os 7 blocos presentes.

### Passo 4.4 — Edição e exportação (2h)

1. Abrir o **DaVinci Resolve** (ou Clipchamp se preferir interface mais simples).
2. Importar o arquivo gravado.
3. Cortar:
   - Os primeiros 2-3 segundos de "respiração" antes de começar a falar
   - Pausas longas, gaguejos óbvios, erros de leitura corrigidos depois
   - Os últimos 2-3 segundos depois do "Obrigado"
4. Adicionar **título de abertura** (3 segundos, sobre os primeiros segundos do vídeo):
   - Linha 1: "Conexão Solidária"
   - Linha 2: "Prova de Conceito da Arquitetura"
   - Linha 3: "PUC Minas — PMV-ADS — Turma 6 — 2026/1"
   - Linha 4: nomes dos integrantes da equipe
5. Adicionar **legendas inferiores** identificando o RF de cada bloco. Exemplo: aos 0:20, mostrar "RF01 — Criação de Conta" por 5 segundos.
6. Conferir que o áudio está em volume audível ao longo de todo o vídeo. Aplicar normalização se necessário.
7. Exportar:
   - Formato: **MP4 (H.264)**
   - Resolução: **1920×1080**
   - Frame rate: 30fps
   - Bitrate: 8000 kbps
   - Áudio: AAC 192 kbps

**Validação:** Arquivo final entre 100 e 300 MB. Duração entre 3:30 e 4:30. Áudio audível do começo ao fim.

### Passo 4.5 — Upload no YouTube (30 min)

1. Acessar https://studio.youtube.com com a conta institucional (ou pessoal, se a equipe combinou).
2. Clicar em "Criar" → "Enviar vídeo".
3. Selecionar o MP4 exportado.
4. Preencher:
   - **Título:** "Conexão Solidária — Prova de Conceito da Arquitetura"
   - **Descrição:** copiar do `trecho-poc-com-video.md` (modelo de descrição com timestamps)
   - **Miniatura:** o YouTube vai gerar; pode customizar se quiser (não é obrigatório)
5. Em "Visibilidade", selecionar **Não listado**.
6. Confirmar o upload e aguardar o processamento HD (5-10 minutos).
7. Copiar o link no formato `https://youtu.be/ID_DO_VIDEO`.

**Validação:** Abrir o link em janela anônima e confirmar que o vídeo carrega e reproduz com qualidade HD.

### Passo 4.6 — Atualizar links nos documentos (30 min)

1. Abrir o `trecho-poc-com-video.md` e fazer find-and-replace de `ID_DO_VIDEO_AQUI` pelo ID real do seu vídeo.
2. Copiar a seção "Vídeo de Apresentação da POC" e colar no `06-Prova de Conceito.md`, antes da seção "Referências".
3. Copiar a seção do README e colar no `README.md` da raiz, próximo ao topo.
4. Conferir que o thumbnail do YouTube renderiza corretamente nos dois locais (o GitHub busca a imagem em `https://img.youtube.com/vi/ID/maxresdefault.jpg`).
5. Commit final:
   ```bash
   git add .
   git commit -m "docs: vídeo da POC publicado e links atualizados"
   git push origin main
   ```

**Validação:** Abrir o repositório no GitHub e clicar nos links do vídeo a partir do README e do `06-Prova de Conceito.md`. Ambos devem abrir o YouTube no vídeo correto.

---

## Dia 5 — Verificação final e entrega (2 a 3 horas)

### Passo 5.1 — Checklist da rubrica da disciplina (1h)

Conferir item a item se cada exigência da Etapa 3 está atendida:

- [ ] **Template Padrão da Aplicação** documentado em `docs/07-Implementação da Solução.md`
- [ ] **Funcionalidades do Sistema** com subseção para cada uma das 8 telas
- [ ] Cada funcionalidade descreve **RF atendido**, **estrutura de dados**, **acesso** e **verificação**
- [ ] **Aplicação publicada e funcional** em https://conexao-solidaria.azurewebsites.net
- [ ] **Registro de Testes de Software** em `docs/08-Avaliação da Solução.md` com 12 casos
- [ ] Cada caso de teste tem **evidência (screenshot)** anexada
- [ ] **Registro de Testes de Usabilidade** com participantes, métricas e relatos
- [ ] **URLs do código e do vídeo da POC** listadas no README.md
- [ ] **Vídeo da POC** publicado no YouTube como Não Listado
- [ ] Todos os documentos versionados no GitHub na branch `main`

### Passo 5.2 — Submeter a entrega (1h)

1. Abrir a issue ou pull request da Etapa 3 no GitHub (geralmente o template da disciplina já tem uma).
2. Listar:
   - Link do repositório
   - Link da aplicação publicada
   - Link do vídeo da POC
   - Resumo dos 4 artefatos entregues
3. Marcar a issue como concluída ou submeter no Canvas / sistema da disciplina conforme orientação do professor.

### Passo 5.3 — Backup (30 min)

1. Fazer um clone fresco do repositório em outra pasta para confirmar que tudo está versionado:
   ```bash
   cd /tmp
   git clone https://github.com/SEU_USUARIO/SEU_REPO.git verificacao
   cd verificacao
   ls docs/
   ```
2. Conferir que os 4 documentos da Etapa 3 estão lá.
3. Salvar o vídeo MP4 final em pelo menos dois lugares (Google Drive, OneDrive, HD externo) — caso o YouTube remova o vídeo por algum motivo, você ainda terá o original.

---

## Resumo de tempo por dia

| Dia | Atividades | Tempo |
|-----|------------|-------|
| 1 | Setup local, primeiro `dotnet run`, validação | 3-4h |
| 2 | Provisionamento Azure, deploy, primeiro teste em produção | 4-5h |
| 3 | Validação de responsividade, testes de software, testes de usabilidade reais, correções | 4-6h |
| 4 | Atualização de docs, gravação da POC, edição, upload, links | 5-6h |
| 5 | Verificação final, entrega | 2-3h |
| **Total** | | **18-24h líquidas** |

Distribuído em 5 dias com 4-5h de trabalho por dia, ou em 3 dias intensivos com 7-8h de trabalho por dia, conforme a sua disponibilidade.

---

## Pontos de atenção solo

Como você está executando tudo sozinho, alguns cuidados específicos valem ser destacados:

**Sobre revisão.** Sem outro membro da equipe para revisar, é fácil deixar passar erros. Recomendação: **dormir entre os Dias 4 e 5**, e no Dia 5 reler todos os documentos como se fosse a primeira vez vendo. Erros de digitação, números inconsistentes e referências quebradas costumam aparecer nessa segunda leitura.

**Sobre os testes de usabilidade.** Você precisa de pessoas externas. Não tem como burlar isso de forma defensável. Se o cronograma realmente apertar, faça com **2 pessoas** apenas (em vez de 4) e atualize a Tabela 12 de Perfil dos Participantes para refletir isso. É mais honesto que inventar.

**Sobre o Azure.** O crédito gratuito de estudante é de US$ 100. Os recursos provisionados (B1 + Basic SQL + LRS Storage) consomem cerca de US$ 30/mês. Para uma POC de 1 semana, isso fica em torno de US$ 8. Não corre risco de estourar, mas ao final da disciplina **delete o Resource Group** para evitar cobrança contínua:
```bash
az group delete -n rg-conexao-solidaria --yes --no-wait
```

**Sobre a gravação.** A maior fonte de retrabalho é tentar gravar tudo de primeira sem ensaiar. Antes da gravação real, faça **uma simulação completa** sem áudio, percorrendo as 8 telas na ordem do roteiro. Isso te dá memória muscular dos cliques e reduz o número de takes pela metade.
