export enum FieldTableType {
    text = 0,
    textLink = 1,
    textDate = 2,
    textDateLong = 3,
    onlineStatus = 4    
}

export class FieldTable {

    value;
    fieldType: FieldTableType;
    link;

    constructor(_value:any, _fieldType: FieldTableType, _link: string){
        this.value = _value;
        this.fieldType = _fieldType;
        this.link = _link;
    }

}