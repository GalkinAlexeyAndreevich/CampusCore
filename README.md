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
Строка подключения сейчас захардкожена в `src/CampusCore.Tools/Utils/DatabaseUtils.cs`:

`Server=localhost;Username=postgres;Password=password;Database=campus_core`

Создайте базу в PostgreSQL: `campus_core`.

Миграции (создание таблиц) лежат в `src/CampusCore.Migrator/Migrations/000000/`.
Сейчас `CampusCore.Migrator` не реализован, поэтому SQL необходимо применить вручную.

### Запуск
#### Фронтенд
Установите зависимости в `src/CampusCore.BackOffice/client-app`:

```bash
npm ci
```

```bash
npm run development
```

#### Сервер (ASP.NET)

```bash
dotnet run --project "src/CampusCore.BackOffice/CampusCore.BackOffice.csproj" --launch-profile "CampusCore.BackOffice"
```
