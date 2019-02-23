import { Cultivation } from "./cultivation.model";
import { CultivationComment } from "./cultivationComment.model";

export class CultivationInfo{

    cultivation: Cultivation;
    cultivationComments: CultivationComment[];
    online: boolean;

    constructor(cultivation: Cultivation,
        cultivationComments: CultivationComment[],
        online: boolean
    ){
        this.cultivation = cultivation;
        this.cultivationComments = cultivationComments;
        this.online = online;
    }
}