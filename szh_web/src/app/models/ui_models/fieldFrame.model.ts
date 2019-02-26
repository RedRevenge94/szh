export enum FieldFrameType {
    text = 0,
    textLink = 1
}

export class FieldFrame {

    constructor(public value:any, public fieldType: FieldFrameType, public link: String){   
    }

}