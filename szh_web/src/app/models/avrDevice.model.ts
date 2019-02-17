import { Tunnel } from "./tunnel.model";

export class AvrDevice {

    id: number;
    ip: string;
    tunnel: Tunnel;
    last_update: Date

    constructor(
        _id: number,
        _ip: string,
        _tunnel: Tunnel,
        _last_update: Date
    ) { 
        this.id = _id;
        this.ip = _ip;
        this.tunnel = _tunnel;
        this.last_update = _last_update;
    }
}