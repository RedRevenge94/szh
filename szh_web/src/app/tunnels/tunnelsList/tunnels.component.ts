import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';
import { TunnelInfo } from '../../models/tunnelInfo.model';
import { FrameComponent } from '../../ui/frame/frame.component';
import { RowFrame } from '../../models/ui_models/rowFrame.model';
import { FieldFrame, FieldFrameType } from '../../models/ui_models/fieldFrame.model';
import { TableComponent } from '../../ui/table/table.component';
import { RowTable } from '../../models/ui_models/rowTable.model';
import { FieldTable, FieldTableType } from '../../models/ui_models/fieldTable.model';
import { RoutingComponent } from '../../ui/routing/routing.component';

@Component({
  selector: 'app-tunnels',
  templateUrl: './tunnels.component.html',
  styleUrls: ['./tunnels.component.scss'],
  animations: [

    trigger('showingAnnimation', [
      transition('* => *', [
        query(':enter',style({opacity: 0}), {optional: true}),
        query(':enter',stagger('150ms',[
          animate('.6s ease-in',keyframes([
            style({opacity: 0,transform: 'translateY(-20%)',offset:0}),
            style({opacity: .5,transform: 'translateY(20px)',offset:.3}),
            style({opacity: 1,transform: 'translateY(0)',offset:1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})
export class TunnelsComponent implements OnInit {

  public loadingSpinner: LoadingSpinnerComponent;
  public ui_Frames: FrameComponent;

  private intervalId;
  private getTunnelsSubscription: ISubscription;
  private addNewTunnelSubscription: ISubscription;

  tunnels: TunnelInfo[];
  breedingShowingState: boolean[];

  displayedColumns: string[] = ['Id', 'Nazwa', 'Roślina', 'Ilość', 'Czas rozpoczęcia'];

  //Creating new tunnel
  newTunnelName: string;
  messageIsShowing: boolean;
  responseIsOk: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private _tunnelService: TunnelsService) {
    //this.route.params.subscribe(res => console.log(res.id));
  }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
    this.ui_Frames = new FrameComponent(this.router);
    this.getTunnels();
    this.intervalId = setInterval(() => { this.getTunnels(); }, 5000);
  }

  ngOnDestroy() {
    if (this.getTunnelsSubscription) {
      this.getTunnelsSubscription.unsubscribe();
    }
    if (this.addNewTunnelSubscription) {
      this.addNewTunnelSubscription.unsubscribe();
    }
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }

  sendMeHome() {
    this.router.navigate(['']);
  }

  getTunnels() {

    this.loadingSpinner.startLoadingDate = new Date();

    this.getTunnelsSubscription = this._tunnelService.getTunnelsInfo().subscribe(
      data => { this.tunnels = data },
      err => console.error('Erroren :' + err),
      () => {
        if (this.tunnels != null && this.breedingShowingState == null) {
          this.breedingShowingState = new Array(this.tunnels.length).fill(false);
        }

        let executionTime = this.loadingSpinner.GetLoadingTime();
        if(executionTime < this.loadingSpinner.minLoadingTime){
          setTimeout(()=>{this.loadingSpinner.loadingView = false; }, this.loadingSpinner.minLoadingTime - executionTime)
        }
      }
    );
  }

  showBreedings(id) {
    if (this.breedingShowingState != null) {
      this.breedingShowingState[id] = !this.breedingShowingState[id];
    }
  }

  onAddNewTunnelSubmit(tunnelName) {
    console.log(JSON.stringify(tunnelName));
    this.addNewTunnelSubscription = this._tunnelService.createTunnel(tunnelName.name).subscribe(
      data => { },
      err => { this.responseIsOk = false; this.messageIsShowing = true; },
      () => { this.responseIsOk = true; this.messageIsShowing = true; this.getTunnels(); }
    );
    
  }

  getTunnelDetailsLink(id) {
    return 'tunnels/' + id;
  }

  tunnelsInfoArrayToRowArray(){

    let arrayOfFrameRows = new Array<RowFrame>();

    for (var v in this.tunnels)
        {  
          let rowRight = new Array<FieldFrame>();
          let rowLeft = new Array<FieldFrame>();

          let tableComponent = new TableComponent(this.router);
          tableComponent.columns = ["Id","Nazwa uprawy","Roślina","Ilość","Data rozpoczęcia"];
          
          let arrayOfTableRows = new Array<RowTable>();
          for (var c in this.tunnels[v].cultivations){
            let row = new Array<FieldTable>();
            row.push(new FieldTable(this.tunnels[v].cultivations[c].id,FieldTableType.text,""));
            row.push(new FieldTable(this.tunnels[v].cultivations[c].name,FieldTableType.textLink,
              RoutingComponent.getCutlivationDetailsLink(this.tunnels[v].cultivations[c].id)));
            row.push(new FieldTable(this.tunnels[v].cultivations[c].plant.plantSpecies.name,FieldTableType.textLink,
              RoutingComponent.getPlantTunnelDetailsLink(this.tunnels[v].cultivations[c].plant.plantSpecies.id)));
            row.push(new FieldTable(this.tunnels[v].cultivations[c].pieces,FieldTableType.text,""));
            row.push(new FieldTable(this.tunnels[v].cultivations[c].start_date,FieldTableType.textDate,""));
            arrayOfTableRows.push(new RowTable(row));
          }
          tableComponent.data = arrayOfTableRows;

          rowLeft.push(new FieldFrame("Hodowle (" + this.tunnels[v].cultivations.length + "): ",FieldFrameType.text,""));
          rowLeft.push(new FieldFrame( tableComponent,FieldFrameType.tableComponent,""));
          rowLeft.push(new FieldFrame("Szczegóły tunelu -> ",FieldFrameType.textLink, this.getTunnelDetailsLink(this.tunnels[v].tunnel.id)));

          let temperature = "";
          if(this.tunnels[v].temperature == null){
            temperature = "---";
          } else {
            temperature = this.tunnels[v].temperature.toString();
          }

          rowRight.push(new FieldFrame(temperature + " °C",FieldFrameType.text,""));

          let alarmStringInfo = "";
          if(this.tunnels[v].isAlarm){
            alarmStringInfo = "❗";
          }
          let alarmInfo = new FieldFrame(alarmStringInfo,FieldFrameType.text,"");

          arrayOfFrameRows.push(new RowFrame(this.tunnels[v].tunnel.name,rowLeft,rowRight,alarmInfo));
        } 
    return arrayOfFrameRows;

  }

}
