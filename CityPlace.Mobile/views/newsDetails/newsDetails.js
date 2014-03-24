CityPlace_Mobile.newsDetails = function (params) {

    var viewModel = {
        id: params.id,
        title: ko.observable("Загрузка..."),
        pdate: ko.observable("Загрузка..."),
        content: ko.observable("Загрузка..."),
        img: ko.observable("Загрузка...")
    };
    
    $.getJSON("http://cityplace.softgears.ru/mobile-api/publication/" + params.id, function (data) {
        viewModel.title(data.title);
        viewModel.content(data.content);
        viewModel.img(data.img);
        viewModel.pdate(data.pdate);
    });

    return viewModel;
};