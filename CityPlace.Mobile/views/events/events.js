CityPlace_Mobile.events = function (params) {

    var viewModel = {
        eventsArray: [
            {
                key: 'Сегодня',
                items: [
                    { id: 1, title: "Событие 1" },
                    { id: 2, title: "Событие 2" },
                    { id: 3, title: "Событие 3" },
                ]
            },
            {
                key: 'Завтра',
                items: [
                    { id: 1, title: "Событие 4" },
                    { id: 2, title: "Событие 5" },
                    { id: 3, title: "Событие 6" },
                ]
            },
            {
                key: '03.04.2014',
                items: [
                    { id: 1, title: "Событие 7" },
                ]
            },
            {
                key: '05.04.2014',
                items: [
                    { id: 1, title: "Событие 8" },
                ]
            }
        ]
    };

    return viewModel;
};