# Monopoly

Серверная часть «Монополии» — многопользовательской игры, которую можно поднять
на своём сервере. Написана на .NET по чистой архитектуре: домен ничего не знает
про базу и HTTP, каждый сценарий вынесен в отдельный use case.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-Npgsql-512BD4?style=flat-square)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=flat-square&logo=postgresql&logoColor=white)

> Проект в активной разработке: игровая механика ещё собирается, часть методов
> репозиториев не реализована. Готовы авторизация, схема данных и каркас сценариев.

## Архитектура

Четыре проекта, зависимости направлены строго внутрь:

```
MonopolyBack                → HTTP: minimal API endpoints, DTO, DI
MonopolyBack.Application    → сценарии, интерфейсы репозиториев и auth
MonopolyBack.Infrastructure → EF Core, репозитории, хеширование, middleware
MonopolyBack.Domain         → сущности, без единой внешней зависимости
```

Каждый сценарий — это тройка `Command` → `UseCase` → `Result`, лежащая в своей
папке. Приложение не знает, откуда придут данные: `Application` объявляет
интерфейсы (`IGameRepository`, `IUserRepository`, `IPasswordHasher`), а
`Infrastructure` их реализует.

### Сценарии

| Область | Сценарий | Назначение |
|---|---|---|
| Auth | `LoginUseCase` | Вход, выдача access-токена |
| Auth | `LogoutUseCase` | Завершение сессии |
| Main | `CreateGameUseCase` | Создание игровой комнаты |
| Main | `ReconnectGameUseCase` | Возврат в игру после обрыва связи |
| Main | `EndGameUseCase` | Завершение партии |

### Доменные модели

`User`, `Game`, `GamePlayer`, `GameStatus`, `AuthSession`, `BoardCard`,
`BoardCardType` — поля карточек поля, статусы партии и связь игроков с игрой.

## Безопасность

- Пароли — **PBKDF2** (`Pbkdf2PasswordHasher`)
- Access-токены хранятся в базе только в виде **SHA-256**-хеша (`Sha256AccessTokenHasher`)
- Проверка токена вынесена в `AuthenticationMiddleware`, текущий пользователь
  доступен через `ICurrentUserService`
- Сессии живут в таблице `AuthSessions` — их можно отзывать

## API

| Метод | Путь | Описание |
|---|---|---|
| `POST` | `/auth/login` | Вход по логину и паролю |
| `POST` | `/auth/logout` | Выход |
| `POST` | `/main/createGame` | Создать партию |
| `POST` | `/main/reconnect` | Переподключиться к партии |
| `POST` | `/main/endGame` | Завершить партию |

Примеры запросов — в [`MonopolyBack.http`](MonopolyBack/MonopolyBack/MonopolyBack.http).

## Запуск

Нужны .NET 10 SDK и PostgreSQL.

```bash
git clone https://github.com/SK0LX/monopoly.git
cd monopoly/MonopolyBack
```

Строка подключения задаётся в `appsettings.json` либо переменной окружения:

```bash
export ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=monopoly;Username=...;Password=..."
dotnet run --project MonopolyBack
```

## Структура

```
MonopolyBack/
├── MonopolyBack/                 # входной слой
│   ├── EndPoints/                # MainEndpoints, AuthenticationEndpoints
│   │   └── Model/                # запросы и ответы
│   ├── Program.cs
│   └── DependencyInjection.cs
├── MonopolyBack.Application/
│   ├── Abstractions/             # интерфейсы Auth и Persistence
│   ├── Auth/                     # Login, Logout
│   └── Main/                     # CreateGame, ReconnectGame, EndGame
├── MonopolyBack.Infrastructure/
│   ├── Auth/                     # хеширование, middleware
│   └── Persistence/              # DbContext, конфигурации, репозитории
└── MonopolyBack.Domain/
    └── Model/                    # доменные сущности
```

## Дальше в планах

- Доделать реализацию `GameRepository`
- Игровая механика: ходы, покупка и аренда, аукционы, банкротство
- Реальное время между игроками
