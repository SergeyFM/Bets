
# Требования и спецификация на проект Bets

## 1. Введение
### Цель: 
Этот документ описывает требования и спецификацию для проекта Bets, сервиса ставок, который позволяет клиентам делать ставки на различные события.

### Скоуп: 
Проект включает разработку системы на основе микросервисной архитектуры с использованием ASP.NET, включая backend, frontend и инфраструктуру деплоя.

## 2. Обзор проекта
### Цели проекта:
- Создать надежную и масштабируемую систему ставок.
- Обеспечить удобный и безопасный интерфейс для пользователей.
- Обеспечить возможность администраторам управлять событиями и ставками.

### Заинтересованные стороны:
- Пользователи
- Администраторы системы
- Разработчики
- Тестировщики

## 3. Требования
### Функциональные требования:
- **Регистрация и аутентификация пользователей:**
  - Пользователи должны иметь возможность регистрироваться и входить в систему.
- **Создание и управление событиями:**
  - Администраторы должны иметь возможность создавать, редактировать и удалять события.
- **Ставки:**
  - Пользователи должны иметь возможность делать ставки на доступные события.
- **История ставок:**
  - Пользователи должны иметь доступ к истории своих ставок и результатам.
- **Внутренняя валюта (УЕ):**
  - Система должна использовать внутреннюю валюту, условную единицу - "УЕ".
  - Должна быть возможность обновлять счета пользователей, когда они приобретают УЕ.
  - У пользователей должна быть возможность заказать конвертацию и вывод УЕ во вне.

### Нефункциональные требования:
- **Производительность:** Система должна обрабатывать большое количество одновременных пользователей.
- **Безопасность:** Обеспечить безопасную аутентификацию пользователей и защиту данных.
- **Масштабируемость:** Система должна масштабироваться для удовлетворения растущего числа пользователей.


## 4. User Stories

### Регистрация и аутентификация:
- Как пользователь, я хочу регистрироваться на сайте, чтобы получить доступ к функционалу ставок.
- Как пользователь, я хочу восстанавливать пароль, чтобы получить доступ к моему аккаунту в случае его утери.
- Как пользователь, я хочу входить в систему с использованием двухфакторной аутентификации для повышения безопасности моего аккаунта.

### Профиль пользователя:
- Как пользователь, я хочу просматривать и редактировать информацию своего профиля, чтобы поддерживать актуальность данных.
- Как пользователь, я хочу просматривать историю своих ставок, чтобы анализировать свои прошлые решения.

### События и ставки:
- Как пользователь, я хочу просматривать список доступных событий, чтобы выбирать, на что ставить.
- Как пользователь, я хочу получать подробную информацию о каждом событии, чтобы делать информированные ставки.
- Как пользователь, я хочу делать одиночные ставки, чтобы поставить деньги на один из исходов события.
- Как пользователь, я хочу иметь возможность отменять свою ставку до начала события, если я передумал.

### Внутренняя валюта и счета:
- Как пользователь, я хочу пополнять свой счет внутренней валютой (УЕ), чтобы иметь возможность делать ставки.
- Как пользователь, я хочу переводить деньги на свой счет с банковского счета или карты, чтобы приобрести УЕ.
- Как пользователь, я хочу видеть текущий баланс своего счета, чтобы отслеживать доступные средства.
- Как пользователь, я хочу выводить деньги с моего счета на банковский счет или карту, если я выиграл.

### Уведомления и напоминания:
- Как пользователь, я хочу получать уведомления о статусе моих ставок, чтобы быть в курсе результатов.
- Как пользователь, я хочу получать напоминания о предстоящих событиях, чтобы не пропустить возможность сделать ставку.
- Как пользователь, я хочу настраивать предпочтения уведомлений, чтобы получать только нужные мне оповещения.

### Бонусы и акции:
- Как пользователь, я хочу получать бонусы при регистрации и пополнении счета, чтобы увеличить мои шансы на выигрыш.
- Как пользователь, я хочу участвовать в акциях и конкурсах, чтобы иметь возможность выиграть дополнительные призы.

### Социальные функции:
- Как пользователь, я хочу делиться своими ставками и результатами с друзьями через социальные сети, чтобы показать свои успехи.
- Как пользователь, я хочу следить за ставками и результатами других пользователей, чтобы учиться у более опытных игроков.

### Поддержка и помощь:
- Как пользователь, я хочу иметь доступ к разделу помощи и часто задаваемых вопросов (FAQ), чтобы находить ответы на мои вопросы.
- Как пользователь, я хочу обращаться в службу поддержки через чат или email, чтобы получать помощь по возникающим вопросам.

### Дополнительные User Stories для администраторов

### Управление пользователями:
- Как администратор, я хочу иметь возможность блокировать или разблокировать пользователей, чтобы поддерживать порядок на сайте.
- Как администратор, я хочу просматривать и управлять профилями пользователей, чтобы проверять и редактировать информацию.

### Управление событиями:
- Как администратор, я хочу добавлять новые события в систему, чтобы пользователи могли делать ставки.
- Как администратор, я хочу редактировать информацию о событиях, чтобы поддерживать ее актуальность.
- Как администратор, я хочу устанавливать дату публикации, срок отсечки, минимальные и максимальные суммы ставок.

### Управление ставками:
- Как администратор, я хочу просматривать и анализировать ставки пользователей, чтобы выявлять подозрительную активность.
- Как администратор, я хочу аннулировать ставки в случае выявления нарушений или ошибок.

### Управление внутренней валютой:
- Как администратор, я хочу отслеживать транзакции внутренней валюты, чтобы контролировать финансовые операции на сайте.
- Как администратор, я хочу управлять курсом обмена внутренней валюты, чтобы регулировать экономику сайта.


## 5. Архитектура системы
### Обзор: 
Система будет построена на основе микросервисной архитектуры, где каждый микросервис отвечает за определенную функцию.

### Микросервисная архитектура:
- **Сервис пользователей:** Обработка регистрации и аутентификации пользователей.
- **Сервис событий:** Управление созданием и обновлением событий.
- **Сервис ставок:** Обработка ставок пользователей.
- **Сервис истории:** Управление историей ставок и результатами.
- **Сервис валюты:** Управление счётом пользователя.

**Стек технологий:**
- **Backend:** ASP.NET
- **Frontend:** React
- **Базы данных:** SQL Server
- **CI/CD:** GitHub Actions

## 6. Дизайн спецификации
### Компоненты системы:
- **Frontend:** Интерфейс пользователя для регистрации, входа, просмотра событий и ставок.
- **Backend:** API для взаимодействия с фронтендом и микросервисами.

### Диаграммы потоков данных:
- Потоки данных между микросервисами для регистрации, создания событий и ставок.

### ER-диаграммы: 
- Схемы баз данных для управления пользователями, событиями и ставками.

### Дизайн пользовательского интерфейса: 
- Макеты экранов для регистрации, создания событий и ставок.

## 7. Бизнес и торговая логика
## Основные сущности
**Событие:** Событие, на которое можно сделать ставку (например, выборы, спортивные соревнования).
**Исход**: Возможный исход на рынке, на который можно сделать ставку (например, "Кандидат A выиграет").
**Ставка**: Перевод УЕ на тот или иной исход события.

## Процесс создания событий
1. Администратор создает событие и соответствующие исходы.
2. Пользователи делают ставки на исходы внутри события.
3. Пользователи отменяют ставки до отсечки, платя увеличивающуюся комиссию.
4. После отсечки и по окончании события происходит расчёт ставок и выплата выигрышей с учётом комиссии платформы.

## Жизненный цикл события
* Администратор создаёт событие и исходы
* Администратор задаёт дату отсечки, минимальные и максимальные размеры ставок
* Происходит публикация события
* Пользователи делают ставки на исходы
* В дату отсечки ставки прекращаются и ожидается результат события
* Администратор вносит реузультат события
* Запускается расчёт ставок
* Производятся выплаты выигрышей на кошельки пользователей

## Расчёт ставок
Например, создадим событие, что завтра пойдёт дождь. Добавляем два исхода - пойдёт (да) и не пойдёт (нет).
У нас есть 4 пользователя, они делают ставки:

- п1 - 100уе нет
- п2 - 100уе нет
- п3 - 100уе да
- п4 - 300уе да

Дождь всё-таки пощёл.

- Пользователи п1 и п2 ничего не получают.
- Общий банк 600уе.
- Право требований у п3 25% (100 из 400)
- Паво требований у п4 75% (300 из 400)

Платформа берёт комиссию с выигрышей (здесь 10%).

Выплата выигрышей:

- п1 - 0
- п2 - 0
- п3 - (150-15)=135уе
- п4 - (450-45)=405уе


## 8. План реализации
**Среда разработки:** Visual Studio, VS Code, Docker для контейнеризации.

**Стандарты кодирования:** Использование стандартизированных методов и стилей кодирования для C# и JavaScript.

**Контроль версий:** Использование Git для контроля версий и управления кодом.

## 9. План тестирования
**Стратегия тестирования:** Подход к тестированию включает юнит-тесты, интеграционные тесты и тесты интерфейса.

**Тест-кейсы:** Спецификации для тестирования функциональности регистрации, ставок и управления событиями.

**Критерии приемки:** Определение критериев для оценки соответствия требованиям.

## 10. План деплоя
**Среда деплоя:** Настройка серверов для деплоя, использование контейнеров Docker.

**CI/CD:** Настройка непрерывной интеграции и деплоя для автоматизации процесса релиза.

