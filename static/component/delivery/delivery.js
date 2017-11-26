function delivery(template) {
    dust.renderSource(template, {}, function (err, out) {
        $("#content").empty().append(out);
    });
}
