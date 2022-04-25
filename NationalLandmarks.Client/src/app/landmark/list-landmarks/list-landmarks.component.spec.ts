import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListLandmarksComponent } from './list-landmarks.component';

describe('ListLandmarksComponent', () => {
  let component: ListLandmarksComponent;
  let fixture: ComponentFixture<ListLandmarksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListLandmarksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListLandmarksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
