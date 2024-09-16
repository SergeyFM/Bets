
# UI Спецификация для сайта ставок "CoolBet"

В этой спецификации описаны основные элементы пользовательского интерфейса и их реализации. Даны примеры информации на страницах.   
Имплетентация средствами MVC ASP NET.

Все страницы открываются в основном layout.
## Меню
- **CoolBet** - ссылка на главную страницу
- **События и ставки** - страница с доступными событиями для ставок
- **Мои ставки** - список ставок пользователя
- **Личный кабинет** - информация о пользователе и его аккаунте
- **Информация** - информация для пользователей

На верхней правой части страницы:
- **Зайти** или **Зарегистрироваться**, если пользователь не вошел в систему
- Имя пользователя, если пользователь уже авторизован

## Главная страница
На главной странице отображаются следующие блоки:

### Краткое описание платформы
- "CoolBet - самая надежная платформа для ставок на различные события. Мы предлагаем пользователям возможность делать ставки на исходы интересных и актуальных событий по всему миру."

### Отзывы пользователей
1. "Отличная платформа! Сделал несколько ставок на спортивные события и остался доволен результатами. Всё честно и прозрачно!" – Иван Петров
2. "Никогда не думала, что ставки могут быть такими увлекательными. Уже выиграла несколько раз, советую всем!" – Мария Смирнова
3. "CoolBet действительно делает процесс ставок простым и понятным. Ставил на курс рубля – всё чётко!" – Алексей Иванов

### Быстрое руководство по началу работы
- "1. Зарегистрируйтесь на сайте"
- "2. Пополните ваш кошелек"
- "3. Выберите событие и сделайте ставку"
- "4. Следите за исходом события и получите свои выигрыши"

## События и ставки
На этой странице отображается список доступных событий, на которые можно сделать ставки. Включает следующие элементы:

- Поисковое поле для поиска событий по ключевым словам
- Результаты поиска включают кнопку **Отменить** для сброса результатов

### Пример блока события:
- Название: "Кто победит в президентских выборах в США?"
- Количество участников: "9 человек"
- Размер банка: "35 000 руб."

### Дополнительные примеры событий:
- Название: "Исход выборов премьер-министра Великобритании"
  - Количество участников: "7 человек"
  - Размер банка: "25 000 руб."

- Название: "Исход финала чемпионата мира по футболу 2026"
  - Количество участников: "20 человек"
  - Размер банка: "150 000 руб."

- Название: "Курс RUR/USD – будет ли больше 100 руб. к 1 февраля 2025 года?"
  - Количество участников: "15 человек"
  - Размер банка: "45 000 руб."

При нажатии на событие открывается новая вкладка с детальной информацией о событии.

## Страница события
На странице события пользователь видит следующую информацию:

- Название события: "Кто победит в президентских выборах в США?"
- Количество участников: "9 человек"
- Размер банка: "35 000 руб."
- Таблица исходов, включая:
    - Исход: "Победит Камала Харрис"
    - Количество ставок: "6"
    - Потенциальный выигрыш: "1 500 руб."

Пользователь может ввести свою ставку в поле, например, 1000 руб. Поле **Потенциальный выигрыш** автоматически обновляется.

Кнопки:
- **Сделать ставку** - при успешной ставке показывается сообщение "Ставка сделана"
- Если ставка сделана, кнопка меняется на **Отменить ставку** - при успешной отмене показывается сообщение "Ставка отменена"

## Мои ставки
Раздел включает три списка событий:

- **Активные события** - отображается текущая сумма ставки
- **Завершённые события** - отображается выигрыш для каждого события
- **Просмотренные события** - стандартная информация о событиях

## Личный кабинет
Это профиль пользователя. Включает следующие поля:

- **Имя**: Иван Иванов
- **Электронная почта**: ivan@example.com
- **Номер телефона**: +7 999 123 4567
- **Дата рождения**: 12 января 1990 года
- **Фото профиля** - изображение, которое можно загрузить или изменить

Все поля редактируемые. Если какое-то поле изменено, появляется кнопка **Сохранить**.

## Информация

### История платформы CoolBet
CoolBet была основана в 2023 году группой энтузиастов, которые стремились сделать ставки на события простыми и доступными для всех. Наша платформа была создана для того, чтобы каждый пользователь мог с лёгкостью делать ставки на исходы интересующих его событий — от спортивных матчей до политических выборов и экономических прогнозов. CoolBet быстро завоевала популярность благодаря удобному интерфейсу, прозрачным условиям ставок и честным расчетам выигрышей.

Мы верим, что ставки должны приносить удовольствие, и стараемся сделать их максимально понятными и безопасными для наших пользователей.

### Дисклеймер
CoolBet напоминает своим пользователям, что ставки могут быть связаны с рисками. Прежде чем делать ставку, пожалуйста, учтите следующее:

1. **Финансовые риски**: Ставки могут привести к потере средств. Делайте ставки только на те суммы, которые вы готовы потерять.
2. **Психологические риски**: Ставки могут вызвать азарт, что может привести к финансовым потерям и стрессу. Пожалуйста, играйте ответственно.
3. **Юридические риски**: Законы о ставках могут различаться в зависимости от вашей юрисдикции. Убедитесь, что вы следуете местным законам и правилам.

CoolBet не несет ответственности за убытки, понесенные пользователями в результате ставок. Если вы чувствуете, что ставки становятся для вас проблемой, обратитесь за профессиональной помощью.

### Контактная форма для связи с поддержкой.

## Основные объекты в решении
- **Событие**: BetEvent
- **Исход**: Outcome
- **Ставка**: UserBet

## Используемые сервисы в контроллерах
- **IBetsService**
- **INotificationService**
- **IUserService**
- **IWalletService**
- **ILogger**