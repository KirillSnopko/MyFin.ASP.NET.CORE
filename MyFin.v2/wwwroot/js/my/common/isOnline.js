$(document).ready(function () {
    $(window).on('load', function () {
        const isOnline = window.navigator.onLine;
        if (isOnline) {
            $('#widget').html('<span class="badge bg-success">Online</span><br/>' +
                '<fxwidget-er inverse="false" amount="1" decimals="2" large="false" shadow="true" symbol = "true" flag = "true" changes = "true" grouping = "true" border = "false" main-curr="BYN" sel-curr="RUB,USD,EUR," background-color="#9bddff" border-radius="0.35" ></fxwidget-er> ' +
                '<br/>' +
                '<fxwidget-cc amount="100" decimals="2" large="false" shadow="true" symbol="false" grouping="true" border = "false" from = "USD" to = "EUR" background-color="#89cff0" border-radius="0.35" pop-curr="BYN,RUB,USD,EUR" ></fxwidget-cc> ');
        } else {
            $('#widget').html('<span class="badge bg-danger">Offline</span>')
        }
    });
});