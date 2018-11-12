import { Variety } from "./variety.model";

export class Plant {

id:number;
name:string;
varieties: Variety[];

    constructor(
        id: number,
        name: string,
        varieties: Variety[]
    ) {
        this.id = id;
        this.name = name;
        this.varieties = varieties;
     }
}