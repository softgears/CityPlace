window.CityPlace_Mobile = window.CityPlace_Mobile || {};

$(function() {
    CityPlace_Mobile.app = new DevExpress.framework.html.HtmlApplication({
        namespace: CityPlace_Mobile,
        defaultLayout: CityPlace_Mobile.config.defaultLayout,
        navigation: CityPlace_Mobile.config.navigation
    });
    CityPlace_Mobile.app.router.register(":view/:id", { view: "home", id: undefined });

    CityPlace_Mobile.app.navigate();

});
