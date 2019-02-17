import { Tunnel } from "./tunnel.model";

export class NotificationInfo{

    public tunnel: Tunnel;
    public condition: string;
    public measurement_type: string;
    public value: number;
    public repeat_after: number;
    public receivers: string;
    public isActive: boolean;

}