import { FieldFrame } from "./fieldFrame.model";

export class RowFrame {

    constructor(public title:String, public rowLeftContent:FieldFrame[], public rowRightContent:FieldFrame[], public alarmInfo:FieldFrame ){}

}