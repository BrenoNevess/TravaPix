# Fraud-Detection · TravaPix

Sistema **web** de detecção de fraudes em transações financeiras, escrito em **C# / ASP.NET Core MVC**, com banco **MySQL** em container Docker. Cada transação é analisada (valor e horário) e classificada como **Segura**, **Suspeita** ou **Alto Risco**.

A interface é renderizada na web (HTML + CSS, tema *dark fintech*) e a lógica fica em C#, separada da parte visual, seguindo o modelo **MVC** (Controllers / Models / Views) — no mesmo espírito de organização do projeto [BeatSense](https://github.com/BrenoNevess/BeatSense).

## Estrutura da solução

| Projeto | Descrição |
|---|---|
| **`FraudDetection.Web/`** | 🟢 Aplicação principal (site MVC). Controllers + Views (Razor/HTML) + CSS por página, Services (lógica), `AppDbContext` (EF Core) acessando o MySQL. |
| `FraudDetection.API/` | Web API original (mantida como referência histórica). |
| `Fraud-Detection/` | Cliente WinForms original (substituído pela versão web; mantido como referência). |

### Organização do `FraudDetection.Web`

```
Controllers/   Home, Account (login/cadastro/logout), Dashboard, Transaction, History, Fraud
Models/        Entities (User, Card, Transaction), Enums, ViewModels
Services/      AuthService (BCrypt), TransactionService, FraudDetectionService, DashboardService, validators
Data/          AppDbContext (EF Core + Pomelo MySQL)
Migrations/    InitialCreate
Views/         Shared/_Layout (sidebar+topbar), Shared/_AuthLayout, e uma view por página
wwwroot/css/   site.css (tema) + um CSS por página (login, register, dashboard, ...)
wwwroot/js/    site.js (máscaras de CPF/cartão + menu responsivo)
```

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) (para o MySQL)
- Ferramenta EF Core: `dotnet tool install --global dotnet-ef`

## Como executar

```powershell
# 1. Subir o MySQL (porta 3307)
docker compose up -d

# 2. Aplicar o banco de dados
cd FraudDetection.Web
dotnet ef database update

# 3. Rodar o site
dotnet run
```

Acesse **http://localhost:5079**. O fluxo: **Criar conta** → login automático → **Dashboard** → **Nova Transação** → **Histórico**.

> Conexão configurada em `FraudDetection.Web/appsettings.json`
> (`server=localhost;port=3307;database=fraudetectiondb;...`).

## Regras de detecção de fraude

| Condição | Nível |
|---|---|
| Valor ≥ R$ 5.000 | **Alto Risco** |
| Valor ≥ R$ 1.000 | **Suspeita** |
| Horário ≤ 5h ou ≥ 23h | **Suspeita** |

O nível final é sempre o de maior severidade entre as regras acionadas.

## Acesso de administrador (página de Fraudes)

A página **Fraudes** é restrita a usuários com papel `ADMIN`. Para promover um usuário já cadastrado:

```sql
UPDATE fraudetectiondb.Users SET Role = 'ADMIN' WHERE Cpf = '00000000000';
```

Depois, faça login novamente para o novo papel valer.

## Segurança

- Senhas armazenadas com **hash BCrypt** (nunca em texto puro).
- Autenticação por **cookie** com claims; páginas internas exigem login.
- **Anti-forgery token** em todos os formulários.

## Tecnologias

ASP.NET Core MVC · Entity Framework Core (Pomelo MySQL) · MySQL 8 (Docker) · BCrypt.Net · HTML + CSS.
