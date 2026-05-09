using ConexaoSolidaria.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConexaoSolidaria.Data;

/// <summary>
/// Popula o banco com dados de teste plausíveis para a gravação da POC.
/// Roda automaticamente no startup. Idempotente: só insere se a base estiver vazia.
/// </summary>
public static class DbSeeder
{
    // Note que adicionamos o RoleManager aqui
    public static async Task SeedAsync(AppDbContext db, UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
    {
        await db.Database.MigrateAsync();

        // -----------------------------------------------------------------
        // Criar Perfis de Acesso (Roles)
        // -----------------------------------------------------------------
        string[] roleNames = { "Admin", "Beneficiario", "Doador" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        if (await userManager.FindByEmailAsync("teste@conexaosolidaria.app") != null) return;

        // -----------------------------------------------------------------
        // Conta "Modo Deus" da Defesa Civil (Administrador)
        // -----------------------------------------------------------------
        var adminUser = new Usuario
        {
            UserName = "admin@defesacivil.gov.br",
            Email = "admin@defesacivil.gov.br",
            EmailConfirmed = true,
            NomeCompleto = "Defesa Civil - Central",
            Telefone = "199",
            Cidade = "São Paulo",
            Estado = "SP",
            TipoPerfil = "admin"
        };
        var adminResult = await userManager.CreateAsync(adminUser, "Admin@2026");
        if (adminResult.Succeeded)
        {
            // Dá a chave do sistema para este usuário
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // -----------------------------------------------------------------
        // Usuário de teste (usado durante a gravação)
        // -----------------------------------------------------------------
        var teste = new Usuario
        {
            UserName = "teste@conexaosolidaria.app",
            Email = "teste@conexaosolidaria.app",
            EmailConfirmed = true,
            NomeCompleto = "Usuário de Teste",
            Telefone = "(31) 99999-0000",
            Cidade = "Belo Horizonte",
            Estado = "MG",
            TipoPerfil = "beneficiario"
        };
        await userManager.CreateAsync(teste, "Teste@2026");
        await userManager.AddToRoleAsync(teste, "Beneficiario");

        // -----------------------------------------------------------------
        // Usuários adicionais para popular a listagem
        // -----------------------------------------------------------------
        var maria = new Usuario
        {
            UserName = "maria.silva@example.com",
            Email = "maria.silva@example.com",
            EmailConfirmed = true,
            NomeCompleto = "Maria Silva",
            Telefone = "(24) 98888-1111",
            Cidade = "Petrópolis",
            Estado = "RJ",
            TipoPerfil = "beneficiario"
        };
        await userManager.CreateAsync(maria, "Maria@2026");
        await userManager.AddToRoleAsync(maria, "Beneficiario");

        var joao = new Usuario
        {
            UserName = "joao.santos@example.com",
            Email = "joao.santos@example.com",
            EmailConfirmed = true,
            NomeCompleto = "João Santos",
            Telefone = "(12) 97777-2222",
            Cidade = "São Sebastião",
            Estado = "SP",
            TipoPerfil = "beneficiario"
        };
        await userManager.CreateAsync(joao, "Joao@2026");
        await userManager.AddToRoleAsync(joao, "Beneficiario");

        var ana = new Usuario
        {
            UserName = "ana.ferreira@example.com",
            Email = "ana.ferreira@example.com",
            EmailConfirmed = true,
            NomeCompleto = "Ana Ferreira",
            Telefone = "(51) 96666-3333",
            Cidade = "Canoas",
            Estado = "RS",
            TipoPerfil = "beneficiario"
        };
        await userManager.CreateAsync(ana, "Ana@2026");
        await userManager.AddToRoleAsync(ana, "Beneficiario");

        // -----------------------------------------------------------------
        // Solicitações com urgências e tipos variados
        // -----------------------------------------------------------------
        var solicitacoes = new[]
        {
            new Solicitacao { UsuarioId = maria.Id, TipoNecessidade = "medicamentos", Titulo = "Medicamentos para hipertensão", Descricao = "Necessito Losartana 50mg para minha mãe idosa. A receita médica está em anexo. Agradeço imensamente.", Urgencia = "alta", Cidade = "Petrópolis", Estado = "RJ", Status = "ativa", CriadoEm = DateTime.UtcNow.AddHours(-3) },
            new Solicitacao { UsuarioId = joao.Id, TipoNecessidade = "abrigo", Titulo = "Famílias desabrigadas precisam de cobertores", Descricao = "Cinco famílias estão em abrigo municipal após enchente. Precisamos de cobertores e colchões para os próximos dias.", Urgencia = "alta", Cidade = "São Sebastião", Estado = "SP", Status = "ativa", CriadoEm = DateTime.UtcNow.AddHours(-8) },
            new Solicitacao { UsuarioId = ana.Id, TipoNecessidade = "alimentos", Titulo = "Cesta básica para família de 4 pessoas", Descricao = "Solicito alimentos não perecíveis: arroz, feijão, óleo, macarrão, leite e enlatados. Família com duas crianças pequenas.", Urgencia = "media", Cidade = "Canoas", Estado = "RS", Status = "ativa", CriadoEm = DateTime.UtcNow.AddDays(-1) },
            new Solicitacao { UsuarioId = teste.Id, TipoNecessidade = "vestuario", Titulo = "Fraldas geriátricas tamanho G", Descricao = "Preciso de fraldas geriátricas tamanho G para meu pai acamado. Qualquer quantidade ajuda muito.", Urgencia = "media", Cidade = "Belo Horizonte", Estado = "MG", Status = "ativa", CriadoEm = DateTime.UtcNow.AddDays(-2) },
            new Solicitacao { UsuarioId = maria.Id, TipoNecessidade = "vestuario", Titulo = "Roupas adultas tamanho M", Descricao = "Família perdeu tudo na enchente. Roupas adultas tamanho M (masculino e feminino) ajudariam muito a recomeçar.", Urgencia = "baixa", Cidade = "Petrópolis", Estado = "RJ", Status = "ativa", CriadoEm = DateTime.UtcNow.AddDays(-3) }
        };

        db.Solicitacoes.AddRange(solicitacoes);
        await db.SaveChangesAsync();
    }
}