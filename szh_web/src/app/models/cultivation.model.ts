import { Plant } from "./plant.model";
import { Tunnel } from "./tunnel.model";

export class Cultivation {

    public id: number;
    public name: string;
    public plant: Plant;
    public pieces: number;
    public tunnel: Tunnel;
    public start_date: Date;
    public end_date: Date;

}