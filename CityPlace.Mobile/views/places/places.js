CityPlace_Mobile.places = function (params) {
    var cityId = 1;
    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/places/"+params.id+"?cityId="+cityId);

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};