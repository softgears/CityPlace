CityPlace_Mobile.placeAddress = function (params) {

    var viewModel = {
        address: ko.observable(params.address)
    };

    ymaps.ready(init);
    var myMap;

    function init() {
        myMap = new ymaps.Map("map", {
            center: [55.76, 37.64],
            zoom: 7
        });
    }

    return viewModel;
};