window.CityPlace_Mobile = window.CityPlace_Mobile || {};

$(function () {
    DevExpress.devices.current({
        phone: true,
        platform: 'generic'
    });
    CityPlace_Mobile.app = new DevExpress.framework.html.HtmlApplication({
        namespace: CityPlace_Mobile,
        defaultLayout: CityPlace_Mobile.config.defaultLayout,
        navigation: CityPlace_Mobile.config.navigation
    });
    CityPlace_Mobile.app.router.register(":view/:id", { view: "home", id: undefined });

    CityPlace_Mobile.app.navigate();

    $.support.cors = true;

    pushNotification = window.plugins.pushNotification;

    // Регистрируем приложение
    if (device.platform == 'android' || device.platform == 'Android' || device.platform == "amazon-fireos") {

        pushNotification.register(
            successHandler,
            errorHandler,
            {
                "senderID": "replace_with_sender_id",
                "ecb": "onNotification"
            }
       );
    } else {
        pushNotification.register(
            tokenHandler,
            errorHandler,
            {
                "badge": "true",
                "sound": "true",
                "alert": "true",
                "ecb": "onNotificationAPN"
            }
       );
    }
});

/* PUSH */
var pushNotification;

// iOS
function onNotificationAPN(event) {
    if (event.alert) {
        navigator.notification.alert(event.alert);
    }

    if (event.sound) {
        var snd = new Media(event.sound);
        snd.play();
    }

    if (event.badge) {
        pushNotification.setApplicationIconBadgeNumber(successHandler, errorHandler, event.badge);
    }
}

function tokenHandler(result) {
    alert('device token = ' + result);
}

// Android and Amazon Fire OS
function onNotification(e) {
    // TODO: пуш уведомления на Android
}

// Успешная PUSH регистрация
function successHandler(result) {
    alert('result = ' + result);
}

// Не удалось пуш зарегистрировать
function errorHandler(error) {
    alert('error = ' + error);
}