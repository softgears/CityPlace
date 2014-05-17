CityPlace_Mobile.settings = function (params) {

    var viewModel = {
        login: ko.observable("test"),
        authorized: ko.observable(true),
        authVk: function() {
            CityPlace_Mobile.app.navigate("authorizeVk/0");
        }
    };

    return viewModel;
};