/*
Routing jQuery Plugin for one-page applications

by Alexey 'Ethical Engineer' Smyshliaev 04.08.2017

version 1.0.1
*/
(function ($) {
    $.Router = function () {
        var router = {};
        if (location.hash !== "") {
            var pathAndArgs = location.hash.split('?'),
                path = pathAndArgs[0].split('/');

            path[0] = path[0].slice(1);
            for (var i = 0; i<path.length; i++){
                if(path[i] === ""){
                    path.splice(i, 1);
                    i-=1;
                }
            }
            router["path"] = path;
            if(pathAndArgs[1]){
                router["args"] = {};
                var args = pathAndArgs[1].split('&');
                args.forEach(function (arg) {
                    var singleArg = arg.split('=');
                    router["args"][singleArg[0]]=singleArg[1];
                });
            }
        } else {
            if (location.search !== "") {
                router["args"] = {};
                var args = location.search.slice(1).split('&');
                args.forEach(function (arg) {
                    var singleArg = arg.split('=');
                    router["args"][singleArg[0]]=singleArg[1];
                });
            }
        }
        return router;
  };
})(jQuery);
