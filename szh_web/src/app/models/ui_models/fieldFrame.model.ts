export enum FieldFrameType {
    text = 0,
    textLink = 1,
    tableComponent = 5
}

export class FieldFrame {

    constructor(public value:any, public fieldType: FieldFrameType, public link: String){   
    }

}