import { Component, OnInit } from '@angular/core';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { PlantsService } from '../../services/plants.service';
import { ISubscription } from 'rxjs/Subscription';
import { NotificationsService } from '../../services/notifications.service';
import { Router } from '@angular/router';
import { Tunnel } from '../../models/tunnel.model';
import { TunnelsService } from '../../services/tunnels.service';
import { TunnelInfo } from '../../models/tunnelInfo.model';
import { MeasurementType } from '../../models/measurementType.model';
import { MeasurementsService } from '../../services/measurements.service';
import { TableComponent } from '../../ui/table/table.component';
import { RowTable } from '../../models/ui_models/rowTable.model';
import { FieldTable, FieldTableType } from '../../models/ui_models/fieldTable.model';
import { NotificationInfo } from '../../models/notification.model';

@Component({
  selector: 'app-notification-configuration',
  templateUrl: './notification-configuration.component.html',
  styleUrls: ['./notification-configuration.component.scss'],
  animations: [

    trigger('showingAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('050ms',[
          animate('.4s ease-in',keyframes([
            style({opacity: 0,transform: 'translateY(-20%)',offset:0}),
            style({opacity: .5,transform: 'translateY(20px)',offset:.3}),
            style({opacity: 1,transform: 'translateY(0)',offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class NotificationConfigurationComponent implements OnInit {

  public loadingSpinner: LoadingSpinnerComponent;
  public ui_Table: TableComponent;

  notifications :NotificationInfo[];
  tunnels :TunnelInfo[];
  measurement_types: MeasurementType[];

  private intervalId;
  private getNotificationsSubscription: ISubscription;
  private getTunnelsSubscription: ISubscription;
  private getMeasurementSubscription: ISubscription;
  private addNotificationSubscription: ISubscription;

  //NgForm
  notificationIsActiveForm = true;

  constructor(
    private _notificationService: NotificationsService,
    private _tunnelsService: TunnelsService,
    private _measurementsService: MeasurementsService,
    private router: Router
    ) { }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
    this.ui_Table = new TableComponent(this.router);
    this.ui_Table.columns = ['Tunel','Właściwość', 'Warunek', 'Wartość', 'Adresy email', 'Ponawiaj co', 'Aktywny'];
    this.getNotifications();
    this.getTunnels();
    this.getMeasurementTypes();
  }

  ngOnDestroy(){
    if (this.getNotificationsSubscription) {
      this.getNotificationsSubscription.unsubscribe();
    }
    if (this.getTunnelsSubscription) {
      this.getTunnelsSubscription.unsubscribe();
    }
    if (this.getMeasurementSubscription) {
      this.getMeasurementSubscription.unsubscribe();
    }
    if (this.addNotificationSubscription) {
      this.addNotificationSubscription.unsubscribe();
    }
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }

  }

  getNotifications(){

    this.loadingSpinner.startLoadingDate = new Date();

    this.getNotificationsSubscription = this._notificationService.getNotifications().subscribe(
      data => { this.notifications = data },
      err => console.error('Erroren :' + err),
      () => {

        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
        }
      }
    );
  }

  responseIsOk;
  messageIsShowing;

  onAddNewNotificationSubmit(notification){
    console.log(notification); 
    this.addNotificationSubscription = this._notificationService.addNotification(notification).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; console.log(err);},
      () => { this.responseIsOk = true; this.messageIsShowing = true; this.getNotifications(); }
    );
    
  }

  onShowTunnelDetails(tunnelId){
    this.router.navigate(['tunnels/' + tunnelId]);
  }

  getTunnels(){
    this.getTunnelsSubscription = this._tunnelsService.getTunnels().subscribe(
      data => {this.tunnels = data},
      err => console.error(err),
      () => {}
    )
  }

  getMeasurementTypes(){
    this.getMeasurementSubscription = this._measurementsService.getMeasurementTypes().subscribe(
      data => {this.measurement_types = data},
      err => console.error(err),
      () => {}
    )
  }

  getTunnelDetailsLink(tunnelId){
    return 'tunnels/' + tunnelId;
  }

  notificationArrayToRowArray(){
    let arrayOfRows = new Array<RowTable>();

    for (var v in this.notifications)
        {  

          let row = new Array<FieldTable>();
          row.push(new FieldTable(this.notifications[v].tunnel.name,FieldTableType.textLink,
            this.getTunnelDetailsLink(this.notifications[v].tunnel.id)));
          row.push(new FieldTable(this.notifications[v].measurement_type,FieldTableType.text,""));
          row.push(new FieldTable(this.notifications[v].condition,FieldTableType.text,""));
          row.push(new FieldTable(this.notifications[v].value,FieldTableType.text,""));
          row.push(new FieldTable(this.notifications[v].receivers,FieldTableType.text,""));
          row.push(new FieldTable(this.notifications[v].repeat_after,FieldTableType.text,""));
          row.push(new FieldTable(this.notifications[v].isActive,FieldTableType.yesNoStatus,""));

          arrayOfRows.push(new RowTable(row));
        } 
        return arrayOfRows;
  }

}
