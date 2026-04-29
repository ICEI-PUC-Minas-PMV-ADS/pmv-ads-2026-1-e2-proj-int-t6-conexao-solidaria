-- =============================================================================
-- seed.sql — Conexão Solidária (POC)
-- Popula o Azure SQL Database com dados de teste plausíveis para a gravação.
-- Use apenas se preferir popular manualmente via SSMS / Azure Data Studio.
-- O DbSeeder.cs já faz isso automaticamente no startup da aplicação.
-- =============================================================================

-- Atenção: as senhas no Identity são armazenadas como hash (PBKDF2).
-- Este script cadastra apenas as solicitações; os usuários devem ser criados
-- pela própria aplicação (via Criar Conta) ou pelo DbSeeder.cs.

-- Primeiro descubra os Ids dos usuários cadastrados pelo Seeder:
-- SELECT Id, Email, NomeCompleto FROM AspNetUsers;

-- Substitua os valores @MariaId, @JoaoId, @AnaId, @TesteId pelos GUIDs reais
-- antes de executar.

DECLARE @MariaId NVARCHAR(450) = 'SUBSTITUIR_PELO_GUID_DA_MARIA';
DECLARE @JoaoId  NVARCHAR(450) = 'SUBSTITUIR_PELO_GUID_DO_JOAO';
DECLARE @AnaId   NVARCHAR(450) = 'SUBSTITUIR_PELO_GUID_DA_ANA';
DECLARE @TesteId NVARCHAR(450) = 'SUBSTITUIR_PELO_GUID_DO_TESTE';

INSERT INTO Solicitacoes
  (UsuarioId, TipoNecessidade, Titulo, Descricao, Urgencia, Cidade, Estado, Status, CriadoEm)
VALUES
  (@MariaId, 'medicamentos',
   'Medicamentos para hipertensão',
   'Necessito Losartana 50mg para minha mãe idosa. A receita médica está em anexo. Agradeço imensamente.',
   'alta',  'Petrópolis',     'RJ', 'ativa', DATEADD(HOUR, -3, GETUTCDATE())),

  (@JoaoId, 'abrigo',
   'Famílias desabrigadas precisam de cobertores',
   'Cinco famílias estão em abrigo municipal após enchente. Precisamos de cobertores e colchões para os próximos dias.',
   'alta',  'São Sebastião',  'SP', 'ativa', DATEADD(HOUR, -8, GETUTCDATE())),

  (@AnaId, 'alimentos',
   'Cesta básica para família de 4 pessoas',
   'Solicito alimentos não perecíveis: arroz, feijão, óleo, macarrão, leite e enlatados. Família com duas crianças pequenas.',
   'media', 'Canoas',         'RS', 'ativa', DATEADD(DAY, -1, GETUTCDATE())),

  (@TesteId, 'vestuario',
   'Fraldas geriátricas tamanho G',
   'Preciso de fraldas geriátricas tamanho G para meu pai acamado. Qualquer quantidade ajuda muito.',
   'media', 'Belo Horizonte', 'MG', 'ativa', DATEADD(DAY, -2, GETUTCDATE())),

  (@MariaId, 'vestuario',
   'Roupas adultas tamanho M',
   'Família perdeu tudo na enchente. Roupas adultas tamanho M (masculino e feminino) ajudariam muito a recomeçar.',
   'baixa', 'Petrópolis',     'RJ', 'ativa', DATEADD(DAY, -3, GETUTCDATE()));

GO
