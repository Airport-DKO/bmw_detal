$(function () {
    $(document).ready(function() {
        loadWrapper();
        window.addEventListener("hashchange", loadContent, false);
    });

    //Загрузка обвязки
    function loadWrapper() {
        index_loadTemplate("wrapper", function(template) {
            dust.renderSource(template, {}, function (err, out) {
                $("#app").empty().append(out);

                //Обработчики обвязки
                initWrapper();

                //Загружаем контент
                loadContent();
            });
        });
    }

    //Загружаем контент
    function loadContent() {
        var componentName;
        $.Router().path ? componentName = $.Router().path[0] : componentName = "main";

        index_loadTemplate(componentName, function(template) {
            index_loadScript(componentName, template);
        })
    }

    //Инициализация обработчиков обвязки
    function initWrapper() {
        var $returnPolicy = $("#returnPolicy"),
            $contacts = $("#contactsButton"),
            $logo = $("#logo");
        $returnPolicy.on("click", function() {
            window.location.href = "#returnPolicy";
        });
        $contacts.on("click", function() {
            window.location.href = "#contacts";
        });
        $logo.on("click", function() {
            window.location.href = "/";
        });
    }
});

//Загружаем шаблон
function index_loadTemplate (componentName, loadCallback, errorCallback) {
    $.ajax({
        url: "component/" + componentName + "/" + componentName + ".dust"
    }).done(function (template) {
        loadCallback ? loadCallback(template) : null;
    }).fail(function (XHR) {
        console.error("Ошибка загрузки шаблона: " + componentName + ".dust");
        errorCallback ? errorCallback(XHR) : null;
    })
}

//Запускаем нужный скрипт
function index_loadScript (componentName, template, errorCallback) {
    $.ajax({
        url: "component/" + componentName + "/" + componentName + ".js"
    }).done(function (script) {
        script ? window[componentName](template) : null;
    }).fail(function (XHR) {
        console.error("Ошибка загрузки скрипта: " + componentName + ".js");
        errorCallback ? errorCallback(XHR) : null;
    });
}
