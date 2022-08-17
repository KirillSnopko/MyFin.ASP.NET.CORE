﻿$(document).ready(function () {


    $(window).on('load', function () {
        $.get("/Credit/List", {},
            function (credits) {
                var table = $('<table class="table"><tr><th>Name</th><th>Returned</th><th>Initial value</th><th>Open date</th><th>Closed date</th><th>Status</th><th>Options</th></tr>');
                $(credits).each(function (index, item) {
                    var id = item.id;
                    var info = item.comment;
                    var returned = item.returnedAmount;
                    var balanceOwed = item.initialAmount;
                    var date1 = item.date1;
                    var date2 = item.date2;
                    var type = item.typeOfMoney;

                    if (returned < balanceOwed) {
                        table.append('<tr class="table-default"><td>' + info + '</td><td>' + returned + type + '</td><td><strong>' + balanceOwed + type + '</strong></td > <td>' + date1 + '</td><td>' + date2 + '</td><td>' + getProgress(balanceOwed, returned) + '</td><td>' + button1(id) + '</td></tr >');
                    } else {
                        table.append('<tr class="table-success"><td>' + info + '</td><td>' + returned + type + '</td><td><strong>' + balanceOwed + type + '</strong></td > <td>' + date1 + '</td><td>' + date2 + '</td><td>' + getProgress(balanceOwed, returned) + '</td><td>' + button2(id) + '</td></tr >');
                    }
                });
                table.append('</table>');
                $('#credits').html(table);


                $(".delete").click(function (e) {
                    var b = e.target.getAttribute('data-value');
                    $('#idCredit_delete').val(b);
                });
                $(".reduce").click(function (e) {
                    var b = e.target.getAttribute('data-value');
                    $('#idCredit_reduce').val(b);
                });
                $(".history").click(function (e) {
                    var b = e.target.getAttribute('data-value');

                    $.get("/Credit/HistoryById?id=" + b, {},
                        function (history) {
                            var table = $('<table class="table"><tr><th>Date</th><th>Info</th><th>Returned</th></tr>');
                            $(history).each(function (index, item) {
                                var date = item.date;
                                var info = item.comment;
                                var returned = item.value;
                                table.append('<tr class="table-default"><td>' + date + '</td><td>' + info + '</td><td><strong>' + returned + '</strong></td ></tr >');

                            });
                            table.append('</table>');
                            $('#credit_history').html(table);
                        });

                }
                );
            }
        );





        function getProgress(x1, x2) {
            var diff;
            if (x2 != 0) {
                diff = x2 / x1 * 100;
            } else {
                diff = 0;
            }
            diff = diff.toFixed(2);
            return '<div class="progress"><div class="progress-bar progress-bar-striped active" role = "progressbar" aria - valuenow=' + diff + 'aria - valuemin="0" aria - valuemax="100" style = "width:' + diff + '%" >' + diff + '%</div ></div>'
        }

        function button1(id) {
            return '<div class="dropdown">' +
                '<button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                ' Settings' +
                ' </button>' +
                '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
                '<a class="dropdown-item reduce" href="#" data-toggle="modal" data-target="#reduce" id="reduce_credit" data-value = "' + id + '" >Reduce</a>' +
                '<a class="dropdown-item delete" href="#" data-toggle="modal" data-target="#delete" id="delete_credit" data-value = "' + id + '" >Delete</a>' +
                '<a class="dropdown-item history" href="#" data-toggle="modal" data-target="#history" id="history_credit" data-value = "' + id + '" >History</a>' +
                ' </div>' +
                '</div>'
        }

        function button2(id) {
            return '<div class="dropdown">' +
                '<button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                ' Settings' +
                ' </button>' +
                '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
                '<a class="dropdown-item delete" href="#" data-toggle="modal" data-target="#delete" id="delete_credit" data-value = "' + id + '" >Delete</a>' +
                '<a class="dropdown-item history" href="#" data-toggle="modal" data-target="#history" id="history_credit" data-value = "' + id + '" >History</a>' +
                ' </div>' +
                '</div>'
        }

    });
});


