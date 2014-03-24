CityPlace_Mobile.eventDetails = function (params) {

    var viewModel = {
        id: params.id,
        title: ko.observable("Нет данных"),
        description: ko.observable("Нет данных"),
        img: ko.observable("Нет данных"),
        event_start: ko.observable("Нет данных"),
        event_end: ko.observable("Нет данных"),
    };

    $.getJSON("http://cityplace.softgears.ru/mobile-api/event/" + params.id, function(data) {
        viewModel.title(data.title);
        viewModel.description(data.description);
        viewModel.img(data.img);
        viewModel.event_start(data.event_start);
        viewModel.event_end(data.event_end);
    });

    return viewModel;
};