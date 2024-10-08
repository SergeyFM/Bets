
# Основы чистой архитектуры, зачем она, почему используется в проекте

Чистая архитектура представляет собой подход к проектированию программного обеспечения, целью которого является создание гибкой, масштабируемой и легко поддерживаемой системы. В основе чистой архитектуры лежит принцип разделения приложения на независимые слои, каждый из которых отвечает за определенный аспект функциональности.

Основные преимущества чистой архитектуры:
1. **Гибкость**: Изменения в одном слое минимально влияют на другие слои, что облегчает добавление новых функций и исправление ошибок.
2. **Масштабируемость**: Четкая структура позволяет легко масштабировать приложение по мере роста требований.
3. **Тестируемость**: Разделение ответственности упрощает тестирование отдельных компонентов системы.
4. **Понимание и поддержка**: Ясная структура кода облегчает понимание и поддержку проекта, особенно для новых членов команды.

В нашем проекте чистая архитектура используется для обеспечения высокого уровня качества кода, что критически важно в условиях постоянно меняющихся требований и необходимости быстрого реагирования на запросы пользователей.

## Состав sln-решения с названиями проектов

**Bets**: точка запуска и взаимодействия с пользователем. Содержит в себе пользовательский интерфейс и бекенд **Engine**, использующий сервисы.

1. **Bets.Core**: Основной проект, содержащий сущности и сценарии использования.
2. **Bets.Application**: Содержит интерфейсы и классы, которые связывают сценарии использования и инфраструктурные компоненты.
3. **Bets.Infrastructure**: Содержит реализацию доступа к данным, внешние сервисы и инфраструктурные компоненты.
4. **Bets.Api**: Веб-API проект, отвечающий за взаимодействие с пользователем через HTTP-запросы.
5. **Bets.UI**: Проект на React, реализующий клиентскую часть веб-приложения.
6. **Bets.Tests**: Проект для модульного и интеграционного тестирования всех слоев приложения.

## Сервисы
Сервисы реализуются отдельно и независимо, предоставляя API для работы с ними. 
В решении находятся интерфейсы и реализации этих сервисов (только для связи).
Bets.Application/ServiceInterfaces - здесь находятся интерфейсы сервисов, описывающих их api.
Bets.Infrastructure/ServiceConnectors - здесь будет лежать реализация интерфейса, которая является коннектором с соответствующим внешним сервисом.

В первое время, для скорости разработки, сервисы будут реализовываться в том же решении. В папке Services для каждого сервиса будет создан отдельный проект с классом, реализующим соответсвующий интерфейс из Bets.Application/ServiceInterfaces. По мере разрастания сервисов, они будут выноситься в отдельные решения и имплементация будет заменена на коннектор.   
DTOs для общения с сервисами находятся в Bets.Application.ServiceDTOs/Name_of_the_service.  

Управление пользователями сразу реализуется в виде отельного UserService + IdentityServer.  

Сервисы независимы, хранят текущие и исторические данные по своим сущностям. Данные не удаляются, а только помечаются на удаление. Ведётся аудит-лог.  
Разрастание БД и замусоривание историческими данными решается средствами СУБД.


## Сервисы и сущности
### UserService
**UserService Solution**: будет предоставлять функционал UserService через RESTful API.   
**IdentityServer Solution**: Этот проект будет хостить IdentityServer4, отвечающий за выдачу JWT токенов и обработку аутентификации и авторизации.
[Подробнее об архитектуре UserService](./UserService_Architecture.md)
Сущности:   
**User**: Основная сущность пользователя.  
**Role**: Роли, которые может иметь пользователь.  
**UserRole**: Связь между пользователями и ролями.  
**UserClaim**: Дополнительные утверждения для пользователей.  
**UserLogin**: Внешние логины пользователей (например, через Google, Facebook и т.д.).  
**UserToken**: Токены для двухфакторной аутентификации или восстановления пароля.  
**UserProfile**: Дополнительная информация о пользователе, такая как имя, фамилия, дата рождения, фото, документы
**Permission**: Разрешения, связанные с ролями или пользователями.  
**AuditLog**: Запись журнала аудита действий пользователя для обеспечения безопасности и отслеживания активности.

### WalletService
Cервис по работе с валютой и кошельками пользователей.  
### BetsService
Cервис биржи ставок, содержит в себе текущее состояние и историю.  
### NotificationService
Управляет рассылкой email-сообщений (в будущем также оповещения внутри сервиса).  


## Используемые технологии

1. **.NET Core 8**: Фреймворк для разработки приложений, позволяющий создавать кроссплатформенные приложения высокой производительности.
2. **ASP.NET Core 8**: Веб-фреймворк для создания современных веб-приложений и API.
3. **Entity Framework Core 8**: ORM (Object-Relational Mapper) для взаимодействия с базой данных, упрощающий выполнение запросов и управление данными.
4. **FluentValidation**: Библиотека для упрощения валидации объектов и создания декларативных правил валидации.
5. **AutoMapper**: Инструмент для автоматического сопоставления объектов различных типов, что упрощает преобразование данных между слоями.
6. **xUnit**: Популярная фреймворк для модульного тестирования .NET-приложений.
7. **FluentAssertions**: Библиотека для написания читабельных и выразительных проверок в тестах.
8. **Docker, Docker Compose**: Инструменты для контейнеризации приложений и оркестрации контейнеров, что обеспечивает переносимость и удобство развертывания.
9. **React**: JavaScript-библиотека для создания пользовательских интерфейсов, позволяющая строить масштабируемые и быстрые фронтенд-приложения.
10. **Swagger / Swashbuckle**: Инструменты для документирования и тестирования API.
11. **Serilog**: Библиотека для структурированного логирования.
12. **MediatR**: Библиотека для реализации паттерна медиатора, упрощающего взаимодействие между различными компонентами системы.
13. **Redis**: Используется для кэширования данных и улучшения производительности приложения.
14. **RabbitMQ**: Платформа для обмена сообщениями между сервисами, которая используется для реализации асинхронных процессов.

Эти технологии помогут нам создать надежное и масштабируемое приложение, обеспечивая при этом высокую производительность и удобство поддержки.
