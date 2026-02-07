import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderAccount } from './header-account';

describe('HeaderAccount', () => {
  let component: HeaderAccount;
  let fixture: ComponentFixture<HeaderAccount>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeaderAccount]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeaderAccount);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
