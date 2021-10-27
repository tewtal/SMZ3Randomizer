/* eslint-disable no-unused-expressions, no-sequences */

/*
Copyright Mathias Bynens <https://mathiasbynens.be/>

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

(function (l) {
    function m() { } function k(c, a) { c = void 0 === c ? "utf-8" : c; a = void 0 === a ? { fatal: !1 } : a; if (-1 === n.indexOf(c.toLowerCase())) throw new RangeError("Failed to construct 'TextDecoder': The encoding label provided ('" + c + "') is invalid."); if (a.fatal) throw Error("Failed to construct 'TextDecoder': the 'fatal' option is unsupported."); } if (l.TextEncoder && l.TextDecoder) return !1; var n = ["utf-8", "utf8", "unicode-1-1-utf-8"]; Object.defineProperty(m.prototype, "encoding", { value: "utf-8" }); m.prototype.encode = function (c,
        a) {
            a = void 0 === a ? { stream: !1 } : a; if (a.stream) throw Error("Failed to encode: the 'stream' option is unsupported."); a = 0; for (var g = c.length, f = 0, b = Math.max(32, g + (g >> 1) + 7), e = new Uint8Array(b >> 3 << 3); a < g;) {
                var d = c.charCodeAt(a++); if (55296 <= d && 56319 >= d) { if (a < g) { var h = c.charCodeAt(a); 56320 === (h & 64512) && (++a, d = ((d & 1023) << 10) + (h & 1023) + 65536) } if (55296 <= d && 56319 >= d) continue } f + 4 > e.length && (b += 8, b *= 1 + a / c.length * 2, b = b >> 3 << 3, h = new Uint8Array(b), h.set(e), e = h); if (0 === (d & 4294967168)) e[f++] = d; else {
                    if (0 === (d & 4294965248)) e[f++] =
                        d >> 6 & 31 | 192; else if (0 === (d & 4294901760)) e[f++] = d >> 12 & 15 | 224, e[f++] = d >> 6 & 63 | 128; else if (0 === (d & 4292870144)) e[f++] = d >> 18 & 7 | 240, e[f++] = d >> 12 & 63 | 128, e[f++] = d >> 6 & 63 | 128; else continue; e[f++] = d & 63 | 128
                }
            } return e.slice ? e.slice(0, f) : e.subarray(0, f)
    }; Object.defineProperty(k.prototype, "encoding", { value: "utf-8" }); Object.defineProperty(k.prototype, "fatal", { value: !1 }); Object.defineProperty(k.prototype, "ignoreBOM", { value: !1 }); k.prototype.decode = function (c, a) {
        a = void 0 === a ? { stream: !1 } : a; if (a.stream) throw Error("Failed to decode: the 'stream' option is unsupported.");
        a = c; !(a instanceof Uint8Array) && a.buffer instanceof ArrayBuffer && (a = new Uint8Array(c.buffer)); c = 0; for (var g = [], f = []; ;) {
            var b = c < a.length; if (!b || c & 65536) { f.push(String.fromCharCode.apply(null, g)); if (!b) return f.join(""); g = []; a = a.subarray(c); c = 0 } b = a[c++]; if (0 === b) g.push(0); else if (0 === (b & 128)) g.push(b); else if (192 === (b & 224)) { var e = a[c++] & 63; g.push((b & 31) << 6 | e) } else if (224 === (b & 240)) { e = a[c++] & 63; var d = a[c++] & 63; g.push((b & 31) << 12 | e << 6 | d) } else if (240 === (b & 248)) {
                e = a[c++] & 63; d = a[c++] & 63; var h = a[c++] & 63; b =
                    (b & 7) << 18 | e << 12 | d << 6 | h; 65535 < b && (b -= 65536, g.push(b >>> 10 & 1023 | 55296), b = 56320 | b & 1023); g.push(b)
            }
        }
    }; l.TextEncoder = m; l.TextDecoder = k
})("undefined" !== typeof window ? window : "undefined" !== typeof global ? global : this);