export class Measurement{

    constructor(
        public id:number,
        public measurement_type:number,
        public avr_device:number,
        public value:number,
        public date_time: Date
    ){}
}