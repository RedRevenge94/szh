import { AvrDevice } from "./avrDevice.model";

export class AvrDeviceInfo{

    constructor(
        public avrDevice:AvrDevice,
        public online: boolean
    ){}
}