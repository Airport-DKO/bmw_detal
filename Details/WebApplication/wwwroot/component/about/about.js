function about(template) {
    dust.renderSource(template, {}, function (err, out) {
        $("#content").empty().append(out);
    });
}
