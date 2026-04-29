import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderButton } from './header-button';

describe('HeaderButton', () => {
  let component: HeaderButton;
  let fixture: ComponentFixture<HeaderButton>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeaderButton]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeaderButton);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
