CityPlace_Mobile.home = function (params) {
    var cityId = 1;
    var dataSource = new DevExpress.data.DataSource("http://cityplace.softgears.ru/mobile-api/home-data/"+cityId);

    var viewModel = {
        ds: dataSource,
        itemClick: function(e, itemData) {
            var id = $(e.element).attr("itemid");
            var type = $(e.element).attr("objType");
            switch (type) {
            case "event":
                CityPlace_Mobile.app.navigate("eventDetails/" + id);
                break;
            case "publication":
                CityPlace_Mobile.app.navigate("newsDetails/" + id);
                break;
            case "place":
                CityPlace_Mobile.app.navigate("placeDetails/" + id);
                break;
            default:
            }
        }
    };

    return viewModel;
};