import { Cultivation } from "./cultivation.model";
import { Tunnel } from "./tunnel.model";

export class TunnelInfo{

    constructor(
        public tunnel:Tunnel,
        public cultivations: Cultivation[],
        public online: boolean,
        public temperature: number,
        public isAlarm: boolean
    ){}
}