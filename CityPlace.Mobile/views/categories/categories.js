CityPlace_Mobile.categories = function (params) {
    
    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/categories");
    
    var viewModel = {
        ds: dataSource
    };

    return viewModel;
};