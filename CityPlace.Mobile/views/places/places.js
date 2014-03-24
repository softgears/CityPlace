CityPlace_Mobile.places = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/places/"+params.id);

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};