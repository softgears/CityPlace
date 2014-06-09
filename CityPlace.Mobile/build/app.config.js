window.CityPlace_Mobile = $.extend(true, window.CityPlace_Mobile, {
    "config": {
        "defaultLayout": "slideout",
        "navigation": [
            {
                title: "Сегодня", // События сегодня, заведения дня, реклама
                action: "#home",
                icon: "home"
            },
            {
                title: "Новости", // Новости города
                action: "#news",
                icon: "news"
            },
            {
                title: "События", // Календарь со списком мероприятий ближайщих
                action: "#events",
                icon: "events"
            },
            {
                title: "Заведения", // Сперва категории заведений: пожрать, кино, клубы, сауны, бары, биллиарды -> список заведений в категории: логотип, название -> карточка заведения: время работы, описания, рейтинг, чекины, средний чек, подписка на уведомления о новых мероприятииях
                action: "#categories",
                icon: "places"
            },
            {
                title: "Настройки", // Выбор города, отключение и включение уведомлений, регистрация и профиль
                action: "#settings",
                icon: "settings"
            },
            {
                title: "О приложении", // Информация о приложении
                action: "#about",
                icon: "about"
            }
        ]
    }
});