$(document).keydown(function (event) {
    var c = 0;
    event = (event || window.event);
    if (event.keyCode == 113) {
        window.location.href = ShortcutKeyURL + '/ExtensionOfStay/ViewExtensionOfStay';
    }
    else if (event.keyCode == 115) {
        window.location.href = ShortcutKeyURL + '/Registration/AdmissionList';
    }
    else if (event.keyCode == 118) {
        window.location.href = ShortcutKeyURL + '/BlockPackage/ViewBlockPackage';
    }
    else if (event.keyCode == 119) {
        window.location.href = ShortcutKeyURL + '/UnblockPackage/ViewUnblockPackage';
    }
    else if (event.keyCode == 120) {
        window.location.href = ShortcutKeyURL + '/Discharge/ViewDischarge';
    }
});