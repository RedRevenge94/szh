export class CultivationComment {

    constructor(
        id: number,
        public text: string,
        cultivation: number,
        public create_date: Date
    ) { }
}