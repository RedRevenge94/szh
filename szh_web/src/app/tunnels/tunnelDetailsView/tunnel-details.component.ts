import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { Chart } from 'chart.js';
import 'rxjs/add/operator/map';

@Component({
  selector: 'app-tunnel-details',
  templateUrl: './tunnel-details.component.html',
  styleUrls: ['./tunnel-details.component.scss']
})
export class TunnelDetailsComponent implements OnInit {

  tunnelId;

  temperature;
  date;
  tempChart = [];

  private getTemperatureSubscription: ISubscription;

  constructor(private route: ActivatedRoute, private _tunnelService: TunnelsService) {
    this.route.params.subscribe(res => this.tunnelId = res.id);
    this.getTemperature();
  }


  ngOnInit() {
    
  }

  getTemperature() {
    this.getTemperatureSubscription = this._tunnelService.getTemperatureForLastWeek(this.tunnelId).subscribe(
      data => { 
        this.temperature = data.map(data => data.value);
        this.date  = data.map(data => data.date_time)
       },
      err => console.error('Erroren :' + err),
      () => {
        this.makeTemperatureChart();
      }
    );
  }

  makeTemperatureChart(){
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
        scales:{
          xAxes: [{
            display: true
          }],
          yAxes: [{
            display: true
          }]
        }
      }
    })
  }

}
