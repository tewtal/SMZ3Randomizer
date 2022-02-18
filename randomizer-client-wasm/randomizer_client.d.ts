/* tslint:disable */
/* eslint-disable */
/**
*/
export enum Message {
  ConsoleDisconnected,
  ConsoleReconnecting,
  ConsoleConnected,
  ConsoleError,
  GameState,
  ItemFound,
  ItemReceived,
  ItemsConfirmed,
}
/**
*/
export class ConsoleInterface {
  free(): void;
/**
*/
  static init(): void;
/**
* @param {string} proto
* @param {string | undefined} uri
*/
  constructor(proto: string, uri?: string);
/**
* @returns {Promise<any>}
*/
  connect(): Promise<any>;
/**
* @returns {Promise<any>}
*/
  disconnect(): Promise<any>;
/**
* @returns {Promise<any>}
*/
  list_devices(): Promise<any>;
/**
* @param {string} device
* @param {number} address
* @param {number} size
* @returns {Promise<any>}
*/
  read(device: string, address: number, size: number): Promise<any>;
/**
* @param {string} device
* @param {Uint32Array} address_info
* @returns {Promise<any>}
*/
  read_multi(device: string, address_info: Uint32Array): Promise<any>;
/**
* @param {string} device
* @param {number} address
* @param {Uint8Array} data
* @returns {Promise<any>}
*/
  write(device: string, address: number, data: Uint8Array): Promise<any>;
/**
* @param {string} device
* @param {Uint32Array} addresses
* @param {(Uint8Array)[]} data
* @returns {Promise<any>}
*/
  write_multi(device: string, addresses: Uint32Array, data: (Uint8Array)[]): Promise<any>;
}
/**
*/
export class IntoUnderlyingByteSource {
  free(): void;
/**
* @param {any} controller
*/
  start(controller: any): void;
/**
* @param {any} controller
* @returns {Promise<any>}
*/
  pull(controller: any): Promise<any>;
/**
*/
  cancel(): void;
/**
* @returns {number}
*/
  readonly autoAllocateChunkSize: number;
/**
* @returns {any}
*/
  readonly type: any;
}
/**
*/
export class IntoUnderlyingSink {
  free(): void;
/**
* @param {any} chunk
* @returns {Promise<any>}
*/
  write(chunk: any): Promise<any>;
/**
* @returns {Promise<any>}
*/
  close(): Promise<any>;
/**
* @param {any} reason
* @returns {Promise<any>}
*/
  abort(reason: any): Promise<any>;
}
/**
*/
export class IntoUnderlyingSource {
  free(): void;
/**
* @param {any} controller
* @returns {Promise<any>}
*/
  pull(controller: any): Promise<any>;
/**
*/
  cancel(): void;
}
/**
* Raw options for [`pipeTo()`](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream/pipeTo).
*/
export class PipeOptions {
  free(): void;
/**
* @returns {boolean}
*/
  readonly preventAbort: boolean;
/**
* @returns {boolean}
*/
  readonly preventCancel: boolean;
/**
* @returns {boolean}
*/
  readonly preventClose: boolean;
/**
* @returns {AbortSignal | undefined}
*/
  readonly signal: AbortSignal | undefined;
}
/**
*/
export class QueuingStrategy {
  free(): void;
/**
* @returns {number}
*/
  readonly highWaterMark: number;
}
/**
*/
export class RandomizerClient {
  free(): void;
/**
*/
  static init(): void;
/**
* @param {string} session_uri
* @param {string} session_guid
* @param {Function} callback
*/
  constructor(session_uri: string, session_guid: string, callback: Function);
/**
* @returns {Promise<any>}
*/
  initialize(): Promise<any>;
/**
* @returns {Promise<any>}
*/
  get_session_data(): Promise<any>;
/**
* @param {number} world_id
* @returns {Promise<any>}
*/
  register_player(world_id: number): Promise<any>;
/**
* @returns {Promise<any>}
*/
  unregister_player(): Promise<any>;
/**
* @param {string} client_guid
* @returns {Promise<any>}
*/
  login_player(client_guid: string): Promise<any>;
/**
* @returns {Promise<any>}
*/
  get_client_data(): Promise<any>;
/**
* @returns {Promise<any>}
*/
  get_patch(): Promise<any>;
/**
* @returns {Promise<any>}
*/
  list_devices(): Promise<any>;
/**
* @param {Int32Array} event_types
* @param {number | undefined} from_event_id
* @param {number | undefined} to_event_id
* @param {number | undefined} from_world_id
* @param {number | undefined} to_world_id
* @returns {Promise<any>}
*/
  get_events(event_types: Int32Array, from_event_id?: number, to_event_id?: number, from_world_id?: number, to_world_id?: number): Promise<any>;
/**
* @param {number} from_event_id
* @param {Int32Array} event_types
* @returns {Promise<any>}
*/
  get_report(from_event_id: number, event_types: Int32Array): Promise<any>;
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
  send_event(event_type: number, to_world_id: number, item_id: number, item_location: number, sequence_num: number, confirmed: boolean, message: string): Promise<any>;
/**
* @returns {Promise<any>}
*/
  forfeit(): Promise<any>;
/**
* @param {string} device
* @returns {Promise<any>}
*/
  start(device: string): Promise<any>;
/**
* @returns {Promise<any>}
*/
  update(): Promise<any>;
}
/**
* Raw options for [`getReader()`](https://developer.mozilla.org/en-US/docs/Web/API/ReadableStream/getReader).
*/
export class ReadableStreamGetReaderOptions {
  free(): void;
/**
* @returns {any}
*/
  readonly mode: any;
}
