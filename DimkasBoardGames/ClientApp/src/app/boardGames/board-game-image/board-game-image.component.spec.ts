import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardGameImageComponent } from './board-game-image.component';

describe('BoardGameImageComponent', () => {
  let component: BoardGameImageComponent;
  let fixture: ComponentFixture<BoardGameImageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoardGameImageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoardGameImageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
