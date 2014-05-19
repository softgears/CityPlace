CityPlace_Mobile.events = function (params) {
    var cityId = 1;
    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/events/"+cityId);

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};