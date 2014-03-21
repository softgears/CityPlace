CityPlace_Mobile.home = function (params) {
    
    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/home-data");

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};