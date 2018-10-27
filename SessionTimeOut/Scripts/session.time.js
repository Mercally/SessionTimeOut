
/*
 * Author: josuemercally@gmail.com
 * Date: 27/10/2018
 * 
 *
 */

"use strict";

$(window).load(function () {
    $(document).ready(function () {
        $("body").on("click", "button#btn-close-session-modal", function () {
            closeSession();
        });

        $("body").on("click", "button#btn-keep-session-modal", function () {
            keepSession();
        });

        var sessionInterval = 0;
        var countModalInterval = 0;

        var initTimeOut = getInitialTimeOut();
        displaySesion();

        var secWaitCloseSesssion = initTimeOut.SecondsWaitCloseSesssion;

        function displaySesion() {
            if (!initTimeOut.InSession) {
                $("h2#status-session-time").text("Off");
                closeSessionAlert("El tiempo de permanencia de usuario inactivo se ha agotado.");
            } else {
                $("h2#status-session-time").text("On");
                setIntervalSession();
            }
        }

        function SessionTime(inSession, timeOut, secondsWaitCloseSesssion) {
            this.InSession = inSession;
            this.TimeOut = timeOut;
            this.SecondsWaitCloseSesssion = secondsWaitCloseSesssion;
        }

        function salert(title, body, type) {
            if (typeof swal !== "undefined") {
                swal(title, body, type);
            } else {
                alert(title + "\n" + body);
            }
        }

        function setIntervalSession() {
            window.clearInterval(countModalInterval);
            if (initTimeOut.TimeOut > 0) {
                sessionInterval = window.setInterval(function () {
                    waitCloseSession();
                }, initTimeOut.TimeOut);
            }
        }

        function closeSessionAlert(msj, interval) {
            salert("¡Advertencia!", "La sesión se cerrará. " + (msj || ""), "warning");
            window.setInterval(function () {
                closeSession();
            }, interval || 5000);
        }

        function getInitialTimeOut() {
            var response = {};
            $.ajax({
                type: "post",
                async: false,
                cache: false,
                url: "/Home/InSession",
                success: function (_response) {
                    response = _response;
                }, error: function () {
                    closeSessionAlert("Lo sentimos, no se ha podido verificar su sesión.", 1000);
                    response = new SessionTime(true, 20000, 60);
                }
            });
            
            return response;
        }

        function waitCloseSession() {
            window.clearInterval(sessionInterval);
            $("span#cuentaRegresiva").html(secWaitCloseSesssion + " seg.");
            $("#sesionExpiradaModal").modal("show");
            $("#sesionExpiradaModal").attr("style", "z-index: 9999");

            countModalInterval = window.setInterval(function () { countModal(); }, 1000);
        }

        function countModal() {
            secWaitCloseSesssion--;
            if (secWaitCloseSesssion >= 0)
                $("span#cuentaRegresiva").html(secWaitCloseSesssion + " seg.");

            if (secWaitCloseSesssion === 0) {
                closeSession();
            }
        }

        function closeSession() {
            window.location = "/Home/CloseSession";
        }

        function keepSession() {
            initTimeOut = getInitialTimeOut();
            displaySesion();
            timeOut = initTimeOut.TimeOut;
            secWaitCloseSesssion = initTimeOut.SecondsWaitCloseSesssion;
            setIntervalSession();
        }
    });
});