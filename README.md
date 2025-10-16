## Лабораторная работа
### Цель
Реализация проекта сервисно-ориентированного приложения.

### Задание: Пункт велопроката
Реализация объектной модели данных и unit-тестов для пункта велопроката в рамках учебной лабораторной работы по разработке корпоративных приложений.

Предметная область: система управления пунктом проката велосипедов.

### Классы и связи

- **Bike** — конкретный велосипед.  
  Поля: `Id`, `SerialNumber`, `Color`, `Model`.

- **BikeModel** — справочник моделей велосипедов.  
  Поля: `Id`, `Name`, `Type (BikeType)`, `WheelSize`, `MaxRiderWeight`, `BikeWeight`, `BrakeType`, `ModelYear`, `HourlyRate`.

- **Renter** — арендатор.  
  Поля: `Id`, `FullName`, `Phone`.

- **Rental** — факт аренды велосипеда.  
  Поля: `Id`, `Bike`, `Renter`, `StartTime`, `DurationHours`, метод `GetTotalPrice()` для расчёта стоимости аренды.

- **BikeType (Enum)** — перечисление типов велосипедов (например: `Road`, `Mountain`, `Hybrid`, `BMX`, `Electric`).

DataSeeder — это вспомогательный класс, который создаёт тестовые данные.


### Unit-тесты
- **GetAllSportBikes_ReturnsOnlySportModels()**

Проверяет, что запрос возвращает только спортивные велосипеды.

- **GetTop5ModelsByProfit_ReturnsCorrectOrder()**

Считает топ-5 моделей по прибыли от аренды (на основе цены * часов).

- **GetTop5ModelsByDuration_ReturnsCorrectOrder()**

Считает топ-5 моделей по длительности аренды.

- **GetMinMaxAverageRentalTime_ReturnsCorrectValues()**

Вычисляет минимальное, максимальное и среднее время аренды среди всех записей.

- **GetTotalRentalTimeByType_ReturnsCorrectSum()**

Считает суммарное время аренды по каждому типу велосипеда.

- **GetTopRentersByCount_ReturnsCorrectClients()**

Определяет арендаторов, которые чаще всего брали велосипеды в прокат.
