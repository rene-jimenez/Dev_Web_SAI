/*
 * Detact Mobile Browser
 */
if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
    $('html').addClass('ismobile');
}

$(window).load(function () {
    /* --------------------------------------------------------
     Page Loader
     -----------------------------------------------------------*/
    if (!$('html').hasClass('ismobile')) {
        if ($('.page-loader')[0]) {
            setTimeout(function () {
                $('.page-loader').fadeOut();
            }, 500);

        }
    }
})

$(document).ready(function () {
    /* --------------------------------------------------------
    	Layout
    -----------------------------------------------------------*/
    (function () {

        //Get saved layout type from LocalStorage
        var layoutStatus = localStorage.getItem('ma-layout-status');

        if (!$('#header-2')[0]) { //Make it work only on normal headers
            if (layoutStatus == 1) {
                $('body').addClass('sw-toggled');
                $('#tw-switch').prop('checked', true);
            }
        }

        $('body').on('change', '#toggle-width input:checkbox', function () {
            if ($(this).is(':checked')) {
                setTimeout(function () {
                    $('body').addClass('toggled sw-toggled');
                    localStorage.setItem('ma-layout-status', 1);
                }, 250);
            } else {
                setTimeout(function () {
                    $('body').removeClass('toggled sw-toggled');
                    localStorage.setItem('ma-layout-status', 0);
                }, 250);
            }
        });
    })();

    /* --------------------------------------------------------
    	Scrollbar
    -----------------------------------------------------------*/
    function scrollBar(selector, theme, mousewheelaxis) {
        $(selector).mCustomScrollbar({
            theme: theme,
            scrollInertia: 100,
            axis: 'yx',
            mouseWheel: {
                enable: true,
                axis: mousewheelaxis,
                preventDefault: true
            }
        });
    }

    if (!$('html').hasClass('ismobile')) {
        //On Custom Class
        if ($('.c-overflow')[0]) {
            scrollBar('.c-overflow', 'minimal-dark', 'y');
        }
    }

    /*
     * Top Search
     */
    (function () {
        $('body').on('click', '#top-search > a', function (e) {
            e.preventDefault();

            $('#header').addClass('search-toggled');
            $('#top-search-wrap input').focus();
        });

        $('body').on('click', '#top-search-close', function (e) {
            e.preventDefault();

            $('#header').removeClass('search-toggled');
        });
    })();

    /*
     * Sidebar
     */
    (function () {
        //Toggle
        $('body').on('click', '#menu-trigger, #chat-trigger', function (e) {
            e.preventDefault();
            var x = $(this).data('trigger');

            $(x).toggleClass('toggled');
            $(this).toggleClass('open');

            //Close opened sub-menus
            $('.sub-menu.toggled').not('.active').each(function () {
                $(this).removeClass('toggled');
                $(this).find('ul').hide();
            });



            $('.profile-menu .main-menu').hide();

            if (x == '#sidebar') {

                $elem = '#sidebar';
                $elem2 = '#menu-trigger';

                $('#chat-trigger').removeClass('open');

                if (!$('#chat').hasClass('toggled')) {
                    $('#header').toggleClass('sidebar-toggled');
                } else {
                    $('#chat').removeClass('toggled');
                }
            }

            if (x == '#chat') {
                $elem = '#chat';
                $elem2 = '#chat-trigger';

                $('#menu-trigger').removeClass('open');

                if (!$('#sidebar').hasClass('toggled')) {
                    $('#header').toggleClass('sidebar-toggled');
                } else {
                    $('#sidebar').removeClass('toggled');
                }
            }

            //When clicking outside
            if ($('#header').hasClass('sidebar-toggled')) {
                $(document).on('click', function (e) {
                    if (($(e.target).closest($elem).length === 0) && ($(e.target).closest($elem2).length === 0)) {
                        setTimeout(function () {
                            $($elem).removeClass('toggled');
                            $('#header').removeClass('sidebar-toggled');
                            $($elem2).removeClass('open');
                        });
                    }
                });
            }
        })

        //Submenu
        $('body').on('click', '.sub-menu > a', function (e) {
            e.preventDefault();
            $(this).next().slideToggle(200);
            $(this).parent().toggleClass('toggled');
        });
    })();

    /*
     * Clear Notification
     */
    $('body').on('click', '[data-clear="notification"]', function (e) {
        e.preventDefault();

        var x = $(this).closest('.listview');
        var y = x.find('.lv-item');
        var z = y.size();

        $(this).parent().fadeOut();

        x.find('.list-group').prepend('<i class="grid-loading hide-it"></i>');
        x.find('.grid-loading').fadeIn(1500);


        var w = 0;
        y.each(function () {
            var z = $(this);
            setTimeout(function () {
                z.addClass('animated fadeOutRightBig').delay(1000).queue(function () {
                    z.remove();
                });
            }, w += 150);
        })

        //Popup empty message
        setTimeout(function () {
            $('#notifications').addClass('empty');
        }, (z * 150) + 200);
    });

    /*
     * Dropdown Menu
     */
    if ($('.dropdown')[0]) {
        //Propagate
        $('body').on('click', '.dropdown.open .dropdown-menu', function (e) {
            e.stopPropagation();
        });

        $('.dropdown').on('shown.bs.dropdown', function (e) {
            if ($(this).attr('data-animation')) {
                $animArray = [];
                $animation = $(this).data('animation');
                $animArray = $animation.split(',');
                $animationIn = 'animated ' + $animArray[0];
                $animationOut = 'animated ' + $animArray[1];
                $animationDuration = ''
                if (!$animArray[2]) {
                    $animationDuration = 500; //if duration is not defined, default is set to 500ms
                } else {
                    $animationDuration = $animArray[2];
                }

                $(this).find('.dropdown-menu').removeClass($animationOut)
                $(this).find('.dropdown-menu').addClass($animationIn);
            }
        });

        $('.dropdown').on('hide.bs.dropdown', function (e) {
            if ($(this).attr('data-animation')) {
                e.preventDefault();
                $this = $(this);
                $dropdownMenu = $this.find('.dropdown-menu');

                $dropdownMenu.addClass($animationOut);
                setTimeout(function () {
                    $this.removeClass('open')

                }, $animationDuration);
            }
        });
    }



    /*
     * Auto Hight Textarea
     */
    if ($('.auto-size')[0]) {
        autosize($('.auto-size'));
    }

    /*
     * Profile Menu
     */
    $('body').on('click', '.profile-menu > a', function (e) {
        e.preventDefault();
        $(this).parent().toggleClass('toggled');
        $(this).next().slideToggle(200);
    });

    /*
     * Text Feild
     */

    //Add blue animated border and remove with condition when focus and blur
    if ($('.fg-line')[0]) {
        $('body').on('focus', '.fg-line .form-control', function () {
            $(this).closest('.fg-line').addClass('fg-toggled');
        })

        $('body').on('blur', '.form-control', function () {
            var p = $(this).closest('.form-group, .input-group');
            var i = p.find('.form-control').val();

            if (p.hasClass('fg-float')) {
                if (i.length == 0) {
                    $(this).closest('.fg-line').removeClass('fg-toggled');
                }
            } else {
                $(this).closest('.fg-line').removeClass('fg-toggled');
            }
        });
    }

    //Add blue border for pre-valued fg-flot text feilds
    if ($('.fg-float')[0]) {
        $('.fg-float .form-control').each(function () {
            var i = $(this).val();

            if (!i.length == 0) {
                $(this).closest('.fg-line').addClass('fg-toggled');
            }

        });
    }

    /*
     * Audio and Video
     */
    if ($('audio, video')[0]) {
        $('video,audio').mediaelementplayer();
    }

    /*
     * Tag Select
     */
    if ($('.chosen')[0]) {
        $('.chosen').chosen({
            width: '100%',
            allow_single_deselect: true
        });
    }

    /*
     * Input Slider
     */
    //Basic
    if ($('.input-slider')[0]) {
        $('.input-slider').each(function () {
            var isStart = $(this).data('is-start');

            $(this).noUiSlider({
                start: isStart,
                range: {
                    'min': 0,
                    'max': 100,
                }
            });
        });
    }

    //Range slider
    if ($('.input-slider-range')[0]) {
        $('.input-slider-range').noUiSlider({
            start: [30, 60],
            range: {
                'min': 0,
                'max': 100
            },
            connect: true
        });
    }

    //Range slider with value
    if ($('.input-slider-values')[0]) {
        $('.input-slider-values').noUiSlider({
            start: [45, 80],
            connect: true,
            direction: 'rtl',
            behaviour: 'tap-drag',
            range: {
                'min': 0,
                'max': 100
            }
        });

        $('.input-slider-values').Link('lower').to($('#value-lower'));
        $('.input-slider-values').Link('upper').to($('#value-upper'), 'html');
    }

    /*
     * Input Mask
     */
    if ($('input-mask')[0]) {
        $('.input-mask').mask();
    }

    /*
     * Color Picker
     */
    if ($('.color-picker')[0]) {
        $('.color-picker').each(function () {
            var colorOutput = $(this).closest('.cp-container').find('.cp-value');
            $(this).farbtastic(colorOutput);
        });
    }

    /*
     * HTML Editor
     */
    if ($('.html-editor')[0]) {
        $('.html-editor').summernote({
            height: 150
        });
    }

    if ($('.html-editor-click')[0]) {
        //Edit
        $('body').on('click', '.hec-button', function () {
            $('.html-editor-click').summernote({
                focus: true
            });
            $('.hec-save').show();
        })

        //Save
        $('body').on('click', '.hec-save', function () {
            $('.html-editor-click').code();
            $('.html-editor-click').destroy();
            $('.hec-save').hide();
            notify('Content Saved Successfully!', 'success');
        });
    }

    //Air Mode
    if ($('.html-editor-airmod')[0]) {
        $('.html-editor-airmod').summernote({
            airMode: true
        });
    }

    /*
     * Date Time Picker
     */

    //Date Time Picker
    if ($('.date-time-picker')[0]) {
        $('.date-time-picker').datetimepicker();
    }

    //Time
    if ($('.time-picker')[0]) {
        $('.time-picker').datetimepicker({
            format: 'LT'
        });
    }

    //Date
    if ($('.date-picker')[0]) {
        $('.date-picker').datetimepicker({
            format: 'DD/MM/YYYY'
        });
    }

    /*
     * Form Wizard
     */

    if ($('.form-wizard-basic')[0]) {
        $('.form-wizard-basic').bootstrapWizard({
            tabClass: 'fw-nav',
            'nextSelector': '.next',
            'previousSelector': '.previous'
        });
    }

    /*
     * Bootstrap Growl - Notifications popups
     */
    function notify(message, type) {
        $.growl({
            message: message
        }, {
            type: type,
            allow_dismiss: false,
            label: 'Cancel',
            className: 'btn-xs btn-inverse',
            placement: {
                from: 'top',
                align: 'right'
            },
            delay: 2500,
            animate: {
                enter: 'animated bounceIn',
                exit: 'animated bounceOut'
            },
            offset: {
                x: 20,
                y: 85
            }
        });
    };

    ///*
    // * Waves Animation
    // */
    //(function(){
    //	 Waves.attach('.btn:not(.btn-icon):not(.btn-float)');
    //	 Waves.attach('.btn-icon, .btn-float', ['waves-circle', 'waves-float']);
    //	Waves.init();
    //})();

    /*
     * Lightbox
     */
    if ($('.lightbox')[0]) {
        $('.lightbox').lightGallery({
            enableTouch: true
        });
    }

    /*
     * Link prevent
     */
    $('body').on('click', '.a-prevent', function (e) {
        e.preventDefault();
    });

    /*
     * Collaspe Fix
     */
    if ($('.collapse')[0]) {

        //Add active class for opened items
        $('.collapse').on('show.bs.collapse', function (e) {
            $(this).closest('.panel').find('.panel-heading').addClass('active');
        });

        $('.collapse').on('hide.bs.collapse', function (e) {
            $(this).closest('.panel').find('.panel-heading').removeClass('active');
        });

        //Add active class for pre opened items
        $('.collapse.in').each(function () {
            $(this).closest('.panel').find('.panel-heading').addClass('active');
        });
    }

    /*
     * Tooltips
     */
    if ($('[data-toggle="tooltip"]')[0]) {
        $('[data-toggle="tooltip"]').tooltip();
    }

    /*
     * Popover
     */
    if ($('[data-toggle="popover"]')[0]) {
        $('[data-toggle="popover"]').popover();
    }

    /*
     * Message
     */

    //Actions
    if ($('.on-select')[0]) {
        var checkboxes = '.lv-avatar-content input:checkbox';
        var actions = $('.on-select').closest('.lv-actions');

        $('body').on('click', checkboxes, function () {
            if ($(checkboxes + ':checked')[0]) {
                actions.addClass('toggled');
            } else {
                actions.removeClass('toggled');
            }
        });
    }

    if ($('#ms-menu-trigger')[0]) {
        $('body').on('click', '#ms-menu-trigger', function (e) {
            e.preventDefault();
            $(this).toggleClass('open');
            $('.ms-menu').toggleClass('toggled');
        });
    }

    /*
     * Login
     */
    if ($('.login-content')[0]) {
        //Add class to HTML. This is used to center align the logn box
        $('html').addClass('login-content');

        $('body').on('click', '.login-navigation > li', function () {
            var z = $(this).data('block');
            var t = $(this).closest('.lc-block');

            t.removeClass('toggled');

            setTimeout(function () {
                $(z).addClass('toggled');
            });

        })
    }

    /*
     * Fullscreen Browsing
     */
    if ($('[data-action="fullscreen"]')[0]) {
        var fs = $("[data-action='fullscreen']");
        fs.on('click', function (e) {
            e.preventDefault();

            //Launch
            function launchIntoFullscreen(element) {

                if (element.requestFullscreen) {
                    element.requestFullscreen();
                } else if (element.mozRequestFullScreen) {
                    element.mozRequestFullScreen();
                } else if (element.webkitRequestFullscreen) {
                    element.webkitRequestFullscreen();
                } else if (element.msRequestFullscreen) {
                    element.msRequestFullscreen();
                }
            }

            //Exit
            function exitFullscreen() {

                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen();
                }
            }

            launchIntoFullscreen(document.documentElement);
            fs.closest('.dropdown').removeClass('open');
        });
    }

    /*
     * Clear Local Storage
     */
    if ($('[data-action="clear-localstorage"]')[0]) {
        var cls = $('[data-action="clear-localstorage"]');

        cls.on('click', function (e) {
            e.preventDefault();

            swal({
                title: "¿Estás seguro?",
                text: "Todos tus datos serán eliminados",
                type: "Peligro",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Si, borra todo!",
                closeOnConfirm: false
            }, function () {
                localStorage.clear();
                swal("Listo!", "datos locales han sido eliminados", "vientos!");
            });
        });
    }

    /*
     * Profile Edit Toggle
     */


    if ($('[data-pmb-action]')[0]) {
        $('body').on('click', '[data-pmb-action]', function (e) {
            e.preventDefault();
            var d = $(this).data('pmb-action');

            if (d === "edit") {
                $(this).closest('.pmb-block').toggleClass('toggled');
            }

            if (d === "reset") {
                $(this).closest('.pmb-block').removeClass('toggled');
            }


        });
    }

    /*
     * IE 9 Placeholder
     */
    if ($('html').hasClass('ie9')) {
        $('input, textarea').placeholder({
            customClass: 'ie9-placeholder'
        });
    }


    /*
     * Listview Search
     */
    if ($('.lvh-search-trigger')[0]) {


        $('body').on('click', '.lvh-search-trigger', function (e) {
            e.preventDefault();
            x = $(this).closest('.lv-header-alt').find('.lvh-search');

            x.fadeIn(300);
            x.find('.lvhs-input').focus();
        });

        //Close Search
        $('body').on('click', '.lvh-search-close', function () {
            x.fadeOut(300);
            setTimeout(function () {
                x.find('.lvhs-input').val('');
            }, 350);
        })
    }


    /*
     * Print
     */
    if ($('[data-action="print"]')[0]) {
        $('body').on('click', '[data-action="print"]', function (e) {
            e.preventDefault();

            window.print();
        })
    }

    /*
     * Typeahead Auto Complete
     */
    if ($('.typeahead')[0]) {

        var statesArray = ['Acambay', 'Acolman', 'Aculco', 'Almoloya de Alquisiras', 'Almoloya de Juárez', 'Almoloya del Río', 'Amanalco', 'Amatepec', 'Amecameca', 'Apaxco', 'Atenco', 'Atizapán', 'Atizapán de Zaragoza', 'Atlacomulco', 'Atlautla', 'Axapusco', 'Ayapango', 'Calimaya', 'Capulhuac', 'Chalco', 'Chapa de Mota', 'Chapultepec', 'Chiautla', 'Chicoloapan', 'Chiconcuac', 'Chimalhuacán', 'Coacalco de Berriozábal', 'Coatepec Harinas', 'Cocotitlán', 'Coyotepec', 'Cuautitlán', 'Cuautitlán Izcalli', 'Donato Guerra', 'Ecatepec de Morelos', 'Ecatzingo', 'El Oro', 'Huehuetoca', 'Hueypoxtla', 'Huixquilucan', 'Isidro Fabela', 'Ixtapaluca', 'Ixtapan de la Sal', 'Ixtapan del Oro', 'Ixtlahuaca', 'Jaltenco', 'Jilotepec', 'Jilotzingo', 'Jiquipilco', 'Jocotitlán', 'Joquicingo', 'Juchitepec', 'La Paz', 'Lerma', 'Luvianos', 'Malinalco', 'Melchor Ocampo', 'Metepec', 'Mexicaltzingo', 'Morelos', 'Naucalpan de Juárez', 'Nextlalpan', 'Nezahualcóyotl', 'Nicolás Romero', 'Nopaltepec', 'Ocoyoacac', 'Ocuilan', 'Otumba', 'Otzoloapan', 'Otzolotepec', 'Ozumba', 'Papalotla', 'Polotitlán', 'Rayón', 'San Antonio la Isla', 'San Felipe del Progreso', 'San José del Rincón', 'San Martín de las Pirámides', 'San Mateo Atenco', 'San Simón de Guerrero', 'Santo Tomás', 'Soyaniquilpan de Juárez', 'Sultepec', 'Tecámac', 'Tejupilco', 'Temamatla', 'Temascalapa', 'Temascalcingo', 'Temascaltepec', 'Temoaya', 'Tenancingo', 'Tenango del Aire', 'Tenango del Valle', 'Teoloyucán', 'Teotihuacán', 'Tepetlaoxtoc', 'Tepetlixpa', 'Tepotzotlán', 'Tequixquiac', 'Texcaltitlán', 'Texcalyacac', 'Texcoco', 'Tezoyuca', 'Tianguistenco', 'Timilpan', 'Tlalmanalco', 'Tlalnepantla de Baz', 'Tlatlaya', 'Toluca', 'Tonanitla', 'Tonatico', 'Tultepec', 'Tultitlán', 'Valle de Bravo', 'Valle de Chalco Solidaridad', 'Villa de Allende', 'Villa del Carbón', 'Villa Guerrero', 'Villa Victoria', 'Xalatlaco', 'Xonacatlán', 'Zacazonapan', 'Zacualpan', 'Zinacantepec', 'Zumpahuacán', 'Zumpango'];
        var states = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            local: statesArray
        });

        $('.typeahead').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
        }, {
            name: 'Estados',
            source: states
        });
    }


    /*
     * Wall
     */
    if ($('.wcc-toggle')[0]) {
        var z = '<div class="wcc-inner">' +
            '<textarea class="wcci-text auto-size" placeholder="Escribe Algo..."></textarea>' +
            '</div>' +
            '<div class="m-t-15">' +
            '<button class="btn btn-sm btn-primary">Mensaje</button>' +
            '<button class="btn btn-sm btn-link wcc-cencel">Cancelar</button>' +
            '</div>'


        $('body').on('click', '.wcc-toggle', function () {
            $(this).parent().html(z);
            autosize($('.auto-size')); //Reload Auto size textarea
        });

        //Cancel
        $('body').on('click', '.wcc-cencel', function (e) {
            e.preventDefault();

            $(this).closest('.wc-comment').find('.wcc-inner').addClass('wcc-toggle').html('Write Something...')
        });

    }

    /*
     * Skin Change
     */
    $('body').on('click', '[data-skin]', function () {
        var currentSkin = $('[data-current-skin]').data('current-skin');
        var skin = $(this).data('skin');

        $('[data-current-skin]').attr('data-current-skin', skin)

    });

});

var bmm023ln2k = function () {
    function t(t) {
        return null == t ? String(t) : W[Y.call(t)] || "object"
    }

    function e(e) {
        return "function" == t(e)
    }

    function n(t) {
        return null != t && t == t.window
    }

    function r(t) {
        return null != t && t.nodeType == t.DOCUMENT_NODE
    }

    function i(e) {
        return "object" == t(e)
    }

    function a(t) {
        return i(t) && !n(t) && Object.getPrototypeOf(t) == Object.prototype
    }

    function o(t) {
        return "number" == typeof t.length
    }

    function s(t) {
        return k.call(t, function (t) {
            return null != t
        })
    }

    function c(t) {
        return t.length > 0 ? N.fn.concat.apply([], t) : t
    }

    function u(t) {
        return t.replace(/::/g, "/").replace(/([A-Z]+)([A-Z][a-z])/g, "$1_$2").replace(/([a-z\d])([A-Z])/g, "$1_$2").replace(/_/g, "-").toLowerCase()
    }

    function l(t) {
        return t in R ? R[t] : R[t] = new RegExp("(^|\\s)" + t + "(\\s|$)")
    }

    function f(t, e) {
        return "number" != typeof e || L[u(t)] ? e : e + "px"
    }

    function h(t) {
        var e, n;
        return M[t] || (e = j.createElement(t), j.body.appendChild(e), n = getComputedStyle(e, "").getPropertyValue("display"), e.parentNode.removeChild(e), "none" == n && (n = "block"), M[t] = n), M[t]
    }

    function p(t) {
        return "children" in t ? O.call(t.children) : N.map(t.childNodes, function (t) {
            return 1 == t.nodeType ? t : void 0
        })
    }

    function d(t, e, n) {
        for (T in e) n && (a(e[T]) || V(e[T])) ? (a(e[T]) && !a(t[T]) && (t[T] = {}), V(e[T]) && !V(t[T]) && (t[T] = []), d(t[T], e[T], n)) : e[T] !== x && (t[T] = e[T])
    }

    function m(t, e) {
        return null == e ? N(t) : N(t).filter(e)
    }

    function g(t, n, r, i) {
        return e(n) ? n.call(t, r, i) : n
    }

    function v(t, e, n) {
        null == n ? t.removeAttribute(e) : t.setAttribute(e, n)
    }

    function y(t, e) {
        var n = t.className || "",
            r = n && n.baseVal !== x;
        return e === x ? r ? n.baseVal : n : void (r ? n.baseVal = e : t.className = e)
    }

    function w(t) {
        try {
            return t ? "true" == t || ("false" == t ? !1 : "null" == t ? null : +t + "" == t ? +t : /^[\[\{]/.test(t) ? N.parseJSON(t) : t) : t
        } catch (e) {
            return t
        }
    }

    function b(t, e) {
        e(t);
        for (var n = 0, r = t.childNodes.length; r > n; n++) b(t.childNodes[n], e)
    }
    var x, T, N, E, S, C, A = [],
        O = A.slice,
        k = A.filter,
        j = window.document,
        M = {},
        R = {},
        L = {
            "column-count": 1,
            columns: 1,
            "font-weight": 1,
            "line-height": 1,
            opacity: 1,
            "z-index": 1,
            zoom: 1
        },
        Z = /^\s*<(\w+|!)[^>]*>/,
        P = /^<(\w+)\s*\/?>(?:<\/\1>|)$/,
        H = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/gi,
        D = /^(?:body|html)$/i,
        F = /([A-Z])/g,
        _ = ["val", "css", "html", "text", "data", "width", "height", "offset"],
        z = ["after", "prepend", "before", "append"],
        I = j.createElement("table"),
        G = j.createElement("tr"),
        B = {
            tr: j.createElement("tbody"),
            tbody: I,
            thead: I,
            tfoot: I,
            td: G,
            th: G,
            "*": j.createElement("div")
        },
        U = /complete|loaded|interactive/,
        $ = /^[\w-]*$/,
        W = {},
        Y = W.toString,
        q = {},
        J = j.createElement("div"),
        X = {
            tabindex: "tabIndex",
            readonly: "readOnly",
            "for": "htmlFor",
            "class": "className",
            maxlength: "maxLength",
            cellspacing: "cellSpacing",
            cellpadding: "cellPadding",
            rowspan: "rowSpan",
            colspan: "colSpan",
            usemap: "useMap",
            frameborder: "frameBorder",
            contenteditable: "contentEditable"
        },
        V = Array.isArray || function (t) {
            return t instanceof Array
        };
    return q.matches = function (t, e) {
        if (!e || !t || 1 !== t.nodeType) return !1;
        var n = t.webkitMatchesSelector || t.mozMatchesSelector || t.oMatchesSelector || t.matchesSelector;
        if (n) return n.call(t, e);
        var r, i = t.parentNode,
            a = !i;
        return a && (i = J).appendChild(t), r = ~q.qsa(i, e).indexOf(t), a && J.removeChild(t), r
    }, S = function (t) {
        return t.replace(/-+(.)?/g, function (t, e) {
            return e ? e.toUpperCase() : ""
        })
    }, C = function (t) {
        return k.call(t, function (e, n) {
            return t.indexOf(e) == n
        })
    }, q.fragment = function (t, e, n) {
        var r, i, o;
        return P.test(t) && (r = N(j.createElement(RegExp.$1))), r || (t.replace && (t = t.replace(H, "<$1></$2>")), e === x && (e = Z.test(t) && RegExp.$1), e in B || (e = "*"), o = B[e], o.innerHTML = "" + t, r = N.each(O.call(o.childNodes), function () {
            o.removeChild(this)
        })), a(n) && (i = N(r), N.each(n, function (t, e) {
            _.indexOf(t) > -1 ? i[t](e) : i.attr(t, e)
        })), r
    }, q.Z = function (t, e) {
        return t = t || [], t.__proto__ = N.fn, t.selector = e || "", t
    }, q.isZ = function (t) {
        return t instanceof q.Z
    }, q.init = function (t, n) {
        var r;
        if (!t) return q.Z();
        if ("string" == typeof t)
            if (t = t.trim(), "<" == t[0] && Z.test(t)) r = q.fragment(t, RegExp.$1, n), t = null;
            else {
                if (n !== x) return N(n).find(t);
                r = q.qsa(j, t)
            }
        else {
            if (e(t)) return N(j).ready(t);
            if (q.isZ(t)) return t;
            if (V(t)) r = s(t);
            else if (i(t)) r = [t], t = null;
            else if (Z.test(t)) r = q.fragment(t.trim(), RegExp.$1, n), t = null;
            else {
                if (n !== x) return N(n).find(t);
                r = q.qsa(j, t)
            }
        }
        return q.Z(r, t)
    }, N = function (t, e) {
        return q.init(t, e)
    }, N.extend = function (t) {
        var e, n = O.call(arguments, 1);
        return "boolean" == typeof t && (e = t, t = n.shift()), n.forEach(function (n) {
            d(t, n, e)
        }), t
    }, q.qsa = function (t, e) {
        var n, i = "#" == e[0],
            a = !i && "." == e[0],
            o = i || a ? e.slice(1) : e,
            s = $.test(o);
        return r(t) && s && i ? (n = t.getElementById(o)) ? [n] : [] : 1 !== t.nodeType && 9 !== t.nodeType ? [] : O.call(s && !i ? a ? t.getElementsByClassName(o) : t.getElementsByTagName(e) : t.querySelectorAll(e))
    }, N.contains = j.documentElement.contains ? function (t, e) {
        return t !== e && t.contains(e)
    } : function (t, e) {
        for (; e && (e = e.parentNode) ;)
            if (e === t) return !0;
        return !1
    }, N.type = t, N.isFunction = e, N.isWindow = n, N.isArray = V, N.isPlainObject = a, N.isEmptyObject = function (t) {
        var e;
        for (e in t) return !1;
        return !0
    }, N.inArray = function (t, e, n) {
        return A.indexOf.call(e, t, n)
    }, N.camelCase = S, N.trim = function (t) {
        return null == t ? "" : String.prototype.trim.call(t)
    }, N.uuid = 0, N.support = {}, N.expr = {}, N.map = function (t, e) {
        var n, r, i, a = [];
        if (o(t))
            for (r = 0; r < t.length; r++) n = e(t[r], r), null != n && a.push(n);
        else
            for (i in t) n = e(t[i], i), null != n && a.push(n);
        return c(a)
    }, N.each = function (t, e) {
        var n, r;
        if (o(t)) {
            for (n = 0; n < t.length; n++)
                if (e.call(t[n], n, t[n]) === !1) return t
        } else
            for (r in t)
                if (e.call(t[r], r, t[r]) === !1) return t;
        return t
    }, N.grep = function (t, e) {
        return k.call(t, e)
    }, window.JSON && (N.parseJSON = JSON.parse), N.each("Boolean Number String Function Array Date RegExp Object Error".split(" "), function (t, e) {
        W["[object " + e + "]"] = e.toLowerCase()
    }), N.fn = {
        forEach: A.forEach,
        reduce: A.reduce,
        push: A.push,
        sort: A.sort,
        indexOf: A.indexOf,
        concat: A.concat,
        map: function (t) {
            return N(N.map(this, function (e, n) {
                return t.call(e, n, e)
            }))
        },
        slice: function () {
            return N(O.apply(this, arguments))
        },
        ready: function (t) {
            return U.test(j.readyState) && j.body ? t(N) : j.addEventListener("DOMContentLoaded", function () {
                t(N)
            }, !1), this
        },
        get: function (t) {
            return t === x ? O.call(this) : this[t >= 0 ? t : t + this.length]
        },
        toArray: function () {
            return this.get()
        },
        size: function () {
            return this.length
        },
        remove: function () {
            return this.each(function () {
                null != this.parentNode && this.parentNode.removeChild(this)
            })
        },
        each: function (t) {
            return A.every.call(this, function (e, n) {
                return t.call(e, n, e) !== !1
            }), this
        },
        filter: function (t) {
            return e(t) ? this.not(this.not(t)) : N(k.call(this, function (e) {
                return q.matches(e, t)
            }))
        },
        add: function (t, e) {
            return N(C(this.concat(N(t, e))))
        },
        is: function (t) {
            return this.length > 0 && q.matches(this[0], t)
        },
        not: function (t) {
            var n = [];
            if (e(t) && t.call !== x) this.each(function (e) {
                t.call(this, e) || n.push(this)
            });
            else {
                var r = "string" == typeof t ? this.filter(t) : o(t) && e(t.item) ? O.call(t) : N(t);
                this.forEach(function (t) {
                    r.indexOf(t) < 0 && n.push(t)
                })
            }
            return N(n)
        },
        has: function (t) {
            return this.filter(function () {
                return i(t) ? N.contains(this, t) : N(this).find(t).size()
            })
        },
        eq: function (t) {
            return -1 === t ? this.slice(t) : this.slice(t, +t + 1)
        },
        first: function () {
            var t = this[0];
            return t && !i(t) ? t : N(t)
        },
        last: function () {
            var t = this[this.length - 1];
            return t && !i(t) ? t : N(t)
        },
        find: function (t) {
            var e, n = this;
            return e = t ? "object" == typeof t ? N(t).filter(function () {
                var t = this;
                return A.some.call(n, function (e) {
                    return N.contains(e, t)
                })
            }) : 1 == this.length ? N(q.qsa(this[0], t)) : this.map(function () {
                return q.qsa(this, t)
            }) : N()
        },
        closest: function (t, e) {
            var n = this[0],
                i = !1;
            for ("object" == typeof t && (i = N(t)) ; n && !(i ? i.indexOf(n) >= 0 : q.matches(n, t)) ;) n = n !== e && !r(n) && n.parentNode;
            return N(n)
        },
        parents: function (t) {
            for (var e = [], n = this; n.length > 0;) n = N.map(n, function (t) {
                return (t = t.parentNode) && !r(t) && e.indexOf(t) < 0 ? (e.push(t), t) : void 0
            });
            return m(e, t)
        },
        parent: function (t) {
            return m(C(this.pluck("parentNode")), t)
        },
        children: function (t) {
            return m(this.map(function () {
                return p(this)
            }), t)
        },
        contents: function () {
            return this.map(function () {
                return O.call(this.childNodes)
            })
        },
        siblings: function (t) {
            return m(this.map(function (t, e) {
                return k.call(p(e.parentNode), function (t) {
                    return t !== e
                })
            }), t)
        },
        empty: function () {
            return this.each(function () {
                this.innerHTML = ""
            })
        },
        pluck: function (t) {
            return N.map(this, function (e) {
                return e[t]
            })
        },
        show: function () {
            return this.each(function () {
                "none" == this.style.display && (this.style.display = ""), "none" == getComputedStyle(this, "").getPropertyValue("display") && (this.style.display = h(this.nodeName))
            })
        },
        replaceWith: function (t) {
            return this.before(t).remove()
        },
        wrap: function (t) {
            var n = e(t);
            if (this[0] && !n) var r = N(t).get(0),
                i = r.parentNode || this.length > 1;
            return this.each(function (e) {
                N(this).wrapAll(n ? t.call(this, e) : i ? r.cloneNode(!0) : r)
            })
        },
        wrapAll: function (t) {
            if (this[0]) {
                N(this[0]).before(t = N(t));
                for (var e;
                    (e = t.children()).length;) t = e.first();
                N(t).append(this)
            }
            return this
        },
        wrapInner: function (t) {
            var n = e(t);
            return this.each(function (e) {
                var r = N(this),
                    i = r.contents(),
                    a = n ? t.call(this, e) : t;
                i.length ? i.wrapAll(a) : r.append(a)
            })
        },
        unwrap: function () {
            return this.parent().each(function () {
                N(this).replaceWith(N(this).children())
            }), this
        },
        clone: function () {
            return this.map(function () {
                return this.cloneNode(!0)
            })
        },
        hide: function () {
            return this.css("display", "none")
        },
        toggle: function (t) {
            return this.each(function () {
                var e = N(this);
                (t === x ? "none" == e.css("display") : t) ? e.show() : e.hide()
            })
        },
        prev: function (t) {
            return N(this.pluck("previousElementSibling")).filter(t || "*")
        },
        next: function (t) {
            return N(this.pluck("nextElementSibling")).filter(t || "*")
        },
        html: function (t) {
            return 0 in arguments ? this.each(function (e) {
                var n = this.innerHTML;
                N(this).empty().append(g(this, t, e, n))
            }) : 0 in this ? this[0].innerHTML : null
        },
        text: function (t) {
            return 0 in arguments ? this.each(function (e) {
                var n = g(this, t, e, this.textContent);
                this.textContent = null == n ? "" : "" + n
            }) : 0 in this ? this[0].textContent : null
        },
        attr: function (t, e) {
            var n;
            return "string" != typeof t || 1 in arguments ? this.each(function (n) {
                if (1 === this.nodeType)
                    if (i(t))
                        for (T in t) v(this, T, t[T]);
                    else v(this, t, g(this, e, n, this.getAttribute(t)))
            }) : this.length && 1 === this[0].nodeType ? !(n = this[0].getAttribute(t)) && t in this[0] ? this[0][t] : n : x
        },
        removeAttr: function (t) {
            return this.each(function () {
                1 === this.nodeType && t.split(" ").forEach(function (t) {
                    v(this, t)
                }, this)
            })
        },
        prop: function (t, e) {
            return t = X[t] || t, 1 in arguments ? this.each(function (n) {
                this[t] = g(this, e, n, this[t])
            }) : this[0] && this[0][t]
        },
        data: function (t, e) {
            var n = "data-" + t.replace(F, "-$1").toLowerCase(),
                r = 1 in arguments ? this.attr(n, e) : this.attr(n);
            return null !== r ? w(r) : x
        },
        val: function (t) {
            return 0 in arguments ? this.each(function (e) {
                this.value = g(this, t, e, this.value)
            }) : this[0] && (this[0].multiple ? N(this[0]).find("option").filter(function () {
                return this.selected
            }).pluck("value") : this[0].value)
        },
        offset: function (t) {
            if (t) return this.each(function (e) {
                var n = N(this),
                    r = g(this, t, e, n.offset()),
                    i = n.offsetParent().offset(),
                    a = {
                        top: r.top - i.top,
                        left: r.left - i.left
                    };
                "static" == n.css("position") && (a.position = "relative"), n.css(a)
            });
            if (!this.length) return null;
            var e = this[0].getBoundingClientRect();
            return {
                left: e.left + window.pageXOffset,
                top: e.top + window.pageYOffset,
                width: Math.round(e.width),
                height: Math.round(e.height)
            }
        },
        css: function (e, n) {
            if (arguments.length < 2) {
                var r, i = this[0];
                if (!i) return;
                if (r = getComputedStyle(i, ""), "string" == typeof e) return i.style[S(e)] || r.getPropertyValue(e);
                if (V(e)) {
                    var a = {};
                    return N.each(e, function (t, e) {
                        a[e] = i.style[S(e)] || r.getPropertyValue(e)
                    }), a
                }
            }
            var o = "";
            if ("string" == t(e)) n || 0 === n ? o = u(e) + ":" + f(e, n) : this.each(function () {
                this.style.removeProperty(u(e))
            });
            else
                for (T in e) e[T] || 0 === e[T] ? o += u(T) + ":" + f(T, e[T]) + ";" : this.each(function () {
                    this.style.removeProperty(u(T))
                });
            return this.each(function () {
                this.style.cssText += ";" + o
            })
        },
        index: function (t) {
            return t ? this.indexOf(N(t)[0]) : this.parent().children().indexOf(this[0])
        },
        hasClass: function (t) {
            return t ? A.some.call(this, function (t) {
                return this.test(y(t))
            }, l(t)) : !1
        },
        addClass: function (t) {
            return t ? this.each(function (e) {
                if ("className" in this) {
                    E = [];
                    var n = y(this),
                        r = g(this, t, e, n);
                    r.split(/\s+/g).forEach(function (t) {
                        N(this).hasClass(t) || E.push(t)
                    }, this), E.length && y(this, n + (n ? " " : "") + E.join(" "))
                }
            }) : this
        },
        removeClass: function (t) {
            return this.each(function (e) {
                if ("className" in this) {
                    if (t === x) return y(this, "");
                    E = y(this), g(this, t, e, E).split(/\s+/g).forEach(function (t) {
                        E = E.replace(l(t), " ")
                    }), y(this, E.trim())
                }
            })
        },
        toggleClass: function (t, e) {
            return t ? this.each(function (n) {
                var r = N(this),
                    i = g(this, t, n, y(this));
                i.split(/\s+/g).forEach(function (t) {
                    (e === x ? !r.hasClass(t) : e) ? r.addClass(t) : r.removeClass(t)
                })
            }) : this
        },
        scrollTop: function (t) {
            if (this.length) {
                var e = "scrollTop" in this[0];
                return t === x ? e ? this[0].scrollTop : this[0].pageYOffset : this.each(e ? function () {
                    this.scrollTop = t
                } : function () {
                    this.scrollTo(this.scrollX, t)
                })
            }
        },
        scrollLeft: function (t) {
            if (this.length) {
                var e = "scrollLeft" in this[0];
                return t === x ? e ? this[0].scrollLeft : this[0].pageXOffset : this.each(e ? function () {
                    this.scrollLeft = t
                } : function () {
                    this.scrollTo(t, this.scrollY)
                })
            }
        },
        position: function () {
            if (this.length) {
                var t = this[0],
                    e = this.offsetParent(),
                    n = this.offset(),
                    r = D.test(e[0].nodeName) ? {
                        top: 0,
                        left: 0
                    } : e.offset();
                return n.top -= parseFloat(N(t).css("margin-top")) || 0, n.left -= parseFloat(N(t).css("margin-left")) || 0, r.top += parseFloat(N(e[0]).css("border-top-width")) || 0, r.left += parseFloat(N(e[0]).css("border-left-width")) || 0, {
                    top: n.top - r.top,
                    left: n.left - r.left
                }
            }
        },
        offsetParent: function () {
            return this.map(function () {
                for (var t = this.offsetParent || j.body; t && !D.test(t.nodeName) && "static" == N(t).css("position") ;) t = t.offsetParent;
                return t
            })
        }
    }, N.fn.detach = N.fn.remove, ["width", "height"].forEach(function (t) {
        var e = t.replace(/./, function (t) {
            return t[0].toUpperCase()
        });
        N.fn[t] = function (i) {
            var a, o = this[0];
            return i === x ? n(o) ? o["inner" + e] : r(o) ? o.documentElement["scroll" + e] : (a = this.offset()) && a[t] : this.each(function (e) {
                o = N(this), o.css(t, g(this, i, e, o[t]()))
            })
        }
    }), z.forEach(function (e, n) {
        var r = n % 2;
        N.fn[e] = function () {
            var e, i, a = N.map(arguments, function (n) {
                return e = t(n), "object" == e || "array" == e || null == n ? n : q.fragment(n)
            }),
                o = this.length > 1;
            return a.length < 1 ? this : this.each(function (t, e) {
                i = r ? e : e.parentNode, e = 0 == n ? e.nextSibling : 1 == n ? e.firstChild : 2 == n ? e : null;
                var s = N.contains(j.documentElement, i);
                a.forEach(function (t) {
                    if (o) t = t.cloneNode(!0);
                    else if (!i) return N(t).remove();
                    i.insertBefore(t, e), s && b(t, function (t) {
                        null == t.nodeName || "SCRIPT" !== t.nodeName.toUpperCase() || t.type && "text/javascript" !== t.type || t.src || window.eval.call(window, t.innerHTML)
                    })
                })
            })
        }, N.fn[r ? e + "To" : "insert" + (n ? "Before" : "After")] = function (t) {
            return N(t)[e](this), this
        }
    }), q.Z.prototype = N.fn, q.uniq = C, q.deserializeValue = w, N.bmm023ln2k = q, N
}();
window.bmm023ln2k = bmm023ln2k,
    function (t) {
        function e(t) {
            return t._zid || (t._zid = h++)
        }

        function n(t, n, a, o) {
            if (n = r(n), n.ns) var s = i(n.ns);
            return (g[e(t)] || []).filter(function (t) {
                return !(!t || n.e && t.e != n.e || n.ns && !s.test(t.ns) || a && e(t.fn) !== e(a) || o && t.sel != o)
            })
        }

        function r(t) {
            var e = ("" + t).split(".");
            return {
                e: e[0],
                ns: e.slice(1).sort().join(" ")
            }
        }

        function i(t) {
            return new RegExp("(?:^| )" + t.replace(" ", " .* ?") + "(?: |$)")
        }

        function a(t, e) {
            return t.del && !y && t.e in w || !!e
        }

        function o(t) {
            return b[t] || y && w[t] || t
        }

        function s(n, i, s, c, l, h, p) {
            var d = e(n),
                m = g[d] || (g[d] = []);
            i.split(/\s/).forEach(function (e) {
                if ("ready" == e) return t(document).ready(s);
                var i = r(e);
                i.fn = s, i.sel = l, i.e in b && (s = function (e) {
                    var n = e.relatedTarget;
                    return !n || n !== this && !t.contains(this, n) ? i.fn.apply(this, arguments) : void 0
                }), i.del = h;
                var d = h || s;
                i.proxy = function (t) {
                    if (t = u(t), !t.isImmediatePropagationStopped()) {
                        t.data = c;
                        var e = d.apply(n, t._args == f ? [t] : [t].concat(t._args));
                        return e === !1 && (t.preventDefault(), t.stopPropagation()), e
                    }
                }, i.i = m.length, m.push(i), "addEventListener" in n && n.addEventListener(o(i.e), i.proxy, a(i, p))
            })
        }

        function c(t, r, i, s, c) {
            var u = e(t);
            (r || "").split(/\s/).forEach(function (e) {
                n(t, e, i, s).forEach(function (e) {
                    delete g[u][e.i], "removeEventListener" in t && t.removeEventListener(o(e.e), e.proxy, a(e, c))
                })
            })
        }

        function u(e, n) {
            return (n || !e.isDefaultPrevented) && (n || (n = e), t.each(E, function (t, r) {
                var i = n[t];
                e[t] = function () {
                    return this[r] = x, i && i.apply(n, arguments)
                }, e[r] = T
            }), (n.defaultPrevented !== f ? n.defaultPrevented : "returnValue" in n ? n.returnValue === !1 : n.getPreventDefault && n.getPreventDefault()) && (e.isDefaultPrevented = x)), e
        }

        function l(t) {
            var e, n = {
                originalEvent: t
            };
            for (e in t) N.test(e) || t[e] === f || (n[e] = t[e]);
            return u(n, t)
        }
        var f, h = 1,
            p = Array.prototype.slice,
            d = t.isFunction,
            m = function (t) {
                return "string" == typeof t
            },
            g = {},
            v = {},
            y = "onfocusin" in window,
            w = {
                focus: "focusin",
                blur: "focusout"
            },
            b = {
                mouseenter: "mouseover",
                mouseleave: "mouseout"
            };
        v.click = v.mousedown = v.mouseup = v.mousemove = "MouseEvents", t.event = {
            add: s,
            remove: c
        }, t.proxy = function (n, r) {
            var i = 2 in arguments && p.call(arguments, 2);
            if (d(n)) {
                var a = function () {
                    return n.apply(r, i ? i.concat(p.call(arguments)) : arguments)
                };
                return a._zid = e(n), a
            }
            if (m(r)) return i ? (i.unshift(n[r], n), t.proxy.apply(null, i)) : t.proxy(n[r], n);
            throw new TypeError("expected function")
        }, t.fn.bind = function (t, e, n) {
            return this.on(t, e, n)
        }, t.fn.unbind = function (t, e) {
            return this.off(t, e)
        }, t.fn.one = function (t, e, n, r) {
            return this.on(t, e, n, r, 1)
        };
        var x = function () {
            return !0
        },
            T = function () {
                return !1
            },
            N = /^([A-Z]|returnValue$|layer[XY]$)/,
            E = {
                preventDefault: "isDefaultPrevented",
                stopImmediatePropagation: "isImmediatePropagationStopped",
                stopPropagation: "isPropagationStopped"
            };
        t.fn.delegate = function (t, e, n) {
            return this.on(e, t, n)
        }, t.fn.undelegate = function (t, e, n) {
            return this.off(e, t, n)
        }, t.fn.live = function (e, n) {
            return t(document.body).delegate(this.selector, e, n), this
        }, t.fn.die = function (e, n) {
            return t(document.body).undelegate(this.selector, e, n), this
        }, t.fn.on = function (e, n, r, i, a) {
            var o, u, h = this;
            return e && !m(e) ? (t.each(e, function (t, e) {
                h.on(t, n, r, e, a)
            }), h) : (m(n) || d(i) || i === !1 || (i = r, r = n, n = f), (d(r) || r === !1) && (i = r, r = f), i === !1 && (i = T), h.each(function (f, h) {
                a && (o = function (t) {
                    return c(h, t.type, i), i.apply(this, arguments)
                }), n && (u = function (e) {
                    var r, a = t(e.target).closest(n, h).get(0);
                    return a && a !== h ? (r = t.extend(l(e), {
                        currentTarget: a,
                        liveFired: h
                    }), (o || i).apply(a, [r].concat(p.call(arguments, 1)))) : void 0
                }), s(h, e, i, r, n, u || o)
            }))
        }, t.fn.off = function (e, n, r) {
            var i = this;
            return e && !m(e) ? (t.each(e, function (t, e) {
                i.off(t, n, e)
            }), i) : (m(n) || d(r) || r === !1 || (r = n, n = f), r === !1 && (r = T), i.each(function () {
                c(this, e, r, n)
            }))
        }, t.fn.trigger = function (e, n) {
            return e = m(e) || t.isPlainObject(e) ? t.Event(e) : u(e), e._args = n, this.each(function () {
                e.type in w && "function" == typeof this[e.type] ? this[e.type]() : "dispatchEvent" in this ? this.dispatchEvent(e) : t(this).triggerHandler(e, n)
            })
        }, t.fn.triggerHandler = function (e, r) {
            var i, a;
            return this.each(function (o, s) {
                i = l(m(e) ? t.Event(e) : e), i._args = r, i.target = s, t.each(n(s, e.type || e), function (t, e) {
                    return a = e.proxy(i), i.isImmediatePropagationStopped() ? !1 : void 0
                })
            }), a
        }, "focusin focusout focus blur load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select keydown keypress keyup error".split(" ").forEach(function (e) {
            t.fn[e] = function (t) {
                return 0 in arguments ? this.bind(e, t) : this.trigger(e)
            }
        }), t.Event = function (t, e) {
            m(t) || (e = t, t = e.type);
            var n = document.createEvent(v[t] || "Events"),
                r = !0;
            if (e)
                for (var i in e) "bubbles" == i ? r = !!e[i] : n[i] = e[i];
            return n.initEvent(t, r, !0), u(n)
        }
    }(bmm023ln2k),
    function (t) {
        function e(e, n, r) {
            var i = t.Event(n);
            return t(e).trigger(i, r), !i.isDefaultPrevented()
        }

        function n(t, n, r, i) {
            return t.global ? e(n || y, r, i) : void 0
        }

        function r(e) {
            e.global && 0 === t.active++ && n(e, null, "ajaxStart")
        }

        function i(e) {
            e.global && !--t.active && n(e, null, "ajaxStop")
        }

        function a(t, e) {
            var r = e.context;
            return e.beforeSend.call(r, t, e) === !1 || n(e, r, "ajaxBeforeSend", [t, e]) === !1 ? !1 : void n(e, r, "ajaxSend", [t, e])
        }

        function o(t, e, r, i) {
            var a = r.context,
                o = "success";
            r.success.call(a, t, o, e), i && i.resolveWith(a, [t, o, e]), n(r, a, "ajaxSuccess", [e, r, t]), c(o, e, r)
        }

        function s(t, e, r, i, a) {
            var o = i.context;
            i.error.call(o, r, e, t), a && a.rejectWith(o, [r, e, t]), n(i, o, "ajaxError", [r, i, t || e]), c(e, r, i)
        }

        function c(t, e, r) {
            var a = r.context;
            r.complete.call(a, e, t), n(r, a, "ajaxComplete", [e, r]), i(r)
        }

        function u() { }

        function l(t) {
            return t && (t = t.split(";", 2)[0]), t && (t == N ? "html" : t == T ? "json" : b.test(t) ? "script" : x.test(t) && "xml") || "text"
        }

        function f(t, e) {
            return "" == e ? t : (t + "&" + e).replace(/[&?]{1,2}/, "?")
        }

        function h(e) {
            e.processData && e.data && "string" != t.type(e.data) && (e.data = t.param(e.data, e.traditional)), !e.data || e.type && "GET" != e.type.toUpperCase() || (e.url = f(e.url, e.data), e.data = void 0)
        }

        function p(e, n, r, i) {
            return t.isFunction(n) && (i = r, r = n, n = void 0), t.isFunction(r) || (i = r, r = void 0), {
                url: e,
                data: n,
                success: r,
                dataType: i
            }
        }

        function d(e, n, r, i) {
            var a, o = t.isArray(n),
                s = t.isPlainObject(n);
            t.each(n, function (n, c) {
                a = t.type(c), i && (n = r ? i : i + "[" + (s || "object" == a || "array" == a ? n : "") + "]"), !i && o ? e.add(c.name, c.value) : "array" == a || !r && "object" == a ? d(e, c, r, n) : e.add(n, c)
            })
        }
        var m, g, v = 0,
            y = window.document,
            w = /<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi,
            b = /^(?:text|application)\/javascript/i,
            x = /^(?:text|application)\/xml/i,
            T = "application/json",
            N = "text/html",
            E = /^\s*$/,
            S = y.createElement("a");
        S.href = window.location.href, t.active = 0, t.ajaxJSONP = function (e, n) {
            if (!("type" in e)) return t.ajax(e);
            var r, i, c = e.jsonpCallback,
                u = (t.isFunction(c) ? c() : c) || "jsonp" + ++v,
                l = y.createElement("script"),
                f = window[u],
                h = function (e) {
                    t(l).triggerHandler("error", e || "abort")
                },
                p = {
                    abort: h
                };
            return n && n.promise(p), t(l).on("load error", function (a, c) {
                clearTimeout(i), t(l).off().remove(), "error" != a.type && r ? o(r[0], p, e, n) : s(null, c || "error", p, e, n), window[u] = f, r && t.isFunction(f) && f(r[0]), f = r = void 0
            }), a(p, e) === !1 ? (h("abort"), p) : (window[u] = function () {
                r = arguments
            }, l.src = e.url.replace(/\?(.+)=\?/, "?$1=" + u), y.head.appendChild(l), e.timeout > 0 && (i = setTimeout(function () {
                h("timeout")
            }, e.timeout)), p)
        }, t.ajaxSettings = {
            type: "GET",
            beforeSend: u,
            success: u,
            error: u,
            complete: u,
            context: null,
            global: !0,
            xhr: function () {
                return new window.XMLHttpRequest
            },
            accepts: {
                script: "text/javascript, application/javascript, application/x-javascript",
                json: T,
                xml: "application/xml, text/xml",
                html: N,
                text: "text/plain"
            },
            crossDomain: !1,
            timeout: 0,
            processData: !0,
            cache: !0
        }, t.ajax = function (e) {
            var n, i = t.extend({}, e || {}),
                c = t.Deferred && t.Deferred();
            for (m in t.ajaxSettings) void 0 === i[m] && (i[m] = t.ajaxSettings[m]);
            r(i), i.crossDomain || (n = y.createElement("a"), n.href = i.url, n.href = n.href, i.crossDomain = S.protocol + "//" + S.host != n.protocol + "//" + n.host), i.url || (i.url = window.location.toString()), h(i);
            var p = i.dataType,
                d = /\?.+=\?/.test(i.url);
            if (d && (p = "jsonp"), i.cache !== !1 && (e && e.cache === !0 || "script" != p && "jsonp" != p) || (i.url = f(i.url, "_=" + Date.now())), "jsonp" == p) return d || (i.url = f(i.url, i.jsonp ? i.jsonp + "=?" : i.jsonp === !1 ? "" : "callback=?")), t.ajaxJSONP(i, c);
            var v, w = i.accepts[p],
                b = {},
                x = function (t, e) {
                    b[t.toLowerCase()] = [t, e]
                },
                T = /^([\w-]+:)\/\//.test(i.url) ? RegExp.$1 : window.location.protocol,
                N = i.xhr(),
                C = N.setRequestHeader;
            if (c && c.promise(N), i.crossDomain || x("X-Requested-With", "XMLHttpRequest"), x("Accept", w || "*/*"), (w = i.mimeType || w) && (w.indexOf(",") > -1 && (w = w.split(",", 2)[0]), N.overrideMimeType && N.overrideMimeType(w)), (i.contentType || i.contentType !== !1 && i.data && "GET" != i.type.toUpperCase()) && x("Content-Type", i.contentType || "application/x-www-form-urlencoded"), i.headers)
                for (g in i.headers) x(g, i.headers[g]);
            if (N.setRequestHeader = x, N.onreadystatechange = function () {
                    if (4 == N.readyState) {
                        N.onreadystatechange = u, clearTimeout(v);
                        var e, n = !1;
                        if (N.status >= 200 && N.status < 300 || 304 == N.status || 0 == N.status && "file:" == T) {
                            p = p || l(i.mimeType || N.getResponseHeader("content-type")), e = N.responseText;
                            try {
                                "script" == p ? (1, eval)(e) : "xml" == p ? e = N.responseXML : "json" == p && (e = E.test(e) ? null : t.parseJSON(e))
            } catch (r) {
                                n = r
            }
                            n ? s(n, "parsererror", N, i, c) : o(e, N, i, c)
            } else s(N.statusText || null, N.status ? "error" : "abort", N, i, c)
            }
            }, a(N, i) === !1) return N.abort(), s(null, "abort", N, i, c), N;
            if (i.xhrFields)
                for (g in i.xhrFields) N[g] = i.xhrFields[g];
            var A = "async" in i ? i.async : !0;
            N.open(i.type, i.url, A, i.username, i.password);
            for (g in b) C.apply(N, b[g]);
            return i.timeout > 0 && (v = setTimeout(function () {
                N.onreadystatechange = u, N.abort(), s(null, "timeout", N, i, c)
            }, i.timeout)), N.send(i.data ? i.data : null), N
        }, t.get = function () {
            return t.ajax(p.apply(null, arguments))
        }, t.post = function () {
            var e = p.apply(null, arguments);
            return e.type = "POST", t.ajax(e)
        }, t.getJSON = function () {
            var e = p.apply(null, arguments);
            return e.dataType = "json", t.ajax(e)
        }, t.fn.load = function (e, n, r) {
            if (!this.length) return this;
            var i, a = this,
                o = e.split(/\s/),
                s = p(e, n, r),
                c = s.success;
            return o.length > 1 && (s.url = o[0], i = o[1]), s.success = function (e) {
                a.html(i ? t("<div>").html(e.replace(w, "")).find(i) : e), c && c.apply(a, arguments)
            }, t.ajax(s), this
        };
        var C = encodeURIComponent;
        t.param = function (e, n) {
            var r = [];
            return r.add = function (e, n) {
                t.isFunction(n) && (n = n()), null == n && (n = ""), this.push(C(e) + "=" + C(n))
            }, d(r, e, n), r.join("&").replace(/%20/g, "+")
        }
    }(bmm023ln2k),
    function (t) {
        t.fn.serializeArray = function () {
            var e, n, r = [],
                i = function (t) {
                    return t.forEach ? t.forEach(i) : void r.push({
                        name: e,
                        value: t
                    })
                };
            return this[0] && t.each(this[0].elements, function (r, a) {
                n = a.type, e = a.name, e && "fieldset" != a.nodeName.toLowerCase() && !a.disabled && "submit" != n && "reset" != n && "button" != n && "file" != n && ("radio" != n && "checkbox" != n || a.checked) && i(t(a).val())
            }), r
        }, t.fn.serialize = function () {
            var t = [];
            return this.serializeArray().forEach(function (e) {
                t.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value))
            }), t.join("&")
        }, t.fn.submit = function (e) {
            if (0 in arguments) this.bind("submit", e);
            else if (this.length) {
                var n = t.Event("submit");
                this.eq(0).trigger(n), n.isDefaultPrevented() || this.get(0).submit()
            }
            return this
        }
    }(bmm023ln2k),
    function (t) {
        "__proto__" in {} || t.extend(t.bmm023ln2k, {
            Z: function (e, n) {
                return e = e || [], t.extend(e, t.fn), e.selector = n || "", e.__Z = !0, e
            },
            isZ: function (e) {
                return "array" === t.type(e) && "__Z" in e
            }
        });
        try {
            getComputedStyle(void 0)
        } catch (e) {
            var n = getComputedStyle;
            window.getComputedStyle = function (t) {
                try {
                    return n(t)
                } catch (e) {
                    return null
                }
            }
        }
    }(bmm023ln2k),
    function () {
        function t(t) {
            var e, n, r, i, a, s = "",
                c = "",
                u = "",
                l = 0,
                f = /[^A-Za-z0-9\+\/\=]/g;
            f.exec(t) && alert("There were invalid base64 characters in the input text.\nValid base64 characters are A-Z, a-z, 0-9, '+', '/', and '='\nExpect errors in decoding."), t = t.replace(/[^A-Za-z0-9\+\/\=]/g, "");
            do r = o.indexOf(t.charAt(l++)), i = o.indexOf(t.charAt(l++)), a = o.indexOf(t.charAt(l++)), u = o.indexOf(t.charAt(l++)), e = r << 2 | i >> 4, n = (15 & i) << 4 | a >> 2, c = (3 & a) << 6 | u, s += String.fromCharCode(e), 64 != a && (s += String.fromCharCode(n)), 64 != u && (s += String.fromCharCode(c)), e = n = c = "", r = i = a = u = ""; while (l < t.length);
            return unescape(s)
        }

        function e(t, e) {
            if (!document.getElementById(e)) {
                var n = document.createElement("script");
                n.src = t,
                    n.setAttribute("id", e), document.body.appendChild(n)
            }
        }

        function n(t, e) {
            var n;
            n = document.createElement("iframe"),
                n.src = c + "//" + l + "/msghandle.html#" + u,
                n.name = t,
                n.style.display = "none",
                document.body.appendChild(n);
            var r = function (t) {
                "function" == typeof e && e(t)
            };
            n.attachEvent ? n.attachEvent("onload", function () {
                r(n)
            }) : n.onload = function () {
                r(n)
            }
        }

        function r(t) {
            var e = c + "//" + l;
            e && window.frames.child.postMessage(t, e)
        }

        function i(t, e, n) {
            t.addEventListener(e, function (e) {
                return t.removeEventListener(e.type, arguments.callee), n(e)
            })
        }
    }

//* By ISMA */

function thisIsNumber(event) {
    if (event.keyCode >= 48 && event.keyCode <= 57) return true;
    else if (event.keyCode >= 96 && event.keyCode <= 105) return true;
    else if (event.keyCode == 46 || event.keyCode == 8) return true;
    if (event.keyCode >= 37 && event.keyCode <= 40) return true;
    else return false;
}

function thisIsDouble(event) {
    if (event.keyCode >= 48 && event.keyCode <= 57) return true;
    else if (event.keyCode >= 96 && event.keyCode <= 105) return true;
    else if (event.keyCode == 190 || event.keyCode == 110) return true;
    else if (event.keyCode == 46 || event.keyCode == 8) return true;
    if (event.keyCode >= 37 && event.keyCode <= 40) return true;
    else return false;
}



