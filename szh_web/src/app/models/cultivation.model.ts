import { Plant } from "./plant.model";
import { Tunnel } from "./tunnel.model";
import { Variety } from "./variety.model";

export class Cultivation {

    id: number;
    name: string;
    plant: Plant;
    variety: Variety;
    pieces: number;
    tunnel: Tunnel;
    start_date: Date;
    end_date: Date;
}