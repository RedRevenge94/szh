import { Plant } from "./plant.model";

export class Variety {

id:number;
name:string;
plant:Plant;

    constructor(
        id: number,
        name: string,
        plant: Plant
    ) {
        this.id = id;
        this.name = name;
        this.plant = plant;
     }
}