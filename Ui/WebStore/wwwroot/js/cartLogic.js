Cart = {
    _properties: {
        addToCartLink: '',
        getCartViewLink:''
    },
    init: function(properties) {
        $.extend(Cart._properties, properties);

        $('a.callAddToCart').on('click', Cart.addToCart);
    },
    addToCart: function (event) {
        var button = $(this);
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');

        $.get(Cart._properties.addToCartLink + '/' + id)
            .done(function() {
                Cart.showTooltip(button);
                Cart.refreshCartView();

            })
            .fail(function() {
                console.log('addToCart error');
            });
      
    },
    showTooltip: function(button) {
        // Отображаем тултип
        button.tooltip({
            title: "Добавлено в корзину"
        }).tooltip('show');
        // Дестроим его через 0.5 секунды
        setTimeout(function () {
            button.tooltip('destroy');
        }, 500);
    },
    refreshCartView: function() {
        // Получаем контейнер корзины
        var container = $("#cartContainer");
        // Получение представления корзины
        $.get(Cart._properties.getCartViewLink)
            .done(function (result) {
            // Обновление html
            container.html(result);
            })
            .fail(function () { console.log('refreshCartView error'); });

    }
}