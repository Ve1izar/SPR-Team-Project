# Team3 Proj

Повностек-проєкт з фронтендом на React + Vite та бекендом на ASP.NET Core Web API з SQLite.

## Структура проєкту

```text
Team3_Project/
├─ backend/    # ASP.NET Core Web API
└─ frontend/   # React + Vite клієнт
```

## Технології та версії

### Backend (ASP.NET Core)

- .NET Target Framework: **net10.0**
- ASP.NET Core OpenAPI: **10.0.3**
- Entity Framework Core Design: **10.0.7**
- Entity Framework Core SQLite: **10.0.7**
- Entity Framework Core Tools: **10.0.7**
- База даних: **SQLite**
- Рядок підключення за замовчуванням: `Data Source=drive_archive.db`

### Frontend (React + Vite)

- React: **^19.2.5**
- React DOM: **^19.2.5**
- Vite: **^8.0.10**
- ESLint: **^10.2.1**
- @vitejs/plugin-react: **^6.0.1**

### Інші інструменти

- npm lockfile: **package-lock.json** (npm)
- CORS у backend налаштований на: **http://localhost:5173**

## Передумови (що встановити на ПК)

1. **Git**
2. **.NET SDK 10.0** (або сумісний з `net10.0`)
3. **Node.js LTS** (рекомендовано актуальний LTS) + **npm**

## Встановлення проєкту локально

### 1. Клонування репозиторію

```bash
git clone https://github.com/Ve1izar/SPR-Team-Project
cd Team3_Project
```

### 2. Налаштування та запуск Backend

```bash
cd backend
dotnet restore
dotnet run
```

Після запуску API зазвичай доступне за адресами:

- http://localhost:5229
- https://localhost:7065

Swagger у режимі Development:

- https://localhost:7065/swagger
- або http://localhost:5229/swagger

### 3. Налаштування та запуск Frontend

В окремому терміналі:

```bash
cd frontend
npm install
npm run dev
```

Frontend буде доступний за адресою:

- http://localhost:5173

## Рекомендований робочий процес

1. Запустити backend (`dotnet run` у папці `backend`).
2. Запустити frontend (`npm run dev` у папці `frontend`).
3. Відкрити фронтенд у браузері: http://localhost:5173

## Корисні команди

### Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet run
```

### Frontend

```bash
cd frontend
npm install
npm run dev
npm run build
npm run preview
npm run lint
```

## Примітки

- Файл `.gitignore` у корені вже налаштований для обох частин проєкту.
- Локальні змінні оточення для frontend (наприклад, `.env.local`) не повинні потрапляти в git.
- Якщо в майбутньому додасте EF Core migrations, застосовуйте їх командою `dotnet ef database update` у папці `backend`.
