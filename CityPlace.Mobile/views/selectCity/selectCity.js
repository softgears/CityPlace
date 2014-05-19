CityPlace_Mobile.selectCity = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/cities");

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};