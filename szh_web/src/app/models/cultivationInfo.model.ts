import { Cultivation } from "./cultivation.model";
import { CultivationComment } from "./cultivationComment.model";

export class CultivationInfo{

    constructor(
        public cultivation: Cultivation,
        public cultivationComments: CultivationComment[],
        public online: boolean
    ){}
}