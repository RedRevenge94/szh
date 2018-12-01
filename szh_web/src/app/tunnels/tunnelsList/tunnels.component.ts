import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';
import { trigger,style,transition,animate,keyframes,query,stagger } from '@angular/animations';
import { LoadingSpinnerComponent } from '../../ui/loading-spinner/loading-spinner.component';

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

  private loadingSpinner: LoadingSpinnerComponent;

  private intervalId;
  private getTunnelsSubscription: ISubscription;
  private addNewTunnelSubscription: ISubscription;

  tunnels;
  breedingShowingState: boolean[];

  displayedColumns: string[] = ['Id', 'Name', 'Plant', 'Pieces', 'Start date'];

  //Creating new tunnel
  newTunnelName: string;
  messageIsShowing: boolean;
  responseIsOk: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private _tunnelService: TunnelsService) {
    //this.route.params.subscribe(res => console.log(res.id));
  }

  ngOnInit() {
    this.loadingSpinner = new LoadingSpinnerComponent();
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
      () => { this.responseIsOk = true; this.messageIsShowing = true; }
    );
    this.getTunnels();
  }

  showTunnelDetails(id) {
    this.router.navigate(['tunnels/' + id]);
  }

}
