## CampusCore

Учебный проект на **C# / .NET** и **React** для отработки построения full-stack приложения, с учётом определенного стиля кода и архитектурных особенностей заказчика.

### Архитектура / Architecture

- **Backend (ASP.NET Core)**: серверная часть в `src/CampusCore.BackOffice` реализует HTTP API, обработку сценариев и доступ к данным.
- **Frontend (React)**: клиент в `src/CampusCore.BackOffice/client-app` реализует интерфейс BackOffice и взаимодействует с API по сети.
- **Database (PostgreSQL)**
- **Migrations**: SQL-миграции в `src/CampusCore.Migrator/Migrations/000000/` (на текущем этапе применяются вручную).



### Необходимые технологии

- .NET SDK 9.x
- IIS Express 10
- PostgreSQL
- Node.js + npm (для фронтенда)



### База данных

Приложение использует единый ключ `ConnectionStrings:Default`. В Docker он передаётся
переменной `ConnectionStrings__Default` из `.env`, а при локальном запуске хранится
в .NET User Secrets

Миграции (создание таблиц) лежат в `src/CampusCore.Migrator/Migrations/000000/`.
Сейчас `CampusCore.Migrator` не реализован, поэтому SQL необходимо применить вручную.

### Запуск



#### Docker (рекомендуется)

Скопируйте `.env.example` в `.env` и при необходимости измените значения.

Для запуска BackOffice и PostgreSQL:

```bash
docker compose up --build
```

Приложение будет доступно по адресу `http://localhost:8081`.
При первом запуске PostgreSQL автоматически применит SQL-миграции.

Остановить контейнеры:

```bash
docker compose down
```

Чтобы удалить также базу данных и повторно применить миграции при следующем запуске:

```bash
docker compose down --volumes
```

Настройки PostgreSQL, окружение ASP.NET и порт приложения задаются в `.env`.

#### Фронтенд

Установите зависимости в `src/CampusCore.BackOffice/client-app`:

```bash
npm ci
```

```bash
npm run development
```



#### Сервер (ASP.NET)

Один раз сохраните локальную строку подключения в User Secrets:

```bash
dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Database=campus_core;Username=postgres;Password=<password>" --project "src/CampusCore.BackOffice/CampusCore.BackOffice.csproj"
```

```bash
dotnet run --project "src/CampusCore.BackOffice/CampusCore.BackOffice.csproj" --launch-profile "CampusCore.BackOffice"
```

