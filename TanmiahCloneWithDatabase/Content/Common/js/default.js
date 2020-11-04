(function (window, document, $, undefined) {
    var app = {
        windowHeight: $(window).height(),
        windowWidth: $(window).width(),
        isMobile: false,
        isTouch: false,
        resizeTimeoutID: null,
        $body: $("body"),
        rtl: false,
        isIe: false,
        $hb: $("html, body"),
        filterSlider: {
            slider: null,
        },
        jobVacanciesData: null,
        scrollToPosDuration: 500,
        detectDevice: function () {
            (function (a) {
                if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) {
                    app.isMobile = true;
                }
            })(navigator.userAgent || navigator.vendor || window.opera);
            if (navigator.userAgent.match(/Android/i) || navigator.userAgent.match(/webOS/i) || navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPad/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/BlackBerry/i) || navigator.userAgent.match(/Windows Phone/i)) {
                app.isTouch = true;
                app.$body.addClass("touch");
            } else {
                app.$body.addClass("no-touch");
            }
        },
        detectCulture: function () {
            if ($("body").hasClass('ar')) {
                app.rtl = true;
            }
        },
        _windowResize: function () {
            app.windowHeight = $(window).height();
            app.windowWidth = $(window).width();
            app.iframeHeightAssign();
            if (app.filterSlider.slider) {
                app.listingFilterOffsets();
                app.filterSlider.forceRerender = true;
                app.listingFilterSticky();
            }
        },
        resizeListner: function () {
            if (!app.isMobile) {
                $(window).resize(function () {
                    clearTimeout(app.resizeTimeoutID);
                    app.resizeTimeoutID = setTimeout(app._windowResize, 500);
                });
            } else {
                window.addEventListener('orientationchange', function () {
                    app._windowResize();
                });
            }
        },
        addEventListner: function () {

            // For the material like text effect on fields
            $('.text-field').each(function () {
                if ($(this).val().length) {
                    $(this).addClass('stop-animate has-value');
                    setTimeout(function () {
                        $('.stop-animate').removeClass('stop-animate');
                    }, 250);
                }
            });
            $('.text-field').on('blur', function () {
                if ($(this).val().length) {
                    $(this).addClass('has-value');
                } else {
                    $(this).removeClass('has-value');
                }
            });
            // For File attachment on field field
            var attachmentExists, fileName;
            $('.form-control-file').on('change', function (e) {
                fileName = $(this).val().trim();
                attachmentExists = fileName.length ? true : false;
                if (attachmentExists) {
                    fileName = fileName.substr(fileName.lastIndexOf('\\') + 1);
                    $(this).closest('.form-group-file').addClass('has-value').find('.value').text(fileName);
                } else {
                    $(this).closest('.form-group-file').removeClass('has-value').find('.value').text('');
                }
            });
            $(document).on('click', '.js-contact-slider .swiper-slide .full-click', function () {
                $(this).parent().parent().addClass('active').siblings().removeClass('active');
            });
            //transtion fix on load
            window.onload = function () {
                $("body").removeClass("preload");
            }();
            // Inpage Link scrolling
            $(document).on('click', 'a[href*="#"]', function (e) {
                if ((this.pathname == location.pathname || ('/' + this.pathname == location.pathname)) && this.hash.length) {
                    if ($(this).closest('header.nav-active').length) {
                        $('.hamburger-btn').trigger('click');
                    }
                    app.scrollToWidget(this.hash);
                }
            })
        },
        setCss: function () {
            $('.highlighted-content form').each(function () {
                $(this).find('.form-group').last().addClass('form-last-field');
            });
        },
        msIeVersion: function () {
            var ua = window.navigator.userAgent,
                msie = ua.indexOf("MSIE");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                $("body").addClass("ie");
                app.isIe = true;
            }
            return false;
        },
        splitRows: function (elem, elemCounter) {
            var item = elem,
                counter = 0,
                length = item.length / elemCounter,
                rowHtml = "<div class='filtered-row'></div>";
            for (var i = 0; i < length; i++) {
                item.slice(counter, counter + elemCounter).wrapAll(rowHtml);
                counter += elemCounter;
            }
        },
        backToTop: function () {
            app.scrollToPos(0)
        },
        swiperInit: function () {
            var productCounter = $('.js-product-slider').data('rows-itemcount') || 4;
            $('.js-product-slider').swiperReducer({
                slider: { // Swiper values
                    loop: true,
                    slidesPerView: productCounter,
                    spaceBetween: 30,
                    breakpoints: {
                        // when window width is <= 320px
                        360: {
                            slidesPerView: 1,
                            spaceBetween: 10
                        },
                        // when window width is <= 480px
                        767: {
                            slidesPerView: 2,
                            spaceBetween: 20
                        },
                        // when window width is <= 640px
                        991: {
                            slidesPerView: 3,
                            spaceBetween: 25
                        }
                    }
                },
                hasNavigation: true, // by default true
                hasPagination: true, // by default false
                checkLength: 4, // more then given value
                beforeLoadClassName: 'before-load'
                // Add this class on slider's parent markup with above class name
                // and after init this class will be removed
            });
            $('.js-recipe-slider').swiperReducer({
                slider: { // Swiper values
                    loop: true,
                    spaceBetween: 30,
                    autoHeight: true
                },
                hasNavigation: true, // by default true
                hasPagination: true, // by default false
                checkLength: 1, // more then given value
                beforeLoadClassName: 'before-load'
                // Add this class on slider's parent markup with above class name
                // and after init this class will be removed
            });

            /* listing slider */
            $('.js-listing-slider').swiperReducer({
                slider: { // Swiper values
                    speed: 600,
                    slidesPerView: 2,
                    slidesPerGroup: 2,
                    spaceBetween: 60,
                    watchSlidesVisibility: true,
                    breakpoints: {
                        1024: {
                            spaceBetween: 30,
                        },
                        767: {
                            slidesPerView: 1,
                            slidesPerGroup: 1
                        },
                    }
                },
                hasNavigation: true, // by default true
                hasPagination: true, // by default false
                checkLength: 1, // more then given value
                customInitFunction: function (item) {
                    var slidesCount = $(item).data('rows-itemcount') || 2;
                    return {
                        slidesPerView: slidesCount,
                        slidesPerGroup: slidesCount
                    }
                }
            });
            $('.js-contact-slider').swiperReducer({
                slider: { // Swiper values
                    loop: false,
                    slidesPerView: 4,
                    spaceBetween: 30,
                    breakpoints: {
                        // when window width is <= 480px
                        767: {
                            slidesPerView: 1.5,
                            spaceBetween: 10
                        },
                        // when window width is <= 640px
                        991: {
                            slidesPerView: 2,
                            spaceBetween: 20
                        },
                        // when window width is <= 1200px
                        1199: {
                            slidesPerView: 3,
                            spaceBetween: 25
                        }
                    }
                },
                hasPagination: true // by default false
                // Add this class on slider's parent markup with above class name
                // and after init this class will be removed
            });

            /* Slider two cols */

            if (window.matchMedia('(max-width: 767px)').matches) {
                // $('.')
                $('.js-two-col-slider').swiperReducer({
                    slider: {
                        speed: 600,
                        autoHeight: true,
                        slidesPerView: 1,
                        centeredSlides: true,
                        simulateTouch: true,
                    },
                    hasNavigation: false,
                    hasPagination: true,
                    checkLength: 1
                });
            }

        },
        listingFilterSlider: function () {
            /* Listing filter slider */
            if ($('.listing-filter .swiper-container .list-item').length) {
                app.filterSlider.setSpaceBetween = function (isSticky) {
                    var spaceBetween = 0;
                    if (isSticky) {
                        if (window.matchMedia('(max-width: 767px)').matches) {
                            spaceBetween = 34;
                        } else if (window.matchMedia('(max-width: 1024px)').matches) {
                            spaceBetween = 40;
                        } else if (window.matchMedia('(max-width: 1265px)').matches) {
                            spaceBetween = 30;
                        } else {
                            spaceBetween = 52;
                        }
                    } else {
                        if (window.matchMedia('(max-width: 767px)').matches) {
                            spaceBetween = 34;
                        } else if (window.matchMedia('(max-width: 1024px)').matches) {
                            spaceBetween = 40;
                        } else if (window.matchMedia('(max-width: 1265px)').matches) {
                            spaceBetween = 50;
                        } else {
                            spaceBetween = 86;
                        }
                    }
                    app.filterSlider.slider.params.spaceBetween = spaceBetween;
                };
                app.filterSlider.slider = new Swiper('.listing-filter .swiper-container', {
                    speed: 400,
                    slideClass: 'list-item',
                    slidesPerView: 'auto',
                    spaceBetween: 86,
                    // spaceBetween: 0,
                    centerInsufficientSlides: true,
                    breakpoints: {
                        767: {
                            spaceBetween: 34
                        },
                        1024: {
                            spaceBetween: 40
                        },
                        1265: {
                            spaceBetween: 50
                        }
                    },
                    on: {
                        resize: function () {
                            if (!app.isTouch) {
                                this.slideTo(0, 0);
                            }
                        }
                    }
                });
                app.listingFilterOffsets();
                app.listingFilterSticky();
                $(window).scroll(function () {
                    app.listingFilterSticky();
                });
            }
        },
        listingFilterOffsets: function () {
            app.filterSlider.stickyPos = $('.listing-filter-container').outerHeight() || 256;
            app.filterSlider.stickyDivHeight = $('.listing-filter').outerHeight();
            app.filterSlider.forceRerender = false;
        },
        listingFilterSticky: function () {
            if (window.matchMedia('(max-width: 767px)').matches) {
                app.filterSlider.slider.slideTo(0, 0);
                return false
            }
            var scroll = $(window).scrollTop();
            var isSticky = $(".listing-filter-container.listing-filter-sticky").length;
            if (scroll >= app.filterSlider.stickyPos) {
                if (!isSticky || app.filterSlider.forceRerender) {
                    $('.listing-filter-container').css('min-height', app.filterSlider.stickyDivHeight);
                    $(".listing-filter-container").addClass("listing-filter-sticky sticky-transitions-removal");
                    app.filterSlider.setSpaceBetween(true);
                    app.filterSlider.slider.update();
                    if (!app.isTouch) {
                        app.filterSlider.slider.slideTo(0, 0);
                    }
                    $(".listing-filter-container").removeClass("sticky-transitions-removal");
                    app.filterSlider.forceRerender = false;
                }
            } else {
                if (isSticky || app.filterSlider.forceRerender) {
                    $(".listing-filter-container").addClass("sticky-transitions-removal");
                    $(".listing-filter-container").removeClass("listing-filter-sticky");
                    $('.listing-filter-container').css('min-height', 'auto');
                    app.filterSlider.setSpaceBetween(false);
                    app.filterSlider.slider.update();
                    if (!app.isTouch) {
                        app.filterSlider.slider.slideTo(0, 0);
                    }
                    $(".listing-filter-container").removeClass("sticky-transitions-removal");
                    app.filterSlider.forceRerender = false;
                }
            }
        },
        objectFit: function () {
            if (document.documentMode || /Edge/.test(navigator.userAgent)) {
                jQuery('.object-fit').each(function () {
                    var t = jQuery(this),
                        s = 'url(' + t.attr('src') + ')',
                        p = t.parent(),
                        d = jQuery('<div></div>', {class: 'object-fit-div'});
                    p.append(d);
                    d.css({
                        'height': t.parent().css('height'),
                        'background-size': 'cover',
                        'background-repeat': 'no-repeat',
                        'background-position': '50% 50%',
                        'background-image': s
                    });
                    t.hide();
                });
            }
        },
        videoOverlay: function () {
            var overlayContainer = $('.overlay-container'),
                videoOverlay = overlayContainer.find('.video-overlay'),
                closeBtnSelector = overlayContainer.find('.close-icon'),
                videoWrapper,
                html = $('html');
            $(document).on('click', '.cta-overlay-video', function () {
                var $this = $(this),
                    videoData = $this.data('video-id');
                youtubeData(videoData);
            });

            function youtubeData(videoId) {
                videoWrapper = $('.overlay-video-wrapper');
                videoOverlay.addClass('video-iframe');
                videoWrapper.find('iframe').remove();
                videoWrapper.append('<iframe src="https://www.youtube.com/embed/' + videoId + '?rel=0 " allowfullscreen="allowfullscreen"></iframe>');
                setTimeout(function () {
                    $('.overlay-video-wrapper iframe').addClass('iframe-loaded');
                }, 800);
                html.addClass('open-overlay');
                overlayContainer.fadeIn();
            }

            closeBtnSelector.add(html).click(function () {
                var videoWrapper = $('.overlay-video-wrapper');
                if (overlayContainer.is(":visible")) {
                    html.removeClass('open-overlay');
                    overlayContainer.fadeOut();
                    setTimeout(function () {
                        videoWrapper.children().remove();
                    }, 300);
                }
            });
            videoOverlay.add('.content-overlay').click(function (e) {
                e.stopPropagation();
            });
        },
        navSticky: function () {
            var scrollStickyPos = 50;

            function navSticky() {
                var scroll = $(window).scrollTop();
                if (window.matchMedia('(min-width: 992px)').matches) {
                    if (scroll >= scrollStickyPos) {
                        $("header").addClass("sticky");
                    } else {
                        $("header").removeClass("sticky");
                    }
                } else {
                }
            }

            navSticky();
            $(window).scroll(function () {
                navSticky();
            });
            $('header .nav li .secondary-menu').parent().addClass('has-secondary-menu');
            var allPanels = $('header .nav li .secondary-menu');
            if ($(window).width() < 992 || ($(window).width() <= 1366 && app.isTouch)) {
                allPanels.hide();
                //Mobile btn
                $(".hamburger-btn").click(function () {
                    $('body').toggleClass("scroll-none");
                    $(this).toggleClass("active");
                    $('header').toggleClass("nav-active");
                });
                $('header .nav > li.has-secondary-menu > a').click(function () {
                    var $this = $(this);
                    var $target = $this.next();
                    if ($this.hasClass("active")) {
                        allPanels.removeClass('show').slideUp();
                        $target.removeClass('show');
                        $this.removeClass("active");
                    } else {
                        $("header .nav li > a").removeClass("active");
                        allPanels.removeClass('show').slideUp();
                        $target.addClass('show').slideDown();
                        $this.addClass("active");
                    }
                    return false;
                });
            }
        },
        widgetController: function () {
            $('.section-recipe').length &&
            $('.section-recipe-deck').length
            && $('body').addClass('recipe-widgets-gap');
        },
        careerWidget: function () {
            var widgetSelector = $('#career-data'),
                listingSelector = widgetSelector.find('#feature-listing'),
                overlaySelector = $('#feature-listing-overlay'),
                overlayWrapperSelector = overlaySelector.find('.overlay-content-wrapper'),
                filterListSelector = widgetSelector.find('#filter-list'),
                closeBtnSelector = overlaySelector.find('.close-icon'),
                applicationFormSelector = $('#application-form'),
                jobSelectSelector = applicationFormSelector.find('#field-job-title'),
                thankYouStateSelector = $('#thankYouMessageContainer'),
                jobApplicationWrapperSelector = $('#jobApplicationWrapper'),
                html = $('html'),
                vacanciesTitle = [],
                data = app.jobVacanciesData;
            if (app.jobVacanciesData === null) return false;
            if (data !== {}) {
                widgetSelector.removeClass('loader-small')
                content(data);
                filters(data);
                listing(data);
                filterEventBinder();
                setTimeout(function () {
                    overlay();
                }, 300);
            }

            function content(data) {
                widgetSelector.find('.heading-bold-primary').eq(0).text(data.Title);
            }

            function filters(data) {
                var items = [];
                $.each(data.Filters, function (key, val) {
                    var filterValue = val.CompanyName.length > 12 ? val.CompanyAbbreviation : val.CompanyName;
                    items.push(
                        '<span id="filter-list-' + key + '" data-id="' + val.Id + '" class="filter-list__item">' + filterValue + '</span>'
                    );
                });
                filterListSelector.append(items.join(''));
                filterListSelector.find('.filter-list__item').first().addClass('active');
            }

            function listing(data) {
                var items = [];
                vacanciesTitle.push(data.DefaultJobTitle);
                $.each(data.Vacancies, function (key, val) {
                    _listingMarkupGenerator(val, items);
                });
                listingSelector.append(
                    items.join('')
                );
                _listingEventBinder(data);
            }

            function overlay() {
                listingSelector.click(function (e) {
                    e.stopPropagation();
                });
                closeBtnSelector.add(html).click(function () {
                    _overlayCloseBtn();
                });
            }

            function filterEventBinder() {
                var filterPanel = $('#filter-list'),
                    filterItem = filterPanel.find('.filter-list__item'),
                    listingSelectorItem = listingSelector.find('.list-item');
                filterPanel.on('click', '.filter-list__item', function () {
                    _checkMessageStateShowNHide();
                    filterItem.removeClass('active');
                    $(this).addClass('active');
                    var id = parseInt($(this).data('id')),
                        currentItem;
                    if (id === 0) {
                        listingSelectorItem.show()
                    } else {
                        listingSelectorItem
                            .filter(function (index, item) {
                                currentItem = $(item);
                                if (currentItem.data('industry-id') === id) {
                                    currentItem.show();
                                } else {
                                    currentItem.hide();
                                }
                            });
                    }
                })
            }

            function setDataIntoFormDropDown() {
                var vacanciesTitleSorted = vacanciesTitle.filter(function (elem, index, self) {
                    return index === self.indexOf(elem);
                });
                for (var i = 0; i < vacanciesTitleSorted.length; i++) {
                    if (i === 0) {
                        jobSelectSelector.append('<option value="" disabled selected class="hidden">' + vacanciesTitleSorted[i] + '</option>');
                    } else {
                        jobSelectSelector.append('<option value="' + _stringFormatter(vacanciesTitleSorted[i]) + '">' + vacanciesTitleSorted[i] + '</option>');
                    }
                }
            }

            function _overlayListing(data) {
                var items = [];
                $.each(data.IndustryDetail, function (key, val) {
                    var checkValue = '';
                    if (val.Key === "Location") {
                        var isArray = val.Value.split(' ');
                        if (isArray.length > 1) {
                            $.each(isArray, function (key, value) {
                                checkValue += value + ' <br>';
                            });
                        } else {
                            checkValue = val.Value;
                        }
                    } else {
                        checkValue = val.Value;
                    }
                    _overlayListingMarkupGenerator(val, items, checkValue);
                });
                $("<ul/>", {
                    "class": "feature-listing feature-listing--fluid " +
                    "feature-listing--wrap feature-listing--items-two-col " +
                    "feature-listing--three-col feature-listing--borders feature-listing--counters",
                    html: items.join("")
                }).appendTo(overlayWrapperSelector);
            }

            function _overlayContent(vacancies, data) {
                $("<div/>", {
                    "class": "overlay-para-1",
                    html: vacancies.JobDescription
                }).appendTo(overlayWrapperSelector);
                $("<div/>", {
                    "class": "overlay-para-2",
                    html: vacancies.JobResponsibilities
                }).appendTo(overlayWrapperSelector);
                $("<div/>", {
                    "class": "overlay-cta-bar",
                }).append($("<span/>", {
                    "class": "btn-primary",
                    "id": "overlay-cta",
                    html: data.CtaText
                })).appendTo(overlayWrapperSelector);
            }

            function _stringFormatter(str) {
                return str.replace(/\s+/g, '-').toLowerCase();
            }

            function _listingMarkupGenerator(val, arrayItem) {
                arrayItem.push(
                    '<li data-id="' + val.Id + '" data-industry-id="' + val.IndustryId + '" class="list-item">'
                    + '<h3 class="list-item__lg-heading">' +
                    val.NoOfVacancies
                    + '</h3>'
                    + '<div class="list-content">'
                    + '<h5 class="list-content__small-heading">' +
                    val.Title
                    + '</h5>'
                    + '<h4 class="list-content__heading">' +
                    val.IndustryDetail[0].Value
                    + '</h4>'
                    + '</div>'
                    + '</li>');
                vacanciesTitle.push(val.Title);
            }

            function _overlayListingMarkupGenerator(val, arrayItem, checkValue) {
                arrayItem.push(
                    '<li class="list-item">'
                    + '<div class="list-content">'
                    + '<h5 class="list-content__small-heading">' +
                    val.Key
                    + '</h5>'
                    + '<h4 class="list-content__heading">' +
                    checkValue
                    + '</h4>'
                    + '</div>'
                    + '</li>');
            }

            function _listingEventBinder(data) {
                listingSelector.on('click', '.list-item', function () {
                    _checkMessageStateShowNHide();
                    var id = $(this).data('id'),
                        dataArray = data.Vacancies,
                        currentVacancyObj,
                        vacancyData;
                    currentVacancyObj = $.grep(dataArray, function (e) {
                        return e.Id == id;
                    });
                    vacancyData = currentVacancyObj[0];
                    overlayWrapperSelector.append(
                        '<h2>' + vacancyData.Title + '</h2>'
                        + '<p>' + vacancyData.ValidUptoText + '</p>'
                    );
                    _overlayListing(vacancyData);
                    _overlayContent(vacancyData, data);
                    overlaySelector.fadeIn();
                    $(html).addClass('open-overlay');
                    // Selector | Offset Top Value | Animation Delay Value | Set Timeout Value
                    _scrollTopAnimation(overlayWrapperSelector, 0, 350, 350);
                    _overlayCTA(id);
                });
            }

            function _overlayCloseBtn() {
                $(html).removeClass('open-overlay');
                if (!overlayWrapperSelector.children().length) return false;
                setTimeout(function () {
                    overlayWrapperSelector.html('')
                }, 600);
            }

            function _overlayCTA(id) {
                var formOffset = applicationFormSelector.offset().top;
                $('#overlay-cta').click(function () {
                    _setDropDownValue(jobSelectSelector, id);
                    overlaySelector.fadeOut();
                    _overlayCloseBtn();
                    // Selector | Offset Top Value | Animation Delay Value | Set Timeout Value
                    _scrollTopAnimation($("html, body"), formOffset - 200, 800, 300);
                });
            }

            function _setDropDownValue(data, id) {
                $.each(data.find('option'), function (key, val) {
                    if ($(val).data('id') !== -1) {
                        $(this).removeAttr("selected");
                        if ($(val).data('id') === id) {
                            $(val).attr("selected", "selected");
                            var company = $(val).data('company');
                            $('#jobApplicationContainer form #CompanyName').val(company);
                            var companyEmail = $(val).data('companyemail');
                            $('#jobApplicationContainer form #companyEmail').val(companyEmail);
                        }
                    } else {
                        $(val).removeAttr("selected");
                    }
                });
            }

            function _scrollTopAnimation(selector, offsetTopValue, animvationDelayValue, stoValue) {
                setTimeout(function () {
                    selector.animate({
                        scrollTop: offsetTopValue
                    }, animvationDelayValue);
                }, stoValue);
            }

            function _checkMessageStateShowNHide() {
                if (thankYouStateSelector.is(":visible"))
                    jobApplicationWrapperSelector.show();
                thankYouStateSelector.hide();
                applicationFormSelector.find("input[type=text], input[type=email]").val("");
            }
        },
        scrollToPos: function (pos) {
            if (pos == $(window).scrollTop())
                return false;
            app.$hb.stop();
            app.$hb.animate({
                scrollTop: pos
            }, app.scrollToPosDuration);
            return false;
        },
        getStickyElementsHeights: function () {
            var totalHeight = 0,
                headerHeight = 0,
                listingFilterHeight = 0,
                bufferHeight = 20;
            if (app.$body.hasClass('no-touch') && window.matchMedia('(min-width: 992px)').matches) {
                headerHeight = 63;
            } else {
                headerHeight = Math.round($("header").outerHeight());
            }
            if ($('.listing-filter-container').length) {
                if (window.matchMedia('(max-width: 991px)').matches) {
                    listingFilterHeight = $('.listing-filter .swiper-container').outerHeight();
                } else {
                    listingFilterHeight = 65;
                }
            }
            totalHeight = Math.round(headerHeight + listingFilterHeight + bufferHeight);
            return totalHeight;
        },
        scrollToHashOnPageLoad: function () {
            var hash = window.location.hash;
            if (hash.length) {
                app.scrollToWidget(hash);
            }
        },
        scrollToWidget: function (hash) {
            var $targetElement,
                topOffset;
            if (hash.length && (hash.indexOf("#") !== -1)) {
                hash = hash.substring(hash.lastIndexOf("#") + 1);
                $targetElement = $("[data-widget-id='" + hash + "']");
                if ($targetElement.length) {
                    topOffset = Math.round($targetElement.offset().top) - app.getStickyElementsHeights();
                    app.scrollToPos(topOffset)
                }
            }
        },
        iframeHeightAssign: function () {
            var height = undefined;
            $(".iframe-parent").each(function (index, item) {
                if (window.matchMedia('(max-width: 767px)').matches) {
                    height = $(item).data('mobile-height');
                } else {
                    height = $(item).data('desktop-height');
                }
                height = parseInt(height);
                if (!isNaN(height)) {
                    $(item).find('iframe').css('height', height)
                }
            });
        },
        parallax: function () {

            if (!$('.slider-two-cols').length || window.matchMedia('(max-width: 767px)').matches) {
                return false
            }
            // Using nice scroll when theres no touch device or mobile device


            var controller1 = new ScrollMagic.Controller({
                globalSceneOptions: {
                    triggerHook: 0.9,
                    duration: "110%",
                    triggerElement: "#parallax-container-1"
                }
            });

            var controller2 = new ScrollMagic.Controller({
                globalSceneOptions: {
                    triggerHook: 0.9,
                    duration: "110%",
                    triggerElement: "#parallax-container-2"
                }
            });


            var contentController1 = new ScrollMagic.Controller({
                globalSceneOptions: {
                    triggerHook: 1,
                    duration: "80%",
                    triggerElement: "#parallax-content-container-1"
                }
            });

            var contentController2 = new ScrollMagic.Controller({
                globalSceneOptions: {
                    triggerHook: 1,
                    duration: "80%",
                    triggerElement: "#parallax-content-container-2"
                }
            });


            var contentController3 = new ScrollMagic.Controller({
                globalSceneOptions: {
                    triggerHook: 1,
                    duration: "80%",
                    triggerElement: "#parallax-content-container-3"
                }
            });

            function controllerSelector(controller) {
                if (controller === 1) {
                    return controller1
                } else {
                    return controller2
                }
            }

            //
            // $('.slider-two-cols .swiper-slide:nth-child(1) .slider-content h3, .slider-two-cols .swiper-slide:nth-child(1) .slider-content p')

            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(1) .slider-content h3", {y: "0px", delay: 1})
                .addTo(contentController1);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(1) .slider-content .content-img", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController1);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(1) .slider-content p:last-of-type ~ .btn", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController1);


            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(2) .slider-content h3", {y: "0px", delay: 1})
                .addTo(contentController2);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(2) .slider-content .content-img", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController2);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(2) .slider-content p:last-of-type ~ .btn", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController2);


            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(3) .slider-content h3", {y: "0px", delay: 1})
                .addTo(contentController3);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(3) .slider-content .content-img", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController3);
            new ScrollMagic.Scene()
                .setTween(".slider-two-cols .swiper-slide:nth-child(3) .slider-content p:last-of-type ~ .btn", {
                    y: "0px",
                    delay: 1
                })
                .addTo(contentController3);


            /*  new ScrollMagic.Scene()
                  .setTween(".slider-two-cols .swiper-slide:nth-child(1) .slider-content h3, .slider-two-cols .swiper-slide:nth-child(1) .slider-content p", {marginTop:"0px", delay: 1})
                  .addTo(containerController1);

              new ScrollMagic.Scene()
                  .setTween(".slider-two-cols .swiper-slide:nth-child(2) .slider-content h3, .slider-two-cols .swiper-slide:nth-child(2) .slider-content p", {marginTop:"0px", delay: 1})
                  .addTo(containerController2);

              new ScrollMagic.Scene()
                  .setTween(".slider-two-cols .swiper-slide:nth-child(3) .slider-content h3, .slider-two-cols .swiper-slide:nth-child(3) .slider-content p", {marginTop:"0px", delay: 1})
                  .addTo(containerController3);

  */
            var selector = '';
            $('.parallax-element img').each(function (index, item) {
                selector = "#" + $(this).closest('.parallax-element').attr('id') + " img";
                new ScrollMagic.Scene()
                    .setTween(selector, {y: $(this).data('tween-y')})
                    .addTo(controllerSelector($(this).data('controller')));
            });

            var $endingElement = $($('.slider-two-cols').next());
            var scrollChangeLock = false;

            $('.slides-opacity-null').removeClass('slides-opacity-null');

            swiperNavigationSticky();
            slidesActiveStatus();
            $(window).scroll(throttle(swiperNavigationSticky, 30));
            $(window).scroll(throttle(slidesActiveStatus, 130));


            function swiperNavigationSticky() {
                var scrollTop = $(window).scrollTop()  + Math.round($(window).outerHeight() / 2),
                topStickyPoint = $('.slider-two-cols .swiper-wrapper .swiper-slide').first().offset().top + ( Math.round($('.slider-two-cols .swiper-wrapper .swiper-slide').first().outerHeight() / 2 )),
                holdStickyPoint = $('.slider-two-cols .swiper-wrapper .swiper-slide').last().offset().top + ( Math.round($('.slider-two-cols .swiper-wrapper .swiper-slide').last().outerHeight() / 2 ));

                if (scrollTop >= topStickyPoint) {
                    app.$body.addClass('swiper-nav-sticky');
                } else {
                    app.$body.removeClass('swiper-nav-sticky');
                }

                if (scrollTop >= holdStickyPoint) {
                    app.$body.addClass('swiper-nav-sticky-hold');
                } else {
                    app.$body.removeClass('swiper-nav-sticky-hold');
                }
            }

            function slidesActiveStatus() {
                if (!scrollChangeLock) {
                    var scrollTop = $(window).scrollTop() + ( $(window).outerHeight() / 2 );
                    $($('.slider-two-cols .swiper-slide').get().reverse()).each(function (index, item) {
                        if (scrollTop >= $(this).offset().top) {
                            if (!$('.swiper-pagination-desktop .swiper-pagination-bullet').eq($(item).index()).hasClass('swiper-pagination-bullet-active')) {
                                $('.swiper-pagination-desktop .swiper-pagination-bullet-active').removeClass('swiper-pagination-bullet-active');
                                $('.swiper-pagination-desktop .swiper-pagination-bullet').eq($(item).index()).addClass('swiper-pagination-bullet-active');
                            }
                            return false;
                        }
                    });
                }
            }


            var pos = 0,
                scrollDuration = 1100;

            $('.swiper-pagination-desktop').on('click', '.swiper-pagination-bullet', function () {
                scrollChangeLock = true;
                pos = $('.slider-two-cols .swiper-slide').eq($(this).index()).offset().top + ( $('.slider-two-cols .swiper-slide').eq($(this).index()).outerHeight() / 2 ) - ( $(window).outerHeight() / 2 )
                if (pos != $(window).scrollTop()) {
                    app.$hb.stop();
                    app.$hb.animate({
                        scrollTop: pos
                    }, scrollDuration);
                }

                // app.scrollToPos($('.slider-two-cols .swiper-slide').eq($(this).index()).offset().top + ( $('.slider-two-cols .swiper-slide').eq($(this).index()).outerHeight() / 2 ) - ( $(window).outerHeight() / 2 ));
                $('.slider-two-cols .swiper-pagination-bullet-active').removeClass('swiper-pagination-bullet-active');
                $(this).addClass('swiper-pagination-bullet-active');
                setTimeout(function () {
                    scrollChangeLock = false;
                }, scrollDuration);
            });

            if (!app.isTouch) {
                $("body").niceScroll({
                    cursorcolor: "#c1c1c1",
                    cursorwidth: "17px",
                    cursorborder: "0px solid #000",
                    scrollspeed: 80,
                    autohidemode: false,
                    background: '#f1f1f1',
                    cursorfixedheight: false,
                    cursorminheight: 20,
                    enablekeyboard: true,
                    horizrailenabled: false,
                    bouncescroll: false,
                    smoothscroll: true,
                    iframeautoresize: true,
                    touchbehavior: false,
                    zindex: 999
                });
            }


        },
        random: function () {
            if ($('.feature-listing-3 .download-btn').length) {
                $('.feature-listing-3').addClass('has-button');
            }
        },
        counter: function () {
            var textValue;
            $('[data-counter]').each(function () {
                $(this).animate(
                    {
                        Counter: $(this).data('counter')
                    },
                    {
                        duration: 1000,
                        easing: 'swing',
                        step: function (now) {
                            $(this).text(Math.ceil(now));
                        },
                        complete: function () {
                            if ($(this).text().length > 2) {
                                app.dataLimit($(this));
                                textValue = $(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                                $(this).text(textValue);
                            }

                        }
                    });
            });
        },
        dataLimit: function (selector) {
            $('[data-limit]').each(function () {
                if (selector.data('limit') === '' || selector.data('limit') === 0) return false;
                var limitValue = selector.data('limit'),
                    currentValue = selector.data('counter');
                if (parseInt(currentValue) >= parseInt(limitValue)) {
                    selector.text(limitValue).addClass('plus-sign');
                }
            });
        },
        containerFocus: function () {
            var elem = $('.feature-listing-2');
            if (elem.length) {
                var elemPos = elem.offset().top,
                    value,
                    scrollValue,
                    callFunc = true;
                $(window).scroll(function () {
                    scrollValue = $(window).scrollTop();
                    if ((elemPos + 300) <= scrollValue && callFunc) {
                        callFunc = false;
                        app.counter();
                    }
                    value = elem.scrollTop;
                });
            }
        },
        init: function () {
            app.detectCulture();
            app.detectDevice();
            app.resizeListner();
            app.addEventListner();
            app.setCss();
            app.msIeVersion();
            app.iframeHeightAssign();
            app.videoOverlay();
            app.navSticky();
            app.swiperInit();
            app.listingFilterSlider();
            app.widgetController();
            app.objectFit();
            app.random();
            app.counter();
            /*setTimeout(function() {
                app.dataLimit();
            }, 600);*/
            // app.containerFocus();
        }
    }
    window.app = app;
})
(window, document, jQuery);
$(document).ready(function () {
    app.init();
});
$(window).on("load", function () {
    // Runs when all content, images and dom is ready
    app.scrollToHashOnPageLoad();
});


function throttle(fn, threshhold, scope) {
    threshhold || (threshhold = 250);
    var last,
        deferTimer;
    return function () {
        var context = scope || this;

        var now = +new Date,
            args = arguments;
        if (last && now < last + threshhold) {
            // hold on to it
            clearTimeout(deferTimer);
            deferTimer = setTimeout(function () {
                last = now;
                fn.apply(context, args);
            }, threshhold);
        } else {
            last = now;
            fn.apply(context, args);
        }
    };
}

