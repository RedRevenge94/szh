import { Variety } from "./variety.model";
import { PlantSpecies } from "./PlantSpecies";

export class Plant {

    id: number;
    plantSpecies: PlantSpecies;
    variety: Variety;

    constructor(
        id: number,
        plantSpecies: PlantSpecies,
        variety: Variety
    ) {
     }
}