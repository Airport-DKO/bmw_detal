function main(template) {
    dust.renderSource(template, {}, function (err, out) {
        $("#content").empty().append(out);

        var $freeCallBtn = $("#freeCallBtn"),
            $backBtn = $("#backBtn"),
            $callModal = $("#callModal"),
            $phoneNumber = $("#phoneNumber"),
            $callBtn = $("#callBtn");

        //Показываем модальник
        $freeCallBtn.unbind("click");
        $freeCallBtn.on("click", function() {
            $callModal.show();

            //Фокусируемся на ввод телефона
            var $phoneNumber = $("#phoneNumber");
            $phoneNumber.focus();

            //Обработчики кастомных чекбоксов
            var $checkbox = $(".checkbox");
            $checkbox.unbind("click");
            $checkbox.on("click", function() {
                $(this).toggleClass("active");
                if ($(this).hasClass("active")) {
                    $(this).attr('data-checked', 'true');
                } else {
                    $(this).attr('data-checked', 'false');
                }
                checkButtonAllowance();
            });
        });

        $phoneNumber.unbind("input");
        $phoneNumber.on("input", function() {
            checkButtonAllowance();
        });

        //Прячем модальник
        $backBtn.unbind("click");
        $backBtn.on("click", function() {
            $callModal.hide();
        });

        //Проверяем доступность кнопки
        function checkButtonAllowance() {
            var checkBoxCount = $(".checkbox.active").length;

            if (checkBoxCount > 0 && $phoneNumber.val().length > 6) {
                if (!$callBtn.hasClass("active")) {
                    $callBtn.addClass("active");
                }
            } else {
                $callBtn.removeClass("active");
            }
        }

        $callBtn.unbind("click");
        $callBtn.on("click", function() {
            if ($(this).hasClass("active")) {

                var data = {
                    MobileNumber : $phoneNumber.val(),
                    Type : 1,
                    IsNew : true
                }

                $.ajax({
                    type: "POST",
                    url: "http://127.0.0.1:5000/api/CallMeTicket",
                    dataType: 'jsonp',
                    data: JSON.stringify(data),

                }).done(function(a) {
                    console.log(a)
                }).fail(function(a) {
                    console.log(a)
                })
            }
        });
    });
}

