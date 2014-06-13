CityPlace_Mobile.placeEvents = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/places/events/" + params.id);

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};