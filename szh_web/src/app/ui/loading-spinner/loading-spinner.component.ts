import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent implements OnInit {

  public minLoadingTime: number;
  public loadingView: boolean;

  public startLoadingDate: Date;
  public endLoadingDate: Date;

  constructor(  ) { 
    this.minLoadingTime = 500;
    this.loadingView = true;
  }

  ngOnInit() {
  }

  GetLoadingTime(){
    this.endLoadingDate = new Date();
    return this.endLoadingDate.getTime() - this.startLoadingDate.getTime();
  }

}
