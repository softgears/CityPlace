CityPlace_Mobile.placeAddress = function (params) {

    var viewModel = {
        address: ko.observable(params.address)
    };

    return viewModel;
};