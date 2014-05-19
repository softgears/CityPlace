CityPlace_Mobile.settings = function (params) {

    var viewModel = {
        login: ko.observable("test"),
        authorized: ko.observable(true),
        city: 'Хабаровск'
    };

    return viewModel;
};