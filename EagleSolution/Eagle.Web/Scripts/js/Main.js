define(['/Scripts/jquery-2.1.4.min', '/Scripts/bootstrap.min', '/Scripts/TweenMax.min', '/Scripts/resizeable', '/Scripts/joinable', '/Scripts/xenon-api', '/Scripts/xenon-toggles', '/Scripts/xenon-custom', '/Scripts/xenon-notes', '/Scripts/xenon-widgets', '/Scripts/knockout.mapping-latest', '/Scripts/jquery.tmpl.min', 'checkTable'], function (require) {

    // Here is just a sample how to open chat conversation box
    $(document).ready(function ($) {


        var $chatConversation = $(".chat-conversation");

        $(".chat-group a").on('click', function (ev) {
            ev.preventDefault();

            $chatConversation.toggleClass('is-open');

            $(".chat-conversation textarea").trigger('autosize.resize').focus();
        });

        $(".conversation-close").on('click', function (ev) {
            ev.preventDefault();
            $chatConversation.removeClass('is-open');
        });
    });

    var sysbtnClick = function (mainbody, branch, btn) {
        var randomNum = Math.random();
        var path = mainbody.map() + '/' + btn.ActionName() + '?r=' + randomNum;
        var callBack = function (data) {
            mainbody.dialog(data);
            jQuery('#system-dialog').modal('show', { backdrop: 'static' });
        };

        if (btn.Post) {
            var id = new Array();
            $("#MainPart tbody div[class*='cbr-checked'] input").each(function () {
                id.push($(this).val());
            });
            $.post(path, { id: JSON.stringify(id) }, function (data) { callBack(data) });
        } else {
            $.get(path, callBack(data));
        }
    }

    return {
        checkFun: require('checkTable').checkFun,
        sysbtnClick: sysbtnClick,
        alert: require('Alert').Alert,
    }
});



define('checkTable', ['/Scripts/jquery-2.1.4.min'], function () {

    var checkEvent = function (tableName) {
        // Replace checkboxes when they appear
        var $state = $("#" + tableName + " thead input[type='checkbox']");

        $("#" + tableName).on('draw.dt', function () {
            cbr_replace();
            $state.trigger('change');
        });

        // Script to select all checkboxes
        $state.on('change', function (ev) {
            var $chcks = $("#" + tableName + " tbody input[type='checkbox']");

            if ($state.is(':checked')) {
                $chcks.prop('checked', true).trigger('change');
            }
            else {
                $chcks.prop('checked', false).trigger('change');
            }
        });
    }
    return {
        checkFun: checkEvent
    }
});


define('Alert', [], function (require) {


    var showMessageBox = function (result) {
        var opts = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-bottom-center",
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        if (result.Flag) {
            toastr.success(result.Message || "成功", opts);
        } else {
            toastr.error(result.Message || "失败", opts);
        }
    }

    return {
        Alert: showMessageBox
    }
});
