import { Component, OnInit } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { AvrDevicesService } from '../services/avrDevices.service';
import { TunnelsService } from '../services/tunnels.service';

@Component({
  selector: 'app-avrdevices',
  templateUrl: './avrdevices.component.html',
  styleUrls: ['./avrdevices.component.scss']
})
export class AvrdevicesComponent implements OnInit {

  //getting data
  private intervalId;
  private getAvrDevicesSubscription: ISubscription;
  private getTunnelsSubscription: ISubscription;
  private addNewAvrDeviceSubscription: ISubscription;

  avrDeviceIpForm;
  avrDeviceTunnelForm;


  avrDevices;
  tunnels;

  //Creating new avr
  messageIsShowing: boolean;
  responseIsOk: boolean;

  constructor(
    private _avrDeviceInfoService: AvrDevicesService,
    private _tunnelsService: TunnelsService) { }

  ngOnInit() {
    this.getAvrDevices();
    this.getTunnels();
    this.intervalId = setInterval(() => { this.getAvrDevices(); }, 5000);
  }

  ngOnDestroy() {
    if (this.getAvrDevicesSubscription) {
      this.getAvrDevicesSubscription.unsubscribe();
    }
    if (this.getTunnelsSubscription) {
      this.getTunnelsSubscription.unsubscribe();
    }
    if (this.addNewAvrDeviceSubscription) {
      this.addNewAvrDeviceSubscription.unsubscribe();
    }
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  getAvrDevices(){
    this.getAvrDevicesSubscription = this._avrDeviceInfoService.getAvrDevicesInfo().subscribe(
      data => { this.avrDevices = data },
      err => console.error('Erroren :' + err),
      () => {
      }
    );
  }

  getTunnels(){
    this.getTunnelsSubscription = this._tunnelsService.getTunnels().subscribe(
      data => {this.tunnels = data},
      err => console.error(err),
      () => {}
    )
  }

  onAddNewAvrDeviceSubmit(avrDevice){

    avrDevice.tunnel = this.tunnels.find(x => x.id == avrDevice.tunnel)
    this.addNewAvrDeviceSubscription = this._avrDeviceInfoService.createAvrDevice(avrDevice).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.getAvrDevices();
  }

}
