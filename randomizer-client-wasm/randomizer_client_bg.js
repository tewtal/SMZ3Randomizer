import { bytes_literal } from './snippets/wasm-streams-8c20110b5d812e48/inline0.js';
import * as wasm from './randomizer_client_bg.wasm';

const heap = new Array(32).fill(undefined);

heap.push(undefined, null, true, false);

function getObject(idx) { return heap[idx]; }

let heap_next = heap.length;

function dropObject(idx) {
    if (idx < 36) return;
    heap[idx] = heap_next;
    heap_next = idx;
}

function takeObject(idx) {
    const ret = getObject(idx);
    dropObject(idx);
    return ret;
}

function addHeapObject(obj) {
    if (heap_next === heap.length) heap.push(heap.length + 1);
    const idx = heap_next;
    heap_next = heap[idx];

    heap[idx] = obj;
    return idx;
}

const lTextDecoder = typeof TextDecoder === 'undefined' ? (0, module.require)('util').TextDecoder : TextDecoder;

let cachedTextDecoder = new lTextDecoder('utf-8', { ignoreBOM: true, fatal: true });

cachedTextDecoder.decode();

let cachegetUint8Memory0 = null;
function getUint8Memory0() {
    if (cachegetUint8Memory0 === null || cachegetUint8Memory0.buffer !== wasm.memory.buffer) {
        cachegetUint8Memory0 = new Uint8Array(wasm.memory.buffer);
    }
    return cachegetUint8Memory0;
}

function getStringFromWasm0(ptr, len) {
    return cachedTextDecoder.decode(getUint8Memory0().subarray(ptr, ptr + len));
}

let WASM_VECTOR_LEN = 0;

const lTextEncoder = typeof TextEncoder === 'undefined' ? (0, module.require)('util').TextEncoder : TextEncoder;

let cachedTextEncoder = new lTextEncoder('utf-8');

const encodeString = (typeof cachedTextEncoder.encodeInto === 'function'
    ? function (arg, view) {
    return cachedTextEncoder.encodeInto(arg, view);
}
    : function (arg, view) {
    const buf = cachedTextEncoder.encode(arg);
    view.set(buf);
    return {
        read: arg.length,
        written: buf.length
    };
});

function passStringToWasm0(arg, malloc, realloc) {

    if (realloc === undefined) {
        const buf = cachedTextEncoder.encode(arg);
        const ptr = malloc(buf.length);
        getUint8Memory0().subarray(ptr, ptr + buf.length).set(buf);
        WASM_VECTOR_LEN = buf.length;
        return ptr;
    }

    let len = arg.length;
    let ptr = malloc(len);

    const mem = getUint8Memory0();

    let offset = 0;

    for (; offset < len; offset++) {
        const code = arg.charCodeAt(offset);
        if (code > 0x7F) break;
        mem[ptr + offset] = code;
    }

    if (offset !== len) {
        if (offset !== 0) {
            arg = arg.slice(offset);
        }
        ptr = realloc(ptr, len, len = offset + arg.length * 3);
        const view = getUint8Memory0().subarray(ptr + offset, ptr + len);
        const ret = encodeString(arg, view);

        offset += ret.written;
    }

    WASM_VECTOR_LEN = offset;
    return ptr;
}

function isLikeNone(x) {
    return x === undefined || x === null;
}

let cachegetInt32Memory0 = null;
function getInt32Memory0() {
    if (cachegetInt32Memory0 === null || cachegetInt32Memory0.buffer !== wasm.memory.buffer) {
        cachegetInt32Memory0 = new Int32Array(wasm.memory.buffer);
    }
    return cachegetInt32Memory0;
}

function debugString(val) {
    // primitive types
    const type = typeof val;
    if (type == 'number' || type == 'boolean' || val == null) {
        return  `${val}`;
    }
    if (type == 'string') {
        return `"${val}"`;
    }
    if (type == 'symbol') {
        const description = val.description;
        if (description == null) {
            return 'Symbol';
        } else {
            return `Symbol(${description})`;
        }
    }
    if (type == 'function') {
        const name = val.name;
        if (typeof name == 'string' && name.length > 0) {
            return `Function(${name})`;
        } else {
            return 'Function';
        }
    }
    // objects
    if (Array.isArray(val)) {
        const length = val.length;
        let debug = '[';
        if (length > 0) {
            debug += debugString(val[0]);
        }
        for(let i = 1; i < length; i++) {
            debug += ', ' + debugString(val[i]);
        }
        debug += ']';
        return debug;
    }
    // Test for built-in
    const builtInMatches = /\[object ([^\]]+)\]/.exec(toString.call(val));
    let className;
    if (builtInMatches.length > 1) {
        className = builtInMatches[1];
    } else {
        // Failed to match the standard '[object ClassName]'
        return toString.call(val);
    }
    if (className == 'Object') {
        // we're a user defined class or Object
        // JSON.stringify avoids problems with cycles, and is generally much
        // easier than looping through ownProperties of `val`.
        try {
            return 'Object(' + JSON.stringify(val) + ')';
        } catch (_) {
            return 'Object';
        }
    }
    // errors
    if (val instanceof Error) {
        return `${val.name}: ${val.message}\n${val.stack}`;
    }
    // TODO we could test for more things here, like `Set`s and `Map`s.
    return className;
}

function makeMutClosure(arg0, arg1, dtor, f) {
    const state = { a: arg0, b: arg1, cnt: 1, dtor };
    const real = (...args) => {
        // First up with a closure we increment the internal reference
        // count. This ensures that the Rust closure environment won't
        // be deallocated while we're invoking it.
        state.cnt++;
        const a = state.a;
        state.a = 0;
        try {
            return f(a, state.b, ...args);
        } finally {
            if (--state.cnt === 0) {
                wasm.__wbindgen_export_2.get(state.dtor)(a, state.b);

            } else {
                state.a = a;
            }
        }
    };
    real.original = state;

    return real;
}
function __wbg_adapter_30(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__h6c2eeb6a95afb3a3(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_33(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__h7e43481001c160ad(arg0, arg1, addHeapObject(arg2));
}

function __wbg_adapter_36(arg0, arg1) {
    wasm._dyn_core__ops__function__FnMut_____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__hfbc062f85ccba163(arg0, arg1);
}

function __wbg_adapter_39(arg0, arg1, arg2) {
    wasm._dyn_core__ops__function__FnMut__A____Output___R_as_wasm_bindgen__closure__WasmClosure___describe__invoke__h25db5ebf9fa5f9ce(arg0, arg1, addHeapObject(arg2));
}

let cachegetUint32Memory0 = null;
function getUint32Memory0() {
    if (cachegetUint32Memory0 === null || cachegetUint32Memory0.buffer !== wasm.memory.buffer) {
        cachegetUint32Memory0 = new Uint32Array(wasm.memory.buffer);
    }
    return cachegetUint32Memory0;
}

function passArray32ToWasm0(arg, malloc) {
    const ptr = malloc(arg.length * 4);
    getUint32Memory0().set(arg, ptr / 4);
    WASM_VECTOR_LEN = arg.length;
    return ptr;
}

function passArrayJsValueToWasm0(array, malloc) {
    const ptr = malloc(array.length * 4);
    const mem = getUint32Memory0();
    for (let i = 0; i < array.length; i++) {
        mem[ptr / 4 + i] = addHeapObject(array[i]);
    }
    WASM_VECTOR_LEN = array.length;
    return ptr;
}

function handleError(f, args) {
    try {
        return f.apply(this, args);
    } catch (e) {
        wasm.__wbindgen_exn_store(addHeapObject(e));
    }
}
function __wbg_adapter_101(arg0, arg1, arg2, arg3) {
    wasm.wasm_bindgen__convert__closures__invoke2_mut__h6de85b57e9069a50(arg0, arg1, addHeapObject(arg2), addHeapObject(arg3));
}

function getArrayU8FromWasm0(ptr, len) {
    return getUint8Memory0().subarray(ptr / 1, ptr / 1 + len);
}
/**
*/
export const Message = Object.freeze({ ConsoleDisconnected:0,"0":"ConsoleDisconnected",ConsoleReconnecting:1,"1":"ConsoleReconnecting",ConsoleConnected:2,"2":"ConsoleConnected",ConsoleError:3,"3":"ConsoleError",GameState:4,"4":"GameState",ItemFound:5,"5":"ItemFound",ItemReceived:6,"6":"ItemReceived",ItemsConfirmed:7,"7":"ItemsConfirmed", });
/**
*/
export class ConsoleInterface {

    static __wrap(ptr) {
        const obj = Object.create(ConsoleInterface.prototype);
        obj.ptr = ptr;

        return obj;
    }

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_consoleinterface_free(ptr);
    }
    /**
    */
    static init() {
        wasm.consoleinterface_init();
    }
    /**
    * @param {string} proto
    * @param {string | undefined} uri
    */
    constructor(proto, uri) {
        var ptr0 = passStringToWasm0(proto, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ptr1 = isLikeNone(uri) ? 0 : passStringToWasm0(uri, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len1 = WASM_VECTOR_LEN;
        var ret = wasm.consoleinterface_new(ptr0, len0, ptr1, len1);
        return ConsoleInterface.__wrap(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    connect() {
        var ret = wasm.consoleinterface_connect(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    disconnect() {
        var ret = wasm.consoleinterface_disconnect(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    list_devices() {
        var ret = wasm.consoleinterface_list_devices(this.ptr);
        return takeObject(ret);
    }
    /**
    * @param {string} device
    * @param {number} address
    * @param {number} size
    * @returns {Promise<any>}
    */
    read(device, address, size) {
        var ptr0 = passStringToWasm0(device, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.consoleinterface_read(this.ptr, ptr0, len0, address, size);
        return takeObject(ret);
    }
    /**
    * @param {string} device
    * @param {Uint32Array} address_info
    * @returns {Promise<any>}
    */
    read_multi(device, address_info) {
        var ptr0 = passStringToWasm0(device, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ptr1 = passArray32ToWasm0(address_info, wasm.__wbindgen_malloc);
        var len1 = WASM_VECTOR_LEN;
        var ret = wasm.consoleinterface_read_multi(this.ptr, ptr0, len0, ptr1, len1);
        return takeObject(ret);
    }
    /**
    * @param {string} device
    * @param {number} address
    * @param {Uint8Array} data
    * @returns {Promise<any>}
    */
    write(device, address, data) {
        var ptr0 = passStringToWasm0(device, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.consoleinterface_write(this.ptr, ptr0, len0, address, addHeapObject(data));
        return takeObject(ret);
    }
    /**
    * @param {string} device
    * @param {Uint32Array} addresses
    * @param {(Uint8Array)[]} data
    * @returns {Promise<any>}
    */
    write_multi(device, addresses, data) {
        var ptr0 = passStringToWasm0(device, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ptr1 = passArray32ToWasm0(addresses, wasm.__wbindgen_malloc);
        var len1 = WASM_VECTOR_LEN;
        var ptr2 = passArrayJsValueToWasm0(data, wasm.__wbindgen_malloc);
        var len2 = WASM_VECTOR_LEN;
        var ret = wasm.consoleinterface_write_multi(this.ptr, ptr0, len0, ptr1, len1, ptr2, len2);
        return takeObject(ret);
    }
}
/**
*/
export class IntoUnderlyingByteSource {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_intounderlyingbytesource_free(ptr);
    }
    /**
    * @returns {any}
    */
    get type() {
        var ret = wasm.intounderlyingbytesource_type(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {number}
    */
    get autoAllocateChunkSize() {
        var ret = wasm.intounderlyingbytesource_autoAllocateChunkSize(this.ptr);
        return ret >>> 0;
    }
    /**
    * @param {any} controller
    */
    start(controller) {
        wasm.intounderlyingbytesource_start(this.ptr, addHeapObject(controller));
    }
    /**
    * @param {any} controller
    * @returns {Promise<any>}
    */
    pull(controller) {
        var ret = wasm.intounderlyingbytesource_pull(this.ptr, addHeapObject(controller));
        return takeObject(ret);
    }
    /**
    */
    cancel() {
        const ptr = this.__destroy_into_raw();
        wasm.intounderlyingbytesource_cancel(ptr);
    }
}
/**
*/
export class IntoUnderlyingSink {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_intounderlyingsink_free(ptr);
    }
    /**
    * @param {any} chunk
    * @returns {Promise<any>}
    */
    write(chunk) {
        var ret = wasm.intounderlyingsink_write(this.ptr, addHeapObject(chunk));
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    close() {
        const ptr = this.__destroy_into_raw();
        var ret = wasm.intounderlyingsink_close(ptr);
        return takeObject(ret);
    }
    /**
    * @param {any} reason
    * @returns {Promise<any>}
    */
    abort(reason) {
        const ptr = this.__destroy_into_raw();
        var ret = wasm.intounderlyingsink_abort(ptr, addHeapObject(reason));
        return takeObject(ret);
    }
}
/**
*/
export class IntoUnderlyingSource {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_intounderlyingsource_free(ptr);
    }
    /**
    * @param {any} controller
    * @returns {Promise<any>}
    */
    pull(controller) {
        var ret = wasm.intounderlyingsource_pull(this.ptr, addHeapObject(controller));
        return takeObject(ret);
    }
    /**
    */
    cancel() {
        const ptr = this.__destroy_into_raw();
        wasm.intounderlyingsource_cancel(ptr);
    }
}
/**
* Raw options for [`pipeTo()`](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream/pipeTo).
*/
export class PipeOptions {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_pipeoptions_free(ptr);
    }
    /**
    * @returns {boolean}
    */
    get preventClose() {
        var ret = wasm.pipeoptions_preventClose(this.ptr);
        return ret !== 0;
    }
    /**
    * @returns {boolean}
    */
    get preventCancel() {
        var ret = wasm.pipeoptions_preventCancel(this.ptr);
        return ret !== 0;
    }
    /**
    * @returns {boolean}
    */
    get preventAbort() {
        var ret = wasm.pipeoptions_preventAbort(this.ptr);
        return ret !== 0;
    }
    /**
    * @returns {AbortSignal | undefined}
    */
    get signal() {
        var ret = wasm.pipeoptions_signal(this.ptr);
        return takeObject(ret);
    }
}
/**
*/
export class QueuingStrategy {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_queuingstrategy_free(ptr);
    }
    /**
    * @returns {number}
    */
    get highWaterMark() {
        var ret = wasm.queuingstrategy_highWaterMark(this.ptr);
        return ret;
    }
}
/**
*/
export class RandomizerClient {

    static __wrap(ptr) {
        const obj = Object.create(RandomizerClient.prototype);
        obj.ptr = ptr;

        return obj;
    }

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_randomizerclient_free(ptr);
    }
    /**
    */
    static init() {
        wasm.randomizerclient_init();
    }
    /**
    * @param {string} session_uri
    * @param {string} session_guid
    * @param {Function} callback
    */
    constructor(session_uri, session_guid, callback) {
        var ptr0 = passStringToWasm0(session_uri, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ptr1 = passStringToWasm0(session_guid, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len1 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_new(ptr0, len0, ptr1, len1, addHeapObject(callback));
        return RandomizerClient.__wrap(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    initialize() {
        var ret = wasm.randomizerclient_initialize(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    get_session_data() {
        var ret = wasm.randomizerclient_get_session_data(this.ptr);
        return takeObject(ret);
    }
    /**
    * @param {number} world_id
    * @returns {Promise<any>}
    */
    register_player(world_id) {
        var ret = wasm.randomizerclient_register_player(this.ptr, world_id);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    unregister_player() {
        var ret = wasm.randomizerclient_unregister_player(this.ptr);
        return takeObject(ret);
    }
    /**
    * @param {string} client_guid
    * @returns {Promise<any>}
    */
    login_player(client_guid) {
        var ptr0 = passStringToWasm0(client_guid, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_login_player(this.ptr, ptr0, len0);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    get_client_data() {
        var ret = wasm.randomizerclient_get_client_data(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    get_patch() {
        var ret = wasm.randomizerclient_get_patch(this.ptr);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    list_devices() {
        var ret = wasm.randomizerclient_list_devices(this.ptr);
        return takeObject(ret);
    }
    /**
    * @param {Int32Array} event_types
    * @param {number | undefined} from_event_id
    * @param {number | undefined} to_event_id
    * @param {number | undefined} from_world_id
    * @param {number | undefined} to_world_id
    * @returns {Promise<any>}
    */
    get_events(event_types, from_event_id, to_event_id, from_world_id, to_world_id) {
        var ptr0 = passArray32ToWasm0(event_types, wasm.__wbindgen_malloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_get_events(this.ptr, ptr0, len0, !isLikeNone(from_event_id), isLikeNone(from_event_id) ? 0 : from_event_id, !isLikeNone(to_event_id), isLikeNone(to_event_id) ? 0 : to_event_id, !isLikeNone(from_world_id), isLikeNone(from_world_id) ? 0 : from_world_id, !isLikeNone(to_world_id), isLikeNone(to_world_id) ? 0 : to_world_id);
        return takeObject(ret);
    }
    /**
    * @param {number} from_event_id
    * @param {Int32Array} event_types
    * @returns {Promise<any>}
    */
    get_report(from_event_id, event_types) {
        var ptr0 = passArray32ToWasm0(event_types, wasm.__wbindgen_malloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_get_report(this.ptr, from_event_id, ptr0, len0);
        return takeObject(ret);
    }
    /**
    * @param {number} event_type
    * @param {number} to_world_id
    * @param {number} item_id
    * @param {number} item_location
    * @param {number} sequence_num
    * @param {boolean} confirmed
    * @param {string} message
    * @returns {Promise<any>}
    */
    send_event(event_type, to_world_id, item_id, item_location, sequence_num, confirmed, message) {
        var ptr0 = passStringToWasm0(message, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_send_event(this.ptr, event_type, to_world_id, item_id, item_location, sequence_num, confirmed, ptr0, len0);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    forfeit() {
        var ret = wasm.randomizerclient_forfeit(this.ptr);
        return takeObject(ret);
    }
    /**
    * @param {string} device
    * @returns {Promise<any>}
    */
    start(device) {
        var ptr0 = passStringToWasm0(device, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
        var len0 = WASM_VECTOR_LEN;
        var ret = wasm.randomizerclient_start(this.ptr, ptr0, len0);
        return takeObject(ret);
    }
    /**
    * @returns {Promise<any>}
    */
    update() {
        var ret = wasm.randomizerclient_update(this.ptr);
        return takeObject(ret);
    }
}
/**
* Raw options for [`getReader()`](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream/getReader).
*/
export class ReadableStreamGetReaderOptions {

    __destroy_into_raw() {
        const ptr = this.ptr;
        this.ptr = 0;

        return ptr;
    }

    free() {
        const ptr = this.__destroy_into_raw();
        wasm.__wbg_readablestreamgetreaderoptions_free(ptr);
    }
    /**
    * @returns {any}
    */
    get mode() {
        var ret = wasm.readablestreamgetreaderoptions_mode(this.ptr);
        return takeObject(ret);
    }
}

export function __wbindgen_object_drop_ref(arg0) {
    takeObject(arg0);
};

export function __wbg_new_16f24b0728c5e67b() {
    var ret = new Array();
    return addHeapObject(ret);
};

export function __wbg_push_a72df856079e6930(arg0, arg1) {
    var ret = getObject(arg0).push(getObject(arg1));
    return ret;
};

export function __wbindgen_number_new(arg0) {
    var ret = arg0;
    return addHeapObject(ret);
};

export function __wbg_call_471669b9b42539e5() { return handleError(function (arg0, arg1, arg2, arg3) {
    var ret = getObject(arg0).call(getObject(arg1), getObject(arg2), getObject(arg3));
    return addHeapObject(ret);
}, arguments) };

export function __wbg_new_d3138911a89329b0() {
    var ret = new Object();
    return addHeapObject(ret);
};

export function __wbg_set_fbb49ad265f9dee8(arg0, arg1, arg2) {
    getObject(arg0)[takeObject(arg1)] = takeObject(arg2);
};

export function __wbg_new_4beacc9c71572250(arg0, arg1) {
    try {
        var state0 = {a: arg0, b: arg1};
        var cb0 = (arg0, arg1) => {
            const a = state0.a;
            state0.a = 0;
            try {
                return __wbg_adapter_101(a, state0.b, arg0, arg1);
            } finally {
                state0.a = a;
            }
        };
        var ret = new Promise(cb0);
        return addHeapObject(ret);
    } finally {
        state0.a = state0.b = 0;
    }
};

export function __wbindgen_string_new(arg0, arg1) {
    var ret = getStringFromWasm0(arg0, arg1);
    return addHeapObject(ret);
};

export function __wbindgen_cb_drop(arg0) {
    const obj = takeObject(arg0).original;
    if (obj.cnt-- == 1) {
        obj.a = 0;
        return true;
    }
    var ret = false;
    return ret;
};

export function __wbg_send_2bad75269a8cc966() { return handleError(function (arg0, arg1, arg2) {
    getObject(arg0).send(getStringFromWasm0(arg1, arg2));
}, arguments) };

export function __wbg_send_f8c73b0122d29309() { return handleError(function (arg0, arg1, arg2) {
    getObject(arg0).send(getArrayU8FromWasm0(arg1, arg2));
}, arguments) };

export function __wbg_new_9d38005ad72b669a() { return handleError(function (arg0, arg1) {
    var ret = new WebSocket(getStringFromWasm0(arg0, arg1));
    return addHeapObject(ret);
}, arguments) };

export function __wbg_newwithstrsequence_8de2522d41e8a01c() { return handleError(function (arg0, arg1, arg2) {
    var ret = new WebSocket(getStringFromWasm0(arg0, arg1), getObject(arg2));
    return addHeapObject(ret);
}, arguments) };

export function __wbg_code_8b1cd95c3142dec4(arg0) {
    var ret = getObject(arg0).code;
    return ret;
};

export function __wbg_setbinaryType_ffc26541bf7058b2(arg0, arg1) {
    getObject(arg0).binaryType = takeObject(arg1);
};

export function __wbg_code_32a97f32b2304d71(arg0) {
    var ret = getObject(arg0).code;
    return ret;
};

export function __wbg_reason_51cb1f322946c0a8(arg0, arg1) {
    var ret = getObject(arg1).reason;
    var ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    var len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
};

export function __wbg_wasClean_105e50a18f6fe524(arg0) {
    var ret = getObject(arg0).wasClean;
    return ret;
};

export function __wbg_new_e3b800e570795b3c(arg0) {
    var ret = new Uint8Array(getObject(arg0));
    return addHeapObject(ret);
};

export function __wbg_new_226d109446575877() { return handleError(function () {
    var ret = new Headers();
    return addHeapObject(ret);
}, arguments) };

export function __wbg_newwithstrandinit_c07f0662ece15bc6() { return handleError(function (arg0, arg1, arg2) {
    var ret = new Request(getStringFromWasm0(arg0, arg1), getObject(arg2));
    return addHeapObject(ret);
}, arguments) };

export function __wbindgen_object_clone_ref(arg0) {
    var ret = getObject(arg0);
    return addHeapObject(ret);
};

export function __wbg_instanceof_Window_434ce1849eb4e0fc(arg0) {
    var ret = getObject(arg0) instanceof Window;
    return ret;
};

export function __wbg_fetch_427498e0ccea81f4(arg0, arg1) {
    var ret = getObject(arg0).fetch(getObject(arg1));
    return addHeapObject(ret);
};

export function __wbg_instanceof_Response_ea36d565358a42f7(arg0) {
    var ret = getObject(arg0) instanceof Response;
    return ret;
};

export function __wbg_status_3a55bb50e744b834(arg0) {
    var ret = getObject(arg0).status;
    return ret;
};

export function __wbg_headers_e4204c6775f7b3b4(arg0) {
    var ret = getObject(arg0).headers;
    return addHeapObject(ret);
};

export function __wbg_iterator_4b9cedbeda0c0e30() {
    var ret = Symbol.iterator;
    return addHeapObject(ret);
};

export function __wbg_get_8bbb82393651dd9c() { return handleError(function (arg0, arg1) {
    var ret = Reflect.get(getObject(arg0), getObject(arg1));
    return addHeapObject(ret);
}, arguments) };

export function __wbindgen_is_object(arg0) {
    const val = getObject(arg0);
    var ret = typeof(val) === 'object' && val !== null;
    return ret;
};

export function __wbg_next_c7a2a6b012059a5e(arg0) {
    var ret = getObject(arg0).next;
    return addHeapObject(ret);
};

export function __wbg_next_dd1a890d37e38d73() { return handleError(function (arg0) {
    var ret = getObject(arg0).next();
    return addHeapObject(ret);
}, arguments) };

export function __wbg_done_982b1c7ac0cbc69d(arg0) {
    var ret = getObject(arg0).done;
    return ret;
};

export function __wbg_value_2def2d1fb38b02cd(arg0) {
    var ret = getObject(arg0).value;
    return addHeapObject(ret);
};

export function __wbg_body_72844214d9261339(arg0) {
    var ret = getObject(arg0).body;
    return isLikeNone(ret) ? 0 : addHeapObject(ret);
};

export function __wbg_getReader_268c9e9795240516() { return handleError(function (arg0) {
    var ret = getObject(arg0).getReader();
    return addHeapObject(ret);
}, arguments) };

export function __wbg_get_f45dff51f52d7222(arg0, arg1) {
    var ret = getObject(arg0)[arg1 >>> 0];
    return addHeapObject(ret);
};

export function __wbindgen_string_get(arg0, arg1) {
    const obj = getObject(arg1);
    var ret = typeof(obj) === 'string' ? obj : undefined;
    var ptr0 = isLikeNone(ret) ? 0 : passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    var len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
};

export function __wbg_cancel_2175e969af9d33a2(arg0) {
    var ret = getObject(arg0).cancel();
    return addHeapObject(ret);
};

export function __wbg_catch_b154329609256e1b(arg0, arg1) {
    var ret = getObject(arg0).catch(getObject(arg1));
    return addHeapObject(ret);
};

export function __wbg_read_3bfd847961db5cbf(arg0) {
    var ret = getObject(arg0).read();
    return addHeapObject(ret);
};

export function __wbg_done_9c6332fdc0070179(arg0) {
    var ret = getObject(arg0).done;
    return ret;
};

export function __wbg_value_7cdf3071300e2ffd(arg0) {
    var ret = getObject(arg0).value;
    return addHeapObject(ret);
};

export function __wbg_length_30803400a8f15c59(arg0) {
    var ret = getObject(arg0).length;
    return ret;
};

export function __wbindgen_is_function(arg0) {
    var ret = typeof(getObject(arg0)) === 'function';
    return ret;
};

export function __wbg_call_89558c3e96703ca1() { return handleError(function (arg0, arg1) {
    var ret = getObject(arg0).call(getObject(arg1));
    return addHeapObject(ret);
}, arguments) };

export function __wbg_self_e23d74ae45fb17d1() { return handleError(function () {
    var ret = self.self;
    return addHeapObject(ret);
}, arguments) };

export function __wbg_window_b4be7f48b24ac56e() { return handleError(function () {
    var ret = window.window;
    return addHeapObject(ret);
}, arguments) };

export function __wbg_globalThis_d61b1f48a57191ae() { return handleError(function () {
    var ret = globalThis.globalThis;
    return addHeapObject(ret);
}, arguments) };

export function __wbg_global_e7669da72fd7f239() { return handleError(function () {
    var ret = global.global;
    return addHeapObject(ret);
}, arguments) };

export function __wbindgen_is_undefined(arg0) {
    var ret = getObject(arg0) === undefined;
    return ret;
};

export function __wbg_newnoargs_f579424187aa1717(arg0, arg1) {
    var ret = new Function(getStringFromWasm0(arg0, arg1));
    return addHeapObject(ret);
};

export function __wbg_call_94697a95cb7e239c() { return handleError(function (arg0, arg1, arg2) {
    var ret = getObject(arg0).call(getObject(arg1), getObject(arg2));
    return addHeapObject(ret);
}, arguments) };

export function __wbg_set_c42875065132a932() { return handleError(function (arg0, arg1, arg2) {
    var ret = Reflect.set(getObject(arg0), getObject(arg1), getObject(arg2));
    return ret;
}, arguments) };

export function __wbindgen_memory() {
    var ret = wasm.memory;
    return addHeapObject(ret);
};

export function __wbg_buffer_5e74a88a1424a2e0(arg0) {
    var ret = getObject(arg0).buffer;
    return addHeapObject(ret);
};

export function __wbg_newwithbyteoffsetandlength_278ec7532799393a(arg0, arg1, arg2) {
    var ret = new Uint8Array(getObject(arg0), arg1 >>> 0, arg2 >>> 0);
    return addHeapObject(ret);
};

export function __wbg_set_5b8081e9d002f0df(arg0, arg1, arg2) {
    getObject(arg0).set(getObject(arg1), arg2 >>> 0);
};

export function __wbindgen_debug_string(arg0, arg1) {
    var ret = debugString(getObject(arg1));
    var ptr0 = passStringToWasm0(ret, wasm.__wbindgen_malloc, wasm.__wbindgen_realloc);
    var len0 = WASM_VECTOR_LEN;
    getInt32Memory0()[arg0 / 4 + 1] = len0;
    getInt32Memory0()[arg0 / 4 + 0] = ptr0;
};

export function __wbindgen_throw(arg0, arg1) {
    throw new Error(getStringFromWasm0(arg0, arg1));
};

export function __wbindgen_rethrow(arg0) {
    throw takeObject(arg0);
};

export function __wbg_then_58a04e42527f52c6(arg0, arg1, arg2) {
    var ret = getObject(arg0).then(getObject(arg1), getObject(arg2));
    return addHeapObject(ret);
};

export function __wbg_then_a6860c82b90816ca(arg0, arg1) {
    var ret = getObject(arg0).then(getObject(arg1));
    return addHeapObject(ret);
};

export function __wbg_resolve_4f8f547f26b30b27(arg0) {
    var ret = Promise.resolve(getObject(arg0));
    return addHeapObject(ret);
};

export function __wbg_error_644d3bc8c0537e80(arg0, arg1, arg2, arg3) {
    console.error(getObject(arg0), getObject(arg1), getObject(arg2), getObject(arg3));
};

export function __wbg_warn_ca021eeadd0df9cd(arg0, arg1, arg2, arg3) {
    console.warn(getObject(arg0), getObject(arg1), getObject(arg2), getObject(arg3));
};

export function __wbg_info_8bed0988e7416289(arg0, arg1, arg2, arg3) {
    console.info(getObject(arg0), getObject(arg1), getObject(arg2), getObject(arg3));
};

export function __wbg_log_681299aef22afa27(arg0, arg1, arg2, arg3) {
    console.log(getObject(arg0), getObject(arg1), getObject(arg2), getObject(arg3));
};

export function __wbg_debug_6df4b1a327dd2e94(arg0, arg1, arg2, arg3) {
    console.debug(getObject(arg0), getObject(arg1), getObject(arg2), getObject(arg3));
};

export function __wbg_error_ca520cb687b085a1(arg0) {
    console.error(getObject(arg0));
};

export function __wbg_respond_3de33521af0cc48d(arg0, arg1) {
    getObject(arg0).respond(arg1 >>> 0);
};

export function __wbg_view_c4f26d2fe459fbbe(arg0) {
    var ret = getObject(arg0).view;
    return isLikeNone(ret) ? 0 : addHeapObject(ret);
};

export function __wbg_byteLength_1660d1ca53a8dc1d(arg0) {
    var ret = getObject(arg0).byteLength;
    return ret;
};

export function __wbg_close_1e7d15218d3841b0(arg0) {
    getObject(arg0).close();
};

export function __wbg_new_55259b13834a484c(arg0, arg1) {
    var ret = new Error(getStringFromWasm0(arg0, arg1));
    return addHeapObject(ret);
};

export function __wbg_buffer_91f3eb8fd33df09f(arg0) {
    var ret = getObject(arg0).buffer;
    return addHeapObject(ret);
};

export function __wbg_byteOffset_8b38bb7a6db8fca5(arg0) {
    var ret = getObject(arg0).byteOffset;
    return ret;
};

export function __wbg_byobRequest_f6341fff5655cedc(arg0) {
    var ret = getObject(arg0).byobRequest;
    return isLikeNone(ret) ? 0 : addHeapObject(ret);
};

export function __wbg_close_22a285b060f5f873(arg0) {
    getObject(arg0).close();
};

export function __wbg_enqueue_7b983d315f84999d(arg0, arg1) {
    getObject(arg0).enqueue(getObject(arg1));
};

export function __wbg_releaseLock_bf7c575037ad36b4() { return handleError(function (arg0) {
    getObject(arg0).releaseLock();
}, arguments) };

export function __wbg_bytesliteral_94cbaf79adf81aa1() {
    var ret = bytes_literal();
    return addHeapObject(ret);
};

export function __wbg_set_f9448486a94c9aef() { return handleError(function (arg0, arg1, arg2, arg3, arg4) {
    getObject(arg0).set(getStringFromWasm0(arg1, arg2), getStringFromWasm0(arg3, arg4));
}, arguments) };

export function __wbg_readyState_b3c4e9e7a3a58af3(arg0) {
    var ret = getObject(arg0).readyState;
    return ret;
};

export function __wbg_setonopen_c398a1a152e85bb6(arg0, arg1) {
    getObject(arg0).onopen = getObject(arg1);
};

export function __wbg_setonerror_5b2b08538f86d976(arg0, arg1) {
    getObject(arg0).onerror = getObject(arg1);
};

export function __wbg_setonclose_bcd7f603edae3db7(arg0, arg1) {
    getObject(arg0).onclose = getObject(arg1);
};

export function __wbg_setonmessage_7b6b02a417012ab3(arg0, arg1) {
    getObject(arg0).onmessage = getObject(arg1);
};

export function __wbg_close_dfed2f697da2eca4() { return handleError(function (arg0) {
    getObject(arg0).close();
}, arguments) };

export function __wbg_data_44aaea098b9f4c6b(arg0) {
    var ret = getObject(arg0).data;
    return addHeapObject(ret);
};

export function __wbg_instanceof_ArrayBuffer_649f53c967aec9b3(arg0) {
    var ret = getObject(arg0) instanceof ArrayBuffer;
    return ret;
};

export function __wbindgen_is_string(arg0) {
    var ret = typeof(getObject(arg0)) === 'string';
    return ret;
};

export function __wbg_instanceof_Blob_4c6e8fd441ac7315(arg0) {
    var ret = getObject(arg0) instanceof Blob;
    return ret;
};

export function __wbindgen_closure_wrapper1121(arg0, arg1, arg2) {
    var ret = makeMutClosure(arg0, arg1, 48, __wbg_adapter_30);
    return addHeapObject(ret);
};

export function __wbindgen_closure_wrapper1128(arg0, arg1, arg2) {
    var ret = makeMutClosure(arg0, arg1, 99, __wbg_adapter_33);
    return addHeapObject(ret);
};

export function __wbindgen_closure_wrapper1129(arg0, arg1, arg2) {
    var ret = makeMutClosure(arg0, arg1, 51, __wbg_adapter_36);
    return addHeapObject(ret);
};

export function __wbindgen_closure_wrapper1597(arg0, arg1, arg2) {
    var ret = makeMutClosure(arg0, arg1, 92, __wbg_adapter_39);
    return addHeapObject(ret);
};

