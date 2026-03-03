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
dotnet run --project "src/CampusCore.BackOffice/CampusCore.BackOffice.csproj"
```
