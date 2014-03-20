CityPlace_Mobile.home = function (params) {

    var dataItems = [
        {
            key: 'Мероприятия',
            items: [
                {id: 1, title: "Событие 1"},
                {id: 2, title: "Событие 2"},
                {id: 3, title: "Событие 3"},
            ]
        },
        {
            key: 'Новости',
            items: [
                { id: 1, title: "Новость 1" },
                { id: 2, title: "Новость 2" },
                { id: 3, title: "Новость 3" },
            ]
        }
    ];

    var viewModel = {
        homeDataItems: dataItems
    };

    return viewModel;
};