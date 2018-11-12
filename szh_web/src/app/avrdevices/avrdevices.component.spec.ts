import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AvrdevicesComponent } from './avrdevices.component';

describe('AvrdevicesComponent', () => {
  let component: AvrdevicesComponent;
  let fixture: ComponentFixture<AvrdevicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AvrdevicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AvrdevicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
