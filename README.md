# texode

# Тестовое задание
## Описание:
На сегодняшний день существует множество сервисов для контроля своей физической активности (прогулок, пробежек) и дальнейшего исследования прогресса тренировок.
Считыванием показателей здоровья занимаются различные трекеры, в частности шагоме-ры, которые помогают отслеживать пройденное расстояние.
Такое устройство отправляет собранную информацию на сервер для последующего хра-нения и обработки полученных результатов. Сервер хранит информацию от различных пользователей и устройств. Для управления, мониторинга, синхронизации данных ис-пользуются самые разнообразные приложения: Mi Fit, Samsung Health, Apple Health и др.

## Постановка задачи:
Разработать C# WPF приложение с применением паттерна MVVM для возможности ана-лиза количества пройденных шагов за определенный период (с построением графика) по разным пользователям.
###Требования:
1) При разработке приложения необходимо использовать тестовый набор данных. Эти файлы с результатами измерений по некоторому количеству пользователей прилагаются к тестовому заданию (в случае отсутствия файлов обратитесь, пожалуйста, к HR-менеджеру). В каждом файле содержатся результаты по группе пользователей за кон-кретный день.
2) Файлы содержат в себе данные в JSON-формате следующего вида:
[{
    "Rank": 5,
    "User": "Сидоров Виктор",	
    "Status": "Finished",
    "Steps": 4325
},
{
    "Rank": 6,				// Рейтинг пользователя за текущий день
    "User": "Иванова Марина",	// Имя пользователя
    "Status": "Finished",		// Статус (завершил, отказался и др.)
    "Steps": 7560			// Количество пройденных шагов
}]
3) Обработка сохраненной статистики и ее графическое отображение может быть пред-ставлено в следующем виде (можно использовать график или диаграмму):
![graph]:(https://github.com/OlgaSheva/texode/blob/master/img/graph.jpg)
4) График по выбранному пользователю должен отображать зависимость пройденных ша-гов по дням.
5) Таблица пользователей должна содержать в себе следующие поля:
	Информация о пользователе (Фамилия и имя);
	Среднее количество пройденных шагов за весь период;
	Лучший результат за весь период;
	Худший результат за весь период.
6) Выделить в таблице другим цветом тех пользователей, чьи лучшие или худшие резуль-таты отличаются от среднего количества шагов за весь период (по этому пользователю) более чем на 20%.
7) Экспорт данных по выбранному пользователю на диск (на выбор: XML, JSON, CSV). Поля Rank и Status, которые не отображаются в интерфейсе, должны экспортироваться. Также должны сохраняться значения из таблицы: среднее количество пройденных шагов, лучший/худший результат.
8) Максимальное или минимальное количество шагов на графике необходимо выделить (например, раскрасить точки разными цветами).
9) Обработка ошибок при получении/сохранении данных файлов JSON-формата и др., возможность выбора нескольких файлов непосредственно из приложения.

При выполнении задания возможно использование сторонних nuget-пакетов.
