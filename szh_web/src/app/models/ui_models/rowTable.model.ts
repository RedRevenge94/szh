import { FieldTable } from "./fieldTable.model";

export class RowTable {

    rowContent: FieldTable[];

    constructor(_rowContent:FieldTable[]){
        this.rowContent = _rowContent;
    }

}