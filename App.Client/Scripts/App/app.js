/* **************************************************************************************************** */
/* app.js                                                                                               */
/* global configuration                                                                                 */
/* **************************************************************************************************** */

var ajaxLoader = "#ajax-loader";
var loadTimeout;
var timeoutValue = 2000;
var alertContainer = 'alert-container';
var alertFloat = '.alert-float';
var message = 'Network busy or large data requested! Please wait...';

/* Executes everytime an ajax request is started. */
$(document).ajaxStart(function () {
    $(ajaxLoader).removeClass("fa fa-fw fa-lg fa-bolt");
    $(ajaxLoader).addClass("fa fa-fw fa-lg fa-cog fa-spin");
    loadTimeout = setTimeout(function () {
        $(ajaxLoader).addClass("fa fa-fw fa-lg fa-cog fa-spin");
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-warning\'>' + '<p> <span class=\'fa fa-fw fa-lg fa-info-circle\'></span>' + message + '<p/></div>';
    }, timeoutValue);

    console.log("Ajaxt Request Started - " + new Date($.now()));
});

/* Executes everytime an ajax request has completed. */
$(document).ajaxComplete(function () {
    clearTimeout(loadTimeout);
    setTimeout(function () {
        $(ajaxLoader).removeClass("fa fa-fw fa-lg fa-cog fa-spin");
        $(ajaxLoader).addClass("fa fa-fw fa-lg fa-bolt");
        $(alertFloat).fadeOut(500, 0).slideUp(500, function () {
            $(this).remove();
        });
        console.log("Ajax Request Completed - " + new Date($.now()));
    }, 500);
});




/* Bootstrap alerts. */
function BootstrapAlert(data) {

    var alertContainer = 'alert-container';
    var alertFloat = '.alert-float';
    var danger = 1;
    var info = 2;
    var success = 3;
    var warning = 4;


    /* Danger */
    if (data.type === danger) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-danger\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-times-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }
        /* Info */
    else if (data.type === info) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-info\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-info-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }
        /* Success */
    else if (data.type === success) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-success\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-check-circle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }
        /* Warning */
    else if (data.type === warning) {
        document.getElementById(alertContainer).innerHTML = '<div class=\'alert alert-float alert-warning\'><button type=\'button\' class=\'close\' data-dismiss=\'alert\'>×</button>' + '<p> <span class=\'fa fa-exclamation-triangle fa-fw fa-lg\'></span>' + data.message + '<p/></div>';
        window.setTimeout(function () {
            $(alertFloat).fadeOut(2000, 0).slideUp(2000, function () {
                $(this).remove();
            });
        }, 2000);
    }
}


// some product base api address.
var someProductBaseApi = 'http://localhost:49923/api/DemoAuditData/';