// TravaPix — interações de UI (máscaras de entrada + menu responsivo)
(function () {
    "use strict";

    // ----- Máscaras -----
    function onlyDigits(value) {
        return value.replace(/\D/g, "");
    }

    function maskCpf(value) {
        var d = onlyDigits(value).slice(0, 11);
        if (d.length <= 3) return d;
        if (d.length <= 6) return d.slice(0, 3) + "." + d.slice(3);
        if (d.length <= 9) return d.slice(0, 3) + "." + d.slice(3, 6) + "." + d.slice(6);
        return d.slice(0, 3) + "." + d.slice(3, 6) + "." + d.slice(6, 9) + "-" + d.slice(9);
    }

    function maskCard(value) {
        var d = onlyDigits(value).slice(0, 16);
        return d.replace(/(.{4})/g, "$1 ").trim();
    }

    function maskExpiry(value) {
        var d = onlyDigits(value).slice(0, 4);
        if (d.length <= 2) return d;
        return d.slice(0, 2) + "/" + d.slice(2);
    }

    var maskers = { cpf: maskCpf, card: maskCard, expiry: maskExpiry };

    document.querySelectorAll("[data-mask]").forEach(function (input) {
        var fn = maskers[input.getAttribute("data-mask")];
        if (!fn) return;

        input.value = fn(input.value);
        input.addEventListener("input", function () {
            var start = input.selectionStart === input.value.length;
            input.value = fn(input.value);
            if (start) {
                input.selectionStart = input.selectionEnd = input.value.length;
            }
        });
    });

    // ----- Menu responsivo -----
    var shell = document.getElementById("appShell");
    var toggle = document.getElementById("menuToggle");
    var backdrop = document.getElementById("backdrop");

    if (shell && toggle) {
        toggle.addEventListener("click", function () {
            shell.classList.toggle("menu-open");
        });
    }
    if (shell && backdrop) {
        backdrop.addEventListener("click", function () {
            shell.classList.remove("menu-open");
        });
    }
})();
