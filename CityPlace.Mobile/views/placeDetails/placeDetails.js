CityPlace_Mobile.placeDetails = function (params) {

    var viewModel = {
        id: params.id,
        title: ko.observable("Загрузка..."),
        img: ko.observable("Загрузка..."),
        description: ko.observable("Загрузка..."),
        address: ko.observable("Загрузка..."),
        phone1: ko.observable("Загрузка..."),
        phone2: ko.observable("Загрузка..."),
        phone3: ko.observable("Загрузка..."),
        fax: ko.observable("Загрузка..."),
        site: ko.observable("Загрузка..."),
        email: ko.observable("Загрузка..."),
        lat: ko.observable(null),
        lon: ko.observable(null),
        work_time: ko.observable("Загрузка..."),
        cash: ko.observable(false),
        cashless: ko.observable(false),
        booking: ko.observable(false),
        ordering: ko.observable(false),
        wifi: ko.observable(false),
        vip: ko.observable(false),
        liveMusic: ko.observable(false),
        businessLunch: ko.observable(false),
        check: ko.observable(0.0),
        hasImage: function () {
            return viewModel.img() != "";
        }
    };
    
    $.getJSON("http://cityplace.softgears.ru/mobile-api/place/" + params.id, function (data) {
        viewModel.title(data.title);
        viewModel.description(data.description);
        viewModel.img(data.img);
        viewModel.address(data.address);
        viewModel.phone1(data.phone1);
        viewModel.phone2(data.phone2);
        viewModel.phone3(data.phone3);
        viewModel.fax(data.fax);
        viewModel.site(data.site);
        viewModel.email(data.email);
        viewModel.lat(data.lat);
        viewModel.lon(data.lon);
        viewModel.work_time(data.work_time);
        viewModel.cash(data.cash);
        viewModel.cashless(data.cashless);
        viewModel.booking(data.booking);
        viewModel.ordering(data.ordering);
        viewModel.wifi(data.wifi);
        viewModel.vip(data.vip);
        viewModel.liveMusic(data.liveMusic);
        viewModel.businessLunch(data.businessLunch);
        viewModel.check(data.check);
    });

    return viewModel;
};