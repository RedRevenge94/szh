import { Component, OnInit, Input } from '@angular/core';
import { RowFrame } from '../../models/ui_models/rowFrame.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-frame',
  templateUrl: './frame.component.html',
  styleUrls: ['./frame.component.scss']
})
export class FrameComponent implements OnInit {

  @Input() data: RowFrame[];

  constructor(private router: Router) { }

  ngOnInit() {}

  onNavigate(path: string){
    this.router.navigate([path]);
  }

}
