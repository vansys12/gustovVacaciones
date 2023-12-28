import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VacacionesAddEditComponent } from './vacaciones-add-edit.component';

describe('VacacionesAddEditComponent', () => {
  let component: VacacionesAddEditComponent;
  let fixture: ComponentFixture<VacacionesAddEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VacacionesAddEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VacacionesAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
