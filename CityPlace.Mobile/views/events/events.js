CityPlace_Mobile.events = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/events");

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};