import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { Chart } from 'chart.js';
import 'rxjs/add/operator/map';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';

@Component({
  selector: 'app-tunnel-details',
  templateUrl: './tunnel-details.component.html',
  styleUrls: ['./tunnel-details.component.scss'],
  animations: [

    trigger('tunnelsAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('300ms',[
          animate('1.5s ease-in',keyframes([
            style({opacity: 0,offset:0}),
            style({opacity: .3,offset:.6}),
            style({opacity: 1,offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class TunnelDetailsComponent implements OnInit {

  minLoadingTime = 500;
  loadingView: boolean = true;

  tunnelId;

  temperature;
  date;
  tempChart = [];

  startDateForm;
  endDateForm; 

  private getTemperatureSubscription: ISubscription;

  constructor(private route: ActivatedRoute, private _tunnelService: TunnelsService) {
    this.animationDuration = 1000;
    this.route.params.subscribe(res => this.tunnelId = res.id);

    let date = new Date();
    this.endDateForm = date.toISOString().split('T')[0];
    date.setDate(date.getDate() - 1);
    this.startDateForm = date.toISOString().split('T')[0];

    this.getTemperature();
  }

  private intervalId;
  ngOnInit() {
    this.intervalId = setInterval(() => { this.getTemperature(); }, 5000);
    this.animationDuration = 0;
  }

  ngOnDestroy(){
    if (this.getTemperatureSubscription) {
      this.getTemperatureSubscription.unsubscribe();
    }
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  start;
  getTemperature() {

    this.start = new Date().getTime();

    let dateRange = {
      "startDate": this.startDateForm,
      "endDate": this.endDateForm,
    };

    this.getTemperatureSubscription = this._tunnelService.getTemperatureForDateRange(this.tunnelId,dateRange).subscribe(
      data => { 
        this.temperature = data.map(data => data.value);
        
        console.log("hello worlds"); 
        this.date  = data.map(data => data.date_time)
       },
      err => console.error('Erroren :' + err),
      () => {
        this.makeTemperatureChart();       
      }
    );
  }

  onSetDateFilter(){
    this.getTemperature();
  }


  animationDuration;
  makeTemperatureChart(){

    var end = new Date().getTime();
    var executionTime = end - this.start;
    if(executionTime < this.minLoadingTime){
      setTimeout(()=>{this.loadingView = false; 
      
        this.tempChart = new Chart('canvas', {
          type: 'line',
          data: {
            labels: this.date,
            datasets: [
              {
                data: this.temperature,
                borderColor: '#446644',
                fill: false
              }
            ]
          },
          options: {
            legend: {
              display: false
            },
            animation:{
              duration: this.animationDuration
            },
            scales:{
              xAxes: [{
                type: 'time',
                time: {
                  displayFormats:{
                    hour: 'DD.MM HH:mm'
                  }
                },
                display: true,
                distribution: 'linear'
              }],
              yAxes: [{
                display: true
              }]
            }
          }
        })
      
      }, this.minLoadingTime - executionTime)
    }
  }

}
