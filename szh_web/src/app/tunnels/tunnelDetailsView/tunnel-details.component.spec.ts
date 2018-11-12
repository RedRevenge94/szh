import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TunnelDetailsComponent } from './tunnel-details.component';

describe('TunnelDetailsComponent', () => {
  let component: TunnelDetailsComponent;
  let fixture: ComponentFixture<TunnelDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TunnelDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TunnelDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
