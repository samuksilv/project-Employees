import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployessComponent } from './employess.component';

describe('EmployessComponent', () => {
  let component: EmployessComponent;
  let fixture: ComponentFixture<EmployessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
