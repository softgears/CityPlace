CityPlace_Mobile.settings = function (params) {

    var viewModel = {
        login: ko.observable("test"),
        authorized: ko.observable(true),
        city: ko.observable('Хабаровск')
    };

    var cityName = window.localStorage.getItem("cityName");
    if (cityName) {
        viewModel.city(cityName);
    }

    return viewModel;
};