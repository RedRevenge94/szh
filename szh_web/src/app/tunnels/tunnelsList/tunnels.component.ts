import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ISubscription } from 'rxjs/Subscription';
import { TunnelsService } from '../../services/tunnels.service';

@Component({
  selector: 'app-tunnels',
  templateUrl: './tunnels.component.html',
  styleUrls: ['./tunnels.component.scss']
})
export class TunnelsComponent implements OnInit {

  showSpinner: boolean = true;

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
    this.getTunnelsSubscription = this._tunnelService.getTunnelsInfo().subscribe(
      data => { this.tunnels = data },
      err => console.error('Erroren :' + err),
      () => {
        if (this.tunnels != null && this.breedingShowingState == null) {
          this.breedingShowingState = new Array(this.tunnels.length).fill(false);
        }
        this.showSpinner = false;
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
