CityPlace_Mobile.news = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/publications");

    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};