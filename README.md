# Renove Backup Service

Sistema de backup automatizado para bancos de dados MySQL, desenvolvido em .NET 6, com suporte a agendamento, compactação de arquivos, armazenamento local e envio para FTP ou VPS.

## Visão Geral

O Renove Backup Service foi desenvolvido para automatizar a criação e distribuição de backups de bancos de dados MySQL.

O serviço executa periodicamente:

1. Extração dos bancos de dados através do `mysqldump.exe`;
2. Geração dos arquivos `.sql`;
3. Compactação dos backups em arquivo `.zip`;
4. Armazenamento local dos arquivos;
5. Envio opcional para servidor FTP;
6. Envio opcional para servidor VPS via API HTTP.

O agendamento é configurado através de expressões Cron armazenadas em banco SQLite e executado pelo Quartz.NET.

---

## Arquitetura da Solução

```text
┌─────────────────────────┐
│ Configurador WinForms   │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ SQLite (Configurações)  │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ Worker Service (.NET 6) │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ Quartz.NET Scheduler    │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ mysqldump.exe           │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ Arquivos SQL            │
└────────────┬────────────┘
             │
             ▼
┌─────────────────────────┐
│ Compactação ZIP         │
└───────┬─────────┬───────┘
        │         │
        ▼         ▼
  Backup Local   Upload
                 FTP/VPS
```

---

## 🚀 Tecnologias Utilizadas

* .NET 6
* Worker Service
* Quartz.NET
* Serilog
* SQLite
* MySQL
* FluentFTP
* Windows Service
* WinForms

---

## Estrutura do Projeto

```text
RenoveBackupService.Core.sln
│
├── RenoveBackupService.Core
│   ├── Worker Service
│   ├── Quartz Scheduler
│   ├── BackupService
│   └── Program.cs
│
├── RenoveBackupService.Configurator
│   ├── Dashboard
│   ├── Configuração FTP
│   ├── Configuração VPS
│   ├── Configuração Backup
│   └── Configuração Serviço
│
├── ServicosGlobais
│   ├── Models
│   ├── Services
│   ├── SQLite
│   └── Utilitários
│
└── Db
    └── RenoveBackupConfigDb.db
```

---

## ⚙️ Funcionalidades

### Backup Automático

* Backup de múltiplos bancos MySQL.
* Execução através de agendamento Cron.
* Geração de arquivos SQL individuais.

### Compactação

* Compactação automática em formato ZIP.
* Substituição de arquivos anteriores quando necessário.

### Armazenamento Local

* Diretório principal de backup.
* Diretório alternativo para redundância.

### Upload FTP

* Conexão FTP explícita com TLS.
* Upload automático dos arquivos ZIP.
* Configuração através do aplicativo desktop.

### Upload VPS

* Envio via HTTP Multipart/Form-Data.
* Integração com APIs REST.

### Logs

* Registro detalhado das operações.
* Logs diários utilizando Serilog.

---

## Dependências Principais

```xml
<PackageReference Include="Quartz" />
<PackageReference Include="Quartz.Extensions.Hosting" />
<PackageReference Include="Serilog" />
<PackageReference Include="Serilog.Sinks.File" />
<PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" />
```

---

## 🔧 Configuração Inicial

### 1. Configurar o MySQLDump

Localize o executável:

```text
mysqldump.exe
```

Exemplo:

```text
C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe
```

Configure este caminho através do aplicativo Configurator.

---

### 2. Configurar Banco de Dados

Informe:

* Host
* Porta
* Usuário
* Senha

O sistema identificará automaticamente os bancos disponíveis para backup.

---

### 3. Configurar Diretórios

Defina:

* Diretório principal de backup
* Diretório alternativo de backup (opcional)

---

### 4. Configurar Agendamento

Exemplo:

```cron
0 0 2 ? * *
```

Executa diariamente às 02:00.

---

## Executando Localmente

### Restaurar Pacotes

```bash
dotnet restore
```

### Compilar

```bash
dotnet build
```

### Executar

```bash
dotnet run --project RenoveBackupService.Core
```

---

## 🪟 Instalação como Serviço Windows

Publicar:

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

Criar serviço:

```cmd
sc create RenoveBackupService binPath= "C:\RenoveBackupService\RenoveBackupService.Core.exe"
```

Iniciar:

```cmd
sc start RenoveBackupService
```

---

## Logs

Os logs são armazenados na pasta:

```text
logs\
```

Exemplos registrados:

* Inicialização do serviço
* Execução de backups
* Falhas de conexão
* Uploads FTP
* Uploads VPS
* Erros críticos

---

## Recursos de Segurança

* Credenciais armazenadas em configuração local.
* Upload FTP com suporte a TLS.
* Upload para VPS via HTTPS (quando configurado).
* Execução sem interface gráfica através de Windows Service.

---

## Roadmap

* [ ] Criptografia dos arquivos ZIP
* [ ] Notificações por e-mail
* [ ] Dashboard Web
* [ ] Histórico de execuções
* [ ] Integração com Azure Blob Storage
* [ ] Integração com Amazon S3

---

## Autor

**Joanes Gonzaga**

Desenvolvedor .NET focado em soluções corporativas, automação de processos e integração de sistemas.

---

## 📄 Licença

Este projeto está disponível para fins de estudo e uso interno, conforme definido pelo autor.
