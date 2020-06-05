! function(e) {
    "use strict";
    "function" == typeof define && define.amd ? define(e) : window.Elba = e()
}(function() {
    "use strict";

    function n(e) {
        return new RegExp("(^|\\s+)" + e + "(\\s+|$)")
    }
    var i, o, s;

    function e(e, t) {
        (i(e, t) ? s : o)(e, t)
    }
    s = "classList" in document.documentElement ? (i = function(e, t) {
        return e.classList.contains(t)
    }, o = function(e, t) {
        e.classList.add(t)
    }, function(e, t) {
        e.classList.remove(t)
    }) : (i = function(e, t) {
        return n(t).test(e.className)
    }, o = function(e, t) {
        i(e, t) || (e.className = e.className + " " + t)
    }, function(e, t) {
        e.className = e.className.replace(n(t), " ")
    });
    var p = {
        hasClass: i,
        addClass: o,
        removeClass: s,
        toggleClass: e,
        has: i,
        add: o,
        remove: s,
        toggle: e
    };
    window.classie = p, HTMLElement.prototype.wrap = function(e) {
        e.length || (e = [e]);
        for (var t = e.length - 1; 0 <= t; t--) {
            var n = 0 < t ? this.cloneNode(!0) : this,
                i = e[t],
                o = i.parentNode,
                s = i.nextSibling;
            n.appendChild(i), s ? o.insertBefore(n, s) : o.appendChild(n)
        }
    }, Element.prototype.remove = function() {
        this.parentElement.removeChild(this)
    }, NodeList.prototype.remove = window.HTMLCollection.prototype.remove = function() {
        for (var e = 0, t = this.length; e < t; e++) this[e] && this[e].parentElement && this[e].parentElement.removeChild(this[e])
    };
    var f = 1 < window.devicePixelRatio,
        I = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.oRequestAnimationFrame,
        N = window.cancelAnimationFrame || window.mozCancelAnimationFrame,
        t = !!navigator.pointerEnabled || navigator.msPointerEnabled,
        v = (!!("ontouchstart" in window) && navigator.userAgent.indexOf("PhantomJS"), function(e) {
            var t = e.toLowerCase(),
                n = "MS" + e;
            return navigator.msPointerEnabled ? n : t
        }),
        H = {
            easeInSine: [.47, 0, .745, .715],
            easeOutSine: [.39, .575, .565, 1],
            easeInOutSine: [.445, .05, .55, .95],
            easeInQuad: [.55, .085, .68, .53],
            easeOutQuad: [.25, .46, .45, .94],
            easeInOutQuad: [.455, .03, .515, .955],
            easeInCubic: [.55, .055, .675, .19],
            easeOutCubic: [.215, .61, .355, 1],
            easeInOutCubic: [.645, .045, .355, 1],
            easeInQuart: [.895, .03, .685, .22],
            easeOutQuart: [.165, .84, .44, 1],
            easeInOutQuart: [.77, 0, .175, 1],
            easeInQuint: [.755, .05, .855, .06],
            easeOutQuint: [.23, 1, .32, 1],
            easeInOutQuint: [.86, 0, .07, 1],
            easeInExpo: [.95, .05, .795, .035],
            easeOutExpo: [.19, 1, .22, 1],
            easeInOutExpo: [1, 0, 0, 1],
            easeInCirc: [.6, .04, .98, .335],
            easeOutCirc: [.075, .82, .165, 1],
            easeInOutCirc: [.785, .135, .15, .86],
            easeInBack: [.6, -.28, .735, .045],
            easeOutBack: [.175, .885, .32, 1.275],
            easeInOutBack: [.68, -.55, .265, 1.55]
        };

    function a(e, t) {
        this.base = {
            el: e,
            container: null,
            slides: [],
            wrapper: null,
            count: 0,
            source: 0,
            navigation: {
                left: null,
                right: null,
                dots: null
            },
            pointer: 0,
            directionHint: "right",
            resizeTimout: null,
            animated: !1
        }, this.options = function(e, t) {
            for (var n in t) t.hasOwnProperty(n) && (e[n] = t[n]);
            return e
        }(this.defaults, t), this.touchHandler = {
            touchEvents: {
                touchStart: v("PointerDown") + " touchstart",
                touchEnd: v("PointerUp") + " touchend",
                touchMove: v("PointerMove") + " touchmove"
            }
        };
        var a = function(e, t) {
                e.navigation[t] = document.createElement("a"), e.navigation[t].className = "elba-" + t + "-nav", e.navigation[t].innerHtml = t, e.wrapper.appendChild(e.navigation[t])
            },
            r = function(t, e) {
                var n = t.source = 0,
                    i = g(t.container);
                ! function(e, t) {
                    if (e && t)
                        for (var n = e.length, i = 0; i < n && !1 !== t(e[i], i); i++);
                }(e.breakpoints, function(e) {
                    if (e.width <= i && Math.abs(i - e.width) < Math.abs(i - n)) return n = e.width, t.source = e.src, !0
                })
            },
            l = function(e) {
                var t = g(e.container),
                    n = 0;
                e.slides.forEach(function(e) {
                    n += t, e.style.width = t + "px"
                }), e.el.style.width = n + "px"
            },
            u = function(e, t) {
                var n, i, o, s = t.naturalHeight / t.naturalWidth,
                    a = g(e.container),
                    r = void 0 !== (n = e.container) && n ? n.offsetHeight : window.innerHeight || document.documentElement.clientHeight,
                    l = r / a;
                t.width = o = s <= l ? (t.height = i = Math.ceil(r), Math.ceil(r / s)) : (t.height = i = Math.ceil(a * s), Math.ceil(a));
                var c = (a - o) / 2,
                    u = (r - i) / 2;
                t.style.left = Math.ceil(c) + "px", t.style.top = Math.ceil(u) + "px"
            },
            c = function(n, i, e) {
                var o = e || n.pointer,
                    s = n.slides[o];
                (b(s, i.successClass) || b(s, i.errorClass)) && d(n, i, o);
                var t = s.getAttribute(n.source || i.src),
                    a = s.querySelector(".elba-island");
                if (t) {
                    var r = t.split(i.separator),
                        l = r[f && 1 < r.length ? 1 : 0],
                        c = new Image;
                    c.onerror = function() {
                        i.error && i.error(s, "invalid"), s.className = s.className + " " + i.errorClass
                    }, c.onload = function() {
                        var e, t;
                        (a.src = l, u(n, a), p.add(s, i.successClass), i.success && i.success(s), 1 < n.count && (1 === o || 0 === o || o === n.count - 1 || o === n.count - 2)) && (b(e = 1 === o ? n.slides[n.count - 1] : o === n.count - 1 ? n.slides[1] : 0 === o ? n.slides[n.count - 2] : n.slides[0], i.successClass) || ((t = e.querySelector(".elba-island")).src = l, u(n, t), p.add(e, "no-bg-img"), p.add(e, i.successClass)));
                        d(n, i, o)
                    }, c.src = l
                } else d(n, i, o), i.error && i.error(s, "missing"), s.className = s.className + " " + i.errorClass
            };

        function d(e, t, n) {
            if ("right" === e.directionHint) 1 < e.count && n + 1 < e.count - 1 && Math.abs(n + 1 - e.pointer) <= t.preload && c(e, t, ++n);
            else {
                if (!(1 < e.count && 0 < n - 1 && Math.abs(n - 1 - e.pointer) <= t.preload)) return !1;
                c(e, t, --n)
            }
        }
        var h = function(e, t) {
            l(e), e.el.style.left = M(e.container, e.pointer) + "px";
            var n = e.source;
            if (r(e, t), n !== e.source) ! function(e, t) {
                for (var n = e.slides.length, i = 0; i < n; i++) {
                    var o = e.slides[i];
                    o && (p.remove(o, "no-bg-img"), p.remove(o, t.successClass))
                }
            }(e, t), c(e, t);
            else
                for (var i = 0; i < e.slides.length; i++) {
                    var o = e.slides[i];
                    if (o) {
                        var s = o.querySelector(".elba-island");
                        u(e, s)
                    }
                }
        };
        this.init = function() {
            var e, n = this;
            if (function(e, t) {
                    var n = (e.el || document).querySelectorAll(t.selector);
                    e.count = n.length;
                    for (var i = e.count; i--; e.slides.unshift(n[i]));
                }(n.base, n.options), (e = n.base).wrapper = document.createElement("div"), e.wrapper.className = "elba-wrapper", e.wrapper.wrap(e.el), function(e) {
                    if (1 < e.count) {
                        e.pointer = 1;
                        var t = e.slides[e.count - 1].cloneNode(!0);
                        e.el.insertBefore(t, e.el.firstChild), e.slides.unshift(t);
                        var n = e.slides[1].cloneNode(!0);
                        e.el.appendChild(n), e.slides.push(n), e.count += 2
                    }
                    e.slides.forEach(function(e) {
                        var t = document.createElement("img");
                        t.className = "elba-island", e.appendChild(t)
                    })
                }(n.base), n.base.container = function(e, t) {
                    for (; e && e.parentNode;)
                        if ((e = e.parentNode).className === t) return e;
                    return null
                }(n.base.el, n.options.container), 1 < n.base.count && (n.base.el.style.left = -g(n.base.container) + "px"), n.options.navigation && (a(n.base, "left"), a(n.base, "right"), n.base.navigation.left.addEventListener("click", function(e) {
                    e.preventDefault(), n.goTo("left"), n.options.slideshow && n.startSlideshow()
                }, !1), n.base.navigation.right.addEventListener("click", function(e) {
                    e.preventDefault(), n.goTo("right"), n.options.slideshow && n.startSlideshow()
                }, !1)), n.options.dots) {
                ! function(e, t) {
                    var n;
                    e.navigation.dots = [], t ? n = document.getElementById(t) : ((n = document.createElement("div")).className = "elba-dots-ctr", e.wrapper.appendChild(n));
                    for (var i = 1; i < e.count - 1; i++) e.navigation.dots[i] = document.createElement("a"), e.navigation.dots[i].className = "elba-dot", n.appendChild(e.navigation.dots[i])
                }(n.base, n.options.dotsContainer), p.add(n.base.navigation.dots[n.base.pointer], "active-dot");
                for (var t = function(t) {
                        return function() {
                            var e = n.base.navigation.dots[t].getAttribute("data-target");
                            return parseInt(e) === n.base.pointer || (n.goTo(e), n.options.slideshow && n.startSlideshow()), !1
                        }
                    }, i = 1; i < n.base.slides.length - 1; i++) n.base.navigation.dots[i].setAttribute("data-target", i), n.base.navigation.dots[i].addEventListener("click", t(i), !1)
            }
            if (l(n.base), r(n.base, n.options), c(n.base, n.options), window.addEventListener("resize", function() {
                    var e, t;
                    e = n.base, t = n.options, e.resizeTimeout && clearTimeout(e.resizeTimeout), e.resizeTimeout = setTimeout(function() {
                        h(e, t), e.resizeTimeout = null
                    }, 200)
                }, !1), n.bindTouchEvents(), n.options.slideshow) {
                var o, s;
                if (void 0 !== document.hidden ? (o = "hidden", s = "visibilitychange") : void 0 !== document.mozHidden ? (o = "mozHidden", s = "mozvisibilitychange") : void 0 !== document.msHidden ? (o = " msHidden", s = "msvisibilitychange") : void 0 !== document.webkitHidden && (o = "webkitHidden", s = "webkitvisibilitychange"), void 0 !== document[o]) {
                    document.addEventListener(s, function() {
                        document[o] ? n.clearSlideshow() : n.startSlideshow()
                    }, !1)
                }
                n.startSlideshow()
            }
        }, this.goTo = function(e) {
            var t, n, i = this;
            if (!i.base.animated) {
                if ("string" == typeof e && isNaN(e)) {
                    var o = i.base.slides.length;
                    if ("right" === e) {
                        if (i.base.pointer + 1 >= o) return !1;
                        i.base.directionHint = "right", i.base.pointer++, c(i.base, i.options), m(i.base, i.options, "right")
                    } else {
                        if (i.base.pointer - 1 < 0) return !1;
                        i.base.directionHint = "left", i.base.pointer--, c(i.base, i.options), m(i.base, i.options, "left")
                    }
                } else if (!isNaN(e)) {
                    var s = i.base.pointer;
                    i.base.pointer = parseInt(e), i.base.pointer > s ? (i.base.directionHint = "right", c(i.base, i.options), m(i.base, i.options, "right")) : (i.base.directionHint = "left", c(i.base, i.options), m(i.base, i.options, "left"))
                }
                i.options.dots && ((t = i.base).navigation.dots.forEach(function(e) {
                    e && p.remove(e, "active-dot")
                }), n = t.pointer === t.slides.length - 1 ? 1 : 0 === t.pointer ? t.slides.length - 2 : t.pointer, t.navigation.dots[n] && p.add(t.navigation.dots[n], "active-dot"))
            }
        }, this.bindTouchEvents = function() {
            var o, s, a, r, n, l = this,
                i = function(e) {
                    return e.targetTouches ? e.targetTouches[0] : e
                };
            w(l.base.el, l.touchHandler.touchEvents.touchStart + " mousedown", function(e) {
                "mousedown" === e.type && e.preventDefault();
                var t = i(e);
                a = o = t.pageX, r = s = t.pageY, !0, 0, clearTimeout(n), n = setTimeout(function() {
                    0
                }, 200)
            }), w(l.base.el, l.touchHandler.touchEvents.touchEnd + " mouseup", function() {
                var e = [],
                    t = r - s,
                    n = a - o;
                if (!1, n <= -80 && (e.push("swiperight"), l.goTo("left")), 80 <= n && (e.push("swipeleft"), l.goTo("right")), t <= -80 && e.push("swipedown"), 80 <= t && e.push("swipeup"), e.length)
                    for (var i = 0; i < e.length; i++) e[i];
                l.options.slideshow && l.startSlideshow()
            }), w(l.base.el, l.touchHandler.touchEvents.touchMove + " mousemove", function(e) {
                var t = i(e);
                o = t.pageX, s = t.pageY
            })
        }, this.init()
    }

    function m(o, e, t) {
        var s = o.el;
        if (o.animated) return !1;
        o.animated = !0;
        var n = M(o.container, o.pointer),
            a = o.slides.length,
            r = T(s.style.left),
            l = Math.abs(r - n);
        "right" === t && (l = -l);
        var i, c, u, d, h, p, f, v, m, g, b = e.duration,
            w = 1e3 / 60 / b / 4,
            E = (i = H[e.easing], c = w, u = i[0], d = i[1], h = i[2], p = i[3], f = c, v = function(e) {
                var t = 1 - e;
                return 3 * t * t * e * u + 3 * t * e * e * h + e * e * e
            }, m = function(e) {
                var t = 1 - e;
                return 3 * t * t * e * d + 3 * t * e * e * p + e * e * e
            }, function(e) {
                var t, n, i, o, s, a, r, l, c = e;
                for (i = c, a = 0; a < 8; a++) {
                    if (o = v(i) - c, Math.abs(o) < f) return m(i);
                    if (s = 3 * (2 * ((r = i) - 1) * r + (l = 1 - r) * l) * u + 3 * (-r * r * r + 2 * l * r) * h, Math.abs(s) < 1e-6) break;
                    i -= o / s
                }
                if (n = 1, (i = c) < (t = 0)) return m(t);
                if (n < i) return m(n);
                for (; t < n;) {
                    if (o = v(i), Math.abs(o - c) < f) return m(i);
                    o < c ? t = i : n = i, i = .5 * (n - t) + t
                }
                return m(i)
            }),
            C = null;
        if (I && N) g = I(function e(t) {
            null === C && (C = t);
            var n = (t - C) / b;
            1 < n && (n = 1);
            var i = E(n).toFixed(6);
            S(s, i, r, l), 1 === n ? ((n = 1) < a && (o.pointer === a - 1 ? (o.pointer = 1, s.style.left = M(o.container, o.pointer) + "px") : 0 === o.pointer && (o.pointer = a - 2, s.style.left = M(o.container, o.pointer) + "px")), N(g), C = null, o.animated = !1) : I(e)
        });
        else var y = setInterval(function() {
            null === C && (C = new Date);
            var e = (new Date - C) / b;
            1 < e && (e = 1);
            var t = E(e).toFixed(6);
            S(s, t, r, l), 1 === e && ((e = 1) < a && (o.pointer === a - 1 ? (o.pointer = 1, s.style.left = M(o.container, o.pointer) + "px") : 0 === o.pointer && (o.pointer = a - 2, s.style.left = M(o.container, o.pointer) + "px")), clearInterval(y), C = null, o.animated = !1)
        }, 25)
    }

    function S(e, t, n, i) {
        var o = n + i * t;
        e.style.left = Math.ceil(o) + "px"
    }

    function g(e) {
        return void 0 !== e && e ? e.offsetWidth : window.innerWidth || document.documentElement.clientWidth
    }

    function T(e) {
        return e ? parseInt(e, 10) : 0
    }

    function M(e, t) {
        return T(-g(e) * t)
    }

    function b(e, t) {
        return p.has(e, t)
    }

    function w(e, t, n) {
        for (var i = t.split(" "), o = i.length; o--;) e.addEventListener(i[o], n, !1)
    }
    return a.prototype.defaults = {
        selector: ".elba",
        separator: "|",
        breakpoints: !1,
        successClass: "elba-loaded",
        errorClass: "elba-error",
        container: "elba-wrapper",
        src: "data-src",
        error: !1,
        success: !1,
        duration: 1e3,
        easing: "easeInOutCubic",
        navigation: !0,
        dots: !0,
        dotsContainer: !1,
        slideshow: 5e3,
        preload: 1
    }, a.prototype.startSlideshow = function() {
        var t = this;
        1 < t.base.slides.length && (t.slideshow && clearInterval(t.slideshow), t.slideshow = setInterval(function() {
            if (! function(e) {
                    "function" == typeof jQuery && e instanceof jQuery && (e = e[0]);
                    var t = e.getBoundingClientRect();
                    return 0 <= t.top && 0 <= t.left && t.bottom <= (window.innerHeight || document.documentElement.clientHeight) && t.right <= (window.innerWidth || document.documentElement.clientWidth)
                }(t.base.container)) return !1;
            var e = t.base.slides[t.base.pointer + 1];
            e && (p.has(e, t.options.successClass) || p.has(e, t.options.errorClass)) && t.goTo("right")
        }, t.options.slideshow))
    }, a.prototype.clearSlideshow = function() {
        this.slideshow && clearInterval(this.slideshow)
    }, a.prototype.stopSlideshow = function() {
        this.slideshow && clearInterval(this.slideshow), this.options.slideshow = 0
    }, a.prototype.getCurrent = function() {
        return this.base.pointer
    }, a
});