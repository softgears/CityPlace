CityPlace_Mobile.selectCity = function (params) {

    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/cities");

    var viewModel = {
        ds: dataSource,
        selectCity: function(id) {
            window.localStorage.setItem("cityId", id);
            window.localStorage.setItem("cityName", name);
            CityPlace_Mobile.app.back();
        }
    };

    return viewModel;
};