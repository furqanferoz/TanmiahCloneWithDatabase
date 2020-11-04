(function ($) {
    $.fn.swiperReducer = function (options) {
        var $this = $(this);
        if (!$this.length) return false;
        var classNameSet = $(this).attr('class'),
            classNameArray = classNameSet.split(' '),
            className = classNameArray.length > 1 ? classNameArray[1] : classNameArray[0],
            defaults = {
                hasNavigation: true,
                hasPagination: false,
                parentClassName: className + '--swiper-parent',
                beforeLoadClassName: null,
                customInitFunction: null
            },
            settings = $.extend({}, defaults, options),
            sliderOptions = settings.slider,
            concatStringName = "-swiper",
            sliderVariable = className.concat(concatStringName),
            mergeObjects, navigationObj, paginationObj,
            slideLength, itemParent,
            customInitFunctionProps = null;

        function initSlider(index, item) {
            $(item).parent().addClass(settings.parentClassName);
            if (settings.customInitFunction) {
                customInitFunctionProps = settings.customInitFunction(item) || {};
            }
            navigationObj = {
                navigation: {
                    nextEl: $(item)
                        .closest('.' + settings.parentClassName)
                        .find('.swiper-button-next').get(0),
                    prevEl: $(item)
                        .closest('.' + settings.parentClassName)
                        .find('.swiper-button-prev').get(0),
                }
            };
            paginationObj = {
                pagination: {
                    el: $(item).closest('.' + settings.parentClassName).find('.swiper-pagination').get(0),
                    clickable: true
                }
            };
            mergeObjects = $.extend({},
                sliderOptions,
                settings.hasNavigation ? navigationObj : null,
                settings.hasPagination ? paginationObj : null,
                customInitFunctionProps
            );
            sliderVariable = new Swiper(item, mergeObjects);
            $(item).parent().removeClass(settings.beforeLoadClassName);
        }

        $(this).each(function (index, item) {
            itemParent = $(item).parent();
            !settings.hasNavigation && itemParent.find(
                '.swiper-button-prev, ' +
                '.swiper-button-next ')
                .hide()
                .end()
                .addClass('no-slider-nav');
            slideLength = $(item).find('.swiper-slide').length;
            if (settings.checkLength) {
                if (slideLength > settings.checkLength) {
                    initSlider(index, item);
                } else {
                    itemParent.find(
                        '.swiper-button-prev, ' +
                        '.swiper-button-next, ' +
                        '.swiper-pagination')
                        .hide()
                        .end()
                        .addClass('no-slider');
                }
            } else {
                initSlider(index, item);
            }
        });
    };
})(jQuery);




