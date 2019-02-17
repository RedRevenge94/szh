import { AvrDevice } from "./avrDevice.model";

export class AvrDeviceInfo{

    avrDevice: AvrDevice;
    online: boolean;

    constructor(_avrDevice:AvrDevice, _online: boolean){
        this.avrDevice = _avrDevice;
        this.online = _online;
    }
}