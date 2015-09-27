define(['/Scripts/jquery-2.1.4.min', '/Scripts/bootstrap.min', '/Scripts/TweenMax.min', '/Scripts/resizeable', '/Scripts/joinable', '/Scripts/xenon-api', '/Scripts/xenon-toggles', '/Scripts/xenon-custom', '/Scripts/xenon-notes', '/Scripts/xenon-widgets', '/Scripts/knockout.mapping-latest', '/Scripts/jquery.tmpl.min', 'checkTable'], function (require) {
    var alertClass = require('Alert');

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
        var path = mainbody.map() + '/' + btn.ActionName + '?r=' + randomNum;
        var callBack = function (data) {
            if (data.Flag || !btn.Callback) {
                alertClass.Alert(data);
                if (btn.Reload) {
                    window.mainModel.ReloadBranch();
                }
                return;
            }
            mainbody.dialog(data);
            jQuery('#system-dialog').modal('show', { backdrop: 'static' });
        };

        var inputlist = $("#MainPart tbody div[class*='cbr-checked'] input");
        var id = null;
        if (btn.ParamNum < 0 || btn.ParamNum > 1) {
            if (inputlist == null || (btn.ParamNum !== inputlist.length && btn.ParamNum > 1)) {
                alertClass.AlertError("请至少选择一条要操作的数据");
                return;
            }
            id = new Array();
            inputlist.each(function (index) {
                id[index] = $(this).val();
            });
        } else if (btn.ParamNum === 1) {
            if (inputlist.length != 1) {
                alertClass.AlertError("请选择一条要操作的数据");
                return;
            }
            id = inputlist.val();
        }

        $.ajax({
            url: path,
            data: { id: id },
            type: btn.Post ? 'POST' : 'GET',
            success: function (data) {
                callBack(data);
            }
        });

    }

    return {
        checkFun: require('checkTable').checkFun,
        sysbtnClick: sysbtnClick,
        alert: alertClass.Alert,
        alertError: alertClass.AlertError,
        buttons: require('systemButton').buttons,
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

    var showMessageBox = function (result) {
        if (result.Flag) {
            toastr.success(result.Message || '提交成功', opts);
        } else {
            toastr.error(result.Message || '操作失败', opts);
        }
    }
    var showError = function (message) {
        toastr.error(message || '操作失败', opts);
    }

    return {
        Alert: showMessageBox,
        AlertError: showError,
    }
});


define('systemButton', [], function (require) {
    function actionButton(name, actionName, post, dialog, callback, paramNum, reload) {
        this.Name = name;
        this.ActionName = actionName;
        this.Post = post;
        this.Dialog = dialog;
        this.Callback = callback;
        this.ParamNum = paramNum;
        this.Reload = reload;
    };

    var buttonList = [
        new actionButton("新增", "Create", false, 1, true, 0, false),
        new actionButton("修改", "Create", false, 1, true, 1, false),
        new actionButton("删除", "Delete", true, 0, false, -1, true)
    ];

    return {
        buttons: buttonList
    }
});